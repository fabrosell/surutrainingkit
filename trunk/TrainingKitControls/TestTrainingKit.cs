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
            txtAnswer.ReadOnly = false;

            lbQuestions.Items.Clear();

            if (DictQuestions == null)
            {
                txtQuestion.Text = "Test cannot be started because it lacks of configurations...";
                OnStop();
                return;
            }

            foreach (Int16 i in DictQuestions.Keys)
                lbQuestions.Items.Add(i);
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
        }

        #endregion
    }
}
