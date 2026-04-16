using BGD;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

internal class Player : GameObject
{
    private const float ANIMATION_SPEED = 8f;
    private const int TILING_X = 10;
    private const int TILING_Y = 8;
    private static readonly int[]
        animationTypeFramesCount = {3, 3, 1, 3, 10, 10, 10, 10};

    public IntRect CollisionRect { get; private set; }

    private readonly Sprite animatedSprite;
    private float animationTime;
    private Animationtype animationType = Animationtype.IdleDown;

    private Vector2i colliderSize = new Vector2i(50,60);
    private readonly float movementSpeed = 150.0f;

    public Player()
    {
        Texture playerTexture = new Texture(@".\Assets\playerSpriteSheet.png");
        animatedSprite = new Sprite(playerTexture);
        animatedSprite.Scale = new Vector2f(1f, 1f);
        animatedSprite.Position = new Vector2f(0, 0);
        
        animatedSprite.TextureRect =
            new IntRect(0,
                0,
                animatedSprite.TextureRect.Width / TILING_X,
                animatedSprite.TextureRect.Height / TILING_Y);

        Origin = new Vector2f(animatedSprite.TextureRect.Width / 2 , animatedSprite.TextureRect.Height / 2);

        UpdateTransform();
        UpdateCollider();
    }

    public IntRect PlayerCollisionRect { get; private set; }

    public override void Draw(RenderWindow window)
    {
        DoAnimation();

        window.Draw(animatedSprite);

       DebugDraw.Instance.DrawRectOutline( 
            new Vector2f ( animatedSprite.GetGlobalBounds().Left, animatedSprite.GetGlobalBounds().Top),
            (int) animatedSprite.GetGlobalBounds().Width,
            (int) animatedSprite.GetGlobalBounds().Height,
            Color.Red);

        DebugDraw.Instance.DrawRectOutline( CollisionRect, Color.Green);
    }

    public override void Update(float deltaTime)
    {
        UpdateTransform();
        UpdateCollider();
        HandleIdle();

        HandleInput(deltaTime);

        animationTime += deltaTime * ANIMATION_SPEED;

        PlayerCollisionRect =
            new IntRect((int) Position.X,
                (int) Position.Y,
                (int) animatedSprite.GetGlobalBounds().Width,
                (int) animatedSprite.GetGlobalBounds().Height);
    }

    private void UpdateTransform()
    {
        animatedSprite.Position = Position;
        animatedSprite.Rotation = Rotation;
        animatedSprite.Scale = Scale;
        animatedSprite.Origin = Origin;
    }

    private void UpdateCollider()
    {
          CollisionRect =
            new IntRect(
                (int) Position.X - colliderSize.X / 2 ,
                (int) Position.Y - colliderSize.Y / 2,
                (int) colliderSize.X,
                (int) colliderSize.Y);
    }

    private void HandleInput(float deltaTime)
    {
        if (InputManager.Instance.GetKeyPressed(Keyboard.Key.W))
        {
            animationType = Animationtype.RunUp;
            Position -= new Vector2f(0, 1) * movementSpeed * deltaTime;
        }

        if (InputManager.Instance.GetKeyPressed(Keyboard.Key.A))
        {
            animationType = Animationtype.RunLeft;
            Position -= new Vector2f(1, 0) * movementSpeed * deltaTime;
        }

        if (InputManager.Instance.GetKeyPressed(Keyboard.Key.S))
        {
            animationType = Animationtype.RunDown;
            Position += new Vector2f(0, 1) * movementSpeed * deltaTime;
        }

        if (InputManager.Instance.GetKeyPressed(Keyboard.Key.D))
        {
            animationType = Animationtype.RunRight;
            Position += new Vector2f(1, 0) * movementSpeed * deltaTime;
        }
    }

    private void HandleIdle()
    {
        if (animationType == Animationtype.RunUp) 
            animationType = Animationtype.IdleUp;
        if (animationType == Animationtype.RunDown) 
            animationType = Animationtype.IdleDown;
        if (animationType == Animationtype.RunLeft) 
            animationType = Animationtype.IdleLeft;
        if (animationType == Animationtype.RunRight) 
            animationType = Animationtype.IdleRight;
    }

    private void DoAnimation()
    {
        var animationFrame = (int)(animationTime % animationTypeFramesCount[(int)animationType]);

        animatedSprite.TextureRect = new IntRect
        {
            Left = animationFrame * animatedSprite.TextureRect.Width,
            Top = (int)animationType * animatedSprite.TextureRect.Height,
            Width = animatedSprite.TextureRect.Width,
            Height = animatedSprite.TextureRect.Height
        };
    }
}