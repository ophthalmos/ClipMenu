using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ClipMenu
{
    public partial class FrmClipCalc : Form
    {
        public string Result { get { return tbDisplay.Text; } }
        public int DecPlaces { get { return trackBar.Value; } }

        private bool resultIsDisplayed = false; // wird benötigt um das Runden von Ausdrücken zu verhindern
        private string prevCalcString = "";
        private double res1 = 0.0;
        private readonly CalcClass calcInstance = new();
        private bool shownTaskDialog = false;

        public FrmClipCalc(string clipString, int decPlaces)
        {
            InitializeComponent();
            tbDisplay.Text = clipString;
            trackBar.Value = decPlaces;
        }

        private void Calculator_Load(object sender, EventArgs e)
        {
            Utilities.IsCalcOpen = true;
            NativeMethods.SendMessage(tbDisplay.Handle, NativeMethods.EM_SETMARGINS, NativeMethods.EC_LEFTMARGIN, 65536 + 3);
        }

        private void BtnStd0_Click(object sender, EventArgs e) { AddToDisplay("0"); }
        private void BtnStd1_Click(object sender, EventArgs e) { AddToDisplay("1"); }
        private void BtnStd2_Click(object sender, EventArgs e) { AddToDisplay("2"); }
        private void BtnStd3_Click(object sender, EventArgs e) { AddToDisplay("3"); }
        private void BtnStd4_Click(object sender, EventArgs e) { AddToDisplay("4"); }
        private void BtnStd5_Click(object sender, EventArgs e) { AddToDisplay("5"); }
        private void BtnStd6_Click(object sender, EventArgs e) { AddToDisplay("6"); }
        private void BtnStd7_Click(object sender, EventArgs e) { AddToDisplay("7"); }
        private void BtnStd8_Click(object sender, EventArgs e) { AddToDisplay("8"); }
        private void BtnStd9_Click(object sender, EventArgs e) { AddToDisplay("9"); }
        private void BtnStdComma_Click(object sender, EventArgs e) { AddToDisplay(","); }
        private void BtnStdPlus_Click(object sender, EventArgs e) { AddToDisplay("+"); }
        private void BtnStdMinus_Click(object sender, EventArgs e) { AddToDisplay("-"); }
        private void BtnStdMultiply_Click(object sender, EventArgs e) { AddToDisplay("*"); }
        private void BtnStdSlash_Click(object sender, EventArgs e) { AddToDisplay("/"); }
        private void btnStdPower_Click(object sender, EventArgs e) { AddToDisplay("^"); }
        private void BtnStdCalc_Click(object sender, EventArgs e) { CalcRechner(); }

        private void BtnStdConvert_Click(object sender, EventArgs e)
        {
            string trimDisplay = tbDisplay.Text.TrimEnd();
            int endNumberLen = Utilities.EndsWithNumber(trimDisplay);
            if (endNumberLen > 0)
            {
                int totalLen = trimDisplay.Length; bool hasBracket = false; string endbracket = ")";
                string endNumberValue = Regex.Replace(trimDisplay.Substring(totalLen - endNumberLen), @"\s+", "");
                //string endNumberValue = trimDisplay.Substring(totalLen - endNumberLen);
                if (endNumberValue.EndsWith(endbracket))
                {
                    hasBracket = true;
                    endNumberValue = endNumberValue.Remove(endNumberValue.Length - 1);
                }
                endNumberValue = endNumberValue.Replace('.', ','); // muss nach Math.PI und Math.E stehen!
                double.TryParse(endNumberValue, out double dblText);
                if (dblText != 0) { endNumberValue = (dblText * -1).ToString(); }
                tbDisplay.Text = trimDisplay.Substring(0, totalLen - endNumberLen) + endNumberValue + (hasBracket ? endbracket : "");
            }
            tbDisplay.Focus(); // sonst hat möglicherweise auslösende Button [+/-] noch den Fokus
            tbDisplay.Select(tbDisplay.Text.Length, 0); // Cursor am Ende positionieren; sonst wird gesamter Inhalt markiert
        }

        private void BtnStdClear_Click(object sender, EventArgs e)
        {
            tbDisplay.Text = "";
            resultIsDisplayed = false;
            tbDisplay.Focus(); // sonst hat möglicherweise auslösende Button [C] noch den Fokus
        }

        private void BtnStdBack_Click(object sender, EventArgs e)
        {
            if (prevCalcString.Length > 0) { tbDisplay.Text = prevCalcString; }
            resultIsDisplayed = false;
            tbDisplay.Focus(); // sonst hat möglicherweise auslösende Button [?] noch den Fokus
            tbDisplay.Select(tbDisplay.Text.Length, 0); // Cursor am Ende positionieren; sonst wird gesamter Inhalt markiert
        }

        private void ShowCalcHelp()
        {
            if (!shownTaskDialog)
            {
                shownTaskDialog = true;

                string foot = "© " + Utilities.GetBuildDate().ToString("yyyy") + " W. Happe, Mathematics by Zaur Nasibov 2008";
                string msg = "Drücken Sie die Taste \"b\", um den letzten Rechenschritt anzuzeigen (back) und \"Enter\" um die Berechnung auzulösen oder das Ergebnis einzufügen." + Environment.NewLine +
                    "Bei Bedarf können Klammern sowie die Konstanten \"pi\" und \"e\" verwendet werden.";
                TaskDialog.ShowDialog(Handle, new TaskDialogPage() { Caption = Text, Text = msg, AllowCancel = true, Buttons = { TaskDialogButton.OK }, Footnote = foot });
                tbDisplay.Focus(); // sonst hat möglicherweise auslösende Button [?] noch den Fokus
                shownTaskDialog = false;
            }
        }

        private void AddToDisplay(String btnValue)
        {
            tbDisplay.AppendText(btnValue);
            resultIsDisplayed = false;
            tbDisplay.Focus();
        }

        private void CalcRechner()
        {
            try
            {// commented code for multiline textbox - not yet implemented!
                //string lastString = tbDisplay.Lines[tbDisplay.Lines.Length - 1];
                //if (lastString != res1.ToString()) { prevCalcString = lastString.Trim().Replace("~", ""); };
                if (tbDisplay.Text != res1.ToString()) { prevCalcString = tbDisplay.Text.Trim().Replace("~", ""); };
                if (new Regex(@"^0[×|x][a-zA-Z0-9]+$").Match(tbDisplay.Text).Success) { res1 = Convert.ToInt32(tbDisplay.Text, 16); } // Hex to int
                else { res1 = calcInstance.Evaluate(tbDisplay.Text); } // die Berechnung des Formelausdrucks!
                if (trackBar.Value >= 0) { tbDisplay.Text = Math.Round(res1, trackBar.Value).ToString(); }
                else { tbDisplay.Text = res1.ToString(); }
                if (tbDisplay.Text != res1.ToString()) { tbDisplay.Text = "~" + tbDisplay.Text; }
                //commented code for multiline textbox - not yet implemented!
                //tbDisplay.Text = tbDisplay.Text.Remove(tbDisplay.GetFirstCharIndexFromLine(tbDisplay.Lines.Length - 1), lastString.Length); // letzte Zeile löschen
                //tbDisplay.AppendText(lastString.Insert(0, "~"));
                resultIsDisplayed = true; // wird benötigt um das Runden von Ausdrücken zu verhindern
                tbDisplay.Select(tbDisplay.Text.Length, 0); // (Anfangsposition, Markierungslänge)
                btnStdBack.Enabled = true;
            }
            catch (CalculateException ex)
            {
                Utilities.ErrorMsgTaskDlg(Handle, ex.Message);
                if (ex.TokenPosition >= 0)
                {
                    int lenStr = calcInstance.TokenString.Length; // abschließendes Semikolon entfernen
                    tbDisplay.Text = calcInstance.TokenString.Substring(0, lenStr > 0 ? lenStr - 1 : 0);
                    tbDisplay.SelectionStart = ex.TokenPosition;
                }
            }
            tbDisplay.Focus(); // Cursor ans Ende stellen
        }

        private void TbDisplay_KeyDown(object sender, KeyEventArgs e)
        {
            resultIsDisplayed = false;
            if ((e.KeyCode == Keys.Z || e.KeyCode == Keys.Y) && e.Modifiers == Keys.Control) { e.SuppressKeyPress = true; btnStdBack.PerformClick(); }
            if (e.KeyCode == Keys.B && e.Modifiers == 0) { e.SuppressKeyPress = true; btnStdBack.PerformClick(); }
            if (e.KeyCode == Keys.C && e.Modifiers == 0) { e.SuppressKeyPress = true; btnStdClear.PerformClick(); } // Clear Display
            if (e.KeyCode == Keys.X && e.Modifiers == 0) { e.SuppressKeyPress = true; btnStdMultiply.PerformClick(); } // x als Malzeichen
            if (e.KeyCode == Keys.F1) { ShowCalcHelp(); }
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Return)) { e.SuppressKeyPress = true; CalcRechner(); }
        }

        private void TbDisplay_TextChanged(object sender, EventArgs e)
        {
            if (tbDisplay.Text.Length > 0) { btnStdClear.Enabled = true; }
            else { btnStdClear.Enabled = false; }
            if (Utilities.EndsWithNumber(tbDisplay.Text) > 0) { btnStdConvert.Enabled = true; }
            else { btnStdConvert.Enabled = false; }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            else if (keyData == Keys.Enter)
            {
                if (resultIsDisplayed)
                {
                    if (Modal) { DialogResult = DialogResult.OK; }
                    else { SendResult(tbDisplay.Text.TrimStart('~')); }
                }
                else if (tbDisplay.Text.Length > 0) { CalcRechner(); }
                else if (Modal) { DialogResult = DialogResult.Cancel; }
                return true;
            }
            else if (keyData == Keys.F9) //&& !tb1Display.Focus())
            {
                trackBar.Value = -1;
                return true;
            }
            else if (keyData == Keys.F10)
            {
                trackBar.Value = 0;
                return true;
            }
            else if (keyData == Keys.F11)
            {
                trackBar.Value = 1;
                return true;
            }
            else if (keyData == Keys.F12)
            {
                trackBar.Value = 2;
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Calculator_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            ShowCalcHelp();
        }

        private void Calculator_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            hlpevent.Handled = true;
            ShowCalcHelp();
        }

        private void TrackBar_ValueChanged(object sender, EventArgs e)
        {
            if (trackBar.Value >= 0)
            {
                labelTrackbar.Text = "Dezimalstellen: " + trackBar.Value.ToString();
                if (resultIsDisplayed)
                {
                    tbDisplay.Text = Math.Round(res1, trackBar.Value).ToString();
                    if (tbDisplay.Text != res1.ToString()) { tbDisplay.Text = "~" + tbDisplay.Text; };
                }
            }
            else
            {
                labelTrackbar.Text = "Keine Rundung";
                if (resultIsDisplayed)
                {
                    tbDisplay.Text = res1.ToString();
                }
            }
            tbDisplay.Focus();
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            if (Modal) { DialogResult = DialogResult.OK; }
            else { SendResult(tbDisplay.Text.TrimStart('~')); }
        }
        private void SendResult(string text)
        {
            try
            {
                Hide(); // Hiding is equivalent to setting the Visible property to false
                Clipboard.Clear();
                if (Application.OpenForms[0] is FrmClipMenu form) { form.IgnoreClipboardChange = true; }
                if (!Utilities.SetClipboardText(text))
                {
                    Utilities.ErrorMsgTaskDlg(Handle, "Es ist ein Fehler aufgetreten.\nVersuchen Sie es noch einmal.");
                    Show();
                }
                else
                {
                    if (NativeMethods.SetForegroundWindow(NativeMethods.lastActiveWindow)) { NativeMethods.SendKeysPaste(); }
                }
            }
            catch (Exception ex) when (ex is NullReferenceException) { }
            finally { Close(); }
        }

        private void FrmClipCalc_FormClosing(object sender, FormClosingEventArgs e)
        {
            Utilities.IsCalcOpen = false;
            NativeMethods.PostMessage(Application.OpenForms[0].Handle, NativeMethods.WM_CLIPCALC_MSG, 0, 0); // dontHide false;
            if (!Modal) { NativeMethods.PostMessage(Application.OpenForms[0].Handle, NativeMethods.WM_USER, trackBar.Value, 0); }
        }
    }
}
