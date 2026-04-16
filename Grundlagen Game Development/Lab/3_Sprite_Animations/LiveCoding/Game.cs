using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class Game
{
    private const int WINDOW_WIDTH = 800;
    private const int WINDOW_HEIGHT = 600;

    private const int PLAYER_TILING_X = 10;
    private const int PLAYER_TILING_Y = 8;

    private RenderWindow window;
    private View view;

    private Sound completeSound;
    private Sprite playerSprite;

    private Clock clock = new Clock();

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

    public Game()
    {
        VideoMode mode = new VideoMode(WINDOW_WIDTH, WINDOW_HEIGHT);
        window = new RenderWindow(mode, "GrundlagenGameDev");
        window.Closed += (sender, args) => window.Close();
        window.Resized += (sender, args) => view.Size = (Vector2f)window.Size;

        view = new View(new Vector2f(0, 0), new Vector2f(WINDOW_WIDTH, WINDOW_HEIGHT));
        window.SetView(view);
    }

    public void Run()
    {
        Initialize();

        while (window.IsOpen)
        {
            float deltaTime = clock.Restart().AsSeconds();

            HandleEvents();

            Update(deltaTime);

            Draw();
        }
    }

    private void Draw()
    {
        window.Clear(Color.Blue);
        window.SetView(view);
        
        window.Draw(playerSprite);
        window.Display();
    }

    private void Update(float deltaTime)
    {
        const float playerSpeed = 50;
        const int animationSpeed = 5; // Speed of the animation in fps

        if (InputManager.Instance.GetKeyDown(Keyboard.Key.A))
            currentAniomationType = AnimationType.WalkLeft;
        else
            currentAniomationType = AnimationType.IdleDown;

        if (Mouse.IsButtonPressed(Mouse.Button.Left))
            Console.WriteLine("Left mouse button pressed");

        // Console.WriteLine($"Mouse position: {Mouse.GetPosition(window)}");

        Vector2f movementDirection = new Vector2f(0, 0);

        if (InputManager.Instance.GetKeyDown(Keyboard.Key.D))
            movementDirection += new Vector2f(1, 0);
        if (InputManager.Instance.GetKeyDown(Keyboard.Key.W))
            movementDirection += new Vector2f(0, -1);

        movementDirection = movementDirection.Normalize();
        playerSprite.Position += movementDirection * playerSpeed * deltaTime;
        Console.WriteLine(movementDirection);

        if (InputManager.Instance.GetKeyPressed(Keyboard.Key.Space))
            completeSound.Play();

        animationTime += deltaTime * animationSpeed;
        int animationFrameIdx = (int)animationTime % animationFrameCounts[(int)currentAniomationType];

        Vector2i currentTextureSize = playerSprite.TextureRect.Size;
        playerSprite.TextureRect = new IntRect(
            animationFrameIdx * currentTextureSize.X,
            (int)currentAniomationType * currentTextureSize.Y,
            currentTextureSize.X,
            currentTextureSize.Y
        );

        

        InputManager.Instance.Update(deltaTime);
    }

    private void HandleEvents()
    {
        window.DispatchEvents();
    }

    private void Initialize()
    {
        InputManager.Instance.Initialize(window);

        Texture playerTexture = new Texture("./Assets/playerSpriteSheet.png");
        playerSprite = new Sprite(playerTexture);
        playerSprite.Scale = new Vector2f(1, 1);
        playerSprite.Position = new Vector2f(0, 0);
        playerSprite.TextureRect = new IntRect(0, 0, (int)playerTexture.Size.X / PLAYER_TILING_X, (int)playerTexture.Size.Y / PLAYER_TILING_Y);
        animationTime = 0;

        SoundBuffer buffer = new SoundBuffer("./Assets/completeSound.wav");
        completeSound = new Sound(buffer);
    }

    private float Lerp(float start, float target, float t)
    {
        t = Math.Clamp(t, 0, 1);
        return start + (target - start) * t;
    }
}