using UnityEngine;
using System;

[System.Serializable]
public class FloatingPoint
{
    public double x;
    public double y;
    public double z;

    public FloatingPoint(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static implicit operator FloatingPoint(Vector3 v)
    {
        return new FloatingPoint(v.x, v.y, v.z);
    }

    public static implicit operator Vector3(FloatingPoint v)
    {
        return new Vector3((float)v.x, (float)v.y, (float)v.z);
    }

    public static double CalculateDistance(FloatingPoint a, FloatingPoint b)
    {
        double deltaX = a.x - b.x;
        double deltaY = a.y - b.y;
        double deltaZ = a.z - b.z;

        double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
        return distance;
    }
}