using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray_Tracing;

public class Sphere : IHittable {
    public Vec3 Center;
    public double Radius;
    public Sphere(Vec3 center, double radius) {
        Center = center;
        Radius = Math.Max(0.0,radius);
    }
    public bool Hit(Ray r, double tMin, double tMax, ref hit_record rec)
    {
        Vec3 oc = Center - r.Origin;
        var a = r.Direction.LengthSquared();
        var h = Vec3.Dot(r.Direction, oc);
        var c = oc.LengthSquared() - Radius * Radius;
        var discriminant = h * h - a * c;
        if (discriminant < 0.0)
        {
            return false;
        }
        var sqrtd = Math.Sqrt(discriminant);
        var root = (h - sqrtd) / a;
        if (root < tMin || tMax <= root)
        {
            root = (h + sqrtd) / a;
            if (root < tMin || tMax <= root)
            {
                return false;
            }
        }
        rec.t = root;
        rec.p = r.At(rec.t);
        Vec3 outward_normal = (rec.p - Center ) / Radius;
        rec.set_face_normal(r, outward_normal);
        return true;
    }

}

