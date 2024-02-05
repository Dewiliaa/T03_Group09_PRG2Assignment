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
    class Waffle : IceCream
    {
        //Properties
        public string WaffleFlavour { get; set; }

        //Constructors
        public Waffle() : base() { }

        public Waffle(string option, int scoops, List<Flavour> flavours, List<Topping> toppings, string waffleFlavour)
            : base(option, scoops, flavours, toppings)
        {
            WaffleFlavour = waffleFlavour;
        }

        //CalculatePrice for Waffle
        public override double CalculatePrice()
        {
            double basePrice = base.CalculatePrice();

            basePrice += 3.00; //Extra $3 for special waffle flav

            return basePrice;
        }

        //ToString
        public override string ToString()
        {
            return $"Waffle - Flavour: {WaffleFlavour} - {base.ToString()}";
        }
    }
}
