using LibCs;
using System.Numerics;
using System.Runtime.InteropServices;

namespace UI;

// Struktura przechowuj¹ca informacjê o przeciêciu promienia z obiektem
internal struct HitRecord
{
    public float T; // Odleg³oœæ przeciêcia
    public Vector3 IntersectionPoint; // Punkt przeciêcia
    public Vector3 Normal; // Normalna obiektu
    public Material Material; // Materia³ obiektu
}

internal class Sphere
{
    private readonly Vector3 _center; // Punkt œrodkowy sfery
    private readonly float _radius; // Promieñ sfery
    private readonly Material _material; // Materia³ sfery

    // Funkcja s³u¿¹ca do obliczenia odleg³oœci przeciêcia promienia z obiektem
    public static Func<Vector3, Vector3, Vector3, float, float, float> IntersectionFunc = Intersection.IntersectSphereCs;

    public Sphere(Vector3 center, float radius, Material material)
    {
        _center = center;
        _radius = radius;
        _material = material;
    }

    // Metoda obliczaj¹ca odleg³oœæ przeciêcia promienia z obiektem oraz pozosta³e wymagane parametry
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

    // Importowanie biblioteki dynamicznej
    [DllImport("LibAsm.dll")]
    public static extern float IntersectSphereAsm(Vector3 origin, Vector3 direction, Vector3 center, float radius, float tMax);
}