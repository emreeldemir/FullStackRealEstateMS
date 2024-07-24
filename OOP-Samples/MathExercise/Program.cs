using MathExercise;
using System;
using System.Collections.Generic;

namespace MathExercise
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please select the action you want to take:");
                Console.WriteLine("1. Prime Number");
                Console.WriteLine("2. Perfect Number");
                Console.WriteLine("3. Exit");

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
                        Console.WriteLine("Invalid choice. Try again please!");
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
                Console.WriteLine("1. Check a number");
                Console.WriteLine("2. Check at interval");
                Console.WriteLine("3. Find first 'x' number");
                Console.WriteLine("4. Return to homepage");

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
                        Console.WriteLine("Invalid choice. Try again please!");
                        break;
                }
            }
        }

        static void CheckSpecialNumber(INumberOperations operations)
        {
            Console.Clear();
            Console.Write("Enter the number: ");
            if (int.TryParse(Console.ReadLine(), out int number))
            {
                if (operations.IsSpecialNumber(number))
                    Console.WriteLine($"{number} is a special number.");
                else
                    Console.WriteLine($"{number} is not a special number.");
            }
            else
            {
                Console.WriteLine("Enter a valid number!");
            }
            Console.ReadLine();
        }

        static void FindInRange(INumberOperations operations)
        {
            Console.Clear();
            Console.Write("Enter first value: ");
            int start = int.Parse(Console.ReadLine());
            Console.Write("Enter last value: ");
            int end = int.Parse(Console.ReadLine());

            var numbers = operations.FindInRange(start, end);

            Console.WriteLine("Special numbers in interval:");
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }
            Console.ReadLine();
        }

        static void FindFirstXNumbers(INumberOperations operations)
        {
            Console.Clear();
            Console.Write("How many special number do you want to find?: ");
            int count = int.Parse(Console.ReadLine());

            var numbers = operations.FindFirstXNumbers(count);

            Console.WriteLine($"First {count} special numbers:");
            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }
            Console.ReadLine();
        }
    }
}
