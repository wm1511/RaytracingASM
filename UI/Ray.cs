using System.Numerics;

namespace UI;

internal readonly struct Ray
{
    public Vector3 Origin { get; }
    public Vector3 Direction { get; }

    public Ray(Vector3 origin, Vector3 direction)
    {
        Origin = origin;
        Direction = direction;
    }
}