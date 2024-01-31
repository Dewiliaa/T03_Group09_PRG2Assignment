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

int lastOrderId = 4;

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
    Console.WriteLine("[7] Display monthly charged amount & total charged amount for the year");
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
        CreateOrder();
    }

    else if (choice == "5")
    {
        DisplayCusOrder();
    }

    else if (choice == "6")
    {
        ModifyOrder();
    }

    else if (choice == "7")
    {
        Console.Write("Enter the year: ");
        if (int.TryParse(Console.ReadLine(), out int year))
        {
            DisplayMonthlyAndTotalCharges(year);
        }
        else
        {
            Console.WriteLine("Invalid year format. Please enter a valid year.");
        }
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
void DisplayCurrentOrders(string filePath = "orders.csv")
{
    Console.WriteLine("List of all current orders:");

    // Header and its spacing
    Console.WriteLine("{0,-5} {1,-10} {2,-25} {3,-25} {4,-12} {5,-12} {6,-12} {7,-20} {8,-14} {9,-14} {10,-14} {11,-14} {12,-14} {13,-14} {14,-14}",
        "Id", "MemberId", "TimeReceived", "TimeFulfilled", "Option", "Scoops", "Dipped", "WaffleFlavour", "Flavour1", "Flavour2", "Flavour3", "Topping1", "Topping2", "Topping3", "Topping4");

    try
    {
        using (StreamReader sr = new StreamReader(filePath))
        {
            string? s = sr.ReadLine(); // Skip the header
            while ((s = sr.ReadLine()) != null)
            {
                string[] data = s.Split(',');

                bool isDipped = !string.IsNullOrEmpty(data[6]) && bool.Parse(data[6]); // Parse boolean with a fallback for empty string

                // Check if TimeFulfilled is null before displaying the order
                if (string.IsNullOrEmpty(data[3]))
                {
                    Console.WriteLine("{0,-5} {1,-10} {2,-25} {3,-25} {4,-12} {5,-12} {6,-12} {7,-20} {8,-14} {9,-14} {10,-14} {11,-14} {12,-14} {13,-14} {14,-14}",
                        data[0], data[1], DateTime.Parse(data[2]).ToString("dd/MM/yyyy HH:mm"),
                        string.IsNullOrEmpty(data[3]) ? "" : DateTime.Parse(data[3]).ToString("dd/MM/yyyy HH:mm"),
                        data[4], data[5], isDipped, data[7], data[8], data[9], data[10], data[11], data[12], data[13], data[14]);
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error reading orders from file: {ex.Message}");
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

        // Update in-memory lists
        Name.Add(name);
        MemberIId.Add(idNum);
        DOB.Add(DateOnly.FromDateTime(dob));
        MembershipStatus.Add(pointcard.Tier);
        MembershipPoints.Add(pointcard.Points);
        PunchCard.Add(pointcard.PunchCard);
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
void CreateOrder()
{
    while (true)
    {
        // List all customers
        Console.WriteLine("List of all customers: ");
        foreach (var customerName in Name)
        {
            Console.WriteLine(customerName);
        }

        // Prompt user to select a customer
        Console.Write("Enter the name of the customer: ");
        string selectedCustomerName = Console.ReadLine();

        // Find the MemberId for the selected customer
        int selectedMemberId = MemberIId[Name.FindIndex(name => name.Trim().Equals(selectedCustomerName, StringComparison.OrdinalIgnoreCase))];

        // Check if the entered name is in the list (case-insensitive and trimming whitespace)
        if (selectedMemberId != 0)
        {
            Console.WriteLine($"Selected customer: {selectedCustomerName} (MemberId: {selectedMemberId})");

            // Create a new order
            List<string> options = new List<string>();
            List<int> scoopsList = new List<int>();
            List<bool> dippedList = new List<bool>();
            List<string> waffleFlavours = new List<string>();
            List<string> flavours = new List<string>();
            List<string> toppings1 = new List<string>();
            List<string> toppings2 = new List<string>();
            List<string> toppings3 = new List<string>();
            List<string> toppings4 = new List<string>();

            do
            {
                // Prompt user to enter ice cream order details
                Console.Write("Enter ice cream option (cone,waffle,cup): ");
                string iceCreamOption = Console.ReadLine();

                Console.Write("Enter number of scoops (1,2,3):");
                int scoopsCount = int.Parse(Console.ReadLine());

                Console.Write("Enter waffle flavour(Pandan, Redvelvet, Charcoal): ");
                string waffleFlavour = Console.ReadLine();

                Console.Write("Enter flavour 1 (Vanilla, Chocolate, Strawberry, SeaSalt, Ube, Durian): ");
                string flavour = Console.ReadLine();

                Console.Write("Enter flavour 2 (Vanilla, Chocolate, Strawberry, SeaSalt, Ube, Durian): ");
                string flavour2 = Console.ReadLine();

                Console.Write("Enter flavour 3 (Vanilla, Chocolate, Strawberry, SeaSalt, Ube, Durian): ");
                string flavour3 = Console.ReadLine();

                Console.Write("Enter topping 1(Sprinkles, Mochi, Sago, Oreo): ");
                string topping1 = Console.ReadLine();

                Console.Write("Enter topping 2 (Sprinkles, Mochi, Sago, Oreo): ");
                string topping2 = Console.ReadLine();

                Console.Write("Enter topping 3 (Sprinkles, Mochi, Sago, Oreo): ");
                string topping3 = Console.ReadLine();

                Console.Write("Enter topping 4 (Sprinkles, Mochi, Sago, Oreo): ");
                string topping4 = Console.ReadLine();

                Console.Write("Is it dipped? (Yes/No): ");
                bool isDipped = ParseYesNoToBool(Console.ReadLine());

                // Add details to the lists
                options.Add(iceCreamOption);
                scoopsList.Add(scoopsCount);
                dippedList.Add(isDipped);
                waffleFlavours.Add(waffleFlavour);
                flavours.Add(flavour);
                toppings1.Add(topping1);
                toppings2.Add(topping2);
                toppings3.Add(topping3);
                toppings4.Add(topping4);

                // Prompt user if they want to add another ice cream
                Console.Write("Add another ice cream? (Y/N): ");
            } while (Console.ReadLine()?.ToUpper() == "Y");

            // Display the order details
            Console.WriteLine($"Order for {selectedCustomerName} (MemberId: {selectedMemberId}):");

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"Ice Cream {i + 1} - Option: {options[i]}, Scoops: {scoopsList[i]}, Dipped: {dippedList[i]}, " +
                                  $"Waffle Flavour: {waffleFlavours[i]}, Flavour: {flavours[i]}, " +
                                  $"Topping 1: {toppings1[i]}, Topping 2: {toppings2[i]}, " +
                                  $"Topping 3: {toppings3[i]}, Topping 4: {toppings4[i]}");
            }

            // Save the order details to orders.csv
            SaveOrderToCsv(selectedMemberId, options, scoopsList, dippedList, waffleFlavours, flavours, toppings1, toppings2, toppings3, toppings4);
        }
        else
        {
            Console.WriteLine($"Customer with the name '{selectedCustomerName}' not found.");
        }

        // Ask if the user wants to create another order
        Console.Write("Do you want to create another order? (Y/N): ");
        if (Console.ReadLine()?.ToUpper() != "Y")
        {
            break; // Exit the loop if the user doesn't want to create another order
        }
    }
}


bool ParseYesNoToBool(string input)
{
    return input?.Trim().ToUpper() == "YES";
}

void SaveOrderToCsv(int memberId, List<string> options, List<int> scoops, List<bool> dipped,
                    List<string> waffleFlavours, List<string> flavours,
                    List<string> toppings1, List<string> toppings2, List<string> toppings3, List<string> toppings4)
{
    try
    {
        using (StreamWriter writer = new StreamWriter("orders.csv", true))
        {
            for (int i = 0; i < options.Count; i++)
            {
                // Auto-generate order ID and time received
                int orderId = ++lastOrderId; // Increment the order ID for each new order
                DateTime timeReceived = DateTime.Now;

                // Write order details to CSV
                writer.WriteLine($"{orderId},{memberId},{timeReceived},{null},{options[i]},{scoops[i]},{dipped[i]}," +
                                 $"{waffleFlavours[i]},{flavours[i]},{null},{null}," +
                                 $"{toppings1[i]},{toppings2[i]},{toppings3[i]},{toppings4[i]}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error saving order to file: {ex.Message}");
    }
}


// Method for option 5 - Display order details of a customer
void DisplayCusOrder(string filePath = "orders.csv")
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

        // Header and its spacing
        Console.WriteLine("{0,-5} {1,-10} {2,-25} {3,-25} {4,-12} {5,-12} {6,-12} {7,-20} {8,-14} {9,-14} {10,-14} {11,-14} {12,-14} {13,-14} {14,-14}",
            "Id", "MemberId", "TimeReceived", "TimeFulfilled", "Option", "Scoops", "Dipped", "WaffleFlavour", "Flavour1", "Flavour2", "Flavour3", "Topping1", "Topping2", "Topping3", "Topping4");

        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string? s = sr.ReadLine(); // Skip the header
                while ((s = sr.ReadLine()) != null)
                {
                    string[] data = s.Split(',');

                    if (int.Parse(data[1]) == selectedMemberId)
                    {
                        // Check for empty string before parsing as Boolean
                        bool isDipped = !string.IsNullOrEmpty(data[6]) && bool.Parse(data[6]);

                        Console.WriteLine("{0,-5} {1,-10} {2,-25} {3,-25} {4,-12} {5,-12} {6,-12} {7,-20} {8,-14} {9,-14} {10,-14} {11,-14} {12,-14} {13,-14} {14,-14}",
                            data[0], data[1], DateTime.Parse(data[2]).ToString("dd/MM/yyyy HH:mm"),
                            string.IsNullOrEmpty(data[3]) ? "Not fulfilled" : DateTime.Parse(data[3]).ToString("dd/MM/yyyy HH:mm"),
                            data[4], data[5], isDipped, data[7], data[8], data[9], data[10], data[11], data[12], data[13], data[14]);

                        
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading orders from file: {ex.Message}");
        }
    }
    else
    {
        Console.WriteLine($"Customer with the name '{cusName}' not found.");
    }
}


// Method for option 6 - Modify order details
void ModifyOrder(string filePath = "orders.csv")
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

        // Display the orders for the selected customer with empty TimeFulfilled
        DisplayOrdersForCustomer(filePath, selectedMemberId);

        // Prompt user for the desired action
        Console.WriteLine("Choose an action:");
        Console.WriteLine("[1] Modify existing ice cream order");
        Console.WriteLine("[2] Add a new ice cream order");
        Console.WriteLine("[3] Delete existing ice cream order");
        Console.WriteLine("[4] Go back to Main Menu");
        Console.Write("Enter your choice: ");
        int selectedOption = int.Parse(Console.ReadLine());

        switch (selectedOption)
        {
            case 1:
                ModifyExistingIceCream(filePath, selectedMemberId);
                break;

            case 2:
                // Add a new ice cream order (reuse the logic from Option 4)
                CreateOrder();
                break;

            case 3:
                DeleteOrder(filePath, selectedMemberId);
                break;

            case 4:
                return;

            default:
                Console.WriteLine("Invalid action choice.");
                break;
        }

        // Display the new updated order
        Console.WriteLine("Updated Order:");
        DisplayOrdersForCustomer(filePath, selectedMemberId);
    }
    else
    {
        Console.WriteLine($"Customer with the name '{cusName}' not found.");
    }
}

void DisplayOrdersForCustomer(string filePath, int selectedMemberId)
{
    // Header and its spacing
    Console.WriteLine("{0,-5} {1,-10} {2,-25} {3,-25} {4,-12} {5,-12} {6,-12} {7,-20} {8,-14} {9,-14} {10,-14} {11,-14} {12,-14} {13,-14} {14,-14}",
        "Id", "MemberId", "TimeReceived", "TimeFulfilled", "Option", "Scoops", "Dipped", "WaffleFlavour", "Flavour1", "Flavour2", "Flavour3", "Topping1", "Topping2", "Topping3", "Topping4");

    try
    {
        using (StreamReader sr = new StreamReader(filePath))
        {
            string? s = sr.ReadLine(); // Skip the header
            while ((s = sr.ReadLine()) != null)
            {
                string[] data = s.Split(',');

                if (int.Parse(data[1]) == selectedMemberId && string.IsNullOrEmpty(data[3])) // Check for empty TimeFulfilled
                {
                    // Check for empty string before parsing as Boolean
                    bool isDipped = !string.IsNullOrEmpty(data[6]) && bool.Parse(data[6]);

                    Console.WriteLine("{0,-5} {1,-10} {2,-25} {3,-25} {4,-12} {5,-12} {6,-12} {7,-20} {8,-14} {9,-14} {10,-14} {11,-14} {12,-14} {13,-14} {14,-14}",
                        data[0], data[1], DateTime.Parse(data[2]).ToString("dd/MM/yyyy HH:mm"),
                        string.IsNullOrEmpty(data[3]) ? "Not fulfilled" : DateTime.Parse(data[3]).ToString("dd/MM/yyyy HH:mm"),
                        data[4], data[5], isDipped, data[7], data[8], data[9], data[10], data[11], data[12], data[13], data[14]);
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error reading orders from file: {ex.Message}");
    }
}

void ModifyExistingIceCream(string filePath, int memberID)
{
    // Find all orders associated with the MemberId where "TimeFulfilled" is empty
    List<string[]> memberOrders = FindIncompleteOrdersByMemberId(filePath, memberID);

    if (memberOrders.Count > 0)
    {
        // Prompt user to select which order to modify
        Console.Write("Enter the Id of the order to modify: ");
        int orderId = int.Parse(Console.ReadLine());

        // Find the selected order in the list
        var selectedOrder = memberOrders.FirstOrDefault(order => int.Parse(order[0]) == orderId);

        if (selectedOrder != null)
        {
            // Prompt user for modifications
            Console.WriteLine("Choose what to modify:");
            Console.WriteLine("[1] Option");
            Console.WriteLine("[2] Scoops");
            Console.WriteLine("[3] Toppings");
            Console.Write("Enter your choice: ");
            int modificationChoice = int.Parse(Console.ReadLine());

            switch (modificationChoice)
            {
                case 1:
                    ModifyOption(selectedOrder);
                    break;

                case 2:
                    ModifyScoops(selectedOrder);
                    break;

                case 3:
                    ModifyToppings(selectedOrder);
                    break;

                default:
                    Console.WriteLine("Invalid modification choice.");
                    break;
            }

            // Update the order info accordingly
            UpdateOrder(filePath, memberID, orderId, selectedOrder);
        }
        else
        {
            Console.WriteLine($"Order with Id {orderId} not found in your incomplete orders.");
        }
    }
    else
    {
        Console.WriteLine("No incomplete orders found for the provided MemberId.");
    }
}

void ModifyOption(string[] order)
{
    Console.Write("Enter new option (Cone, Cup, or Waffle): ");
    string newOption = Console.ReadLine();

    // If the previous option was "Cone" and the new option is "Waffle," set "Dipped" to empty
    if (order[4].Equals("Cone", StringComparison.OrdinalIgnoreCase) && newOption.Equals("Waffle", StringComparison.OrdinalIgnoreCase))
    {
        order[6] = ""; // Set "Dipped" to empty
    }
    // If the new option is "Cup," set "Dipped" to empty
    else if (newOption.Equals("Cup", StringComparison.OrdinalIgnoreCase))
    {
        order[6] = ""; // Set "Dipped" to empty
    }
    else if (newOption.Equals("Cone", StringComparison.OrdinalIgnoreCase))
    {
        // Additional prompt for Cone
        Console.Write("Do you want it dipped? (yes/no): ");
        order[6] = Console.ReadLine();
    }

    // Update the "Option" column
    order[4] = newOption;

    if (newOption.Equals("Waffle", StringComparison.OrdinalIgnoreCase))
    {
        // Additional prompt for Waffle
        Console.Write("Enter Waffle Flavour (Original, Red Velvet, Charcoal, Pandan): ");
        order[7] = Console.ReadLine();
    }
}

void ModifyScoops(string[] order)
{
    int numberOfScoops;

    // Validate the input for the number of scoops
    while (true)
    {
        // Prompt user for the number of scoops
        Console.Write("Enter new number of scoops (0-3): ");

        if (int.TryParse(Console.ReadLine(), out numberOfScoops) && numberOfScoops >= 0 && numberOfScoops <= 3)
        {
            break; // Valid input, exit the loop
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a number between 0 and 3.");
        }
    }

    // Update the scoops in the order
    order[5] = numberOfScoops.ToString();

    // Additional prompt for Flavour based on the number of scoops
    for (int i = 1; i <= numberOfScoops; i++)
    {
        Console.Write($"Enter Flavour for Scoop {i} (Vanilla, Chocolate, Strawberry, Durian, Ube, Sea Salt): ");
        order[7 + i] = Console.ReadLine();
    }
}

void ModifyToppings(string[] order)
{
    // Clear existing toppings
    for (int i = 1; i <= 4; i++)
    {
        order[10 + i] = "";
    }

    // Additional prompt for Toppings
    for (int i = 1; i <= 4; i++)
    {
        Console.Write($"Enter Topping {i} (Sprinkles, Mochi, Sago, Oreo), type 'stop' to finish: ");
        string topping = Console.ReadLine();

        if (topping.Equals("stop", StringComparison.OrdinalIgnoreCase))
        {
            break;
        }

        order[10 + i] = topping;
    }
}

// Helper method to find incomplete orders by MemberId
List<string[]> FindIncompleteOrdersByMemberId(string filePath, int memberId)
{
    List<string[]> incompleteOrders = new List<string[]>();

    try
    {
        using (StreamReader sr = new StreamReader(filePath))
        {
            string? header = sr.ReadLine(); // Skip the header
            while ((header = sr.ReadLine()) != null)
            {
                string[] data = header.Split(',');

                // Check if the order belongs to the provided MemberId and "TimeFulfilled" is empty
                if (int.Parse(data[1]) == memberId && string.IsNullOrEmpty(data[3]))
                {
                    incompleteOrders.Add(data);
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error reading orders from file: {ex.Message}");
    }

    return incompleteOrders;
}

void UpdateOrder(string filePath, int memberId, int orderId, string[] updatedOrder)
{
    try
    {
        // Read all lines from the file
        string[] lines = File.ReadAllLines(filePath);

        // Find the index of the order in the lines array
        int orderIndex = -1;
        for (int i = 1; i < lines.Length; i++) // Start from 1 to skip the header
        {
            string[] data = lines[i].Split(',');

            if (int.Parse(data[0]) == orderId && int.Parse(data[1]) == memberId)
            {
                orderIndex = i;
                break;
            }
        }

        // Update the order if found
        if (orderIndex != -1)
        {
            lines[orderIndex] = string.Join(",", updatedOrder);

            // Write the updated lines back to the file
            File.WriteAllLines(filePath, lines);
            Console.WriteLine("Order updated successfully.");
        }
        else
        {
            Console.WriteLine($"Order with Id {orderId} not found for MemberId {memberId}.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error updating order: {ex.Message}");
    }
}

void DeleteOrder(string filePath, int selectedMemberId)
{
    // Prompt user to select which order to delete
    Console.Write("Enter the ID of the order to delete: ");
    int orderId = int.Parse(Console.ReadLine());

    try
    {
        // Read all lines from the file
        string[] lines = File.ReadAllLines(filePath);

        // Find the index of the order in the lines array
        int orderIndex = -1;
        for (int i = 1; i < lines.Length; i++) // Start from 1 to skip the header
        {
            string[] data = lines[i].Split(',');

            if (int.Parse(data[0]) == orderId && int.Parse(data[1]) == selectedMemberId)
            {
                orderIndex = i;
                break;
            }
        }

        // Remove the order if found
        if (orderIndex != -1)
        {
            List<string> updatedLines = new List<string>(lines);
            updatedLines.RemoveAt(orderIndex);

            // Write the updated lines back to the file
            File.WriteAllLines(filePath, updatedLines);
            Console.WriteLine("Order deleted successfully.");
        }
        else
        {
            Console.WriteLine($"Order with Id {orderId} not found for MemberId {selectedMemberId}.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error deleting order: {ex.Message}");
    }
}


void DisplayMonthlyAndTotalCharges(int year)
{
    // Filter orders by year
    List<int> filteredOrdersIndices = new List<int>();
    for (int i = 0; i < id.Count; i++)
    {
        if (TimeFulfilled[i].HasValue && TimeFulfilled[i].Value.Year == year)
        {
            filteredOrdersIndices.Add(i);
        }
    }

    if (filteredOrdersIndices.Count == 0)
    {
        Console.WriteLine($"No orders fulfilled in the year {year}.");
        return;
    }

    // Initialize monthly totals
    Dictionary<int, decimal> monthlyTotals = new Dictionary<int, decimal>();
    for (int month = 1; month <= 12; month++)
    {
        monthlyTotals[month] = 0;
    }

    // Calculate monthly and total charges
    decimal totalCharges = 0;
    foreach (int index in filteredOrdersIndices)
    {
        decimal orderTotal = CalculateOrderTotal(index);
        totalCharges += orderTotal;

        // Update monthly total
        monthlyTotals[TimeFulfilled[index].Value.Month] += orderTotal;
    }

    // Display monthly breakdown
    Console.WriteLine($"Monthly Charged Amounts Breakdown for {year}:");
    for (int month = 1; month <= 12; month++)
    {
        Console.WriteLine($"{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month)} {year}: ${monthlyTotals[month]:F2}");
    }

    // Display total charged amount
    Console.WriteLine($"Total Charged Amount for {year}: ${totalCharges:F2}");
}

decimal CalculateOrderTotal(int index)
{
    // Prices
    decimal basePrice = 0;
    decimal premiumFlavourPrice = 2;
    decimal toppingPrice = 1;

    // Calculate base price based on option
    switch (Option[index].ToLower())
    {
        case "cup":
            basePrice = GetCupPrice(index);
            break;

        case "cone":
            basePrice = GetConePrice(index);
            break;

        case "waffle":
            basePrice = GetWafflePrice(index);
            break;
    }

    // Debug lines
    Console.WriteLine($"Order {index + 1} - Flavour: {Flavour1[index]}");
    Console.WriteLine($"Order {index + 1} - Scoops: {scoops[index]}");

    // Calculate additional charges for premium flavour and toppings
    decimal premiumFlavourCharge = Flavour1[index].ToLower() switch
    {
        "durian" or "ube" or "sea salt" => premiumFlavourPrice * scoops[index],
        _ => 0
    };

    decimal toppingCharge = toppingPrice * GetToppingsCount(index); // Use GetToppingsCount method

    // Debug lines
    Console.WriteLine($"Order {index + 1} - Base Price: ${basePrice:F2}");
    Console.WriteLine($"Order {index + 1} - Premium Flavour Charge: ${premiumFlavourCharge:F2}");
    Console.WriteLine($"Order {index + 1} - Topping Charge: ${toppingCharge:F2}");

    // Calculate total order price
    decimal totalOrderPrice = basePrice + premiumFlavourCharge + toppingCharge;
    Console.WriteLine($"Order {index + 1} - Total Order Price: ${totalOrderPrice:F2}");

    return totalOrderPrice;
}

decimal GetCupPrice(int index)
{
    switch (scoops[index])
    {
        case 1:
            return 4.00m;
        case 2:
            return 5.50m;
        case 3:
            return 6.50m;
        default:
            return 0;
    }
}

decimal GetConePrice(int index)
{
    switch (scoops[index])
    {
        case 1:
            return 4.00m + (Dipped[index] ? 2.00m : 0);
        case 2:
            return 5.50m + (Dipped[index] ? 2.00m : 0);
        case 3:
            return 6.50m + (Dipped[index] ? 2.00m : 0);
        default:
            return 0;
    }
}

decimal GetWafflePrice(int index)
{
    decimal basePrice = 7.00m;
    decimal waffleFlavourPrice = 3.00m;

    // Check if a premium flavour is selected and add the additional cost
    decimal premiumFlavourCharge = Flavour1[index].ToLower() switch
    {
        "durian" or "ube" or "sea salt" => 2.00m * scoops[index], // Premium flavour charge per scoop
        _ => 0
    };

    // Check if a special waffle flavour is selected and add the additional cost
    decimal waffleFlavourCharge = Flavour1[index].ToLower() switch
    {
        "red velvet" or "charcoal" or "pandan" => 3.00m,
        _ => 0
    };

    return basePrice + (scoops[index] * waffleFlavourPrice) + premiumFlavourCharge + waffleFlavourCharge;
}

// Add a method to get the count of toppings for an order
int GetToppingsCount(int index)
{
    return Topping1[index].Length + Topping2[index].Length +
           Topping3[index].Length + Topping4[index].Length;
}