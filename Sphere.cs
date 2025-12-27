using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray_Tracing;

public class Sphere : IHittable {
    private Vec3 Center;
    private double Radius;
    private Material Mat;
    public Sphere(Vec3 center, double radius, Material mat) {
        Center = center;
        Radius = Math.Max(0.0,radius);
        Mat = mat;
    }
    public bool Hit(Ray r, Interval ray_t, ref hit_record rec)
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
        if (!ray_t.Surrounds(root))
        {
            root = (h + sqrtd) / a;
            if (!ray_t.Surrounds(root))
            {
                return false;
            }
        }
        rec.t = root;
        rec.p = r.At(rec.t);
        Vec3 outward_normal = (rec.p - Center ) / Radius;
        rec.set_face_normal(r, outward_normal);
        rec.mat = Mat;
        return true;
    }

}

