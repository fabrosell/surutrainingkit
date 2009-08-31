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
    public partial class ConfiguringTrainingKit : UserControl
    {
        private const String CheckColName = "[Checked]";
        private const String TopicColName = "[Column]";
        
        public List<String> LanguageList { get; set; }
        public List<String> TopicList    { get; set; }

        /// <summary>
        /// Refresh content of the Exam
        /// </summary>
        public void RefreshContent()
        {
            cmbLanguages.Items.Clear();
            dgvTopics.Rows.Clear();            

            #region Load Language List

            if (LanguageList != null)
            {
                cmbLanguages.Items.Clear();

                foreach (String s in LanguageList)
                    cmbLanguages.Items.Add(s);

                if (cmbLanguages.Items.Count > 0)
                    cmbLanguages.SelectedIndex = 0;
            }

            #endregion

            //Load Topic List
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
                }
            }
        }

        //Class Constructor
        public ConfiguringTrainingKit()
        {
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
            }
            else
            {
                nudMinutes.Minimum = 0;
                nudMinutes.Value = 0;
                nudMinutes.Enabled = false;
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
        }
    }
}
