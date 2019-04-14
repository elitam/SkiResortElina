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
    public class Display
    {
        public Display()
        {
            Input();
        }
        private void WelcomeMenu()
        {

            Console.WriteLine();
            Console.WriteLine("         ┌────────────────────────────────┐");
            Console.WriteLine("         │   WELCOME TO OUR SKI RESORT!   │");
            Console.WriteLine("┌────────└────────────────────────────────┘────────┐");
            Console.WriteLine("│                     MENU:                        │   ");
            Console.WriteLine("│                                                  │   ");
            Console.WriteLine("│     0. EXIT                    2. SEVICES        │   ");
            Console.WriteLine("│                                     &            │   ");
            Console.WriteLine("│     1. INFO                     ACTIVITIES       │   ");
            Console.WriteLine("│                                                  │   ");
            Console.WriteLine("└──────────────────────────────────────────────────┘");
            Console.WriteLine();
        }

        private int closeOperation = 0;

        private void Input()
        {
            WelcomeMenu();

            var operation = -1;
            do
            {
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
                        Environment.Exit(0);
                        break;
                    case 1:
                        Info();
                        break;
                    case 2:
                        SevicesAndAct();
                        break;

                    default:
                        break;
                }

            } while (true);

        }

        
        

        private void SevicesAndAct()
        {
            DisplayServicesAndActivities displayServicesAndActivities = new DisplayServicesAndActivities();
        }

        private void Info()
        {
            DisplayInfo displayInfo = new DisplayInfo();
        }
    }
}