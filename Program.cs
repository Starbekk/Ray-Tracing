using System;

namespace Ray_Tracing;

public class Image
{
    static double hit_sphere(Vec3 center, double radius, Ray r)
    {
        Vec3 oc = center - r.Origin;
        var a = r.Direction.LengthSquared();
        var h = Vec3.Dot(r.Direction, oc);
        var c = oc.LengthSquared() - radius * radius;
        var discriminant = h * h - a * c;
        if (discriminant < 0)
        {
            return -1.0;
        }
        else
        {
            return (h - Math.Sqrt(discriminant)) / a;
        }
    }
    static Vec3 RayColor(Ray r)
    {
        var t = hit_sphere(new Vec3(0,0,-1), 0.5, r);

        if (t > 0.0){
            Vec3 N = Vec3.UnitVector(r.At(t) - new Vec3(0, 0, -1));
            return 0.5* new Vec3(N.x + 1, N.y + 1, N.z + 1 )    ;
        }
        Vec3 unit_Direction = Vec3.UnitVector(r.Direction);
        var a = 0.5 * (unit_Direction.y + 1.0);

        return (1.0 - a) * new Vec3(1.0, 1.0, 1.0) + a * new Vec3(0.5, 0.7, 1.0);
    }
    static void Main()
    {
        var aspect_Ratio = 16.0 / 9.0;
        int image_Width = 400;
        int image_Height = (int)(image_Width / aspect_Ratio);
        if(image_Height < 1)
        {
            image_Height = 1;
        }

        var focal_length = 1.0;
        var viewport_Height = 2.0;
        var viewport_Width = viewport_Height * ((double)(image_Width) / image_Height);
        var camera_Center = new Vec3(0, 0, 0);

        var viewport_u = new Vec3(viewport_Width, 0, 0);
        var viewport_v = new Vec3(0, -viewport_Height, 0);

        var pixel_delta_u = viewport_u / image_Width;
        var pixel_delta_v = viewport_v / image_Height;

        var viewport_upper_left = camera_Center - new Vec3(0, 0, focal_length) - viewport_u / 2 - viewport_v / 2;
        var pixel00_loc = viewport_upper_left + 0.5 * (pixel_delta_u + pixel_delta_v);
        Console.WriteLine("P3");
        Console.WriteLine($"{image_Width} {image_Height}");
        Console.WriteLine("255");
        for (int j = 0; j < image_Height; j++)
        {
            Console.Error.Write($"\rScanlines remaining: {image_Height-j}  ");
            for (int i = 0; i < image_Width; i++)
            {
                var pixel_center = pixel00_loc + (i * pixel_delta_u) + (j * pixel_delta_v);
                var ray_Direction = pixel_center - camera_Center;
                Ray r = new Ray(camera_Center, ray_Direction);
                Vec3 pixelColor = RayColor(r);
                Color.WriteColor(Console.Out,  pixelColor);
            }
        }
        Console.Error.WriteLine();
        Console.Error.Write($"\rDone.       \n");
    }
}

