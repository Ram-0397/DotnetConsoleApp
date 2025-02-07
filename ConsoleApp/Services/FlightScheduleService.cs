using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace FlightSchedulerApp.Services
{
    // Responsible for printing the flight schedule for Day 1 and Day 2
    public class FlightScheduleService
    {
        int maxOrders;
        List<string> flightDestinations ;
        string origin;
        
        public FlightScheduleService(int _maxOrders, List<string> _flightDestinations, string _origin)
        {
            maxOrders = _maxOrders;
            flightDestinations = _flightDestinations;
            origin = _origin;
        }
        
        public void PrintFlightSchedule(Dictionary<string, Dictionary<string, string>> orders)
        {
            int flightNumber = 0;

            // Group orders by destination
            var destinations = new Dictionary<string, List<string>>();
            foreach (var order in orders)
            {
                var destination = order.Value["destination"];
                if (!destinations.ContainsKey(destination))
                {
                    destinations[destination] = new List<string>();
                }
                destinations[destination].Add(order.Key);
            }

            // Calculate the number of flights required each day
            int day = 1;
            var flightSchedule = new Dictionary<int, List<string>>();

            while (true)
            {
                bool hasOrders = false;
                var flights = new List<string>();

                foreach (var dest in flightDestinations)
                {
                    if (destinations.ContainsKey(dest) && destinations[dest].Count > 0)
                    {
                        var ordersList = destinations[dest];
                        var flightOrders = ordersList.GetRange(0, Math.Min(maxOrders, ordersList.Count));
                        ordersList.RemoveRange(0, flightOrders.Count);

                        flights.Add(dest);

                        if (ordersList.Count > 0)
                        {
                            hasOrders = true;
                        }
                    }
                }

                if (flights.Count > 0)
                {
                    flightSchedule[day] = flights;
                    day++;
                }

                if (!hasOrders)
                {
                    break;
                }
            }

            // Print the flight itinerary
            foreach (var entry in flightSchedule)
                
            {
                foreach (var flight in entry.Value)
                {
                     //Print flight details
                     Console.WriteLine($"Flight: {++flightNumber}, departure: {origin}, arrival: {flight}, day: {entry.Key}");
                }
            }
            
        }
    }
}
