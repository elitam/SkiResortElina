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
    public class DisplayServicesAndActivities
    {
        public DisplayServicesAndActivities()
        {
            InputServices();

        }
        private void ShowMenuServices()
        {

            Console.WriteLine();
            Console.WriteLine("         ┌────────────────────────────────┐");
            Console.WriteLine("         │      SERIVES & ACTIVITIES      │");
            Console.WriteLine("┌────────└────────────────────────────────┘────────┐");
            Console.WriteLine("│                      MENU:                       │   ");
            Console.WriteLine("│                                                  │   ");
            Console.WriteLine("│                     0. EXIT                      │   ");
            Console.WriteLine("│                                                  │   ");
            Console.WriteLine("│        1. RENTAL              2. HIKES           │   ");
            Console.WriteLine("│                                                  │   ");
            Console.WriteLine("└──────────────────────────────────────────────────┘");
            Console.WriteLine();
        }



        private void InputServices()
        {
            var operation = -1;
            do
            {
            ShowMenuServices();

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
                        Rental();
                        break;
                    case 2:
                        Hikes();
                        break;

                    default:
                        break;
                }

            } while (true);

        }

        private void Rental()
        {
            DisplayRental displayRental = new DisplayRental();
        }

        private void Hikes()
        {
            DisplayHikes displayHikes = new DisplayHikes();
        }

        private void Back()
        {
            Display display = new Display();
        }
    }
}