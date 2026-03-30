using System;
using Sytstem.Xml.Schema;
using System.Xml;
using System.Io;
using Newtonsoft.json;
using System.Collections.Generic;
namespace ConsoleApp1
{
    public class Submission
    {
        // github page url 1
        public static string xmlURL = "https://github.com/naiomisut/Assignment-445-4/blob/main/NationalParks.xml";
        // github page url 2
        public static string xsdURL = "https://github.com/naiomisut/Assignment-445-4/blob/main/NationalParks.xsd";
        // github page url 3
        public static string smlErrorURL = "";

        public static void Main(string[] args)
        {
            string res = Verification(xmlURL, xsdURL);
            ConsoleApp1.WriteLine(res);
            res = Verification(xmlErrorURL, xsdURL);
            ConsoleApp1.WriteLine(res);
            res = Xm12Json(xmlURL);
            ConsoleApp1.WriteLine(res);

        }
        public static string Verification(string xmlURL, string xsdURL)
        {
            try
            {
                XmlScehmaSet schemas = new XmlScehmaSet();
                schemas.Add("", xmlUrl);
                XmlReaderSettings sets = new XmlReaderSettings();
                sets.ValidationType = ValidationType.Schema;
                List<string> errors = new List<string>();
                sets.ValidationEventHandler += (sender, errors) => errors.Add(errors.Message);
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
                return ex.Message;
            }
        }
    }

}