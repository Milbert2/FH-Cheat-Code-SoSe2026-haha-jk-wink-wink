using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class Player : GameObject
{
    private const int PLAYER_TILING_X = 10;
    private const int PLAYER_TILING_Y = 8;

    private float animationTime;
    private AnimationType currentAniomationType = 0;
    private int[] animationFrameCounts = new int[] {
        3,
        3,
        1,
        3,
        10,
        10,
        10,
        10
    };

    private enum AnimationType
    {
        IdleDown = 0,
        IdleLeft = 1,
        IdleUp = 2,
        IdleRight = 3,
        WalkDown = 4,
        WalkLeft = 5,
        WalkUp = 6,
        WalkRight = 7
    }

    private Sprite playerSprite;

    public IntRect CollisionRect
    {
        get;
        private set;
    }

    public Player()
    {
        Texture playerTexture = new Texture("./Assets/playerSpriteSheet.png");
        playerSprite = new Sprite(playerTexture);
        playerSprite.TextureRect = new IntRect(0, 0, (int)playerTexture.Size.X / PLAYER_TILING_X, (int)playerTexture.Size.Y / PLAYER_TILING_Y);
        animationTime = 0;
        UpdateTransform();
        UpdateCollision();
    }

    public override void Update(float deltaTime)
    {
        const float playerSpeed = 100;
        const int animationSpeed = 5; // Speed of the animation in fps

        if (InputManager.Instance.GetKeyDown(Keyboard.Key.A))
            currentAniomationType = AnimationType.WalkLeft;
        else
            currentAniomationType = AnimationType.IdleDown;

        Vector2f movementDirection = new Vector2f(0, 0);

        if (InputManager.Instance.GetKeyDown(Keyboard.Key.D))
            movementDirection += new Vector2f(1, 0);
        if (InputManager.Instance.GetKeyDown(Keyboard.Key.W))
            movementDirection += new Vector2f(0, -1);
        if (InputManager.Instance.GetKeyDown(Keyboard.Key.A))
            movementDirection += new Vector2f(-1, 0);
        if (InputManager.Instance.GetKeyDown(Keyboard.Key.S))
            movementDirection += new Vector2f(0, 1);

        movementDirection = movementDirection.Normalize();
        Position += movementDirection * playerSpeed * deltaTime;

        animationTime += deltaTime * animationSpeed;
        int animationFrameIdx = (int)animationTime % animationFrameCounts[(int)currentAniomationType];

        Vector2i currentTextureSize = playerSprite.TextureRect.Size;
        playerSprite.TextureRect = new IntRect(
            animationFrameIdx * currentTextureSize.X,
            (int)currentAniomationType * currentTextureSize.Y,
            currentTextureSize.X,
            currentTextureSize.Y
        );

        UpdateTransform();
        UpdateCollision();
    }

    public override void Draw(RenderWindow window)
    {
        window.Draw(playerSprite);

        var shape = new RectangleShape();
        shape.Position = (Vector2f)CollisionRect.Position;
        shape.Size = (Vector2f)CollisionRect.Size;
        shape.FillColor = Color.Transparent;
        shape.OutlineColor = Color.Red;
        shape.OutlineThickness = 1;
        window.Draw(shape);
    }

    private void UpdateTransform()
    {
        playerSprite.Scale = Scale;
        playerSprite.Position = Position;
        playerSprite.Origin = Origin;
        playerSprite.Rotation = Rotation;
    }

    private void UpdateCollision()
    {
        CollisionRect = new IntRect(
            (Vector2i)Position,
            playerSprite.TextureRect.Size
        );
    }
}