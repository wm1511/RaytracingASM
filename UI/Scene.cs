using System.Numerics;

namespace UI;

internal class Scene
{
    private readonly Sphere[] _world; // Reprezentacja świata w postaci listy obiektów
    private readonly Random _rng = new(); // Generator liczb pseudolosowych

    // Generowanie losowej sceny
    public Scene()
    {
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
                var sign = (float)_rng.NextDouble();
                Material material;

                if (randomMaterial < 0.5f)
                {
                    sign = 1;
                    material = new Diffuse(new Vector3((float)_rng.NextDouble(), (float)_rng.NextDouble(), (float)_rng.NextDouble()));
                }
                else if (randomMaterial is > 0.5f and < 0.75f)
                {
                    sign = sign < 0.5f ? 1 : -1;
                    material = new Dielectric(1.5f + 0.1f * randomMaterial * sign);
                }
                else
                {
                    sign = 1;
                    material = new Metal(new Vector3((float)_rng.NextDouble(), (float)_rng.NextDouble(), (float)_rng.NextDouble()),
                        0.05f * randomMaterial);
                }

                var offset = new Vector3(a + 0.5f * ((float)_rng.NextDouble() - 0.5f), randomSize, b + 0.5f * ((float)_rng.NextDouble() - 0.5f));
                primitives.Add(new Sphere(new Vector3(0, 0, 0) + offset, sign * randomSize, material));
            }
        }

        _world = primitives.ToArray();
    }

    // Sprawdzanie przecięcia promienia ze wszystkimi obiektami zawartymi w scenie
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