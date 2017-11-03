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

        public void UpdateLibrary(int id, Data.Type entityType)
        {
            _libraryRepository.UpdateEntityChack(id, entityType);
        }

        public void DestroyLibraryItem(int id, Data.Type entityType)
        {
            _libraryRepository.DestroyEntity(id, entityType);
        }
        public List<T> GetChacked<T>()
        {
            return _libraryRepository.GetCheckedEntitys();
        }

        public string SerializeToXml<T>(List<T> entitys)
        {
            XmlSerializer ser = new XmlSerializer(entitys.GetType());
            string result = string.Empty;

            using (MemoryStream memStream = new MemoryStream())
            {
                ser.Serialize(memStream, entitys);

                memStream.Position = 0;
                result = new StreamReader(memStream).ReadToEnd();
            }
            return result;
        }

    }
}