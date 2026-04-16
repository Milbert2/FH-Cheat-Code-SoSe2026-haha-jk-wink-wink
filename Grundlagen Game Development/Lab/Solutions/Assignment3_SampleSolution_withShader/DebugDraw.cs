using SFML.Graphics;
using SFML.System;

internal class DebugDraw
{
    private static DebugDraw instance;

    private RenderWindow window;

    public static DebugDraw Instance
    {
        get
        {
            if (instance == null) 
                instance = new DebugDraw();

            return instance;
        }
    }

    public void Initialize(RenderWindow window)
    {
        this.window = window;
    }

    public void DrawLine(Vector2f startPoint, Vector2f endPoint, Color color)
    {
        var line =
            new Vertex[]
            {
                new(startPoint, color),
                new(endPoint, color)
            };
        window.Draw(line, 0, 2, PrimitiveType.Lines);
    }

    public void DrawRectOutline(Vector2f position, int width, int height, Color color)
    {
        var bottomLeftPos = new Vector2f(position.X, position.Y + height);
        var topLeftPos = new Vector2f(position.X, position.Y);
        var topRightPos = new Vector2f(position.X + width, position.Y);
        var bottomRightPos = new Vector2f(position.X + width, position.Y + height);

        DrawLine(bottomLeftPos, topLeftPos, color);
        DrawLine(topLeftPos, topRightPos, color);
        DrawLine(topRightPos, bottomRightPos, color);
        DrawLine(bottomRightPos, bottomLeftPos, color);
    }

    public void DrawRectOutline(IntRect intRect, Color color)
    {
        var position = new Vector2f(intRect.Left, intRect.Top);
        DrawRectOutline(position, intRect.Width, intRect.Height, color);
    }

    public void DrawRectangle(Vector2f position, int width, int height, Color color)
    {
        var rectangle = new RectangleShape(new Vector2f(width, height));

        rectangle.Position = position;
        rectangle.FillColor = color;
        window.Draw(rectangle);
    }

    public void DrawRectangle(IntRect rect, Color color)
    {
        DrawRectangle(new Vector2f(rect.Left, rect.Top), rect.Width, rect.Height, color);
    }
}