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
    /// <summary>
    /// User control to configure an exam.
    /// </summary>
    public partial class ConfiguringTrainingKit : UserControl
    {
        #region Constants and Variables

        private const String CheckColName = "[Checked]";
        private const String TopicColName = "[Column]";

        //"Input Variables" --> To show in form
        public Dictionary<String, Int16> LanguageDict { get; set; }        
        public List<String> TopicList    { get; set; }

        //"Output Variables" --> Selected info to form
        public String LanguageSelected { get; set; }
        public Nullable<Int16> MinutesSelected { get; set; }
        public List<String> TopicsSelected { get; set; }
        public Nullable<Int16> QuestionNumber { get; set; }

        #endregion

        #region Class Methods

        /// <summary>
        /// Refresh content of the Exam
        /// </summary>
        public void RefreshContent()
        {
            cmbLanguages.Items.Clear();
            dgvTopics.Rows.Clear();            

            #region Load Language List

            if (LanguageDict != null)
            {
                cmbLanguages.Items.Clear();                

                foreach (String s in LanguageDict.Keys)
                    cmbLanguages.Items.Add(s);

                if (cmbLanguages.Items.Count > 0)
                    cmbLanguages.SelectedIndex = 0;
            }

            #endregion

            #region Load Topic List

            if (TopicList != null)
            {
                #region Create Columns if they don't exist - Set basic DataGridView properties

                if (dgvTopics.Columns.Count == 0)
                {
                    DataGridViewCheckBoxColumn dchkCol = new DataGridViewCheckBoxColumn();
                    dchkCol.Name = CheckColName;
                    dchkCol.ReadOnly = false;
                    dchkCol.Width = 40;                    
                    dgvTopics.Columns.Add(dchkCol);
                    DataGridViewTextBoxColumn dtxtCol = new DataGridViewTextBoxColumn();
                    dtxtCol.Name = TopicColName;
                    dtxtCol.ReadOnly = true;
                    dtxtCol.Width = dgvTopics.Width - dchkCol.Width - 10;
                    dgvTopics.Columns.Add(dtxtCol);

                    dgvTopics.AllowUserToResizeColumns = false;
                    dgvTopics.AllowUserToResizeRows = false;
                    dgvTopics.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                }

                #endregion

                Int32 Position;
                DataGridViewCheckBoxCell CheckBoxCell = null;
                DataGridViewTextBoxCell TextBoxCell = null;

                foreach (String s in TopicList)
                {
                    Position = dgvTopics.Rows.Add();

                    CheckBoxCell = new DataGridViewCheckBoxCell();
                    CheckBoxCell.Value = true;                    
                    dgvTopics[CheckColName, Position] = CheckBoxCell;

                    TextBoxCell = new DataGridViewTextBoxCell();
                    TextBoxCell.Value = s;
                    dgvTopics[TopicColName, Position] = TextBoxCell;

                    if (!TopicsSelected.Contains(s))
                        TopicsSelected.Add(s);
                }
            }

            #endregion
        }

        #endregion

        #region Constructors and Event Handlers

        //Class Constructor
        public ConfiguringTrainingKit()
        {
            TopicsSelected = new List<String>();

            InitializeComponent();
        }

        //chkExamTiming Checked Changed Event Handler
        private void chkExamTiming_CheckedChanged(Object sender, EventArgs e)
        {
            if (chkExamTiming.Checked)
            {
                nudMinutes.Value = 90;
                nudMinutes.Minimum = 1;
                nudMinutes.Enabled = true;
                MinutesSelected = (Int16)nudMinutes.Value;
            }
            else
            {
                nudMinutes.Minimum = 0;
                nudMinutes.Value = 0;
                nudMinutes.Enabled = false;
                MinutesSelected = null;
            }
        }

        //Control Load Event Handler
        private void ConfiguringTrainingKit_Load(Object sender, EventArgs e)
        {
            RefreshContent();
        }

        //dgvTopics Cell Content Click Event Hander
        private void dgvTopics_CellContentClick(Object sender, DataGridViewCellEventArgs e)
        {
            dgvTopics[CheckColName, e.RowIndex].Value = !((Boolean)dgvTopics[CheckColName, e.RowIndex].Value);

            if (!((Boolean)dgvTopics[CheckColName, e.RowIndex].Value))
                TopicsSelected.Remove((String)dgvTopics[TopicColName, e.RowIndex].Value);
        }

        //cmbLanguages Selected Index Changed Event Handler
        private void cmbLanguages_SelectedIndexChanged(Object sender, EventArgs e)
        {
            if (LanguageDict.ContainsKey((String)cmbLanguages.SelectedItem))
            {
                nudQuestions.Minimum = 1;
                nudQuestions.Maximum = LanguageDict[(String)cmbLanguages.SelectedItem];
                nudQuestions.Value = nudQuestions.Maximum;
                nudQuestions.Enabled = true;

                lblQuestions.Text = "Questions (" + nudQuestions.Maximum.ToString() + " questions max)";

                LanguageSelected = (String)cmbLanguages.SelectedItem;
                QuestionNumber = (Int16)nudQuestions.Value;
            }
            else
            {
                lblQuestions.Text = "Questions (0 questions)";
                nudQuestions.Minimum = 0;
                nudQuestions.Maximum = 0;
                nudQuestions.Value = 0;
                nudQuestions.Enabled = false;

                LanguageSelected = null;
                QuestionNumber = null;
            }
        }

        //nudMinutes Value Changed Event Handler
        private void nudMinutes_ValueChanged(Object sender, EventArgs e)
        {
            MinutesSelected = (Int16)nudMinutes.Value;
        }

        //nudQuestions Value Changed Event Handler
        private void nudQuestions_ValueChanged(Object sender, EventArgs e)
        {
            QuestionNumber = (Int16)nudQuestions.Value;
        }


        #endregion
    }
}
