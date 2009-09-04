using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
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
        public Boolean ResultsExported { get; set; }
        public Nullable<Int16> ExamMinutes { get; set; }        

        private Dictionary<Int16, String> DictResponses = null;
        private Nullable<Int16> CurrentQuestion = null;
        private Boolean AnswerWasShown = false;
        private String CurrentAnswerString = String.Empty;

        private Boolean PauseTimer = false;
        private Int16 HoursRemaining = 0;
        private Int16 MinutesRemaining = 0;
        private Int16 SecondsRemaining = 0;

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
            DictResponses.Clear();
            ResultsExported = false;

            txtAnswer.ReadOnly = false;

            lbQuestions.Items.Clear();

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

            foreach (Int16 i in DictQuestions.Keys)
                lbQuestions.Items.Add(i);

            if (lbQuestions.Items.Count > 0)
                lbQuestions.SelectedIndex = 0;

            txtAnswer.Focus();
        }

        /// <summary>
        /// Stop the test.
        /// </summary>
        public void StopTest()
        {
            txtAnswer.ReadOnly = true;

            if (StopTimer != null)
                StopTimer.Enabled = false;
        }

        /// <summary>
        /// Check if current answer is true of false.
        /// </summary>
        public void CheckAnswer()
        {
            Int16 QuestionNumber = (Int16)lbQuestions.SelectedItem;

            if (DictAnswers.ContainsKey(QuestionNumber))
            {
                if (String.Compare(txtAnswer.Text.Trim(), DictAnswers[QuestionNumber], true) == 0)
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
            pbQuestionResult.Image = null;
            DictResponses = new Dictionary<Int16, String>();
        }

        //lbQuestion Select Index Changed Event Handler
        private void lbQuestions_SelectedIndexChanged(Object sender, EventArgs e)
        {
            Int16 SelectedQuestion = (Int16)lbQuestions.SelectedItem;

            if (SelectedQuestion != -1)
            {
                //Save previous results, if current question is not null
                if (CurrentQuestion != null)
                {
                    if (!DictResponses.ContainsKey(CurrentQuestion.Value))
                        DictResponses.Add(CurrentQuestion.Value, String.Empty);

                    //Removes Annotation Text
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

        //btnPrevious Click Event Handler
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (chkShowAnswer.Checked && !AnswerWasShown)
            {
                CheckAnswer();
                AnswerWasShown = true;
                return;
            }

            if (lbQuestions.SelectedIndex == 0)
                lbQuestions.SelectedIndex = lbQuestions.Items.Count - 1;
            else
                lbQuestions.SelectedIndex--;
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

            if (lbQuestions.SelectedIndex == lbQuestions.Items.Count - 1)
                lbQuestions.SelectedIndex = 0;
            else
                lbQuestions.SelectedIndex++;
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

        #endregion
    }
}
