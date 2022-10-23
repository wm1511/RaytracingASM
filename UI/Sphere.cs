using LibCs;
using System.Numerics;
using System.Runtime.InteropServices;

namespace UI;

internal struct HitRecord
{
    public float T;
    public Vector3 IntersectionPoint;
    public Vector3 Normal;
    public Material Material;
}

internal class Sphere
{
    private readonly Vector3 _center;
    private readonly float _radius;
    private readonly Material _material;
    public static Func<Vector3, Vector3, Vector3, float, float, float> IntersectionFunc = Intersection.IntersectSphereCs;

    public Sphere(Vector3 center, float radius, Material material)
    {
        _center = center;
        _radius = radius;
        _material = material;
    }

    public bool Hit(Ray ray, float tMax, ref HitRecord record)
    {
        record.T = IntersectionFunc(ray.Origin, ray.Direction, _center, _radius, tMax);
        if (record.T > 0.0)
        {
            record.IntersectionPoint = ray.Origin + record.T * ray.Direction;
            record.Normal = (record.IntersectionPoint - _center) / _radius;
            record.Material = _material;
            return true;
        }
        return false;
    }

    [DllImport("C:\\Users\\lenovo\\source\\JAProjekt\\x64\\Debug\\LibAsm.dll", EntryPoint = "IntersectSphereAsm",
        CallingConvention = CallingConvention.StdCall)]
    public static extern float IntersectSphereAsm(Vector3 origin, Vector3 direction, Vector3 center, float radius, float tMax);
}

//internal class Triangle : Primitive
//{
//    private readonly Vector3 _v0, _v1, _v2;
//    private readonly Material _material;

//    public Triangle(Vector3 v0, Vector3 v1, Vector3 v2, Material material)
//    {
//        _v0 = v0;
//        _v1 = v1;
//        _v2 = v2;
//        _material = material;
//    }

//    public override bool Hit(Ray ray, float tMin, float tMax, ref HitRecord record)
//    {
//        // MT

//        //var v0v1 = _v1 - _v0; 
//        //var v0v2 = _v2 - _v0; 
//        //var pvec = Vector3.Cross(ray.Direction, v0v2); 
//        //var det = Vector3.Dot(v0v1, pvec); 

//        //if (det < float.Epsilon) return false; 

//        //var invDet = 1 / det; 
 
//        //var tvec = ray.Origin - _v0; 
//        //var u = Vector3.Dot(tvec, pvec) * invDet; 
//        //if (u < 0 || u > 1) return false; 
 
//        //var qvec = Vector3.Cross(tvec, v0v1); 
//        //var v = Vector3.Dot(ray.Direction, qvec) * invDet; 
//        //if (v < 0 || u + v > 1) return false; 
 
//        //record.T = Vector3.Dot(v0v2, qvec) * invDet;
//        //record.IntersectionPoint = ray.Origin + record.T * ray.Direction;
//        //record.Normal = Vector3.Cross(v0v2, v0v1);
//        //record.Material = _material;
 
//        //return true; 

//        // Geometric

//        //var v0v1 = _v1 - _v0;
//        //var v0v2 = _v2 - _v0;
//        //record.Normal = Vector3.Normalize(Vector3.Cross(v0v1, v0v2));

//        //var nDotDir = Vector3.Dot(record.Normal, ray.Direction);
//        //if (Math.Abs(nDotDir) < float.Epsilon)
//        //    return false;

//        //var d = Vector3.Dot(-record.Normal, _v0);
//        //record.T = -(Vector3.Dot(record.Normal, ray.Origin) + d) / nDotDir;

//        //if (record.T < 0)
//        //    return false;

//        //record.IntersectionPoint = ray.Origin + record.T * ray.Direction;

//        //var edge0 = _v1 - _v0;
//        //var vp0 = record.IntersectionPoint - _v0;
//        //if (Vector3.Dot(record.Normal, Vector3.Cross(edge0, vp0)) < 0)
//        //    return false;

//        //var edge1 = _v2 - _v1;
//        //var vp1 = record.IntersectionPoint - _v1;
//        //if (Vector3.Dot(record.Normal, Vector3.Cross(edge1, vp1)) < 0)
//        //    return false;

//        //var edge2 = _v0 - _v2;
//        //var vp2 = record.IntersectionPoint - _v2;
//        //if (Vector3.Dot(record.Normal, Vector3.Cross(edge2, vp2)) < 0)
//        //    return false;

//        //record.Material = _material;
//        //return true;
//    }
//}