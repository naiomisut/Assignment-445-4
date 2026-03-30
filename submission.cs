using System;
using System.Xml.Schema;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class Submission
    {
        public static string xmlURL = "RAW_XML_URL";
        public static string xmlErrorURL = "RAW_ERROR_XML_URL";
        public static string xsdURL = "RAW_XSD_URL";

        public static void Main(string[] args)
        {
            string res = Verification(xmlURL, xsdURL);
            Console.WriteLine(res);

            res = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(res);

            res = Xml2Json(xmlURL);
            Console.WriteLine(res);
        }

        public static string Verification(string xmlURL, string xsdURL)
        {
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add("", xsdURL);

            XmlReaderSettings sets = new XmlReaderSettings();
            sets.ValidationType = ValidationType.Schema;
            sets.Schemas = schemas;

            List<string> errors = new List<string>();

            sets.ValidationEventHandler += (sender, e) =>
            {
                errors.Add(e.Message);
            };

            using (XmlReader reader = XmlReader.Create(xmlURL, sets))
            {
                while (reader.Read()) { }
            }

            return errors.Count == 0 ? "No errors are found" : string.Join("\n", errors);
        }

        public static string Xml2Json(string xmlUrl)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlUrl);

            string jsonText = JsonConvert.SerializeXmlNode(doc);
            return jsonText;
        }
    }
}