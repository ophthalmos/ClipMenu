using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ClipMenu
{
    public partial class ImageView : Form
    {
        private readonly string defaultPath = string.Empty;

        public ImageView(Image image, string pictureUserPath)
        {
            InitializeComponent();
            pictureBox.ClientSize = new Size(image.Width, image.Height);
            pictureBox.Image = image;
            Text = pictureBox.Width.ToString() + "x" + pictureBox.Height.ToString();
            defaultPath = !string.IsNullOrEmpty(pictureUserPath) ? pictureUserPath : Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            ToolStripControlHost host = new(new TextBox())
            {
                AutoSize = true,
                Text = pictureUserPath,
                BackColor = SystemColors.Window,
                Enabled = false
            };
            editToolStripMenuItem.DropDownItems.Add(host);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            else if (keyData == (Keys.I | Keys.Control))
            {
                PatientPhotoToolStripMenuItem_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) { Close(); }

        private void PatientPhotoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int newWidth, newHeight;
            string path;
            if (pictureBox.Image.Width / pictureBox.Height > 0.75) // Bild ist zu breit
            {
                newHeight = 320;
                newWidth = pictureBox.Image.Width * 320 / pictureBox.Image.Height;
            }
            else
            {
                newWidth = 240;
                newHeight = pictureBox.Image.Height * 240 / pictureBox.Image.Width;
            }
            try
            {
                if (!Directory.Exists(defaultPath))
                {
                    Utilities.ErrorMsgTaskDlg(Handle, defaultPath + Environment.NewLine + "Der Zielordner wurde nicht gefunden.", TaskDialogIcon.Information);
                    return;
                }
                else { path = Path.Combine(defaultPath, "Patientenbild.jpg"); }
                new Bitmap(new Bitmap(pictureBox.Image), new Size(newWidth, newHeight)).Save(path, ImageFormat.Jpeg);
                Utilities.ErrorMsgTaskDlg(Handle, path + "," + Environment.NewLine + newWidth + " × " + newHeight + " Pixel, erfolgreich gespeichert.", TaskDialogIcon.Information);
                Close();
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException || ex is ExternalException) { Utilities.ErrorMsgTaskDlg(Handle, ex.Message); }

        }

    }
}

