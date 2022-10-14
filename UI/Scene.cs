using LibCS;
namespace UI;

internal class Scene : Primitive
{
    private readonly Primitive[] _world;
    private readonly Random _rng = new();

    public Scene()
    {
        var glass = new Dielectric(1.5);
        var metal = new Metal(new Vec3(0.6, 0.6, 0.6), 0.01);
        var gray = new Diffuse(new Vec3(0.5, 0.5, 0.5));

        var vUp = new Vec3(1, 0.83, -1.71) * 2;
        var vFront = new Vec3(1, 0.01, -1.13) * 2;
        var vLeft = new Vec3(1.5, 0.01, -2) * 2;
        var vRight = new Vec3(0.5, 0.01, -2) * 2;

        var primitives = new Primitive[]
        {
            new Triangle(vRight, vUp, vLeft, glass),
            new Triangle(vRight, vFront, vUp, glass),
            new Triangle(vUp, vFront, vLeft, glass),
            new Triangle(vRight, vFront, vLeft, glass),

            new Sphere(new Vec3(1.5, 1.25, -2), 0.5, gray),
            new Sphere(new Vec3(0, 1.25, -2), 0.5, glass),
            new Sphere(new Vec3(-1.5, 1.25, -2), 0.5, metal),

            new Sphere(new Vec3(0, -1000, 0), 1000, gray)
        };

        //for (var a = -11; a < 11; a++)
        //{
        //    for (var b = -11; b < 11; b++)
        //    {
        //        var chooseMaterial = _rng.NextDouble();
        //        var center = new Vec3(a + 0.9 * _rng.NextDouble(), 0.2, b + 0.9 * _rng.NextDouble());
        //    }
        //}

        _world = primitives;
    }

    public override bool Hit(Ray ray, double tMin, double tMax, ref HitRecord record)
    {
        var isHit = false;
        var closest = tMax;

        foreach (var primitive in _world)
        {
            if (!primitive.Hit(ray, tMin, closest, ref record))
                continue;

            isHit = true;
            closest = record.T;
        }

        return isHit;
    }
}