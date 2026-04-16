using BGD;
using SFML.Graphics;
using SFML.System;

internal class ProjectileEmitter : GameObject
{
    private List<Vector2f> shootDirections = new List<Vector2f>();
    private float timeCache = 0;
    private float tickSpeed = 2f;

    public List<Projectile> projectiles = new List<Projectile>();

    public IntRect CollisionRect { get; private set; }
    private Vector2i ColliderSize = new Vector2i(50, 50);

    private Sprite sprite;

    public ProjectileEmitter()
    {
        AssetManager.LoadTexture("Emitter", @".\Assets\skull.png");
        sprite = new Sprite(AssetManager.Textures["Emitter"]);

        sprite.TextureRect =
            new IntRect(0,
                0,
                sprite.TextureRect.Width,
                sprite.TextureRect.Height);

        Scale = new Vector2f(0.1f, 0.1f);
        Origin = new Vector2f(sprite.TextureRect.Width / 2, sprite.TextureRect.Height / 2);

        UpdateTransform();
        UpdateCollider();
    }

    public override void Draw(RenderWindow window)
    {

        window.Draw(sprite);

        foreach (var projectile in projectiles)
        {
            projectile.Draw(window);
        }
    }

    public void SetupShootDirections(bool Left, bool right, bool up, bool down)
    {
        if (Left) shootDirections.Add(new Vector2f(-1, 0));
        if (right) shootDirections.Add(new Vector2f(1, 0));
        if (up) shootDirections.Add(new Vector2f(0, -1));
        if (down) shootDirections.Add(new Vector2f(0, 1));
    }

    public void SetInterval(float intervall)
    {
        tickSpeed = intervall;
    }

    private void UpdateTransform()
    {
        sprite.Origin = Origin;
        sprite.Position = Position;
        sprite.Rotation = Rotation;
        sprite.Scale = Scale;
    }

    private void UpdateCollider()
    {
        CollisionRect =
          new IntRect(
              (int)Position.X - (ColliderSize.X / 2),
              (int)Position.Y - (ColliderSize.Y / 2),
              (int)ColliderSize.X,
              (int)ColliderSize.Y);
    }

    public override void Update(float deltaTime)
    {
        UpdateTransform();
        UpdateCollider();

        timeCache += deltaTime;
        if (timeCache > tickSpeed)
        {
            timeCache = 0;
            shootProjectiles();
        }

        List<Projectile> toRemove = new List<Projectile>();

        foreach (var projectile in projectiles)
        {
            projectile.Update(deltaTime);
            if (projectile.Position.X > Game.WIDTH)
                toRemove.Add(projectile);
        }

        foreach (var projectile in toRemove)
        {
            projectiles.Remove(projectile);
        }

    }

    private void shootProjectiles()
    {
        AssetManager.Sounds["FireSpawn"].Play();

        for (int i = 0; i < shootDirections.Count; i++)
        {
            var projectile = new Projectile(Position, shootDirections[i] * 100);
            projectiles.Add(projectile);

        }

    }
}