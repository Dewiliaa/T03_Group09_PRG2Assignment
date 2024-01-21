using T03_Group09_PRG2Assignment;
using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;

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
            // call method
        }

        else if (choice == "4")
        {
            // call method
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

                // Display customer information
                Console.WriteLine($"Name: {fields[0]}, MemberID: {fields[1]}, DOB: {fields[2]}");
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
        Console.WriteLine("{0,-5} {1,-10} {2,-25} {3,-25} {4,-12} {5,-12} {6,-12} {7,-20} {8,-14} {9,-14} {10,-14} {11,-14} {12,-14} {13,-14} {14,-14}",
            id[i], MemberId[i], TimeReceived[i], TimeFulfilled[i], Option[i], scoops[i], Dipped[i], WaffleFlavour[i],
            Flavour1[i], Flavour2[i], Flavour3[i], Topping1[i], Topping2[i], Topping3[i], Topping4[i]);
    }
}


// Method for option 3 - Register a new customer



// Method for option 4 - Create a customer's order



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