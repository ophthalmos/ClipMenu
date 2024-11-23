using System.Drawing.Text;
using System.Text.RegularExpressions;

namespace ClipMenu
{
    public partial class FrmClipEdit : Form
    {
        public TextBox ClipEditTextBox { get { return textBox; } }

        private int searchStart = 0;
        private string searchString;
        private readonly string txtboxText;
        private readonly string fontLast = string.Empty;
        private bool caseChecked = false;
        private bool fontChanged = false;

        public FrmClipEdit(string clipText, string fontInfo, bool medistarRef = false)
        {
            InitializeComponent();
            txtboxText = Clipboard.ContainsText() ? clipText : "";
            if (!string.IsNullOrEmpty(txtboxText)) { backgroundWorker.RunWorkerAsync(); }

            if (medistarRef)
            {
                textBox.Font = new Font("Courier New", 14.0f, FontStyle.Bold);
                wordwrapToolStripMenuItem.Checked = textBox.WordWrap = false;
                textBox.SelectionStart = textBox.Text.Length; // Cursor ans Ende
            }
            else
            {
                textBox.Font = new FontConverter().ConvertFromInvariantString(fontInfo) as Font;
                textBox.SelectAll();
            }
            fontLast = textBox.Font.Name + ", " + (int)textBox.Font.Size + "pt" + (textBox.Font.Bold ? ", fett" : "") + (textBox.Font.Italic ? ", kursiv" : "");
        }

        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        { //Hier könnte eine aufwändige Operation durchgeführt werden., Das Problem ist hier allerdings die Wrap-Eigenschaft der TextBox.
            e.Result = txtboxText;
            textBox.Text = "Bitte warten…"; // funktioniert nur mit BackgroundWorker, deshalb doch sinnvoll
        }
        private void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null) { textBox.Text = (string)e.Result; }
        }

        private void FrmClipEdit_Load(object sender, EventArgs e)
        {
            Utilities.IsEditOpen = true; // s. Utilities (am Ende)
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            Left = (workingArea.Width - Size.Width) / 2;
            Top = (workingArea.Height - Size.Height) / 2 - 200;
        }

        private void FrmClipEdit_Shown(object sender, EventArgs e)
        {
            NativeMethods.SendMessage(textBox.Handle, NativeMethods.EM_SETMARGINS, NativeMethods.EC_LEFTMARGIN, 65536 + 3);
            textBox.Focus();
        }

        private void FrmClipEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            Utilities.IsEditOpen = false;
            NativeMethods.PostMessage(Application.OpenForms[0].Handle, NativeMethods.WM_CLIPEDIT_MSG, 0, 0); // dontHide false;
            if (fontChanged)
            {
                TaskDialogButton btnCustom = new TaskDialogCommandLinkButton("&Benutzerdefiniert", textBox.Font.Name + ", " + Math.Round(textBox.Font.Size, 0) + "pt" +
                    (textBox.Font.Bold ? ", fett" : "") + (textBox.Font.Italic ? ", kursiv" : ""));
                TaskDialogButton btnNormal = new TaskDialogCommandLinkButton("&Programmstandard", "Segoe UI, 12pt");
                TaskDialogPage page = new()
                {
                    Caption = "ClipMenu-Einstellungen",
                    Heading = "Welche Schriftart und -größe möchten Sie zukünftig in ClipEdit verwenden?",
                    Text = "Bisher " + fontLast,
                    AllowCancel = true,
                    Icon = new(new Icon(SystemIcons.Question, 32, 32)),
                    Buttons = { btnCustom, btnNormal, TaskDialogButton.Cancel }
                };
                TaskDialogButton result = TaskDialog.ShowDialog(this, page);
                if (result == btnCustom)
                {
                    FontFamily[] fontFamilies = new InstalledFontCollection().Families;
                    for (int j = 0; j < fontFamilies.Length; ++j)
                    {
                        if (fontFamilies[j].Name.Equals(textBox.Font.Name))
                        {
                            NativeMethods.PostMessage(Application.OpenForms[0].Handle, NativeMethods.WM_CLIPEDIT_FNT, j, 0);
                            break;
                        }
                    }
                    NativeMethods.PostMessage(Application.OpenForms[0].Handle, NativeMethods.WM_CLIPEDIT_FSZ, (int)Math.Round(textBox.Font.Size, 0), 0);
                    NativeMethods.PostMessage(Application.OpenForms[0].Handle, NativeMethods.WM_CLIPEDIT_STY, Utilities.GetIntFromBools(textBox.Font.Bold, textBox.Font.Italic), 0);
                }
                else if (result == btnNormal) { NativeMethods.PostMessage(Application.OpenForms[0].Handle, NativeMethods.WM_CLIPEDIT_FNT, -1, 0); } // ändert auch Schriftgröße
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Add | Keys.Control:
                case Keys.Oemplus | Keys.Control:
                    {
                        LargeToolStripMenuItem_Click(null, null);
                        return true;
                    }
                case Keys.Subtract | Keys.Control:
                case Keys.OemMinus | Keys.Control:
                    {
                        SmallToolStripMenuItem_Click(null, null);
                        return true;
                    }
                case Keys.D0 | Keys.Control:
                case Keys.NumPad0 | Keys.Control:
                    {
                        NormalToolStripMenuItem_Click(null, null);
                        return true;
                    }
                case Keys.Enter | Keys.Control:
                    {
                        SendContent();
                        return true;
                    }
                case Keys.A | Keys.Control:
                    {
                        textBox.SelectAll();
                        return true;
                    }
                case Keys.D | Keys.Control:
                    {
                        FontDialogToolStripMenuItem_Click(null, null);
                        return true;
                    }
                case Keys.F | Keys.Control:
                    {
                        SearchForToolStripMenuItem_Click(null, null);
                        return true;
                    }
                case Keys.Z | Keys.Control:
                    {
                        UndoToolStripMenuItem_Click(null, null);
                        return true;
                    }
                case Keys.F1:
                    {
                        AboutToolStripMenuItem_Click(null, null);
                        return true;
                    }
                case Keys.F3:
                    {
                        SearchFor_Find(searchString, caseChecked);
                        return true;
                    }
                case Keys.F5:
                    {
                        WordwrapToolStripMenuItem_Click(null, null);
                        return true;
                    }
                case Keys.Escape:
                    {
                        Close();
                        return true;
                    }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void SendContent()
        {
            try
            {
                Clipboard.Clear();
                if (Application.OpenForms[0] is FrmClipMenu form) { form.IgnoreClipboardChange = true; }
                if (!Utilities.SetClipboardText(textBox.Text)) { Utilities.ErrorMsgTaskDlg(Handle, "Es ist ein Fehler aufgetreten.\nVersuchen Sie es noch einmal."); }
                else
                {
                    Hide(); // Hiding is equivalent to setting the Visible property to false
                    Application.DoEvents();
                    if (!NativeMethods.SendKeysPaste())
                    {
                        Utilities.ErrorMsgTaskDlg(Handle, "Es ist ein Fehler aufgetreten.\nVersuchen Sie es noch einmal.");
                        Show();
                    }
                    else { Close(); }
                }
            }
            catch (Exception ex) when (ex is NullReferenceException) { }
        }

        private void LargeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox.Font.Size < 36) { textBox.Font = new Font(textBox.Font.FontFamily, textBox.Font.Size + 2.0F); }
        }

        private void SmallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox.Font.Size > 8) { textBox.Font = new Font(textBox.Font.FontFamily, textBox.Font.Size - 2.0F); }
        }

        private void NormalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Font = new Font(textBox.Font.FontFamily, 10);
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = "© " + Utilities.GetBuildDate().ToString("yyyy") + " Wilhelm Happe";
            TaskDialogIcon taskDialogIcon = new TaskDialogIcon(Icon);
            TaskDialog.ShowDialog(Handle, new TaskDialogPage() { Caption = Application.ProductName, SizeToContent = true, Text = text, Icon = taskDialogIcon, AllowCancel = true, Buttons = { TaskDialogButton.OK } });
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e) { textBox.Cut(); }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text)) { textBox.Paste(); }
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e) { textBox.Copy(); }

        private void ClearToolStripMenuItem_Click(object sender, EventArgs e) { textBox.SelectedText = ""; }

        private void SelectToolStripMenuItem_Click(object sender, EventArgs e) { textBox.SelectAll(); }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox.CanUndo) { textBox.Undo(); } else { Console.Beep(); }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK && saveFileDialog.FileName.Length > 0)
            {
                using StreamWriter writer = new(saveFileDialog.FileName);
                writer.Write(textBox.Text);
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) { Close(); }

        private void DeleteAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Clear();
            textBox.Font = new Font(textBox.Font.FontFamily, 10);
        }

        private void SendToolStripMenuItem_Click(object sender, EventArgs e) { SendContent(); }

        private void EditToolStripMenuItem_DropDownOpened(object sender, EventArgs e)
        {
            cutToolStripMenuItem.Enabled = copyToolStripMenuItem.Enabled = selectToolStripMenuItem.Enabled = textBox.Text.Length > 0;
            clearToolStripMenuItem.Enabled = textBox.SelectedText.Length > 0;
            pasteToolStripMenuItem.Enabled = Clipboard.ContainsText();
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            deleteAllToolStripMenuItem.Enabled = sendToolStripMenuItem.Enabled = textBox.Text.Length > 0;
            searchStart = textBox.SelectionStart;
        }

        private void WordwrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.WordWrap = !textBox.WordWrap;
            wordwrapToolStripMenuItem.Checked = textBox.WordWrap;
        }

        private void SearchForToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using TextBoxSearch f = new(searchString, caseChecked);
            f.ShowDialog();
        }
        public void SearchFor_Find(string searchFor, bool checkCase)
        {
            bool success = false;
            caseChecked = checkCase;
            searchString = string.IsNullOrEmpty(searchFor) ? searchString : searchFor;
            if (string.IsNullOrEmpty(searchString)) { return; }
            else { Utilities.SearchHistory.Insert(0, searchString); }
            Regex rgx = new(searchString, caseChecked ? RegexOptions.None : RegexOptions.IgnoreCase);
            if (searchStart > textBox.Text.Length - 1) { searchStart = -1; }
            Match match1 = rgx.Match(textBox.Text, searchStart + 1);
            if (match1.Success)
            {
                searchStart = match1.Index;
                success = true;
            }
            else if (searchStart > 0) // von vorne weitersuchen
            {
                Match match2 = rgx.Match(textBox.Text);
                if (match2.Success)
                {
                    searchStart = match2.Index;
                    success = true;
                }
            }
            if (searchStart >= 0)
            {
                textBox.SelectionStart = searchStart;
                textBox.SelectionLength = success ? searchString.Length : 0;
                textBox.Select();
                textBox.ScrollToCaret();
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e) { searchStart = textBox.SelectionStart; }
        private void TextBox_MouseClick(object sender, MouseEventArgs e) { searchStart = textBox.SelectionStart; }

        private void FontDialogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog.Font = textBox.Font;
            string fontName = fontDialog.Font.FontFamily.Name;
            int fontSize = (int)fontDialog.Font.SizeInPoints;
            int fontStyle = Utilities.GetIntFromBools(textBox.Font.Bold, textBox.Font.Italic);
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                textBox.Font = fontDialog.Font;
                int style = Utilities.GetIntFromBools(textBox.Font.Bold, textBox.Font.Italic);
                if ((textBox.Font.Name != fontName || (int)textBox.Font.SizeInPoints != fontSize || style != fontStyle)
                    && Utilities.IsInFontCollection(textBox.Font.Name)) { fontChanged = true; }
            }
        }

        private void ViewToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            largeToolStripMenuItem.Enabled = textBox.Font.Size < 36;
            smallToolStripMenuItem.Enabled = textBox.Font.Size > 8;
            normalToolStripMenuItem.Enabled = textBox.Font.Size != 12;
        }

    }
}
