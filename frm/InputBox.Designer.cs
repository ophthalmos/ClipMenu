namespace ClipMenu
{
    partial class InputBox
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
            label = new Label();
            textBox = new TextBox();
            buttonOK = new Button();
            buttonCancel = new Button();
            SuspendLayout();
            // 
            // label
            // 
            label.AutoSize = true;
            label.Font = new Font("Segoe UI", 9F);
            label.Location = new Point(12, 8);
            label.Name = "label";
            label.Size = new Size(201, 15);
            label.TabIndex = 0;
            label.Text = "Bitte geben Sie einen ToolTipText ein:";
            // 
            // textBox
            // 
            textBox.Font = new Font("Segoe UI", 10F);
            textBox.Location = new Point(12, 30);
            textBox.Name = "textBox";
            textBox.Size = new Size(201, 25);
            textBox.TabIndex = 1;
            // 
            // buttonOK
            // 
            buttonOK.DialogResult = DialogResult.OK;
            buttonOK.Font = new Font("Segoe UI", 10F);
            buttonOK.Location = new Point(12, 66);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(97, 27);
            buttonOK.TabIndex = 2;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Font = new Font("Segoe UI", 10F);
            buttonCancel.Location = new Point(115, 66);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(98, 27);
            buttonCancel.TabIndex = 3;
            buttonCancel.Text = "Abbrechen";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // InputBox
            // 
            AcceptButton = buttonOK;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = buttonCancel;
            ClientSize = new Size(225, 105);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
            Controls.Add(textBox);
            Controls.Add(label);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "InputBox";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "ClipMenu";
            TopMost = true;
            Load += InputBox_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label;
        private TextBox textBox;
        private Button buttonOK;
        private Button buttonCancel;
    }
}