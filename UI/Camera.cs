using System.Numerics;

namespace UI;

internal class Camera
{
    private readonly Vector3 _lowerLeftCorner;
    private readonly Vector3 _horizontal;
    private readonly Vector3 _vertical;
    private readonly Vector3 _origin;

    public Camera()
    {
        _origin = new Vector3(0, 1, 0);
        _horizontal = new Vector3(2, 0, 0);
        _vertical = new Vector3(0, 2, 0);
        _lowerLeftCorner = _origin - _horizontal / 2 - _vertical / 2 - new Vector3(0, 0, 1);
    }

    public Ray GetRay(float u, float v)
    {
        return new Ray(_origin, _lowerLeftCorner + u * _horizontal + v * _vertical - _origin);
    }
}