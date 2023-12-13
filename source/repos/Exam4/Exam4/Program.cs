using System;
using System.IO;
using System.Threading;

class Program
{
    static bool isFibonacciActive = true;
    static bool isPrimeActive = true;

    static readonly object fiboLock = new object();
    static readonly object primeLock = new object();

    static int fibonacciCount = 0;
    static int primeCount = 0;

    static void Main(string[] args)
    {
        Thread fibonacciThread = new Thread(FindFibonacci);
        Thread primeThread = new Thread(FindPrime);

        fibonacciThread.Start();
        primeThread.Start();

        Console.WriteLine("Threads started. Press any key to stop...");
        Console.ReadKey();

        isFibonacciActive = false;
        isPrimeActive = false;

        fibonacciThread.Join();
        primeThread.Join();

        AnalyzeResults("Fibonacci.txt", "Fibonacci");
        AnalyzeResults("Primes.txt", "Primes");
    }

    static void FindFibonacci()
    {
        int a = 0, b = 1;
        using (StreamWriter writer = new StreamWriter("Fibonacci.txt"))
        {
            while (isFibonacciActive)
            {
                lock (fiboLock)
                {
                    writer.WriteLine(a);
                    int temp = a + b;
                    a = b;
                    b = temp;
                    fibonacciCount++;
                }
                Thread.Sleep(100); // Optional: Add a delay to control the rate of generating Fibonacci numbers
            }
        }
    }

    static bool IsPrime(int number)
    {
        if (number <= 1) return false;
        if (number <= 3) return true;

        if (number % 2 == 0 || number % 3 == 0) return false;

        int i = 5;
        while (i * i <= number)
        {
            if (number % i == 0 || number % (i + 2) == 0)
                return false;
            i += 6;
        }
        return true;
    }

    static void FindPrime()
    {
        int number = 2;
        using (StreamWriter writer = new StreamWriter("Primes.txt"))
        {
            while (isPrimeActive)
            {
                lock (primeLock)
                {
                    if (IsPrime(number))
                    {
                        writer.WriteLine(number);
                        primeCount++;
                    }
                    number++;
                }
                Thread.Sleep(100); // Optional: Add a delay to control the rate of finding prime numbers
            }
        }
    }

    static void AnalyzeResults(string filePath, string label)
    {
        Console.WriteLine($"Results for {label}:");
        string content = File.ReadAllText(filePath);
        Console.WriteLine(content);
        int count = label.ToLower() switch
        {
            "fibonacci" => fibonacciCount,
            "primes" => primeCount,
            _ => 0,
        };
        Console.WriteLine($"Total {label}: {count}");
        Console.WriteLine();
    }
}
