using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace Suru.TrainingKit.Controls
{
    public partial class TestTrainingKit : UserControl
    {
        #region Local Variables

        public event EventHandler TestStopped = null;

        public Dictionary<Int16, String> DictQuestions { get; set; }
        public Dictionary<Int16, String> DictAnswers { get; set; }
        public Dictionary<Int16, String> DictAnnotations { get; set; }
        public Dictionary<Int16, Decimal> DictPoints { get; set; }        
        public Dictionary<Int16, String> DictResponses { get; set; }
        public Nullable<Int16> ExamMinutes { get; set; }
        public List<Int16> QuestionsOK { get; set; }
        public List<Int16> QuestionsUnanswered { get; set; }
        public List<Int16> QuestionsBad { get; set; }
        public Int16 ApprobationPercentage { get; set; }
        public Decimal Points { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public String ExamLanguage { get; set; }
        
        private Nullable<Int16> CurrentQuestion = null;
        private Boolean AnswerWasShown = false;
        private String CurrentAnswerString = String.Empty;

        private Boolean PauseTimer = false;
        private Int16 HoursRemaining = 0;
        private Int16 MinutesRemaining = 0;
        private Int16 SecondsRemaining = 0;
        private Boolean Stopped;

        private const String QuestionColumn = "[Question]";
        private const String ImageColumn = "[Image]";

        #endregion

        #region Class Methods

        /// <summary>
        /// Pauses the timer.
        /// </summary>
        public void Pause()
        {
            PauseTimer = true;
        }

        /// <summary>
        /// Resumes the timer.
        /// </summary>
        public void Resume()
        {
            PauseTimer = false;
        }

        /// <summary>
        /// OnStop method event firer
        /// </summary>
        private void OnStop()
        {
            if (TestStopped != null)
                TestStopped(this, new EventArgs());
        }

        /// <summary>
        /// Starts the test.
        /// </summary>
        public void StartTest()
        {
            Stopped = false;

            Points = 0;
            lblExamResult.Text = String.Empty;

            DictResponses.Clear();            

            txtAnswer.ReadOnly = false;
            
            dgvQuestionList.Rows.Clear();

            if (DictQuestions == null)
            {
                txtQuestion.Text = "Test cannot be started because it lacks of configurations...";
                OnStop();
                return;
            }

            if (ExamMinutes != null)
            {
                SecondsRemaining = 0;
                
                HoursRemaining = (Int16)Math.Truncate((Decimal)ExamMinutes.Value / 60);
                MinutesRemaining = (Int16)(ExamMinutes.Value % 60);

                StopTimer = new Timer();
                StopTimer.Interval = 1000;
                StopTimer.Tick += new EventHandler(StopTimer_Tick);
                StopTimer.Enabled = true;

                ShowTime();
            }
            else
                lblRemainingTime.Text = String.Empty;


            #region Load Question List

            /*
            foreach (Int16 i in DictQuestions.Keys)
                lbQuestions.Items.Add(i);

            if (lbQuestions.Items.Count > 0)
                lbQuestions.SelectedIndex = 0;
            */

            //Load Question List into DataGridView
            if (dgvQuestionList.Columns.Count != 2)
            {
                dgvQuestionList.Columns.Clear();

                DataGridViewTextBoxColumn ctxt = new DataGridViewTextBoxColumn();
                ctxt.Name = QuestionColumn;
                ctxt.ReadOnly = true;
                ctxt.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                ctxt.Width = 27;

                dgvQuestionList.Columns.Add(ctxt);

                DataGridViewImageColumn cimg = new DataGridViewImageColumn();
                cimg.Name = ImageColumn;                
                cimg.ReadOnly = true;
                cimg.CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                cimg.Width = 20;

                dgvQuestionList.Columns.Add(cimg);
            }

            DataGridViewImageCell dimg;

            foreach (Int16 i in DictQuestions.Keys)
            {
                dgvQuestionList.Rows.Add();
                dgvQuestionList.Rows[dgvQuestionList.Rows.Count - 1].Cells[QuestionColumn].Value = i;
                dimg = (DataGridViewImageCell)dgvQuestionList.Rows[dgvQuestionList.Rows.Count - 1].Cells[ImageColumn];
                dimg.Value = Resources.NoImage;
            }

            dgvQuestionList.Rows[0].Selected = true;
            dgvQuestionList_SelectionChanged(null, null);

            #endregion

            StartTime = DateTime.Now;

            txtAnswer.Focus();
        }

        /// <summary>
        /// Stop the test.
        /// </summary>
        public void StopTest()
        {
            EndTime = DateTime.Now;

            //Commit previous changes
            dgvQuestionList_SelectionChanged(null, null);

            txtAnswer.ReadOnly = true;

            if (StopTimer != null)
                StopTimer.Enabled = false;

            QuestionsOK = new List<Int16>();
            QuestionsUnanswered = new List<Int16>();
            QuestionsBad = new List<Int16>();

            Points = 0;
            Int16 QuestionNumber;
            DataGridViewImageCell dimg;

            //Update all columns from List
            for (Int32 i = 0; i < dgvQuestionList.Rows.Count; i++)
            {
                if (dgvQuestionList[QuestionColumn, i].Value == null || dgvQuestionList[QuestionColumn, i].Value.ToString() == String.Empty)
                {
                    MessageBox.Show("Cannot check values due to internal error (question number was empty on question list)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                QuestionNumber = (Int16)dgvQuestionList[QuestionColumn, i].Value;
                dimg = (DataGridViewImageCell)dgvQuestionList[ImageColumn, i];

                if (DictResponses.ContainsKey(QuestionNumber) && DictResponses[QuestionNumber] != String.Empty)
                {
                    if (String.Compare(DictResponses[QuestionNumber], DictAnswers[QuestionNumber], true) == 0)
                    {
                        //User Answered the question, and values is OK. Add the Points.
                        if (DictPoints.ContainsKey(QuestionNumber))
                            Points += DictPoints[QuestionNumber];

                        //Question is OK
                        dimg.Value = Resources.ok;
                        QuestionsOK.Add(QuestionNumber);
                    }
                    else
                    {
                        //Question is not correct
                        dimg.Value = Resources.bad;
                        QuestionsBad.Add(QuestionNumber);
                    }
                }
                else
                {
                    //There are no answer
                    dimg.Value = Resources.nn;
                    QuestionsUnanswered.Add(QuestionNumber);
                }                
            }

            /*
            //Check all answers to match 
            foreach (KeyValuePair<Int16, String> kvp in DictAnswers)
            {                
                if (DictResponses.ContainsKey(kvp.Key))
                {
                    if (String.Compare(DictResponses[kvp.Key], DictAnswers[kvp.Key], true) == 0)
                    {
                        //User Answered the question, and values is OK. Add the Points.
                        if (DictPoints.ContainsKey(kvp.Key))
                            Points += DictPoints[kvp.Key];                        
                    }                                                
                }
            }
            */

            Points = Math.Round(Points, 2);

            //Only if exam was timed
            if (lblRemainingTime.Text != String.Empty)
            {
                lblRemainingTime.Text = "Remaining time: (stopped)";
                lblRemainingTime.ForeColor = Color.Black;
            }                
            
            if (Math.Round(Points, 2) < ApprobationPercentage)
            {
                if (Points != 0)
                    lblExamResult.Text = "Exam Fails (" + Points.ToString("##.##") + "%, " + ApprobationPercentage.ToString("00") + "% needed)";               
                else
                    lblExamResult.Text = "Exam Fails (0%, " + ApprobationPercentage.ToString("00") + "% needed)";

                lblExamResult.ForeColor = Color.Red;
            }
            else
            {
                lblExamResult.Text = "Exam Passed! (" + Points.ToString("##.##") + "%, " + ApprobationPercentage.ToString("00") + "% min)";

                lblExamResult.ForeColor = Color.Green;
            }

            Stopped = true;

            dgvQuestionList_SelectionChanged(null, null);
        }

        /// <summary>
        /// Check if current answer is true of false.
        /// </summary>
        public void CheckAnswer()
        {            
            if (dgvQuestionList.SelectedRows.Count == 0)
                return;

            if (dgvQuestionList.SelectedRows[0].Cells[QuestionColumn].Value == null)
                return;

            Int16 QuestionNumber = (Int16)dgvQuestionList.SelectedRows[0].Cells[QuestionColumn].Value;


            //Replacing tabs, new line and carriage returns characters
            StringBuilder sb = new StringBuilder();

            if (DictResponses.ContainsKey(QuestionNumber))
                sb.Append(DictResponses[QuestionNumber]);
            
            sb.Replace("\t", String.Empty);
            sb.Replace("\r", String.Empty);
            sb.Replace("\n", String.Empty);

            if (DictAnswers.ContainsKey(QuestionNumber))
            {
                if (String.Compare(sb.ToString(), DictAnswers[QuestionNumber], true) == 0)
                {
                    pbQuestionResult.Image = Resources.ok;
                    lblResultText.Text = "Answer is OK.";
                }
                else
                {
                    pbQuestionResult.Image = Resources.bad;
                    lblResultText.Text = "Answer is not correct.";
                }
            }
            else
            {
                pbQuestionResult.Image = Resources.nn;
                lblResultText.Text = "There are no answer.";
            }

            sb = new StringBuilder();
            sb.Append(txtAnswer.Text.Trim());
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("Answer is: ");
            sb.Append(Environment.NewLine);
            sb.Append(DictAnswers[QuestionNumber]);
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            if (DictAnnotations.ContainsKey(QuestionNumber) && DictAnnotations[QuestionNumber] != String.Empty)
            {
                sb.Append("Annotation: ");
                sb.Append(DictAnnotations[QuestionNumber]);
                sb.Append(Environment.NewLine);
            }

            CurrentAnswerString = sb.ToString();

            txtAnswer.Text = CurrentAnswerString;
        }

        /// <summary>
        /// Shows the Exam available time in control
        /// </summary>
        private void ShowTime()
        {
            lblRemainingTime.Text = "Remaining Time: ";

            if (HoursRemaining > 0)
                lblRemainingTime.Text += HoursRemaining.ToString() + ":";

            lblRemainingTime.Text += MinutesRemaining.ToString("00") + ":";
            lblRemainingTime.Text += SecondsRemaining.ToString("00");


            lblRemainingTime.ForeColor = Color.Green;

            if (HoursRemaining == 0)
            {
                if (MinutesRemaining < 10)
                {
                    if (MinutesRemaining < 1)
                    {
                        if (SecondsRemaining % 2 == 1)
                            lblRemainingTime.ForeColor = Color.Red;
                        else
                            lblRemainingTime.ForeColor = Color.Black;
                    }
                    else
                    {
                        lblRemainingTime.ForeColor = Color.Orange;
                    }
                }
            }
        }

        #endregion

        #region Constructors & Event Handlers

        //Class Constructor
        public TestTrainingKit()
        {
            InitializeComponent();

            lblResultText.Text = String.Empty;
            lblExamResult.Text = String.Empty;
            pbQuestionResult.Image = null;
            DictResponses = new Dictionary<Int16, String>();
        }

        //btnPrevious Click Event Handler
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (chkShowAnswer.Checked && !AnswerWasShown && !Stopped)
            {
                CheckAnswer();
                AnswerWasShown = true;
                return;
            }

            //Making this in a DataGridView is quite different than making it in the old ListBox
            Int32 Index = dgvQuestionList.SelectedRows[0].Index;

            dgvQuestionList.ClearSelection();

            if (Index == 0)            
                dgvQuestionList.Rows[dgvQuestionList.Rows.Count - 1].Selected = true;                            
            else            
                dgvQuestionList.Rows[Index - 1].Selected = true;
            
            /*
            if (lbQuestions.SelectedIndex == 0)
                lbQuestions.SelectedIndex = lbQuestions.Items.Count - 1;
            else
                lbQuestions.SelectedIndex--;
            */

            txtAnswer.Focus();
        }

        //btnNext Click Event Handler
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (chkShowAnswer.Checked && !AnswerWasShown && !Stopped)
            {
                CheckAnswer();
                AnswerWasShown = true;
                return;
            }

            //Making this in a DataGridView is quite different than making it in the old ListBox
            Int32 Index = dgvQuestionList.SelectedRows[0].Index;

            dgvQuestionList.ClearSelection();

            if (Index == dgvQuestionList.Rows.Count - 1)
                dgvQuestionList.Rows[0].Selected = true;
            else
                dgvQuestionList.Rows[Index + 1].Selected = true;

            /*
            if (lbQuestions.SelectedIndex == lbQuestions.Items.Count - 1)
                lbQuestions.SelectedIndex = 0;
            else
                lbQuestions.SelectedIndex++;
            */

            txtAnswer.Focus();
        }

        //btnShowAnswer Click Event Handler
        private void btnShowAnswer_Click(object sender, EventArgs e)
        {
            CheckAnswer();
            AnswerWasShown = true;
        }

        //StopTimer Tick Event Handler
        private void StopTimer_Tick(object sender, EventArgs e)
        {
            if (PauseTimer)
                return;

            if (SecondsRemaining == 0)
            {
                SecondsRemaining = 59;

                if (MinutesRemaining == 0)
                {
                    MinutesRemaining = 59;

                    if (HoursRemaining == 0)
                    {
                        StopTest();
                        return;
                    }
                    else
                        HoursRemaining--;
                }
                else
                    MinutesRemaining--;
            }
            else
                SecondsRemaining--;

            ShowTime();
        }

        //dgvQuestionList Selection Changed Event Handler
        private void dgvQuestionList_SelectionChanged(Object sender, EventArgs e)
        {
            //Int16 SelectedQuestion = (Int16)lbQuestions.SelectedItem;

            if (dgvQuestionList.SelectedRows.Count == 0)
                return;

            if (dgvQuestionList.SelectedRows[0].Cells[QuestionColumn].Value == null)
                return;

            Int16 SelectedQuestion = (Int16)dgvQuestionList.SelectedRows[0].Cells[QuestionColumn].Value;

            dgvQuestionList.CurrentCell = dgvQuestionList.SelectedRows[0].Cells[0];

            if (SelectedQuestion != -1)
            {
                //Save previous results, if current question is not null
                if (CurrentQuestion != null)
                {
                    if (!DictResponses.ContainsKey(CurrentQuestion.Value))
                        DictResponses.Add(CurrentQuestion.Value, String.Empty);

                    //Removes Annotation Text
                    if (CurrentAnswerString != String.Empty)
                        if (txtAnswer.Text.Trim() != String.Empty)
                            txtAnswer.Text = txtAnswer.Text.Replace(CurrentAnswerString, String.Empty);

                    if (!Stopped)
                        DictResponses[CurrentQuestion.Value] = txtAnswer.Text.Trim();
                }

                //Set current question
                CurrentQuestion = SelectedQuestion;

                txtQuestion.Text = DictQuestions[SelectedQuestion];

                lblQuestionNumber.Text = "Question " + SelectedQuestion.ToString() + " (" + (dgvQuestionList.SelectedRows[0].Index + 1).ToString() + "/" + dgvQuestionList.Rows.Count.ToString() + ")";

                if (DictResponses.ContainsKey(SelectedQuestion))
                    txtAnswer.Text = DictResponses[SelectedQuestion];
                else
                    txtAnswer.Text = String.Empty;

                if (Stopped)
                    CheckAnswer();

                //Resets label and icons
                pbQuestionResult.Image = null;
                lblResultText.Text = String.Empty;
                AnswerWasShown = false;                
            }
        }

        //TestTrainingKit (control) Key Down Event Handler
        private void TestTrainingKit_KeyDown(Object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
                btnNext_Click(null, null);

            if (e.KeyCode == Keys.Left)
                btnPrevious_Click(null, null);
        }

        //txtAnswer Click Event Handler
        private void txtAnswer_Click(Object sender, EventArgs e)
        {
            //This method will work only if the test is stopped
            //...and if there are more than 5 lines of text in it.
            if (Stopped)
            {
                Int16 SelectedQuestion = (Int16)dgvQuestionList.SelectedRows[0].Cells[QuestionColumn].Value;

                frmAnswerDetail frmAD = new frmAnswerDetail(SelectedQuestion, ExamLanguage, txtAnswer.Text);

                frmAD.ShowDialog();
            }
        }

        #endregion

    }
}
