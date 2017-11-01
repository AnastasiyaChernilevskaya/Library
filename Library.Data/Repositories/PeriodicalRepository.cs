using System.Collections.Generic;
using System.Linq;

namespace Library.Data.Repositories
{
    public class PeriodicalRepository : LibraryRepository
    {
        private Context _context;

        public PeriodicalRepository()
        {
            _context = new Context();
        }

        public List<Periodical> GetPeriodicals()
        {
            var result = new List<Periodical>();
            result = _context.Periodicals.ToList();

            return result;
        }

        public void CreatePeriodical(Periodical periodical)
        {
            var entity = new Periodical();

            entity.IncludeToPage = periodical.IncludeToPage;
            entity.Name = periodical.Name;
            entity.Publisher = periodical.Publisher;

            entity.LibraryType = Type.Periodical.ToString();

            entity.YearOfPublishing = periodical.YearOfPublishing;

            _context.Periodicals.Add(entity);
            _context.SaveChanges();
        }

        public void UpdatePeriodical(Periodical periodical)
        {
            var entity = new Periodical();

            entity.IncludeToPage = periodical.IncludeToPage;
            entity.Name = periodical.Name;
            entity.Publisher = periodical.Publisher;

            entity.YearOfPublishing = periodical.YearOfPublishing;

            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void DestroyPeriodical(int id)
        {
            _context.Periodicals.Remove(GetPeriodical(id));
            _context.SaveChanges();
        }

        public Periodical GetPeriodical(int id)
        {
            return GetPeriodicals().FirstOrDefault(p => p.Id == id);
        }
        public List<Periodical> GetCheckedPeriodicals()
        {
            var result = new List<Periodical>();
            foreach (Periodical periodical in GetPeriodicals())
            {
                if (periodical.IncludeToPage)
                {
                    result.Add(periodical);
                }
            }
            return result;
        }
    }
}