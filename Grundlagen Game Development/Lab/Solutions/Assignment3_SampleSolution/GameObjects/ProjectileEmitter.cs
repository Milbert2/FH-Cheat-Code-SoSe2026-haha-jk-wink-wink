using BGD;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

internal class ProjectileEmitter : GameObject
{
    public IEnumerable<Projectile> Projectiles
    {
        get { return projectiles; }
    }

    public IntRect CollisionRect { get; private set; }

    private List<Vector2f> shootDirections = new List<Vector2f>();
    private List<Projectile> projectiles = new List<Projectile>();
    private float elapsedTime = 0;
    private float spawnInterval = 2f;
    
    private Vector2i ColliderSize = new Vector2i(50, 50);
    private Sprite sprite;
    private Sound spawnSound;

    public ProjectileEmitter(Sound spawnSound)
    {
        this.spawnSound = spawnSound;

        Texture emitterTexture = new Texture(@".\Assets\skull.png");
        sprite = new Sprite(emitterTexture);

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
        spawnInterval = intervall;
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

        elapsedTime += deltaTime;
        if (elapsedTime > spawnInterval)
        {
            elapsedTime = 0;
            ShootProjectiles();
        }

        for (int i = projectiles.Count - 1; i >= 0; i--)
        {
            Projectile projectile = projectiles[i];
            projectile.Update(deltaTime);

            if (projectile.Position.X > Game.WIDTH || projectile.Position.X < 0 || 
                projectile.Position.Y > Game.HEIGHT || projectile.Position.Y < 0)
                projectiles.RemoveAt(i);
        }
    }

    private void ShootProjectiles()
    {
        spawnSound.Play();

        for (int i = 0; i < shootDirections.Count; i++)
        {
            var projectile = new Projectile(Position, shootDirections[i] * 100);
            projectiles.Add(projectile);
        }
    }
}