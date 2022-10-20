using LibCS;
namespace UI;

internal class Scene
{
    private readonly Sphere[] _world;
    private readonly Random _rng = new();

    public Scene()
    {
        //var tBaseUp = new Vec3(0, 0.82, 0);
        //var tBaseFront = new Vec3(0, 0, 0.58);
        //var tBaseLeft = new Vec3(0.5, 0, -0.29);
        //var tBaseRight = new Vec3(-0.5, 0, -0.29);
        var sBaseCenter = new Vec3(0, 0, 0);

        var primitives = new List<Sphere>
        {
            new(new Vec3(0, -1000, 0), 1000, new Diffuse(new Vec3(0.5, 0.5, 0.5)))
        };

        for (var a = -10; a < 10; a++)
        {
            for (var b = -10; b < 10; b++)
            {
                var randomMaterial = _rng.NextDouble();
                var randomSize = 0.5 * _rng.NextDouble();
                var randomSign = _rng.NextDouble();
                Material material;

                if (randomMaterial < 0.5)
                {
                    randomSign = 1.0;
                    material = new Diffuse(new Vec3(_rng.NextDouble(), _rng.NextDouble(), _rng.NextDouble()));
                }
                else if (randomMaterial is > 0.5 and < 0.75)
                {
                    randomSign = randomSign < 0.5 ? 1.0 : -1.0;
                    material = new Dielectric(1.5 + 0.1 * randomMaterial * randomSign);
                }
                else
                {
                    randomSign = 1.0;
                    material = new Metal(new Vec3(_rng.NextDouble(), _rng.NextDouble(), _rng.NextDouble()),
                        0.05 * randomMaterial);
                }

                var offset = new Vec3(a + 0.5 * (_rng.NextDouble() - 0.5), randomSize, b + 0.5 * (_rng.NextDouble() - 0.5));
                primitives.Add(new Sphere(sBaseCenter + offset, randomSign * randomSize, material));

                //var offset = new Vec3(a + _rng.NextDouble() - 0.5, 0.01, b + _rng.NextDouble() - 0.5);
                //primitives.Add(new Triangle(randomSize * tBaseRight + offset, randomSize * tBaseUp + offset,
                //    randomSize * tBaseLeft + offset, material));
                //primitives.Add(new Triangle(randomSize * tBaseRight + offset, randomSize * tBaseFront + offset,
                //    randomSize * tBaseUp + offset, material));
                //primitives.Add(new Triangle(randomSize * tBaseUp + offset, randomSize * tBaseFront + offset,
                //    randomSize * tBaseLeft + offset, material));
                //primitives.Add(new Triangle(randomSize * tBaseRight + offset, randomSize * tBaseFront + offset,
                //    randomSize * tBaseLeft + offset, material));
            }
        }

        _world = primitives.ToArray();
    }

    public bool Hit(Ray ray, double tMin, double tMax, ref HitRecord record)
    {
        var isHit = false;
        var closest = tMax;

        foreach (var primitive in _world)
        {
            if (!primitive.Hit(ray, closest, ref record))
                continue;

            isHit = true;
            closest = record.T;
        }

        return isHit;
    }
}