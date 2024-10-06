namespace ClipMenu
{
    partial class ImageView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageView));
            pictureBox = new PictureBox();
            menuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            patientPhotoToolStripMenuItem = new ToolStripMenuItem();
            xmlImagePathToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox
            // 
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Location = new Point(0, 24);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(256, 250);
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(256, 24);
            menuStrip.TabIndex = 1;
            menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 20);
            fileToolStripMenuItem.Text = "Datei";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.ShortcutKeyDisplayString = "Esc";
            exitToolStripMenuItem.Size = new Size(144, 22);
            exitToolStripMenuItem.Text = "Beenden";
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { patientPhotoToolStripMenuItem, xmlImagePathToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(75, 20);
            editToolStripMenuItem.Text = "Bearbeiten";
            // 
            // patientPhotoToolStripMenuItem
            // 
            patientPhotoToolStripMenuItem.Name = "patientPhotoToolStripMenuItem";
            patientPhotoToolStripMenuItem.ShortcutKeyDisplayString = "Strg+I";
            patientPhotoToolStripMenuItem.Size = new Size(230, 22);
            patientPhotoToolStripMenuItem.Text = "Patientenbild erstellen";
            patientPhotoToolStripMenuItem.Click += PatientPhotoToolStripMenuItem_Click;
            // 
            // xmlImagePathToolStripMenuItem
            // 
            xmlImagePathToolStripMenuItem.Enabled = false;
            xmlImagePathToolStripMenuItem.Name = "xmlImagePathToolStripMenuItem";
            xmlImagePathToolStripMenuItem.Size = new Size(230, 22);
            xmlImagePathToolStripMenuItem.Text = "»ImagePatth« in XML-File: ";
            // 
            // ImageView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(256, 274);
            Controls.Add(pictureBox);
            Controls.Add(menuStrip);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip;
            Name = "ImageView";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            Text = "ImageView";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox;
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem patientPhotoToolStripMenuItem;
        private ToolStripMenuItem xmlImagePathToolStripMenuItem;
    }
}