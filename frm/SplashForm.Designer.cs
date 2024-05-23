namespace ClipMenu
{
    partial class SplashForm
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
            components = new System.ComponentModel.Container();
            splashLabel = new Label();
            timer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // splashLabel
            // 
            splashLabel.BackColor = Color.WhiteSmoke;
            splashLabel.BorderStyle = BorderStyle.FixedSingle;
            splashLabel.Dock = DockStyle.Fill;
            splashLabel.Font = new Font("Segoe UI", 10F);
            splashLabel.Location = new Point(0, 0);
            splashLabel.Name = "splashLabel";
            splashLabel.Size = new Size(233, 35);
            splashLabel.TabIndex = 0;
            splashLabel.Text = "splashLabel";
            splashLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // timer
            // 
            timer.Enabled = true;
            timer.Interval = 6500;
            timer.Tick += Timer_Tick;
            // 
            // SplashForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(233, 35);
            Controls.Add(splashLabel);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MaximumSize = new Size(1167, 69);
            MinimizeBox = false;
            MinimumSize = new Size(233, 35);
            Name = "SplashForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label splashLabel;
        private System.Windows.Forms.Timer timer;
    }
}