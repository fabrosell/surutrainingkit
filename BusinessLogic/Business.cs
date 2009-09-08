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

        /// <summary>
        /// Loads an Exam configuration from a XML file.
        /// </summary>
        /// <param name="FileName">XML File to use.</param>
        /// <returns>Exam configuration, or null if an error ocurrs.</returns>
        public static Exam GetExamConfiguration(String FileName, String MinorBracketReplacement, String MayorBracketReplacement, String UmpersandReplacement)
        {
            try
            {
                Exam CurrentExam = new Exam();
                CurrentExam.ExamTopics = new Dictionary<String, Topics>();

                XmlDocument xmlIn = new XmlDocument();
                StreamReader sr = new StreamReader(FileName);
                xmlIn.LoadXml(sr.ReadToEnd());
                sr.Close();
                XmlNode xmlRootNode = xmlIn.DocumentElement;

                //Processing main exam
                //<exam name="Sample Exam" approbingpercentage="80">
                XmlNodeList xmlNodes = xmlRootNode.SelectNodes("//exam");

                if (xmlNodes.Count != 1)
                    throw new XmlException("More or less than 1 exam in xml file...");

                XmlNode xmlNode = xmlNodes[0];

                CurrentExam.Name = xmlNode.Attributes["name"].Value;
                CurrentExam.ApprobationPercentage = Int16.Parse(xmlNode.Attributes["approbingpercentage"].Value);                

                xmlNodes = xmlNode.SelectNodes("//topic");

                Topics t = null;
                Question q = null;
                XmlNodeList xmlQuestions;
                List<Int16> QuestionNumber = new List<Int16>();
                Decimal PercentageTotal = 0;
                Decimal Value;
                Int16 Number;
                StringBuilder sb;

                //Processing each topic
                //<topic name="Topic 1" topicexampercentage ="25">
                foreach (XmlNode xmlTopicNode in xmlNodes)
                {
                    t = new Topics();

                    t.Name = xmlTopicNode.Attributes["name"].Value;

                    if (!Decimal.TryParse(xmlTopicNode.Attributes["topicexampercentage"].Value, out Value))
                        throw new ApplicationException("Topic Percentage Incorrect Format (Percentage: " + xmlTopicNode.Attributes["topicexampercentage"].Value + ", Topic: " + t.Name + ")");

                    t.TopicValueOnExam = Value;
                    //t.TopicValueOnExam = Decimal.Parse(xmlTopicNode.Attributes["topicexampercentage"].Value);

                    t.QuestionsPerLanguage = new Dictionary<String, List<Question>>();

                    PercentageTotal += t.TopicValueOnExam;

                    xmlQuestions = xmlTopicNode.SelectNodes("child::question");
                   
                    //Processing each question
                    /* 		<question number="1" language="C#">
			                    <text>
				                    1 + 1
			                    </text>
			                    <answer>
				                    2
			                    </answer>
                                <annotation/>
                     */
                    foreach (XmlNode xmlQuestionNode in xmlQuestions)
                    {
                        q = new Question();

                        q.Language = xmlQuestionNode.Attributes["language"].Value.ToUpper();

                        if (!Int16.TryParse(xmlQuestionNode.Attributes["number"].Value, out Number))
                            throw new ApplicationException("Invalid Question Number (Value: " + xmlQuestionNode.Attributes["number"].Value + ", Topic: " + t.Name + ")");

                        q.Number = Number;
                        
                        //q.Number = Int16.Parse(xmlQuestionNode.Attributes["number"].Value);
                        
                        if (QuestionNumber.Contains(q.Number))
                            throw new ApplicationException("Question " + q.Number.ToString() + " already exist (topic: " + t.Name + ")");

                        QuestionNumber.Add(q.Number);

                        //New Line, Carriage Return and Tab ARE accepted as question formatting.
                        sb = new StringBuilder();
                        sb.Append(xmlQuestionNode.SelectSingleNode("child::text").InnerText);
                        sb.Replace(MinorBracketReplacement, "<");
                        sb.Replace(MayorBracketReplacement, ">");
                        sb.Replace(UmpersandReplacement, "&");                                                
                        q.Text = sb.ToString();

                        //New Line, Carriage Return and Tab are not accepted as answers.
                        sb = new StringBuilder();
                        sb.Append(xmlQuestionNode.SelectSingleNode("child::answer").InnerText);
                        sb.Replace(MinorBracketReplacement, "<");
                        sb.Replace(MayorBracketReplacement, ">");
                        sb.Replace(UmpersandReplacement, "&");
                        sb.Replace("\t", String.Empty);
                        sb.Replace("\n", String.Empty);
                        sb.Replace("\r", String.Empty);
                        q.Answer = sb.ToString();

                        //New Line, Carriage Return and Tab ARE accepted as question formatting.
                        sb = new StringBuilder();
                        sb.Append(xmlQuestionNode.SelectSingleNode("child::annotation").InnerText);
                        sb.Replace(MinorBracketReplacement, "<");
                        sb.Replace(MayorBracketReplacement, ">");
                        sb.Replace(UmpersandReplacement, "&");                        
                        q.Annotation = sb.ToString();

                        if (!t.QuestionsPerLanguage.ContainsKey(q.Language))
                            t.QuestionsPerLanguage.Add(q.Language, new List<Question>());

                        t.QuestionsPerLanguage[q.Language].Add(q);
                    }

                    if (CurrentExam.ExamTopics.ContainsKey(t.Name))
                        throw new ApplicationException("Topic \"" + t.Name + "\" is more than once on xml file...");

                    CurrentExam.ExamTopics.Add(t.Name, t);
                }

                if (Math.Round(PercentageTotal, 0) != 100)
                    throw new ApplicationException("The zero-decimal-rounded sum of the topic percentages is not 100 (value: " + Math.Round(PercentageTotal, 0).ToString() + ")");

                return CurrentExam;                
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
}
