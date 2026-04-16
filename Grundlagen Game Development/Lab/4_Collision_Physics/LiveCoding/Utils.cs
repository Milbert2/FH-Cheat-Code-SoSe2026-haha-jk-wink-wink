using SFML.System;

public static class Utils
{
    /// <summary>
    /// Calculate the squared length of this vector.
    /// </summary>
    /// <param name="input">The vector to calculate the length of.</param>
    /// <returns>The squared length of the specified vector.</returns>
    public static float SquaredLength(this Vector2f input)
    {
        return input.X * input.X + input.Y * input.Y;
    }

    public static Vector2f Normalize(this Vector2f input)
    {
        float mag = input.Magnitude();
        if (mag == 0)
            return input;
            
        input /= mag;
        return input;
    }

    public static float Magnitude(this Vector2f input)
    {
        return MathF.Sqrt(input.X * input.X + input.Y * input.Y);
    }
}