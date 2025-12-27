using System;
namespace Ray_Tracing;
using Colorr = Vec3;

public static class Color
{
    public static double linear_to_gamma(double linear_component)
    {
        if(linear_component > 0)
        {
            return Math.Sqrt(linear_component); 
        }
        return 0;
    }
   public static void WriteColor(TextWriter output, Colorr pixelColor)
    {
        double r = pixelColor.x;
        double g = pixelColor.y;
        double b = pixelColor.z;

        r = linear_to_gamma(r);
        g = linear_to_gamma(g);
        b = linear_to_gamma(b);

        Interval intensity = new Interval(0.000, 0.999);

        int rbyte = (int)(256 * intensity.Clamp(r));
        int gbyte = (int)(256 * intensity.Clamp(g));
        int bbyte = (int)(256 * intensity.Clamp(b));
        output.WriteLine($"{rbyte} {gbyte} {bbyte}");
    }
}