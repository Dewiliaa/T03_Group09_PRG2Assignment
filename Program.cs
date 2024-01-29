using T03_Group09_PRG2Assignment;
using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using System.Globalization;

// For orders.csv
List<int> id = new List<int>();
List<int> MemberId = new List<int>();
List<DateTime> TimeReceived = new List<DateTime>();
List<DateTime?> TimeFulfilled = new List<DateTime?>();
List<string> Option = new List<string>();
List<int> scoops = new List<int>();
List<bool> Dipped = new List<bool>();
List<string> WaffleFlavour = new List<string>();
List<string> Flavour1 = new List<string>();
List<string> Flavour2 = new List<string>();
List<string> Flavour3 = new List<string>();
List<string> Topping1 = new List<string>();
List<string> Topping2 = new List<string>();
List<string> Topping3 = new List<string>();
List<string> Topping4 = new List<string>();
List<Order> orders = new List<Order>();

// For customers.csv
List<string> Name = new List<string>();
List<int> MemberIId = new List<int>();
List<DateOnly> DOB = new List<DateOnly>();
List<string> MembershipStatus = new List<string>();
List<int> MembershipPoints = new List<int>();
List<int> PunchCard = new List<int>();

Dictionary<int, Queue<Order>> GoldOrderQueues = new Dictionary<int, Queue<Order>>();
Dictionary<int, Queue<Order>> RegularOrderQueues = new Dictionary<int, Queue<Order>>();

