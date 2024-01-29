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



// Method for option 4 - Create a customer's order


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