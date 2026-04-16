using System.Collections.ObjectModel;
using BGD;
using SFML.Graphics;
using SFML.System;

class CrystalSpawner : GameObject
{
    private static readonly Random rng = new Random();

    public IEnumerable<Crystal> Crystals
    {
        get
        {
            return crystals;
        }
    }

    private List<Crystal> crystals = new List<Crystal>();
    private List<Crystal> toRemove = new List<Crystal>();

    private float elapsedTime = 0;
    private float spawnInterval = 5f;

    public override void Draw(RenderWindow window)
    {
        foreach (var crystal in crystals)
        {
            crystal.Draw(window);
        }
    }

    public override void Update(float deltaTime)
    {
        elapsedTime += deltaTime;
        if(elapsedTime > spawnInterval)
        {
            elapsedTime = 0;
            SpawnCrystal();
        }

        foreach (Crystal crystal in toRemove)
        {
            crystals.Remove(crystal);
        }
        toRemove.Clear();

        foreach (Crystal crystal in crystals)
        {
            crystal.Update(deltaTime);
        }
    }

    private void SpawnCrystal()
    {
        Vector2f spawnPosition = new Vector2f(rng.Next(0, Game.WIDTH), rng.Next(0,Game.HEIGHT));

        Crystal crystal = new Crystal(spawnPosition);
        crystals.Add(crystal);
    }

    public void RemoveCrystal(Crystal crystal)
    {
        toRemove.Add(crystal);
    }
}