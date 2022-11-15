namespace UI;

internal class Bmp
{
    private readonly int _rasterDataSize; // Rozmiar tablicy zawierającej dane obrazu
    private readonly int _size; // Szerokość i wysokość obrazu w pikselach
    private const int HeaderSize = 54; // Rozmiar nagłówka mapy bitowej
    public byte[] ImageData { get; } // Dane mapy bitowej

    // Funkcja tworzy mapę bitową o podanych wymiarach na podstawie przekazanych danych obrazu
    public Bmp(int size, byte[] rasterData)
    {
        _size = size;
        _rasterDataSize = rasterData.Length;
        ImageData = new byte[_rasterDataSize + HeaderSize];
        PrepareHeader();
        PrepareRasterData(rasterData);
    }

    // Funkcja tworzy nagłówek mapy bitowej i zapisuje go na początku tablicy
    private void PrepareHeader()
    {
        ImageData[0] = 0x42;
        ImageData[1] = 0x4d;
        Array.Copy(BitConverter.GetBytes(_rasterDataSize + HeaderSize), 0, ImageData, 2, 4);
        Array.Copy(BitConverter.GetBytes(0), 0, ImageData, 6, 2);
        Array.Copy(BitConverter.GetBytes(0), 0, ImageData, 8, 2);
        Array.Copy(BitConverter.GetBytes(54), 0, ImageData, 10, 4);
        Array.Copy(BitConverter.GetBytes(40), 0, ImageData, 14, 4);
        Array.Copy(BitConverter.GetBytes(_size), 0, ImageData, 18, 4);
        Array.Copy(BitConverter.GetBytes(_size), 0, ImageData, 22, 4);
        Array.Copy(BitConverter.GetBytes(0), 0, ImageData, 26, 2);             
        Array.Copy(BitConverter.GetBytes(24), 0, ImageData, 28, 2);
        Array.Copy(BitConverter.GetBytes(0), 0, ImageData, 30, 4);
        Array.Copy(BitConverter.GetBytes(_rasterDataSize), 0, ImageData, 34, 4);
        Array.Copy(BitConverter.GetBytes(0), 0, ImageData, 38, 4);
        Array.Copy(BitConverter.GetBytes(0), 0, ImageData, 42, 4);
        Array.Copy(BitConverter.GetBytes(0), 0, ImageData, 46, 4);
        Array.Copy(BitConverter.GetBytes(0), 0, ImageData, 50, 4);
    }

    // Funkcja scala nagłówek oraz dane obrazu
    private void PrepareRasterData(byte[] rasterData)
    {
        Array.Copy(rasterData, 0, ImageData, 54, _rasterDataSize);
    }
}