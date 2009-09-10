using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Configuration;
using System.ComponentModel;
using Suru.TrainingKit.Controls;
using Suru.TrainingKit.Entities;
using System.Collections.Generic;
using Suru.TrainingKit.BusinessLogic;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Suru.TrainingKit.UI
{
    public partial class frmMain : Form
    {
        #region Local Variables

        /// <summary>
        /// Form Status Enumeration (mainly to set control status)
        /// </summary>
        private enum FormStatus { WhitoutExam, ConfiguratingExam, TakingTest, TestEnded };

        private SortedDictionary<String, String> AvailableExams = null;
        private String ExamsDirectory = null;
        private FormStatus Status;

        Dictionary<String, Int32> LanguageDict = null;
        private List<String> TopicList = null;
        private Exam CurrentExam = null;
        private Boolean ResultsExported = false;

        #endregion

        #region Class Methods

        /// <summary>
        /// Enables / Disables form controls based on current status.
        /// </summary>
        /// <param name="StatusToSet">Status to set controls</param>
        private void SetControlStatus(FormStatus StatusToSet)
        {
            Status = StatusToSet;

            switch (Status)
            {
                case FormStatus.ConfiguratingExam:
                    tsmiAvailableExams.Enabled = true;
                    tsmiTestStatus.Enabled = true;
                    tsmiTestStatus.Text = "Start Test";
                    ctkConfig.Visible = true;
                    ctkConfig.LanguageDict = LanguageDict;
                    ctkConfig.TopicList = TopicList;
                    ctkConfig.RefreshContent();
                    ttkTest.Visible = false;
                    tsmiTestResult.Enabled = false;
                    break;
                case FormStatus.TakingTest:
                    tsmiAvailableExams.Enabled = false;
                    tsmiTestStatus.Enabled = true;
                    tsmiTestStatus.Text = "Stop Test";
                    ctkConfig.Visible = false;
                    ttkTest.Visible = true;
                    tsmiTestResult.Enabled = false;
                    break;
                case FormStatus.TestEnded:
                    tsmiAvailableExams.Enabled = true;
                    tsmiTestStatus.Enabled = true;
                    tsmiTestStatus.Text = "Restart Test";
                    ctkConfig.Visible = false;
                    ttkTest.Visible = true;
                    tsmiTestResult.Enabled = true;
                    break;
                case FormStatus.WhitoutExam:
                    tsmiAvailableExams.Enabled = true;
                    tsmiTestStatus.Enabled = false;
                    tsmiTestStatus.Text = "Start Test";
                    ctkConfig.Visible = false;
                    ttkTest.Visible = false;
                    tsmiTestResult.Enabled = false;
                    break;
            }
        }
            
        /// <summary>
        /// Loads Selected Configuration
        /// </summary>
        /// <param name="ConfigFileName">XML file containing Exam's configuration</param>
        private Boolean LoadConfiguration(String ConfigFileName)
        {
            if (File.Exists(ConfigFileName))
            {
                CurrentExam = ExamConfiguration.GetExamConfiguration(ConfigFileName, Settings.Default.MinorBracketSubstitution, Settings.Default.MayorBracketSubstitution, Settings.Default.UmpersandSubstitution);

                if (CurrentExam != null)
                {
                    if (TopicList == null)
                        TopicList = new List<String>();

                    TopicList.Clear();

                    if (LanguageDict == null)
                        LanguageDict = new Dictionary<String, Int32>();

                    LanguageDict.Clear();

                    foreach (KeyValuePair<String, Topics> kvp in CurrentExam.ExamTopics)
                    {
                        TopicList.Add(kvp.Key);

                        foreach (String s in kvp.Value.QuestionsPerLanguage.Keys)
                        {
                            if (!LanguageDict.ContainsKey(s))
                                LanguageDict.Add(s, 0);

                            LanguageDict[s] += kvp.Value.QuestionsPerLanguage[s].Count;
                        }
                    }

                    ResultsExported = false;

                    return true;
                }
                else
                {
                    MessageBox.Show("Cannot load configuration file " + ConfigFileName + ". Application must exit. Check trace.log file on application folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }
            }
            else
            {
                MessageBox.Show("Exam config file is missing (" + ConfigFileName + "). Please check it out.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return false;
        }

        /// <summary>
        /// Loads the TestTrainingKit object configurations, so it can take the test.
        /// </summary>
        private void LoadSelectedConfiguration()
        {
            //Yes, I know. This procedure has a lot of data structures and it's cumbersone.
            //I don't really cares that it be a beauty piece of code (but I will try to do things as best as a fast develop can)
            //I've tryed to comment it as much as possible to make easier mantain it

            #region Variables to use.

            //Must set these properties for the TestTrainingKit control
            Dictionary<Int16, String> DictQuestions = new Dictionary<Int16, String>();
            Dictionary<Int16, String> DictAnswers = new Dictionary<Int16, String>();
            Dictionary<Int16, String> DictAnnotations = new Dictionary<Int16, String>();
            Dictionary<Int16, Decimal> DictPoints = new Dictionary<Int16, Decimal>();

            Random RandGen = new Random();

            Dictionary<String, Int32> TopicQuestionsNumber = new Dictionary<String, Int32>();

            Int32 TotalQuestions = 0;

            //Dictionary of topic-name and question number's list
            Dictionary<String, List<Int16>> QuestionList = new Dictionary<String, List<Int16>>();

            #endregion

            #region Analize question data per Topic

            //Get number of questions per topic
            foreach (KeyValuePair<String, Topics> kvp in CurrentExam.ExamTopics)
            {
                //Only Process Selected Topics
                if (TopicList.Contains(kvp.Value.Name))
                {
                    if (kvp.Value.QuestionsPerLanguage.ContainsKey(ctkConfig.LanguageSelected))
                    {
                        TopicQuestionsNumber.Add(kvp.Value.Name, kvp.Value.QuestionsPerLanguage[ctkConfig.LanguageSelected].Count);
                        TotalQuestions += kvp.Value.QuestionsPerLanguage[ctkConfig.LanguageSelected].Count;

                        QuestionList.Add(kvp.Value.Name, new List<Int16>());

                        //Build list of questions per topic (to make easier randomize and limit its number)
                        foreach (Int16 Number in kvp.Value.QuestionsPerLanguage[ctkConfig.LanguageSelected].Keys)
                            QuestionList[kvp.Value.Name].Add(Number);
                    }
                }
            }

            #endregion

            #region Analize amount of questions to use

            //100% of the questions (1)
            Decimal QuestionAmount = 1;

            //Limit the number of questions
            if (ctkConfig.QuestionNumber != null && TotalQuestions > ctkConfig.QuestionNumber.Value)
                QuestionAmount = (Decimal)ctkConfig.QuestionNumber.Value / (Decimal)TotalQuestions;

            Int32 AddedQuestions = 0;
            Decimal AddedQuestionsPrecise = 0;

            List<KeyValuePair<String, Int16>> UnrandomizedQuestionList = new List<KeyValuePair<String, Int16>>();
            KeyValuePair<String, Int16> Pair;

            Boolean EndRandCicle;

            #endregion

            #region Select Random Questions from Topics (but still ordered by topic)

            //QuestionList is a dictionary that will contain:
            // - Only selected topics
            // - Only questions from the selected language
            // So, its only needed limit and randomize the questions in QuestionList.
            if (QuestionAmount != 1)
            {
                //This awful piece of code will be executed only if there will be less than the 100% of the questions
                foreach (KeyValuePair<String, List<Int16>> kvp in QuestionList)
                {
                    AddedQuestionsPrecise = (Decimal)QuestionAmount * (Decimal)TopicQuestionsNumber[kvp.Key];
                    AddedQuestionsPrecise = Math.Round(AddedQuestionsPrecise, 2, MidpointRounding.AwayFromZero);

                    //Here will go code to set the topic error truncation. This will be omitted first hoping rounding and decimal precision
                    //number will no need any correction (operations are perfect)

                    //AddedQuestions = (Int32)Math.Truncate(AddedQuestionsPrecise);
                    AddedQuestions = (Int32)Math.Round(AddedQuestionsPrecise, 0, MidpointRounding.AwayFromZero);

                    EndRandCicle = false;

                    //This cycle will generate AddedQuestions questions in random order to UnrandomizedQuestionList
                    while (!EndRandCicle)
                    {
                        //Generates a new random pair
                        Pair = new KeyValuePair<String, Int16>(kvp.Key, kvp.Value[RandGen.Next(kvp.Value.Count - 1)]);

                        //If pair is not in the list, add it
                        if (!UnrandomizedQuestionList.Contains(Pair))
                        {
                            UnrandomizedQuestionList.Add(Pair);
                            AddedQuestions--;
                        }
                        //(else keep cycling)

                        if (AddedQuestions == 0)
                            EndRandCicle = true;
                    }
                }
            }
            else
            {
                //Add all questions to list (as easy as two nested foreachs)
                foreach (KeyValuePair<String, List<Int16>> kvp in QuestionList)
                {
                    foreach (Int16 QuestNum in kvp.Value)
                        UnrandomizedQuestionList.Add(new KeyValuePair<String, Int16>(kvp.Key, QuestNum));
                }
            }

            #endregion

            #region Mix Randoms Questions and Topics (to get real random questions)

            List<KeyValuePair<String, Int16>> FinalQuestionList = new List<KeyValuePair<String, Int16>>();

            List<Int16> MixedOrder = new List<Int16>();
            Int16 RandomNumber;

            //Randomize list (generates a random order of items)
            for (Int32 i = 0; i < UnrandomizedQuestionList.Count; i++)
            {
                while (true)
                {
                    RandomNumber = (Int16)RandGen.Next(UnrandomizedQuestionList.Count);

                    if (!MixedOrder.Contains(RandomNumber))
                    {
                        MixedOrder.Add(RandomNumber);
                        break;
                    }
                }
            }

            //Traspass random question order
            foreach (Int16 i in MixedOrder)
                FinalQuestionList.Add(UnrandomizedQuestionList[i]);

            #endregion

            #region Load Data Structures into TestTrainingKit control

            //FinalQuestionList is a list that is randomized and will be used to generate
            //the data structures for the TestTrainingKit object. It contains the number of questions defined by user in exam's configuration.
            //It contains: list of pairs topic-question number. It may be randomized (this is not enforced with a dictionary)

            Question q;

            foreach (KeyValuePair<String, Int16> kvp in FinalQuestionList)
            {                
                q = CurrentExam.ExamTopics[kvp.Key].QuestionsPerLanguage[ctkConfig.LanguageSelected][kvp.Value];

                DictQuestions.Add(q.Number, q.Text);
                DictAnswers.Add(q.Number, q.Answer);
                DictAnnotations.Add(q.Number, q.Annotation);

                //Remember: questions are perfectly distributed by topic percentajes (if 1 topic 50%, 2 25% topic, then from 12 questions, 6 from topic 1, 3 from t2 and 3 from t3)
                DictPoints.Add(q.Number, (Decimal)100 / (Decimal)FinalQuestionList.Count);
            }

            ttkTest.ApprobationPercentage = CurrentExam.ApprobationPercentage;
            ttkTest.DictPoints = DictPoints;
            ttkTest.DictQuestions = DictQuestions;
            ttkTest.DictAnswers = DictAnswers;
            ttkTest.DictAnnotations = DictAnnotations;
            ttkTest.ExamLanguage = ctkConfig.LanguageSelected;

            ttkTest.ExamMinutes = ctkConfig.MinutesSelected;

            #endregion
        }

        #endregion

        #region Constructor & Event Handlers

        //Class Constructor
        public frmMain()
        {
            InitializeComponent();
        }

        //frmMain Load Event Handler
        private void frmMain_Load(Object sender, EventArgs e)
        {
            this.Text = "Suru Training Kit - v." + Application.ProductVersion;

            //Application accepts Relative and Absolute Path on Setting file. 
            if (Directory.Exists(Settings.Default.ExamDirectory))
                ExamsDirectory = Settings.Default.ExamDirectory;
            else
                ExamsDirectory = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), Settings.Default.ExamDirectory);

            AvailableExams = ExamConfiguration.GetAvailableExams(ExamsDirectory);

            if (AvailableExams != null)
            {
                if (AvailableExams.Count != 0)
                {
                    ToolStripMenuItem mi = null;

                    //Process Exams...
                    foreach (String ExamName in AvailableExams.Keys)
                    {
                        mi = new ToolStripMenuItem();
                        mi.Name = AvailableExams[ExamName];
                        mi.Text = ExamName;
                        mi.Click += new EventHandler(ToolStripMenuItem_Click);
                        tsmiAvailableExams.DropDownItems.Add(mi);
                    }
                }
                else
                {
                    MessageBox.Show("There are no exams to load on path ", "No Exams", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }
            }
            else
            {
                MessageBox.Show("Cannot load exams. Application will terminate.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }

            SetControlStatus(FormStatus.WhitoutExam);
        }

        //Generic Tool Strip Menu Item Click (fires when user clicks on exams)      
        private void ToolStripMenuItem_Click(Object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;

            //If Configuration can be loaded succesfully, proceed
            if (LoadConfiguration(tsmi.Name))
            {
                //Uncheck all menu items
                foreach (ToolStripMenuItem tsmi_exams in tsmiAvailableExams.DropDownItems)
                    tsmi_exams.Checked = false;

                //Check current menu item
                tsmi.Checked = true;

                this.Text = "Suru Training Kit - v." + Application.ProductVersion + " - " + tsmi.Text;

                SetControlStatus(FormStatus.ConfiguratingExam);
            }
            else
                SetControlStatus(FormStatus.WhitoutExam);
        }

        //tsmiTestStatus Event Handler
        private void tsmiTestStatus_Click(Object sender, EventArgs e)
        {
            //This menu item will have two functions: will start or stop a test.
            if (Status != FormStatus.TakingTest)
            {
                //Start Test.
                SetControlStatus(FormStatus.TakingTest);

                LoadSelectedConfiguration();

                ttkTest.StartTest();
            }
            else
            {
                ttkTest.Pause();

                //Ask user if it REALLY wants to stop test.
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to end the test?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    ttkTest_TestStopped(sender, e);

                ttkTest.Resume();
            }
        }

        //tsmiAbout Click Event Handler
        private void tsmiAbout_Click(Object sender, EventArgs e)
        {
            AboutBox frmAbout = new AboutBox();
            frmAbout.ShowDialog();
        }

        //ttkTeset Test Stopped Event Handler
        private void ttkTest_TestStopped(Object sender, EventArgs e)
        {
            SetControlStatus(FormStatus.TestEnded);

            ttkTest.StopTest();            
        }

        //frmMain Form Closing Event Handler
        private void frmMain_FormClosing(Object sender, FormClosingEventArgs e)
        {
            if (Status == FormStatus.TakingTest)
            {
                ttkTest.Pause();

                if (DialogResult.No == MessageBox.Show("Do you really want to exit and end current test?", "Test in progress", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    e.Cancel = true;
                    ttkTest.Resume();
                    return;
                }                
            }

            if (Status == FormStatus.TestEnded && !ResultsExported)
            {
                if (DialogResult.No == MessageBox.Show("If you leave now you won't be able to review your anwers. Do you really want to exit and lose current test?", "Unsaved Results Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        //tsmiTestResult Click Event Handler
        private void tsmiTestResult_Click(object sender, EventArgs e)
        {
            frmTestResult TestResult = new frmTestResult(ttkTest, CurrentExam, TopicList);

            TestResult.ShowDialog();

            ResultsExported = true;
        }

        #endregion
    }
}
