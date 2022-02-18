/*
This is a small gane I built when I was first learning C# in school.
I never went back to it to update its ability to use OOP or anything
but it still works nice and is playable.

Enjoy!
*/

using System;
using System.Threading;

namespace Game_Final
{
    class TextBasedGame
    {
        static void Main()
        {
            
            Random rnd = new Random();
            bool end = false;
            int eHealth = 100;
            int eHealth2;
            int pHealth = 100;
            int skipTurn = 0;
            int attack = 0;
            bool attackSelect = false;

            //EXP - LEVEL UP
            int exp = 0;
            int level = 0;
            int upgradeToken = 0;
            //End Exp

            int milliseconds = 100;
            bool next = false;
            string mmOption = String.Empty;
            int option;

            do
            {
                Console.Clear();
                MainMenu();
                Console.Clear();
                MainMenu();

                eHealth = EnemyHealth(ref eHealth);
                eHealth2 = eHealth;
                pHealth = 100;
                end = false;
                skipTurn = 0;
                attack = 0;
                mmOption = String.Empty;
                option = 0;

                do
                {

                    Console.SetCursorPosition(27, 10);
                    mmOption = "0";
                    mmOption = Console.ReadLine();

                    if ((mmOption == "1" || mmOption == "2" || mmOption == "3"))
                    {
                        next = true;
                    }

                } while (next == false);

                option = int.Parse(mmOption);

                if (MenuSwitch(option) == 1)
                {
                    Console.Clear();
                    BattleMenu(75, 16, ref pHealth, ref eHealth);
                    Console.Clear();
                    BattleMenu(75, 16, ref pHealth, ref eHealth);

                    do
                    {
                        if (skipTurn > 0)
                        {
                            skipTurn--;
                            BattleMenu(75, 16, ref pHealth, ref eHealth);

                            if (end == false)
                            {
                                attack = rnd.Next(1, 3);
                                
                                Thread.Sleep(milliseconds);

                                pHealth -= EnemyAttack(attack, ref eHealth2);
                            }

                        }
                        else if (skipTurn == 0)
                        {
                            do
                            {
                                attack = 0;
                                string option2 = String.Empty;
                                BattleMenu(75, 16, ref pHealth, ref eHealth);
                                Console.SetCursorPosition(38, 11);
                                option2 = Console.ReadLine();
                                attack = int.Parse(option2);

                                if ((option2 == "1" || option2 == "2" || option2 == "3" || option2 == "4"))
                                {
                                    attackSelect = true;
                                }

                            } while (attackSelect == false);


                            if(attackSelect == true)
                            {

                                //PLAYER ATTACKING CALCULATIONS
                                if (attack != 4)
                                {
                                    eHealth -= PlayerAttack(attack);
                                }
                                if (attack == 3)
                                {
                                    skipTurn = 1;
                                    attack = 0;
                                }
                                if (attack == 4)
                                {
                                    if (PlayerAttack(attack) < 7)
                                    {
                                        end = false;
                                        pHealth -= 5;
                                    }
                                    else
                                    {
                                        end = true;
                                    }
                                }
                                //END PLAYER ATTACKING CALCULATIONS


                                //ENEMY ATTACKING CALCULATIONS
                                if (end == false)
                                {
                                    Thread.Sleep(milliseconds);
                                    attack = rnd.Next(1, 3);

                                    pHealth -= EnemyAttack(attack, ref eHealth2);
                                }
                                //END ENEMY ATTACKING CALCULATIONS

                            }

                            //CHECKING FOR GAME OVER
                            if (pHealth <= 0)
                            {
                                end = true;
                                pHealth = 0;

                            }
                            else if (eHealth <= 0)
                            {
                                end = true;
                                eHealth = 0;
                            }
                            //END CHECKING FOR GAME OVER
                        }

                    } while (end == false);

                }
                else if (MenuSwitch(option) == 2)
                {
                    Console.Clear();
                    UpgradeMenu();
                    Console.Clear();
                    UpgradeMenu();
                }
                else if (MenuSwitch(option) == 3)
                {
                    Console.Clear();
                    Environment.Exit(0);
                }

                if (end == true)
                {
                    BattleMenu(75, 16, ref pHealth, ref eHealth);
                }

                milliseconds = 3000;
                Thread.Sleep(milliseconds);

            } while (option != 3);

            Environment.Exit(0);

        }

