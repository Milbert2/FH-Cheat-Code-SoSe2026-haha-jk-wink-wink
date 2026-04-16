using BGD;
using SFML.Graphics;
using SFML.System;

internal class Hud : GameObject
{
    private Text highscoreText;
    private Text scoreText;
    private Text gameOverText;

    public Hud()
    {
        Font arial = new Font("C:/Windows/Fonts/arial.ttf");

        highscoreText = new Text("0", arial);
        scoreText = new Text("0", arial);
        gameOverText = new Text("Game Over", arial);

        highscoreText.CharacterSize = scoreText.CharacterSize = 30;
        highscoreText.FillColor = scoreText.FillColor = Color.White;
        highscoreText.OutlineColor = scoreText.OutlineColor = Color.Black;
        highscoreText.OutlineThickness = scoreText.OutlineThickness = 1;

        highscoreText.Position = new Vector2f(10, 10);
        scoreText.Position = new Vector2f(10, 40);

        gameOverText.CharacterSize = 40;
        gameOverText.FillColor = Color.Yellow;
        gameOverText.OutlineColor = Color.Red;
        gameOverText.OutlineThickness = 3;
        Origin =
            new Vector2f(gameOverText.GetLocalBounds().Width / 2,
        gameOverText.GetLocalBounds().Height / 2);
        gameOverText.Position = new Vector2f(Game.WIDTH / 2f, Game.HEIGHT / 2f);
    }

    public override void Update(float deltaTime) { }

    public override void Draw(RenderWindow window)
    {
        window.Draw(highscoreText);
        window.Draw(scoreText);
    }

    public void SetHighScoreText(string text)
    {
        highscoreText.DisplayedString = text;
    }

    public void SetCurrentScoreText(string text)
    {
        scoreText.DisplayedString = text;
    }
}