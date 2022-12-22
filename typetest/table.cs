using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace txttypingtest
{
    public static class table
    {
        public static void Table()
        {
            ConsoleKeyInfo Key;
            do
            {
                List<User> users;
                if (File.Exists("Table.json"))
                {
                    string usersInfo = File.ReadAllText("Table.json");
                    users = JsonConvert.DeserializeObject<List<User>>(usersInfo);
                }
                else
                {
                    FileStream fileStream = File.Create("Table.json");
                    users = new List<User>();
                    fileStream.Dispose();
                }

                int i = 1;

                foreach (User user in users)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write(i + " | "+ user.Name + " " + user.LettersPerMinute + " letters per minute " + user.LettersPerSecond + " letters per second " + user.Mistakes + " mistakes");
                    i++;
                }

                Key = Console.ReadKey(true);
            } while (Key.Key != ConsoleKey.Escape);
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Program.Start();
        }
    }
}