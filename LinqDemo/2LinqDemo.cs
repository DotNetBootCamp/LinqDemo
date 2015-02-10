using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Reference: Deferred execution sample: http://jesseliberty.com/2011/05/16/linq-deferred-executionoops/
//Reference: 101 Linq Samples: http://code.msdn.microsoft.com/101-LINQ-Samples-3fb9811b
namespace LinqDemo
{
    public class LinqDemo
    {

        public void Deferred_Execution_Sample()
        {
            int counter = 0;
            var evenNumbersInSeries =
                Enumerable.Range(0, 10).Select(
                x =>
                {
                    int result = x + counter;
                    counter++;
                    return result;
                }
                ).ToList();

            // List the numbers in the series
            Console.WriteLine("First Try:");
            foreach (int i in evenNumbersInSeries)
            {
                Console.WriteLine(i);
            }

            // List them again
            Console.WriteLine("Second Try:");
            foreach (int i in evenNumbersInSeries)
            {
                Console.WriteLine(i);
            }

        }

        public void Useful_Deferred_Execution_Example(IEnumerable<Account> accounts, int accountTypes)
        {
            var query = from account in accounts //Does NOT execute here
                        where account.Balance < 0
                        select account; 

            if (accountTypes == 1)
            {
                query = query.Where(account => account.Name.StartsWith("A"));
            }

            foreach (var account in query) //Executes here
            {
                Console.WriteLine(account.Name);
            }
        }

        public IEnumerable Samples101_Anonymous_Types()
        {
            string[] words = { "aPPLE", "BlUeBeRrY", "cHeRry" };

            var upperLowerWords =
                from w in words
                select new { Upper = w.ToUpper(), Lower = w.ToLower() };

            foreach (var ul in upperLowerWords)
            {
                Console.WriteLine("Uppercase: {0}, Lowercase: {1}", ul.Upper, ul.Lower);
            }

            return upperLowerWords;
        }

        public void Test_Anonymous_Return()
        {
            var output = Samples101_Anonymous_Types();
            //NOTE trying to access columns does not work
        }

        public void Samples101_Skip_Take()
        {
            var range = Enumerable.Range(1, 20)
                .Skip(5)
                .Take(5);

            foreach (var number in range) //6 to 10
            {
                Console.WriteLine(number);
            }
        }

        public void Samples101_ThenBy()
        {
            string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            var sortedDigits = //digits.OrderBy(d => d.Length).ThenBy(d => d);
                from d in digits
                orderby d.Length, d
                select d; 

            Console.WriteLine("Sorted digits:");
            foreach (var d in sortedDigits)
            {
                Console.WriteLine(d);
            }
        }

        public void Samples101_GroupBy()
        {
            
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var numberGroups =
                from n in numbers
                group n by n % 5 into g
                select new { Remainder = g.Key, Numbers = g };

            foreach (var g in numberGroups)
            {
                Console.WriteLine("Numbers with a remainder of {0} when divided by 5:", g.Remainder);
                foreach (var n in g.Numbers)
                {
                    Console.WriteLine(n);
                }
            }
        }
        public void AnotherGroupExample()
        {
            var customers = new List<Account>() 
            { 
                new Account { Name = "Peter", Address = "1"},
                new Account { Name = "Peter", Address = "2"},
                new Account { Name = "Peter", Address = "3"},
                new Account { Name = "Peter", Address = "4"},
                new Account { Name = "Peter", Address = "5"},
            };

            var groupedCust = customers
                .GroupBy(customer => customer.Name)
                .Select(g => g);

            foreach (var accountGroup in groupedCust)
            {
                var name = accountGroup.Key;
                foreach (var address in accountGroup)
                {

                }
            }
        }

        public void Samples101_ToDictionary()
        {
            var scoreRecords = new[]
                                   {
                                       new { Name = "Alice", Score = 50, Gender = 1, },
                                       new { Name = "Bob", Score = 40, Gender = 2, },
                                       new { Name = "Cathy", Score = 45, Gender = 1, },
                                   };

            var scoreRecordsDict = scoreRecords.ToDictionary(sr => sr.Name, sr => sr.Score);
            Console.WriteLine("Bob's score: {0}", scoreRecordsDict["Bob"]); //Bob's score: 40

            var scoreComplexRecordsDict = scoreRecords.ToDictionary(sr => sr.Name);
            Console.WriteLine("Bob's score: {0}", scoreComplexRecordsDict["Bob"]); //Bob's score: { Name = Bob, Score = 40, Gender = 2, }
          
        }

        
    }
}
