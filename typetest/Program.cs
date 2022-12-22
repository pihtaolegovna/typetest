using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace txttypingtest
{
    public class Program
    {
        public static int amount;
        public static int selector = 0;
        public static int selected;
        public static int isrunning;
        static void Main(string[] args)
        {
            Start();
            Console.CursorVisible = false;
        }
        public static void Start()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("  Start");
            Console.WriteLine("  Table");
            Console.WriteLine("  Exit");
            arrows(3);
            Console.WriteLine(selected);
            switch (selected)
            {
                case 0:
                    {
                        Console.Clear();
                        tpngtext.Start();
                        break;
                    }
                case 1:
                    {
                        table.Table();
                        break;
                    }
                case 2:
                    {
                        Environment.Exit(1);
                        break;
                    }
            }

        }
        public static void arrows(int amount)
        {
            Console.SetCursorPosition(0, 2);
            Console.Write("1");
            Console.SetCursorPosition(0, 2);
            bool run = false;
            while (run != true)
            {
                ConsoleKeyInfo menuchoosekey = Console.ReadKey();
                string choosekey = (menuchoosekey.Key.ToString());
                switch (choosekey)
                {
                    case "UpArrow":
                        Console.SetCursorPosition(0, selector+2);
                        selector--;
                        break;
                    case "DownArrow":
                        Console.SetCursorPosition(0, selector+2);
                        selector++;
                        break;
                    case "_":
                        {
                            break;
                        }
                    case "Escape":
                        {
                            Environment.Exit(1);
                            break;
                        }
                    case "Enter":
                        {
                            run = true;
                            selected = selector;
                            Console.WriteLine(selected);
                            break;
                        }
                }
                if (selector < 0)
                    selector = amount - 1;
                if (selector > amount - 1)
                {
                    selector = 0;
                    Console.Write("  ");
                    Console.SetCursorPosition(0, 0);
                    Console.SetCursorPosition(0, 2);
                }
                Console.Write("  ");
                Console.SetCursorPosition(0, 2);
                Console.SetCursorPosition(0, selector+2);
                Console.Write(selector + 1);
            }
        }
    }
}
