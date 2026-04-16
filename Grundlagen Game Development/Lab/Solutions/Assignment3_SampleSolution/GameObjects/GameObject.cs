using SFML.Graphics;

internal abstract class GameObject : Transformable
{
    public GameObject() { }

    public abstract void Update(float deltaTime);
    public abstract void Draw(RenderWindow window);
}