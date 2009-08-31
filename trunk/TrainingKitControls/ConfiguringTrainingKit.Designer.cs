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
            ((System.ComponentModel.ISupportInitialize)(this.nudMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopics)).BeginInit();
            this.SuspendLayout();
            // 
            // nudMinutes
            // 
            this.nudMinutes.Location = new System.Drawing.Point(120, 31);
            this.nudMinutes.Maximum = new decimal(new int[] {
            1000,
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
            // 
            // lblTimeLimit
            // 
            this.lblTimeLimit.AutoSize = true;
            this.lblTimeLimit.Location = new System.Drawing.Point(10, 33);
            this.lblTimeLimit.Name = "lblTimeLimit";
            this.lblTimeLimit.Size = new System.Drawing.Size(104, 13);
            this.lblTimeLimit.TabIndex = 1;
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
            this.cmbLanguages.Location = new System.Drawing.Point(76, 61);
            this.cmbLanguages.Name = "cmbLanguages";
            this.cmbLanguages.Size = new System.Drawing.Size(86, 21);
            this.cmbLanguages.TabIndex = 3;
            // 
            // lblLanguages
            // 
            this.lblLanguages.AutoSize = true;
            this.lblLanguages.Location = new System.Drawing.Point(10, 64);
            this.lblLanguages.Name = "lblLanguages";
            this.lblLanguages.Size = new System.Drawing.Size(55, 13);
            this.lblLanguages.TabIndex = 4;
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
            this.lblTopics.TabIndex = 6;
            this.lblTopics.Text = "Topics";
            // 
            // ConfiguringTrainingKit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTopics);
            this.Controls.Add(this.dgvTopics);
            this.Controls.Add(this.lblLanguages);
            this.Controls.Add(this.cmbLanguages);
            this.Controls.Add(this.chkExamTiming);
            this.Controls.Add(this.lblTimeLimit);
            this.Controls.Add(this.nudMinutes);
            this.Name = "ConfiguringTrainingKit";
            this.Size = new System.Drawing.Size(488, 265);
            this.Load += new System.EventHandler(this.ConfiguringTrainingKit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopics)).EndInit();
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
    }
}
