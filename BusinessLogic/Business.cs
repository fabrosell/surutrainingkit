using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Text;
using Suru.TrainingKit.Entities;
using System.Collections.Generic;

namespace Suru.TrainingKit.BusinessLogic
{
    public class Configuration
    {
        public static Dictionary<String, String> GetAvailableExams(String ExamFolder)
        {
            try
            {
                Dictionary<String, String> Exams = new Dictionary<String, String>();

                DirectoryInfo di = new DirectoryInfo(ExamFolder);

                StreamReader sr = null;
                XmlDocument xmlIn = new XmlDocument();
                XmlNode xmlRootNode;
                XmlNodeList xmlNodes;

                foreach (FileInfo fi in di.GetFiles("*.xml"))
                {
                    sr = new StreamReader(fi.FullName);
                    xmlIn.LoadXml(sr.ReadToEnd());
                    sr.Close();
                    xmlNodes = null;

                    xmlRootNode = xmlIn.DocumentElement;

                    xmlNodes = xmlRootNode.SelectNodes("//exam");

                    //Only add valid files
                    if (xmlNodes != null && xmlNodes.Count == 1)                    
                        Exams.Add(xmlNodes[0].Attributes["name"].Value, fi.FullName);                    
                }

                return Exams;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }

    /*
     
    public static Dictionary<String, String> ProcessXMLConfiguration(String xml_in)
        {
            XmlDocument xmlIn = new XmlDocument();
            xmlIn.LoadXml(xml_in);
            XmlNode xmlRootNode = xmlIn.DocumentElement;

            XmlNodeList xmlNodes;

            xmlNodes = xmlRootNode.SelectNodes("//key");

            Dictionary<String, String> ValueList = new Dictionary<String, String>();

            //Process the list of nodes. 
            foreach (XmlNode xmlNode in xmlNodes)
                ValueList.Add(xmlNode.Attributes["name"].Value, xmlNode.Attributes["value"].Value);


            return ValueList;
        }
     
     */
}
