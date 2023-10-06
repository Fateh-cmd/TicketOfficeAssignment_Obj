using System;

namespace TicketOffice2
{
    public enum Place
    {
        Seated,
        Standing
    }

    public class Ticket
    {
        public int Age { get; private set; }
        public Place Place { get; private set; }
        public int Number { get; private set; }

        public Ticket(int age, Place place)
        {
            Age = age;
            Place = place;
            Number = TicketNumberGenerator(); // Generate the ticket number in the constructor
        }

        public int Price()
        {
            int seatedPrice = 0;
            int standingPrice = 0;

            if (Age < 11)
            {
                seatedPrice = 50;
                standingPrice = 25;
            }
            else if (Age >= 12 && Age <= 64)
            {
                seatedPrice = 170;
                standingPrice = 110;
            }
            else if (Age > 65)
            {
                seatedPrice = 100;
                standingPrice = 60;
            }

            return Place == Place.Seated ? seatedPrice : standingPrice;
        }

        public decimal Tax()
        {
            int price = Price();
            decimal tax = (1 - 1 / 1.06m) * price;
            return Math.Round(tax, 2);
        }

        private int TicketNumberGenerator()
        {
            Random random = new Random();
            return random.Next(1, 8000);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Ticket Office App!");

            int age = GetCustomerAge();
            Place place = GetCustomerPlacePreference();

            Ticket ticket = new Ticket(age, place);

            Console.WriteLine($"Ticket Price: {ticket.Price()} SEK");
            Console.WriteLine($"Tax: {ticket.Tax()} SEK");
            Console.WriteLine($"Ticket Number: {ticket.Number}");
        }

        static int GetCustomerAge()
        {
            while (true)
            {
                Console.Write("Please enter your age: ");
                string input = Console.ReadLine();
                if (input.Length >= 1 && input.Length <= 3 && IsNumeric(input))
                {
                    return int.Parse(input);
                }
                else
                {
                    Console.WriteLine("Invalid age format or range. Please enter a valid age between 1 and 3 characters.");
                }
            }
        }

        static bool IsNumeric(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        static Place GetCustomerPlacePreference()
        {
            while (true)
            {
                Console.Write("Do you want a standing or seated ticket: ");
                string input = Console.ReadLine().Trim().ToLower();

                if (input == "s" || input == "seated")
                {
                    return Place.Seated;
                }
                else if (input == "standing")
                {
                    return Place.Standing;
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                }
            }
        }
    }
}
