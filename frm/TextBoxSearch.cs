namespace ClipMenu
{
    public partial class TextBoxSearch : Form
    {
        public TextBoxSearch(string searchString, bool caseChecked)
        {
            InitializeComponent();
            checkCase.Checked = caseChecked;
            cbxSearch.Items.Clear();
            cbxSearch.Text = searchString;
            Utilities.SearchHistory ??= []; // Liste initialisieren, wenn noch nicht geschehen
            if (Utilities.SearchHistory.Count > 0) { cbxSearch.Items.AddRange(Utilities.SearchHistory.Distinct().ToArray()); }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (Utilities.IsEditOpen && Application.OpenForms["FrmClipEdit"] is FrmClipEdit frmClipEdit && cbxSearch.Text.Length > 0)
            {
                frmClipEdit.SearchFor_Find(cbxSearch.Text, checkCase.Checked);
                frmClipEdit.ClipEditTextBox.Focus();
                Close();
            }
        }
/* IsTextSearchCaseSensitive gibt es in WPF - nicht in WinForms - DropDown/Autocomplete leider immer CaseInsensitive! */
    }
}
