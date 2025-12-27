using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray_Tracing;

public abstract class Material
{
    public abstract bool Scatter(Ray rIn, hit_record rec, ref Vec3 attenuation, ref Ray scattered);
}

public class Lambertian: Material
{
    private Vec3 Albedo;
    public Lambertian(Vec3 albedo)
    {
        Albedo = albedo;
    }
    public override bool Scatter(Ray rIn, hit_record rec, ref Vec3 attenuation,ref Ray scattered)
    {
        var scatter_direction = rec.normal + Vec3.RandomUnitVector();
        if (scatter_direction.NearZero())
        {
            scatter_direction = rec.normal;
        }
        scattered = new Ray(rec.p, scatter_direction);
        attenuation = Albedo;
        return true;
    }

}
public class Metal : Material
{
    private Vec3 Albedo;
    private double Fuzz;
    public Metal(Vec3 albedo, double fuzz)
    {
        Albedo = albedo;
        Fuzz = fuzz;
    }
    public override bool Scatter(Ray rIn, hit_record rec, ref Vec3 attenuation, ref Ray scattered)
    {
        Vec3 reflected = Vec3.Reflect(rIn.Direction, rec.normal);
        reflected = Vec3.UnitVector(reflected) + (Fuzz * Vec3.RandomUnitVector());
        scattered = new Ray(rec.p, reflected);
        attenuation = Albedo;
        return (Vec3.Dot(scattered.Direction, rec.normal) > 0);
    }
}
public class Dielectric : Material
{
    private double Refraction_index;
    public Dielectric(double refraction_index)
    {
        Refraction_index = refraction_index;
    }
    public override bool Scatter(Ray rIn, hit_record rec, ref Vec3 attenuation, ref Ray scattered)
    {
        attenuation = new Vec3(1.0, 1.0, 1.0);
        double ri = rec.front_face ? (1.0 / Refraction_index) : Refraction_index;
        Vec3 unit_direction = Vec3.UnitVector(rIn.Direction);
        double cos_theta = Math.Min(Vec3.Dot(-unit_direction, rec.normal), 1.0);
        double sin_theta = Math.Sqrt(1.0 - cos_theta * cos_theta);
        bool cannot_reflect = ri * sin_theta > 1.0;
        Vec3 direction = new Vec3();
     
        if (cannot_reflect || reflectance(cos_theta, ri) > Rtweekend.RandomDouble())
        {
            direction = Vec3.Reflect(unit_direction, rec.normal);
        }
        else
        {
            direction = Vec3.Refract(unit_direction, rec.normal, ri);
        }
        scattered = new Ray(rec.p, direction);
        return true;
    }
    static double reflectance(double cosine, double refraction_index)
    {
        var r0 = (1 - refraction_index) / (1 + refraction_index);
        r0 = r0 * r0;
        return r0 + (1 - r0) * Math.Pow(1-cosine,5);
    }
}