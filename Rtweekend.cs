using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray_Tracing;

public readonly struct Rtweekend
{
    public static readonly double Pi = Math.PI;
    public static readonly double Infinity = double.PositiveInfinity;
    public static double DegreesToRadians(double d) => d * Pi / 180.0;
    private static readonly Random rng = new Random();
    public static double RandomDouble() => rng.NextDouble();
    public static double RandomDouble(double Min, double Max) => Min + (Max - Min) * RandomDouble();
}