namespace Suru.TrainingKit.UI
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.msMenu = new System.Windows.Forms.MenuStrip();
            this.tsmiAvailableExams = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTestStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.ctkConfig = new Suru.TrainingKit.Controls.ConfiguringTrainingKit();
            this.ttkTest = new Suru.TrainingKit.Controls.TestTrainingKit();
            this.tsmiTestResult = new System.Windows.Forms.ToolStripMenuItem();
            this.msMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMenu
            // 
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAvailableExams,
            this.tsmiTestStatus,
            this.tsmiTestResult,
            this.tsmiAbout});
            this.msMenu.Location = new System.Drawing.Point(0, 0);
            this.msMenu.Name = "msMenu";
            this.msMenu.Size = new System.Drawing.Size(600, 24);
            this.msMenu.TabIndex = 0;
            this.msMenu.Text = "Menu";
            // 
            // tsmiAvailableExams
            // 
            this.tsmiAvailableExams.Name = "tsmiAvailableExams";
            this.tsmiAvailableExams.Size = new System.Drawing.Size(96, 20);
            this.tsmiAvailableExams.Text = "&Available Exams";
            // 
            // tsmiTestStatus
            // 
            this.tsmiTestStatus.Name = "tsmiTestStatus";
            this.tsmiTestStatus.Size = new System.Drawing.Size(67, 20);
            this.tsmiTestStatus.Text = "&Start Test";
            this.tsmiTestStatus.Click += new System.EventHandler(this.tsmiTestStatus_Click);
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(48, 20);
            this.tsmiAbout.Text = "A&bout";
            this.tsmiAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
            // 
            // ctkConfig
            // 
            this.ctkConfig.LanguageDict = null;
            this.ctkConfig.LanguageSelected = null;
            this.ctkConfig.Location = new System.Drawing.Point(0, 27);
            this.ctkConfig.MinutesSelected = ((short)(90));
            this.ctkConfig.Name = "ctkConfig";
            this.ctkConfig.QuestionNumber = null;
            this.ctkConfig.Size = new System.Drawing.Size(435, 301);
            this.ctkConfig.TabIndex = 1;
            this.ctkConfig.TopicList = null;
            this.ctkConfig.TopicsSelected = ((System.Collections.Generic.List<string>)(resources.GetObject("ctkConfig.TopicsSelected")));
            // 
            // ttkTest
            // 
            this.ttkTest.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ttkTest.ApprobationPercentage = ((short)(0));
            this.ttkTest.DictAnnotations = null;
            this.ttkTest.DictAnswers = null;
            this.ttkTest.DictPoints = null;
            this.ttkTest.DictQuestions = null;
            this.ttkTest.ExamMinutes = null;
            this.ttkTest.Location = new System.Drawing.Point(0, 27);
            this.ttkTest.Name = "ttkTest";
            this.ttkTest.Points = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ttkTest.QuestionsOK = null;
            this.ttkTest.Size = new System.Drawing.Size(600, 326);
            this.ttkTest.TabIndex = 2;
            this.ttkTest.TestStopped += new System.EventHandler(this.ttkTest_TestStopped);
            // 
            // tsmiTestResult
            // 
            this.tsmiTestResult.Name = "tsmiTestResult";
            this.tsmiTestResult.Size = new System.Drawing.Size(73, 20);
            this.tsmiTestResult.Text = "&Test Result";
            this.tsmiTestResult.Click += new System.EventHandler(this.tsmiTestResult_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 352);
            this.Controls.Add(this.ctkConfig);
            this.Controls.Add(this.msMenu);
            this.Controls.Add(this.ttkTest);
            this.MainMenuStrip = this.msMenu;
            this.Name = "frmMain";
            this.Text = "Suru Training Kit";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiAvailableExams;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
        private System.Windows.Forms.ToolStripMenuItem tsmiTestStatus;
        private Suru.TrainingKit.Controls.ConfiguringTrainingKit ctkConfig;
        private Suru.TrainingKit.Controls.TestTrainingKit ttkTest;
        private System.Windows.Forms.ToolStripMenuItem aboutLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiTestResult;
    }
}

