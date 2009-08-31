using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Configuration;
using Suru.TrainingKit.Entities;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Suru.TrainingKit.BusinessLogic
{
    public class ExamConfiguration
    {
        /// <summary>
        /// Loads the Available Exams and its xml configuration files. Will ignore xml with a different format.
        /// </summary>
        /// <param name="ExamFolder">Folder to check exam's xml.</param>
        /// <returns>Dictionary with pairs ExamName and xml full file path.</returns>
        public static SortedDictionary<String, String> GetAvailableExams(String ExamFolder)
        {
            try
            {
                SortedDictionary<String, String> Exams = new SortedDictionary<String, String>();                

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

                    //Only add valid files, and distinct exam names
                    if (xmlNodes != null && xmlNodes.Count == 1)    
                        if (!Exams.ContainsKey(xmlNodes[0].Attributes["name"].Value))
                            Exams.Add(xmlNodes[0].Attributes["name"].Value, fi.FullName);                    
                }

                return Exams;
            }
            catch (Exception ex)
            {
                Boolean rethrow = ExceptionPolicy.HandleException(ex, ConfigurationManager.AppSettings.Get("DefaultPolicy"));

                if (rethrow)
                    throw ex;

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
