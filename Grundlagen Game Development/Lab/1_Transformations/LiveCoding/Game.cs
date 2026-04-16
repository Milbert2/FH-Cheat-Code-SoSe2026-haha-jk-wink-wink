using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class Game
{
    private RenderWindow window;
    private View view;
    private Sprite playerSprite;

    private Clock clock;

    private float gameTime;
    private Vertex[] line;

    public Game()
    {
        VideoMode mode = new VideoMode(800, 600);
        window = new RenderWindow(mode, "GrundlagenGameDev");
        window.Closed += (sender, args) => { window.Close(); };
        window.Resized += (sender, args) => { view.Size = (Vector2f)window.Size; };

        view = new View(new Vector2f(0, 0), new Vector2f(800, 600));
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
        gameTime += deltaTime;

        float progress = gameTime / 2;

        const float playerSpeed = 50;
        playerSprite.Position = new Vector2f(Lerp(0, 100, progress), 0);
    }

    private void HandleEvents()
    {
        window.DispatchEvents();
    }

    private void Initialize()
    {
        Texture playerTexture = new Texture("./Assets/car.png");
        playerSprite = new Sprite(playerTexture);
        playerSprite.Scale = new Vector2f(0.5f, 0.5f);
        playerSprite.Position = new Vector2f(0, 0);
        playerSprite.TextureRect = new IntRect((int)playerTexture.Size.X / 2, 0, (int)playerTexture.Size.X / 2, (int)playerTexture.Size.Y);

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