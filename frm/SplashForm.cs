using System.Text.RegularExpressions;

namespace ClipMenu
{
    public partial class SplashForm : Form
    {
        public SplashForm(string text, int timerInterval = 6500, Color? background = null, bool centerParent = false)
        {
            InitializeComponent();
            timer.Interval = timerInterval;
            splashLabel.Text = Utilities.LimitedSubstr(Regex.Replace(Regex.Replace(text, @"\t|\r\n?|\n", " "), @"[ ]{2,}", " ").Trim(), 100);
            Font segoeUIFont = new("Segoe UI", 10, FontStyle.Regular);
            Size textSize = TextRenderer.MeasureText(splashLabel.Text, segoeUIFont);
            Width = textSize.Width + 30;
            Height = textSize.Height + 6;
            splashLabel.BackColor = (Color)(background == null ? Color.WhiteSmoke : background);
            if (centerParent) { StartPosition = FormStartPosition.CenterParent;             }
            else { StartPosition = FormStartPosition.CenterScreen; }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            Close();
        }

    }
}