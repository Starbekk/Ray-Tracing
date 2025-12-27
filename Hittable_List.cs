using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ray_Tracing;

public class HittableList: IHittable
{
    public List<IHittable> Objects = new List<IHittable>();
    public HittableList() { }
    public HittableList(IHittable obj)
    {
        Add(obj);
    }
    public void Add(IHittable obj)
    {
        Objects.Add(obj); 
    }
    public bool Hit(Ray r, Interval ray_t, ref hit_record rec)
    {
        hit_record tempRec = new hit_record();
        bool hitAnything = false;
        double closestSoFar = ray_t.Max;
        foreach(var obj in Objects)
        {
            if(obj.Hit(r, new Interval(ray_t.Min, closestSoFar), ref tempRec))
            {
                hitAnything = true;
                closestSoFar = tempRec.t;
                rec = tempRec;
            }
        }
        return hitAnything;
    }

}