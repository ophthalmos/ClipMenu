namespace ClipMenu
{
    public partial class InputBox : Form
    {
        public TextBox InputTextBox { get { return textBox; } }

        public InputBox(string title, string prompt, string defaultValue = "")
        {
            InitializeComponent();
            Text = title;
            label.Text = prompt;
            textBox.Text = defaultValue;   
        }
    }
}