        static void ColorBackAndLine(ConsoleColor color, ConsoleColor color2)
        {
            Console.ForegroundColor = color2;
            Console.BackgroundColor = color;
        }

        static void MainMenu(int a = 50, int b = 15)
        {

            Console.Title = "Battle Simulator - Main Menu";

            Console.SetWindowSize(a, b); //Witdh , Hight
            Console.SetBufferSize(a, b);

            string separator = new string('-', a - 2);
            string separator2 = new string(' ', a - 2);

            ColorBackAndLine(ConsoleColor.Black, ConsoleColor.White);

            //How to get it in the middle of screen correctly
            //(Window width - string length) / 2
            ColorBackAndLine(ConsoleColor.DarkCyan, ConsoleColor.Black);
            Console.SetCursorPosition(0, 0);
            Console.Write("BUILD V. 1");
            
            ColorBackAndLine(ConsoleColor.DarkBlue, ConsoleColor.Yellow);
            Console.SetCursorPosition(17, 5);
            Console.WriteLine("BATTLE SIMULATOR");

            ColorBackAndLine(ConsoleColor.DarkCyan, ConsoleColor.Black);
            Console.SetCursorPosition(20, 7);
            Console.Write("1. Battle");

            Console.SetCursorPosition(20, 8);
            Console.Write("2. Upgrade");

            Console.SetCursorPosition(19, 9);
            Console.Write("3. Exit Game");

            Console.SetCursorPosition(19, 10);
            Console.Write("Select: ");
            //End main menu options


            Console.SetCursorPosition(0, 14);
            //Menu Options
        }

        static void BattleMenu(int a, int b, ref int pHealth, ref int eHealth)
        {

            //CONSOLE MAIN SETTINGS
            Console.Clear();
            Console.Title = "Battle Simulator - Battle";
            ColorBackAndLine(ConsoleColor.Black, ConsoleColor.White);

            //SETTING WALLS
            Console.SetWindowSize(a, b); //Witdh , Hight
            Console.SetBufferSize(a, b);

            //SEPERATORS USED FOR WALLS
            string separator = new string('-', a - 3);
            string separator2 = new string(' ', a - 3);

            //BASIC WALL SETUP
            ColorBackAndLine(ConsoleColor.Black, ConsoleColor.White);
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"/{separator}\\");
            for (int i = 1; i <= 13; ++i)
            {
                Console.SetCursorPosition(0, i);
                Console.Write($"|{separator2}|");
                if (i == 9)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write($"|{separator}|");
                }
            }
            Console.SetCursorPosition(0, 14);
            Console.WriteLine($"\\{separator}/");

            //ENENMY HEALTH
            ColorBackAndLine(ConsoleColor.Red, ConsoleColor.White);
            Console.SetCursorPosition(1, 1);
            Console.Write($"Enemy Health: {eHealth}");

            //PLAYER HEALTH
            ColorBackAndLine(ConsoleColor.Green, ConsoleColor.White);
            Console.SetCursorPosition(55, 8);
            Console.Write($"Player Health: {pHealth}");

