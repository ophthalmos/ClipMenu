namespace ClipMenu
{
    partial class FrmClipCalc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmClipCalc));
            pnlDisplayBkgr = new Panel();
            tbDisplay = new TextBox();
            toolTip = new ToolTip(components);
            trackBar = new TrackBar();
            btnStd7 = new Button();
            btnStd8 = new Button();
            btnStd9 = new Button();
            btnStdPlus = new Button();
            tableLayoutPanel = new TableLayoutPanel();
            btnStd4 = new Button();
            btnStd5 = new Button();
            btnStd6 = new Button();
            btnStdMinus = new Button();
            btnStdClear = new Button();
            btnStd1 = new Button();
            btnStd2 = new Button();
            btnStd3 = new Button();
            btnStdMultiply = new Button();
            btnStdBack = new Button();
            btnStd0 = new Button();
            btnStdComma = new Button();
            btnStdConvert = new Button();
            btnStdSlash = new Button();
            btnStdCalc = new Button();
            btnStdPower = new Button();
            labelTrackbar = new Label();
            btnSend = new Button();
            panelHorizLine = new Panel();
            pnlDisplayBkgr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar).BeginInit();
            tableLayoutPanel.SuspendLayout();
            SuspendLayout();
            // 
            // pnlDisplayBkgr
            // 
            pnlDisplayBkgr.BackColor = Color.Transparent;
            pnlDisplayBkgr.Controls.Add(tbDisplay);
            pnlDisplayBkgr.Location = new Point(0, 0);
            pnlDisplayBkgr.Name = "pnlDisplayBkgr";
            pnlDisplayBkgr.Size = new Size(204, 48);
            pnlDisplayBkgr.TabIndex = 0;
            // 
            // tbDisplay
            // 
            tbDisplay.BackColor = Color.MintCream;
            tbDisplay.Font = new Font("Segoe UI", 11F);
            tbDisplay.Location = new Point(2, 9);
            tbDisplay.Name = "tbDisplay";
            tbDisplay.Size = new Size(200, 27);
            tbDisplay.TabIndex = 1;
            tbDisplay.TextChanged += TbDisplay_TextChanged;
            tbDisplay.KeyDown += TbDisplay_KeyDown;
            // 
            // trackBar
            // 
            trackBar.AutoSize = false;
            trackBar.LargeChange = 2;
            trackBar.Location = new Point(0, 213);
            trackBar.Margin = new Padding(0);
            trackBar.Minimum = -1;
            trackBar.Name = "trackBar";
            trackBar.Size = new Size(124, 46);
            trackBar.TabIndex = 21;
            toolTip.SetToolTip(trackBar, "F9-F12");
            trackBar.Value = -1;
            trackBar.ValueChanged += TrackBar_ValueChanged;
            // 
            // btnStd7
            // 
            btnStd7.Dock = DockStyle.Fill;
            btnStd7.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStd7.Location = new Point(0, 0);
            btnStd7.Margin = new Padding(0);
            btnStd7.Name = "btnStd7";
            btnStd7.Size = new Size(40, 40);
            btnStd7.TabIndex = 2;
            btnStd7.Text = "7";
            btnStd7.UseVisualStyleBackColor = true;
            btnStd7.Click += BtnStd7_Click;
            // 
            // btnStd8
            // 
            btnStd8.Dock = DockStyle.Fill;
            btnStd8.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStd8.Location = new Point(40, 0);
            btnStd8.Margin = new Padding(0);
            btnStd8.Name = "btnStd8";
            btnStd8.Size = new Size(40, 40);
            btnStd8.TabIndex = 3;
            btnStd8.Text = "8";
            btnStd8.UseVisualStyleBackColor = true;
            btnStd8.Click += BtnStd8_Click;
            // 
            // btnStd9
            // 
            btnStd9.Dock = DockStyle.Fill;
            btnStd9.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStd9.Location = new Point(80, 0);
            btnStd9.Margin = new Padding(0);
            btnStd9.Name = "btnStd9";
            btnStd9.Size = new Size(40, 40);
            btnStd9.TabIndex = 4;
            btnStd9.Text = "9";
            btnStd9.UseVisualStyleBackColor = true;
            btnStd9.Click += BtnStd9_Click;
            // 
            // btnStdPlus
            // 
            btnStdPlus.Dock = DockStyle.Fill;
            btnStdPlus.Font = new Font("Segoe UI", 10F);
            btnStdPlus.Location = new Point(122, 0);
            btnStdPlus.Margin = new Padding(0);
            btnStdPlus.Name = "btnStdPlus";
            btnStdPlus.Size = new Size(40, 40);
            btnStdPlus.TabIndex = 14;
            btnStdPlus.Text = "➕";
            btnStdPlus.UseVisualStyleBackColor = true;
            btnStdPlus.Click += BtnStdPlus_Click;
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.AutoSize = true;
            tableLayoutPanel.ColumnCount = 7;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 2F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 2F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.Controls.Add(btnStdPlus, 4, 0);
            tableLayoutPanel.Controls.Add(btnStd9, 2, 0);
            tableLayoutPanel.Controls.Add(btnStd8, 1, 0);
            tableLayoutPanel.Controls.Add(btnStd7, 0, 0);
            tableLayoutPanel.Controls.Add(btnStd4, 0, 1);
            tableLayoutPanel.Controls.Add(btnStd5, 1, 1);
            tableLayoutPanel.Controls.Add(btnStd6, 2, 1);
            tableLayoutPanel.Controls.Add(btnStdMinus, 4, 1);
            tableLayoutPanel.Controls.Add(btnStdClear, 6, 1);
            tableLayoutPanel.Controls.Add(btnStd1, 0, 2);
            tableLayoutPanel.Controls.Add(btnStd2, 1, 2);
            tableLayoutPanel.Controls.Add(btnStd3, 2, 2);
            tableLayoutPanel.Controls.Add(btnStdMultiply, 4, 2);
            tableLayoutPanel.Controls.Add(btnStdBack, 6, 2);
            tableLayoutPanel.Controls.Add(btnStd0, 0, 3);
            tableLayoutPanel.Controls.Add(btnStdComma, 1, 3);
            tableLayoutPanel.Controls.Add(btnStdConvert, 2, 3);
            tableLayoutPanel.Controls.Add(btnStdSlash, 4, 3);
            tableLayoutPanel.Controls.Add(btnStdCalc, 6, 3);
            tableLayoutPanel.Controls.Add(btnStdPower, 6, 0);
            tableLayoutPanel.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tableLayoutPanel.Location = new Point(0, 45);
            tableLayoutPanel.Margin = new Padding(0);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 4;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel.Size = new Size(204, 160);
            tableLayoutPanel.TabIndex = 1;
            // 
            // btnStd4
            // 
            btnStd4.Dock = DockStyle.Fill;
            btnStd4.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStd4.Location = new Point(0, 40);
            btnStd4.Margin = new Padding(0);
            btnStd4.Name = "btnStd4";
            btnStd4.Size = new Size(40, 40);
            btnStd4.TabIndex = 5;
            btnStd4.Text = "4";
            btnStd4.UseVisualStyleBackColor = true;
            btnStd4.Click += BtnStd4_Click;
            // 
            // btnStd5
            // 
            btnStd5.Dock = DockStyle.Fill;
            btnStd5.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStd5.Location = new Point(40, 40);
            btnStd5.Margin = new Padding(0);
            btnStd5.Name = "btnStd5";
            btnStd5.Size = new Size(40, 40);
            btnStd5.TabIndex = 6;
            btnStd5.Text = "5";
            btnStd5.UseVisualStyleBackColor = true;
            btnStd5.Click += BtnStd5_Click;
            // 
            // btnStd6
            // 
            btnStd6.Dock = DockStyle.Fill;
            btnStd6.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStd6.Location = new Point(80, 40);
            btnStd6.Margin = new Padding(0);
            btnStd6.Name = "btnStd6";
            btnStd6.Size = new Size(40, 40);
            btnStd6.TabIndex = 7;
            btnStd6.Text = "6";
            btnStd6.UseVisualStyleBackColor = true;
            btnStd6.Click += BtnStd6_Click;
            // 
            // btnStdMinus
            // 
            btnStdMinus.Dock = DockStyle.Fill;
            btnStdMinus.Font = new Font("Segoe UI", 10F);
            btnStdMinus.Location = new Point(122, 40);
            btnStdMinus.Margin = new Padding(0);
            btnStdMinus.Name = "btnStdMinus";
            btnStdMinus.Size = new Size(40, 40);
            btnStdMinus.TabIndex = 16;
            btnStdMinus.Text = "➖";
            btnStdMinus.UseVisualStyleBackColor = true;
            btnStdMinus.Click += BtnStdMinus_Click;
            // 
            // btnStdClear
            // 
            btnStdClear.Dock = DockStyle.Fill;
            btnStdClear.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStdClear.ForeColor = Color.IndianRed;
            btnStdClear.Location = new Point(164, 40);
            btnStdClear.Margin = new Padding(0);
            btnStdClear.Name = "btnStdClear";
            btnStdClear.Size = new Size(40, 40);
            btnStdClear.TabIndex = 17;
            btnStdClear.Text = "C";
            btnStdClear.UseVisualStyleBackColor = true;
            btnStdClear.Click += BtnStdClear_Click;
            // 
            // btnStd1
            // 
            btnStd1.Dock = DockStyle.Fill;
            btnStd1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStd1.Location = new Point(0, 80);
            btnStd1.Margin = new Padding(0);
            btnStd1.Name = "btnStd1";
            btnStd1.Size = new Size(40, 40);
            btnStd1.TabIndex = 8;
            btnStd1.Text = "1";
            btnStd1.UseVisualStyleBackColor = true;
            btnStd1.Click += BtnStd1_Click;
            // 
            // btnStd2
            // 
            btnStd2.Dock = DockStyle.Fill;
            btnStd2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStd2.Location = new Point(40, 80);
            btnStd2.Margin = new Padding(0);
            btnStd2.Name = "btnStd2";
            btnStd2.Size = new Size(40, 40);
            btnStd2.TabIndex = 9;
            btnStd2.Text = "2";
            btnStd2.UseVisualStyleBackColor = true;
            btnStd2.Click += BtnStd2_Click;
            // 
            // btnStd3
            // 
            btnStd3.Dock = DockStyle.Fill;
            btnStd3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStd3.Location = new Point(80, 80);
            btnStd3.Margin = new Padding(0);
            btnStd3.Name = "btnStd3";
            btnStd3.Size = new Size(40, 40);
            btnStd3.TabIndex = 10;
            btnStd3.Text = "3";
            btnStd3.UseVisualStyleBackColor = true;
            btnStd3.Click += BtnStd3_Click;
            // 
            // btnStdMultiply
            // 
            btnStdMultiply.Dock = DockStyle.Fill;
            btnStdMultiply.Location = new Point(122, 80);
            btnStdMultiply.Margin = new Padding(0);
            btnStdMultiply.Name = "btnStdMultiply";
            btnStdMultiply.Size = new Size(40, 40);
            btnStdMultiply.TabIndex = 18;
            btnStdMultiply.Text = "✖";
            btnStdMultiply.UseVisualStyleBackColor = true;
            btnStdMultiply.Click += BtnStdMultiply_Click;
            // 
            // btnStdBack
            // 
            btnStdBack.Dock = DockStyle.Fill;
            btnStdBack.ForeColor = Color.RoyalBlue;
            btnStdBack.Location = new Point(164, 80);
            btnStdBack.Margin = new Padding(0);
            btnStdBack.Name = "btnStdBack";
            btnStdBack.Size = new Size(40, 40);
            btnStdBack.TabIndex = 19;
            btnStdBack.Text = "🔙";
            btnStdBack.UseVisualStyleBackColor = true;
            btnStdBack.Click += BtnStdBack_Click;
            // 
            // btnStd0
            // 
            btnStd0.Dock = DockStyle.Fill;
            btnStd0.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStd0.Location = new Point(0, 120);
            btnStd0.Margin = new Padding(0);
            btnStd0.Name = "btnStd0";
            btnStd0.Size = new Size(40, 40);
            btnStd0.TabIndex = 11;
            btnStd0.Text = "0";
            btnStd0.UseVisualStyleBackColor = true;
            btnStd0.Click += BtnStd0_Click;
            // 
            // btnStdComma
            // 
            btnStdComma.Dock = DockStyle.Fill;
            btnStdComma.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStdComma.Location = new Point(40, 120);
            btnStdComma.Margin = new Padding(0);
            btnStdComma.Name = "btnStdComma";
            btnStdComma.Size = new Size(40, 40);
            btnStdComma.TabIndex = 12;
            btnStdComma.Text = ",";
            btnStdComma.UseVisualStyleBackColor = true;
            btnStdComma.Click += BtnStdComma_Click;
            // 
            // btnStdConvert
            // 
            btnStdConvert.Dock = DockStyle.Fill;
            btnStdConvert.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStdConvert.Location = new Point(80, 120);
            btnStdConvert.Margin = new Padding(0);
            btnStdConvert.Name = "btnStdConvert";
            btnStdConvert.Size = new Size(40, 40);
            btnStdConvert.TabIndex = 13;
            btnStdConvert.Text = "±";
            btnStdConvert.UseVisualStyleBackColor = true;
            btnStdConvert.Click += BtnStdConvert_Click;
            // 
            // btnStdSlash
            // 
            btnStdSlash.Dock = DockStyle.Fill;
            btnStdSlash.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStdSlash.Location = new Point(122, 120);
            btnStdSlash.Margin = new Padding(0);
            btnStdSlash.Name = "btnStdSlash";
            btnStdSlash.Size = new Size(40, 40);
            btnStdSlash.TabIndex = 20;
            btnStdSlash.Text = "➗";
            btnStdSlash.UseVisualStyleBackColor = true;
            btnStdSlash.Click += BtnStdSlash_Click;
            // 
            // btnStdCalc
            // 
            btnStdCalc.Dock = DockStyle.Fill;
            btnStdCalc.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStdCalc.ForeColor = Color.SeaGreen;
            btnStdCalc.Location = new Point(164, 120);
            btnStdCalc.Margin = new Padding(0);
            btnStdCalc.Name = "btnStdCalc";
            btnStdCalc.Size = new Size(40, 40);
            btnStdCalc.TabIndex = 0;
            btnStdCalc.Text = "\U0001f7f0";
            btnStdCalc.UseVisualStyleBackColor = true;
            btnStdCalc.Click += BtnStdCalc_Click;
            // 
            // btnStdPower
            // 
            btnStdPower.Dock = DockStyle.Fill;
            btnStdPower.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStdPower.Location = new Point(164, 0);
            btnStdPower.Margin = new Padding(0);
            btnStdPower.Name = "btnStdPower";
            btnStdPower.Size = new Size(40, 40);
            btnStdPower.TabIndex = 15;
            btnStdPower.Text = "⋀";
            btnStdPower.UseVisualStyleBackColor = true;
            btnStdPower.Click += btnStdPower_Click;
            // 
            // labelTrackbar
            // 
            labelTrackbar.BackColor = Color.Transparent;
            labelTrackbar.Font = new Font("Segoe UI", 9F);
            labelTrackbar.Location = new Point(3, 229);
            labelTrackbar.Margin = new Padding(0);
            labelTrackbar.Name = "labelTrackbar";
            labelTrackbar.Size = new Size(121, 15);
            labelTrackbar.TabIndex = 3;
            labelTrackbar.Text = "Keine Rundung";
            labelTrackbar.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnSend
            // 
            btnSend.DialogResult = DialogResult.OK;
            btnSend.Location = new Point(122, 214);
            btnSend.Margin = new Padding(0);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(82, 38);
            btnSend.TabIndex = 22;
            btnSend.Text = "Einfügen";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += BtnSend_Click;
            // 
            // panelHorizLine
            // 
            panelHorizLine.BorderStyle = BorderStyle.FixedSingle;
            panelHorizLine.Location = new Point(1, 209);
            panelHorizLine.Name = "panelHorizLine";
            panelHorizLine.Size = new Size(202, 2);
            panelHorizLine.TabIndex = 5;
            // 
            // FrmClipCalc
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(205, 255);
            Controls.Add(panelHorizLine);
            Controls.Add(btnSend);
            Controls.Add(labelTrackbar);
            Controls.Add(trackBar);
            Controls.Add(tableLayoutPanel);
            Controls.Add(pnlDisplayBkgr);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmClipCalc";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            Text = "ClipCalc";
            TopMost = true;
            HelpButtonClicked += Calculator_HelpButtonClicked;
            FormClosing += FrmClipCalc_FormClosing;
            Load += Calculator_Load;
            Shown += FrmClipCalc_Shown;
            HelpRequested += Calculator_HelpRequested;
            pnlDisplayBkgr.ResumeLayout(false);
            pnlDisplayBkgr.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar).EndInit();
            tableLayoutPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlDisplayBkgr;
        private TextBox tbDisplay;
        private ToolTip toolTip;
        private TableLayoutPanel tableLayoutPanel;
        private Button btnStd7;
        private Button btnStdPlus;
        private Button btnStd9;
        private Button btnStd8;
        private Button btnStd4;
        private Button btnStd5;
        private Button btnStd6;
        private Button btnStdMinus;
        private Button btnStdClear;
        private Button btnStd1;
        private Button btnStd2;
        private Button btnStd3;
        private Button btnStdMultiply;
        private Button btnStd0;
        private Button btnStdComma;
        private Button btnStdConvert;
        private Button btnStdSlash;
        private Button btnStdCalc;
        private Button btnStdBack;
        private Button btnStdPower;
        private TrackBar trackBar;
        private Label labelTrackbar;
        private Button btnSend;
        private Panel panelHorizLine;
    }
}