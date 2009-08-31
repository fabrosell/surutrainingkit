﻿namespace Suru.TrainingKit.UI
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
            this.msMenu = new System.Windows.Forms.MenuStrip();
            this.tsmiAvailableExams = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTestStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.ctkConfig = new Suru.TrainingKit.Controls.ConfiguringTrainingKit();
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
            this.msMenu.Size = new System.Drawing.Size(507, 24);
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
            this.tsmiAbout.Size = new System.Drawing.Size(114, 22);
            this.tsmiAbout.Text = "&About";
            // 
            // ctkConfig
            // 
            this.ctkConfig.LanguageList = null;
            this.ctkConfig.Location = new System.Drawing.Point(12, 39);
            this.ctkConfig.Name = "ctkConfig";
            this.ctkConfig.Size = new System.Drawing.Size(483, 276);
            this.ctkConfig.TabIndex = 1;
            this.ctkConfig.TopicList = null;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 451);
            this.Controls.Add(this.ctkConfig);
            this.Controls.Add(this.msMenu);
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
    }
}

