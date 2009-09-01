namespace Suru.TrainingKit.Controls
{
    partial class ConfiguringTrainingKit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nudMinutes = new System.Windows.Forms.NumericUpDown();
            this.lblTimeLimit = new System.Windows.Forms.Label();
            this.chkExamTiming = new System.Windows.Forms.CheckBox();
            this.cmbLanguages = new System.Windows.Forms.ComboBox();
            this.lblLanguages = new System.Windows.Forms.Label();
            this.dgvTopics = new System.Windows.Forms.DataGridView();
            this.lblTopics = new System.Windows.Forms.Label();
            this.lblQuestions = new System.Windows.Forms.Label();
            this.nudQuestions = new System.Windows.Forms.NumericUpDown();
            this.chkRandomize = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopics)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuestions)).BeginInit();
            this.SuspendLayout();
            // 
            // nudMinutes
            // 
            this.nudMinutes.Location = new System.Drawing.Point(120, 31);
            this.nudMinutes.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudMinutes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMinutes.Name = "nudMinutes";
            this.nudMinutes.Size = new System.Drawing.Size(42, 20);
            this.nudMinutes.TabIndex = 2;
            this.nudMinutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudMinutes.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.nudMinutes.ValueChanged += new System.EventHandler(this.nudMinutes_ValueChanged);
            // 
            // lblTimeLimit
            // 
            this.lblTimeLimit.AutoSize = true;
            this.lblTimeLimit.Location = new System.Drawing.Point(10, 33);
            this.lblTimeLimit.Name = "lblTimeLimit";
            this.lblTimeLimit.Size = new System.Drawing.Size(104, 13);
            this.lblTimeLimit.TabIndex = 7;
            this.lblTimeLimit.Text = "Time Limits (minutes)";
            // 
            // chkExamTiming
            // 
            this.chkExamTiming.AutoSize = true;
            this.chkExamTiming.Checked = true;
            this.chkExamTiming.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkExamTiming.Location = new System.Drawing.Point(13, 13);
            this.chkExamTiming.Name = "chkExamTiming";
            this.chkExamTiming.Size = new System.Drawing.Size(86, 17);
            this.chkExamTiming.TabIndex = 1;
            this.chkExamTiming.Text = "Exam Timing";
            this.chkExamTiming.UseVisualStyleBackColor = true;
            this.chkExamTiming.CheckedChanged += new System.EventHandler(this.chkExamTiming_CheckedChanged);
            // 
            // cmbLanguages
            // 
            this.cmbLanguages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLanguages.FormattingEnabled = true;
            this.cmbLanguages.Location = new System.Drawing.Point(76, 65);
            this.cmbLanguages.Name = "cmbLanguages";
            this.cmbLanguages.Size = new System.Drawing.Size(86, 21);
            this.cmbLanguages.TabIndex = 3;
            this.cmbLanguages.SelectedIndexChanged += new System.EventHandler(this.cmbLanguages_SelectedIndexChanged);
            // 
            // lblLanguages
            // 
            this.lblLanguages.AutoSize = true;
            this.lblLanguages.Location = new System.Drawing.Point(10, 68);
            this.lblLanguages.Name = "lblLanguages";
            this.lblLanguages.Size = new System.Drawing.Size(55, 13);
            this.lblLanguages.TabIndex = 8;
            this.lblLanguages.Text = "Language";
            // 
            // dgvTopics
            // 
            this.dgvTopics.AllowUserToAddRows = false;
            this.dgvTopics.AllowUserToDeleteRows = false;
            this.dgvTopics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTopics.ColumnHeadersVisible = false;
            this.dgvTopics.Location = new System.Drawing.Point(13, 112);
            this.dgvTopics.Name = "dgvTopics";
            this.dgvTopics.ReadOnly = true;
            this.dgvTopics.RowHeadersVisible = false;
            this.dgvTopics.Size = new System.Drawing.Size(461, 135);
            this.dgvTopics.TabIndex = 4;
            this.dgvTopics.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTopics_CellContentClick);
            // 
            // lblTopics
            // 
            this.lblTopics.AutoSize = true;
            this.lblTopics.Location = new System.Drawing.Point(10, 96);
            this.lblTopics.Name = "lblTopics";
            this.lblTopics.Size = new System.Drawing.Size(39, 13);
            this.lblTopics.TabIndex = 9;
            this.lblTopics.Text = "Topics";
            // 
            // lblQuestions
            // 
            this.lblQuestions.AutoSize = true;
            this.lblQuestions.Location = new System.Drawing.Point(60, 255);
            this.lblQuestions.Name = "lblQuestions";
            this.lblQuestions.Size = new System.Drawing.Size(54, 13);
            this.lblQuestions.TabIndex = 10;
            this.lblQuestions.Text = "Questions";
            // 
            // nudQuestions
            // 
            this.nudQuestions.Location = new System.Drawing.Point(13, 253);
            this.nudQuestions.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudQuestions.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudQuestions.Name = "nudQuestions";
            this.nudQuestions.Size = new System.Drawing.Size(42, 20);
            this.nudQuestions.TabIndex = 5;
            this.nudQuestions.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudQuestions.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.nudQuestions.ValueChanged += new System.EventHandler(this.nudQuestions_ValueChanged);
            // 
            // chkRandomize
            // 
            this.chkRandomize.AutoSize = true;
            this.chkRandomize.Checked = true;
            this.chkRandomize.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRandomize.Location = new System.Drawing.Point(13, 279);
            this.chkRandomize.Name = "chkRandomize";
            this.chkRandomize.Size = new System.Drawing.Size(129, 17);
            this.chkRandomize.TabIndex = 6;
            this.chkRandomize.Text = "Randomize Questions";
            this.chkRandomize.UseVisualStyleBackColor = true;
            this.chkRandomize.CheckedChanged += new System.EventHandler(this.chkRandomize_CheckedChanged);
            // 
            // ConfiguringTrainingKit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkRandomize);
            this.Controls.Add(this.nudQuestions);
            this.Controls.Add(this.lblQuestions);
            this.Controls.Add(this.lblTopics);
            this.Controls.Add(this.dgvTopics);
            this.Controls.Add(this.lblLanguages);
            this.Controls.Add(this.cmbLanguages);
            this.Controls.Add(this.chkExamTiming);
            this.Controls.Add(this.lblTimeLimit);
            this.Controls.Add(this.nudMinutes);
            this.Name = "ConfiguringTrainingKit";
            this.Size = new System.Drawing.Size(488, 322);
            this.Load += new System.EventHandler(this.ConfiguringTrainingKit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopics)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuestions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudMinutes;
        private System.Windows.Forms.Label lblTimeLimit;
        private System.Windows.Forms.CheckBox chkExamTiming;
        private System.Windows.Forms.ComboBox cmbLanguages;
        private System.Windows.Forms.Label lblLanguages;
        private System.Windows.Forms.DataGridView dgvTopics;
        private System.Windows.Forms.Label lblTopics;
        private System.Windows.Forms.Label lblQuestions;
        private System.Windows.Forms.NumericUpDown nudQuestions;
        private System.Windows.Forms.CheckBox chkRandomize;
    }
}
