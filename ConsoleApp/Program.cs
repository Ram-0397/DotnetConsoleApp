using FlightSchedulerApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

// Entry point for the program
class Program
{
    static void Main()
    {
        // Consider this as Configs for the given problem 
        const int MAX_ORDERS = 20;
        List<string> flightDestinations = new List<string> { "YYZ", "YYC", "YVR" };
        string origin = "YUL";

        // Read JSON File and deserialize the JSON content into a Dictionary
        string filePath = "../../../Data/coding-assigment-orders.json"; 
        string jsonContent = File.ReadAllText(filePath);
        var orders = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonContent);

        // User Story 1 - Print trip itineraries (only 2 days for a maximum of 6 flights)
        Console.WriteLine("Running User Story 1, expected Output:");
        Console.WriteLine();
        var flightScheduleService = new FlightScheduleService(MAX_ORDERS, flightDestinations, origin);
        flightScheduleService.PrintFlightSchedule(orders);

        Console.WriteLine();

        // User Story 2 - Process Orders and Flights for each destination
        Console.WriteLine("Running User Story 2, expected Output:");
        Console.WriteLine();
        var flightAssignmentService = new FlightAssignmentService(MAX_ORDERS, flightDestinations, origin);
        flightAssignmentService.AssignOrdersToFlights(orders);

        // Wait for user input to close the console
        Console.ReadLine();
    }
}


