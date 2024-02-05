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
    class Topping
    {
        //Properties
        public string Type { get; set; }

        //Constructors
        public Topping() { }

        public Topping(string type)
        {
            Type = type;
        }

        //ToString Method
        public override string ToString()
        {
            return $"Topping: {Type}";
        }
    }
}
