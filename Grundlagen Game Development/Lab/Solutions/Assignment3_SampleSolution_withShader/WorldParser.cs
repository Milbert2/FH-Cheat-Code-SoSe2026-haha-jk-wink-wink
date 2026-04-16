internal class WorldParser
{
    private const string path = @".\Assets\World.txt";

    char[,] worldData = new char[10, 5];

    public void readFile()
    {

        

        if (File.Exists(path))
        {
            string[] data = File.ReadAllLines(path);

            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    worldData[j, i] = data[i][j];
                }
            }
        }
    }

    public void printData()
    {
         for (int i = 0; i < worldData.GetLength(1); i++)
            {

                for (int j = 0; j < worldData.GetLength(0); j++)
                {
                    Console.WriteLine(worldData[j, i]);
                }
            }
    }
}
