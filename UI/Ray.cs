using System.Numerics;

namespace UI;

internal readonly struct Ray
{
    public Vector3 Origin { get; } // Początek wektora
    public Vector3 Direction { get; } // Kierunek wektora

    public Ray(Vector3 origin, Vector3 direction)
    {
        Origin = origin;
        Direction = direction;
    }
}