using System;
using System.Threading;

class Program
{
    static SemaphoreSlim cashDesk = new SemaphoreSlim(1, 1); // Максимальна кількість кас, одночасно доступних для покупців
    static Random random = new Random();

    static void Main(string[] args)
    {
        for (int i = 1; i <= 10; i++) // Певна кількість покупців
        {
            Thread customerThread = new Thread(Customer);
            customerThread.Name = $"Customer {i}";
            customerThread.Start();
        }
    }

    static void Customer()
    {
        Console.WriteLine($"{Thread.CurrentThread.Name} entered the supermarket.");

        cashDesk.Wait(); // Покупець очікує на доступ до каси
        Console.WriteLine($"{Thread.CurrentThread.Name} is at the cash desk.");
        Thread.Sleep(random.Next(1000, 5000)); // Час на оплату покупки
        Console.WriteLine($"{Thread.CurrentThread.Name} left the cash desk.");

        cashDesk.Release(); // Звільнення каси
    }
}
