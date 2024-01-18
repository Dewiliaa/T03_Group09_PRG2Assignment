using T03_Group09_PRG2Assignment;
using System;
using System.Collections.Generic;

List<Customer> customers = new List<Customer>();

void ReadCustomers(string filePath)
{
    try
    {
        using (StreamReader sr = new StreamReader(filePath))
        {
            string? s = sr.ReadLine(); // heading
            while ((s = sr.ReadLine()) != null) // for the rest of the lines
            {
                string[] data = s.Split(',');

                // data[0] -> Name, data[1] -> MemberId, 
                // data[2] -> DOB, data[3] -> MembershipStatus,
                // data[4] -> MembershipPoints, data[5] -> PunchCard

                string name = data[0];
                int memberId = int.Parse(data[1]);
                DateTime dob = DateTime.Parse(data[2]);
                string membershipStatus = data[3];
                int membershipPoints = int.Parse(data[4]);
                int punchCard = int.Parse(data[5]);

                Customer customer = new Customer(name, memberId, dob);
                customer.Rewards = new PointCard(membershipPoints, punchCard);
                customers.Add(customer);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error reading customers from file: {ex.Message}");
    }
}

ReadCustomers("customers.csv");

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
        // call method
    }

    else if (choice == "2")
    {
        ListAllCurrentOrders();
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
        // call method
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




// Method for option 2 - List all current orders, both gold members and regular queue
// Method for option 2 - List all current orders, both gold members and regular queue
void ListAllCurrentOrders()
{
    Console.WriteLine("List of all current orders:");

    // Display the header with adjusted spacing
    Console.WriteLine("{0,-5} {1,-10} {2,-20} {3,-20} {4,-10} {5,-10} {6,-10} {7,-18} {8,-12} {9,-12} {10,-12} {11,-12} {12,-12} {13,-12} {14,-12}",
        "Id", "MemberId", "TimeReceived", "TimeFulfilled", "Option", "Scoops", "Dipped", "WaffleFlavour", "Flavour1", "Flavour2", "Flavour3", "Topping1", "Topping2", "Topping3", "Topping4");

    // Read and display orders from orders.csv
    ReadOrders("orders.csv");
}

// Method to read orders from order.csv and display in the desired format
void ReadOrders(string filePath)
{
    try
    {
        using (StreamReader sr = new StreamReader(filePath))
        {
            // Skip the heading
            sr.ReadLine();

            while (true)
            {
                string line = sr.ReadLine();
                if (line == null) break;

                // Split the order details
                string[] data = line.Split(',');

                // Format and display the order details with adjusted spacing
                Console.WriteLine("{0,-5} {1,-10} {2,-20} {3,-20} {4,-10} {5,-10} {6,-10} {7,-18} {8,-12} {9,-12} {10,-12} {11,-12} {12,-12} {13,-12} {14,-12}",
                    data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7], data[8], data[9], data[10], data[11], data[12], data[13], data[14]);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error reading orders from file: {ex.Message}");
    }
}



// Method for option 3 - Register a new customer



// Method for option 4 - Create a customer's order



// Method for option 5 - Display order details of a customer
void DisplayOrderDetailsOfCustomer()
{
    Console.WriteLine("List of all customers");
}


// Method for option 6 - Modify order details