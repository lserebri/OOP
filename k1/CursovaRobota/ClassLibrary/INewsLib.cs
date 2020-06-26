using System.Collections.Generic;

namespace ClassLibrary
{
    public interface INewsLib
    {
        void AddNovelty(string text, string author, string tag, string date, NewsCategory newstype);
        List<Novelty> FindNoveltyWithAutor(string autor);
        List<Novelty> FindNoveltyWithDate(string date);
        List<Novelty> FindNoveltyWithTag(string tag);
        List<Novelty> GetLatest(int count = 5);
        bool NicknameChack(string nickname);
        void Registration(string nick);
        List<Novelty> FindNoveltyWithHeading(string tempheading);
    }
}