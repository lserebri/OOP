using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class NewsLibClass : MainMenu
    {
        private List<string> authors = new List<string>();

        private List<Novelty> Novelties = new List<Novelty>();

        public class Novelty
        {
            public string Name;
            public string Text;
            public string Author;
            public string AllText;
            public string Tag;
            public string Date;

            public Novelty(string text, string author, string tag, string date)
            {
                Text = text;
                Author = author;
                Tag = tag;
                Date = date;
                AllText = $"About {Name} : " + "< " + Tag + " >" + "\n" + Text + "\n" + "At " + Date + " hours\n" + Author;
            }
        }

        public List<Novelty> GetLatest(int count = 5)
        {
            return Novelties.OrderByDescending(x => x.Date).Take(count).ToList();
        }

        public List<Novelty> FindNoveltyWithTag(string tag)
        {
            List<Novelty> novelty = new List<Novelty>();
            for (int i = 0; i < Novelties.Count; i++)
            {
                if (Novelties[i].Tag == tag)
                {
                    novelty.Add(Novelties[i]);
                }
            }

            return novelty;
        }

        public List<Novelty> FindNoveltyWithAutor(string autor)
        {
            List<Novelty> novelty = new List<Novelty>();
            for (int i = 0; i < Novelties.Count; i++)
            {
                if (Novelties[i].Author == autor)
                {
                    novelty.Add(Novelties[i]);
                }
            }

            return novelty;
        }

        public List<Novelty> FindNoveltyWithDate(string date)
        {
            List<Novelty> novelty = new List<Novelty>();
            for (int i = 0; i < Novelties.Count; i++)
            {
                if (Novelties[i].Date == date)
                {
                    novelty.Add(Novelties[i]);
                }
            }

            return novelty;
        }

        public void Registration()
        {
            Console.Write("Please enter your future nickname: ");
            string nick = Console.ReadLine();
            if (nick == "")
            {
                Console.WriteLine("Try again");
                Registration();
            }
            else
            {
                authors.Add(nick);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Welcome {authors[authors.Count - 1]}!");
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        public void Control() //menu
        {
            Registration();
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
                        Registration();
                        break;
                    case 2:
                        NewNews();
                        break;
                    case 3:
                        SearchNewsControl();
                        break;
                }
                if (i != 1 && i != 2 && i != 3 && i != 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid input\n");
                    Control();
                }
                if (i == 0)
                {
                    flag = false;
                    Console.WriteLine("Session is ended");
                }
                Console.ResetColor();
            }
        }

        public void OutputNovelty(List<Novelty> localnovelties)
        {
            if (localnovelties.Count == 0)
                Console.WriteLine("No news found for your request");
            else
                foreach (var novelty in localnovelties)
                    Console.WriteLine(novelty.AllText);
        }

        public void SearchNewsControl()
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
                            OutputNovelty(FindNoveltyWithAutor(tempnick));
                        }
                        break;
                    case 2:
                        Console.WriteLine("This is news search by tags\nEnter tag(s)");
                        string temptag = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        if (!TextChecker(temptag))
                            OutputNovelty(FindNoveltyWithTag(temptag));
                        break;
                    case 3:
                        Console.WriteLine("This is news search by date\nEnter date");
                        string tempdate = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        if (!TextChecker(tempdate))
                        {
                            OutputNovelty(FindNoveltyWithDate(tempdate));
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

        public void NewNews()
        {
            bool Flag = false;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("News can add only registered users\nEnter your nickname: ");
            Console.ResetColor();
            string NickCheck = Console.ReadLine();
            string CurrentNick;
            foreach (string nick in authors)
            {
                if ((nick == NickCheck))
                {
                    Flag = true;
                    break;
                }
                else Flag = false;
            }
            if (Flag)
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
                            SportHead(CurrentNick);
                            break;
                        case 2:
                            GamesHead(CurrentNick);
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
            else
            {

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Nickname was not found\nPress 0 to exit or any number to continue");
                Console.ResetColor();
                int Key = Convert.ToInt32(Console.ReadLine());
                if (Key == 0) Control();
                else NewNews();
            }

        }

        //Empty string check method
        private bool TextChecker(string CurrentText)
        {
            if (CurrentText == String.Empty)
                return true;
            else 
                return false;
        }

        private void GamesHead(string CurrentNick)
        {
            Console.Write("You have chosen a heading cars\nPlease enter your news tags: ");
            string NewsTag = Console.ReadLine();
            if (TextChecker(NewsTag))
            {
                Console.WriteLine("If you want to return to main menu PRESS 0\nIf you want to continue PRESS 1");
                int Key = Convert.ToInt32(Console.ReadLine());
                if (Key == 1) 
                    GamesHead(CurrentNick);
                else Control();
            }
            else
            {
                Console.WriteLine($"Your tags {NewsTag}\nEnter news text");
                string NewsText = Console.ReadLine();
                if (TextChecker(NewsText))
                {
                    Console.WriteLine("If you want to return to main menu PRESS 0\nIf you want to continue PRESS 1");
                    int Key = Convert.ToInt32(Console.ReadLine());
                    if (Key == 1) GamesHead(CurrentNick);
                    else Control();
                }
                else
                {
                    Console.WriteLine("Enter the time of publication of the news");
                    string NewsTime = Console.ReadLine();
                    Novelty NewNewsArticle = new Novelty(NewsText, CurrentNick, NewsTag, NewsTime);
                    Novelties.Add(NewNewsArticle);
                }
            }
        }

        private void SportHead(string CurrentNick)
        {
            Console.Write("You have chosen a heading sports\nPlease enter your news tags: ");
            string NewsTag = Console.ReadLine();
            if (TextChecker(NewsTag))
            {
                Console.WriteLine("If you want to return to main menu PRESS 0\nIf you want to continue PRESS 1");
                int Key = Convert.ToInt32(Console.ReadLine());
                if (Key == 1) SportHead(CurrentNick);
                else Control();
            }
            else
            {
                Console.WriteLine($"Your tags {NewsTag}\nEnter news text");
                string NewsText = Console.ReadLine();
                if (TextChecker(NewsText))
                {
                    Console.WriteLine("If you want to return to main menu PRESS 0\nIf you want to continue PRESS 1");
                    int Key = Convert.ToInt32(Console.ReadLine());
                    if (Key == 1) SportHead(CurrentNick);
                    else Control();
                }
                else
                {
                    Console.WriteLine("Enter the time of publication of the news");
                    string NewsTime = Console.ReadLine();
                    Novelty NewNewsArticle = new Novelty(NewsText, CurrentNick, NewsTag, NewsTime);
                    Novelties.Add(NewNewsArticle);
                }
            }
        }
    }
}
