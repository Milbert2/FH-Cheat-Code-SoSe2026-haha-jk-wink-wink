using SFML.System;

internal static class Utils
{
    /// <summary>
    /// Returns the squared magnitude of a vector (length before square root)
    /// </summary>
    /// <returns>length of input vector</returns>
    public static float SqrMagnitude(this Vector2f input)
    {
        var sqrLength = input.X * input.X + input.Y * input.Y;
        return sqrLength;
    }

    /// <summary>
    /// Rotates a vector by a given angle counterclockwise
    /// </summary>
    /// <param name="input">input vector</param>
    /// <param name="angle">angle in radians</param>
    /// <returns>a new vector rotated by angle radians</returns>
    public static Vector2f RotateVector(Vector2f input, float angle) // counterclockwise by the given angle (angle in radians)
    {
        return new Vector2f(
            input.X * MathF.Cos(angle) - input.Y * MathF.Sin(angle),
            input.X * MathF.Sin(angle) + input.Y * MathF.Cos(angle));
    }

    /// <summary>
    /// Returns the angle between two vectors
    /// </summary>
    /// <returns>angle in radians</returns>
    public static float AngleBetween(Vector2f v1, Vector2f v2)
    {
        return (float)Math.Acos(Dot(v1.Normalize(), v2.Normalize()));
    }

    /// <summary>
    /// Convert from radians to degrees
    /// </summary>
    /// <returns>angle in degrees</returns>
    public static float ToDegrees(this float angle)
    {
        return 180 / MathF.PI * angle;
    }

    /// <summary>
    /// Convert from degrees to radians
    /// </summary>
    /// <returns>angle in radians</returns>
    public static float ToRadians(this float angle)
    {
        return MathF.PI / 180 * angle;
    }

    /// <summary>
    /// Distance between two points.
    /// </summary>
    /// <returns>distance</returns>
    public static float Distance(Vector2f v1, Vector2f v2)
    {
        return MathF.Sqrt(SqrMagnitude(v1 - v2));
    }

    /// <summary>
    /// Normalizes a vector to a length of 1 (in place)
    /// </summary>
    /// <returns>normalized vector itself</returns>
    public static Vector2f Normalize(this Vector2f input)
    {
        var length = MathF.Sqrt(input.X * input.X + input.Y * input.Y);

        if (length != 0 && length != 1)
        {
            input.X /= length;
            input.Y /= length;
        }

        return input;
    }

    /// <summary>
    /// Linear interpolation between two floats
    /// </summary>
    /// <param name="f1">start value</param>
    /// <param name="f2">end value</param>
    /// <param name="t">interpolation between [0,1]</param>
    /// <param name="clamped">clamp the interpolation between 0 and 1</param>
    /// <returns>interpolated value</returns>
    public static float Lerp(this float f1, float f2, float t, bool clamped = true)
    {
        if (clamped)
            t = t > 1.0f ? 1.0f : t;

        return f1 * (1 - t) + f2 * t;
    }

    /// <summary>
    /// Linear interpolation between two vectors
    /// </summary>
    /// <param name="v1">start direction or point</param>
    /// <param name="v2">end direction or point</param>
    /// <param name="t">interpolation between [0,1]</param>
    /// <returns>interpolated vector</returns>
    public static Vector2f Lerp(Vector2f v1, Vector2f v2, float t)
    {
        var x = Lerp(v1.X, v2.X, t);
        var y = Lerp(v1.Y, v2.Y, t);
        return new Vector2f(x, y);
    }

    /// <summary>
    /// Returns the Dot-Product of two vectors
    /// </summary>
    /// <returns>dot product</returns>
    public static float Dot(Vector2f lhs, Vector2f rhs)
    {
        var dotProduct = lhs.X * rhs.X + lhs.Y * rhs.Y;
        return dotProduct;
    }
}