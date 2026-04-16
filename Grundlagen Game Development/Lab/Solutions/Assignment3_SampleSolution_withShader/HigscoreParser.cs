internal class HighscoreParser
{
    private const string dir = @".\Assets\";
    private const string path = dir + @"HighScore.txt";
    public int readFile()
    {

        if (File.Exists(path))
        {
            string[] data = File.ReadAllLines(path);
            int value = 0;
            int.TryParse(data[0], out value);
            return value;
        }
        else
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (StreamWriter sw = File.CreateText(path))
            {
                return 0;
            }
        }
    }

    public void WriteFile(int highscore)
    {
        using (StreamWriter sw = new StreamWriter(path))
        {
            sw.Write(highscore);
        }
    }
}
