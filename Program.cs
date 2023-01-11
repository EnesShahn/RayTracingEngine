using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using EnesShahn.RayTracingEngine;

class Program
{
    static void Main(string[] args)
    {
        if (!Directory.Exists("Output"))
            Directory.CreateDirectory("Output");

        Scene scene1 = SceneManager.ParseSceneData("Resources/scene1_exponent_variations.json");
        Renderer.SetRenderer("Output", "scene1_exponent_variations", scene1, 8, 11.5f);
        Renderer.RenderToImage();

        Scene scene2 = SceneManager.ParseSceneData("Resources/scene2_plane_sphere.json");
        Renderer.SetRenderer("Output", "scene2_plane_sphere", scene2, 8, 11.5f);
        Renderer.RenderToImage();

        Scene scene3 = SceneManager.ParseSceneData("Resources/scene3_colored_lights.json");
        Renderer.SetRenderer("Output", "scene3_colored_lights", scene3, 8, 11.5f);
        Renderer.RenderToImage();

        Scene scene4 = SceneManager.ParseSceneData("Resources/scene4_reflective_sphere.json");
        Renderer.SetRenderer("Output", "scene4_reflective_sphere", scene4, 8, 11.5f);
        Renderer.RenderToImage();

        Scene scene5 = SceneManager.ParseSceneData("Resources/scene5_transparent_sphere.json");
        Renderer.SetRenderer("Output", "scene5_transparent_sphere", scene5, 8, 11.5f);
        Renderer.RenderToImage();

        Scene scene6 = SceneManager.ParseSceneData("Resources/scene6_transparent_sphere2.json");
        Renderer.SetRenderer("Output", "scene6_transparent_sphere2", scene6, 8, 11.5f);
        Renderer.RenderToImage();
    }
}