using System;
using System.Collections.Generic;

// Responsible for assigning orders to flights
public class FlightAssignmentService
{
    int MAX_ORDERS_PER_FLIGHT;
    List<string> destinations;
    string origin;

    // Constructor
    public FlightAssignmentService(int _maxOrders, List<string> _destinations, string _origin)
    {
        MAX_ORDERS_PER_FLIGHT = _maxOrders;
        destinations = _destinations;
        origin = _origin;
    }

    // Method to assign orders to flights
    public void AssignOrdersToFlights(Dictionary<string, Dictionary<string, string>> orders)
    {
        // Keep track of which flight is assigned which orders
        var flightOrders = new Dictionary<string, List<string>>();

        // Number of days to process (can be easily modified)
        int totalDays = 2; // Handling 2 days, as per the problem exercise stated
        int currentDay = 1;

        // Example of flight destinations
        var flightDestinations = new Dictionary<int, Dictionary<string, string>>()
        {
            {1, new Dictionary<string, string>{{"Flight 1", "YYZ"}, {"Flight 2", "YYC"}, {"Flight 3", "YVR"}}},
            {2, new Dictionary<string, string>{{"Flight 4", "YYZ"}, {"Flight 5", "YYC"}, {"Flight 6", "YVR"}}}
        };

        // Initialize the flightOrders dictionary dynamically based on flightDestinations
        foreach (var day in flightDestinations.Values)
        {
            foreach (var flight in day.Keys)
            {
                flightOrders[flight] = new List<string>();
            }
        }

        // Variable to keep track of the last assigned destination
        string lastAssignedDestination = string.Empty;

        // Iterate over orders and assign them to flights
        foreach (var order in orders)
        {
            string destination = order.Value["destination"];
            string assignedFlight = null;

            // If destination changes, reset the day counter to 1
            if (destination != lastAssignedDestination)
            {
                currentDay = 1;
            }

            // Find the flight based on the destination for the current day
            foreach (var flight in flightDestinations[currentDay])
            {
                if (flight.Value == destination)
                {
                    assignedFlight = flight.Key;
                    break;
                }
            }

            // If no matching flight is found, output the order and continue
            if (assignedFlight == null)
            {
                Console.WriteLine($"Order: {order.Key}, Flight: Not Scheduled ");
                continue;
            }

            // Add the order to the appropriate flight for the current day
            flightOrders[assignedFlight].Add(order.Key);

            // Print the order details with the assigned flight, source, destination, and day
            Console.WriteLine($"Order: {order.Key}, FlightNumber: {assignedFlight}, Departure: {origin}, Arrival: {destination}, Day: {currentDay}");

            // If a flight reaches the max number of orders, move to the next day
            if (flightOrders[assignedFlight].Count >= MAX_ORDERS_PER_FLIGHT)
            {
                currentDay++;
                if (currentDay > totalDays)
                {
                    currentDay = 1; // Reset to Day 1 after all days are processed
                    // Reset flight orders for the new day
                    foreach (var flight in flightOrders.Keys)
                    {
                        flightOrders[flight].Clear();
                    }
                }
            }

            // Update the last assigned destination
            lastAssignedDestination = destination;
        }

        Console.ReadLine();
    }
}
