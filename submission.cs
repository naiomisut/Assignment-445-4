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
        public static string xmlURL = "https://raw.githubusercontent.com/naiomisut/Assignment-445-4/refs/heads/main/NationalParks.xml";
        public static string xmlErrorURL = "https://raw.githubusercontent.com/naiomisut/Assignment-445-4/refs/heads/main/NationalParksErrors.xml";
        public static string xsdURL = "https://raw.githubusercontent.com/naiomisut/Assignment-445-4/refs/heads/main/NationalParks.xsd";

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