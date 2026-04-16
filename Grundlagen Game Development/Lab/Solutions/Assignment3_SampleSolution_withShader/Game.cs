using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace BGD;

// Abstract GO
// Player, ProjectileEmitter, Projectile, Hud Class
// Intersect-Logik Player/Projectile (Obstacle?)
// Highscore
// Screen-Borders
// Projictile Spawn Sound, Death Sound (Collision), BG-Music

// ?Boni Restart/Collider
// Projectile, Behaviours? Random Frequence? Auto-Aim? Random Spawn Position?
// Shield? Time-Slowdown?
// Player Dash
internal partial class Game
{
    public const int WIDTH = 640;
    public const int HEIGHT = 480;

    private const string TITLE = "GrundlagenGameDev";

    private readonly Color BG_COLOR = new(50, 50, 50);

    private readonly Clock clock = new();

    private List<GameObject> gameObjects;
    private Player player;
    private List<ProjectileEmitter> projectileEmitters = new List<ProjectileEmitter>();
    private Hud hud;
    private CrystalSpawner crystalSpawner;
    private float elapsedGameTime = 0;
    private int score = 0;
    private int highscore = 0;
    private readonly VideoMode mode = new(WIDTH, HEIGHT);
    private readonly RenderWindow window;
    private RenderStates renderState;

    public Game()
    {
        window = new RenderWindow(mode, TITLE);

        window.SetVerticalSyncEnabled(true);

        // this is called lambda expression (anonymous/nameless function)
        window.Closed += (sender, args) => { window.Close(); };

        // the same functionality but without lambda
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
        highscore = parser.readFile();

        // Setup Inputmanager
        InputManager.Instance.Initialize(window);

        // Initialize Debug Draw 
        DebugDraw.Instance.Initialize(window);

        // Create GameObjects List 
        gameObjects = new List<GameObject>();

        // Setup Player
        player = new Player();
        player.Position = new Vector2f(Game.WIDTH / 2, Game.HEIGHT / 2);
        gameObjects.Add(player);

        // Setup Projectile Emitter and add them to List GameObjects and Emitters
        ProjectileEmitter projectileEmitter = new ProjectileEmitter();
        projectileEmitter.Position = new Vector2f(150f, 100f);
        projectileEmitter.SetupShootDirections(false, true, false, true);
        projectileEmitter.SetInterval(2.0f);

        projectileEmitters.Add(projectileEmitter);
        gameObjects.Add(projectileEmitter);
        projectileEmitter = null;

        projectileEmitter = new ProjectileEmitter();
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

        // Load Sounds
        AssetManager.LoadSound("GameOver", @".\Assets\GameOver.wav");
        AssetManager.LoadSound("Collect", @".\Assets\collect.wav");
        AssetManager.LoadSound("FireSpawn", @".\Assets\fire.wav");

        // Load Music
        AssetManager.LoadMusic("MusicTrack", @".\Assets\musicTrack.ogg");

        // Load Shader
        AssetManager.LoadShader("LightShader", null, null, @".\Assets\LightShader.frag");
        AssetManager.LoadTexture("LightTexture", @".\Assets\LighTextureUV.png");
        renderState = new RenderStates(AssetManager.Shaders["LightShader"]);

        // Play Music
        AssetManager.Music["MusicTrack"].Play();

        // Set Initial Highscore in Hud after initialisation
        hud.setHighScoreText(highscore.ToString());
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
    }

    private void Draw()
    {
        window.Clear(BG_COLOR);

        DrawFloor(new Vector2f(0, 0),
            new Vector2i(10, 10),
            new Vector2i(64, 64));

        foreach (var gameObject in gameObjects)
            gameObject.Draw(window);

        DrawLight(window.Size.X, window.Size.Y);
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

    public void DrawLight(uint ScreenWidth, uint ScreenHeight)
    {
        var rectangle =
            new RectangleShape(new Vector2f(ScreenWidth, ScreenHeight));

        rectangle.Texture = AssetManager.Textures["LightTexture"];

        Vector2f playerScreenPos = new Vector2f(player.Position.X, player.Position.Y);

        playerScreenPos.X /= ScreenWidth;
        playerScreenPos.Y /= ScreenHeight;

        renderState.Shader.SetUniform("PlayerScreenPos", playerScreenPos);

        float coneScale = 0.5f;

        coneScale = MathF.Sin(elapsedGameTime);

        if (coneScale < 0.1f)
            coneScale = 0.1f;


        renderState.Shader.SetUniform("coneScale", coneScale);

        window.Draw(rectangle, renderState);
    }
    private void Update(float deltaTime)
    {
        elapsedGameTime += deltaTime;

        hud.setCurrentScoreText(score.ToString());

        foreach (var gameObject in gameObjects) gameObject.Update(deltaTime);

        // Loop over all Emitter
        foreach (ProjectileEmitter emitter in projectileEmitters)
        {
            // Loop over all Projectiles and check for Collision Logic
            foreach (Projectile p in emitter.projectiles)
            {
                // Collision Logic if Projectile intersects with player
                if (p.CollisionRect.Intersects(player.CollisionRect))
                {
                    Console.WriteLine("Collision w player");
                    AssetManager.Sounds["GameOver"].Play();

                    RespawnPlayer();
                }
            }
        }

        // Loop over all Crystals and check for collision Logic
        foreach (Crystal c in crystalSpawner.crystals)
        {
            if (c.CollisionRect.Intersects(player.CollisionRect))
            {
                Console.WriteLine("Collision w crystal");
                AssetManager.Sounds["Collect"].Play();
                crystalSpawner.removeCrystal(c);
                addScore();
            }
        }

        // Check if Player moves outside of the borders
        checkAreaBorders();

        // InputManager Update
        InputManager.Instance.update();
    }

    private void checkAreaBorders()
    {
        // Define Borders 
        int left = 0;
        int top = 0;
        int right = WIDTH;
        int bottom = HEIGHT;

        // Check if the player position is outside of the borders 
        // player position is calculated +/- half of the player Size ( because player pivot / sprite.Origin -> is at sprite.textureRect.Width / 2 & Height/2 )
        // set player on the min/max valid position
        if (player.Position.Y > bottom - (player.CollisionRect.Height / 2))
            player.Position = new Vector2f(player.Position.X, bottom - (player.CollisionRect.Height / 2));

        if (player.Position.Y < top + (player.CollisionRect.Height / 2))
            player.Position = new Vector2f(player.Position.X, top + (player.CollisionRect.Height / 2));

        if (player.Position.X > right - (player.CollisionRect.Width / 2))
            player.Position = new Vector2f(right - (player.CollisionRect.Width / 2), player.Position.Y);

        if (player.Position.X < left + (player.CollisionRect.Width / 2))
            player.Position = new Vector2f(left + (player.CollisionRect.Width / 2), player.Position.Y);

    }

    private void RespawnPlayer()
    {
        // On Player Death check if Highscore needs to be updated
        if (score > highscore)
        {
            highscore = score;
            hud.setHighScoreText(score.ToString());
        }

        // Reset score , time and player positon
        score = 0;
        elapsedGameTime = 0;
        player.Position = new Vector2f(Game.WIDTH / 2, Game.HEIGHT / 2);
    }

    private void addScore()
    {
        score++;
    }
}