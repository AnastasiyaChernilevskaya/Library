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
        public List<BaseEntity> GetChacked()
        {
            return _libraryRepository.GetCheckedEntitys();
        }

        public static byte[] SerializeToXml<T>(List<T> items)
        {
            XmlSerializer ser = new XmlSerializer(items.GetType());
            string result = string.Empty;

            using (MemoryStream memStream = new MemoryStream())
            {
                ser.Serialize(memStream, items);

                memStream.Position = 0;
                result = new StreamReader(memStream).ReadToEnd();
            }
            
            var memoryStream = new MemoryStream();
            TextWriter textWriter = new StreamWriter(memoryStream);
            textWriter.WriteLine(result);
            textWriter.Flush();
            byte[] bytesInStream = memoryStream.ToArray();
            memoryStream.Close();
            return bytesInStream;
        }

        public static T DeserializeFromXml<T>(string str)
        {

            var stringReader = new StringReader(str);
            var xmlTextReader = new XmlTextReader(stringReader);

            var ser = new XmlSerializer(typeof(T));
            var items = (T)ser.Deserialize(xmlTextReader);
            return items;
        }

    }
}