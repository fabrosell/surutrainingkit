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

        private Dictionary<Int16, String> DictResponses = null;
        private Nullable<Int16> CurrentQuestion = null;

        #endregion

        #region Class Methods

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

            //IconedLabel il = null;

            foreach (Int16 i in DictQuestions.Keys)
            {
                //il = new IconedLabel();
                //il.lblItem.Text = i.ToString();
                //lbQuestions.Items.Add(il);
                lbQuestions.Items.Add(i);
            }

            if (lbQuestions.Items.Count > 0)
                lbQuestions.SelectedIndex = 0;
        }

        /// <summary>
        /// Stop the test.
        /// </summary>
        public void StopTest()
        {
            txtAnswer.ReadOnly = true;
        }

        #endregion

        #region Constructors & Event Handlers

        public TestTrainingKit()
        {
            InitializeComponent();

            lblResultText.Text = String.Empty;
            pbQuestionResult.Image = null;
            DictResponses = new Dictionary<Int16, String>();
        }

        //lbQuestion Select Index Changed Event Handler
        private void lbQuestions_SelectedIndexChanged(object sender, EventArgs e)
        {            
            Int16 SelectedQuestion = (Int16)lbQuestions.SelectedItem;

            if (SelectedQuestion != -1)
            {
                //Save previous results, if current question is not null
                if (CurrentQuestion != null)
                {
                    if (!DictResponses.ContainsKey(CurrentQuestion.Value))
                        DictResponses.Add(CurrentQuestion.Value, String.Empty);

                    DictResponses[CurrentQuestion.Value] = txtAnswer.Text.Trim();
                }

                //Set current question
                CurrentQuestion = SelectedQuestion;

                txtQuestion.Text = DictQuestions[SelectedQuestion];

                if (DictResponses.ContainsKey(SelectedQuestion))
                    txtAnswer.Text = DictResponses[SelectedQuestion];
                else
                    txtAnswer.Text = String.Empty;
            }
            
        }

        #endregion
    }
}
