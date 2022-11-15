using System.Numerics;

namespace UI;

internal class Camera
{
    private readonly Vector3 _lowerLeftCorner; // Po�o�enie dolnego lewego rogu obrazu
    private readonly Vector3 _horizontal; // Szeroko�� obrazu
    private readonly Vector3 _vertical; // Wysoko�� obrazu
    private readonly Vector3 _origin; // Punkt pocz�tkowy promienia (po�o�enie kamery)

    // Tworzenie obiektu kamery i przypisywanie mu parametr�w startowych
    public Camera()
    {
        _origin = new Vector3(0, 1, 0);
        _horizontal = new Vector3(2, 0, 0);
        _vertical = new Vector3(0, 2, 0);
        _lowerLeftCorner = _origin - _horizontal / 2 - _vertical / 2 - new Vector3(0, 0, 1);
    }

    // Metoda zwraca obiekt promienia skierowany w kierunku przekazanych wsp�rz�dnych ekranu
    public Ray GetRay(float u, float v)
    {
        return new Ray(_origin, _lowerLeftCorner + u * _horizontal + v * _vertical - _origin);
    }
}