void ReadCustomersCSV(string filePath)
{
    try
    {
        using (StreamReader sr = new StreamReader(filePath))
        {
            string? s = sr.ReadLine(); // heading
            while ((s = sr.ReadLine()) != null)
            {
                string[] data = s.Split(',');

                string customerName = data[0];
                int memberId = int.Parse(data[1]);
                DateOnly dob = DateOnly.Parse(data[2]);
                string membershipStatus = data[3];
                int membershipPoints = int.Parse(data[4]);
                int punchCard = int.Parse(data[5]);

                // Add the data to the lists
                Name.Add(customerName);
                MemberIId.Add(memberId);
                DOB.Add(dob);
                MembershipStatus.Add(membershipStatus);
                MembershipPoints.Add(membershipPoints);
                PunchCard.Add(punchCard);

            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error reading customers from file: {ex.Message}");
    }
}

void ReadOrdersCSV(string filePath)
{
    try
    {
        using (StreamReader sr = new StreamReader("orders.csv"))
        {
            string? s = sr.ReadLine();
            while ((s = sr.ReadLine()) != null)
            {
                string[] data = s.Split(',');

                id.Add(int.Parse(data[0]));
                MemberId.Add(int.Parse(data[1]));
                TimeReceived.Add(DateTime.Parse(data[2]));
                TimeFulfilled.Add(string.IsNullOrEmpty(data[3]) ? (DateTime?)null : DateTime.Parse(data[3]));
                Option.Add(data[4]);
                scoops.Add(int.Parse(data[5]));
                Dipped.Add(!string.IsNullOrEmpty(data[6]) && bool.Parse(data[6]));
                WaffleFlavour.Add(data[7]);
                Flavour1.Add(data[8]);
                Flavour2.Add(data[9]);
                Flavour3.Add(data[10]);
                Topping1.Add(data[11]);
                Topping2.Add(data[12]);
                Topping3.Add(data[13]);
                Topping4.Add(data[14]);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error reading customers from file: {ex.Message}");
    }
}

// Display menu
void DisplayMenu()
{
    Console.WriteLine();
    Console.WriteLine("Menu");
    Console.WriteLine("[1] List all customers information");
    Console.WriteLine("[2] List all current orders");
    Console.WriteLine("[3] Register a new customer");
    Console.WriteLine("[4] Create a customer's order");
    Console.WriteLine("[5] Display order details of a customer");
    Console.WriteLine("[6] Modify order details");
    Console.WriteLine("[0] Exit");
}


string choice;

ReadOrdersCSV("orders.csv");
ReadCustomersCSV("customers.csv");

// Main method
while (true)
{
    // Call the display menu method
    DisplayMenu();
    Console.Write("Enter option: ");
    choice = Console.ReadLine();
    if (choice == "0")
        break;

    else if (choice == "1")
    {
        DisplayCustomerInformation();
    }

    else if (choice == "2")
    {
        DisplayCurrentOrders();
    }

    else if (choice == "3")
    {
        RegisterNewCustomer();
    }

    else if (choice == "4")
    {
        CreateCustomerOrder();
    }

    else if (choice == "5")
    {
        DisplayCusOrder();
    }

    else if (choice == "6")
    {
        // call method
    }

    else
    {
        Console.WriteLine("Invalid option, please try again.");
    }
}
Console.WriteLine("----------");
Console.WriteLine("Goodbye!");
Console.WriteLine("----------");




// Method for option 1 - List all customers
void DisplayCustomerInformation(string filePath = "customers.csv")
{
    // Check if the file exists
    if (File.Exists(filePath))
    {
        // Header and its spacing
        Console.WriteLine("{0,-10} {1,-10} {2,-12} {3,-17} {4,-17} {5,-17}",
            "Name", "MemberId", "DOB", "MembershipStatus", "MembershipPoints", "PunchCard");

        // Read and display customer information
        using (TextFieldParser parser = new TextFieldParser(filePath))
        {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");

            // Skip the header line
            parser.ReadLine();

            while (!parser.EndOfData)
            {
                // Read the fields from the CSV
                string[] fields = parser.ReadFields();

                // Display customer details and its spacing
                Console.WriteLine("{0,-10} {1,-10} {2,-12} {3,-17} {4,-17} {5,-17}",
                    fields[0], int.Parse(fields[1]), DateOnly.Parse(fields[2]), fields[3], int.Parse(fields[4]), int.Parse(fields[5]));
            }
        }
    }
    else
    {
        Console.WriteLine("The CSV file does not exist.");
    }
}


// Method for option 2 - List all current orders, both gold members and regular queue
void DisplayCurrentOrders()
{
    Console.WriteLine("List of all current orders:");

    // Header and its spacing
    Console.WriteLine("{0,-5} {1,-10} {2,-25} {3,-25} {4,-12} {5,-12} {6,-12} {7,-20} {8,-14} {9,-14} {10,-14} {11,-14} {12,-14} {13,-14} {14,-14}",
        "Id", "MemberId", "TimeReceived", "TimeFulfilled", "Option", "Scoops", "Dipped", "WaffleFlavour", "Flavour1", "Flavour2", "Flavour3", "Topping1", "Topping2", "Topping3", "Topping4");

    // Display order details
    for (int i = 0; i < id.Count; i++)
    {
        if (!TimeFulfilled[i].HasValue)
        {
            Console.WriteLine("{0,-5} {1,-10} {2,-25} {3,-25} {4,-12} {5,-12} {6,-12} {7,-20} {8,-14} {9,-14} {10,-14} {11,-14} {12,-14} {13,-14} {14,-14}",
            id[i], MemberId[i], TimeReceived[i], TimeFulfilled[i], Option[i], scoops[i], Dipped[i], WaffleFlavour[i],
            Flavour1[i], Flavour2[i], Flavour3[i], Topping1[i], Topping2[i], Topping3[i], Topping4[i]);
        }
    }
}


// Method for option 3 - Register a new customer
void RegisterNewCustomer()
{
    Console.Write("Enter name: ");
    string name = Console.ReadLine();

    int idNum;
    if (!GetValidInput("Enter id number: ", out idNum))
    {
        return;
    }

    // Check if the ID is already registered
    if (IsCustomerRegistered(idNum))
    {
        Console.WriteLine("Person with the same ID is already registered.");
        return;
    }

    DateTime dob;
    if (!GetValidDate("Enter date of birth (d/M/yyyy): ", out dob))
    {
        return;
    }

    // Create customer and Pointcard objects
    Customer customer = new Customer(name, idNum, dob);
    PointCard pointcard = new PointCard();
    customer.Rewards = pointcard;

    pointcard.Points = 0;
    pointcard.Tier = "Ordinary";
    pointcard.PunchCard = 1; // Set PunchCard to 1 for every new registration

    string formattedDob = dob.ToString("d/M/yyyy");

    using (StreamWriter writer = new StreamWriter("customers.csv", true))
    {
        // Use properties of the customer object
        writer.WriteLine($"{name},{idNum},{formattedDob},{pointcard.Tier},{pointcard.Points},{pointcard.PunchCard}");
        Console.WriteLine("Customer registered successfully!");
    }
}

// Method to check if a customer is already registered
bool IsCustomerRegistered(int idNum)
{
    using (StreamReader reader = new StreamReader("customers.csv"))
    {
        // Skip the header line
        reader.ReadLine();

        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(',');

            // Ensure there are enough elements in the array
            if (values.Length >= 2 && int.TryParse(values[1], out int parsedId))
            {
                // Compare ID number to check if the customer is already registered
                if (parsedId == idNum)
                {
                    return true;
                }
            }
            else
            {
                Console.WriteLine($"Invalid data format in line: {line}");
            }
        }
    }

    return false;
}

// Helper method to get valid integer input
bool GetValidInput(string prompt, out int result)
{
    Console.Write(prompt);
    string input = Console.ReadLine();

    if (!int.TryParse(input, out result))
    {
        Console.WriteLine("Invalid input. Please enter an integer.");
        return false;
    }

    return true;
}

// Helper method to get valid date input
bool GetValidDate(string prompt, out DateTime result)
{
    Console.Write(prompt);
    if (!DateTime.TryParseExact(Console.ReadLine(), "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
    {
        Console.WriteLine("Invalid date format.");
        return false;
    }

    return true;
}


// Method for option 4 - Create a customer's order
void CreateCustomerOrder()
{
    List<Customer> customers = ReadCustomersFromCSV();

    Console.WriteLine("List of Customers:");
    foreach (var customer in customers)
    {
        Console.WriteLine($"{customer.Name}, Member ID: {customer.MemberId}");
    }

    Console.Write("Select a customer (enter Member ID): ");
    int selectedMemberId;

    while (!int.TryParse(Console.ReadLine(), out selectedMemberId) || customers.All(c => c.MemberId != selectedMemberId))
    {
        Console.WriteLine("Invalid Member ID. Please try again.");
        Console.Write("Select a customer (enter Member ID): ");
    }

    Customer selectedCustomer = customers.Find(c => c.MemberId == selectedMemberId);

    Console.WriteLine($"Selected Customer: {selectedCustomer.Name}");

    Order customerOrder = new Order(); // Create a new Order object here

    do
    {
        Console.Write("Enter ice cream option: ");
        string option = Console.ReadLine();

        Console.Write("Enter number of scoops: ");
        int scoops;
        while (!int.TryParse(Console.ReadLine(), out scoops) || scoops <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a positive integer for scoops.");
            Console.Write("Enter number of scoops: ");
        }

        Console.Write("Enter ice cream flavour 1: ");
        string flavour1 = Console.ReadLine();

        Console.Write("Enter ice cream flavour 2: ");
        string flavour2 = Console.ReadLine();

        Console.Write("Enter ice cream flavour 3: ");
        string flavour3 = Console.ReadLine();

        Console.Write("Enter if waffle is dipped or not (true or false): ");
        bool isWaffleDipped;
        while (!bool.TryParse(Console.ReadLine(), out isWaffleDipped))
        {
            Console.WriteLine("Invalid input. Please enter 'true' or 'false'.");
            Console.Write("Enter if waffle is dipped or not (true or false): ");
        }

        Console.Write("Enter topping 1: ");
        string topping1 = Console.ReadLine();

        Console.Write("Enter topping 2: ");
        string topping2 = Console.ReadLine();

        Console.Write("Enter topping 3: ");
        string topping3 = Console.ReadLine();

        // Instantiate the appropriate ice cream class based on the user's input
        IceCream iceCream = CreateIceCream(new string[] { option, scoops.ToString(), $"{flavour1}|False, {flavour2}|False, {flavour3}|False", $"{topping1}, {topping2}, {topping3}" });

        // Add the ice cream to the order
        customerOrder.AddIceCream(iceCream);

        Console.Write("Add another ice cream? (Y/N): ");
    } while (Console.ReadLine().ToUpper() == "Y");

    selectedCustomer.CurrentOrder = customerOrder;

    // Use the correct property name for rewards tier
    if (selectedCustomer.Rewards.Tier == "Gold")
    {
        EnqueueOrder(GoldOrderQueues, customerOrder);
    }
    else
    {
        EnqueueOrder(RegularOrderQueues, customerOrder);
    }

    SaveOrderToCSV(customerOrder, selectedMemberId, "orders.csv");

    Console.WriteLine("Order has been made successfully!");
}

// Method to save order information to CSV file
static void SaveOrderToCSV(Order order, int memberId, string filePath)
{
    try
    {
        using (StreamWriter sw = new StreamWriter(filePath, true)) // true for append mode
        {
            // Write order information to CSV format
            sw.WriteLine($"{order.Id},{memberId},{order.TimeReceived},{order.TimeFulfilled}");

            // Optionally, write ice cream details to the CSV file
            foreach (var iceCream in order.IceCreamList)
            {
                sw.WriteLine($"{order.Id},{iceCream.Option},{iceCream.Scoops},{string.Join(",", iceCream.Flavours)},{string.Join(",", iceCream.Toppings)}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while saving the order to CSV: {ex.Message}");
    }
}

// Helper method to create IceCream object based on flavor
static IceCream CreateIceCream(string[] iceCreamDetails)
{
    if (iceCreamDetails.Length < 4)
    {
        Console.WriteLine("Error: Insufficient data to create IceCream.");
        return null; // or handle error appropriately
    }

    try
    {
        string option = iceCreamDetails[4].Trim();
        int scoops = int.Parse(iceCreamDetails[5]);
        List<Flavour> flavours = ParseFlavors(iceCreamDetails[8]);
        List<Topping> toppings = ParseToppings(iceCreamDetails[11]);

        // Determine the concrete class based on the option
        switch (option.ToLower())
        {
            case "vanilla":
                return new VanillaIceCream(option, scoops, flavours, toppings);
            case "chocolate":
                return new ChocolateIceCream(option, scoops, flavours, toppings);
            case "strawberry":
                return new StrawberryIceCream(option, scoops, flavours, toppings);
            case "durian":
                return new DurianIceCream(option, scoops, flavours, toppings);
            case "ube":
                return new UbeIceCream(option, scoops, flavours, toppings);
            case "seasalt":
                return new SeasaltIceCream(option, scoops, flavours, toppings);
            default:
                Console.WriteLine("Invalid ice cream option. Using default Vanilla Ice Cream.");
                return new VanillaIceCream(option, scoops, flavours, toppings);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error creating IceCream: " + ex.Message);
        return null; // or handle exception appropriately
    }
}

static List<Flavour> ParseFlavors(string flavorsString)
{
    List<Flavour> flavors = new List<Flavour>();
    string[] flavorArray = flavorsString.Split(',');

    foreach (var flavor in flavorArray)
    {
        // Split each flavor into type and premium information
        string[] flavorInfo = flavor.Trim().Split('|');

        // Check if premium information is provided
        bool isPremium = flavorInfo.Length > 1 && bool.TryParse(flavorInfo[6], out bool premium);

        flavors.Add(new Flavour(flavorInfo[10], isPremium, 0));
    }

    return flavors;
}

static List<Topping> ParseToppings(string toppingsString)
{
    List<Topping> toppings = new List<Topping>();
    string[] toppingArray = toppingsString.Split(',');

    foreach (var topping in toppingArray)
    {
        toppings.Add(new Topping(topping.Trim()));
    }

    return toppings;
}


static void EnqueueOrder(Dictionary<int, Queue<Order>> orderQueues, Order order)
{
    int key = order.Id;
    if (!orderQueues.ContainsKey(key))
    {
        orderQueues.Add(key, new Queue<Order>());
    }
    orderQueues[key].Enqueue(order);
}


static List<Customer> ReadCustomersFromCSV(string filePath = "customers.csv")
{
    List<Customer> customers = new List<Customer>();

    try
    {
        if (File.Exists(filePath))
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                // Skip the header row
                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] data = line.Split(',');

                    string name = data[0].Trim();
                    int idNum = int.Parse(data[1].Trim());

                    // Ensure consistent date parsing
                    if (DateTime.TryParseExact(data[2].Trim(), "dd/mm/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dob))
                    {
                        string tier = data[3].Trim();
                        int points = int.Parse(data[4].Trim());
                        int punchcard = int.Parse(data[5].Trim());

                        // Create the PointCard object and set points and punch card values
                        PointCard pointCard = new PointCard(points, punchcard);

                        // Create the Customer object and set the values
                        Customer customer = new Customer(name, idNum, dob);
                        customer.Rewards = pointCard; // Assign the created PointCard to the customer

                        customers.Add(customer);
                    }
                    else
                    {
                        Console.WriteLine($"Error parsing date for customer {name}. Skipping this entry.");
                    }
                }
            }
        }
        else
        {
            Console.WriteLine($"Error: File not found - {filePath}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while loading customers: {ex.Message}");
    }

    return customers;
}



// Method for option 5 - Display order details of a customer
void DisplayCusOrder()
{
    // List all customers
    Console.WriteLine("List of all customers: ");
    foreach (var customerName in Name)
    {
        Console.WriteLine(customerName);
    }

    // Prompt user to select a name
    Console.Write("Enter the name of the customer: ");
    string cusName = Console.ReadLine();

    // Check if the entered name is in the list (case-insensitive and trimming whitespace)
    int selectedMemberId = MemberIId[Name.FindIndex(name => name.Trim().Equals(cusName, StringComparison.OrdinalIgnoreCase))];

    if (selectedMemberId != 0)
    {
        Console.WriteLine($"Order details for {cusName} (MemberId: {selectedMemberId})");

        // Display details for each order
        Console.WriteLine("{0,-5} {1,-10} {2,-25} {3,-25} {4,-12} {5,-12} {6,-12} {7,-20} {8,-14} {9,-14} {10,-14} {11,-14} {12,-14} {13,-14} {14,-14}",
            "Id", "MemberId", "TimeReceived", "TimeFulfilled", "Option", "Scoops", "Dipped", "WaffleFlavour", "Flavour1", "Flavour2", "Flavour3", "Topping1", "Topping2", "Topping3", "Topping4");

        for (int i = 0; i < id.Count; i++)
        {
            if (MemberId[i] == selectedMemberId)
            {
                Console.WriteLine("{0,-5} {1,-10} {2,-25} {3,-25} {4,-12} {5,-12} {6,-12} {7,-20} {8,-14} {9,-14} {10,-14} {11,-14} {12,-14} {13,-14} {14,-14}",
                    id[i], MemberId[i], TimeReceived[i], TimeFulfilled[i]?.ToString() ?? "Not fulfilled", Option[i], scoops[i], Dipped[i], WaffleFlavour[i],
                    Flavour1[i], Flavour2[i], Flavour3[i], Topping1[i], Topping2[i], Topping3[i], Topping4[i]);

                Console.WriteLine("------------------------------");
            }
        }
    }
    else
    {
        Console.WriteLine($"Customer with the name '{cusName}' not found.");
    }
}


// Method for option 6 - Modify order details
void ModifyOrder()
{
    // List all customer
    Console.WriteLine("List of all customers: ");
    foreach (var customerName in Name)
    {
        Console.WriteLine(customerName);
    }

    // Prompt user 
    Console.Write("Enter the name of the customer: ");
    string cusName = Console.ReadLine();

    // Check for name
    int selectedMemberId = MemberIId[Name.FindIndex(name => name.Trim().Equals(cusName, StringComparison.OrdinalIgnoreCase))];

    if (selectedMemberId != 0)
    {
        Console.WriteLine($"Modify order details for {cusName} (MemberId: {selectedMemberId})");

        // Display details for each order
        Console.WriteLine("{0,-5} {1,-10} {2,-25} {3,-25} {4,-12} {5,-12} {6,-12} {7,-20} {8,-14} {9,-14} {10,-14} {11,-14} {12,-14} {13,-14} {14,-14}",
            "Id", "MemberId", "TimeReceived", "TimeFulfilled", "Option", "Scoops", "Dipped", "WaffleFlavour", "Flavour1", "Flavour2", "Flavour3", "Topping1", "Topping2", "Topping3", "Topping4");

        for (int i = 0; i < id.Count; i++)
        {
            if (MemberId[i] == selectedMemberId)
            {
                Console.WriteLine("{0,-5} {1,-10} {2,-25} {3,-25} {4,-12} {5,-12} {6,-12} {7,-20} {8,-14} {9,-14} {10,-14} {11,-14} {12,-14} {13,-14} {14,-14}",
                    id[i], MemberId[i], TimeReceived[i], TimeFulfilled[i]?.ToString() ?? "Not fulfilled", Option[i], scoops[i], Dipped[i], WaffleFlavour[i],
                    Flavour1[i], Flavour2[i], Flavour3[i], Topping1[i], Topping2[i], Topping3[i], Topping4[i]);

                Console.WriteLine("------------------------------");

                // Prompt user to modify
                Console.Write("Enter the Order Id to modify: ");
                int orderId = int.Parse(Console.ReadLine());

                // Find the index of the selected order
                int orderIndex = id.FindIndex(order => order == orderId);

                if (orderIndex != -1)
                {
                    // Display the ice cream details in the selected order
                    Console.WriteLine("Ice Cream Details in the selected order:");
                    Console.WriteLine("{0,-12} {1,-12} {2,-20} {3,-14} {4,-14} {5,-14} {6,-14}",
                        "Option", "Scoops", "Dipped", "WaffleFlavour", "Flavour1", "Flavour2", "Flavour3");

                    Console.WriteLine("{0,-12} {1,-12} {2,-20} {3,-14} {4,-14} {5,-14} {6,-14}",
                        Option[orderIndex], scoops[orderIndex], Dipped[orderIndex], WaffleFlavour[orderIndex],
                        Flavour1[orderIndex], Flavour2[orderIndex], Flavour3[orderIndex]);

                    // Prompt user to choose an action
                    Console.WriteLine("Choose an action:");
                    Console.WriteLine("[1] Modify an existing ice cream");
                    Console.WriteLine("[2] Add a new ice cream");
                    Console.WriteLine("[3] Delete an existing ice cream");
                    Console.Write("Enter your choice: ");
                    int actionChoice = int.Parse(Console.ReadLine());

                    switch (actionChoice)
                    {
                        case 1:
                            ModifyExistingIceCream(orderIndex);
                            break;
                        case 2:
                            AddNewIceCream(orderIndex);
                            break;
                        case 3:
                            DeleteExistingIceCream(orderIndex);
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }

                    // Display the updated order details
                    Console.WriteLine("Updated order details:");
                    DisplayOrderDetails(orderIndex);
                }
                else
                {
                    Console.WriteLine($"Order with Id {orderId} not found.");
                }
            }
            else
            {
                Console.WriteLine($"Customer with the name '{cusName}' not found.");
            }
        }

        // Helper method to display ice cream details in a specific order
        void DisplayOrderDetails(int orderIndex)
        {
            Console.WriteLine("{0,-5} {1,-10} {2,-25} {3,-25} {4,-12} {5,-12} {6,-12} {7,-20} {8,-14} {9,-14} {10,-14} {11,-14} {12,-14} {13,-14} {14,-14}",
                "Id", "MemberId", "TimeReceived", "TimeFulfilled", "Option", "Scoops", "Dipped", "WaffleFlavour", "Flavour1", "Flavour2", "Flavour3", "Topping1", "Topping2", "Topping3", "Topping4");

            Console.WriteLine("{0,-5} {1,-10} {2,-25} {3,-25} {4,-12} {5,-12} {6,-12} {7,-20} {8,-14} {9,-14} {10,-14} {11,-14} {12,-14} {13,-14} {14,-14}",
                id[orderIndex], MemberId[orderIndex], TimeReceived[orderIndex], TimeFulfilled[orderIndex], Option[orderIndex], scoops[orderIndex],
                Dipped[orderIndex], WaffleFlavour[orderIndex], Flavour1[orderIndex], Flavour2[orderIndex], Flavour3[orderIndex],
                Topping1[orderIndex], Topping2[orderIndex], Topping3[orderIndex], Topping4[orderIndex]);

            Console.WriteLine("------------------------------");
        }

        // Helper method to modify an existing ice cream in the order
        void ModifyExistingIceCream(int orderIndex)
        {
            // Prompt user to select an ice cream to modify
            Console.Write("Enter the index of the ice cream to modify: ");
            int iceCreamIndex = int.Parse(Console.ReadLine());

            // Check if the selected ice cream index is valid
            if (iceCreamIndex >= 0 && iceCreamIndex < 3)
            {
                // Prompt user for new information
                Console.Write("Enter new option: ");
                Option[orderIndex] = Console.ReadLine();

                Console.Write("Enter new scoops: ");
                scoops[orderIndex] = int.Parse(Console.ReadLine());

                Console.Write("Enter new Dipped (True/False): ");
                Dipped[orderIndex] = bool.Parse(Console.ReadLine());

                Console.Write("Enter new WaffleFlavour: ");
                WaffleFlavour[orderIndex] = Console.ReadLine();

                Console.Write("Enter new Flavour1: ");
                Flavour1[orderIndex] = Console.ReadLine();

                Console.Write("Enter new Flavour2: ");
                Flavour2[orderIndex] = Console.ReadLine();

                Console.Write("Enter new Flavour3: ");
                Flavour3[orderIndex] = Console.ReadLine();

                Console.Write("Enter new Topping1: ");
                Topping1[orderIndex] = Console.ReadLine();

                Console.Write("Enter new Topping2: ");
                Topping2[orderIndex] = Console.ReadLine();

                Console.Write("Enter new Topping3: ");
                Topping3[orderIndex] = Console.ReadLine();

                Console.Write("Enter new Topping4: ");
                Topping4[orderIndex] = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Invalid ice cream index. Please try again.");
            }
        }

        // Helper method to add a new ice cream to the order
        void AddNewIceCream(int orderIndex)
        {
            // Check if the order already has 3 ice creams
            if (scoops[orderIndex] == 3)
            {
                Console.WriteLine("Cannot add more ice creams. The order already has the maximum allowed scoops (3).");
            }
            else
            {
                // Prompt user for new ice cream information
                Console.Write("Enter option for the new ice cream: ");
                string newOption = Console.ReadLine();

                Console.Write("Enter scoops for the new ice cream: ");
                int newScoops = int.Parse(Console.ReadLine());

                Console.Write("Enter Dipped for the new ice cream (True/False): ");
                bool newDipped = bool.Parse(Console.ReadLine());

                Console.Write("Enter WaffleFlavour for the new ice cream: ");
                string newWaffleFlavour = Console.ReadLine();

                Console.Write("Enter Flavour1 for the new ice cream: ");
                string newFlavour1 = Console.ReadLine();

                Console.Write("Enter Flavour2 for the new ice cream: ");
                string newFlavour2 = Console.ReadLine();

                Console.Write("Enter Flavour3 for the new ice cream: ");
                string newFlavour3 = Console.ReadLine();

                Console.Write("Enter Topping1 for the new ice cream: ");
                string newTopping1 = Console.ReadLine();

                Console.Write("Enter Topping2 for the new ice cream: ");
                string newTopping2 = Console.ReadLine();

                Console.Write("Enter Topping3 for the new ice cream: ");
                string newTopping3 = Console.ReadLine();

                Console.Write("Enter Topping4 for the new ice cream: ");
                string newTopping4 = Console.ReadLine();

                // Add the new ice cream to the order
                id.Add(id.Count + 1);
                MemberId.Add(MemberId[orderIndex]);
                TimeReceived.Add(DateTime.Now);
                TimeFulfilled.Add(null);
                Option.Add(newOption);
                scoops.Add(newScoops);
                Dipped.Add(newDipped);
                WaffleFlavour.Add(newWaffleFlavour);
                Flavour1.Add(newFlavour1);
                Flavour2.Add(newFlavour2);
                Flavour3.Add(newFlavour3);
                Topping1.Add(newTopping1);
                Topping2.Add(newTopping2);
                Topping3.Add(newTopping3);
                Topping4.Add(newTopping4);
            }
        }

        // Helper method to delete an existing ice cream from the order
        void DeleteExistingIceCream(int orderIndex)
        {
            // Check if the order has only one ice cream
            if (scoops[orderIndex] == 1)
            {
                Console.WriteLine("Cannot delete the only ice cream in the order. There must be at least one ice cream.");
            }
            else
            {
                // Prompt user to select an ice cream to delete
                Console.Write("Enter the index of the ice cream to delete: ");
                int iceCreamIndex = int.Parse(Console.ReadLine());

                // Check if the selected ice cream index is valid
                if (iceCreamIndex >= 0 && iceCreamIndex < 3)
                {
                    // Remove the selected ice cream from the order
                    id.RemoveAt(orderIndex);
                    MemberId.RemoveAt(orderIndex);
                    TimeReceived.RemoveAt(orderIndex);
                    TimeFulfilled.RemoveAt(orderIndex);
                    Option.RemoveAt(orderIndex);
                    scoops.RemoveAt(orderIndex);
                    Dipped.RemoveAt(orderIndex);
                    WaffleFlavour.RemoveAt(orderIndex);
                    Flavour1.RemoveAt(orderIndex);
                    Flavour2.RemoveAt(orderIndex);
                    Flavour3.RemoveAt(orderIndex);
                    Topping1.RemoveAt(orderIndex);
                    Topping2.RemoveAt(orderIndex);
                    Topping3.RemoveAt(orderIndex);
                    Topping4.RemoveAt(orderIndex);
                }
                else
                {
                    Console.WriteLine("Invalid ice cream index. Please try again.");
                }
            }
        }
    }
}