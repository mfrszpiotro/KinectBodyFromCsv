using Microsoft.Kinect;
using System;

public class Floor
{
    public float X { get; internal set; }
    public float Y { get; internal set; }
    public float Z { get; internal set; }
    public float W { get; internal set; }

    public Floor(Vector4 floorClipPlane)
    {
        X = floorClipPlane.X;
        Y = floorClipPlane.Y;
        Z = floorClipPlane.Z;
        W = floorClipPlane.W;
    }

    public Floor(float x, float y, float z, float w)
    {
        X = x; Y = y; Z = z; W = w;
    }

    public float SensorHeight
    {
        get { return W; }
    }

    public double SensorTilt
    {
        get { return Math.Atan(Z / Y) * (180.0 / Math.PI); }
    }

    public double DistanceFrom(CameraSpacePoint point)
    {
        double numerator = X * point.X + Y * point.Y + Z * point.Z + W;
        double denominator = Math.Sqrt(X * X + Y * Y + Z * Z);
        return numerator / denominator;
    }
}