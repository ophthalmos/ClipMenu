namespace ClipMenu
{
    public partial class TextBoxSearch : Form
    {
        private bool checkCaseChecked = false;

        public TextBoxSearch(string searchString, bool caseChecked)
        {
            InitializeComponent();
            cbxSearch.Text = searchString;
            checkCase.Checked = caseChecked; // checkCaseChecked erhält true im CheckCase_CheckedChanged-Event
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (Utilities.IsEditOpen && Application.OpenForms["FrmClipEdit"] is FrmClipEdit frmClipEdit && cbxSearch.Text.Length > 0)
            {
                string searchText = cbxSearch.Text;
                frmClipEdit.SearchFor_Find(searchText, checkCaseChecked);
                frmClipEdit.ClipEditTextBox.Focus();
                Close();
            }
        }

        private void CheckCase_CheckedChanged(object sender, EventArgs e)
        {
            checkCaseChecked = checkCase.Checked;
        }
    }
}
