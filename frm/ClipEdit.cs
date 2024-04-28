using System.Reflection;

namespace ClipMenu
{
    public partial class FrmClipEdit : Form
    {
        public TextBox ClipEditTextBox { get { return textBox; } }

        public FrmClipEdit()
        {
            InitializeComponent();
        }

        private void FrmClipEdite_Load(object sender, EventArgs e)
        {
            Utilities.IsEditOpen = true; // s. Utilities (am Ende)
            Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
            Left = (workingArea.Width - Size.Width) / 2;
            Top = (workingArea.Height - Size.Height) / 2 - 200;
            textBox.Text = Clipboard.ContainsText() ? Clipboard.GetText() : "";
        }

        private void FrmClipEdit_Shown(object sender, EventArgs e)
        {
            NativeMethods.SendMessage(textBox.Handle, NativeMethods.EM_SETMARGINS, NativeMethods.EC_LEFTMARGIN, 65536 + 3);
            if (Clipboard.ContainsText())
            {
                string data = Clipboard.GetText();
                if (!string.IsNullOrEmpty(data)) { textBox.Text = data; }
            }
            textBox.Focus();
            textBox.SelectionStart = textBox.Text.Length;
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
                Hide(); // Hiding is equivalent to setting the Visible property to false
                Clipboard.Clear();
                if (Application.OpenForms[0] is FrmClipMenu form) { form.IgnoreClipboardChange = true; }
                if (!Utilities.SetClipboardText(textBox.Text))
                {
                    Utilities.ErrorMsgTaskDlg(Handle, "Es ist ein Fehler aufgetreten.\nVersuchen Sie es noch einmal.");
                    Show();
                }
                else
                {
                    if (NativeMethods.SetForegroundWindow(NativeMethods.lastActiveWindow)) { NativeMethods.SendKeysPaste(); } //SendKeys.SendWait("^(v)");
                    Close();
                }
            }
            catch (Exception ex) when (ex is NullReferenceException) { }
        }

        private void LargeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox.Font.Size < 36) { textBox.Font = new Font(textBox.Font.FontFamily, textBox.Font.Size + 2); }
        }

        private void SmallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox.Font.Size > 8) { textBox.Font = new Font(textBox.Font.FontFamily, textBox.Font.Size - 2); }
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

        private void FrmClipEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            Utilities.IsEditOpen = false;
            NativeMethods.PostMessage(Application.OpenForms[0].Handle, NativeMethods.WM_CLIPEDIT_MSG, 0, 0); // dontHide false;
        }

        private void DeleteAllToolStripMenuItem_Click(object sender, EventArgs e) { textBox.Clear(); }

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
        }
    }
}
