using LibCs;
using System.Numerics;
using System.Runtime.InteropServices;

namespace UI;

// Struktura przechowująca informację o przecięciu promienia z obiektem
internal struct HitRecord
{
    public float T; // Odległość przecięcia
    public Vector3 IntersectionPoint; // Punkt przecięcia
    public Vector3 Normal; // Normalna obiektu
    public Material Material; // Materiał obiektu
}

internal class Sphere
{
    private readonly Vector3 _center; // Punkt środkowy sfery
    private readonly float _radius; // Promień sfery
    private readonly Material _material; // Materiał sfery

    // Funkcja służąca do obliczenia odległości przecięcia promienia z obiektem
    public static Func<Vector3, Vector3, Vector3, float, float, float> IntersectionFunc = Intersection.IntersectSphereCs;

    public Sphere(Vector3 center, float radius, Material material)
    {
        _center = center;
        _radius = radius;
        _material = material;
    }

    // Metoda obliczająca odległość przecięcia promienia z obiektem oraz pozostałe wymagane parametry
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