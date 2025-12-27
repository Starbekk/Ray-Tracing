using System;

namespace Ray_Tracing;

public class Image
{
    static void Main()
    {
 
        HittableList world = new HittableList();

        var material_ground = new Lambertian(new Vec3(0.5,0.5,0.5));
        world.Add(new Sphere(new Vec3(0, -1000, 0), 1000, material_ground));

        for(int a = -11;  a < 11; a++)
        {
            for(int b = -11; b < 11; b++)
            {
                var choosemat = Rtweekend.RandomDouble();
                Vec3 Center = new Vec3(a+0.9*Rtweekend.RandomDouble(), 0.2, b+0.9*Rtweekend.RandomDouble());
                if((Center - new Vec3(4,0.2, 0)).Length() > 0.9)
                {
                    Material sphere_material;
                    if(choosemat < 0.8)
                    {
                        var albedo = Vec3.Random() * Vec3.Random();
                        sphere_material = new Lambertian(albedo);
                        world.Add(new Sphere(Center, 0.2, sphere_material));
                    }
                    else if(choosemat < 0.95)
                    {
                        Vec3 albedo = Vec3.Random(0.5, 1.0);
                            double fuzz = Rtweekend.RandomDouble(0.0, 0.5);
                        sphere_material = new Metal(albedo, fuzz);
                        world.Add(new Sphere(Center, 0.2, sphere_material));
                    }
                    else
                    {
                        sphere_material = new Dielectric(1.5);
                        world.Add(new Sphere(Center, 0.2, sphere_material));
                    }
                }
       
            }
        }
        Material material1 = new Dielectric(1.5);
        world.Add(new Sphere(new Vec3(0, 1, 0), 1.0, material1));
        Material material2 = new Lambertian(new Vec3(0.4,0.2,0.1));
        world.Add(new Sphere(new Vec3(-4, 1, 0), 1.0, material2));
        Material material3 = new Metal(new Vec3(0.7, 0.6, 0.5), 0.0);
        world.Add(new Sphere(new Vec3(4, 1, 0), 1.0, material3));
        Camera cam = new Camera();
        cam.aspect_Ratio = 16.0 / 9.0;
        cam.ImageWidth = 1200;
        cam.samples_per_pixel = 500;
        cam.MaxDepth = 50;
        cam.vfov = 20;
        cam.lookfrom = new Vec3(13,2,3);
        cam.lookat = new Vec3(0, 0, 0);
        cam.vup = new Vec3(0, 1, 0);
        cam.defocus_angle = 0.6;
        cam.focus_dist = 10.0;
        cam.Render(world);
    }
}

