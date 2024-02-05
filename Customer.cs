//==========================================
// Student Number : S10262440
// Student Name   : Dewi Lia Virnanda
// Part Name      : Lavaniya D/O Rajamoorthi
//==========================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T03_Group09_PRG2Assignment
{
    class Customer
    {
        public string Name { get; set; }
        public int MemberId { get;  set; }
        public DateTime DOB { get; set; }
        public Order CurrentOrder { get; set; }
        public List<Order> OrderHistory { get; set; } = new List<Order>();
        public PointCard Rewards { get; set; }

        public Customer()
        {
        }

        public Customer(string name, int memberId, DateTime dob)
        {
            Name = name;
            MemberId = memberId;
            DOB = dob;
            Rewards = new PointCard();
        }
            
        public Order MakeOrder()
        {
            CurrentOrder = new Order();
            return CurrentOrder;
        }

        public bool IsBirthday()
        {
            return DOB.Month == DateTime.Today.Month && DOB.Day == DateTime.Today.Day;
        }

        public override string ToString()
        {
            return $"Customer: {Name}, Member ID: {MemberId}, Date of Birth: {DOB.ToString("d")}, Rewards: {Rewards}";
        }
    }

}
