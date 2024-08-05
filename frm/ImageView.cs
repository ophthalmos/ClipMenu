using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ClipMenu
{
    public partial class ImageView : Form
    {
        public ImageView(Image image)
        {
            InitializeComponent();
            pictureBox.ClientSize = new Size(image.Width, image.Height);
            pictureBox.Image = image;
            Text = pictureBox.Width.ToString() + "x" + pictureBox.Height.ToString();
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
                string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Patientenbild.jpg");
                new Bitmap(new Bitmap(pictureBox.Image), new Size(newWidth, newHeight)).Save(path, ImageFormat.Jpeg);
                Utilities.ErrorMsgTaskDlg(Handle, path + "," + Environment.NewLine + newWidth + " × " + newHeight + " Pixel, erfolgreich gespeichert.", TaskDialogIcon.Information);
                Close();
            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException || ex is ExternalException) { Utilities.ErrorMsgTaskDlg(Handle, ex.Message); }

        }

    }
}

