﻿namespace ClassLibrary
{

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
}