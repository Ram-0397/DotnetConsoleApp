public class FlightAssignmentService
{
    int maxOrders;
    List<string> flightDestinations;
    string origin;
   
   // Constructor 
    public FlightAssignmentService(int _maxOrders, List<string> _flightDestinations, string _origin)
    {
        maxOrders = _maxOrders;
        flightDestinations = _flightDestinations;
        origin = _origin;
    }

    public void AssignOrdersToFlights(Dictionary<string, Dictionary<string, string>> orders)
    {
        var flightOrders = new Dictionary<string, List<string>>();

        // Calculate the number of flights needed (one per destination for each day)
        int totalFlights = flightDestinations.Count * 3;  // 3 flights per day (example)

        // Calculate total days based on the number of flights and the number of flights per day
        int totalDays = (int)Math.Ceiling((double)orders.Count / (double)(3 * maxOrders));

        int orderCounter = 1;  // Starting from order 001

        // Create flights for each day dynamically based on totalDays
        var dayFlights = new Dictionary<int, List<Tuple<string, string>>>();
        
        // Assign flights for each day
        for (int day = 1; day <= totalDays; day++)
        {
            var flightsForDay = new List<Tuple<string, string>>();
            int flightNumber = (day - 1) * 3 + 1;  // Flight numbering starts at 1 for day 1
            foreach (var destination in flightDestinations)
            {
                flightsForDay.Add(new Tuple<string, string>($"Flight {flightNumber}", destination));
                flightNumber++;
            }
            dayFlights[day] = flightsForDay;
        }

        // Initialize flight orders for each flight
        foreach (var day in dayFlights.Values)
        {
            foreach (var flight in day)
            {
                flightOrders[flight.Item1] = new List<string>();
            }
        }

        // For each order, attempt to assign it to a flight based on the day and destination
        foreach (var order in orders)
        {
            string destination = order.Value["destination"];
            string assignedFlight = null;
            bool flightAssigned = false;

            // Attempt to assign the order to a flight for Day 1
            foreach (var flight in dayFlights[1])  // Check Day 1 flights
            {
                if (flight.Item2 == destination)
                {
                    assignedFlight = flight.Item1;
                    if (flightOrders[assignedFlight].Count < maxOrders)
                    {
                        flightOrders[assignedFlight].Add(order.Key);

                        // Output the order assignment
                        Console.WriteLine($"order: {order.Key}, flightNumber: {assignedFlight}, departure: {origin}, arrival: {destination}, day: 1");

                        flightAssigned = true;
                        break;
                    }
                }
            }

            // Check if flight for Day 2 needs to be assigned
            if (!flightAssigned)
            {
                foreach (var flight in dayFlights[2])  // Check Day 2 flights
                {
                    if (flight.Item2 == destination)
                    {
                        assignedFlight = flight.Item1;
                        if (flightOrders[assignedFlight].Count < maxOrders)
                        {
                            flightOrders[assignedFlight].Add(order.Key);

                            // Output the order assignment for Day 2
                            Console.WriteLine($"order: {order.Key}, flightNumber: {assignedFlight}, departure: {origin}, arrival: {destination}, day: 2");

                            flightAssigned = true;
                            break;
                        }
                    }
                }
            }

            // If the order couldn't be assigned, output "Flight: Not Scheduled"
            if (!flightAssigned)
            {
                Console.WriteLine($"order: {order.Key}, flightNumber: Not Scheduled");
            }
        }
    }
}
