using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ
{
    class Program
    {
        #region Class Definitions
        public class Customer
        {
            public string First { get; set; }
            public string Last { get; set; }
            public string State { get; set; }
            public double Price { get; set; }
            public string[] Purchases { get; set; }
        }

        public class Distributor
        {
            public string Name { get; set; }
            public string State { get; set; }
        }

        public class CustDist
        {
            public string custName { get; set; }
            public string distName { get; set; }
        }

        public class CustDistGroup
        {
            public string custName { get; set; }
            public IEnumerable<string> distName { get; set; }
        }
        #endregion

        #region Create data sources

        static List<Customer> customers = new List<Customer>
        {
            new Customer {First = "Cailin", Last = "Alford", State = "GA", Price = 930.00, Purchases = new string[] {"Panel 625", "Panel 200"}},
            new Customer {First = "Theodore", Last = "Brock", State = "AR", Price = 2100.00, Purchases = new string[] {"12V Li"}},
            new Customer {First = "Jerry", Last = "Gill", State = "MI", Price = 585.80, Purchases = new string[] {"Bulb 23W", "Panel 625"}},
            new Customer {First = "Owens", Last = "Howell", State = "GA", Price = 512.00, Purchases = new string[] {"Panel 200", "Panel 180"}},
            new Customer {First = "Adena", Last = "Jenkins", State = "OR", Price = 2267.80, Purchases = new string[] {"Bulb 23W", "12V Li", "Panel 180"}},
            new Customer {First = "Medge", Last = "Ratliff", State = "GA", Price = 1034.00, Purchases = new string[] {"Panel 625"}},
            new Customer {First = "Sydney", Last = "Bartlett", State = "OR", Price = 2105.00, Purchases = new string[] {"12V Li", "AA NiMH"}},
            new Customer {First = "Malik", Last = "Faulkner", State = "MI", Price = 167.80, Purchases = new string[] {"Bulb 23W", "Panel 180"}},
            new Customer {First = "Serena", Last = "Malone", State = "GA", Price = 512.00, Purchases = new string[] {"Panel 180", "Panel 200"}},
            new Customer {First = "Hadley", Last = "Sosa", State = "OR", Price = 590.20, Purchases = new string[] {"Panel 625", "Bulb 23W", "Bulb 9W"}},
            new Customer {First = "Nash", Last = "Vasquez", State = "OR", Price = 10.20, Purchases = new string[] {"Bulb 23W", "Bulb 9W"}},
            new Customer {First = "Joshua", Last = "Delaney", State = "WA", Price = 350.00, Purchases = new string[] {"Panel 200"}}
        };

        static List<Distributor> distributors = new List<Distributor>
        {
            new Distributor {Name = "Edgepulse", State = "UT"},
            new Distributor {Name = "Jabbersphere", State = "GA"},
            new Distributor {Name = "Quaxo", State = "FL"},
            new Distributor {Name = "Yakijo", State = "OR"},
            new Distributor {Name = "Scaboo", State = "GA"},
            new Distributor {Name = "Innojam", State = "WA"},
            new Distributor {Name = "Edgetag", State = "WA"},
            new Distributor {Name = "Leexo", State = "HI"},
            new Distributor {Name = "Abata", State = "OR"},
            new Distributor {Name = "Vidoo", State = "TX"}
        };

        static double[] exchange = { 0.89, 0.65, 120.29 };
        #endregion

        static void Main(string[] args)
        {
            // LINQ select
            Console.WriteLine($" ============== LINQ select======================= ");

            var inEuroQuery = from c in customers
                              select new { Name = c.Last, Price = c.Price * 0.89 };

            foreach (var cust in inEuroQuery)
            {
                Console.WriteLine($" Name = {cust.Name}, Price = {cust.Price}");
            }


            var purchasesQuery = from c in customers
                                 from p in c.Purchases
                                 select p;

            foreach (var value in purchasesQuery)
            {
                Console.WriteLine($" {value}");
            }

            // LINQ where
            Console.WriteLine($" ============== LINQ where======================= ");

            var where = from c in customers where c.State == "OR" select c.Last;
            foreach (var cust in where)
            {
                Console.WriteLine($" people with last name where state = OR : {cust}");
            }

            //order by
            var orderBy = from c in customers orderby c.State descending select c;

            foreach (var o in orderBy)
            {
                Console.WriteLine($" customers with state order by state {o.Last} - {o.State}");
            }


            // group by
            IEnumerable<IGrouping<string, Customer>> groupByState = from c in customers group c by c.State;

            foreach (IGrouping<string, Customer> state in groupByState)
            {
                Console.WriteLine($" { state.Key}");
                foreach (Customer customer in state)
                {
                    Console.WriteLine($"   {customer.Last} {customer.First}");
                }
            }

            IEnumerable<IGrouping<bool, Customer>> groupByPriceOver1000 = from c in customers group c by c.Price > 1000;

            foreach (IGrouping<bool, Customer> value in groupByPriceOver1000)
            {
                Console.WriteLine($" {value.Key} ");
                foreach (Customer customer in value)
                {
                    Console.WriteLine($"   {customer.Last}-{customer.First} : {customer.Price}");
                }
            }

            //join
            var joinQuery = from c in customers
                            join d in distributors on c.State equals d.State
                            select new { CustName = c.First, Distributor = d.Name };

            foreach (var value in joinQuery)
            {
                Console.WriteLine($" Customer: {value.CustName} -  Distributor :{value.Distributor}");
            }

            //Group Join
            var groupJoinQuery = from c in customers
                                 join d in distributors on c.State equals d.State into matches
                                 select new { CustName = c.First, Distributor = matches.Select(x => x.Name) };

            foreach (var value in groupJoinQuery)
            {
                Console.WriteLine($" {value.CustName}");
                foreach (var o in value.Distributor)
                {
                    Console.WriteLine($"   Distributor :{o}");
                }
            }



            Console.ReadKey();
        }
    }
}
