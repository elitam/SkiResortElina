using SkiResort.Business;
using SkiResort.Data.Models;
using SkiResort.Views.Hikes;
using SkiResort.Views.Info;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Console = Colorful.Console;

namespace SkiResort.Views
{
    public class DisplayInfo
    {
        public DisplayInfo()
        {
            InputInfo();
            // hikeController = new HikeController();
        }



        // private HikeController hikeController;


        private void ShowMenuINFO()
        {
            Console.WriteLine();
            Console.WriteLine("         ┌────────────────────────────────┐");
            Console.WriteLine("         │             INFO!              │");
            Console.WriteLine("┌────────└────────────────────────────────┘────────┐");
            Console.WriteLine("│                      MENU:                       │   ");
            Console.WriteLine("│                                                  │   ");
            Console.WriteLine("│      0. BACK              2. TRAILS              │   ");
            Console.WriteLine("│                                                  │   ");
            Console.WriteLine("│      1. LIFTS             3. LIFT PASSES         │   ");
            Console.WriteLine("│                                                  │   ");
            Console.WriteLine("└──────────────────────────────────────────────────┘");
            Console.WriteLine();

        }

        private void InputInfo()
        {

            do
            {
                ShowMenuINFO();

                int operation = -1;
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
                        Lift();
                        break;
                    case 2:
                        Trail();
                        break;
                    case 3:
                        LiftPass();
                        break;

                    default:
                       
                        break;
                }

            } while (true);
        }

        private void LiftPass()
        {
            DisplayLiftPass displayLiftPass = new DisplayLiftPass();
        }

        private void Exit()
        {
            Display display = new Display();
        }

        private void Trail()
        {
            var displayTrail = new DisplayTrail();
        }

        private void Lift()
        {
            var displayLift = new DisplayLift();
        }
    }
}