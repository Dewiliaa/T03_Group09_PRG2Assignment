using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T03_Group09_PRG2Assignment
{
    class Flavour
    {
        //Properties
        public string Type { get; set; }
        public bool Premium { get; set; }
        public int Quantity { get; set; }


        //Constructors
        public Flavour() { }

        public Flavour(string type, bool premium, int quantity)
        {
            Type = type;
            Premium = premium;
            Quantity = quantity;
        }

        //ToString Method
        public override string ToString()
        {
            return $"Flavour: {Type} - Premium: {Premium} - Quantity: {Quantity}";
        }
    }
}

