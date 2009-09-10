using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
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
        private List<String> TopicList = null;

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

            cimg = new DataGridViewImageColumn();
            cimg.Name = "[Image]";
            cimg.ReadOnly = true;
            cimg.HeaderText = String.Empty;
            //cimg.Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.05d);

            dgvTestResult.Columns.Add(cimg);

            ctxt = new DataGridViewTextBoxColumn();
            ctxt.Name = "[Topic]";
            ctxt.ReadOnly = true;
            ctxt.HeaderText = "Topic";
            //ctxt.Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.46d);

            dgvTestResult.Columns.Add(ctxt);

            ctxt = new DataGridViewTextBoxColumn();
            ctxt.Name = "[Percentage]";
            ctxt.ReadOnly = true;
            ctxt.HeaderText = "% on Exam";
            ctxt.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //ctxt.Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.07d);

            dgvTestResult.Columns.Add(ctxt);

            ctxt = new DataGridViewTextBoxColumn();
            ctxt.Name = "[PercentageTopic]";
            ctxt.ReadOnly = true;
            ctxt.HeaderText = "% Topic";
            ctxt.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //ctxt.Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.07);

            dgvTestResult.Columns.Add(ctxt);

            ctxt = new DataGridViewTextBoxColumn();
            ctxt.Name = "[Result]";
            ctxt.ReadOnly = true;
            ctxt.HeaderText = "Result";
            ctxt.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //ctxt.Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.07);

            dgvTestResult.Columns.Add(ctxt);

            ctxt = new DataGridViewTextBoxColumn();
            ctxt.Name = "[Questions]";
            ctxt.ReadOnly = true;
            ctxt.HeaderText = "# of Questions";
            ctxt.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //ctxt.Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.07);

            dgvTestResult.Columns.Add(ctxt);

            ctxt = new DataGridViewTextBoxColumn();
            ctxt.Name = "[QuestionsOK]";
            ctxt.ReadOnly = true;
            ctxt.HeaderText = "# OK";
            ctxt.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //ctxt.Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.07);

            dgvTestResult.Columns.Add(ctxt);

            ctxt = new DataGridViewTextBoxColumn();
            ctxt.Name = "[QuestionsBad]";
            ctxt.ReadOnly = true;
            ctxt.HeaderText = "# Bad ";
            ctxt.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //ctxt.Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.07);

            dgvTestResult.Columns.Add(ctxt);

            ctxt = new DataGridViewTextBoxColumn();
            ctxt.Name = "[QuestionsNotAnswered]";
            ctxt.ReadOnly = true;
            ctxt.HeaderText = "# of Empty";
            ctxt.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //ctxt.Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.07);

            dgvTestResult.Columns.Add(ctxt);

            dgvTestResult_Resize(null, null);

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

            Topics t;
            Int32 QuestionsOK, QuestionsBad, QuestionsUnanswered;
            Decimal TargetPercentage;


            //Complete each Topic with his data
            foreach (String sTopic in TopicList)
            {
                dgvTestResult.Rows.Add();

                dgvTestResult.Rows[dgvTestResult.Rows.Count - 1].Cells["[Topic]"].Value = sTopic;

                if (!CurrentExam.ExamTopics.ContainsKey(sTopic))
                    throw new ApplicationException("Exam " + CurrentExam.Name + " does not contain topic " + sTopic);

                t = CurrentExam.ExamTopics[sTopic];

                dgvTestResult.Rows[dgvTestResult.Rows.Count - 1].Cells["[Percentage]"].Value = t.TopicValueOnExam.ToString() + "%";

                dgvTestResult.Rows[dgvTestResult.Rows.Count - 1].Cells["[Questions]"].Value = t.QuestionsPerLanguage[ttkTest.ExamLanguage].Count;

                //Calculate Topic Hits
                QuestionsOK = 0;
                QuestionsBad = 0;
                QuestionsUnanswered = 0;
                TargetPercentage = 0;

                foreach (Int16 i in ttkTest.QuestionsOK)
                    if (t.QuestionsPerLanguage[ttkTest.ExamLanguage].ContainsKey(i))
                        QuestionsOK++;

                foreach (Int16 i in ttkTest.QuestionsBad)
                    if (t.QuestionsPerLanguage[ttkTest.ExamLanguage].ContainsKey(i))
                        QuestionsBad++;

                foreach (Int16 i in ttkTest.QuestionsUnanswered)
                    if (t.QuestionsPerLanguage[ttkTest.ExamLanguage].ContainsKey(i))
                        QuestionsUnanswered++;

                dgvTestResult.Rows[dgvTestResult.Rows.Count - 1].Cells["[QuestionsOK]"].Value = QuestionsOK;
                dgvTestResult.Rows[dgvTestResult.Rows.Count - 1].Cells["[QuestionsBad]"].Value = QuestionsBad;
                dgvTestResult.Rows[dgvTestResult.Rows.Count - 1].Cells["[QuestionsNotAnswered]"].Value = QuestionsUnanswered;

                TargetPercentage = (Decimal)QuestionsOK / (Decimal)t.QuestionsPerLanguage[ttkTest.ExamLanguage].Count;
                TargetPercentage *= 100;

                TargetPercentage = Math.Round(TargetPercentage, 2, MidpointRounding.AwayFromZero);

                if (TargetPercentage == 0)
                    dgvTestResult.Rows[dgvTestResult.Rows.Count - 1].Cells["[PercentageTopic]"].Value = "0%";
                else
                    dgvTestResult.Rows[dgvTestResult.Rows.Count - 1].Cells["[PercentageTopic]"].Value = TargetPercentage.ToString("##.##") + "%";

                //Analizes benchmarks topic per topic
                if (TargetPercentage > CurrentExam.ApprobationPercentage)
                {
                    dgvTestResult.Rows[dgvTestResult.Rows.Count - 1].Cells["[Image]"].Value = Properties.Resources.ok;
                    dgvTestResult.Rows[dgvTestResult.Rows.Count - 1].Cells["[Result]"].Value = "Pass";
                }
                else
                {
                    dgvTestResult.Rows[dgvTestResult.Rows.Count - 1].Cells["[Image]"].Value = Properties.Resources.bad;
                    dgvTestResult.Rows[dgvTestResult.Rows.Count - 1].Cells["[Result]"].Value = "Fail";
                }
            }

            #endregion
        }

        #endregion

        #region Constructors and Event Handlers

        //Class Constructor
        public frmTestResult(TestTrainingKit ParentTestControl, Exam Test, List<String> lTopicList)
        {
            if (ParentTestControl == null)
                throw new ApplicationException("frmTestResult(ParentTestControl, Exam, lTopicList) : ParentTestControl cannot be null...");

            if (Test == null)
                throw new ApplicationException("frmTestResult(ParentTestControl, Exam, lTopicList) : Exam cannot be null...");

            if (lTopicList == null)
                throw new ApplicationException("frmTestResult(ParentTestControl, Exam, lTopicList) : lTopicList cannot be null...");

            if (lTopicList.Count == 0)
                throw new ApplicationException("frmTestResult(ParentTestControl, Exam, lTopicList) : lTopicList cannot be empty...");

            ttkTest = ParentTestControl;
            CurrentExam = Test;
            TopicList = lTopicList;

            InitializeComponent();
        }

        //frmTestResult Load Event Handler
        private void frmTestResult_Load(Object sender, EventArgs e)
        {
            InitializeDataGridView();

            LoadData();

            this.Text = "Exams Results for Exam: " + CurrentExam.Name;
        }

        //btnExit Click Event Handler
        private void btnExit_Click(Object sender, EventArgs e)
        {
            this.Close();
        }

        //btnExportResults Click Event Handler
        private void btnExportResults_Click(Object sender, EventArgs e)
        {                        
            SaveFileDialog sfd = new SaveFileDialog();

            //File Name Format: yyyyMMdd_hhmmss_Exam.txt
            String FileName = DateTime.Now.Year.ToString() + 
                              DateTime.Now.Month.ToString("00") + 
                              DateTime.Now.Day.ToString("00") +
                              "_" +
                              DateTime.Now.Hour.ToString("00") +
                              DateTime.Now.Minute.ToString("00") +
                              DateTime.Now.Second.ToString("00") + 
                              "_Exam.txt";

            sfd.FileName = FileName;

            if (DialogResult.Cancel != sfd.ShowDialog())
            {
                FileName = sfd.FileName;                

                if (File.Exists(FileName))
                    File.Delete(FileName);

                StreamWriter sw = new StreamWriter(FileName);
                sw.AutoFlush = true;

                #region Header

                sw.WriteLine("Exam " + CurrentExam.Name);
                sw.WriteLine("Approbation Percentage: " + CurrentExam.ApprobationPercentage.ToString() + "%");
                sw.WriteLine();
                sw.WriteLine(lblExamResult.Text + " " + lblTestResult.Text);
                sw.WriteLine();
                sw.WriteLine(lblStartTimeCaption.Text + " \t" + lblStartTime.Text);
                sw.WriteLine(lblEndTimeCaption.Text + " \t" + lblEndTime.Text);
                sw.WriteLine(lblElapsed.Text);
                sw.WriteLine();
                sw.WriteLine(lblQuestions.Text);
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();

                #endregion

                #region Topics Detail

                foreach (DataGridViewRow dr in dgvTestResult.Rows)
                {
                    sw.WriteLine("Topic: " + dr.Cells["[Topic]"].Value.ToString());
                    sw.WriteLine();
                    sw.WriteLine("Size on exam: " + dr.Cells["[Percentage]"].Value.ToString());
                    sw.WriteLine("Result: " + dr.Cells["[Result]"].Value.ToString());
                    sw.WriteLine("Topic Questions Hit: " + dr.Cells["[PercentageTopic]"].Value.ToString());
                    sw.WriteLine();
                    sw.WriteLine("Total Questions: \t" + dr.Cells["[Questions]"].Value.ToString());
                    sw.WriteLine("Correct Questions: \t" + dr.Cells["[QuestionsOK]"].Value.ToString());
                    sw.WriteLine("Incorrect Questions: \t" + dr.Cells["[QuestionsBad]"].Value.ToString());
                    sw.WriteLine("Unanswered Questions: \t" + dr.Cells["[QuestionsNotAnswered]"].Value.ToString());
                    sw.WriteLine();
                    sw.WriteLine();
                }

                #endregion

                #region Question Detail

                sw.WriteLine("Question Detail: ");
                sw.WriteLine();

                foreach (Int16 i in ttkTest.DictQuestions.Keys)                
                {
                    sw.WriteLine("Question: " + i.ToString());
                    sw.WriteLine();
                    sw.WriteLine(ttkTest.DictQuestions[i]);
                    sw.WriteLine();
                    sw.WriteLine("Your Answer: ");

                    if (ttkTest.DictResponses.ContainsKey(i))
                        sw.WriteLine(ttkTest.DictResponses[i]);
                    else
                        sw.WriteLine("(none answer)");

                    sw.WriteLine();
                    sw.WriteLine("Correct Answer: ");
                    sw.WriteLine(ttkTest.DictAnswers[i]);
                    sw.WriteLine();

                    if (ttkTest.DictAnnotations.ContainsKey(i) && ttkTest.DictAnnotations[i] != String.Empty)
                    {
                        sw.WriteLine("Question Annotation:");
                        sw.WriteLine(ttkTest.DictAnnotations[i]);
                        sw.WriteLine();
                    }

                    sw.WriteLine();
                }

                #endregion

                sw.WriteLine();
                sw.WriteLine("Generated by Suru Training Kit on " + DateTime.Now.ToString());

                sw.Close();

                Process.Start(FileName);
            }
        }

        //dgvTestResult Resize Event Handler
        private void dgvTestResult_Resize(Object sender, EventArgs e)
        {
            //Resize the columns
            dgvTestResult.Columns["[Image]"].Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.05d);
            dgvTestResult.Columns["[Topic]"].Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.46d);
            dgvTestResult.Columns["[Percentage]"].Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.07d);
            dgvTestResult.Columns["[PercentageTopic]"].Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.07d);
            dgvTestResult.Columns["[Result]"].Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.07d);
            dgvTestResult.Columns["[Questions]"].Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.07d);
            dgvTestResult.Columns["[QuestionsOK]"].Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.07d);
            dgvTestResult.Columns["[QuestionsBad]"].Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.07d);
            dgvTestResult.Columns["[QuestionsNotAnswered]"].Width = (Int32)Math.Truncate(dgvTestResult.Width * 0.07d);
        }


        #endregion
    }
}
