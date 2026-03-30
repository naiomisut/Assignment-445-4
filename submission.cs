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
        // github page url 1
        public static string xmlURL = "https://raw.githubusercontent.com/naiomisut/Assignment-445-4/main/NationalParks.xml";
        // github page url 2
        public static string xsdURL = "https://raw.githubusercontent.com/naiomisut/Assignment-445-4/main/NationalParks.xsd";
        // github page url 3
        public static string xmlErrorURL = "https://raw.githubusercontent.com/naiomisut/Assignment-445-4/main/NationalParksErrors.xml";

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
            try
            {
                XmlSchemaSet schemas = new XmlSchemaSet();
                schemas.Add("", xsdUrl);
                XmlReaderSettings sets = new XmlReaderSettings();
                sets.schemas = schemas;
                sets.ValidationType = ValidationType.Schema;
                List<string> errors = new List<string>();
                sets.ValidationEventHandler += (sender, e) =>
                {
                    errors.add(e.Message);
                };
                using (XmlReader reader = XmlReader.Create(xmlURL, sets))
                {
                    while (reader.Read()) { }
                }
                return errors.Count == 0 ? "No errors are found" :
                string.Join("\n", errors);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }
        public static string Xml12Json(string xmlUrl)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlUrl);
                string jsontext = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented, true);
                return jsontext;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }

}