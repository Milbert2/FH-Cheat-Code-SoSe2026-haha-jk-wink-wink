using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SFML_Intro;

internal class Game
{
    #region Fields

    private const int WIDTH = 640;
    private const int HEIGHT = 480;
    private const string TITLE = "GGD_Assignment01 ⭐ Reference Implementation";

    private readonly Clock clock = new();
    private readonly VideoMode mode = new(WIDTH, HEIGHT);
    private readonly Vector2f[] path =
        {new(50, 50), new(230, 50), new(230, 150), new(280, 150), new(250, 350), new(50, 390)};
    private readonly float speed = 100.0f;

    private int currentPathIdx;
    private float subPathProgress = 0;
    private Sprite playerSprite;
    private Texture playerTexture;

    private View view;
    private readonly RenderWindow window;
    #endregion

    #region Game_Loop

    public Game()
    {
        window = new RenderWindow(mode, TITLE);

        window.SetVerticalSyncEnabled(true);

        // this is called lambda expression (anonymous/nameless function)
        window.Closed += (sender, args) => { window.Close(); };

        // the same functionality but without lambda
        window.KeyPressed += CloseGame;

        window.Resized += (sender, args) =>
        {
            if (view != null)
                view.Size = (Vector2f)window.Size;
        };
    }

    private void Initialize()
    {
        // Setup Player Sprite  
        playerTexture = new Texture(@".\Assets\car.png");

        playerSprite = new Sprite(playerTexture);
        playerSprite.Scale = new Vector2f(0.1f, 0.1f);
        playerSprite.Position = new Vector2f(0, 0);
        playerSprite.Origin = new Vector2f(playerSprite.TextureRect.Width / 2f, playerSprite.TextureRect.Height / 2f);
        playerSprite.Rotation = 90f;


        playerSprite.TextureRect = new IntRect(0, 0, playerSprite.TextureRect.Width, playerSprite.TextureRect.Height);

        view = new View(playerSprite.Position, (Vector2f)window.Size);
    }

    public void Run()
    {
        Initialize();

        while (window.IsOpen)
        {
            var deltaTime = clock.Restart().AsSeconds();

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

        DrawPath();

        var bounds = playerSprite.GetGlobalBounds();

        DrawRectOutline(new Vector2f(bounds.Left, bounds.Top), (int)bounds.Width, (int)bounds.Height, Color.Red);

        window.Display();
    }

    private void Update(float deltaTime)
    {
        view.Center = playerSprite.Position;

        MoveAlongPath(deltaTime);
    }

    #endregion

    #region Helper_Methods

    private void CloseGame(object? sender, KeyEventArgs e)
    {
        if (e.Code == Keyboard.Key.Escape)
            window?.Close();
    }

    private void HandleEvents()
    {
        window.DispatchEvents();
    }

    private void MoveAlongPath(float deltaTime)
    {
        int previousPathIdx = currentPathIdx - 1;
        if (previousPathIdx < 0)
            previousPathIdx = path.Length - 1;

        // calculate progress between current and next point
        var distance = Utils.Distance(path[currentPathIdx], path[previousPathIdx]);
        subPathProgress += (speed * deltaTime) / distance;
        if (subPathProgress >= 1f) // next point reached
        {
            subPathProgress -= 1;
            previousPathIdx = currentPathIdx;
            currentPathIdx = (currentPathIdx + 1) % path.Length;
        }

        // calculate the direction vector
        var direction = path[currentPathIdx] - playerSprite.Position;
        direction = direction.Normalize();

        // calculate Rotation (look at)
        playerSprite.Rotation = MathF.Sign(direction.Y) * Utils.AngleBetween(new Vector2f(1, 0), direction).ToDegrees();

        // calculate movement
        playerSprite.Position = Utils.Lerp(path[previousPathIdx], path[currentPathIdx], subPathProgress);
    }

    private void DrawPath()
    {
        foreach (var p in path)
            DrawCircle(p, 10, Color.Red);

        for (var i = 0; i < path.Length; i++)
        {
            var targetIdx = i + 1 > path.Length - 1 ? 0 : i + 1;
            DrawLine(path[i], path[targetIdx], Color.Red);
        }
    }

    private void DrawLine(Vector2f startPoint, Vector2f endPoint, Color color)
    {
        Vertex[] line = { new(startPoint, color), new(endPoint, color) };
        window.Draw(line, 0, 2, PrimitiveType.Lines);
    }

    private void DrawRectOutline(Vector2f position, int width, int height, Color color)
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

    private void DrawRectangle(Vector2f position, int width, int height, Color color)
    {
        var rectangle = new RectangleShape(new Vector2f(width, height));

        rectangle.Position = position;
        rectangle.FillColor = color;
        window.Draw(rectangle);
    }

    private void DrawRectangle(IntRect rect, Color color)
    {
        DrawRectangle(new Vector2f(rect.Left, rect.Top), rect.Width, rect.Height, color);
    }

    private void DrawCircle(Vector2f position, int radius, Color color)
    {
        var circle = new CircleShape(radius);
        circle.Origin = new Vector2f(radius, radius);
        circle.Position = position;
        circle.FillColor = color;
        window.Draw(circle);
    }
    #endregion
}