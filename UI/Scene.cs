using LibCS;
namespace UI;

internal class Scene : Primitive
{
    private readonly Primitive[] _world;
    private readonly Random _rng = new();

    public Scene()
    {
        var tBaseUp = new Vec3(0, 0.82, 0);
        var tBaseFront = new Vec3(0, 0, 0.58);
        var tBaseLeft = new Vec3(0.5, 0, -0.29);
        var tBaseRight = new Vec3(-0.5, 0, -0.29);
        var sBaseCenter = new Vec3(0, 0.5, 0);

        var primitives = new Primitive[]
        {
            //new Triangle(tBaseRight, tBaseUp, tBaseLeft, glass),
            //new Triangle(tBaseRight, tBaseFront, tBaseUp, glass),
            //new Triangle(tBaseUp, tBaseFront, tBaseLeft, glass),
            //new Triangle(tBaseRight, tBaseFront, tBaseLeft, glass),

            //new Sphere(new Vec3(1.5, 1.25, -2), 0.5, gray),
            //new Sphere(new Vec3(0, 1.25, -2), 0.5, glass),
            //new Sphere(new Vec3(-1.5, 1.25, -2), 0.5, metal),
        };

        primitives.Append(new Sphere(new Vec3(0, -1000, 0), 1000, new Diffuse(new Vec3(0.5, 0.5, 0.5))));

        //TODO przerobienie sceny na losową
        for (var a = -10; a < 10; a++)
        {
            for (var b = -10; b < 10; b++)
            {
                var randomMaterial = _rng.NextDouble();
                Material material;
                if (randomMaterial < 0.5)
                    material = new Diffuse(new Vec3(_rng.NextDouble(), _rng.NextDouble(), _rng.NextDouble()));
                else if (randomMaterial > 0.5 && randomMaterial < 0.75)
                    material = new Dielectric(1.5 + randomMaterial < 0.625 ? 0.1 * randomMaterial : -0.1 * randomMaterial);
                else
                    material = new Metal(new Vec3(_rng.NextDouble(), _rng.NextDouble(), _rng.NextDouble()), 1 - randomMaterial);

                var center = new Vec3(a + 0.9 * _rng.NextDouble(), 0.2, b + 0.9 * _rng.NextDouble());
            }
        }

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