using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Library.Services
{
    public class FileService
    {
        public void ExportXML(string BookId)
        {


            XmlDocument doc = new XmlDocument();

            // XML declaration
            XmlNode declaration = doc.CreateNode(XmlNodeType.XmlDeclaration, null, null);
            doc.AppendChild(declaration);

            // Root element: Catalog
            XmlElement root = doc.CreateElement("Catalog");
            doc.AppendChild(root);

            // Sub-element: srsapiversion of root
            XmlElement book = doc.CreateElement("book");
            root.AppendChild(book);


            // Sub-element: srsapiversion of root
            XmlElement bookname = doc.CreateElement("bookname");
            book.InnerText = "2 States";
            root.AppendChild(bookname);

            // Sub-element: id of root
            XmlElement id = doc.CreateElement("id");
            id.InnerText = "70-515";
            root.AppendChild(id);

            // Sub-element: author of root
            XmlElement author = doc.CreateElement("author");
            author.InnerText = "Chetan Bhagat";
            //Attribute age of author
            XmlAttribute age = doc.CreateAttribute("age");
            age.Value = "43";
            author.Attributes.Append(age);
            root.AppendChild(author);
            
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, System.Text.Encoding.UTF8);

            doc.WriteTo(writer);
            writer.Flush();
            Response.Clear();
            byte[] byteArray = stream.ToArray();
            Response.AppendHeader("Content-Disposition", "filename=Books.xml");
            Response.AppendHeader("Content-Length", byteArray.Length.ToString());
            Response.ContentType = "application/octet-stream";
            Response.BinaryWrite(byteArray);
            writer.Close();

        }
    }
}