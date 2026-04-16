using SFML.Graphics;
using SFML.System;
using SFML.Window;

public class Program
{
    public static void Main(string[] args)
    {
        VideoMode mode = new VideoMode(800, 600);
        RenderWindow window = new RenderWindow(mode, "GrundlagenGameDev");
        window.Closed += (sender, args) => window.Close();

        View view = new View(new Vector2f(0, 0), new Vector2f(800, 600));
        window.SetView(view);

        Texture playerTexture = new Texture("./Assets/car.png");
        Sprite playerSprite = new Sprite(playerTexture);
        playerSprite.Scale = new Vector2f(0.5f, 0.5f);
        playerSprite.Position = new Vector2f(0, 0);
        playerSprite.TextureRect = new IntRect(0, 0, (int)playerTexture.Size.X / 2, (int)playerTexture.Size.Y);

        // Example of Extension Methods:
        var vec2 = new Vector2f(2, 1);
        vec2.SquaredLength();
        Utils.SquaredLength(vec2);

        // Example of struct pass by value
        var vec = new CustomVector();
        AddToVector(vec, 2);
        Console.WriteLine(vec.X);

        // Example of struct copy on assignment
        var vec3 = vec2;
        vec3.X = 0;
        Console.WriteLine(vec2.X);

        while (window.IsOpen)
        {
            window.DispatchEvents();
            window.Clear(Color.Blue);

            window.Draw(playerSprite);

            view.Move(new Vector2f(0.01f, 0));
            window.SetView(view);

            window.Display();
        }
    }

    // Struct example:
    public struct CustomVector
    {
        public float X;
        public float Y;

        public CustomVector()
        {
            X = 1;
            Y = 1;
        }

        public CustomVector(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    public static void AddToVector(CustomVector input, int add)
    {
        input.X += add;
        input.Y += add;
    }


}