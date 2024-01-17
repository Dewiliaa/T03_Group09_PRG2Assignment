using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T03_Group09_PRG2Assignment
{
    class PointCard
    {
        public int Points { get; set; }
        public int PunchCard { get; set; }
        public string Tier { get; set; }

        // Default constructor
        public PointCard()
        {
            Points = 0;
            PunchCard = 0;
            Tier = "Ordinary";
        }

        // Parameterized constructor
        public PointCard(int points, int punchCard)
        {
            Points = points;
            PunchCard = punchCard;
            MemberTier();
        }

        // Method to add points to the point card
        public void AddPoints(int amount)
        {
            Points += amount;
            MemberTier();
        }

        // Method to redeem points from the point card
        public bool RedeemPoints(int amount)
        {
            if (Points >= amount)
            {
                Points -= amount;
                MemberTier();
                return true;
            }
            return false;
        }

        // Method to punch the punch card
        public void Punch()
        {
            PunchCard++;
            if (PunchCard == 10)
            {
                PunchCard = 0;
            }
        }

        // Method to set the membership tier based on points
        private void MemberTier()
        {
            if (Points >= 100)
            {
                Tier = "Gold";
            }
            else if (Points >= 50)
            {
                Tier = "Silver";
            }
            else
            {
                Tier = "Ordinary";
            }
        }

        // Override ToString method for better representation
        public override string ToString()
        {
            return $"Points: {Points}, Punch Card: {PunchCard}, Tier: {Tier}";
        }
    }

}
