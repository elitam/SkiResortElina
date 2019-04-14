using SkiResort.Business;
using SkiResort.Data.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Console = Colorful.Console;


namespace SkiResort.Views
{
    class DisplayRental
    {
        public DisplayRental()
        {
            rentalController = new RentalController();
            InputRental();
        }

        private RentalController rentalController;

        int closeOperation = 6;

        private void ShowMenuRental()
        {
            Console.WriteLine();
            Console.WriteLine("         ┌────────────────────────────────┐");
            Console.WriteLine("         │             RENTAL!            │");
            Console.WriteLine("┌────────└────────────────────────────────┘────────┐");
            Console.WriteLine("│                        MENU:                     │");
            Console.WriteLine("│                                                  │");
            Console.WriteLine("│      0. BACK                     3. LIST ITEMS   │");
            Console.WriteLine("│                                                  │");
            Console.WriteLine("│      1. ADD ITEM                 4. RENT ITEM    │");
            Console.WriteLine("│                                                  │");
            Console.WriteLine("│      2. REMOVE ITEM              5. RETURN ITEM  │");
            Console.WriteLine("│                                                  │");
            Console.WriteLine("└──────────────────────────────────────────────────┘");
            Console.WriteLine();

        }

        private void InputRental()
        {
            var operation = -1;
            do
            {
                ShowMenuRental();

                try
                {
                    operation = int.Parse(Console.ReadLine());

                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input!", Color.Salmon);
                }
                switch (operation)
                {
                    case 0:
                        Back();
                        break;
                    case 1:
                        AddItem();
                        break;
                    case 2:
                        RemoveItem();
                        break;
                    case 3:
                        ListAll();
                        break;
                    case 4:
                        RentItem();
                        break;
                    case 5:
                        ReturnItem();
                        break;
                    default:
                        break;
                }

            } while (operation != closeOperation);
        }

        private void ReturnItem()
        {
            ListRentedItems();

            var item = new Item();
            do
            {
                item = GetById();
            } while (item.Size == null);


            this.rentalController.ReturnItem(item.Id);
            Console.WriteLine("\nItem returned successfully!", Color.LightGreen);
        } // validated

        private void RentItem()
        {
            ListAll();

            var item = new Item();
            do
            {
                item = GetById();
            } while (item.Size == null);


            this.rentalController.RentItem(item.Id);
            Console.WriteLine("\nItem rented successfully!", Color.LightGreen);

        } // validated

        private void RemoveItem()
        {
            ListAll();

            var item = new Item();
            do
            {
                item = GetById();
            } while (item.Size == null);


            this.rentalController.RemoveById(item.Id);
            Console.WriteLine("\nItem removed successfully!", Color.LightGreen);

        } // validated

        private void AddItem()
        {
            Item item = new Item();

            Console.WriteLine("\nEnter item name: ");
            item.Name = Console.ReadLine();

            string input;
            decimal value;
            int i = 1;
            do
            {
                if (i > 1)
                {
                    Console.WriteLine("Invalid input!", Color.Pink);

                }
                Console.WriteLine("\nEnter item price: ");
                input = Console.ReadLine();
                i++;

            } while (!decimal.TryParse(input, out value));
            item.Price = value;

            Console.WriteLine("\nEnter item size: ");
            item.Size = Console.ReadLine();

            string gender;
            int b = 1;
            do
            {
                if (b > 1)
                {
                    Console.WriteLine("Invalid input!", Color.Pink);
                }
                Console.WriteLine("\nEnter item gender: (Female, Male, Kids)");
                gender = Console.ReadLine();
                b++;
            } while (gender.ToLower() != "female" && gender.ToLower() != "male" && gender.ToLower() != "kids");
            item.Gender = FirstCharToUpper(gender);

            this.rentalController.Add(item);
            Console.WriteLine("\nItem added successfully!", Color.LightGreen);
        } // validated

        private void ListAll()
        {
            PrintTitle("RENTAL");
            PrintTitle("FEMALE");

            ListByGenderAndType("Female", "Not Rented");
            PrintTitle("MALE");
            ListByGenderAndType("Male", "Not Rented");
            PrintTitle("KIDS");
            ListByGenderAndType("Kids", "Not Rented");
            Console.WriteLine();
        }

        private void PrintTitle(string title)
        {
            if (title == "FEMALE" || title == "RENTAL")
            {
                Console.WriteLine(new string('-', 40));
                Console.WriteLine(new string(' ', 17) + title + new string(' ', 18));
                Console.WriteLine(new string('-', 40));
            }
            else if (title == "MALE" || title == "KIDS")
            {
                Console.WriteLine(new string('-', 40));
                Console.WriteLine(new string(' ', 18) + title + new string(' ', 18));
                Console.WriteLine(new string('-', 40));
            }
            else
            {
                Console.WriteLine(new string('-', 40));
                Console.WriteLine(new string(' ', 14) + title + new string(' ', 18));
                Console.WriteLine(new string('-', 40));
            }


        }

        private void ListByGenderAndType(string gender, string status)
        {
            var items = rentalController.GetAllItems();
            foreach (var item in items.Where(it => it.Gender == gender))
            {
                if (item.Status == status)
                {
                    //Console.WriteLine($"{item.Id}: {item.Name} ({item.Size} size), costs {item.Price} lv", Color.Orange);

                    string text = "{0}: {1} ({2} size), costs {3} lv";
                    string[] data = new string[]
                    {
                     item.Id.ToString(),
                     item.Name,
                     item.Size,
                     item.Price.ToString()
                    };

                    Console.WriteLineFormatted(text, Color.Salmon, Color.White, data);
                }

            }


            
        }

        private void ListRentedItems()
        {
            PrintTitle("RENTED ITEMS");
            PrintTitle("FEMALE");

            ListByGenderAndType("Female", "Rented");
            PrintTitle("MALE");
            ListByGenderAndType("Male", "Rented");
            PrintTitle("KIDS");
            ListByGenderAndType("Kids", "Rented");
            Console.WriteLine();
        }

        private void Back()
        {
            DisplayServicesAndActivities displayServicesAndActivities = new DisplayServicesAndActivities();
        }

        private Item GetById()
        {
            Item item = new Item();
            Console.WriteLine("\nEnter ID :");

            int value;
            if (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Input must be a number!", Color.Pink);
            }
            else
            {
                int id = value;
                item = rentalController.GetItem(id);
            }


            return item;
        }

        private string FirstCharToUpper(string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }


    }
}
