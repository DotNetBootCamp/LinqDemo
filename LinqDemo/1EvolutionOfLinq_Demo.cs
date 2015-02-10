using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Reference: The Evolution Of LINQ And Its Impact On The Design Of C# http://msdn.microsoft.com/en-us/magazine/cc163400.aspx
namespace LinqDemo
{
    public partial class EvolutionOfLinq_Demo
    {
        /// <summary>
        /// Old long way
        /// </summary>
        public IEnumerable<Account> Find_Negative_Balances_Old(IEnumerable<Account> accounts)
        {
            var results = new List<Account>();

            foreach (Account account in accounts)
            {
                if (account.Balance < 0)
                {
                    results.Add(account);
                }
            }

            return results;
        }

        public IEnumerable Find_Negative_Balances_New(IEnumerable<Account> accounts)
        {
            return from account in accounts
                   where account.Balance < 0
                   select new { account.Name, account.Address };
        }

    }

    //----------------------
    // 1. Lambda Expressions
    //----------------------

    public delegate bool FilterAccountDelegate(Account account);

    public partial class EvolutionOfLinq_Demo
    {
        public static IEnumerable<Account> FindAccountsWhere(IEnumerable<Account> accounts, FilterAccountDelegate filter)
        {
            var results = new List<Account>();

            foreach (Account account in accounts)
            {
                if (filter(account))
                {
                    results.Add(account);
                }
            }

            return results;
        }

        public IEnumerable<Account> Find_Negative_Balances_Using_Delegate(IEnumerable<Account> accounts)
        {

            return FindAccountsWhere(accounts, delegate(Account account)
                                                        {
                                                            return account.Balance < 0;
                                                        });
        }

        public IEnumerable<Account> Find_Negative_Balances_Using_Lambda(IEnumerable<Account> accounts)
        {

            return FindAccountsWhere(accounts, account =>
            {
                return account.Balance < 0;
            });
        }

        public IEnumerable<Account> Find_Negative_Balances_Using_Lambda_Simple(IEnumerable<Account> accounts)
        {

            return FindAccountsWhere(accounts, account => account.Balance < 0);
        }
    }

    //---------------------
    // 2. Extension Methods
    //---------------------

    public static class AccountEnumerableExtensions
    {
        //Do you all understand static?
        public static IEnumerable<Account> AccountFilterExtension(this IEnumerable<Account> accounts, 
                                                                    FilterAccountDelegate filter)
        {
            return EvolutionOfLinq_Demo.FindAccountsWhere(accounts, filter);
        }
    }

    public partial class EvolutionOfLinq_Demo
    {

        /// <summary>
        /// Filter using a delegate and extension
        /// </summary>
        public IEnumerable<Account> Find_Negative_Balances_Anonymous_Delegate(IEnumerable<Account> accounts)
        {
            return accounts.AccountFilterExtension(delegate(Account account)
                                                       {
                                                           return account.Balance < 0;
                                                       });
        }

        /// <summary>
        /// Filter using a lambda expression
        /// </summary>
        public IEnumerable<Account> Find_Negative_Balances_Lambda(IEnumerable<Account> accounts)
        {
            return accounts.AccountFilterExtension(account =>
                                                       {
                                                           return account.Balance < 0;
                                                       });
        }

        public IEnumerable<Account> Find_Negative_Balances_Lambda_Simpler(IEnumerable<Account> accounts)
        {
            return accounts.Where(account => account.Balance < 0);
        }

    //---------------------
    // 3. Anonymous Types
    //---------------------

        public IEnumerable<string> Find_Negative_Balances_Customer_Names(IEnumerable<Account> accounts)
        {
            return accounts.Where(account => account.Balance < 0)
                .Select(account => account.Name);
        }
    }

    public partial class EvolutionOfLinq_Demo
    {

        public IEnumerable<Customer> Find_Negative_Balances_CustomerTuples(IEnumerable<Account> accounts)
        {
            return accounts.Where(account => account.Balance < 0)
                .Select(account => new Customer(account.Name, account.Address));
        }

        public IEnumerable Find_Negative_Balances_Linq_Customer_Details(IEnumerable<Account> accounts)
        {
            return accounts.Where(account => account.Balance < 0)
                .Select(account => new { account.Name, account.Address });
        }

        public IEnumerable Find_Negative_Balances_Linq_Customer_Details_Explicitly_Declared(IEnumerable<Account> accounts)
        {
            var query = accounts.Where(account => account.Balance < 0)
                .Select(account => new { FullName = account.Name, HomeAddress = account.Address });

            foreach (var customer in query)
            {
                Console.WriteLine(customer.FullName + customer.HomeAddress);
            }

            return query;
        }

        //------------------------------------
        // 4. Implicitly Typed Local Variables
        //------------------------------------

        public void Implicit_Example(IEnumerable<Account> accounts)
        {
            var integer = 1;
            //integer = "hello"; //compiler error

            var customerListLookup = new Dictionary<string, List<Customer>>(); //nifty, right?

            //Notice that I don't specify the type of account
            var query = accounts.Where(account => account.Balance < 0)
                .Select(account => new { account.Name, account.Address });
        }

        //-----------------------
        // 5. Object Initializers
        //-----------------------

        public IEnumerable<Customer> Object_Initializers_Example(IEnumerable<Account> accounts)
        {
            var customerLong = new Customer();
            customerLong.Name = "Roger";
            customerLong.Address = "1 Wilco Way";

            var customer = new Customer { Name = "Roger", Address = "1 Wilco Way" };

            var naughtyCustomers = accounts.Where(account => account.Balance < 0)
                .Select(account => new Customer { Name = account.Name, Address = account.Address });

            return naughtyCustomers;
        }

        //---------------------
        // 6. Query Expressions
        //---------------------

        public void And_Finally_____Query_Expressions(IEnumerable<Account> accounts)
        {
            var naughtyCustomers = from account in accounts
                                   where account.Balance < 0
                                   select (new Customer { Name = account.Name, Address = account.Address });
            /* or */
            naughtyCustomers = accounts.Where(account => account.Balance < 0)
                .Select(account => new Customer { Name = account.Name, Address = account.Address });
        }

    }


}
