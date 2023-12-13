using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    private static double[] data = new double[2000]; // assuming 2000 points to be plotted
    private static object locker = new object();

    static void Main(string[] args)
    {
        Task dataCalculation = Task.Run(CalculateData);
        Task plotGraph = Task.Run(PlotGraph);

        Task.WaitAll(dataCalculation, plotGraph);
    }

    static void CalculateData()
    {
        for (int i = 0; i < data.Length; i++)
        {
            double x = i * 0.01; // x with a step of 0.01
            double y = 23 * x * x - 33;
            lock (locker)
            {
                data[i] = y;
            }
        }
    }

    static void PlotGraph()
    {
        for (int i = 0; i < data.Length; i++)
        {
            double y;
            lock (locker)
            {
                y = data[i];
            }
            Console.WriteLine($"x = {i * 0.01}, y = {y}");
        }
    }
}
