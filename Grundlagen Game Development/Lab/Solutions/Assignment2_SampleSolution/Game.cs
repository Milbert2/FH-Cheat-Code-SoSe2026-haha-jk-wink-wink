using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace GGD_Template;

internal class Game
{
    private const int WIDTH = 640;
    private const int HEIGHT = 480;
    private const string TITLE = "GrundlagenGameDev ⭐ Assignment 2 Solution";

    private const int PLAYER_TILING_X = 10;
    private const int PLAYER_TILING_Y = 8;

    private const float MOVE_SPEED = 100.0f;

    private readonly float animationSpeed = 8;

    private readonly int[] animationTypeFramesCount =
    {
        3,
        3,
        1,
        3,
        10,
        10,
        10,
        10
    };

    private readonly Clock clock = new();

    private readonly List<Drawable> drawables = new();
    private readonly VideoMode mode = new(WIDTH, HEIGHT);
    private readonly RenderWindow window;

    private Sprite player;
    private Sound completeSound;
    private Music backgroundMusic;

    private AnimationType currentAnimation = AnimationType.IdleDown;
    private float animationTimeIndex;

    public Game()
    {
        window = new RenderWindow(mode, TITLE);

        window.SetVerticalSyncEnabled(true);

        window.Closed += OnWindowClosePressed;

        DebugDraw.ActiveWindow = window;
    }

    public void Run()
    {
        Initialize();

        while (window.IsOpen)
        {
            var deltaTime = clock.Restart().AsSeconds();

            window.DispatchEvents();
            Update(deltaTime);
            Draw();
        }
    }

    private void Initialize()
    {
        InputManager.Instance.Initialize(window);

        // Setup Player
        player = new Sprite(new Texture(@".\Assets\playerSpriteSheet.png"));
        player.Scale = new Vector2f(0.6f, 0.6f);
        player.Position = new Vector2f(0, 0);

        player.TextureRect = new IntRect(0, 0, player.TextureRect.Width / PLAYER_TILING_X,
            player.TextureRect.Height / PLAYER_TILING_Y);

        drawables.Add(player);

        // Load Sound / Music
        completeSound = new Sound(new SoundBuffer(@".\Assets\completeSound.wav"));
        backgroundMusic = new Music(@".\Assets\musicTrack.ogg");
    }

    private void Update(float deltaTime)
    {
        if (InputManager.Instance.GetKeyDown(Keyboard.Key.Escape))
            CloseGame();

        OrientationAwareRunToIdle();

        PlayerMovement(deltaTime);

        DoSpriteAnimation();

        HandleAudio();

        // Update Animation Idx
        animationTimeIndex += deltaTime * animationSpeed;

        InputManager.Instance.Update();
    }

    private void OrientationAwareRunToIdle()
    {
        if (currentAnimation == AnimationType.RunUp)
            currentAnimation = AnimationType.IdleUp;
        if (currentAnimation == AnimationType.RunDown)
            currentAnimation = AnimationType.IdleDown;
        if (currentAnimation == AnimationType.RunLeft)
            currentAnimation = AnimationType.IdleLeft;
        if (currentAnimation == AnimationType.RunRight)
            currentAnimation = AnimationType.IdleRight;
    }

    private void PlayerMovement(float deltaTime)
    {
        // Get input
        if (InputManager.Instance.GetKeyPressed(Keyboard.Key.W))
        {
            currentAnimation = AnimationType.RunUp;
            player.Position -= new Vector2f(0, 1) * MOVE_SPEED * deltaTime;
        }

        if (InputManager.Instance.GetKeyPressed(Keyboard.Key.A))
        {
            currentAnimation = AnimationType.RunLeft;
            player.Position -= new Vector2f(1, 0) * MOVE_SPEED * deltaTime;
        }

        if (InputManager.Instance.GetKeyPressed(Keyboard.Key.S))
        {
            currentAnimation = AnimationType.RunDown;
            player.Position += new Vector2f(0, 1) * MOVE_SPEED * deltaTime;
        }

        if (InputManager.Instance.GetKeyPressed(Keyboard.Key.D))
        {
            currentAnimation = AnimationType.RunRight;
            player.Position += new Vector2f(1, 0) * MOVE_SPEED * deltaTime;
        }
    }

    private void DoSpriteAnimation()
    {
        var animationFrame = (int)animationTimeIndex % animationTypeFramesCount[(int)currentAnimation];
        player.TextureRect = new IntRect(animationFrame * player.TextureRect.Width,
            (int)currentAnimation * player.TextureRect.Height, player.TextureRect.Width, player.TextureRect.Height);
    }

    private void HandleAudio()
    {
        // Play Music
        if (InputManager.Instance.GetKeyDown(Keyboard.Key.Num1))
            backgroundMusic.Play();

        if (InputManager.Instance.GetKeyDown(Keyboard.Key.Num2))
            backgroundMusic.Pause();

        // Play SFX
        if (InputManager.Instance.GetKeyDown(Keyboard.Key.Space))
            completeSound.Play();
    }

    private void Draw()
    {
        window.Clear(Color.Blue);

        DrawFloor(new Vector2f(0, 0), new Vector2i(20, 20), new Vector2i(42, 42));

        foreach (var drawable in drawables)
            window.Draw(drawable);

        DebugDraw.DrawRectOutline(
            player.Position,
            (int)player.GetGlobalBounds().Width,
            (int)player.GetGlobalBounds().Height,
            Color.Red);

        window.Display();
    }

    private void DrawFloor(Vector2f position, Vector2i tiles, Vector2i tileSize)
    {
        for (var x = 0; x < tiles.X; x++)
            for (var y = 0; y < tiles.Y; y++)
            {
                var tilepos = new Vector2f(position.X + x * tileSize.X, position.Y + y * tileSize.Y);
                DebugDraw.DrawRectangle(tilepos, tileSize.X, tileSize.Y, (x + y) % 2 == 0 ? Color.White : Color.Black);
            }
    }

    private void OnWindowClosePressed(object? sender, EventArgs e)
    {
        CloseGame();
    }

    private void CloseGame()
    {
        window?.Close();
    }
}