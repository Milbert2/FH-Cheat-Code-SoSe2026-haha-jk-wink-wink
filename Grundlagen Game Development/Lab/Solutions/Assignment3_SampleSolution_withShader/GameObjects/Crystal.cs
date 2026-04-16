using BGD;
using SFML.Graphics;
using SFML.System;

internal class Crystal : GameObject
{
    private Sprite sprite;

    public IntRect CollisionRect { get; private set; }

    private Vector2i ColliderSize = new Vector2i(20, 20);

    public Crystal(Vector2f position)
    {
        AssetManager.LoadTexture("Crystal", @".\Assets\crystal.png");
        sprite = new Sprite(AssetManager.Textures["Crystal"]);
        sprite.TextureRect =
            new IntRect(0,
                0,
                sprite.TextureRect.Width,
                sprite.TextureRect.Height);

        Scale = new Vector2f(0.05f, 0.05f);
        Origin = new Vector2f(sprite.TextureRect.Width / 2, sprite.TextureRect.Height / 2);
        Position = position;

        UpdateTransform();
        UpdateCollider();

    }

    public override void Draw(RenderWindow window)
    {

        window.Draw(sprite);

        DebugDraw.Instance.DrawRectOutline(
             new Vector2f(sprite.GetGlobalBounds().Left, sprite.GetGlobalBounds().Top),
             (int)sprite.GetGlobalBounds().Width,
             (int)sprite.GetGlobalBounds().Height,
             Color.Red);

        DebugDraw.Instance.DrawRectOutline(CollisionRect, Color.Green);
    }

    public override void Update(float deltaTime)
    {
        UpdateTransform();
        UpdateCollider();

    }

    private void UpdateTransform()
    {
        sprite.Position = Position;
        sprite.Rotation = Rotation;
        sprite.Scale = Scale;
        sprite.Origin = Origin;
    }

    private void UpdateCollider()
    {
        CollisionRect =
          new IntRect(
              (int)Position.X - ColliderSize.X / 2,
              (int)Position.Y - ColliderSize.Y / 2,
              (int)ColliderSize.X,
              (int)ColliderSize.Y);
    }

}