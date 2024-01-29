namespace T03_Group09_PRG2Assignment
{
    abstract class IceCream
    {
        //Properties
        public string Option { get; set; }
        public int Scoops { get; set; }
        public List<Flavour> Flavours { get; set; }
        public List<Topping> Toppings { get; set; }

        //Constructors
        public IceCream() { }

        public IceCream(string option, int scoops, List<Flavour> flavours, List<Topping> toppings)
        {
            Option = option;
            Scoops = scoops;
            Flavours = flavours;
            Toppings = toppings;
        }

        //Methods
        public virtual double CalculatePrice()
        {
            double basePrice = 0;

            //Price based on ice cream option and scoops
            switch (Option.ToLower())
            {
                case "cup":
                    basePrice = 4.00 + (Scoops - 1) * 1.50; //Extra $1.50 for each extra scoop
                    break;
                case "cone":
                    basePrice = 4.00 + (Scoops - 1) * 1.50;
                    break;
                case "waffle":
                    basePrice = 7.00 + (Scoops - 1) * 1.50;
                    break;
                default:
                    Console.WriteLine("Invalid ice cream option");
                    break;
            }

            //Add extra cost for premium flav
            foreach (var flavour in Flavours)
            {
                if (flavour.Premium)
                {
                    basePrice += 2.00;
                }
            }

            //Add extra cost for toppings
            basePrice += Toppings.Count; //$1 for each topping
            return basePrice;
        }

        public override string ToString()
        {
            return $"Ice Cream: {Option} - Scoops: {Scoops} - Flavours: {string.Join(", ", Flavours)} - Toppings: {string.Join(", ", Toppings)}";
        }
    }

    class VanillaIceCream : IceCream
    {
        public VanillaIceCream(string option, int scoops, List<Flavour> flavours, List<Topping> toppings)
            : base(option, scoops, flavours, toppings)
        {

        }
    }

    class ChocolateIceCream : IceCream
    {
        public ChocolateIceCream(string option, int scoops, List<Flavour> flavours, List<Topping> toppings)
            : base(option, scoops, flavours, toppings)
        {

        }
    }

    class StrawberryIceCream : IceCream
    {
        public StrawberryIceCream(string option, int scoops, List<Flavour> flavours, List<Topping> toppings)
            : base(option, scoops, flavours, toppings)
        {

        }
    }
    class DurianIceCream : IceCream
    {
        public DurianIceCream(string option, int scoops, List<Flavour> flavours, List<Topping> toppings)
            : base(option, scoops, flavours, toppings)
        {

        }
    }

    class UbeIceCream : IceCream
    {
        public UbeIceCream(string option, int scoops, List<Flavour> flavours, List<Topping> toppings)
            : base(option, scoops, flavours, toppings)
        {

        }
    }

    class SeasaltIceCream : IceCream
    {
        public SeasaltIceCream(string option, int scoops, List<Flavour> flavours, List<Topping> toppings)
            : base(option, scoops, flavours, toppings)
        {

        }
    }
}