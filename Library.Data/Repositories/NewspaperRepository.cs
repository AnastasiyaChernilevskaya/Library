using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Data.Repositories;

namespace Library.Data.Repositories
{
    public class NewspaperRepository : LibraryRepository
    {
        private Context _context;

        public NewspaperRepository()
        {
            _context = new Context();
        }

        public List<Newspaper> GetNewspapers()
        {
            var result = new List<Newspaper>();
            result = _context.Newspapers.ToList();

            return result;
        }

        public void CreateNewspaper(Newspaper newspaper)
        {
            var entity = new Newspaper();

            entity.IncludeToPage = newspaper.IncludeToPage;
            entity.Name = newspaper.Name;
            entity.Publisher = newspaper.Publisher;

            entity.LibraryType = Type.Newspaper.ToString();

            entity.YearOfPublishing = newspaper.YearOfPublishing;

            _context.Newspapers.Add(entity);
            _context.SaveChanges();
        }

        public void UpdateNewspaper(Newspaper newspaper)
        {
            var entity = new Newspaper();

            entity.IncludeToPage = newspaper.IncludeToPage;
            entity.Name = newspaper.Name;
            entity.Publisher = newspaper.Publisher;

            entity.YearOfPublishing = newspaper.YearOfPublishing;

            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void DestroyNewspaper(int id)
        {
            _context.Newspapers.Remove(GetNewspaper(id));
            _context.SaveChanges();
        }

        public Newspaper GetNewspaper(int id)
        {
            return GetNewspapers().FirstOrDefault(p => p.Id == id);
        }
        public List<Newspaper> GetCheckedNewspapers()
        {
            var result = new List<Newspaper>();
            foreach (Newspaper newspaper in GetNewspapers())
            {
                if (newspaper.IncludeToPage)
                {
                    result.Add(newspaper);
                }
            }
            return result;
        }
    }
}
