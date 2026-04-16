using BGD;
using SFML.Graphics;
using SFML.System;

class CrystalSpawner : GameObject
{

    float timeCache = 0;
    float tickSpeed = 5f;

     public List<Crystal> crystals = new List<Crystal>();

    private List<Crystal> toRemove = new List<Crystal>();
    public override void Draw(RenderWindow window)
    {
        foreach (var crystal in crystals)
        {
            crystal.Draw(window);
        }
    }

    public override void Update(float deltaTime)
    {

        timeCache += deltaTime;
        if(timeCache > tickSpeed)
        {
            timeCache = 0;
            spawnCrystal();
        }

        foreach (var crystal in toRemove)
        {
            crystals.Remove(crystal);

        }

        toRemove.Clear();

         foreach (var crystal in crystals)
        {
            crystal.Update(deltaTime);
        }
    }

    private void spawnCrystal()
    {
       System.Random r = new Random();
       Vector2f spawnPosition = new Vector2f(r.Next(0, Game.WIDTH), r.Next(0,Game.HEIGHT));
       
        var crystal = new Crystal( spawnPosition );
       crystals.Add(crystal);
    }

    public void removeCrystal(Crystal crystal){
        toRemove.Add(crystal);
    }
}