            //PLAYER OPTIONS
            ColorBackAndLine(ConsoleColor.Black, ConsoleColor.White);
            Console.SetCursorPosition(1, 10);
            Console.Write("1. Basic Attack");
            Console.SetCursorPosition(1, 11);
            Console.Write("2. Magic Attack");
            Console.SetCursorPosition(1, 12);
            Console.Write("3. Heavy Attack");
            Console.SetCursorPosition(1, 13);
            Console.Write("4. Flee");
            Console.SetCursorPosition(30, 11);
            Console.Write("Option: ");

        }

        static void UpgradeMenu(int a = 50, int b = 15)
        {
            Console.Clear();
            Console.Title = "Battle Simulator - Upgrade Menu - NOT READY!";

            Console.SetWindowSize(a, b); //Witdh , Hight
            Console.SetBufferSize(a, b);

            string separator = new string('-', a - 2);
            string separator2 = new string(' ', a - 2);

            ColorBackAndLine(ConsoleColor.DarkGreen, ConsoleColor.White);
        }

        static int MenuSwitch(int selection)
        {
            int menu = 0;

            switch (selection)
            {
                case 1:
                    menu = 1;
                    break;
                case 2:
                    menu = 2;
                    break;
                case 3:
                    menu = 3;
                    break;
                default:
                    break;
            }

            return menu;

        }

        static int PlayerAttack(int attackNumber)
        {
            Random rnd = new Random();
            int AtkDmg;
            int sum = 0;
            int milliseconds = 2000;
            int a = 20;
            int b = 3;


            switch (attackNumber)
            {

                case 1:
                    AtkDmg = rnd.Next(2, 5);
                    sum = AtkDmg;

                    Console.SetCursorPosition(a, b);
                    Console.Write($"You hit the enemy with a baisc attack!");
                    Console.SetCursorPosition(a + 6, b + 1);
                    Console.Write($"You hit the enemy for {sum}!");

                    Thread.Sleep(milliseconds);

                    break;
                case 2:
                    AtkDmg = rnd.Next(6, 9);
                    sum = AtkDmg;

                    Console.SetCursorPosition(a, b);
                    Console.Write($"You hit the enemy with a magic attack!");
                    Console.SetCursorPosition(a + 6, b + 1);
                    Console.Write($"You hit the enemy for {sum}!");

                    Thread.Sleep(milliseconds);

                    break;
                case 3:
                    AtkDmg = rnd.Next(10, 12);
                    sum = AtkDmg;

                    Console.SetCursorPosition(a, b);
                    Console.Write($"You hit the enemy with a heavy attack!");
                    Console.SetCursorPosition(a + 6, b + 1);
                    Console.Write($"You hit the enemy for {sum}!");

                    Thread.Sleep(milliseconds);

                    break;
                case 4:
                    break;
                default:
                    break;
            }
            return sum;
        }

        static int EnemyAttack(int attackNumber, ref int H)
        {
            Random rnd = new Random();
            int AtkDmg;
            int sum = 0;
            int milliseconds;
            int a = 15;
            int b = 3;


            string separator2 = new string(' ', 15);


            if (H == 100)
            {
                switch (attackNumber)
                {
                    case 1:
                        milliseconds = 100;
                        Thread.Sleep(milliseconds);
                        AtkDmg = rnd.Next(1, 3);
                        sum = AtkDmg;

                        Console.SetCursorPosition(a, b);
                        Console.Write($"You were hit by the enemy with a baisc attack!");
                        Console.SetCursorPosition(a + 11, b + 1);
                        Console.Write($"You got hit for {sum}!      ");

                        milliseconds = 2000;
                        Thread.Sleep(milliseconds);

                        break;
                    case 2:
                        milliseconds = 100;
                        Thread.Sleep(milliseconds);
                        AtkDmg = rnd.Next(4, 7);
                        sum = AtkDmg;

                        Console.SetCursorPosition(a, b);
                        Console.Write($"You were hit by the enemy with a magic attack!");
                        Console.SetCursorPosition(a + 11, b + 1);
                        Console.Write($"You got hit for {sum}!      ");

                        milliseconds = 2000;
                        Thread.Sleep(milliseconds);

                        break;
                    case 3:
                        milliseconds = 100;
                        Thread.Sleep(milliseconds);
                        AtkDmg = rnd.Next(8, 10);
                        sum = AtkDmg;

                        Console.SetCursorPosition(a, b);
                        Console.Write($"You were hit by the enemy with a heavy attack!");
                        Console.SetCursorPosition(a + 11, b + 1);
                        Console.Write($"You got hit for {sum}!      ");

                        milliseconds = 2000;
                        Thread.Sleep(milliseconds);

                        break;
                    default:
                        break;
                }
            }
            else if (H == 150)
            {
                switch (attackNumber)
                {
                    case 1:
                        milliseconds = 100;
                        Thread.Sleep(milliseconds);
                        AtkDmg = rnd.Next(3, 5);
                        sum = AtkDmg;

                        Console.SetCursorPosition(a, b);
                        Console.Write($"You were hit by the enemy with a baisc attack!");
                        Console.SetCursorPosition(a + 11, b + 1);
                        Console.Write($"You got hit for {sum}!      ");

                        milliseconds = 2000;
                        Thread.Sleep(milliseconds);

                        break;
                    case 2:
                        milliseconds = 100;
                        Thread.Sleep(milliseconds);
                        AtkDmg = rnd.Next(6, 9);
                        sum = AtkDmg;

                        Console.SetCursorPosition(a, b);
                        Console.Write($"You were hit by the enemy with a magic attack!");
                        Console.SetCursorPosition(a + 11, b + 1);
                        Console.Write($"You got hit for {sum}!      ");

                        milliseconds = 2000;
                        Thread.Sleep(milliseconds);

                        break;
                    case 3:
                        milliseconds = 100;
                        Thread.Sleep(milliseconds);
                        AtkDmg = rnd.Next(8, 11);
                        sum = AtkDmg;

                        Console.SetCursorPosition(a, b);
                        Console.Write($"You were hit by the enemy with a heavy attack!");
                        Console.SetCursorPosition(a + 11, b + 1);
                        Console.Write($"You got hit for {sum}!      ");

                        milliseconds = 2000;
                        Thread.Sleep(milliseconds);

                        break;
                    default:
                        break;
                }
            }
            else if (H == 200)
            {
                switch (attackNumber)
                {
                    case 1:
                        milliseconds = 100;
                        Thread.Sleep(milliseconds);
                        AtkDmg = rnd.Next(5, 7);
                        sum = AtkDmg;

                        Console.SetCursorPosition(a, b);
                        Console.Write($"You were hit by the enemy with a baisc attack!");
                        Console.SetCursorPosition(a + 11, b + 1);
                        Console.Write($"You got hit for {sum}!      ");

                        milliseconds = 2000;
                        Thread.Sleep(milliseconds);

                        break;
                    case 2:
                        milliseconds = 100;
                        Thread.Sleep(milliseconds);
                        AtkDmg = rnd.Next(8, 11);
                        sum = AtkDmg;

                        Console.SetCursorPosition(a, b);
                        Console.Write($"You were hit by the enemy with a magic attack!");
                        Console.SetCursorPosition(a + 11, b + 1);
                        Console.Write($"You got hit for {sum}!      ");

                        milliseconds = 2000;
                        Thread.Sleep(milliseconds);

                        break;
                    case 3:
                        milliseconds = 100;
                        Thread.Sleep(milliseconds);
                        AtkDmg = rnd.Next(10, 13);
                        sum = AtkDmg;

                        Console.SetCursorPosition(a, b);
                        Console.Write($"You were hit by the enemy with a heavy attack!");
                        Console.SetCursorPosition(a + 11, b + 1);
                        Console.Write($"You got hit for {sum}!      ");

                        milliseconds = 2000;
                        Thread.Sleep(milliseconds);

                        break;
                    default:
                        break;
                }
            }

            return sum;
        }

        static int EnemyHealth(ref int H)
        {
            Random rnd = new Random();
            int eD = rnd.Next(0, 10);


            int milliseconds = 100;
            Thread.Sleep(milliseconds);

            if (eD >= 0 && eD <= 5)
            {
                H = 100;
                //Console.WriteLine($"eD = {H} : Health = {H}");
            }
            else if (eD >= 6 && eD <= 8)
            {
                H = 150;
                //Console.WriteLine($"eD = {H} : Health = {H}");
            }
            else if (eD >= 9 && eD <= 10)
            {
                H = 200;
                //Console.WriteLine($"eD = {H} : Health = {H}");
            }
            return H;
        }


    }

}
