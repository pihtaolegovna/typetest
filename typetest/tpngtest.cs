using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Media;
using Newtonsoft.Json;
using System.IO;

namespace txttypingtest
{
    public static class tpngtext
    {
        static int letters = 0;
        static int mistakes = 0;
        static int cursorwidth;
        static int cursorheight;
        static string name;
        static bool isrunning = true;
        static int i;
        static List<char> textToWrite = new List<char>();
        public static void Start()
        {
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            if (name == "") name = "Unknown";
            Console.Clear();
            Console.WriteLine("Press Enter to start");
            do
            {
                Console.WriteLine();
            } while (Console.ReadKey(true).Key != ConsoleKey.Enter);
            Console.SetCursorPosition(0, 0);
            string text = "Осень и зима миновали без всяких событий. Артур прилежно занимался, и у него оставалось мало свободного времени. Все же раз, а то и два раза в неделю он улучал минутку, чтобы заглянуть к Монтанелли. Иногда он заходил к нему с книгой за разъяснением какого-нибудь трудного места, и в таких случаях разговор шел только об этом. Чувствуя вставшую между ними почти неосязаемую преграду, Монтанелли избегал всего, что могло показаться попыткой с его стороны восстановить прежнюю близость. Посещения Артура доставляли ему теперь больше горечи, чем радости. Трудно было выдерживать постоянное напряжение, казаться спокойным и вести себя так, словно ничто не изменилось. Артур тоже замечал некоторую перемену в обращении padre и, понимая, что она связана с тяжким вопросом о его «новых идеях», избегал всякого упоминания об этой теме, владевшей непрестанно его мыслями.";

            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(text[i]);
                textToWrite.Add(text[i]);
                cursorwidth += 1;
                Console.SetCursorPosition(cursorwidth, cursorheight);
                if (cursorwidth > 76)
                {
                    cursorheight++;
                    cursorwidth = 0;
                }
            }
            cursorwidth = 0;
            cursorheight = 0;
            letters = 0;
            mistakes = 0;
            Console.CursorVisible = true;
            Console.SetCursorPosition(0, 0);
            new Thread(timer).Start();
            foreach (char ch in textToWrite)
            {
                if (!isrunning) break;
                char userKey = Console.ReadKey(true).KeyChar;
                if (userKey == ch)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(cursorwidth, cursorheight);
                    Console.Write(ch);
                    Console.ResetColor();
                    letters++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(cursorwidth, cursorheight);
                    Console.Write(ch);
                    Console.ResetColor();
                    mistakes++;
                }
                if (cursorwidth > 76)
                {
                    cursorwidth = 0;
                    cursorheight++;
                }
                cursorwidth++;
                Console.SetCursorPosition(cursorwidth, cursorheight);
            }
            Console.CursorVisible = false;

        }
        public static void timer()
        {
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            do
            {
                Console.CursorVisible = false;

                Console.SetCursorPosition(0, 15);
                Console.WriteLine($"Time Left: 0:{60 - stopwatch.ElapsedMilliseconds / 1000}");
                Console.SetCursorPosition(cursorwidth, cursorheight);
                Thread.Sleep(1000);
            }
            while (60 - stopwatch.ElapsedMilliseconds / 1000 >= 0);
            isrunning = false;

            stopwatch.Stop();
            stopwatch.Reset();
            results();

        }
        static void results()
        {
            Console.SetCursorPosition(0, 16);
            Console.WriteLine("Result");
            Console.WriteLine($"{mistakes} mistakes");
            Console.WriteLine($"{letters} leters/minute");
            Console.Write($"{Math.Round(Convert.ToDouble(letters) / 60, 3)} letters/second.");

            List<User> users;
            if (!File.Exists("ScoreTable.json"))
            {
                FileStream fileStream = File.Create("ScoreTable.json");
                users = new List<User>();
                fileStream.Dispose();
            }
            else
            {
                string usersInfo = File.ReadAllText("Table.json");
                users = JsonConvert.DeserializeObject<List<User>>(usersInfo);
            }
            users.Add(new User(name, letters, Math.Round(Convert.ToDouble(letters) / 60, 3), mistakes));
            users.Sort((x, y) => x.LettersPerMinute.CompareTo(y.LettersPerMinute));
            users.Reverse();
            string json = JsonConvert.SerializeObject(users);
            File.WriteAllText("Table.json", json);
            ConsoleKeyInfo Key;
            do
            {
                Key = Console.ReadKey(true);
                if (Key.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    Program.Start();
                }
            } while (Key.Key != ConsoleKey.Enter);

            Console.Clear();
            Start();
        }
    }
}
