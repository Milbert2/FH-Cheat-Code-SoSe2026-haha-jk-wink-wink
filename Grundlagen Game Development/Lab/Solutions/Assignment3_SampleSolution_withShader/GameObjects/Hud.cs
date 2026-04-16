using BGD;
using SFML.Graphics;
using SFML.System;

internal class Hud : GameObject
{
    private Font font;

    private Text highscoreText,
        scoreText,
        gameOverText;

    public Hud()
    {
        AssetManager.LoadFont("Arial", "C:/Windows/Fonts/arial.ttf");

        highscoreText = new Text("0", AssetManager.Fonts["Arial"]);
        scoreText = new Text("0", AssetManager.Fonts["Arial"]);
        gameOverText = new Text("Game Over", AssetManager.Fonts["Arial"]);

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

    public override void Update(float deltaTime)
    {
    }

    public override void Draw(RenderWindow window)
    {
        window.Draw(highscoreText);
        window.Draw(scoreText);
        //window.Draw(gameOverText);
    }

    public void setHighScoreText(string text)
    {
        highscoreText.DisplayedString = text;
    }

    public void setCurrentScoreText(string text){
        scoreText.DisplayedString = text;
    }
}