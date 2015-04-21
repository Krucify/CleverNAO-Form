namespace CleverNAO_Form
{
    partial class Form1
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
            this.convoBox = new System.Windows.Forms.RichTextBox();
            this.clearConvoBtn = new System.Windows.Forms.Button();
            this.inputBox = new System.Windows.Forms.TextBox();
            this.speechOnOff = new System.Windows.Forms.Button();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // convoBox
            // 
            this.convoBox.Location = new System.Drawing.Point(12, 12);
            this.convoBox.Name = "convoBox";
            this.convoBox.Size = new System.Drawing.Size(463, 219);
            this.convoBox.TabIndex = 1;
            this.convoBox.Text = "";
            this.convoBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pushToTalk_keyDown);
            this.convoBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.pushToTalk_keyUp);
            // 
            // clearConvoBtn
            // 
            this.clearConvoBtn.Location = new System.Drawing.Point(339, 294);
            this.clearConvoBtn.Name = "clearConvoBtn";
            this.clearConvoBtn.Size = new System.Drawing.Size(84, 36);
            this.clearConvoBtn.TabIndex = 2;
            this.clearConvoBtn.Text = "Clear Conversation";
            this.clearConvoBtn.UseVisualStyleBackColor = true;
            // 
            // inputBox
            // 
            this.inputBox.Location = new System.Drawing.Point(12, 256);
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(463, 20);
            this.inputBox.TabIndex = 3;
            this.inputBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pushToTalk_keyDown);
            this.inputBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.inputBox_enter);
            this.inputBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.pushToTalk_keyUp);
            // 
            // speechOnOff
            // 
            this.speechOnOff.BackColor = System.Drawing.SystemColors.Control;
            this.speechOnOff.Location = new System.Drawing.Point(62, 294);
            this.speechOnOff.Name = "speechOnOff";
            this.speechOnOff.Size = new System.Drawing.Size(88, 36);
            this.speechOnOff.TabIndex = 4;
            this.speechOnOff.Text = "Off";
            this.speechOnOff.UseVisualStyleBackColor = false;
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.ErrorLabel.Location = new System.Drawing.Point(217, 349);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(0, 13);
            this.ErrorLabel.TabIndex = 5;
            this.ErrorLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 371);
            this.Controls.Add(this.ErrorLabel);
            this.Controls.Add(this.speechOnOff);
            this.Controls.Add(this.inputBox);
            this.Controls.Add(this.clearConvoBtn);
            this.Controls.Add(this.convoBox);
            this.Name = "Form1";
            this.Text = "CleverNAO";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pushToTalk_keyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.pushToTalk_keyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox convoBox;
        private System.Windows.Forms.Button clearConvoBtn;
        private System.Windows.Forms.TextBox inputBox;
        private System.Windows.Forms.Button speechOnOff;
        private System.Windows.Forms.Label ErrorLabel;

    }
}

