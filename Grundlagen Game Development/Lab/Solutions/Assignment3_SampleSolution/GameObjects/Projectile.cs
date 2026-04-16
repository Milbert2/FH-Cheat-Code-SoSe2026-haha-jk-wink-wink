using BGD;
using SFML.Graphics;
using SFML.System;

internal class Projectile : GameObject
{
    private const float ANIMATION_SPEED = 8f;
    private const int TILING_X = 6;

    public IntRect CollisionRect { get; private set; }

    private readonly Sprite animatedSprite;
    private float animationTime;

    private Vector2f movementSpeed;
    private Vector2i ColliderSize = new Vector2i(25,25);

    public Projectile(Vector2f position, Vector2f movementSpeed)
    {
        Texture projectileTexture = new Texture(@".\Assets\projectile.png");
        animatedSprite = new Sprite(projectileTexture);
        animatedSprite.TextureRect =
            new IntRect(0,
                0,
                animatedSprite.TextureRect.Width / TILING_X,
                animatedSprite.TextureRect.Height);
        
        Scale = new Vector2f(0.1f, 0.1f);
        Origin = new Vector2f(animatedSprite.TextureRect.Width / 2 , animatedSprite.TextureRect.Height / 2);
        Position=position;
        Rotation = Utils.AngleBetween(new Vector2f(1, 0), movementSpeed).ToDegrees();
        if (movementSpeed.Y < 0)
            Rotation *= -1;
        this.movementSpeed = movementSpeed;
        
        UpdateTransform();
        UpdateCollider();
    }

    public override void Draw(RenderWindow window)
    {
        DoAnimation();

        window.Draw(animatedSprite);
        var bounds = animatedSprite.GetGlobalBounds();
        DebugDraw.Instance.DrawRectOutline(new Vector2f(bounds.Left,bounds.Top),(int) bounds.Width,(int)bounds.Height, Color.Red);
        DebugDraw.Instance.DrawRectOutline(CollisionRect, Color.Green);
    }

    public override void Update(float deltaTime)
    {
        Position+=movementSpeed*deltaTime;

        UpdateTransform();

        UpdateCollider();

        animationTime += deltaTime * ANIMATION_SPEED;

    }

    private void UpdateTransform()
    {
        animatedSprite.Origin = Origin;
        animatedSprite.Position = Position;
        animatedSprite.Rotation = Rotation;
        animatedSprite.Scale = Scale;
    }

      private void UpdateCollider()
    {
          CollisionRect =
            new IntRect(
                (int) Position.X - ColliderSize.X / 2 ,
                (int) Position.Y - ColliderSize.Y / 2,
                (int) ColliderSize.X,
                (int) ColliderSize.Y);
    }

    private void DoAnimation()
    {
        var animationFrame = (int)animationTime % TILING_X;

        animatedSprite.TextureRect = new IntRect
        {
            Left = animationFrame * animatedSprite.TextureRect.Width,
            Top = 0,
            Width = animatedSprite.TextureRect.Width,
            Height = animatedSprite.TextureRect.Height
        };
    }
}