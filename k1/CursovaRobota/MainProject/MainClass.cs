using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MainProject
{
    class MainClass
    {
        static void Main(string[] args)
        {
            var backend = new NewsLib();
            bool flag = true;
            while (flag)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\nThis is main menu\nPress [1] to Create new user\nPress [2] to Create new news\nPress [3] to Search news\nPress [0] to Exit");
                Console.Write("--> ");
                int i = Convert.ToInt32(Console.ReadLine());
                switch (i)
                {
                    case 1:
                        Registration(backend);
                        break;
                    case 2:
                        NewNews(backend);
                        break;
                    case 3:
                        SearchNewsControl(backend);
                        break;
                }
                if (i != 1 && i != 2 && i != 3 && i != 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid input\n");
                }
                else if (i == 0)
                {
                    flag = false;
                    Console.WriteLine("Session is ended");
                }
                Console.ResetColor();
            }
        }

        public static void Registration(NewsLib backend)
        {
            Console.Write("Please enter your future nickname: ");
            string nick = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nick))
            {
                Console.WriteLine("Try again");
                Registration(backend);
            }
            else
            {
                backend.Registration(nick);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Welcome {nick}!");
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        public static void OutputNovelty(List<Novelty> localnovelties)
        {
            if (localnovelties.Count == 0)
                Console.WriteLine("No news found for your request");
            else
                foreach (var novelty in localnovelties)
                    Console.WriteLine(novelty.AllText);
        }

        public static void SearchNewsControl(NewsLib backend)
        {
            bool flag = true;
            while (flag)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("\nThis is the search stage of the news\tSelect the search parameter(s)");
                Console.WriteLine("Press [1] to search by nick\nPress [2] to search by tag\nPress [3] to search by time\nPress [0] to exit");
                Console.Write("--> ");
                int i = Convert.ToInt32(Console.ReadLine());
                switch (i)
                {
                    case 1:
                        Console.WriteLine("This is news search by nick\nEnter nick");
                        string tempnick = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        if (!TextChecker(tempnick))
                        {
                            OutputNovelty(backend.FindNoveltyWithAutor(tempnick));
                        }
                        break;
                    case 2:
                        Console.WriteLine("This is news search by tags\nEnter tag(s)");
                        string temptag = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        if (!TextChecker(temptag))
                            OutputNovelty(backend.FindNoveltyWithTag(temptag));
                        break;
                    case 3:
                        Console.WriteLine("This is news search by date\nEnter date");
                        string tempdate = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        if (!TextChecker(tempdate))
                        {
                            OutputNovelty(backend.FindNoveltyWithDate(tempdate));
                        }
                        break;
                }
                if (i != 0 && i != 1 && i != 2 && i != 3 && i != 4)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid input\n");
                    Console.ResetColor();
                }
                if (i == 0)
                {
                    flag = false;
                }
            }
        }

        public static void NewNews(NewsLib backend)
        {
            bool Flag = true;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("News can add only registered users\nEnter your nickname: ");
            Console.ResetColor();
            string NickCheck = Console.ReadLine();
            string CurrentNick;
            if (!backend.NicknameChack(NickCheck))
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Nickname was not found\nPress 0 to exit or any number to continue");
                Console.ResetColor();
                int Key = Convert.ToInt32(Console.ReadLine());
                if (Key != 0) 
                    NewNews(backend);
            }
            else if (Flag)
            {
                CurrentNick = NickCheck;
                bool flag = true;
                while (flag)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("\nThis is the stage of news entry\n\nSelect a category\n1 - About Sport\t2 - About Games\nPress 0 to return in main menu");
                    Console.Write("--> ");
                    int Key = Convert.ToInt32(Console.ReadLine());
                    switch (Key)
                    {
                        case 1:
                            SportHead(CurrentNick, backend);
                            break;
                        case 2:
                            GamesHead(CurrentNick, backend);
                            break;
                    }
                    if (Key != 1 && Key != 2 && Key != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Invalid input\n");
                        Console.ResetColor();
                    }
                    if (Key == 0)
                    {
                        flag = false;
                    }
                    Console.ResetColor();
                }
            }

        }

        //Empty string check method
        public static bool TextChecker(string CurrentText)
        {
            if (CurrentText == String.Empty)
                return true;
            else
                return false;
        }

        public static void GamesHead(string CurrentNick, NewsLib backend)
        {
            Console.Write("You have chosen a heading cars\nPlease enter your news tags: ");
            string NewsTag = Console.ReadLine();
            if (TextChecker(NewsTag))
            {
                Console.WriteLine("If you want to return to main menu PRESS 0\nIf you want to continue PRESS 1");
                int Key = Convert.ToInt32(Console.ReadLine());
                if (Key == 1)
                    GamesHead(CurrentNick, backend);
                else if (Key == 0)
                    return;
            }
            else
            {
                Console.WriteLine($"Your tags {NewsTag}\nEnter news text");
                string NewsText = Console.ReadLine();
                if (TextChecker(NewsText))
                {
                    Console.WriteLine("If you want to return to main menu PRESS 0\nIf you want to continue PRESS 1");
                    int Key = Convert.ToInt32(Console.ReadLine());
                    if (Key == 1) 
                        GamesHead(CurrentNick, backend);
                }
                else
                {
                    Console.WriteLine("Enter the time of publication of the news");
                    string NewsTime = Console.ReadLine();
                    backend.AddNovelty(NewsText, CurrentNick, NewsTag, NewsTime);
                }
            }
        }

        public static void SportHead(string CurrentNick, NewsLib backend)
        {
            Console.Write("You have chosen a heading cars\nPlease enter your news tags: ");
            string NewsTag = Console.ReadLine();
            if (TextChecker(NewsTag))
            {
                Console.WriteLine("If you want to return to main menu PRESS 0\nIf you want to continue PRESS 1");
                int Key = Convert.ToInt32(Console.ReadLine());
                if (Key == 1)
                    SportHead(CurrentNick, backend);
                else if (Key == 0)
                    return;
            }
            else
            {
                Console.WriteLine($"Your tags {NewsTag}\nEnter news text");
                string NewsText = Console.ReadLine();
                if (TextChecker(NewsText))
                {
                    Console.WriteLine("If you want to return to main menu PRESS 0\nIf you want to continue PRESS 1");
                    int Key = Convert.ToInt32(Console.ReadLine());
                    if (Key == 1)
                        SportHead(CurrentNick, backend);
                }
                else
                {
                    Console.WriteLine("Enter the time of publication of the news");
                    string NewsTime = Console.ReadLine();
                    backend.AddNovelty(NewsText, CurrentNick, NewsTag, NewsTime);
                }
            }
        }

        //public static void SportHead(string CurrentNick, NewsLib backend)
        //{
        //    Console.Write("You have chosen a heading sports\nPlease enter your news tags: ");
        //    string NewsTag = Console.ReadLine();
        //    if (TextChecker(NewsTag))
        //    {
        //        Console.WriteLine("If you want to return to main menu PRESS 0\nIf you want to continue PRESS 1");
        //        int Key = Convert.ToInt32(Console.ReadLine());
        //        if (Key == 1) SportHead(CurrentNick, backend);
        //        else Control();
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Your tags {NewsTag}\nEnter news text");
        //        string NewsText = Console.ReadLine();
        //        if (TextChecker(NewsText))
        //        {
        //            Console.WriteLine("If you want to return to main menu PRESS 0\nIf you want to continue PRESS 1");
        //            int Key = Convert.ToInt32(Console.ReadLine());
        //            if (Key == 1) SportHead(CurrentNick);
        //            else Control();
        //        }
        //        else
        //        {
        //            Console.WriteLine("Enter the time of publication of the news");
        //            string NewsTime = Console.ReadLine();
        //            backend.AddNovelty(NewsText, CurrentNick, NewsTag, NewsTime);
        //        }
        //    }
        //}
    }
}
