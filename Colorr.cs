using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray_Tracing;

public static class Colorr
{
    public static string WriteColorr(Vec3 pixelColor)
    {
        double r = pixelColor.x;
        double g = pixelColor.y;
        double b = pixelColor.z;

        r = r > 0 ? Math.Sqrt(r) : 0;
        g = g > 0 ? Math.Sqrt(g) : 0;
        b = b > 0 ? Math.Sqrt(b) : 0;

        Interval intensity = new Interval(0.000, 0.999);

        int rbyte = (int)(256 * intensity.Clamp(r));
        int gbyte = (int)(256 * intensity.Clamp(g));
        int bbyte = (int)(256 * intensity.Clamp(b));
        return $"{rbyte} {gbyte} {bbyte}\n";
    }
}