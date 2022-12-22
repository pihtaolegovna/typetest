using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace txttypingtest
{
    internal class UsersAndTable
    {
        public void recordtable()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Record table:");
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
        }
    }
    internal class User
    {
        public string Name;
        public double LettersPerSecond;
        public int LettersPerMinute;
        public int Mistakes;

        public User(string username, int lettersPerMinute, double lettersPerSecond, int mistakes)
        {
            Name = username;
            LettersPerSecond = lettersPerSecond;
            LettersPerMinute = lettersPerMinute;
            Mistakes = mistakes;
        }
    }
}
