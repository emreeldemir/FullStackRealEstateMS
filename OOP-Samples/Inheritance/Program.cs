using Inheritance;
using System;
using System.Collections.Generic;

namespace Inheritance
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Lütfen yapmak istediğiniz işlemi seçin:");
                Console.WriteLine("1. Prime Number");
                Console.WriteLine("2. Perfect Number");
                Console.WriteLine("3. Çıkış");

                var choice = Console.ReadLine();

                INumberOperations operations = null;

                switch (choice)
                {
                    case "1":
                        operations = new PrimeNumberOperations();
                        break;
                    case "2":
                        operations = new PerfectNumberOperations();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Geçersiz seçim. Tekrar deneyin.");
                        break;
                }

                if (operations != null)
                {
                    ShowSubMenu(operations);
                }
            }
        }

        static void ShowSubMenu(INumberOperations operations)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Sayı sorgula");
                Console.WriteLine("2. Aralık belirt");
                Console.WriteLine("3. İlk 'x' tane sayıyı bul");
                Console.WriteLine("4. Ana menüye dön");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CheckSpecialNumber(operations);
                        break;
                    case "2":
                        FindInRange(operations);
                        break;
                    case "3":
                        FindFirstXNumbers(operations);
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim. Tekrar deneyin.");
                        break;
                }
            }
        }

        static void CheckSpecialNumber(INumberOperations operations)
        {
            Console.Clear();
            Console.Write("Sorgulamak istediğiniz sayıyı girin: ");
            if (int.TryParse(Console.ReadLine(), out int number))
            {
                if (operations.IsSpecialNumber(number))
                    Console.WriteLine($"{number} özel bir sayıdır.");
                else
                    Console.WriteLine($"{number} özel bir sayı değildir.");
            }
            else
            {
                Console.WriteLine("Geçerli bir sayı girin.");
            }
            Console.ReadLine();
        }

        static void FindInRange(INumberOperations operations)
        {
            Console.Clear();
            Console.Write("Başlangıç değeri girin: ");
            int start = int.Parse(Console.ReadLine());
            Console.Write("Bitiş değeri girin: ");
            int end = int.Parse(Console.ReadLine());

            var numbers = operations.FindInRange(start, end);

            Console.WriteLine("Aralıktaki özel sayılar:");
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }
            Console.ReadLine();
        }

        static void FindFirstXNumbers(INumberOperations operations)
        {
            Console.Clear();
            Console.Write("Kaç tane özel sayı bulmak istiyorsunuz?: ");
            int count = int.Parse(Console.ReadLine());

            var numbers = operations.FindFirstXNumbers(count);

            Console.WriteLine($"İlk {count} özel sayılar:");
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }
            Console.ReadLine();
        }
    }
}
