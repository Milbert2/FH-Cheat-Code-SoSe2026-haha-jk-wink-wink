using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace SFML_Intro
{
    internal class Game
    {
        private const int WIDTH = 800;
        private const int HEIGHT = 400;
        private const string TITLE = "GrundlagenGameDev";
        Color backgroundColor = new Color(25, 25, 25);

        private VideoMode mode = new VideoMode(WIDTH, HEIGHT);
        private RenderWindow window;
        private Clock clock = new Clock();
        float gameTime = 0f;

        Vector2f[] points = new Vector2f[] { new(100, 300), new(200, 100), new(700, 300) };

        public Game()
        {
            this.window = new RenderWindow(this.mode, TITLE);

            this.window.SetVerticalSyncEnabled(true);

            // this is called lambda expression ( anonymous/nameless function)
            this.window.Closed += (sender, args) =>
            {
                this.window.Close();
            };

            // the same functionality but without lambda
            this.window.KeyPressed += CloseGame;
        }

        private void CloseGame(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
            {
                window?.Close();
            }
        }

        private void Initialize()
        {
        }

        public void Run()
        {
            Initialize();

            while (this.window.IsOpen)
            {
                float deltaTime = clock.Restart().AsSeconds();

                this.HandleEvents();
                this.Update(deltaTime);
                this.Draw();
            }
        }

        private void HandleEvents()
        {
            this.window.DispatchEvents();
        }

        private void Draw()
        {
            this.window.Clear(backgroundColor);

            foreach (var point in points)
            {
                DrawCircle(point, 5, Color.Cyan);
            }

            var normalizedSin = (MathF.Sin(gameTime) + 1f) / 2f;

            DrawCircle(EvaluateBezier(points[0], points[1], points[2], normalizedSin), 5, Color.Magenta);

            this.window.Display();
        }

        private void Update(float deltaTime)
        {
            gameTime += deltaTime;
        }

        #region Utils
        public void DrawCircle(Vector2f position, int radius, Color color)
        {
            var circle = new CircleShape(radius);
            circle.Origin = new Vector2f(radius, radius);
            circle.Position = position;
            circle.FillColor = color;
            window.Draw(circle);
        }

        public static float Lerp(float firstFloat, float secondFloat, float t, bool clamped = true)
        {
            if (clamped)
                t = Math.Clamp(t, 0, 1);

            return firstFloat * (1f - t) + secondFloat * t;
        }

        public static Vector2f Lerp(Vector2f firstVector, Vector2f secondVector, float t, bool clamped = true)
        {
            return new Vector2f()
            {
                X = Lerp(firstVector.X, secondVector.X, t, clamped),
                Y = Lerp(firstVector.Y, secondVector.Y, t, clamped)
            };
        }

        public static Vector2f EvaluateBezier(Vector2f p0, Vector2f p1, Vector2f p2, float t)
        {
            var p01 = Lerp(p0, p1, t);
            var p12 = Lerp(p1, p2, t);
            return Lerp(p01, p12, t);
        }
        
        #endregion
    }
}
