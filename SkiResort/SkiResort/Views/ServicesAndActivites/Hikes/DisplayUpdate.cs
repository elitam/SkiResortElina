using SkiResort.Business;
using SkiResort.Data.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Console = Colorful.Console;


namespace SkiResort.Views.Hikes
{
    public class DisplayUpdate
    {
        public DisplayUpdate()
        {
            hikeController = new HikeController();
            InputUpdate();
        }



        private HikeController hikeController;
        private RateController rateController = new RateController();

        private void ShowMenuUpdate()
        {
            Console.WriteLine();
            Console.WriteLine("         ┌────────────────────────────────┐");
            Console.WriteLine("         │             UPDATE!            │");
            Console.WriteLine("┌────────└────────────────────────────────┘────────┐");
            Console.WriteLine("│                       MENU:                      │");
            Console.WriteLine("│                                                  │");
            Console.WriteLine("│    0.BACK                  3. UPDATE AVERAGE     │");
            Console.WriteLine("│                                  DURATION        │");
            Console.WriteLine("│    1. UPDATE START POINT   4. UPDATE START DATE  │");
            Console.WriteLine("│                                                  │");
            Console.WriteLine("│    2. UPDATE END POINT     5. UPDATE ALL         │");
            Console.WriteLine("│                                                  │");
            Console.WriteLine("└──────────────────────────────────────────────────┘");
            Console.WriteLine();

        }

        private void InputUpdate()
        {

            var operation = -1;
            do
            {
                ShowMenuUpdate();

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
                        UpdateStartPoint();
                        break;
                    case 2:
                        UpdateEndPoint();
                        break;
                    case 3:
                        UpdateAverageDuration();
                        break;
                    case 4:
                        UpdateStartDate();
                        break;
                    case 5:
                        UpdateAll();
                        break;

                    default:
                        break;
                }

            } while (true);
        }

        private void UpdateAll()
        {
            ListAll();
            var hike = new Hike();
            do
            {
                hike = GetHike();
            } while (hike.StartPoint == null);


            Console.WriteLine("\nEnter start point: ");
            hike.StartPoint = Console.ReadLine();

            Console.WriteLine("\nEnter end point: ");
            hike.EndPoint = Console.ReadLine();

            AddStartDate(hike);
            AddAverageDuration(hike);

            hikeController.Update(hike);
            Console.WriteLine("\nHike updated successfully!", Color.LightGreen);



        }

        private static void AddAverageDuration(Hike hike)
        {
            

            string text;
            decimal value;
            int l = 1;
            do
            {
                if (l > 1)
                {
                    Console.WriteLine("Invalid input!", Color.Pink);
                }
                Console.WriteLine("\nEnter average duration in hours: ");
                text = Console.ReadLine();
                l++;

            } while (!decimal.TryParse(text, out value));

            hike.AverageDuration = value;
        }

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

        private void UpdateAverageDuration()
        {
            ListAll();

            var hike = new Hike();
            do
            {
                hike = GetHike();
            } while (hike.StartPoint == null);

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
            Console.WriteLine("\nAverage duration updated successfully!", Color.LightGreen);



        }

        private void UpdateEndPoint()
        {
            ListAll();

            var hike = new Hike();
            do
            {
                hike = GetHike();
            } while (hike.StartPoint == null);

            Console.WriteLine("\nEnter end point: ");
            hike.EndPoint = Console.ReadLine();
            hikeController.Update(hike);
            Console.WriteLine("\nEnd point updated successfully!", Color.LightGreen);


        }

        private void UpdateStartDate()
        {
            ListAll();

            var hike = new Hike();
            do
            {
                hike = GetHike();
            } while (hike.StartPoint == null);

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
            Console.WriteLine("\nStart Date updated successfully!", Color.LightGreen);

        }

        private void UpdateStartPoint()
        {
            ListAll();


            var hike = new Hike();
            do
            {
                hike = GetHike();
            } while (hike.StartPoint == null);


            Console.WriteLine("\nEnter start point: ");
            hike.StartPoint = Console.ReadLine();
            hikeController.Update(hike);
            Console.WriteLine("\nStart Point updated successfully!", Color.LightGreen);

        }

        private void Exit()
        {
            DisplayHikes displayHikes = new DisplayHikes();

        }

        private Hike GetHike()
        {


            Console.WriteLine("\nEnter ID to update:");

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

        private string PrintHike(Hike hike)
        {
            return $"{hike.Id} From {hike.StartPoint} to {hike.EndPoint}- {hike.AverageDuration:F0} hours on {hike.StartDate.ToString("dd/MM/yyyy")}";

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