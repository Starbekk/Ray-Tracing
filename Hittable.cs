using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray_Tracing;

public struct hit_record {
    public Vec3 p;
    public Vec3 normal;
    public double t;
    bool front_face;
    public void set_face_normal(Ray r, Vec3 outward_normal)
    {
        front_face = Vec3.Dot(r.Direction, outward_normal) < 0;
        normal = front_face ? outward_normal : -outward_normal;  
    }
}

public interface IHittable
{
    bool Hit(Ray r, double tMin, double tMax, ref hit_record rec);
}
