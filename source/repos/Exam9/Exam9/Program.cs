using System;
using System.Threading;

class Program
{
    static int[][] map;
    static int mapWidth = 10;
    static int mapHeight = 10;
    static int targetsFound = 0;

    static void Main()
    {
        // Генеруємо випадкову карту з цілями
        GenerateMap();

        int numOfScouts = 5; // Кількість розвідників

        for (int i = 0; i < numOfScouts; i++)
        {
            Thread t = new Thread(Scout);
            t.Start();
        }

        Console.ReadLine();
    }

    static void GenerateMap()
    {
        Random rand = new Random();
        map = new int[mapWidth][];
        for (int i = 0; i < mapWidth; i++)
        {
            map[i] = new int[mapHeight];
            for (int j = 0; j < mapHeight; j++)
            {
                // Шанс зустрічі цілі - наприклад, 1 з 10
                map[i][j] = (rand.Next(10) == 0) ? 1 : 0;
            }
        }
    }

    static void Scout()
    {
        Random rand = new Random();
        int startX = rand.Next(mapWidth);
        int startY = rand.Next(mapHeight);

        Console.WriteLine($"Розвідник розпочав роботу з позиції ({startX}, {startY}).");

        for (int x = startX - 1; x <= startX + 1; x++)
        {
            for (int y = startY - 1; y <= startY + 1; y++)
            {
                if (x >= 0 && y >= 0 && x < mapWidth && y < mapHeight)
                {
                    if (map[x][y] == 1)
                    {
                        Interlocked.Increment(ref targetsFound);
                    }
                }
            }
        }

        Console.WriteLine($"Розвідник завершив роботу та виявив {targetsFound} цілей.");
    }
}
