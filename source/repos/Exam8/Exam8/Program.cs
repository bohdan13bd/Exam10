using System;
using System.Threading;

class Program
{
    static int[][] obstacleTrack;
    static int runners;
    static int trackLength = 10; // Довжина траси
    static int obstacleDelay = 100; // Затримка при зіткненні з перешкодою (в мілісекундах)

    static void Main()
    {
        runners = 5; // Кількість бігунів

        obstacleTrack = new int[trackLength][];
        for (int i = 0; i < trackLength; i++)
        {
            obstacleTrack[i] = new int[runners];
            for (int j = 0; j < runners; j++)
            {
                obstacleTrack[i][j] = (i % 2 == 0) ? 1 : 0; // Генерація перешкод
            }
        }

        Thread[] threads = new Thread[runners];
        for (int i = 0; i < runners; i++)
        {
            threads[i] = new Thread(Run);
            threads[i].Start(i);
        }

        foreach (Thread t in threads)
        {
            t.Join();
        }

        Console.WriteLine("Всі бігуни дійшли до фінішу.");
    }

    static void Run(object runnerNumber)
    {
        int? runner = (int?)runnerNumber;
        Console.WriteLine($"Бігун {runner.GetValueOrDefault() + 1} розпочав біг.");

        for (int i = 0; i < trackLength; i++)
        {
            if (obstacleTrack[i][runner.GetValueOrDefault()] == 1)
            {
                Console.WriteLine($"Бігун {runner.GetValueOrDefault() + 1} зіткнувся з перешкодою на позиції {i + 1}. Затримка...");
                Thread.Sleep(obstacleDelay);
            }
        }

        Console.WriteLine($"Бігун {runner.GetValueOrDefault() + 1} перетнув фінішну лінію.");
    }
}
