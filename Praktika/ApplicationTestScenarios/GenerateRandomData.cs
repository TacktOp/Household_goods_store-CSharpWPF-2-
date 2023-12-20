﻿using System;
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
            var shops = new List<Shop>();
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
                "Мыло",
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
            Console.WriteLine("Generated shops");
            var addres = new List<string>()
            {
                "г. Тюмень, Метелевская, 2",
                "г. Тюмень, 70 лет Октября, 1/1",
                "г. Тюмень, Магистральная, 10г",
                "г. Тюмень, Велижанская, 70",
                "г. Тюмень, Щербакова, 146 к2",
                "г. Тюмень, Шишкова, 16",
                "г. Тюмень, Верхнетарманская, 5",
                "г. Тюмень, Ватутина, 16а к1",
                "г. Тюмень, Дружбы, 73",
                "г. Тюмень, Юганский проезд, 11/1",
                "г. Тюмень, Малышева, 31",
                "г. Тюмень, Шаимский проезд, 4а",
                "г. Тюмень, Солнечный проезд, 11",
                "г. Тюмень, Щербакова, 112",
                "г. Тюмень, Беляева, 29 к1",
                "г. Тюмень, Беляева, 21а",
                "г. Тюмень, Беляева, 17",
                "г. Тюмень, Газовиков, 20/2",
                "г. Тюмень, Заречный проезд, 37 к1",
                "г. Тюмень, Газовиков, 35",
                "г. Тюмень, Жуковского, 84 к1",
                "г. Тюмень, Полевая, 105 к2",
                "г. Тюмень, Луначарского, 9/1",
                "г. Тюмень, Садовая, 113а",
                "г. Тюмень, Ямская, 86",
                "г. Тюмень, Луначарского, 59/1",
                "г. Тюмень, Перекопская, 4а",
                "г. Тюмень, Камчатская, 34а",
                "г. Тюмень, Циолковского, 7",
                "г. Тюмень, Ямская, 93",
                "г. Тюмень, Чернышевского, 2а",
                "г. Тюмень, Белинского, 8",
                "г. Тюмень, Урицкого, 22",
                "г. Тюмень, Профсоюзная, 32",
                "г. Тюмень, Минская, 7/1",
                "г. Тюмень, Казачьи Луга, 2",
                "г. Тюмень, Мельникайте, 63",
                "г. Тюмень, Харьковская, 56",
                "г. Тюмень, Декабристов, 147",
                "г. Тюмень, Волгоградская, 117",
                "г. Тюмень, Мебельщиков, 6",
                "г. Тюмень, Интернациональная, 138",
                "г. Тюмень, Волгоградская, 67",
                "г. Тюмень, Республики, 94",
                "г. Тюмень, Минская, 69",
                "г. Тюмень, Калинина, 6",
                "г. Тюмень, Одесская, 31",
                "г. Тюмень, 50 лет ВЛКСМ, 13 к1",
                "г. Тюмень, Красных Зорь, 55",
                "г. Тюмень, Одесская, 49",
                "г. Тюмень, Московский тракт, 100",
                "г. Тюмень, Калинина, 61",
                "г. Тюмень, Республики, 146",
                "г. Тюмень, Московский тракт, 143 к5/2",
                "г. Тюмень, Невская, 112/2",
                "г. Тюмень, Тульская, 7",
                "г. Тюмень, 50 лет ВЛКСМ, 71Б",
                "г. Тюмень, Республики, 186",
                "г. Тюмень, Червишевский тракт, 15/1",
                "г. Тюмень, Республики, 181",
                "г. Тюмень, Николая Чаплина, 115/1",
                "г. Тюмень, Мельникайте, 126/3",
                "г. Тюмень, Гастелло, 78",
                "г. Тюмень, Мельникайте, 126 к2",
                "г. Тюмень, Ставропольская, 4/1",
                "г. Тюмень, Валерии Гнаровской, 7",
                "г. Тюмень, Обдорская, 5",
                "г. Тюмень, Червишевский тракт, 31",
                "г. Тюмень, Республики, 239/1",
                "г. Тюмень, Широтная, 43 к2/1",
                "г. Тюмень, 30 лет Победы, 112а",
                "г. Тюмень, Широтная, 65 ст1",
                "г. Тюмень, Червишевский тракт, 45 к6",
                "г. Тюмень, Олимпийская, 45/1",
                "г. Тюмень, Олимпийская, 10 к2",
                "г. Тюмень, Олимпийская, 6",
                "г. Тюмень, 30 лет Победы, 134а",
                "г. Тюмень, Николая Гондатти, 2/1",
                "г. Тюмень, Пермякова, 78 к4",
                "г. Тюмень, Уездная, 98",
                "г. Тюмень, Евгения Богдановича, 8/1",
                "г. Тюмень, Изумрудная, 21а",
                "г. Тюмень, Широтная, 125",
                "г. Тюмень, Василия Гольцова, 3",
                "г. Тюмень, Беловежская, 13 ст1",
                "г. Тюмень, Моторостроителей, 5",
                "г. Тюмень, Стахановцев, 1/1",
                "г. Тюмень, Станислава Карнацевича, 4",
                "г. Тюмень, Широтная, 134 к1",
                "г. Тюмень, Народная, 7",
                "г. Тюмень, Монтажников, 53",
                "г. Тюмень, Космонавтов, 8",
                "г. Тюмень, Боровская, 9/1",
                "г. Тюмень, Кленовая, 2",
                "г. Тюмень, Малая Боровская, 28",
                "г. Тюмень, Широтная, 215",
            };    
            for (var i = 0; i < addres.Count; i++)
            {
                shops.Add(new Shop
                {
                    Addres = addres[i]
                });
            }

            Console.WriteLine("Generated 10000 stashes");
            using (var context = new PDbContextData())
            {
                context.Database.EnsureCreated();
                context.Users.AddRange(users);
                context.Products.AddRange(products);
                context.Shops.AddRange(shops);
                context.SaveChanges();
            }
            Console.WriteLine("Data generated successfully");
        }
    }
}
