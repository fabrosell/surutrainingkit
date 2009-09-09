using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using Suru.TrainingKit.Controls;
using Suru.TrainingKit.Entities;
using System.Collections.Generic;

namespace Suru.TrainingKit.UI
{
    public partial class frmTestResult : Form
    {
        #region Local Variables

        private TestTrainingKit ttkTest = null;
        private Exam CurrentExam = null;

        #endregion

        #region Class Methods

        /// <summary>
        /// Builds the columns for the results.
        /// </summary>
        private void InitializeDataGridView()
        {
            #region Building DataGridView Columns

            dgvTestResult.Rows.Clear();

            dgvTestResult.Columns.Clear();

            DataGridViewTextBoxColumn ctxt;
            DataGridViewImageColumn cimg;

            ctxt = new DataGridViewTextBoxColumn();
            ctxt.Name = "[Topic]";
            ctxt.ReadOnly = true;
            ctxt.HeaderText = "Topic";
            ctxt.Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.46d);

            dgvTestResult.Columns.Add(ctxt);

            ctxt = new DataGridViewTextBoxColumn();
            ctxt.Name = "[Percentage]";
            ctxt.ReadOnly = true;
            ctxt.HeaderText = "% on Exam";
            ctxt.Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.07d);

            dgvTestResult.Columns.Add(ctxt);

            cimg = new DataGridViewImageColumn();
            cimg.Name = "[Image]";
            cimg.ReadOnly = true;
            cimg.HeaderText = String.Empty;
            cimg.Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.05d);

            dgvTestResult.Columns.Add(cimg);

            ctxt = new DataGridViewTextBoxColumn();
            ctxt.Name = "[Result]";
            ctxt.ReadOnly = true;
            ctxt.HeaderText = "Result";
            ctxt.Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.07);

            dgvTestResult.Columns.Add(ctxt);

            ctxt = new DataGridViewTextBoxColumn();
            ctxt.Name = "[Questions]";
            ctxt.ReadOnly = true;
            ctxt.HeaderText = "# of Questions";
            ctxt.Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.07);

            dgvTestResult.Columns.Add(ctxt);

            ctxt = new DataGridViewTextBoxColumn();
            ctxt.Name = "[QuestionsOK]";
            ctxt.ReadOnly = true;
            ctxt.HeaderText = "# OK";
            ctxt.Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.07);

            dgvTestResult.Columns.Add(ctxt);

            ctxt = new DataGridViewTextBoxColumn();
            ctxt.Name = "[QuestionsBad]";
            ctxt.ReadOnly = true;
            ctxt.HeaderText = "# Bad ";
            ctxt.Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.07);

            dgvTestResult.Columns.Add(ctxt);

            ctxt = new DataGridViewTextBoxColumn();
            ctxt.Name = "[QuestionsNotAnswered]";
            ctxt.ReadOnly = true;
            ctxt.HeaderText = "# of Empty";
            ctxt.Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.07);

            dgvTestResult.Columns.Add(ctxt);

            ctxt = new DataGridViewTextBoxColumn();
            ctxt.Name = "[PercentageTopic]";
            ctxt.ReadOnly = true;
            ctxt.HeaderText = "% Topic";
            ctxt.Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.07);

            dgvTestResult.Columns.Add(ctxt);

            #endregion
        }

        /// <summary>
        /// Loads initial data into form
        /// </summary>
        private void LoadData()
        {
            #region Exam Pass / Fail

            if (ttkTest.Points < CurrentExam.ApprobationPercentage)
            {
                if (ttkTest.Points != 0)                
                    lblTestResult.Text = "Fail (" + ttkTest.Points.ToString("##.##") + "%)";
                else
                    lblTestResult.Text = "Fail (0%)";

                lblTestResult.ForeColor = Color.Red;
            }
            else
            {
                lblTestResult.Text = "Pass (" + ttkTest.Points.ToString("##.##") + "%)";
                lblTestResult.ForeColor = Color.Green;
            }

            #endregion

            #region Timings

            lblStartTime.Text = ttkTest.StartTime.ToString("H:mm:ss");
            lblEndTime.Text = ttkTest.EndTime.ToString("H:mm:ss");

            TimeSpan ts = ttkTest.EndTime - ttkTest.StartTime;

            StringBuilder sb = new StringBuilder();

            sb.Append("Elapsed: ");

            if (ts.Days > 0)
            {
                sb.Append(ts.Days.ToString() + "d ");
                sb.Append(ts.Hours.ToString() + ":");
            }
            else
            {
                //Days are 0
                if (ts.Hours > 0)
                    sb.Append(ts.Hours.ToString() + ":");
            }

            sb.Append(ts.Minutes.ToString("00") + ":");
            sb.Append(ts.Seconds.ToString("00"));

            lblElapsed.Text = sb.ToString();            

            #endregion

            #region Miscelaneous board markers

            lblQuestions.Text = "Questions: " + ttkTest.DictQuestions.Count.ToString();

            #endregion

            #region Topics and Stats

            //TODO: end this region

            #endregion
        }

        #endregion

        #region Constructors and Event Handlers

        //Class Constructor
        public frmTestResult(TestTrainingKit ParentTestControl, Exam Test)
        {
            if (ParentTestControl == null)
                throw new ApplicationException("frmTestResult(ParentTestControl, Exam) : ParentTestControl cannot be null...");

            if (Test == null)
                throw new ApplicationException("frmTestResult(ParentTestControl, Exam) : Exam cannot be null...");


            ttkTest = ParentTestControl;
            CurrentExam = Test;

            InitializeComponent();
        }

        //frmTestResult Load Event Handler
        private void frmTestResult_Load(Object sender, EventArgs e)
        {
            InitializeDataGridView();

            LoadData();
        }

        #endregion
    }
}
