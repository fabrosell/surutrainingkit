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
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.ctkConfig = new Suru.TrainingKit.Controls.ConfiguringTrainingKit();
            this.ttkTest = new Suru.TrainingKit.Controls.TestTrainingKit();
            this.msMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMenu
            // 
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAvailableExams,
            this.tsmiTestStatus,
            this.tsmiHelp});
            this.msMenu.Location = new System.Drawing.Point(0, 0);
            this.msMenu.Name = "msMenu";
            this.msMenu.Size = new System.Drawing.Size(600, 24);
            this.msMenu.TabIndex = 0;
            this.msMenu.Text = "menuStrip1";
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
            // tsmiHelp
            // 
            this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAbout});
            this.tsmiHelp.Name = "tsmiHelp";
            this.tsmiHelp.Size = new System.Drawing.Size(40, 20);
            this.tsmiHelp.Text = "&Help";
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(162, 22);
            this.tsmiAbout.Text = "&About && License";
            this.tsmiAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
            // 
            // ctkConfig
            // 
            this.ctkConfig.LanguageDict = null;
            this.ctkConfig.LanguageSelected = null;
            this.ctkConfig.Location = new System.Drawing.Point(0, 27);
            this.ctkConfig.MinutesSelected = null;
            this.ctkConfig.Name = "ctkConfig";
            this.ctkConfig.QuestionNumber = null;
            this.ctkConfig.RandomizeQuestions = true;
            this.ctkConfig.Size = new System.Drawing.Size(435, 313);
            this.ctkConfig.TabIndex = 1;
            this.ctkConfig.TopicList = null;
            this.ctkConfig.TopicsSelected = ((System.Collections.Generic.List<string>)(resources.GetObject("ctkConfig.TopicsSelected")));
            // 
            // ttkTest
            // 
            this.ttkTest.Location = new System.Drawing.Point(0, 27);
            this.ttkTest.Name = "ttkTest";
            this.ttkTest.Size = new System.Drawing.Size(600, 313);
            this.ttkTest.TabIndex = 2;
            this.ttkTest.TestStopped += new System.EventHandler(this.ttkTest_TestStopped);
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
    }
}

