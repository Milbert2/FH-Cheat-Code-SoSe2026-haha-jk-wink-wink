using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class Game
{
    private const int WINDOW_WIDTH = 800;
    private const int WINDOW_HEIGHT = 600;

    private RenderWindow window;
    private View view;
    private Sprite playerSprite;
    private Sound completeSound;


    private Clock clock;

    private float gameTime;
    private Vertex[] line;

    public Game()
    {
        VideoMode mode = new VideoMode(WINDOW_WIDTH, WINDOW_HEIGHT);
        window = new RenderWindow(mode, "GrundlagenGameDev");
        window.Closed += (sender, args) => { window.Close(); };
        window.Resized += (sender, args) => { view.Size = (Vector2f)window.Size; };

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

        // Example of how to draw a line
        window.Draw(line, PrimitiveType.Lines);

        window.Draw(playerSprite);

        window.Display();
    }

    private void Update(float deltaTime)
    {
        if (InputManager.Instance.GetKeyDown(Keyboard.Key.A))
            Console.WriteLine("A pressed");

        if (InputManager.Instance.GetKeyDown(Keyboard.Key.Space))
            completeSound.Play();

        const float animationSpeed = 2;
        gameTime += deltaTime;

        float progress = gameTime / animationSpeed;

        const float playerSpeed = 50;
        playerSprite.Position = new Vector2f(Lerp(0, 100, progress), 0);

        InputManager.Instance.Update(deltaTime);
    }

    private void HandleEvents()
    {
        window.DispatchEvents();
    }

    private void Initialize()
    {
        InputManager.Instance.Initialize(window);

        Texture playerTexture = new Texture("./Assets/car.png");
        playerSprite = new Sprite(playerTexture);
        playerSprite.Scale = new Vector2f(0.5f, 0.5f);
        playerSprite.Position = new Vector2f(0, 0);
        playerSprite.TextureRect = new IntRect((int)playerTexture.Size.X / 2, 0, (int)playerTexture.Size.X / 2, (int)playerTexture.Size.Y);

        SoundBuffer buffer = new SoundBuffer("./Assets/completeSound.wav");
        completeSound = new Sound(buffer);

        // Example of how to draw a line
        line = new Vertex[]
        {
            new Vertex(new Vector2f(0, 0)), new Vertex(new Vector2f(0, 100))
        };

        clock = new Clock();
        gameTime = 0;
    }

    private float Lerp(float start, float target, float t)
    {
        t = Math.Clamp(t, 0, 1);
        return start + (target - start) * t;
    }
}