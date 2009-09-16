namespace Suru.TrainingKit.Controls
{
    partial class frmAnswerDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAnswerDetail));
            this.txtAnswerDetail = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtAnswerDetail
            // 
            this.txtAnswerDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAnswerDetail.Location = new System.Drawing.Point(0, 0);
            this.txtAnswerDetail.Multiline = true;
            this.txtAnswerDetail.Name = "txtAnswerDetail";
            this.txtAnswerDetail.ReadOnly = true;
            this.txtAnswerDetail.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtAnswerDetail.Size = new System.Drawing.Size(588, 327);
            this.txtAnswerDetail.TabIndex = 0;
            // 
            // frmAnswerDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 327);
            this.Controls.Add(this.txtAnswerDetail);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAnswerDetail";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmAnswerDetail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAnswerDetail;
    }
}