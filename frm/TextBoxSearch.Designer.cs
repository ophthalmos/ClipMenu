namespace ClipMenu
{
    partial class TextBoxSearch
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
            cbxSearch = new ComboBox();
            checkCase = new CheckBox();
            btnSearch = new Button();
            btnAbort = new Button();
            SuspendLayout();
            // 
            // cbxSearch
            // 
            cbxSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbxSearch.AutoCompleteSource = AutoCompleteSource.ListItems;
            cbxSearch.FormattingEnabled = true;
            cbxSearch.Location = new Point(12, 12);
            cbxSearch.Name = "cbxSearch";
            cbxSearch.Size = new Size(194, 23);
            cbxSearch.TabIndex = 0;
            // 
            // checkCase
            // 
            checkCase.AutoSize = true;
            checkCase.Location = new Point(12, 44);
            checkCase.Name = "checkCase";
            checkCase.Size = new Size(199, 19);
            checkCase.TabIndex = 2;
            checkCase.Text = "Groß-/Kleinschreibung beachten";
            checkCase.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(217, 12);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(110, 23);
            btnSearch.TabIndex = 4;
            btnSearch.Text = "Weitersuchen";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += BtnSearch_Click;
            // 
            // btnAbort
            // 
            btnAbort.DialogResult = DialogResult.Abort;
            btnAbort.Location = new Point(217, 41);
            btnAbort.Name = "btnAbort";
            btnAbort.Size = new Size(110, 23);
            btnAbort.TabIndex = 5;
            btnAbort.Text = "Abbrechen";
            btnAbort.UseVisualStyleBackColor = true;
            // 
            // TextBoxSearch
            // 
            AcceptButton = btnSearch;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnAbort;
            ClientSize = new Size(333, 70);
            Controls.Add(btnAbort);
            Controls.Add(btnSearch);
            Controls.Add(checkCase);
            Controls.Add(cbxSearch);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TextBoxSearch";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Suchen";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cbxSearch;
        private CheckBox checkCase;
        private Button btnSearch;
        private Button btnAbort;
    }
}