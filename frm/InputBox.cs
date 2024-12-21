namespace ClipMenu
{
    public partial class InputBox : Form
    {
        public TextBox InputTextBox => textBox;

        public InputBox(string title, string prompt, string defaultValue = "")
        {
            InitializeComponent();
            Text = title;
            label.Text = prompt;
            textBox.Text = defaultValue;
        }

        private void InputBox_Load(object sender, EventArgs e)
        {
            NativeMethods.SendMessage(textBox.Handle, NativeMethods.EM_SETMARGINS, NativeMethods.EC_LEFTMARGIN, 65536 + 3);
        }
    }
}
