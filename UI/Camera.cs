using LibCS;
namespace UI;

internal class Camera
{
    private readonly Vec3 _lowerLeftCorner;
    private readonly Vec3 _horizontal;
    private readonly Vec3 _vertical;
    private readonly Vec3 _origin;

    public Camera()
    {
        _origin = new Vec3(0, 1.0, 0);
        _horizontal = new Vec3(2.0, 0, 0);
        _vertical = new Vec3(0, 2.0, 0);
        _lowerLeftCorner = _origin - _horizontal / 2 - _vertical / 2 - new Vec3(0, 0, 1.0);
    }

    public Ray GetRay(double u, double v)
    {
        return new Ray(_origin, _lowerLeftCorner + u * _horizontal + v * _vertical - _origin);
    }
}