using UnityEngine;
using System;
using Unity.VisualScripting;

[System.Serializable]
public class FloatingPoint
{
    public double x;
    public double y;
    public double z;

    public FloatingPoint()
    {
        this.x = 0;
        this.y = 0;
        this.z = 0;
    }

    public FloatingPoint(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public FloatingPoint(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public FloatingPoint(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static implicit operator FloatingPoint(Vector3 v) => new FloatingPoint(v.x, v.y, v.z);

    public static implicit operator Vector3(FloatingPoint v) => new Vector3((float)v.x, (float)v.y, (float)v.z);

    public static bool operator ==(FloatingPoint a, FloatingPoint b) => a.x == b.x && a.y == b.y && a.z == b.z;

    public static bool operator !=(FloatingPoint a, FloatingPoint b) => !(a == b);

    public static FloatingPoint operator +(FloatingPoint a, FloatingPoint b) => new FloatingPoint(a.x + b.x, a.y + b.y, a.z + b.z);

    public static FloatingPoint operator +(FloatingPoint a, Vector3 b) => new FloatingPoint(a.x + b.x, a.y + b.y, a.z + b.z);

    public static FloatingPoint operator +(Vector3 a, FloatingPoint b) => new FloatingPoint(a.x + b.x, a.y + b.y, a.z + b.z);

    public static FloatingPoint operator -(FloatingPoint a, FloatingPoint b) => new FloatingPoint(a.x - b.x, a.y - b.y, a.z - b.z);

    public static FloatingPoint operator -(FloatingPoint a, Vector3 b) => new FloatingPoint(a.x - b.x, a.y - b.y, a.z - b.z);

    public static FloatingPoint operator -(Vector3 a, FloatingPoint b) => new FloatingPoint(a.x - b.x, a.y - b.y, a.z - b.z);

    public static FloatingPoint operator -(FloatingPoint point) => new FloatingPoint(-point.x, -point.y, -point.z);

    public static FloatingPoint operator *(FloatingPoint point, double multiplier) => new FloatingPoint(point.x * multiplier, point.y * multiplier, point.z * multiplier);

    public static FloatingPoint operator *(double multiplier, FloatingPoint point) => point * multiplier;

    public static FloatingPoint operator /(FloatingPoint point, double divisor)
    {
        if (divisor == 0)
        {
            throw new DivideByZeroException("Division by zero");
        }
        return new FloatingPoint(point.x / divisor, point.y / divisor, point.z / divisor);
    }


    /// <summary>
    /// Rounds the components of the vector to the nearest integer.
    /// </summary>
    public void Round()
    {
        x = Math.Round(x);
        y = Math.Round(y);
        z = Math.Round(z);
    }

    /// <summary>
    /// Rounds the components of the vector up to the nearest integer.
    /// </summary>
    public void Ceil()
    {
        x = Math.Ceiling(x);
        y = Math.Ceiling(y);
        z = Math.Ceiling(z);
    }

    /// <summary>
    /// Rounds the components of the vector down to the nearest integer.
    /// </summary>
    public void Floor()
    {
        x = Math.Floor(x);
        y = Math.Floor(y);
        z = Math.Floor(z);
    }

    /// <summary>
    /// Property to get the position as a <see cref="FloatingPoint"/> instance.
    /// </summary>
    public FloatingPoint Position => new FloatingPoint(x, y, z); // TODO Not needed.

    /// <summary>
    /// Property to get the normalized value of the position.
    /// </summary>
    /// <remarks>
    /// If the magnitude of the vector is greater than 0, returns a new normalized <see cref="FloatingPoint"/> instance.
    /// Otherwise, returns a zero vector.
    /// </remarks>
    public FloatingPoint Normalized
    {
        get
        {
            double magnitude = Math.Sqrt(x * x + y * y + z * z);
            return magnitude > 0 ? new FloatingPoint(x / magnitude, y / magnitude, z / magnitude) : new FloatingPoint(0, 0, 0);
        }
    }

    /// <summary>
    /// Property to get the square root of the position data.
    /// </summary>
    public FloatingPoint SquareRoot => new FloatingPoint(Math.Sqrt(x), Math.Sqrt(y), Math.Sqrt(z));

    /// <summary>
    /// Computes the dot product of two <see cref="FloatingPoint"/> vectors.
    /// </summary>
    /// <param name="a">The first vector.</param>
    /// <param name="b">The second vector.</param>
    /// <returns>The dot product of the two vectors.</returns>
    public static double Dot(FloatingPoint a, FloatingPoint b)
    {
        return a.x * b.x + a.y * b.y + a.z * b.z;
    }

    /// <summary>
    /// Computes the magnitude of the vector.
    /// </summary>
    public double Magnitude => Math.Sqrt(x * x + y * y + z * z);

    /// <summary>
    /// Computes the squared magnitude of the vector.
    /// </summary>
    public double SqrMagnitude => x * x + y * y + z * z;

    /// <summary>
    /// Computes the distance between two points represented by vectors.
    /// </summary>
    /// <param name="a">The first point.</param>
    /// <param name="b">The second point.</param>
    /// <returns>The distance between the two points.</returns>
    public static double Distance(FloatingPoint a, FloatingPoint b)
    {
        return (a - b).Magnitude;
    }

    /// <summary>
    /// Computes the angle in degrees between two vectors.
    /// </summary>
    /// <param name="from">The first vector.</param>
    /// <param name="to">The second vector.</param>
    /// <returns>The angle between the two vectors in degrees.</returns>
    public static double Angle(FloatingPoint from, FloatingPoint to)
    {
        double dot = Dot(from.Normalized, to.Normalized);
        return Math.Acos(Math.Clamp(dot, -1.0, 1.0)) * Mathf.Rad2Deg;
    }

    /// <summary>
    /// Computes the cross product of two vectors.
    /// </summary>
    /// <param name="a">The first vector.</param>
    /// <param name="b">The second vector.</param>
    /// <returns>The cross product of the two vectors.</returns>
    public static FloatingPoint Cross(FloatingPoint a, FloatingPoint b)
    {
        return new FloatingPoint(
            a.y * b.z - a.z * b.y,
            a.z * b.x - a.x * b.z,
            a.x * b.y - a.y * b.x
        );
    }

    /// <summary>
    /// Reflects a vector off a plane defined by a normal vector.
    /// </summary>
    /// <param name="vector">The vector to reflect.</param>
    /// <param name="normal">The normal vector of the plane.</param>
    /// <returns>The reflected vector.</returns>
    public static FloatingPoint Reflect(FloatingPoint vector, FloatingPoint normal)
    {
        return vector - 2 * Dot(vector, normal) * normal;
    }

    /// <summary>
    /// Projects a vector onto another vector.
    /// </summary>
    /// <param name="vector">The vector to project.</param>
    /// <param name="onNormal">The vector to project onto.</param>
    /// <returns>The projected vector.</returns>
    public static FloatingPoint Project(FloatingPoint vector, FloatingPoint onNormal)
    {
        double sqrMagnitude = onNormal.SqrMagnitude;
        if (sqrMagnitude < double.Epsilon)
        {
            return new FloatingPoint(0, 0, 0);
        }
        double dot = Dot(vector, onNormal);
        return onNormal * (dot / sqrMagnitude);
    }

    /// <summary>
    /// Projects a vector onto a plane defined by a normal vector.
    /// </summary>
    /// <param name="vector">The vector to project.</param>
    /// <param name="planeNormal">The normal vector of the plane.</param>
    /// <returns>The projected vector onto the plane.</returns>
    public static FloatingPoint ProjectOnPlane(FloatingPoint vector, FloatingPoint planeNormal)
    {
        double dot = Dot(vector, planeNormal);
        return vector - dot * planeNormal;
    }

    /// <summary>
    /// Orthogonalizes two vectors.
    /// </summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector, which will be orthogonalized to the first vector.</param>
    public static void Orthogonalize(ref FloatingPoint vector1, ref FloatingPoint vector2)
    {
        vector2 -= Project(vector2, vector1);
    }

    /// <summary>
    /// Linearly interpolates between two vectors.
    /// </summary>
    /// <param name="a">The start vector.</param>
    /// <param name="b">The end vector.</param>
    /// <param name="t">The interpolation parameter (0 to 1).</param>
    /// <returns>The interpolated vector.</returns>
    public static FloatingPoint Lerp(FloatingPoint a, FloatingPoint b, double t)
    {
        t = Math.Clamp(t, 0.0, 1.0);
        return new FloatingPoint(
            a.x + (b.x - a.x) * t,
            a.y + (b.y - a.y) * t,
            a.z + (b.z - a.z) * t
        );
    }

    /// <summary>
    /// Linearly interpolates between two vectors without clamping the interpolant.
    /// </summary>
    /// <param name="a">The start vector.</param>
    /// <param name="b">The end vector.</param>
    /// <param name="t">The interpolation parameter.</param>
    /// <returns>The interpolated vector.</returns>
    public static FloatingPoint LerpUnclamped(FloatingPoint a, FloatingPoint b, double t)
    {
        return new FloatingPoint(
            a.x + (b.x - a.x) * t,
            a.y + (b.y - a.y) * t,
            a.z + (b.z - a.z) * t
        );
    }

    /// <summary>
    /// Spherically interpolates between two vectors.
    /// </summary>
    /// <param name="a">The start vector.</param>
    /// <param name="b">The end vector.</param>
    /// <param name="t">The interpolation parameter (0 to 1).</param>
    /// <returns>The interpolated vector.</returns>
    public static FloatingPoint Slerp(FloatingPoint a, FloatingPoint b, double t)
    {
        double dot = Dot(a, b);
        dot = Math.Clamp(dot, -1.0, 1.0);
        double theta = Math.Acos(dot) * t;
        FloatingPoint relativeVec = b - a * dot;
        relativeVec = relativeVec.Normalized;
        return ((a * Math.Cos(theta)) + (relativeVec * Math.Sin(theta)));
    }

    /// <summary>
    /// Moves a vector towards a target vector by a maximum distance delta.
    /// </summary>
    /// <param name="current">The current vector position.</param>
    /// <param name="target">The target vector position.</param>
    /// <param name="maxDistanceDelta">The maximum distance to move.</param>
    /// <returns>The new vector position.</returns>
    public static FloatingPoint MoveTowards(FloatingPoint current, FloatingPoint target, double maxDistanceDelta)
    {
        FloatingPoint delta = target - current;
        double sqrMagnitude = delta.SqrMagnitude;
        if (sqrMagnitude == 0 || (maxDistanceDelta >= 0 && sqrMagnitude <= maxDistanceDelta * maxDistanceDelta))
        {
            return target;
        }
        return current + delta.Normalized * maxDistanceDelta;
    }

    /// <summary>
    /// Moves a vector towards a specified angle by a maximum delta angle.
    /// </summary>
    /// <param name="current">The current vector position.</param>
    /// <param name="targetAngle">The target angle in degrees.</param>
    /// <param name="maxDeltaAngle">The maximum change in angle in degrees.</param>
    /// <returns>The new vector position.</returns>
    public static FloatingPoint MoveTowardsAngle(FloatingPoint current, double targetAngle, double maxDeltaAngle)
    {
        // Calculate the current angle of the vector
        double currentAngle = Math.Atan2(current.y, current.x);

        // Convert the target angle to radians
        double targetAngleRad = targetAngle * Math.PI / 180.0;

        // Calculate the difference between the target angle and the current angle
        double angleDifference = targetAngleRad - currentAngle;

        // Wrap the angle difference to keep it within the range of -pi to pi
        while (angleDifference > Math.PI)
        {
            angleDifference -= 2 * Math.PI;
        }
        while (angleDifference < -Math.PI)
        {
            angleDifference += 2 * Math.PI;
        }

        // Clamp the angle difference within the specified maximum delta angle
        double clampedAngleDifference = Math.Clamp(angleDifference, -maxDeltaAngle * Math.PI / 180.0, maxDeltaAngle * Math.PI / 180.0);

        // Calculate the new angle by adding the clamped angle difference to the current angle
        double newAngle = currentAngle + clampedAngleDifference;

        // Calculate the magnitude of the vector
        double mag = current.Magnitude;

        // Compute the new position using trigonometric functions
        return new FloatingPoint(Math.Cos(newAngle) * mag, Math.Sin(newAngle) * mag, current.z);
    }

    /// <summary>
    /// Smoothly dampens a vector towards a desired target over time.
    /// </summary>
    /// <param name="current">The current vector position.</param>
    /// <param name="target">The target vector position.</param>
    /// <param name="currentVelocity">The current velocity.</param>
    /// <param name="smoothTime">The time to reach the target.</param>
    /// <param name="deltaTime">The time since the last update.</param>
    /// <param name="maxSpeed">The maximum speed of movement.</param>
    /// <returns>The new vector position.</returns>
    public static FloatingPoint SmoothDamp(FloatingPoint current, FloatingPoint target, ref FloatingPoint currentVelocity, double smoothTime, double deltaTime, double maxSpeed = double.PositiveInfinity)
    {
        smoothTime = Math.Max(0.0001, smoothTime);
        double num = 2.0 / smoothTime;
        double num2 = num * deltaTime;
        double d = 1.0 / (1.0 + num2 + 0.48 * num2 * num2 + 0.235 * num2 * num2 * num2);
        FloatingPoint vector = current - target;
        FloatingPoint vector2 = target;
        double maxLength = maxSpeed * smoothTime;
        vector = ClampMagnitude(vector, maxLength);
        target = current - vector;
        FloatingPoint vector3 = (currentVelocity + num * vector) * deltaTime;
        currentVelocity = (currentVelocity - num * vector3) * d;
        FloatingPoint vector4 = target + (vector + vector3) * d;
        if (Dot(vector2 - current, vector4 - vector2) > 0.0)
        {
            vector4 = vector2;
            currentVelocity = (vector4 - vector2) / deltaTime;
        }
        return vector4;
    }

    /// <summary>
    /// Clamps the magnitude of a vector to the specified maximum length.
    /// </summary>
    /// <param name="vector">The vector to clamp.</param>
    /// <param name="maxLength">The maximum length to clamp the vector magnitude to.</param>
    /// <returns>The clamped vector.</returns>
    private static FloatingPoint ClampMagnitude(FloatingPoint vector, double maxLength)
    {
        double sqrMagnitude = vector.SqrMagnitude;
        if (sqrMagnitude > maxLength * maxLength)
        {
            return vector.Normalized * maxLength;
        }
        return vector;
    }

    /// <summary>
    /// Compares two vectors for equality within a small tolerance threshold.
    /// </summary>
    /// <param name="a">The first vector.</param>
    /// <param name="b">The second vector.</param>
    /// <param name="epsilon">The tolerance threshold.</param>
    /// <returns>True if the vectors are almost equal, false otherwise.</returns>
    public static bool AlmostEquals(FloatingPoint a, FloatingPoint b, double epsilon = 1e-5)
    {
        return Math.Abs(a.x - b.x) < epsilon && Math.Abs(a.y - b.y) < epsilon && Math.Abs(a.z - b.z) < epsilon;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        FloatingPoint other = (FloatingPoint)obj;
        return x == other.x && y == other.y && z == other.z;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 23 + x.GetHashCode();
            hash = hash * 23 + y.GetHashCode();
            hash = hash * 23 + z.GetHashCode();
            return hash;
        }
    }
}