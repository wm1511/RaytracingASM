//namespace LibCS;

//public readonly struct Vec3
//{
//    public double X { get; }
//    public double Y { get; }
//    public double Z { get; }

//    public Vec3(double x, double y, double z)
//    {
//        X = x;
//        Y = y;
//        Z = z;
//    }

//    public static Vec3 operator+(Vec3 v1, Vec3 v2) => new(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
//    public static Vec3 operator-(Vec3 v1, Vec3 v2) => new (v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
//    public static Vec3 operator*(Vec3 v1, Vec3 v2) => new (v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
//    public static Vec3 operator*(Vec3 v, double t) => new (v.X * t, v.Y * t, v.Z * t);
//    public static Vec3 operator*(double t, Vec3 v) => new (v.X * t, v.Y * t, v.Z * t);
//    public static Vec3 operator/(Vec3 v, double t) => new (v.X / t, v.Y / t, v.Z / t);
//    public static Vec3 operator-(Vec3 v) => new (-v.X, -v.Y, -v.Z);

//    public double Length() => Math.Sqrt(X * X + Y * Y + Z * Z);
//    public double SquaredLength() => X * X + Y * Y + Z * Z;
//    public static Vec3 Normalize(Vec3 v) => v / v.Length();
//    public static double Dot(Vec3 v1, Vec3 v2) => v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
//    public static Vec3 Cross(Vec3 v1, Vec3 v2) =>
//        new(v1.Y * v2.Z - v1.Z * v2.Y,
//            v1.Z * v2.X - v1.X * v2.Z,
//            v1.X * v2.Y - v1.Y * v2.X);
//}