using System.Collections.Generic;
using System.Linq;


namespace Library.Data.Repositories
{
    public class LibraryRepository : BaseRepository
    {
        private ApplicationDbContext _context;

        public LibraryRepository()
        {
            _context = new ApplicationDbContext();
        }

        public List<BaseEntity> GetEntitys()
        {
            var result = new List<BaseEntity>();

            result.AddRange(_context.Books);
            result.AddRange(_context.Newspapers);
            result.AddRange(_context.Periodicals);

            return result;
        }

        //public void CreateEntity(BaseEntity libraryEntity)
        //{
        //    var entity = new BaseEntity(); //ask Lesha!

        //    entity.Name = libraryEntity.Name;
        //    entity.Publisher = libraryEntity.Publisher;
        //    entity.IncludeToPage = libraryEntity.IncludeToPage;

        //    entity.LibraryType = libraryEntity.LibraryType;

        //    //_context.LibraryEntitys.Add(entity);
        //    _context.SaveChanges();
        //}

        public void UpdateEntityChack(int id, Type entityType)
        {
            var entity = GetEntitys().Where(x => x.Id == id).FirstOrDefault();
            if (entityType == Type.Book)
            {
                BookRepository _repository = new BookRepository();
                var book = _repository.GetBook(id);
                book.IncludeToPage = !book.IncludeToPage;
                if(book != null)
                {
                    _repository.UpdateBook(book);
                }
                return;
            }
            if (entityType == Type.Newspaper)
            {
                NewspaperRepository _repository = new NewspaperRepository();
                var newspaper = _repository.GetNewspaper(id);
                newspaper.IncludeToPage = !newspaper.IncludeToPage;
                if (newspaper != null)
                {
                    _repository.UpdateNewspaper(newspaper);
                }
                return;
            }
            if (entityType == Type.Periodical)
            {
                PeriodicalRepository _repository = new PeriodicalRepository();
                var periodical = _repository.GetPeriodical(id);
                periodical.IncludeToPage = periodical.IncludeToPage;
                if (periodical != null)
                {
                    _repository.UpdatePeriodical(periodical);
                }
            }
        }

        public void DestroyEntity(int id , Type entityType)
        {
            if (entityType == Type.Book)
            {
                BookRepository _repository = new BookRepository();
                _repository.DestroyBook(id);
            }
            if(entityType == Type.Newspaper)
            {
                NewspaperRepository _repository = new NewspaperRepository();
                _repository.DestroyNewspaper(id);
            }
            if(entityType == Type.Periodical)
            {
                PeriodicalRepository _repository = new PeriodicalRepository();
                _repository.DestroyPeriodical(id);
            }
        }

        public BaseEntity GetEntity(int id, Type entityType)
        {
            if (entityType == Type.Book)
            {
                BookRepository _repository = new BookRepository();
                return _repository.GetBook(id);
            }
            if (entityType == Type.Newspaper)
            {
                NewspaperRepository _repository = new NewspaperRepository();
                return _repository.GetNewspaper(id);
            }
            if (entityType == Type.Periodical)
            {
                PeriodicalRepository _repository = new PeriodicalRepository();
                return _repository.GetPeriodical(id);
            }
            return null;
        }

        public List<BaseEntity> GetCheckedEntitys()
        {
            var result = new List<BaseEntity>();
            foreach (BaseEntity entity in GetEntitys())
            {
                if (entity.IncludeToPage)
                {
                    result.Add(entity);
                }
            }
            return result;
        }
    }
}
