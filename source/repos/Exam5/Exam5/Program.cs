using System;
using System.Threading;

class Cinema
{
    private static readonly object locker = new object();
    private static int totalSeats = 0;

    static void Main(string[] args)
    {
        for (int i = 1; i <= 5; i++)
        {
            Thread visitorThread = new Thread(() => Visit(i));
            visitorThread.Start();
        }
    }

    static void Visit(int visitorNumber)
    {
        bool seated = false;

        lock (locker)
        {
            if (totalSeats + 1 <= 230) // Assuming a total of 230 seats in all rooms
            {
                totalSeats++;
                seated = true;
                Console.WriteLine($"Visitor {visitorNumber} is seated.");
            }
        }

        if (!seated)
        {
            Console.WriteLine($"Visitor {visitorNumber} is waiting for the next session.");
        }
        else
        {
            Thread.Sleep(3000); // Simulating movie session duration
            lock (locker)
            {
                totalSeats--;
                Console.WriteLine($"Visitor {visitorNumber} has left the cinema.");
            }
        }
    }
}
