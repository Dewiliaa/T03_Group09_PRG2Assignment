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
    class Cup : IceCream
    {
        //Constructors
        public Cup() : base() { }

        public Cup(string option, int scoops, List<Flavour> flavours, List<Topping> toppings)
            : base(option, scoops, flavours, toppings) { }

        //Method on CalculatePrice for Cup
        public override double CalculatePrice()
        {
            double basePrice = base.CalculatePrice();
            return basePrice;
        }

        //ToString
        public override string ToString()
        {
            return $"Cup - {base.ToString()}";
        }
    }
}
