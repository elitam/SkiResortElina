using SkiResort.Business;
using SkiResort.Data.Models;
using SkiResort.Views.Hikes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Console = Colorful.Console;


namespace SkiResort.Views
{
    public class DisplayHikes
    {

        public DisplayHikes()
        {
            hikeController = new HikeController();
            rateController = new RateController();
            InputHikes();
        }

        private HikeController hikeController;
        private RateController rateController;

        private void ShowMenuHikes()
        {
            Console.WriteLine();
            Console.WriteLine("         ┌────────────────────────────────┐");
            Console.WriteLine("         │            HIKES!              │");
            Console.WriteLine("┌────────└────────────────────────────────┘────────┐");
            Console.WriteLine("│                     MENU:                        │   ");
            Console.WriteLine("│                                                  │   ");
            Console.WriteLine("│      0. BACK                 3. LIST ALL         │   ");
            Console.WriteLine("│                                                  │   ");
            Console.WriteLine("│      1. ADD                  4. UPDATE           │   ");
            Console.WriteLine("│                                                  │   ");
            Console.WriteLine("│      2. REMOVE               5. RATE             │   ");
            Console.WriteLine("│                                                  │   ");
            Console.WriteLine("└──────────────────────────────────────────────────┘");
            Console.WriteLine();

        }

        private void InputHikes()
        {
            var operation = -1;
            do
            {
                ShowMenuHikes();

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
                        Exit();
                        break;
                    case 1:
                        Add();
                        break;
                    case 2:
                        Remove();
                        break;
                    case 3:
                        ListAll();
                        break;
                    case 4:
                        Update();
                        break;
                    case 5:
                        Rate();
                        break;



                    default:
                        break;
                }

            } while (true);
        }

        private void Exit()
        {
            DisplayServicesAndActivities display = new DisplayServicesAndActivities();

        }

        private void Remove()
        {
            this.ListAll();

            var hike = new Hike();
            do
            {
                Console.WriteLine("\nEnter hike id to remove: ");
                hike = GetHike();
            } while (hike.StartPoint == null);

            hikeController.Delete(hike.Id);
            Console.WriteLine("\nHike removed successfully!", Color.LightGreen);


        } //validated

        private void Update()
        {
            this.ListAll();
            DisplayUpdate displayUpdate = new DisplayUpdate();
        }  //validated

        private void Rate()
        {
            this.ListAll();
            Rate rate = new Rate();

            var hike = new Hike();
            do
            {
                Console.WriteLine("\nEnter hike id to rate: ");
                hike = GetHike();
            } while (hike.StartPoint == null);
            rate.HikeId = hike.Id;


            string input;
            int value;
            int i = 1;
            do
            {
                if (i > 1)
                {
                    Console.WriteLine("Invalid input!", Color.Pink);

                }
                Console.WriteLine("\nEnter stars: (1 ... 5)");
                input = Console.ReadLine();
                i++;

            } while (!int.TryParse(input, out value) || value < 1 || value > 5);

            rate.Stars = value;


            this.rateController.AddRate(rate);
            Console.WriteLine("\nRate added successfully!", Color.LightGreen);
        } //validated

        private void Add()
        {
            Hike hike = new Hike();

            Console.WriteLine("\nEnter start point: ");
            hike.StartPoint = Console.ReadLine();

            Console.WriteLine("\nEnter end point: ");
            hike.EndPoint = Console.ReadLine();

            AddStartDate(hike);

            AddAverageDuration(hike);

            this.hikeController.Add(hike);
            Console.WriteLine("\nHike added successfully!", Color.LightGreen);
        } //validated

        private static void AddStartDate(Hike hike)
        {
            var input = "";
            DateTime dDate;
            int i = 1;
            do
            {
                if (i > 1)
                {
                    Console.WriteLine("Invalid input!", Color.Pink);

                }
                Console.WriteLine("\nEnter start date: (dd-mm-yy)");
                input = Console.ReadLine();
                i++;

            } while (!DateTime.TryParse(input, out dDate));
            String.Format("{0:d/MM/yyyy}", dDate);
            hike.StartDate = dDate;
        }

        private static void AddAverageDuration(Hike hike)
        {
            string input;
            decimal value;
            int i = 1;
            do
            {
                if (i > 1)
                {
                    Console.WriteLine("Invalid input!", Color.Pink);

                }
                Console.WriteLine("\nEnter average duration in hours: ");
                input = Console.ReadLine();
                i++;

            } while (!decimal.TryParse(input, out value));

            hike.AverageDuration = value;
        }

        private Hike GetHike()
        {
            Hike hike = new Hike();
            int value;
            if (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Input must be a number!", Color.Pink);
            }
            else
            {
                int id = value;
                hike = hikeController.Get(id);
            }



            return hike;
        }

        public void ListAll()
        {

            Console.WriteLine(new string('-', 40));
            Console.WriteLine(new string(' ', 18) + "HIKES" + new string(' ', 18));
            Console.WriteLine(new string('-', 40));

            var hikes = this.hikeController.GetAll();

            foreach (var hike in hikes)
            {
                var rating = rateController.CalculateRateForHike(hike);
                //Console.WriteLine($"{hike.Id} From {hike.StartPoint} to {hike.EndPoint} - {hike.AverageDuration:f0} hours on {hike.StartDate.ToString("dd/MM/yyyy")}" +
                    //$" with rate {rating} starts ");

                string text = "{0}. From {1} to {2} on {4}, will last for {3} hours and is rated with {5} starts ";
                string[] data = new string[]
                {
                     hike.Id.ToString(),
                     hike.StartPoint,
                     hike.EndPoint,
                     hike.AverageDuration.ToString(),
                     hike.StartDate.ToString("dd/MM/yyyy"),
                     rating.ToString()
                };

                Console.WriteLineFormatted(text, Color.Salmon, Color.White, data);
            }
        }


    }
}