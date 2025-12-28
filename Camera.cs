using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray_Tracing;

public class Camera
{
    public double aspect_Ratio = 1.0;
    public int ImageWidth = 100;
    public int samples_per_pixel = 10;
    public int MaxDepth = 10;
    public double vfov = 90;
    public Vec3 lookfrom = new Vec3(0, 0, 0);
    public Vec3 lookat = new Vec3(0, 0, -1);
    public Vec3 vup = new Vec3(0, 1, 0);
    public double defocus_angle = 0;
    public double focus_dist = 10;

    private int ImageHeight;
    private Vec3 Center;
    private Vec3 pixel00Loc;
    private Vec3 pixelDeltau;
    private Vec3 pixelDeltav;
    private double pixel_samples_scale;
    private Vec3 u;
    private Vec3 v;
    private Vec3 w;
    private Vec3 defocus_disk_u;
    private Vec3 defocus_disk_v;
    
    public void Render(IHittable world)
    {
        Initialize();
        Console.WriteLine("P3");
        Console.WriteLine($"{ImageWidth} {ImageHeight}");
        Console.WriteLine("255");
        string[] rows = new string[ImageHeight];
        Parallel.For(0, ImageHeight, j =>
        {
            var sb = new StringBuilder(ImageWidth * 12);
            for (int i = 0; i < ImageWidth; i++)
            {
                Vec3 pixelColor = new Vec3(0, 0, 0);
                for (int sample = 0; sample < samples_per_pixel; sample++)
                {
                    Ray r = GetRay(i, j);

                    pixelColor += RayColor(r, MaxDepth, world);
                }
                pixelColor *= pixel_samples_scale;
                sb.Append(Colorr.WriteColorr(pixelColor));
            }
            rows[j] = sb.ToString();
        });
        for(int j = 0; j < ImageHeight; j++)
        {
            Console.Write(rows[j]);
        }
        Console.Error.Write("\nDone.");
    }
    private void Initialize()
    {
        ImageHeight = (int)(ImageWidth / aspect_Ratio);
        if (ImageHeight < 1)
        {
            ImageHeight = 1;
        }
        pixel_samples_scale = 1.0 / samples_per_pixel;
        Center = lookfrom;
        
        var theta = Rtweekend.DegreesToRadians(vfov);
        var h = Math.Tan(theta / 2);
        double viewportHeight = 2.0 * h * focus_dist;
        double viewportWidth = viewportHeight * ImageWidth / ImageHeight;

        w = Vec3.UnitVector(lookfrom - lookat);
        u = Vec3.UnitVector(Vec3.Cross(vup, w));
        v = Vec3.Cross(w, u);
        Vec3 viewportu = viewportWidth * u;
        Vec3 viewportv = viewportHeight * -v;
        pixelDeltau = viewportu / ImageWidth;
        pixelDeltav = viewportv / ImageHeight;
        var viewportUpperLeft = Center - (focus_dist* w) - viewportu/2 - viewportv/2;
        pixel00Loc = viewportUpperLeft + 0.5 * (pixelDeltau+pixelDeltav);
        var defocus_radius = focus_dist * Math.Tan(Rtweekend.DegreesToRadians(defocus_angle / 2));
        defocus_disk_u = u * defocus_radius;
        defocus_disk_v = v * defocus_radius;
    }
    private Ray GetRay(int i, int j)
    {
        Vec3 offset = SampleSquare();
        Vec3 pixelSample = pixel00Loc + (i+offset.x)*pixelDeltau + (j + offset.y)*pixelDeltav;
        Vec3 rayOrigin = (defocus_angle <= 0) ? Center : defocus_disk_sample();
        Vec3 rayDirection = pixelSample - rayOrigin;
        return new Ray(rayOrigin, rayDirection);
    }
    private Vec3 SampleSquare()
    {
        return new Vec3(Rtweekend.RandomDouble() - 0.5, Rtweekend.RandomDouble() - 0.5, 0);
    }
    private Vec3 defocus_disk_sample()
    {
        var p = Vec3.RandomInUnitDisk();
        return Center + (p.x * defocus_disk_u) + (p.y * defocus_disk_v);
    }
    private Vec3 RayColor(Ray r, int depth, IHittable world)
    {
        if(depth <= 0)
        {
            return new Vec3(0, 0, 0);
        }
        hit_record rec = new hit_record();
        if (world.Hit(r, new Interval(0.001, Rtweekend.Infinity), ref rec))
        {
            Ray scattered = new Ray();
            Vec3 attenuation = new Vec3();
            if (rec.mat.Scatter(r, rec, ref attenuation, ref scattered))
            {
                return attenuation * RayColor(scattered, depth - 1, world);
            }
            return new Vec3(0, 0, 0);

        }
        Vec3 unit_Direction = Vec3.UnitVector(r.Direction);
        var a = 0.5 * (unit_Direction.y + 1.0);

        return (1.0 - a) * new Vec3(1.0, 1.0, 1.0) + a * new Vec3(0.5, 0.7, 1.0);
    }
}
