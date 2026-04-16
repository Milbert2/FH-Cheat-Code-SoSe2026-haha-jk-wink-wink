using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace BGD;

internal partial class Game
{
    private const string TITLE = "GrundlagenGameDev";
    public const int WIDTH = 640;
    public const int HEIGHT = 480;

    private static readonly Color BG_COLOR = new(50, 50, 50);

    private Player player;
    private int score = 0;
    private int highscore = 0;
    private Hud hud;

    private List<GameObject> gameObjects;
    private List<ProjectileEmitter> projectileEmitters = new List<ProjectileEmitter>();
    private CrystalSpawner crystalSpawner;

    private Sound gameOverSound;
    private Sound collectSound;
    private Sound fireSpawnSound;
    private Music backgroundMusic;

    private readonly VideoMode mode = new(WIDTH, HEIGHT);
    private readonly RenderWindow window;
    private readonly Clock clock = new();

    public Game()
    {
        window = new RenderWindow(mode, TITLE);

        window.SetVerticalSyncEnabled(true);

        window.Closed += (sender, args) => { window.Close(); };

        window.KeyPressed += CloseGame;
    }

    private void CloseGame(object sender, KeyEventArgs e)
    {
        if (e.Code == Keyboard.Key.Escape) window?.Close();
    }

    private void ShutDown()
    {
        // On Shutdown write Highscore to File
        HighscoreParser parser = new HighscoreParser();
        parser.WriteFile(highscore);
    }

    private void Initialize()
    {
        // Create Parser and Read File
        HighscoreParser parser = new HighscoreParser();
        highscore = parser.ReadFile();

        // Setup Inputmanager
        InputManager.Instance.Initialize(window);

        // Initialize Debug Draw 
        DebugDraw.Instance.Initialize(window);

        // Load Sounds
        gameOverSound = new Sound(new SoundBuffer(@".\Assets\GameOver.wav"));
        collectSound = new Sound(new SoundBuffer(@".\Assets\collect.wav"));
        fireSpawnSound = new Sound(new SoundBuffer(@".\Assets\fire.wav"));

        // Create GameObjects List 
        gameObjects = new List<GameObject>();

        // Setup Player
        player = new Player();
        player.Position = new Vector2f(Game.WIDTH / 2, Game.HEIGHT / 2);
        player.Scale = new Vector2f(0.75f, 0.75f);
        gameObjects.Add(player);

        // Setup Projectile Emitter and add them to List GameObjects and Emitters
        ProjectileEmitter projectileEmitter = new ProjectileEmitter(fireSpawnSound);
        projectileEmitter.Position = new Vector2f(150f, 100f);
        projectileEmitter.SetupShootDirections(false, true, false, true);
        projectileEmitter.SetInterval(2.0f);

        projectileEmitters.Add(projectileEmitter);
        gameObjects.Add(projectileEmitter);
        projectileEmitter = null;

        projectileEmitter = new ProjectileEmitter(fireSpawnSound);
        projectileEmitter.Position = new Vector2f(450f, 350f);
        projectileEmitter.SetupShootDirections(true, true, true, true);
        projectileEmitter.SetInterval(2.0f);

        projectileEmitters.Add(projectileEmitter);
        gameObjects.Add(projectileEmitter);

        // setup Hud
        hud = new Hud();
        gameObjects.Add(hud);

        // setup Crystal Spawner
        crystalSpawner = new CrystalSpawner();
        gameObjects.Add(crystalSpawner);

        // Load / Play Music
        backgroundMusic = new Music(@".\Assets\musicTrack.ogg");
        backgroundMusic.Play();

        // Set Initial Highscore in Hud after initialisation
        hud.SetHighScoreText(highscore.ToString());
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

        ShutDown();
    }

    private void HandleEvents()
    {
        window.DispatchEvents();
        Joystick.Update();
    }

    private void Draw()
    {
        window.Clear(BG_COLOR);

        DrawFloor(new Vector2f(0, 0),
            new Vector2i(10, 10),
            new Vector2i(64, 64));

        foreach (var gameObject in gameObjects)
            gameObject.Draw(window);

        window.Display();
    }

    private void DrawFloor(
        Vector2f position,
        Vector2i tiles,
        Vector2i tileSize
    )
    {
        for (var x = 0; x < tiles.X; x++)
            for (var y = 0; y < tiles.Y; y++)
            {
                var tilepos =
                    new Vector2f(position.X + (x * tileSize.X),
                        position.Y + (y * tileSize.Y));
                DebugDraw.Instance.DrawRectangle(tilepos,
                    tileSize.X,
                    tileSize.Y,
                    (x + y) % 2 == 0 ? Color.White : Color.Black);
            }
    }

    private void Update(float deltaTime)
    {
        hud.SetCurrentScoreText(score.ToString());

        foreach (var gameObject in gameObjects) gameObject.Update(deltaTime);

        // Loop over all Emitters
        foreach (ProjectileEmitter emitter in projectileEmitters)
        {
            // Loop over all Projectiles and check for Collision Logic
            foreach (Projectile p in emitter.Projectiles)
            {
                // Collision Logic if Projectile intersects with player
                if (p.CollisionRect.Intersects(player.CollisionRect))
                {
                    gameOverSound.Play();

                    RespawnPlayer();
                }
            }
        }

        // Loop over all Crystals and check for collision Logic
        foreach (Crystal c in crystalSpawner.Crystals)
        {
            if (c.CollisionRect.Intersects(player.CollisionRect))
            {
                collectSound.Play();
                crystalSpawner.RemoveCrystal(c);
                AddScore();
            }
        }

        // InputManager Update
        InputManager.Instance.Update();
    }

    private void RespawnPlayer()
    {
        // On Player Death check if Highscore needs to be updated
        if (score > highscore)
        {
            highscore = score;
            hud.SetHighScoreText(score.ToString());
        }

        // Reset score , time and player positon
        score = 0;
        player.Position = new Vector2f(Game.WIDTH / 2, Game.HEIGHT / 2);
    }

    private void AddScore()
    {
        score++;
    }
}