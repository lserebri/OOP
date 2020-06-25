using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public enum NewsCategory
    {
        Sport,
        Games,
        IT,
        Music
    }

    public class NewsLib : INewsLib
    {
        private List<string> authors = new List<string>();

        private List<Novelty> Novelties = new List<Novelty>();

        public void AddNovelty(string text, string author, string tag, string date, NewsCategory newstype)
        {
            var newnowelty = new Novelty(text, author, tag, date);
            Novelties.Add(newnowelty);
        }
        public void Registration(string nick)
        {
            authors.Add(nick);
        }

        public bool NicknameChack(string nickname)
        {
            for (int i = 0; i < authors.Count; i++)
            {
                if (nickname == authors[i])
                    return true;
            }
            return false;
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

    }
}
