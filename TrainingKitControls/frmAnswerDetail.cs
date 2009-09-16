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
    public partial class frmAnswerDetail : Form
    {
        #region Local Variables

        private Int32 QuestionNumber = -1;
        private String LanguageSelected;
        private String AnswerText;

        #endregion

        #region Class Constructor & Event Handler

        //Class Constructor
        public frmAnswerDetail(Int32 Question, String Language, String Answer)
        {
            QuestionNumber = Question;
            LanguageSelected = Language;
            AnswerText = Answer;

            InitializeComponent();
        }

        //frmAnswerDetail Load Event Handler
        private void frmAnswerDetail_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Answer Detail for Question ");

            if (QuestionNumber == -1)
                sb.Append(" (unknown)");
            else
                sb.Append(QuestionNumber.ToString());

            sb.Append(" - Language: ");

            if (LanguageSelected == null || LanguageSelected.Trim() == String.Empty)
                sb.Append(" (unknown)");
            else
                sb.Append(LanguageSelected.Trim());

            this.Text = sb.ToString();

            if (AnswerText != null)
                txtAnswerDetail.Text = "Your Answer: " + Environment.NewLine + AnswerText;

            txtAnswerDetail.SelectionStart = 0;
            txtAnswerDetail.SelectionLength = 0;
        }

        #endregion
    }
}
