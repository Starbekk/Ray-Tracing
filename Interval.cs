using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray_Tracing;

public readonly struct Interval
{
    public double Min { get; }
    public double Max { get; }
    public Interval()
    {
        Min = double.PositiveInfinity;
        Max = double.NegativeInfinity;
    }
    public Interval(double min, double max)
    {
        Min = min;
        Max = max;
    }
    public bool Contains(double x)
    {
        return Min <= x && x <= Max; 
    }
    public bool Surrounds(double x)
    {
        return (Min < x && x < Max);
    }
    public double Clamp(double x)
    {
        if (x < Min)
        {
            return Min;
        }
        if (x > Max)
        {
            return Max;
        }
        return x;
    }
    public static Interval Empty => new Interval(double.PositiveInfinity, double.NegativeInfinity);
    public static Interval Universe => new Interval(double.NegativeInfinity, double.PositiveInfinity);
}