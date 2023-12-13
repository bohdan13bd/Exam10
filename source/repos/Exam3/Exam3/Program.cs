using System;
using System.Collections.Generic;
using System.Threading;

class Passenger
{
    public int ID { get; set; }
    public string SeatType { get; set; } // 'standing' або 'sitting'
    public string Status { get; set; } // 'standing', 'sitting' або 'waiting'

    public Passenger(int id, string seatType)
    {
        ID = id;
        SeatType = seatType;
        Status = seatType == "standing" ? "standing" : "waiting";
    }
}

class BusStop
{
    public List<Passenger> Passengers { get; set; }

    public BusStop(List<Passenger> passengers)
    {
        Passengers = passengers;
    }
}

class Bus
{
    private readonly int _capacity;
    private readonly Dictionary<string, int> _seats = new Dictionary<string, int>();
    private readonly List<Passenger> _passengers = new List<Passenger>();
    private readonly object _lock = new object();

    public Bus(int capacity)
    {
        _capacity = capacity;
        _seats.Add("standing", 12);
        _seats.Add("sitting", 24);
    }

    public void LoadPassengers(BusStop busStop)
    {
        lock (_lock)
        {
            foreach (var passenger in busStop.Passengers)
            {
                if (_passengers.Count < _capacity)
                {
                    if (passenger.SeatType == "sitting" && _seats["sitting"] > 0)
                    {
                        _passengers.Add(passenger);
                        _seats["sitting"]--;
                    }
                    else if (passenger.SeatType == "standing" && _seats["standing"] > 0)
                    {
                        _passengers.Add(passenger);
                        _seats["standing"]--;
                    }
                }
            }
        }
    }
}

class Route
{
    private readonly List<BusStop> _busStops;

    public Route(List<BusStop> busStops)
    {
        _busStops = busStops;
    }

    public void Travel(Bus bus)
    {
        for (int i = 0; i < _busStops.Count; i++)
        {
            Console.WriteLine($"Bus arrived at stop {i + 1}");
            bus.LoadPassengers(_busStops[i]);
            Thread.Sleep(new Random().Next(1000, 3000));
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Passenger> passengers = new List<Passenger>();
        for (int i = 0; i < 36; i++)
        {
            passengers.Add(new Passenger(i, i < 12 ? "sitting" : "standing"));
        }

        List<BusStop> busStops = new List<BusStop>();
        for (int i = 0; i < passengers.Count; i += 6)
        {
            busStops.Add(new BusStop(passengers.GetRange(i, 6)));
        }

        Bus bus = new Bus(capacity: 36);
        Route route = new Route(busStops);
        route.Travel(bus);
    }
}
