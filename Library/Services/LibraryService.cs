using System;
using System.Collections.Generic;
using Library.Data;
using Library.Data.Repositories;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Library.Services
{
    public class LibraryService
    {
        private LibraryRepository _libraryRepository;
        public LibraryService()
        {
            _libraryRepository = new LibraryRepository();
        }

        public List<BaseEntity> GetLibrary()
        {
            return _libraryRepository.GetEntitys();
        }

        public void CreateLibrary(BaseEntity baseEntity)
        {
            _libraryRepository.CreateEntity(baseEntity);
        }

        public void UpdateLibrary(BaseEntity baseEntity)
        {
            _libraryRepository.UpdateEntity(baseEntity);
        }

        public void DestroyLibraryItem(int id)
        {
            _libraryRepository.DestroyEntity(id);
        }

        public BaseEntity GetLibrary(int id)
        {
            return _libraryRepository.GetEntity(id);
        }

        //__________________________________________________________
    }
}