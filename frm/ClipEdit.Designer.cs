namespace ClipMenu
{
    partial class FrmClipEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmClipEdit));
            textBox = new TextBox();
            statusStrip = new StatusStrip();
            toolStripStatusLabel = new ToolStripStatusLabel();
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            undoToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            cutToolStripMenuItem = new ToolStripMenuItem();
            copyToolStripMenuItem = new ToolStripMenuItem();
            pasteToolStripMenuItem = new ToolStripMenuItem();
            clearToolStripMenuItem = new ToolStripMenuItem();
            selectToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            largeToolStripMenuItem = new ToolStripMenuItem();
            smallToolStripMenuItem = new ToolStripMenuItem();
            normalToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            sendToolStripMenuItem = new ToolStripMenuItem();
            deleteAllToolStripMenuItem = new ToolStripMenuItem();
            saveFileDialog = new SaveFileDialog();
            statusStrip.SuspendLayout();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // textBox
            // 
            textBox.AcceptsReturn = true;
            textBox.AcceptsTab = true;
            textBox.AllowDrop = true;
            textBox.Dock = DockStyle.Fill;
            textBox.Location = new Point(0, 24);
            textBox.Multiline = true;
            textBox.Name = "textBox";
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.Size = new Size(389, 165);
            textBox.TabIndex = 0;
            textBox.TextChanged += TextBox_TextChanged;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel });
            statusStrip.Location = new Point(0, 189);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(389, 22);
            statusStrip.TabIndex = 1;
            statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            toolStripStatusLabel.Name = "toolStripStatusLabel";
            toolStripStatusLabel.Size = new Size(274, 17);
            toolStripStatusLabel.Text = "Einfügen in zuletzt aktive Anwendung: Strg + Enter";
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, viewToolStripMenuItem, helpToolStripMenuItem, sendToolStripMenuItem, deleteAllToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.ShowItemToolTips = true;
            menuStrip.Size = new Size(389, 24);
            menuStrip.TabIndex = 2;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveToolStripMenuItem, toolStripSeparator2, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 20);
            fileToolStripMenuItem.Text = "Datei";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.ShortcutKeyDisplayString = "Strg+S";
            saveToolStripMenuItem.Size = new Size(168, 22);
            saveToolStripMenuItem.Text = "Speichern";
            saveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(165, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.ShortcutKeyDisplayString = "Esc";
            exitToolStripMenuItem.Size = new Size(168, 22);
            exitToolStripMenuItem.Text = "Schließen";
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { undoToolStripMenuItem, toolStripSeparator1, cutToolStripMenuItem, copyToolStripMenuItem, pasteToolStripMenuItem, clearToolStripMenuItem, selectToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(75, 20);
            editToolStripMenuItem.Text = "Bearbeiten";
            editToolStripMenuItem.DropDownOpened += EditToolStripMenuItem_DropDownOpened;
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.ShortcutKeyDisplayString = "Strg+Z";
            undoToolStripMenuItem.Size = new Size(202, 22);
            undoToolStripMenuItem.Text = "Rückgängig";
            undoToolStripMenuItem.Click += UndoToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(199, 6);
            // 
            // cutToolStripMenuItem
            // 
            cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            cutToolStripMenuItem.ShortcutKeyDisplayString = "Strg+X";
            cutToolStripMenuItem.Size = new Size(202, 22);
            cutToolStripMenuItem.Text = "Ausschneiden";
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.ShortcutKeyDisplayString = "Strg+C";
            copyToolStripMenuItem.Size = new Size(202, 22);
            copyToolStripMenuItem.Text = "Kopieren";
            copyToolStripMenuItem.Click += CopyToolStripMenuItem_Click;
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.ShortcutKeyDisplayString = "Strg+V";
            pasteToolStripMenuItem.Size = new Size(202, 22);
            pasteToolStripMenuItem.Text = "Einfügen";
            pasteToolStripMenuItem.Click += PasteToolStripMenuItem_Click;
            // 
            // clearToolStripMenuItem
            // 
            clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            clearToolStripMenuItem.ShortcutKeyDisplayString = "Entf";
            clearToolStripMenuItem.Size = new Size(202, 22);
            clearToolStripMenuItem.Text = "Löschen";
            clearToolStripMenuItem.Click += ClearToolStripMenuItem_Click;
            // 
            // selectToolStripMenuItem
            // 
            selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            selectToolStripMenuItem.ShortcutKeyDisplayString = "Strg+A";
            selectToolStripMenuItem.Size = new Size(202, 22);
            selectToolStripMenuItem.Text = "Alles auswählen";
            selectToolStripMenuItem.Click += SelectToolStripMenuItem_Click;
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { largeToolStripMenuItem, smallToolStripMenuItem, normalToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new Size(59, 20);
            viewToolStripMenuItem.Text = "Ansicht";
            // 
            // largeToolStripMenuItem
            // 
            largeToolStripMenuItem.Name = "largeToolStripMenuItem";
            largeToolStripMenuItem.ShortcutKeyDisplayString = "Strg+Plus";
            largeToolStripMenuItem.Size = new Size(238, 22);
            largeToolStripMenuItem.Text = "Schrift vergrößern";
            largeToolStripMenuItem.Click += LargeToolStripMenuItem_Click;
            // 
            // smallToolStripMenuItem
            // 
            smallToolStripMenuItem.Name = "smallToolStripMenuItem";
            smallToolStripMenuItem.ShortcutKeyDisplayString = "Strg+Minus";
            smallToolStripMenuItem.Size = new Size(238, 22);
            smallToolStripMenuItem.Text = "Schrift verkleinern";
            smallToolStripMenuItem.Click += SmallToolStripMenuItem_Click;
            // 
            // normalToolStripMenuItem
            // 
            normalToolStripMenuItem.Name = "normalToolStripMenuItem";
            normalToolStripMenuItem.ShortcutKeyDisplayString = "Strg+0";
            normalToolStripMenuItem.Size = new Size(238, 22);
            normalToolStripMenuItem.Text = "Normalschrift";
            normalToolStripMenuItem.Click += NormalToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Hilfe";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.ShortcutKeyDisplayString = "F1";
            aboutToolStripMenuItem.Size = new Size(127, 22);
            aboutToolStripMenuItem.Text = "Über…";
            aboutToolStripMenuItem.Click += AboutToolStripMenuItem_Click;
            // 
            // sendToolStripMenuItem
            // 
            sendToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            sendToolStripMenuItem.Enabled = false;
            sendToolStripMenuItem.Image = Properties.Resources.Checkmark_16x;
            sendToolStripMenuItem.Name = "sendToolStripMenuItem";
            sendToolStripMenuItem.Size = new Size(74, 20);
            sendToolStripMenuItem.Text = "Senden";
            sendToolStripMenuItem.ToolTipText = "Senden";
            sendToolStripMenuItem.Click += SendToolStripMenuItem_Click;
            // 
            // deleteAllToolStripMenuItem
            // 
            deleteAllToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            deleteAllToolStripMenuItem.Enabled = false;
            deleteAllToolStripMenuItem.Image = Properties.Resources.DeleteHS;
            deleteAllToolStripMenuItem.Name = "deleteAllToolStripMenuItem";
            deleteAllToolStripMenuItem.Size = new Size(70, 20);
            deleteAllToolStripMenuItem.Text = "Leeren";
            deleteAllToolStripMenuItem.ToolTipText = "Alles löschen";
            deleteAllToolStripMenuItem.Click += DeleteAllToolStripMenuItem_Click;
            // 
            // saveFileDialog
            // 
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.FileName = "ClipMenu.txt";
            saveFileDialog.Filter = "Textdateien(*.txt)|*.txt|Alle Dateien (*.*)|*.*";
            // 
            // FrmClipEdit
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(389, 211);
            Controls.Add(textBox);
            Controls.Add(statusStrip);
            Controls.Add(menuStrip);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(259, 160);
            Name = "FrmClipEdit";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "ClipEdit";
            TopMost = true;
            FormClosing += FrmClipEdit_FormClosing;
            Load += FrmClipEdite_Load;
            Shown += FrmClipEdit_Shown;
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox;
        private StatusStrip statusStrip;
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem cutToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripMenuItem clearToolStripMenuItem;
        private ToolStripMenuItem selectToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem largeToolStripMenuItem;
        private ToolStripMenuItem smallToolStripMenuItem;
        private ToolStripMenuItem normalToolStripMenuItem;
        private ToolStripStatusLabel toolStripStatusLabel;
        private SaveFileDialog saveFileDialog;
        private ToolStripMenuItem deleteAllToolStripMenuItem;
        private ToolStripMenuItem sendToolStripMenuItem;
    }
}