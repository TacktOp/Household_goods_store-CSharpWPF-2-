using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ApplicationScenarios;

namespace Praktika.ApplicationTestScenarios
{
    internal class GenerateRandomData
    {
        public static void Run()
        {
            Console.WriteLine("Generating random data...");
            var random = new Random();
            var users = new List<User>();
            var products = new List<Product>();
            var stashes = new List<Stash>();
            //var transactionHistory = new List<TransactionHistory>();
            Console.WriteLine("Generating users...");
            var appLogin = new AppLogin();
            appLogin.CreateUser(new User
            {
                Login = "admin",
                Password = "admin"
            }, true);
            for (var i = 0; i < 100; i++)
            {
                appLogin.CreateUser(new User
                {
                    Login = $"user{i}",
                    Password = $"password{i}"
                }, true);
            }
            Console.WriteLine($"Generated 100 users");
            Console.WriteLine("Generating products...");
            var product = new List<string>()
            {
                "Soup",
                "Освежители воздуха",
                "Стиральные порошки",
                "Чистящие жидкости",
                "Чистящие порошки",
                "Полотенца бумажные",
                "Салфетки и скатерти",
                "Средства личной гигиены",
                "Туалетная бумага",
                "Урны и корзины",
                "Губки и салфетки",
                "Для мытья стекол",
                "Для уборки",
                "Хоз.ткани и полотенца",
                "Упаковочные материалы",
                "Пакеты",
                "Скотч упаковочный",
                "Клей хозяйственный"
            };
            for (var i = 0; i < product.Count; i++)
            {
                products.Add(new Product
                {
                    Name = product[i],
                    Price = random.Next(500, 8000)
                });
            }
            Console.WriteLine("Generated products");
            // Generate 1 transaction with 0 moneyspent
            /*transactionHistory.Add(new TransactionHistory
            {
                SpendMoney = 0,
                BalanceAfterTransaction = 100000,
                TimeOfTransaction = new DateTime(2021, 1, 1)
            });*/
            Console.WriteLine("Generating stashes...");
            for (var m = 1; m < 13; m++)
            {
                for (var d = 1; d < 29; d++)
                {
                    var Date = new DateTime(2021, m, d);
                    var balance = (double)random.Next(100, 100000);
                    var quota = (double)random.Next(200000, 500000);
                    var amountOfStashes = random.Next(1, 100);
                    var sumSpent = 0.0;
                    for (var j = 0; j < amountOfStashes; j++)
                    {
                        var productId = random.Next(1, 18);
                        var stash = new Stash
                        {
                            ProductId = productId,
                            Product = products[productId],
                            TimeLastCheck = Date,
                            BuyPrice = random.Next(5, 10000)
                        };
                        sumSpent += (double)stash.BuyPrice;
                        stashes.Add(stash);
                    }
                    // Calc balance if there are any transactions
                    /*if (transactionHistory.Count > 0)
                    {
                        balance = (int)transactionHistory.Last().BalanceAfterTransaction;
                    }
                    var transaction = new TransactionHistory
                    {
                        SpendMoney = sumSpent-quota,
                        BalanceAfterTransaction = balance - sumSpent + quota,
                        TimeOfTransaction = Date
                    };
                    transactionHistory.Add(transaction);*/
                }
            }

            Console.WriteLine("Generated 10000 stashes");
            using (var context = new PDbContextData())
            {
                context.Database.EnsureCreated();
                context.Users.AddRange(users);
                context.Products.AddRange(products);
                context.Stashes.AddRange(stashes);
                //context.TransactionHistories.AddRange(transactionHistory);
                context.SaveChanges();
            }
            Console.WriteLine("Data generated successfully");
        }
    }
}
