using System.Collections.Generic;
using System.Linq;


namespace Library.Data.Repositories
{
    public class LibraryRepository : BaseRepository
    {
        private Context _context;
        string _libraryType;

        public LibraryRepository()
        {
            _context = new Context();
            _libraryType = Type.Other.ToString();
        }

        public List<BaseEntity> GetEntitys()
        {
            var result = new List<BaseEntity>();

            result = _context.LibraryEntitys.ToList();

            return result;
        }

        public void CreateEntity(BaseEntity libraryEntity)
        {
            var entity = new BaseEntity(); //ask Lesha!

            entity.Name = libraryEntity.Name;
            entity.Publisher = libraryEntity.Publisher;
            entity.IncludeToPage = libraryEntity.IncludeToPage;

            entity.LibraryType = libraryEntity.LibraryType;

            _context.LibraryEntitys.Add(entity);
            _context.SaveChanges();
        }

        public void UpdateEntity(BaseEntity libraryEntity)
        {
            var entity = new BaseEntity();

            entity.Id = libraryEntity.Id;
            entity.Name = libraryEntity.Name;
            entity.Publisher = libraryEntity.Publisher; 
            entity.IncludeToPage = libraryEntity.IncludeToPage;

            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public void DestroyEntity(int id)
        {
            _context.LibraryEntitys.Remove(GetEntity(id));
            _context.SaveChanges();
        }

        public BaseEntity GetEntity(int id)
        {
            return GetEntitys().FirstOrDefault(p => p.Id == id);
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
