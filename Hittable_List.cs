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
    public bool Hit(Ray r, double tMin, double tMax, ref hit_record rec)
    {
        hit_record tempRec = new hit_record();
        bool hitAnything = false;
        double closestSoFar = tMax;
        foreach(var obj in Objects)
        {
            if(obj.Hit(r, tMin, tMax, ref tempRec))
            {
                hitAnything = true;
                closestSoFar = tempRec.t;
                rec = tempRec;
            }
        }
        return hitAnything;
    }

}