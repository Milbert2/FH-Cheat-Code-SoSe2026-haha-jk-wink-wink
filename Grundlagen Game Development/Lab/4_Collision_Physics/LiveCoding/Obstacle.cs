using SFML.Graphics;
using SFML.System;

public class Obstacle : GameObject
{
    public IntRect CollisionRect
    {
        get;
        private set;
    }

    public Obstacle()
    {
        CollisionRect = new IntRect(
            (Vector2i)Position,
            new Vector2i(50, 50)
        );
    }

    public override void Update(float deltaTime)
    {
        CollisionRect = new IntRect(
            (Vector2i)Position,
            new Vector2i(50, 50)
        );
    }

    public override void Draw(RenderWindow window)
    {
        var shape = new RectangleShape();
        shape.Position = (Vector2f)CollisionRect.Position;
        shape.Size = (Vector2f)CollisionRect.Size;
        shape.FillColor = Color.Transparent;
        shape.OutlineColor = Color.Red;
        shape.OutlineThickness = 1;
        window.Draw(shape);
    }

}