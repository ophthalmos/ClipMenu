namespace ClipMenu
{
    partial class ImagePath
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
            folderBrowserDialog = new FolderBrowserDialog();
            textBox = new TextBox();
            btnBrowse = new Button();
            label = new Label();
            btnOK = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // textBox
            // 
            textBox.Location = new Point(12, 31);
            textBox.Name = "textBox";
            textBox.Size = new Size(174, 25);
            textBox.TabIndex = 0;
            // 
            // btnBrowse
            // 
            btnBrowse.FlatStyle = FlatStyle.Flat;
            btnBrowse.Location = new Point(192, 31);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(30, 25);
            btnBrowse.TabIndex = 1;
            btnBrowse.Text = "…";
            btnBrowse.TextAlign = ContentAlignment.TopCenter;
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += BtnBrowse_Click;
            // 
            // label
            // 
            label.AutoSize = true;
            label.Location = new Point(12, 9);
            label.Name = "label";
            label.Size = new Size(194, 19);
            label.TabIndex = 2;
            label.Text = "Bestimmen Sie den Zielordner:";
            // 
            // btnOK
            // 
            btnOK.DialogResult = DialogResult.OK;
            btnOK.Location = new Point(12, 70);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(105, 25);
            btnOK.TabIndex = 3;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(120, 70);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(105, 25);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Abbrechen";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // ImagePath
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(234, 101);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(label);
            Controls.Add(btnBrowse);
            Controls.Add(textBox);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ImagePath";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "ClipMenu";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FolderBrowserDialog folderBrowserDialog;
        private TextBox textBox;
        private Button btnBrowse;
        private Label label;
        private Button btnOK;
        private Button btnCancel;
    }
}