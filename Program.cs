using T03_Group09_PRG2Assignment;
using System;
using System.Collections.Generic;

// List to store objects
List<Customer> customers = new List<Customer>();
Queue<Order> goldMembersQueue = new Queue<Order>();
Queue<Order> regularQueue = new Queue<Order>();

// Read csvs
static void Main()
{
    ReadCustomers("customer.csv");
    ReadOrders("orders.csv");
    ReadOptions("options.csv");
    ReadFlavours("flavours.csv");
    ReadToppings("toppings.csv");
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

// Main method
while  (true)
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
void ListAllCurrentOrders()
{
    Console.WriteLine("List of all current orders: ");

    // Display gold members' order
    Console.WriteLine("Gold Members Queue");
    foreach (var customer in customers)
    {
        if (customer.Rewards.Tier == "Gold" && customer.CurrentOrder != null)
        {
            Console.WriteLine($"Customer: {customer.Name}");
            Console.WriteLine(customer.CurrentOrder.ToString());
        }
    }

    // Display regular orders
    Console.WriteLine("Regular queue: ");
    foreach (var customer in customers)
    {
        if (customer.Rewards.Tier != "Ordinary" && customer.CurrentOrder != null)
        {
            Console.WriteLine($"Customer: {customer.Name}");
            Console.WriteLine(customer.CurrentOrder.ToString());
        }
    }
}


// Method for option 3 - Register a new customer



// Method for option 4 - Create a customer's order



// Method for option 5 - Display order details of a customer



// Method for option 6 - Modify order details