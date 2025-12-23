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
    public override string ToString() => $"{x} {y} {z}";
}