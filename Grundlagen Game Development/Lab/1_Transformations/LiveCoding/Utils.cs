using SFML.System;

public static class Utils
{
    /// <summary>
    /// Return the squared length of this vector.
    /// </summary>
    /// <param name="input">The vector to get the length of.</param>
    /// <returns>The squared length of the vector.</returns>
    public static float SquaredMagnitude(this Vector2f input)
    {
        return input.X * input.X + input.Y * input.Y;
    }
}