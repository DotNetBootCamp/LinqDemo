using System;
using System.Linq;

namespace LinqDemo
{
    class LinqQuestions
    {
        public void Bigger_Is_Better()
        {
            var random = new Random();
            var numbers = Enumerable.Range(1, 10).Select(i => random.Next(100));

            //Find the largest number
        }

        public void Boxing()
        {
            var random = new Random();
            var numbers = Enumerable.Range(1, 10).Select(i => random.Next(100));

            //Use Linq to make the list of numbers of type double
        }

        public void Divisiblity()
        {
            var random = new Random();
            var numbers = Enumerable.Range(1, 12).Select(i => random.Next(100));

            //Use Linq to find out if every number is divisible by 2
        }

        public void Product_Categories()
        {
            var categories = new[]
                                 {
                                     new {Name = "Vegetables", ID = 003},
                                     new {Name = "Beverages", ID = 001},
                                     new {Name = "Condiments", ID = 002},
                                 };

            var products = new[]
                               {
                                   new {Name = "Tea", CategoryID = 001},
                                   new {Name = "Mustard", CategoryID = 002},
                                   new {Name = "Pickles", CategoryID = 002},
                                   new {Name = "Carrots", CategoryID = 003},
                                   new {Name = "Tomato", CategoryID = 003},
                                   new {Name = "Peaches", CategoryID = 005},
                                   new {Name = "Melons", CategoryID = 005},
                                   new {Name = "Ice Cream", CategoryID = 007},
                                   new {Name = "Mackerel", CategoryID = 012},
                               };

            //Find all the products which have an existing category using a Linq query
        }

        public void Flatten()
        {
            var categories = new[]
                                 {
                                     new {Name = "Beverages", Products = new [] { "Tea", "Coffee", "Beer"},},
                                     new {Name = "Condiments", Products =  new []{ "Mustard", "Pickles",}},
                                     new {Name = "Vegetables", Products = new []{ "Carrots", "Tomato",}},
                                 };

            //Enumerate all the products using a single Linq query
        }
        
    }
}
