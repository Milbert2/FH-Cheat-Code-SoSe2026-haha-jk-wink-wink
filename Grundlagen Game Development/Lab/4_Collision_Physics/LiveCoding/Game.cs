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

    private Player player;
    private Obstacle obstacle;

    private List<GameObject> gameObjects;

    private Sound completeSound;
    private Clock clock = new Clock();

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
        gameObjects.Sort((goA, goB) =>
        {
            return goA.Position.Y.CompareTo(goB.Position.Y);
        });

        window.Clear(Color.Blue);
        window.SetView(view);
        
        foreach (var go in gameObjects)
            go.Draw(window);

        window.Display();
    }

    private void Update(float deltaTime)
    {   
        foreach (var go in gameObjects)
            go.Update(deltaTime);

        if (InputManager.Instance.GetKeyPressed(Keyboard.Key.Space))
            completeSound.Play();

        if (player.CollisionRect.Intersects(obstacle.CollisionRect))
            Console.WriteLine("Player collides with obstacle.");

        InputManager.Instance.Update(deltaTime);
    }

    private void HandleEvents()
    {
        window.DispatchEvents();
    }

    private void Initialize()
    {
        gameObjects = new List<GameObject>();

        InputManager.Instance.Initialize(window);

        SoundBuffer buffer = new SoundBuffer("./Assets/completeSound.wav");
        completeSound = new Sound(buffer);

        player = new Player();
        player.Scale = new Vector2f(0.5f, 0.5f);

        obstacle = new Obstacle();
        obstacle.Position = new Vector2f(-100, -100);

        gameObjects.Add(player);
        gameObjects.Add(obstacle);
    }

    private float Lerp(float start, float target, float t)
    {
        t = Math.Clamp(t, 0, 1);
        return start + (target - start) * t;
    }
}