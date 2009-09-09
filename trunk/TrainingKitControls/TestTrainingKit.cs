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
        public Boolean ResultsExported { get; set; }        
        public Nullable<Int16> ExamMinutes { get; set; }
        public List<Int16> QuestionsOK { get; set; }
        public Int16 ApprobationPercentage { get; set; }
        public Decimal Points { get; set; }

        private Dictionary<Int16, String> DictResponses = null;
        private Nullable<Int16> CurrentQuestion = null;
        private Boolean AnswerWasShown = false;
        private String CurrentAnswerString = String.Empty;

        private Boolean PauseTimer = false;
        private Int16 HoursRemaining = 0;
        private Int16 MinutesRemaining = 0;
        private Int16 SecondsRemaining = 0;

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
            Points = 0;
            lblExamResult.Text = String.Empty;

            DictResponses.Clear();
            ResultsExported = false;

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

            #endregion

            txtAnswer.Focus();
        }

        /// <summary>
        /// Stop the test.
        /// </summary>
        public void StopTest()
        {
            //Commit previous changes
            dgvQuestionList_SelectionChanged(null, null);

            txtAnswer.ReadOnly = true;

            if (StopTimer != null)
                StopTimer.Enabled = false;

            QuestionsOK = new List<Int16>();

            Points = 0;

            //Check all answer to match 
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

            Points = Math.Round(Points, 2);                       
            
            if (Math.Round(Points, 2) < ApprobationPercentage)
            {
                if (Points != 0)
                    lblExamResult.Text = "Exam Fails (" + Points.ToString("##.##") + "% ," + ApprobationPercentage.ToString("00") + "% needed)";               
                else
                    lblExamResult.Text = "Exam Fails (0%, " + ApprobationPercentage.ToString("00") + "% needed)";

                lblExamResult.ForeColor = Color.Red;
            }
            else
            {
                lblExamResult.Text = "Exam Passed! (" + Points.ToString("##.##") + "% ," + ApprobationPercentage.ToString("00") + "% min)";

                lblExamResult.ForeColor = Color.Green;
            }                
        }

        /// <summary>
        /// Check if current answer is true of false.
        /// </summary>
        public void CheckAnswer()
        {
            //Int16 QuestionNumber = (Int16)lbQuestions.SelectedItem;

            if (dgvQuestionList.SelectedRows.Count == 0)
                return;

            if (dgvQuestionList.SelectedRows[0].Cells[QuestionColumn].Value == null)
                return;

            Int16 QuestionNumber = (Int16)dgvQuestionList.SelectedRows[0].Cells[QuestionColumn].Value;


            //Replacing tabs, new line and carriage returns characters
            StringBuilder sb = new StringBuilder();
            sb.Append(txtAnswer.Text.Trim());
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

            CurrentAnswerString = DictAnswers[QuestionNumber];

            txtAnswer.Text = txtAnswer.Text.Trim();
            txtAnswer.Text += Environment.NewLine + Environment.NewLine + CurrentAnswerString;

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
            if (chkShowAnswer.Checked && !AnswerWasShown)
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
            if (chkShowAnswer.Checked && !AnswerWasShown)
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

                    DictResponses[CurrentQuestion.Value] = txtAnswer.Text.Trim();
                }

                //Set current question
                CurrentQuestion = SelectedQuestion;

                txtQuestion.Text = DictQuestions[SelectedQuestion];

                if (DictResponses.ContainsKey(SelectedQuestion))
                    txtAnswer.Text = DictResponses[SelectedQuestion];
                else
                    txtAnswer.Text = String.Empty;

                //Resets label and icons
                pbQuestionResult.Image = null;
                lblResultText.Text = String.Empty;
                AnswerWasShown = false;                
            }
        }

        #endregion
    }
}
