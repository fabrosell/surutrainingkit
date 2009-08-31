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

        private List<String> LanguageList = null;
        private List<String> TopicList = null;

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
                    ctkConfig.LanguageList = LanguageList;
                    ctkConfig.TopicList = TopicList;
                    ctkConfig.RefreshContent();
                    break;
                case FormStatus.TakingTest:
                    tsmiAvailableExams.Enabled = false;
                    tsmiTestStatus.Enabled = true;
                    tsmiTestStatus.Text = "Stop Test";
                    ctkConfig.Visible = false;
                    break;
                case FormStatus.TestEnded:
                    tsmiAvailableExams.Enabled = true;
                    tsmiTestStatus.Enabled = true;
                    tsmiTestStatus.Text = "Restart Test";
                    ctkConfig.Visible = false;
                    break;
                case FormStatus.WhitoutExam:
                    tsmiAvailableExams.Enabled = true;
                    tsmiTestStatus.Enabled = false;
                    tsmiTestStatus.Text = "Start Test";
                    ctkConfig.Visible = false;
                    break;
            }
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

            //Uncheck all menu items
            foreach (ToolStripMenuItem tsmi_exams in tsmiAvailableExams.DropDownItems)            
                tsmi_exams.Checked = false;

            //Check current menu item
            tsmi.Checked = true;
            
            this.Text = "Suru Training Kit - " + tsmi.Text;

            LanguageList = new List<String>();
            LanguageList.Add("C#");
            LanguageList.Add("C++");
            LanguageList.Add("ASM");

            TopicList = new List<String>();
            TopicList.Add("chan");
            TopicList.Add("chen");
            TopicList.Add("chin");
            TopicList.Add("chon");
            TopicList.Add("chun");
            TopicList.Add("asdf");

            SetControlStatus(FormStatus.ConfiguratingExam);
        }

        #endregion
    }
}
