namespace ClipMenu
{
    public partial class ImageView : Form
    {
        public ImageView(Image image)
        {
            InitializeComponent();
            pictureBox.ClientSize = new Size(image.Width, image.Height);
            pictureBox.Image = image;
            Text = pictureBox.Height.ToString() + "x" + pictureBox.Height.ToString();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
