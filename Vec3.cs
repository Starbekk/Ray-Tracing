using System;
namespace Ray_Tracing;

public readonly struct Vec3
{
    public readonly double x;
    public readonly double y;
    public readonly double z;

    public Vec3(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public static Vec3 zero => new Vec3(0, 0, 0);
    public static Vec3 operator -(Vec3 v) => new Vec3(-v.x, -v.y, -v.z);
    public double this[int index]
    {
        get
        {
            return index switch {
                0 => x,
                1 => y,
                2 => z,
                _ => throw new IndexOutOfRangeException("Vec3 index must be 0, 1, or 2")

            };

        }
    }
    public double Length() => Math.Sqrt(LengthSquared());
    public double LengthSquared() => x * x + y * y + z * z;
    public bool NearZero()
    {
        var s = 1e-8;
        return (Math.Abs(x) < s) && (Math.Abs(y) < s) && (Math.Abs(z) < s);
    }
    public static Vec3 Random() => new Vec3(Rtweekend.RandomDouble(), Rtweekend.RandomDouble(), Rtweekend.RandomDouble());
    public static Vec3 Random(double Min,  double Max) => new Vec3(Rtweekend.RandomDouble(Min, Max), Rtweekend.RandomDouble(Min, Max), Rtweekend.RandomDouble(Min, Max));
    public static Vec3 operator +(Vec3 v1, Vec3 v2) => new Vec3(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
    public static Vec3 operator -(Vec3 v1, Vec3 v2) => new Vec3(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
    public static Vec3 operator *(Vec3 v1, Vec3 v2) => new Vec3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
    public static Vec3 operator *(Vec3 v, double s) => new Vec3(v.x * s, v.y * s, v.z * s);
    public static Vec3 operator *(double s, Vec3 v) => v * s;
    public static Vec3 operator /(Vec3 v, double s) => (1.0 / s) * v;

    public static double Dot(Vec3 v1, Vec3 v2) => v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;
    public static Vec3 Cross(Vec3 v1, Vec3 v2) => new Vec3(
        v1.y * v2.z - v1.z * v2.y,
        v1.z * v2.x - v1.x * v2.z,
        v1.x * v2.y - v1.y * v2.x
        );
    public static Vec3 UnitVector(Vec3 v) => v / v.Length();
    public static Vec3 RandomUnitVector()
    {
        while (true)
        {
            var p = Random(-1, 1);
            var lensq = p.LengthSquared();
            if(1e-160< lensq && lensq <= 1)
            {
                return p/Math.Sqrt(lensq);
            }
        }
    }
    public static Vec3 RandomInUnitDisk()
    {
        while (true)
        {
            var p = new Vec3(Rtweekend.RandomDouble(-1, 1), Rtweekend.RandomDouble(-1, 1), 0);
            if (p.LengthSquared() < 1)
            {
                return p;
            }
        }
    }
    public static Vec3 RandomOnHemisphere(Vec3 Normal)
    {
        Vec3 OnUnitSphere = RandomUnitVector();
        if(Vec3.Dot(OnUnitSphere, Normal) > 0.0)
        {
            return OnUnitSphere;
        }
        else
        {
            return -OnUnitSphere;
        }
    }
    public static Vec3 Reflect(Vec3 v, Vec3 n)
    {
        return v - 2 * Dot(v, n) * n;
    }

    public static Vec3 Refract(Vec3 uv, Vec3 n, double etai_over_etat)
    {
        var cos_theta = Math.Min(Dot(-uv, n), 1.0);
        Vec3 r_out_perp = etai_over_etat * (uv + cos_theta * n);
        Vec3 r_out_parallel = -Math.Sqrt(Math.Abs(1.0 - r_out_perp.LengthSquared())) * n;
        return r_out_perp + r_out_parallel;
    }
    public override string ToString() => $"{x} {y} {z}";
}