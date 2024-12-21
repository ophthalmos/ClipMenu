namespace ClipMenu
{
    public partial class ImagePath : Form
    {
        public string Result => textBox.Text;

        public ImagePath(string imagePath)
        {
            InitializeComponent();
            textBox.Text = folderBrowserDialog.InitialDirectory = Path.Exists(imagePath) ? imagePath : Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.Personal; // My Documents
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK) { textBox.Text = folderBrowserDialog.SelectedPath; }
        }
    }
}
