using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T03_Group09_PRG2Assignment
{
    class Order
    {
        private static int nextOrderId = 1;  // Static variable to generate unique IDs

        public int Id { get; private set; }
        public DateTime TimeReceived { get; private set; }
        public DateTime? TimeFulfilled { get; set; }  
        public List<IceCream> IceCreamList { get; private set; }

        // Default constructor
        public Order()
        {
            Id = nextOrderId++;
            TimeReceived = DateTime.Now;
            IceCreamList = new List<IceCream>();
        }

        // Parameterized constructor 
        public Order(int id, DateTime timeReceived)
        {
            Id = id;
            TimeReceived = timeReceived;
            IceCreamList = new List<IceCream>();
        }

        // Modifies an existing ice cream in the order
        public void ModifyIceCream(int index, IceCream newIceCream)
        {
            if (index >= 0 && index < IceCreamList.Count)
            {
                IceCreamList[index] = newIceCream;
            }
        }

        // Adds a new ice cream to the order
        public void AddIceCream(IceCream iceCream)
        {
            IceCreamList.Add(iceCream);
        }

        // Deletes an ice cream from the order
        public void DeleteIceCream(int index)
        {
            if (index >= 0 && index < IceCreamList.Count)
            {
                IceCreamList.RemoveAt(index);
            }
        }

        // Calculates the total cost of the order (using IceCream's calculated prices)
        public double CalculateTotal()
        {
            double total = 0;
            foreach (IceCream iceCream in IceCreamList)
            {
                total += iceCream.CalculatePrice();  
            }
            return total;
        }

        
        public override string ToString()
        {
            return $"Order ID: {Id}, Time Received: {TimeReceived}, Time Fulfilled: {TimeFulfilled}, Total: {CalculateTotal()}";
        }
    }
}
