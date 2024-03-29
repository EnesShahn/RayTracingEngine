﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace EnesShahn
{
    namespace RayTracingEngine
    {
        class Renderer
        {
            private static string outputFolder;
            private static string sceneName;
            private static Scene scene;
            private static float near;
            private static float far;

            private static List<Light> lights;
            private static List<Mesh> meshes;

            private static float weightReduction = 0.05f;

            public static void SetRenderer(string outputFolder, string sceneName, Scene scene, float near, float far)
            {
                Renderer.outputFolder = outputFolder;
                Renderer.sceneName = sceneName;
                Renderer.scene = scene;
                Renderer.near = near;
                Renderer.far = far;
                Renderer.lights = new List<Light>();
                Renderer.meshes = new List<Mesh>();

                for (int i = 0; i < scene.root.ChildCount(); i++)
                {
                    Object3D obj = scene.root.GetChild(i);
                    if (obj.HasComponent<Light>())
                        lights.Add(obj.GetComponent<Light>());
                    if (obj.HasComponent<Mesh>())
                        meshes.Add(obj.GetComponent<Mesh>());
                }
            }

            public static Color01 TraceRay(Ray ray, Hit hit, float weight, int depth)
            {
                float prevIOR = hit.Material != null ? hit.Material.indexOfRefraction : 1f;

                if (depth >= Settings.MAX_BOUNCE)
                    return new Color01(0, 0, 0, 1);

                foreach (Mesh mesh in meshes)
                    mesh.Intersect(ray, hit, 0.0001f);

                if (!hit.Intersection)
                    return scene.Background;

                Color01 fColor = scene.ambientLightColor * hit.Material.diffuseColor; //Init with ambient color

                //Shading Calculation
                foreach (Light light in lights)
                {
                    Vector3 shadowRayDir = -light.GetDirection();
                    Ray shadowRay = new Ray(hit.Point, shadowRayDir);
                    Hit shadowHit = new Hit();
                    foreach (Mesh mesh in meshes)
                        mesh.Intersect(shadowRay, shadowHit, 0.0001f);

                    if (shadowHit.Intersection) // Light is blocked
                        continue;

                    //Light is not blocked so calculate the shading
                    fColor += hit.Material.Shade(ray.Direction, light, hit.Normal, hit.Point);
                }

                #region Reflection Ray
                Vector3 R = Vector3.Reflect(ray.Direction, hit.Normal);
                Ray reflectionRay = new Ray(hit.Point, R);
                fColor += hit.Material.reflectiveColor * TraceRay(reflectionRay, new Hit(), weight - weightReduction, depth + 1);
                #endregion

                #region Refractive Ray
                Vector3 refractDir = Vector3.Refract(ray.Direction, hit.Normal, prevIOR, hit.Material.indexOfRefraction);
                Hit refractedHit = new Hit();
                refractedHit.Material = hit.Material;
                Ray refractedRay = new Ray(hit.Point, refractDir);
                fColor += hit.Material.transparentColor * TraceRay(refractedRay, refractedHit, weight - weightReduction, depth + 1);
                #endregion

                return weight * fColor;
            }

            public static void RenderToImage()
            {
                Console.WriteLine($"Rendering \"{sceneName}\" ...");
                Stopwatch sw = new Stopwatch();
                sw.Start();
                Bitmap bmp = new Bitmap(Settings.IMAGE_RESOLUTION.x, Settings.IMAGE_RESOLUTION.y);
                Bitmap bmpDepth = new Bitmap(Settings.IMAGE_RESOLUTION.x, Settings.IMAGE_RESOLUTION.y);

                SetBGColor(bmp, scene.Background);
                SetBGColor(bmpDepth, new Color01(0, 0, 0));

                //Multipication faster than division so we cache it instead of dividing in the loop
                double oneDivResX = 1f / Settings.IMAGE_RESOLUTION.x;
                double oneDivResY = 1f / Settings.IMAGE_RESOLUTION.y;

                Camera mainCamera = scene.MainCamera.GetComponent<Camera>();

                for (int y = 0; y < Settings.IMAGE_RESOLUTION.y; y++)
                {
                    for (int x = 0; x < Settings.IMAGE_RESOLUTION.x; x++)
                    {
                        //screenCoord goes from 0,0 to 1,1 (Screen Coord) x, y = 0 : lower left corner, = 1 : top right corner.
                        Vector2 screenCoord = new Vector2((float)(x * oneDivResX), (float)(y * oneDivResY));

                        //Primary Ray Intersection Test
                        Ray primaryRay = mainCamera.GenerateRay(screenCoord);
                        Hit primaryHit = new Hit();
                        Color01 fColor = TraceRay(primaryRay, primaryHit, 1f, 0);

                        if (primaryHit.Intersection)
                        {
                            //For Depth rendering
                            float depth01 = (far - primaryHit.TCurrent) / (far - near);
                            int depthRGB = Mathf.Clamp(0, 255, (int)((depth01) * 255));

                            // Bitmap sets pixels from top left corner to lower right corner, thats why we need to invert the y or else image will be flipped on the x axis
                            bmp.SetPixel(x, Settings.IMAGE_RESOLUTION.y - y - 1, Color.FromArgb((int)(fColor.R * 255), (int)(fColor.G * 255), (int)(fColor.B * 255)));
                            bmpDepth.SetPixel(x, Settings.IMAGE_RESOLUTION.y - y - 1, Color.FromArgb(depthRGB, depthRGB, depthRGB));
                        }
                    }
                }

                bmp.Save($"{outputFolder}/{sceneName}.png", ImageFormat.Png);
                bmpDepth.Save($"{outputFolder}/{sceneName}_depth.png", ImageFormat.Png);

                sw.Stop();
                Console.WriteLine($"Finished Rendering \"{sceneName}\", it took {sw.ElapsedMilliseconds / 1000f} seconds.");
            }
            public static void SetBGColor(Bitmap bmp, Color01 color)
            {
                for (int y = 0; y < Settings.IMAGE_RESOLUTION.y; y++)
                {
                    for (int x = 0; x < Settings.IMAGE_RESOLUTION.x; x++)
                    {
                        bmp.SetPixel(x, y, Color.FromArgb((int)(color.R * 255), (int)(color.G * 255), (int)(color.B * 255)));
                    }
                }
            }

        }
    }
}