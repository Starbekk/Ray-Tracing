using System;
namespace Ray_Tracing;
using Colorr = Vec3;

public static class Color
{
   public static void WriteColor(TextWriter output, Colorr pixelColor)
    {
        double r = pixelColor.x;
        double g = pixelColor.y;
        double b = pixelColor.z;
        int rbyte = (int)(255.999 * r);
        int gbyte = (int)(255.999 * g);
        int bbyte = (int)(255.999 * b);
        output.WriteLine($"{rbyte} {gbyte} {bbyte}");
    }
}