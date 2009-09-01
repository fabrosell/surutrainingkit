namespace Suru.TrainingKit.Controls
{
    partial class TestTrainingKit
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
            this.components = new System.ComponentModel.Container();
            this.StopTimer = new System.Windows.Forms.Timer(this.components);
            this.lblRemainingTime = new System.Windows.Forms.Label();
            this.txtQuestion = new System.Windows.Forms.TextBox();
            this.lblQuestionNumber = new System.Windows.Forms.Label();
            this.lblAnswer = new System.Windows.Forms.Label();
            this.txtAnswer = new System.Windows.Forms.TextBox();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnShowAnswer = new System.Windows.Forms.Button();
            this.chkShowAnswer = new System.Windows.Forms.CheckBox();
            this.lbQuestions = new System.Windows.Forms.ListBox();
            this.lblList = new System.Windows.Forms.Label();
            this.pbQuestionResult = new System.Windows.Forms.PictureBox();
            this.lblResultText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbQuestionResult)).BeginInit();
            this.SuspendLayout();
            // 
            // StopTimer
            // 
            this.StopTimer.Interval = 1000;
            // 
            // lblRemainingTime
            // 
            this.lblRemainingTime.AutoSize = true;
            this.lblRemainingTime.Location = new System.Drawing.Point(448, 11);
            this.lblRemainingTime.Name = "lblRemainingTime";
            this.lblRemainingTime.Size = new System.Drawing.Size(137, 13);
            this.lblRemainingTime.TabIndex = 0;
            this.lblRemainingTime.Text = "Remaining Time: ##:##:##";
            this.lblRemainingTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtQuestion
            // 
            this.txtQuestion.Location = new System.Drawing.Point(53, 27);
            this.txtQuestion.Multiline = true;
            this.txtQuestion.Name = "txtQuestion";
            this.txtQuestion.ReadOnly = true;
            this.txtQuestion.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtQuestion.Size = new System.Drawing.Size(532, 140);
            this.txtQuestion.TabIndex = 1;
            // 
            // lblQuestionNumber
            // 
            this.lblQuestionNumber.AutoSize = true;
            this.lblQuestionNumber.Location = new System.Drawing.Point(50, 11);
            this.lblQuestionNumber.Name = "lblQuestionNumber";
            this.lblQuestionNumber.Size = new System.Drawing.Size(72, 13);
            this.lblQuestionNumber.TabIndex = 2;
            this.lblQuestionNumber.Text = "Question nº #";
            // 
            // lblAnswer
            // 
            this.lblAnswer.AutoSize = true;
            this.lblAnswer.Location = new System.Drawing.Point(53, 170);
            this.lblAnswer.Name = "lblAnswer";
            this.lblAnswer.Size = new System.Drawing.Size(42, 13);
            this.lblAnswer.TabIndex = 4;
            this.lblAnswer.Text = "Answer";
            // 
            // txtAnswer
            // 
            this.txtAnswer.Location = new System.Drawing.Point(53, 186);
            this.txtAnswer.Multiline = true;
            this.txtAnswer.Name = "txtAnswer";
            this.txtAnswer.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAnswer.Size = new System.Drawing.Size(532, 76);
            this.txtAnswer.TabIndex = 3;
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(393, 268);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(46, 23);
            this.btnPrevious.TabIndex = 5;
            this.btnPrevious.Text = "< <";
            this.btnPrevious.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(445, 268);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(46, 23);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = "> >";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // btnShowAnswer
            // 
            this.btnShowAnswer.Location = new System.Drawing.Point(497, 268);
            this.btnShowAnswer.Name = "btnShowAnswer";
            this.btnShowAnswer.Size = new System.Drawing.Size(88, 23);
            this.btnShowAnswer.TabIndex = 7;
            this.btnShowAnswer.Text = "Show Answer";
            this.btnShowAnswer.UseVisualStyleBackColor = true;
            // 
            // chkShowAnswer
            // 
            this.chkShowAnswer.AutoSize = true;
            this.chkShowAnswer.Location = new System.Drawing.Point(169, 7);
            this.chkShowAnswer.Name = "chkShowAnswer";
            this.chkShowAnswer.Size = new System.Drawing.Size(161, 17);
            this.chkShowAnswer.TabIndex = 8;
            this.chkShowAnswer.Text = "Automatically Show Answers";
            this.chkShowAnswer.UseVisualStyleBackColor = true;
            // 
            // lbquestions
            // 
            this.lbQuestions.FormattingEnabled = true;
            this.lbQuestions.Location = new System.Drawing.Point(0, 27);
            this.lbQuestions.Name = "lbquestions";
            this.lbQuestions.Size = new System.Drawing.Size(47, 264);
            this.lbQuestions.TabIndex = 9;
            // 
            // lblList
            // 
            this.lblList.AutoSize = true;
            this.lblList.Location = new System.Drawing.Point(3, 11);
            this.lblList.Name = "lblList";
            this.lblList.Size = new System.Drawing.Size(23, 13);
            this.lblList.TabIndex = 10;
            this.lblList.Text = "List";
            // 
            // pbQuestionResult
            // 
            this.pbQuestionResult.Location = new System.Drawing.Point(56, 268);
            this.pbQuestionResult.Name = "pbQuestionResult";
            this.pbQuestionResult.Size = new System.Drawing.Size(20, 20);
            this.pbQuestionResult.TabIndex = 11;
            this.pbQuestionResult.TabStop = false;
            // 
            // lblResultText
            // 
            this.lblResultText.AutoSize = true;
            this.lblResultText.Location = new System.Drawing.Point(82, 273);
            this.lblResultText.Name = "lblResultText";
            this.lblResultText.Size = new System.Drawing.Size(67, 13);
            this.lblResultText.TabIndex = 12;
            this.lblResultText.Text = "[Result Text]";
            // 
            // TestTrainingKit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblResultText);
            this.Controls.Add(this.pbQuestionResult);
            this.Controls.Add(this.lblList);
            this.Controls.Add(this.lbQuestions);
            this.Controls.Add(this.chkShowAnswer);
            this.Controls.Add(this.btnShowAnswer);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Controls.Add(this.lblAnswer);
            this.Controls.Add(this.txtAnswer);
            this.Controls.Add(this.lblQuestionNumber);
            this.Controls.Add(this.txtQuestion);
            this.Controls.Add(this.lblRemainingTime);
            this.Name = "TestTrainingKit";
            this.Size = new System.Drawing.Size(588, 297);
            ((System.ComponentModel.ISupportInitialize)(this.pbQuestionResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer StopTimer;
        private System.Windows.Forms.Label lblRemainingTime;
        private System.Windows.Forms.TextBox txtQuestion;
        private System.Windows.Forms.Label lblQuestionNumber;
        private System.Windows.Forms.Label lblAnswer;
        private System.Windows.Forms.TextBox txtAnswer;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnShowAnswer;
        private System.Windows.Forms.CheckBox chkShowAnswer;
        private System.Windows.Forms.ListBox lbQuestions;
        private System.Windows.Forms.Label lblList;
        private System.Windows.Forms.PictureBox pbQuestionResult;
        private System.Windows.Forms.Label lblResultText;

    }
}
