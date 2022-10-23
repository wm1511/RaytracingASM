using System.Numerics;

namespace UI;

internal class Scene
{
    private readonly Sphere[] _world;
    private readonly Random _rng = new();

    public Scene()
    {
        //var tBaseUp = new Vector3(0, 0.82, 0);
        //var tBaseFront = new Vector3(0, 0, 0.58);
        //var tBaseLeft = new Vector3(0.5, 0, -0.29);
        //var tBaseRight = new Vector3(-0.5, 0, -0.29);
        var sBaseCenter = new Vector3(0, 0, 0);

        var primitives = new List<Sphere>
        {
            new(new Vector3(0, -1000, 0), 1000, new Diffuse(new Vector3(0.5f, 0.5f, 0.5f)))
        };

        for (var a = -10; a < 10; a++)
        {
            for (var b = -10; b < 10; b++)
            {
                var randomMaterial = (float)_rng.NextDouble();
                var randomSize = 0.5f * (float)_rng.NextDouble();
                var randomSign = (float)_rng.NextDouble();
                Material material;

                if (randomMaterial < 0.5f)
                {
                    randomSign = 1;
                    material = new Diffuse(new Vector3((float)_rng.NextDouble(), (float)_rng.NextDouble(), (float)_rng.NextDouble()));
                }
                else if (randomMaterial is > 0.5f and < 0.75f)
                {
                    randomSign = randomSign < 0.5f ? 1 : -1;
                    material = new Dielectric(1.5f + 0.1f * randomMaterial * randomSign);
                }
                else
                {
                    randomSign = 1;
                    material = new Metal(new Vector3((float)_rng.NextDouble(), (float)_rng.NextDouble(), (float)_rng.NextDouble()),
                        0.05f * randomMaterial);
                }

                var offset = new Vector3(a + 0.5f * ((float)_rng.NextDouble() - 0.5f), randomSize, b + 0.5f * ((float)_rng.NextDouble() - 0.5f));
                primitives.Add(new Sphere(sBaseCenter + offset, randomSign * randomSize, material));

                //var offset = new Vector3(a + (float)_rng.NextDouble() - 0.5, 0.01, b + (float)_rng.NextDouble() - 0.5);
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

    public bool Hit(Ray ray, float tMax, ref HitRecord record)
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