namespace Suru.TrainingKit.UI
{
    partial class frmTestResult
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTestResult));
            this.dgvTestResult = new System.Windows.Forms.DataGridView();
            this.lblExamResult = new System.Windows.Forms.Label();
            this.lblTestResult = new System.Windows.Forms.Label();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.lblElapsed = new System.Windows.Forms.Label();
            this.lblQuestions = new System.Windows.Forms.Label();
            this.btnExportResults = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblStartTimeCaption = new System.Windows.Forms.Label();
            this.lblEndTimeCaption = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestResult)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTestResult
            // 
            this.dgvTestResult.AllowUserToAddRows = false;
            this.dgvTestResult.AllowUserToDeleteRows = false;
            this.dgvTestResult.AllowUserToOrderColumns = true;
            this.dgvTestResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTestResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTestResult.Location = new System.Drawing.Point(12, 56);
            this.dgvTestResult.Name = "dgvTestResult";
            this.dgvTestResult.ReadOnly = true;
            this.dgvTestResult.RowHeadersVisible = false;
            this.dgvTestResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTestResult.Size = new System.Drawing.Size(722, 286);
            this.dgvTestResult.TabIndex = 1;
            this.dgvTestResult.Resize += new System.EventHandler(this.dgvTestResult_Resize);
            // 
            // lblExamResult
            // 
            this.lblExamResult.AutoSize = true;
            this.lblExamResult.Location = new System.Drawing.Point(12, 21);
            this.lblExamResult.Name = "lblExamResult";
            this.lblExamResult.Size = new System.Drawing.Size(72, 13);
            this.lblExamResult.TabIndex = 4;
            this.lblExamResult.Text = "Exam Result: ";
            // 
            // lblTestResult
            // 
            this.lblTestResult.AutoSize = true;
            this.lblTestResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTestResult.Location = new System.Drawing.Point(90, 16);
            this.lblTestResult.Name = "lblTestResult";
            this.lblTestResult.Size = new System.Drawing.Size(83, 20);
            this.lblTestResult.TabIndex = 5;
            this.lblTestResult.Text = "[bad / ok]";
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Location = new System.Drawing.Point(459, 9);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(51, 13);
            this.lblStartTime.TabIndex = 7;
            this.lblStartTime.Text = "hh:mm:ss";
            // 
            // lblEndTime
            // 
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Location = new System.Drawing.Point(459, 40);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(51, 13);
            this.lblEndTime.TabIndex = 9;
            this.lblEndTime.Text = "hh:mm:ss";
            // 
            // lblElapsed
            // 
            this.lblElapsed.AutoSize = true;
            this.lblElapsed.Location = new System.Drawing.Point(530, 25);
            this.lblElapsed.Name = "lblElapsed";
            this.lblElapsed.Size = new System.Drawing.Size(109, 13);
            this.lblElapsed.TabIndex = 10;
            this.lblElapsed.Text = "Elapsed Dd hh:mm:ss";
            // 
            // lblQuestions
            // 
            this.lblQuestions.AutoSize = true;
            this.lblQuestions.Location = new System.Drawing.Point(656, 25);
            this.lblQuestions.Name = "lblQuestions";
            this.lblQuestions.Size = new System.Drawing.Size(75, 13);
            this.lblQuestions.TabIndex = 11;
            this.lblQuestions.Text = "Questions: xxx";
            // 
            // btnExportResults
            // 
            this.btnExportResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportResults.Location = new System.Drawing.Point(564, 355);
            this.btnExportResults.Name = "btnExportResults";
            this.btnExportResults.Size = new System.Drawing.Size(89, 23);
            this.btnExportResults.TabIndex = 2;
            this.btnExportResults.Text = "&Export Results";
            this.btnExportResults.UseVisualStyleBackColor = true;
            this.btnExportResults.Click += new System.EventHandler(this.btnExportResults_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(659, 355);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblStartTimeCaption
            // 
            this.lblStartTimeCaption.AutoSize = true;
            this.lblStartTimeCaption.Location = new System.Drawing.Point(402, 9);
            this.lblStartTimeCaption.Name = "lblStartTimeCaption";
            this.lblStartTimeCaption.Size = new System.Drawing.Size(58, 13);
            this.lblStartTimeCaption.TabIndex = 6;
            this.lblStartTimeCaption.Text = "Start Time:";
            // 
            // lblEndTimeCaption
            // 
            this.lblEndTimeCaption.AutoSize = true;
            this.lblEndTimeCaption.Location = new System.Drawing.Point(402, 40);
            this.lblEndTimeCaption.Name = "lblEndTimeCaption";
            this.lblEndTimeCaption.Size = new System.Drawing.Size(55, 13);
            this.lblEndTimeCaption.TabIndex = 8;
            this.lblEndTimeCaption.Text = "End Time:";
            // 
            // frmTestResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 390);
            this.Controls.Add(this.lblEndTimeCaption);
            this.Controls.Add(this.lblStartTimeCaption);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnExportResults);
            this.Controls.Add(this.lblQuestions);
            this.Controls.Add(this.lblElapsed);
            this.Controls.Add(this.lblEndTime);
            this.Controls.Add(this.lblStartTime);
            this.Controls.Add(this.lblTestResult);
            this.Controls.Add(this.lblExamResult);
            this.Controls.Add(this.dgvTestResult);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTestResult";
            this.ShowInTaskbar = false;
            this.Text = "frmTestResult";
            this.Load += new System.EventHandler(this.frmTestResult_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTestResult;
        private System.Windows.Forms.Label lblExamResult;
        private System.Windows.Forms.Label lblTestResult;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.Label lblElapsed;
        private System.Windows.Forms.Label lblQuestions;
        private System.Windows.Forms.Button btnExportResults;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblStartTimeCaption;
        private System.Windows.Forms.Label lblEndTimeCaption;
    }
}