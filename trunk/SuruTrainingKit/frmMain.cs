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

        Dictionary<String, Int16> LanguageDict = null;
        private List<String> TopicList = null;
        private Exam CurrentExam = null;

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
                    break;
                case FormStatus.TakingTest:
                    tsmiAvailableExams.Enabled = false;
                    tsmiTestStatus.Enabled = true;
                    tsmiTestStatus.Text = "Stop Test";
                    ctkConfig.Visible = false;
                    ttkTest.Visible = true;
                    break;
                case FormStatus.TestEnded:
                    tsmiAvailableExams.Enabled = true;
                    tsmiTestStatus.Enabled = true;
                    tsmiTestStatus.Text = "Restart Test";
                    ctkConfig.Visible = false;
                    ttkTest.Visible = true;
                    break;
                case FormStatus.WhitoutExam:
                    tsmiAvailableExams.Enabled = true;
                    tsmiTestStatus.Enabled = false;
                    tsmiTestStatus.Text = "Start Test";
                    ctkConfig.Visible = false;
                    ttkTest.Visible = false;
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
                CurrentExam = ExamConfiguration.GetExamConfiguration(ConfigFileName, Settings.Default.MinorBracketSubstitution, Settings.Default.MayorBracketSubstitution);

                if (CurrentExam != null)
                {
                    if (TopicList == null)
                        TopicList = new List<String>();

                    TopicList.Clear();

                    if (LanguageDict == null)
                        LanguageDict = new Dictionary<String, Int16>();

                    LanguageDict.Clear();

                    foreach (KeyValuePair<String, Topics> kvp in CurrentExam.ExamTopics)
                    {
                        TopicList.Add(kvp.Key);

                        foreach (String s in kvp.Value.QuestionsPerLanguage.Keys)
                        {
                            if (!LanguageDict.ContainsKey(s))
                                LanguageDict.Add(s, 0);

                            LanguageDict[s]++;
                        }
                    }

                    return true;
                }
                else
                {
                    MessageBox.Show("A problem happened with " + ConfigFileName + ". Application must exit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();                    
                }
            }
            else
            {
                MessageBox.Show("Exam config file is missing (" + ConfigFileName + "). Please check it out.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);                
            }

            return false;
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

                this.Text = "Suru Training Kit - " + tsmi.Text;

                SetControlStatus(FormStatus.ConfiguratingExam);
            }
            else
                SetControlStatus(FormStatus.WhitoutExam);                       
        }

        //tsmiTestStatus Event Handler
        private void tsmiTestStatus_Click(object sender, EventArgs e)
        {
            //This menu item will have two functions: will start or stop a test.
            if (Status == FormStatus.TakingTest)
            {
                //Ask user if it REALLY wants to stop test.
                if (DialogResult.Yes == MessageBox.Show("Are you sure you want to end the test?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                    ttkTest_TestStopped(sender, e);                
            }
            else
            {
                //Start Test.
                SetControlStatus(FormStatus.TakingTest);

                ttkTest.StartTest();
                //FALTAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
            }
        }

        //tsmiAbout Click Event Handler
        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            AboutBox frmAbout = new AboutBox();
            frmAbout.ShowDialog();
        }

        //ttkTeset Test Stopped Event Handler
        private void ttkTest_TestStopped(object sender, EventArgs e)
        {
            SetControlStatus(FormStatus.TestEnded);

            ttkTest.StopTest();

            //FALTAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
        }


        #endregion
    }
}
