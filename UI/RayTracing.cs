using LibCS;
namespace UI;

internal class RayTracing
{
    private readonly Random _rng;
    private readonly Scene _scene;
    private readonly Camera _camera;
    private readonly int _size;
    private readonly int _spp;
    private readonly int _maxDepth;

    public RayTracing(int size, int spp, int maxDepth)
    {
        _rng = new Random();
        _scene = new Scene();
        _camera = new Camera();
        _size = size;
        _spp = spp;
        _maxDepth = maxDepth;
    }

    private static Vec3 Color(Ray ray, Scene world, int depth, int maxDepth)
    {
        var record = new HitRecord();

        if (world.Hit(ray, 0.001, double.MaxValue, ref record))
            return depth < maxDepth && 
                   record.Material.Scatter(ray, record, out var attenuation, out var scatterRay)
                ? attenuation * Color(scatterRay, world, depth + 1, maxDepth)
                : new Vec3(0, 0, 0);

        var normalizedDirection = Vec3.Normalize(ray.Direction);
        var t = 0.5 * (normalizedDirection.Y + 1);
        return (1 - t) * new Vec3(1, 1, 1) + t * new Vec3(0.3, 0.7, 1);
    }

    private void RenderLine(byte[] result, int lineNum)
    {
        for (var colNum = 0; colNum < _size; colNum++)
        {
            var col = new Vec3(0, 0, 0);
            for (var s = 0; s < _spp; s++)
            {
                var u = (colNum + _rng.NextDouble()) / _size;
                var v = (lineNum + _rng.NextDouble()) / _size;
                var ray = _camera.GetRay(u, v);
                col += Color(ray, _scene, 0, _maxDepth);
            }
                    
            col /= _spp;
            var pos = 3 * (lineNum * _size + colNum);
            result[pos] = (byte)(byte.MaxValue * Math.Sqrt(col.Z));
            result[pos + 1] = (byte)(byte.MaxValue * Math.Sqrt(col.Y));
            result[pos + 2] = (byte)(byte.MaxValue * Math.Sqrt(col.X));
        }
    }

    public byte[] Render(int threadCount)
    {
        var result = new byte[3 * _size * _size];

        //var chunkSize = _size / threadCount;
        //var rest = _size % chunkSize;
        //Debug.Assert(chunkSize * threadCount + rest == _size);

        for (var i = _size - 1; i >= 0; i--)
        {
            RenderLine(result, i);
        }

        return result;
    }
}