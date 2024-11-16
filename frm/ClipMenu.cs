using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Media;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace ClipMenu
{
    public partial class FrmClipMenu : Form
    {
        internal bool IgnoreClipboardChange { get { return ignoreClipboardChange; } set { ignoreClipboardChange = value; } }
        internal bool AltTabRWin { get { return altTabRWin; } set { altTabRWin = value; } }
        internal bool AltTabApps { get { return altTabApps; } set { altTabApps = value; } }

        private readonly int horizMargins = 6;
        private readonly int vertMargins = 6;
        private readonly int distance = 4;
        private readonly int lineHeight = 18; // listBox.Font.Height + 2; 
        private readonly int indentWidth = 20; // addIndent (z.B. 10 pro Zeichen) wird addiert
        private readonly int indentAdd = 5; // eigentlich ca. 11, darf aber ruhig enger werden, einzige 3-stellige Nr. ist 100
        private readonly DataTable dataTable;
        private DataTable lboxTable = null;
        private static readonly string appName = Application.ProductName; // "ClipMenu";
        private static readonly string assLctn = Path.Combine(AppContext.BaseDirectory, appName + ".exe");  // EXE-Pfad
        private bool ignoreClipboardChange;
        //private bool ignoreClipboardFeedback;
        private readonly int maxDisplayChars = 196; // cave: größere Werte als 197 führen dazu das letzte Zeile nicht selektiert wird
        private readonly int maxTextLength = 8000000;
        private readonly string xmlPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), appName, appName + ".xml");
        private int maxItems = 50;
        private string currNodeText = string.Empty;
        private string imagePath = string.Empty;
        private bool passwdExcld = true;
        private bool copyNoBreak = false;
        private bool plainText = false;
        private bool visualResponse = false;
        private bool acousticResponse = false;
        private bool altTabRWin = false;
        private bool altTabApps = false;
        private bool altTabXBtn = false;
        private bool datesExpanded = true;
        private bool snippetsExpanded = true;
        private bool symbolsExpanded = true;
        private bool propertyExpanderExpanded = true;
        private bool workAround = false;
        private bool medistarRef = false;
        private Tuple<int, int, TreeNode> deletedNodeTuple;
        private bool dontHide = false;
        private bool shownTaskDialog = false;
        private TreeView treeViewCache = new();
        private string selectedString = string.Empty; // clipMenuStrip.Opening > nicht für jedes MenuItem
        private DataRow selecedRow = null; // clipMenuStrip.Opening > nicht für jedes MenuItem
        private int selectedIndex = -1; // clipMenuStrip.Opening > nicht für jedes MenuItem
        private IntPtr hWinEventHook;
        private CheckBox newButton;
        private int decimalPlaces = -1;
        private readonly string wavPath = Path.Combine(AppContext.BaseDirectory, appName + ".wav");
        private readonly List<string> listOfKnownColors = [];
        private static readonly char[] separatorArray = [' ', '\r', '\n'];

        public FrmClipMenu()
        {
            InitializeComponent();
            NativeMethods.lastActiveWindow = NativeMethods.GetForegroundWindow();
            NativeMethods.AddClipboardFormatListener(Handle);
            dataTable = Utilities.GetDataTable(maxTextLength);
            ckbAutoStart.Checked = Utilities.IsAutoStartEnabled(appName);
            if (!Utilities.IsInnoSetupValid(Path.GetDirectoryName(assLctn))) { xmlPath = Path.ChangeExtension(assLctn, ".xml"); } // portable
            cbxMaxItems.SelectedIndex = cbxMaxItems.FindString(maxItems.ToString()); // Default (lässt sich nicht im Designer einstellen)
            foreach (KnownColor color in Enum.GetValues(typeof(KnownColor))) { listOfKnownColors.Add(Color.FromKnownColor(color).Name.ToLower()); }
        }

        private void FrmClipMenu_Load(object sender, EventArgs e)
        {
            hWinEventHook = NativeMethods.SetWinEventHook(NativeMethods.EVENT_SYSTEM_FOREGROUND, NativeMethods.EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, NativeMethods.WinEventProc, 0, 0, NativeMethods.WINEVENT_OUTOFCONTEXT); // | NativeMethods.WINEVENT_SKIPOWNPROCESS);

            newButton = new()
            {
                Appearance = Appearance.Button,
                Text = "📌",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                Location = new Point(285, -1),
                Size = new Size(45, 26),
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.TopCenter,
            };
            newButton.FlatAppearance.BorderSize = 0;
            newButton.FlatAppearance.MouseOverBackColor = Color.SkyBlue;
            newButton.FlatAppearance.MouseDownBackColor = Color.LightSteelBlue;
            newButton.FlatAppearance.CheckedBackColor = Color.PaleGreen;
            toolTip.SetToolTip(newButton, "Bei Deaktivierung nicht Schließen (Alt+Pos1");
            Controls.Add(newButton);
            newButton.BringToFront();
            newButton.CheckedChanged += new EventHandler(NewButton_CheckedChanged);
            newButton.MouseEnter += new EventHandler(NewButton_MouseEnter);
            newButton.MouseLeave += new EventHandler(NewButton_MouseLeave);

            if (File.Exists(xmlPath))
            {
                try
                {
                    treeView.Nodes.Clear();
                    XDocument xDocument = XDocument.Load(xmlPath);
                    if (xDocument == null || xDocument.Root == null) { File.Delete(xmlPath); }
                    foreach (XElement element in xDocument.Root.Descendants("Configuration"))
                    {
                        if (element.Element("MaxItems") != null)
                        {
                            maxItems = int.TryParse(element.Element("MaxItems").Value, out int max) ? max : maxItems;
                            cbxMaxItems.SelectedIndex = cbxMaxItems.FindString(maxItems.ToString());
                        }
                        if (element.Element("DecimalPlaces") != null)
                        {
                            decimalPlaces = int.TryParse(element.Element("DecimalPlaces").Value, out int dec) ? dec : decimalPlaces;
                        }

                        if (element.Element("PasswdExcld") != null)
                        {
                            ckbRegex.Checked = passwdExcld = bool.TryParse(element.Element("PasswdExcld").Value, out passwdExcld) && passwdExcld;
                        }

                        if (element.Element("CopyNoBreak") != null)
                        {
                            checkBoxCopyNoBreak.Checked = copyNoBreak = bool.TryParse(element.Element("CopyNoBreak").Value, out copyNoBreak) && copyNoBreak;
                        }

                        if (element.Element("PasteAsPlain") != null)
                        {
                            checkBoxPlainText.Checked = plainText = bool.TryParse(element.Element("PasteAsPlain").Value, out plainText) && plainText;
                        }

                        if (element.Element("VisualResponse") != null)
                        {
                            cbxVisualResponse.Checked = visualResponse = bool.TryParse(element.Element("VisualResponse").Value, out visualResponse) && visualResponse;
                        }

                        if (element.Element("AcousticResponse") != null)
                        {
                            cbxAcousticResponse.Checked = acousticResponse = bool.TryParse(element.Element("AcousticResponse").Value, out acousticResponse) && acousticResponse;
                        }

                        if (element.Element("AltTabRWin") != null)
                        {
                            checkBoxRWin.Checked = altTabRWin = bool.TryParse(element.Element("AltTabRWin").Value, out altTabRWin) && altTabRWin;
                        }

                        if (element.Element("AltTabApps") != null)
                        {
                            checkBoxMenuKey.Checked = altTabApps = bool.TryParse(element.Element("AltTabApps").Value, out altTabApps) && altTabApps;
                        }

                        if (element.Element("AltTabXBtn") != null)
                        {
                            checkBoxXButton.Checked = altTabXBtn = bool.TryParse(element.Element("AltTabXBtn").Value, out altTabXBtn) && altTabXBtn;
                        }

                        if (element.Element("DatesExpanded") != null)
                        {
                            datesExpanded = bool.TryParse(element.Element("DatesExpanded").Value, out datesExpanded) && datesExpanded;
                        }

                        if (element.Element("SnippetsExpanded") != null)
                        {
                            snippetsExpanded = bool.TryParse(element.Element("SnippetsExpanded").Value, out snippetsExpanded) && snippetsExpanded;
                        }

                        if (element.Element("SymbolsExpanded") != null)
                        {
                            symbolsExpanded = bool.TryParse(element.Element("SymbolsExpanded").Value, out symbolsExpanded) && symbolsExpanded;
                        }

                        if (element.Element("FormSize") != null && new Regex(@"^\d+,\d+$").Match(element.Element("FormSize").Value).Success)
                        {
                            string[] coords = element.Element("FormSize").Value.Split(',');
                            Size = new Size(int.Parse(coords[0]), int.Parse(coords[1]));
                        }
                        if (element.Element("ImagePath") != null)
                        {
                            imagePath = element.Element("ImagePath").Value;
                        }
                    }
                    foreach (XElement element in xDocument.Root.Descendants("Dates"))
                    {
                        TreeNode node = new() { Name = element.Name.ToString(), Text = "Daten", ToolTipText = "Strg+D" };
                        treeView.Nodes.Add(node);
                        foreach (XElement childElement in element.Elements())
                        {
                            TreeNode childNode = new() { Name = childElement.Name.ToString(), Text = childElement.Value };
                            node.Nodes.Add(childNode);
                        }
                    }
                    if (!treeView.Nodes.ContainsKey("Dates")) { treeView.Nodes.Add(new TreeNode() { Name = "Dates", Text = "Daten", ToolTipText = "Strg+D" }); }

                    foreach (XElement element in xDocument.Root.Descendants("Snippets"))
                    {
                        TreeNode node = new() { Name = element.Name.ToString(), Text = "Texte", ToolTipText = "Strg+T" };
                        treeView.Nodes.Add(node);
                        foreach (XElement childElement in element.Elements())
                        {
                            TreeNode childNode;
                            if (childElement.Value.Contains('|'))
                            {
                                int index = childElement.Value.IndexOf('|');
                                childNode = new() { Name = childElement.Name.ToString(), Text = childElement.Value[..index], ToolTipText = childElement.Value.Length > index + 1 ? childElement.Value[(index + 2)..] : "" };
                            }
                            else
                            {
                                childNode = new() { Name = childElement.Name.ToString(), Text = childElement.Value, };
                            }

                            node.Nodes.Add(childNode);
                        }
                    }
                    if (!treeView.Nodes.ContainsKey("Snippets")) { treeView.Nodes.Add(new TreeNode() { Name = "Snippets", Text = "Texte", ToolTipText = "Strg+T" }); }

                    foreach (XElement element in xDocument.Root.Descendants("Symbols"))
                    {
                        TreeNode node = new() { Name = element.Name.ToString(), Text = "Zeichen", ToolTipText = "Strg+Z" };
                        treeView.Nodes.Add(node);
                        foreach (XElement childElement in element.Elements())
                        {
                            TreeNode childNode = new()
                            {
                                Name = childElement.Name.ToString(),
                                Text = childElement.Value[..1],
                                ToolTipText = childElement.Value.Length > 1 ? childElement.Value[2..] : "" // 2 wegen vorgestelltem '|'
                            };
                            node.Nodes.Add(childNode);
                        }
                    }
                    if (!treeView.Nodes.ContainsKey("Symbols")) { treeView.Nodes.Add(new TreeNode() { Name = "Symbols", Text = "Zeichen", ToolTipText = "Strg+Z" }); }

                    if (datesExpanded) { treeView.Nodes[0].Expand(); } else { treeView.Nodes[0].Collapse(); }
                    if (snippetsExpanded) { treeView.Nodes[1].Expand(); } else { treeView.Nodes[1].Collapse(); }
                    if (symbolsExpanded) { treeView.Nodes[2].Expand(); } else { treeView.Nodes[2].Collapse(); }

                    foreach (XElement xElement in xDocument.Root.Descendants("Clips"))
                    {
                        DataRow row = dataTable.NewRow();
                        if (xElement.Element("Time") != null) { row["Time"] = xElement.Element("Time").Value; }
                        if (xElement.Element("Type") != null) { row["Type"] = xElement.Element("Type").Value; }
                        if (xElement.Element("Text") != null) { row["Text"] = Utilities.Truncate(xElement.Element("Text").Value, maxTextLength, string.Empty); }
                        if (xElement.Element("Char") != null) { row["Char"] = xElement.Element("Char").Value; }
                        if (xElement.Element("Word") != null) { row["Word"] = xElement.Element("Word").Value; }
                        dataTable.Rows.Add(row);
                    }
                    dataTable.AcceptChanges();
                    lboxTable = Utilities.DataTable2LBoxDataTable(dataTable, maxDisplayChars);
                    listBox.BeginUpdate();
                    foreach (DataRow row in lboxTable.Rows) { listBox.Items.Add(row); }
                    listBox.EndUpdate();
                    if (listBox.Items.Count > 0) { btnDeleteAll.Enabled = true; }
                }
                catch (XmlException ex) // Unexpected end of file has occurred. The following elements are not closed: Symbol0, Symbols, Clipmenu. Line 33, Position 14.
                {
                    File.Delete(xmlPath);
                    Utilities.ErrorMsgTaskDlg(Handle, ex.Message + "\nDas Programm wird neu gestartet.");
                    ExitApplicationJob();
                    Application.Restart();
                }
            }
            else { Directory.CreateDirectory(Path.GetDirectoryName(xmlPath)); } // If the folder exists already, the line will be ignored.

            treeViewCache = Utilities.CloneTreeView(treeView);

            if (copyNoBreak && NativeMethods.RegisterHotKey(Handle, NativeMethods.HOTKEY_ID0, (uint)(NativeMethods.Modifiers.Shift | NativeMethods.Modifiers.Control), (uint)Keys.C) == false)
            { Utilities.ErrorMsgTaskDlg(Handle, "Strg+Shift+C konnte nicht registriert werden.\nWahrscheinlich wird die Tastenkombination\nbereits von einer anderen App benutzt."); }
            if (plainText && NativeMethods.RegisterHotKey(Handle, NativeMethods.HOTKEY_ID1, (uint)(NativeMethods.Modifiers.Shift | NativeMethods.Modifiers.Control), (uint)Keys.V) == false)
            { Utilities.ErrorMsgTaskDlg(Handle, "Strg+Shift+V konnte nicht registriert werden.\nWahrscheinlich wird die Tastenkombination\nbereits von einer anderen App benutzt."); }
            if (plainText && NativeMethods.RegisterHotKey(Handle, NativeMethods.HOTKEY_ID2, (uint)(NativeMethods.Modifiers.Shift | NativeMethods.Modifiers.Control), (uint)Keys.Insert) == false)
            { Utilities.ErrorMsgTaskDlg(Handle, "Strg+Shift+Einfg konnte nicht registriert werden.\nWahrscheinlich wird die Tastenkombination\nbereits von einer anderen App benutzt."); }
            if (NativeMethods.RegisterAltTabRWin() > 0) { NativeMethods.KeyDown += new KeyEventHandler(GlobalKeyboardHook_KeyDown); }
            if (altTabXBtn) { NativeMethods.RegisterAltTabXBtn(); }
            NativeMethods.SendMessage(snippetSearchBox.Control.Handle, NativeMethods.EM_SETCUEBANNER, 0, "Suche");

            //RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Clipboard");
            //if (key != null && (int)key.GetValue("EnableClipboardHistory") == 1) { key.SetValue("EnableClipboardHistory", 0); }

            //using (var searcher = new ManagementObjectSearcher("SELECT UserName FROM Win32_ComputerSystem"))
            //{
            //    using (var collection = searcher.Get())
            //    {
            //        username = collection.Cast<ManagementBaseObject>().First()["UserName"].ToString();
            //    }
            //}
            //MessageBox.Show("Aktueller Benutzer:" + Environment.NewLine + WindowsIdentity.GetCurrent().Name + Environment.NewLine + Environment.UserName);

        }

        private void GlobalKeyboardHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V)
            {
                if (Visible) { Hide(); }
                else { Show(); }
            }
            else if (e.KeyCode == Keys.C)
            {
                if (Visible)
                {
                    if (clipMenuStrip.Visible) { clipMenuStrip.Visible = false; }
                    Hide();
                }
                else
                {
                    if (Application.OpenForms["FrmClipEdit"] is FrmClipEdit frmClipEdit && ActiveForm == frmClipEdit)
                    {
                        if (frmClipEdit.ClipEditTextBox.SelectedText is string select && select.Length > 0)
                        {
                            InsertClipboard("text", select);
                            ignoreClipboardChange = true;
                            Clipboard.SetText(select);
                        }
                    }
                    else { NativeMethods.SendKeysCopy(); } //SendKeys.SendWait("^(c)");
                    Show(); // auf InsertClipboard warten
                    timer.Enabled = true; //new Thread(new ThreadStart(ThreadJob)) { IsBackground = true }.Start(); // führte auf langsamem PC zum Absturz
                }
            }
            else if (e.KeyCode == Keys.R)
            {
                if (Visible) { dontHide = newButton.Checked = true; } // s. FrmClipMenu_Deactivate
                if (Utilities.IsCalcOpen) { Application.OpenForms["FrmClipCalc"]?.Activate(); } // if (Application.OpenForms["FrmClipCalc"] == null)
                else { StartClipCalc(); }
            }
            else if (e.KeyCode == Keys.Insert)
            {
                if (Visible) { dontHide = newButton.Checked = true; } // s. FrmClipMenu_Deactivate
                if (Utilities.IsEditOpen) { Application.OpenForms["FrmClipEdit"]?.Activate(); }
                else { new FrmClipEdit(dataTable.Rows.Count > 0 && dataTable.Rows[0]["Type"].ToString().Equals("text") ? dataTable.Rows[0]["Text"].ToString() : string.Empty, medistarRef).Show(); }
            }
            else if (e.KeyCode == Keys.RWin) { NativeMethods.SendKeysAltTab(); }
            else if (e.KeyCode == Keys.Apps) { NativeMethods.SendKeysAltTab(); }
            e.Handled = true;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            if (lboxTable.Rows.Count > 0 && lboxTable.Rows[0]["Type"].ToString().Equals("text"))
            {
                clipMenuStrip.Show(listBox, new Point(listBox.Width - clipMenuStrip.Width, listBox.Top + listBox.ItemHeight));
                Cursor.Position = PointToScreen(new Point((listBox.Left + listBox.Right) / 2, listBox.Top + 100)); ;
            }
        }

        private void FrmClipMenu_Shown(object sender, EventArgs e)
        {
            listBox.Focus();
            clipMenuStrip.Show(); // Workaround, damit ContextMenu beim ersten Aufruf nicht bei 0.0 angezeigt wird
            clipMenuStrip.Hide();
            Hide();
        }

        private void FrmClipMenu_SizeChanged(object sender, EventArgs e)
        {
            listBox.DrawMode = DrawMode.OwnerDrawFixed;
            listBox.DrawMode = DrawMode.OwnerDrawVariable;
            if (tabControl.SelectedIndex != 0) { workAround = true; }
            btnStandardSize.Enabled = Size != new Size(346, 602);
        }

        private void FrmClipMenu_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                Rectangle screen = Screen.PrimaryScreen.WorkingArea;
                int xPos = Cursor.Position.X + 10;
                int yPos = Cursor.Position.Y - 100;
                xPos = xPos < 0 ? 0 : xPos + Width > screen.Width ? screen.Width - Width : xPos;
                yPos = yPos < 0 ? 0 : yPos + Height > screen.Height ? screen.Height - Height : yPos;
                Location = new Point(xPos, yPos);
                TopMost = true; // make our form jump to the top of everything
                BringToFront();
                Activate();
                tabControl.SelectedIndex = 0;
                tbSearch.Clear();
                snippetSearchBox.Clear();
                if (listBox.Items.Count > 0) { listBox.SelectedItem = listBox.Items[0]; }
            }
        }

        private void FrmClipMenu_Deactivate(object sender, EventArgs e)
        {
            if (!dontHide) { Hide(); } // dontHide see PropertyToolStripMenuItem_Click
            dontHide = newButton.Checked;
        }

        private void FrmClipMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            newButton.Checked = false;
            if (e.CloseReason == CloseReason.UserClosing && (ModifierKeys & Keys.Shift) != Keys.Shift) // Anwendung kann nur über «Beenden» in cntxtMenuTNA beendet werden!
            {
                e.Cancel = true; // das Schließen des Formulars verhindern
                Hide();
            }
            else { ExitApplicationJob(); }
        }

        private void ExitApplicationJob()
        {
            //timerSuspend.Enabled = false;
            //Utilities.EnableStandby(Handle);
            NativeMethods.UnhookWinEvent(hWinEventHook);
            NativeMethods.UnregisterHotKey(Handle, NativeMethods.HOTKEY_ID0);
            NativeMethods.UnregisterHotKey(Handle, NativeMethods.HOTKEY_ID1);
            NativeMethods.UnregisterHotKey(Handle, NativeMethods.HOTKEY_ID2);
            NativeMethods.UnregisterAltTabRWin();
            NativeMethods.UnregisterAltTabXBtn();
            NativeMethods.RemoveClipboardFormatListener(Handle);
            foreach (DataRow row in dataTable.Rows) // Passwörter nicht in XML speichern - immer, unabhängig von passwdExcld
            {
                if (row["Type"].ToString().Equals("text", StringComparison.Ordinal) && Utilities.MaybePassword(row["Text"].ToString())) { row.Delete(); }
            }
            dataTable.AcceptChanges();
            try
            {
                using StringWriter writer = new();
                dataTable.WriteXml(writer, XmlWriteMode.IgnoreSchema, false);
                XDocument xDocument = new(new XElement(appName, SaveConfiguration()));
                xDocument.Descendants(appName).LastOrDefault()?.Add(Utilities.CreateXmlElementList(treeView.Nodes));
                xDocument.Root.Add(XDocument.Parse(writer.ToString()).Root.Elements()); // Merge to one XDocument
                xDocument.Save(xmlPath);
            }
            catch { }
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0)
            {
                listBox.Focus();
                toolStripStatusLabel.Text = Utilities.CountListBox(listBox);
                toolStripStatusLabel.IsLink = false;
                if (workAround)
                {
                    listBox.DrawMode = DrawMode.OwnerDrawFixed;
                    listBox.DrawMode = DrawMode.OwnerDrawVariable;
                    workAround = false;
                }
            }
            else if (tabControl.SelectedIndex == 1)
            {
                toolStripStatusLabel.Text = Utilities.CountChildNodes(treeView);
                toolStripStatusLabel.IsLink = false;
                if (treeView.Nodes.Count > 0)
                {
                    TreeNode dateNode = treeView.Nodes.Find("Dates", true)[0];
                    if (dateNode != null)
                    {
                        foreach (TreeNode node in dateNode.Nodes)
                        {
                            Tuple<string, bool> tuple = Utilities.GetDateTimeNowFormatted(node.Text);
                            if (tuple != null && tuple.Item2) { node.Text = tuple.Item1; }
                        }
                    }
                }
            }
            else
            {
                toolStripStatusLabel.Text = xmlPath;
                toolStripStatusLabel.IsLink = true;
            }
        }

        private void TbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down) && listBox.Items.Count > 0)
            {
                listBox.Focus();
                listBox.SelectedIndex = 0;
                e.Handled = e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (tbSearch.Text.Length > 0) { tbSearch.Clear(); }
                else
                {
                    listBox.Focus();
                    listBox.SelectedIndex = 0;
                }
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void TbSearch_TextChanged(object sender, EventArgs e)
        {
            lblTbClear.Visible = tbSearch.Text.Length > 0;
            listBox.BeginUpdate();
            listBox.Items.Clear();
            lboxTable.Clear();
            int iText = dataTable.Columns.IndexOf("Text");
            foreach (DataRow row in dataTable.Rows)
            {
                bool isImage = row["Type"].ToString().Equals("image");
                if (isImage && tbSearch.Text.Length > 0 && tbSearch.Text != "i" && tbSearch.Text != "im" && tbSearch.Text != "img" && tbSearch.Text != "ima" && tbSearch.Text != "imag" && tbSearch.Text != "image") { continue; }
                if ((isImage && (tbSearch.Text == "i" || tbSearch.Text == "im" || tbSearch.Text == "img" || tbSearch.Text == "ima" || tbSearch.Text == "imag" || tbSearch.Text == "image")) || row["Text"].ToString().Contains(tbSearch.Text, StringComparison.InvariantCultureIgnoreCase))
                {
                    object[] values = new object[row.ItemArray.Length]; // ein Array zum Speichern der Spaltenwerte
                    row.ItemArray.CopyTo(values, 0); // Spaltenwerte in das Array kopieren
                    if (!isImage) { values[iText] = Utilities.PrepareText2ListBox(values[iText].ToString(), maxDisplayChars); }
                    lboxTable.Rows.Add(values);
                    lboxTable.AcceptChanges();
                }
            }
            foreach (DataRow row in lboxTable.Rows) { listBox.Items.Add(row); }
            listBox.EndUpdate();
        }

        private void ListBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (lboxTable.Rows[e.Index]["Type"].ToString().Equals("image", StringComparison.Ordinal))
            {
                e.ItemHeight = vertMargins + lineHeight * 3 + distance * 3 + vertMargins;
            }
            else
            {
                e.ItemHeight = vertMargins + lineHeight + distance + (lboxTable.Rows[e.Index]["Text"].ToString().Split('\n').Length * lineHeight) + vertMargins;
            }
        }

        private void ListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (lboxTable == null) { return; }
            if (e.Index == -1) { return; }
            if (lboxTable.Rows[e.Index] == null) { return; }
            int number = e.Index + 1;
            DateTime? dateTimeFromString = DateTime.Parse(lboxTable.Rows[e.Index]["Time"].ToString());
            if (dateTimeFromString.HasValue)
            {
                TimeSpan timeSpan = (TimeSpan)(DateTime.Now - dateTimeFromString);
                int secDiff = (int)timeSpan.TotalSeconds;
                string xcaption;
                bool isImage = false;
                bool isFiles = false;
                if (secDiff >= 172800) { xcaption = $"Vor {(int)timeSpan.TotalDays} Tagen"; }
                else if (secDiff >= 7200) { xcaption = $"Vor {(int)timeSpan.TotalHours} Stunden"; }
                else if (secDiff >= 120) { xcaption = $"Vor {(int)timeSpan.TotalMinutes} Minuten"; }
                else { xcaption = secDiff == 0 ? "Jetzt gerade" : $"Vor {secDiff} Sekunden"; }
                Image image = null;
                if (lboxTable.Rows[e.Index]["Type"].ToString().Equals("image"))
                {
                    image = Image.FromStream(new MemoryStream(Convert.FromBase64String(lboxTable.Rows[e.Index]["Text"].ToString())));
                    if (image != null)
                    {
                        isImage = true;
                        xcaption = xcaption + " (" + (int)Math.Round(image.HorizontalResolution, 0) + " Pixels/Inch)";
                    }
                }
                else if (lboxTable.Rows[e.Index]["Type"].ToString().Equals("file"))
                {
                    isFiles = true;
                    string[] array = (string[])lboxTable.Rows[e.Index]["Text"].ToString().Split(Environment.NewLine.ToCharArray());
                    int length = array.Length;
                    xcaption = xcaption + " (" + (array.Length == 1 ? "eine Datei)" : length + " Dateien)");
                }
                else { xcaption = xcaption + " (" + ((int)lboxTable.Rows[e.Index]["Char"]).ToString("N0") + " Zeichen)"; }

                string dtext = lboxTable.Rows[e.Index]["Text"].ToString();
                bool isSelected = false;
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    isSelected = true;
                    e = new DrawItemEventArgs(e.Graphics, e.Font, e.Bounds, e.Index, e.State ^ DrawItemState.Selected, e.ForeColor, isImage ? Color.MediumAquamarine : isFiles ? Color.Salmon : Color.LightSteelBlue); // Choose the color.
                }
                e.DrawBackground(); // Draw the background of the ListBox control for each item.
                using Graphics gfx = e.Graphics;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.CompositingQuality = CompositingQuality.HighQuality;
                gfx.DrawString(number.ToString() + ".", e.Font, Brushes.Black, e.Bounds.X + horizMargins, e.Bounds.Y + vertMargins);
                int addIndent = number.ToString().Length * indentAdd; // damit mehrstellige Zahlen Platz haben
                gfx.DrawString(xcaption, e.Font, isSelected ? Brushes.White : Brushes.SteelBlue, e.Bounds.X + horizMargins + indentWidth + addIndent, e.Bounds.Y + vertMargins);
                if (image != null)
                {
                    int width = image.Width;
                    int height = image.Height;
                    if (height > 48) { width = 48 * width / height; }
                    width = width < 160 ? width : 160;
                    height = height < 48 ? height : 48;

                    gfx.DrawImage(image, new Rectangle(e.Bounds.X + horizMargins + indentWidth + addIndent, e.Bounds.Y + vertMargins + lineHeight + distance, width, height));
                    gfx.DrawString("[" + Image.FromStream(new MemoryStream(Convert.FromBase64String(lboxTable.Rows[e.Index]["Text"].ToString()))).Width + "×" + Image.FromStream(new MemoryStream(Convert.FromBase64String(lboxTable.Rows[e.Index]["Text"].ToString()))).Height + "]", e.Font, Brushes.Black,
                        e.Bounds.X + horizMargins + indentWidth + addIndent + width + horizMargins, e.Bounds.Y + vertMargins * 2 + lineHeight + distance);
                }
                else
                {
                    int lBoxWidth = listBox.ClientSize.Width - SystemInformation.BorderSize.Width * 2 - horizMargins - indentWidth - addIndent - SystemInformation.VerticalScrollBarWidth; // 22 = Einrückung
                    if (listOfKnownColors.Contains(dtext.ToLower()))
                    {
                        gfx.FillEllipse(new SolidBrush(Color.FromName(dtext)), lBoxWidth, e.Bounds.Y + vertMargins + lineHeight + distance, 15, 15);
                    }
                    else if (new Regex(@"^#?(([0-9a-fA-F]{2}){3}|([0-9a-fA-F]){3})$").Match(dtext).Success)
                    {
                        Color color = ColorTranslator.FromHtml(dtext.StartsWith('#') ? dtext : "#" + dtext);
                        gfx.FillEllipse(new SolidBrush(color), lBoxWidth, e.Bounds.Y + vertMargins + lineHeight + distance, 15, 15);
                    }

                    string[] lines = dtext.Split('\n');
                    int offset = 0;
                    foreach (string line in lines)
                    {
                        string txt = line;
                        SizeF size = gfx.MeasureString(line, e.Font);
                        bool isLong = false;
                        while (gfx.MeasureString(txt, e.Font).Width > lBoxWidth)
                        {
                            txt = txt[..^1];
                            isLong = true;
                        }
                        if (isLong) { txt += "…"; }
                        gfx.DrawString(txt, e.Font, Brushes.Black, e.Bounds.X + horizMargins + indentWidth + addIndent, e.Bounds.Y + vertMargins + lineHeight + distance + offset);
                        offset += lineHeight;
                    }
                }
                e.DrawFocusRectangle(); // Draw the focus rectangle around the selected item.
                toolStripStatusLabel.Text = Utilities.CountListBox(listBox);
            }
        }

        private void ListBox_Enter(object sender, EventArgs e) { tbSearch.BackColor = Color.WhiteSmoke; } // SystemColors.Control; 
        private void ListBox_Leave(object sender, EventArgs e) { tbSearch.BackColor = Color.White; } // SystemColors.ControlLightLight; 

        private void ListBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;
            if (char.IsLetterOrDigit(keyChar))
            {
                tbSearch.Text += keyChar.ToString();
                tbSearch.Select(tbSearch.Text.Length, 0);
                tbSearch.Focus();
            }
        }

        private void ListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox.SelectedIndex != -1 && e.KeyCode == Keys.Enter) { SendText(); }
            else if (listBox.SelectedIndex != -1 && e.KeyCode == Keys.F2) { clipMenuStrip.Show(listBox, new Point(listBox.Width - clipMenuStrip.Width, listBox.GetItemRectangle(listBox.SelectedIndex).Location.Y)); }
            else if (listBox.SelectedIndex != -1 && e.KeyCode == Keys.S && e.Modifiers == Keys.Control) { SnippetToolStripMenuItem_Click(null, null); }
        }

        private void ListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (listBox.Items.Count > 0 && e.Button == MouseButtons.Right) { listBox.SelectedIndex = listBox.IndexFromPoint(e.Location); }
        }

        private void ListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox.Items.Count > 0 && listBox.IndexFromPoint(e.Location) != ListBox.NoMatches) { SendText(); }
        }

        private void TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right) { treeView.SelectedNode = e.Node; }
        }

        private void TreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent != null) { SendSnippet(e.Node.Text); }
        }

        private void TreeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TreeNode selectedNode = treeView.SelectedNode;
                if (selectedNode.Parent != null) { SendSnippet(selectedNode.Text); }
            }
            else if (e.KeyCode == Keys.Delete && treeView.SelectedNode != null) { TsButtonDelete_Click(null, null); }
            else if (e.KeyCode == Keys.F2 && treeView.SelectedNode != null) { treeView.SelectedNode.BeginEdit(); }

            else if (e.KeyCode == Keys.Up && e.Modifiers == Keys.Alt && treeView.SelectedNode != null)
            {
                TsButtonMoveUp_Click(null, null);
                e.Handled = e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Down && e.Modifiers == Keys.Alt && treeView.SelectedNode != null)
            {
                TsButtonMoveDn_Click(null, null);
                e.Handled = e.SuppressKeyPress = true;
            }

            else if (e.KeyCode == Keys.Z && e.Modifiers == Keys.Control && deletedNodeTuple != null)
            {
                treeView.Nodes[deletedNodeTuple.Item1].Nodes.Insert(deletedNodeTuple.Item2, deletedNodeTuple.Item3);
                deletedNodeTuple = null;
                tsRestoreBtn.Enabled = false;
                toolStripStatusLabel.Text = Utilities.CountChildNodes(treeView);
                treeViewCache = Utilities.CloneTreeView(treeView);
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void TreeView_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node != null && e.Node.Parent == null) { e.CancelEdit = true; }
            if (e.Node != null && e.Node.Parent != null) { currNodeText = e.Node.Text; }
        }

        private void TreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node == null) { return; }
            if ((e.Label == null || e.Label.Length == 0) && e.Node.Text.Length == 0)
            {
                e.Node.Remove();
                toolStripStatusLabel.Text = Utilities.CountChildNodes(treeView);
                return;
            }
            if (e.Node != null && e.Node.Parent.Name == "Dates" && string.IsNullOrEmpty(e.Node.Text))
            {
                Tuple<string, bool> tuple = Utilities.GetDateTimeNowFormatted(e.Label);
                if (tuple == null || !tuple.Item2)
                {
                    e.CancelEdit = true;
                    Utilities.ErrorMsgTaskDlg(Handle, e.Node.Text + Environment.NewLine + "Das Datumsformat wurde nicht erkannt!");
                    e.Node.BeginEdit();
                }
            }
            else
            {
                if (e.Node != null && (e.Node.Parent.Name == "Symbols" && e.Node.Text?.Length == 1) || (e.Node.Parent.Name == "Texte" && e.Node.Text != currNodeText))
                {
                    dontHide = true;
                    InputBox inputBox = new(e.Node.Text, "Bitte geben Sie einen ToolTipText ein:", e.Node.ToolTipText);
                    if (inputBox.ShowDialog(this) == DialogResult.OK) { e.Node.ToolTipText = inputBox.InputTextBox.Text; }
                    dontHide = false;
                }
                treeView.SelectedNode = e.Node;
                treeViewCache = Utilities.CloneTreeView(treeView);
            }
        }

        private void InsertClipboard(string type, string text)
        {
            if (string.IsNullOrEmpty(text.Trim())) { return; }
            if (passwdExcld && Utilities.MaybePassword(text)) { return; }
            if (text.Length > maxTextLength)
            {
                Console.Beep();
                notifyIcon.BalloonTipTitle = appName;
                notifyIcon.BalloonTipText = "Die Textlänge (" + text.Length.ToString("N0") + ") übersteigt die festgelegte Höchstgrenze von " + maxTextLength.ToString("N0") + " Zeichen.";
                notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon.ShowBalloonTip(1000);
                return;
            }
            text = new string(text.Where(XmlConvert.IsXmlChar).ToArray()); // Utilities.RemoveInvalidXmlChars(text); // new string(text.Where(XmlConvert.IsXmlChar).ToArray());
            if (text.Length == 0) { return; }
            tbSearch.Clear();

            medistarRef = false;
            Match match = new Regex(@"S=(?<sph>[+-]\s*\d+[.,]\d{2}) Z=(?<zyl>[+-]\s*\d+[.,]\d{2})\*\s*(?<axis>\d{1,3})").Match(text);
            while (match.Success) // V1 . R.:S=-24.75 Z=- 7.25*180
            {
                if (double.TryParse(match.Groups["sph"].Value.Replace(" ", string.Empty).Replace(".", ","), out double sph) &&
                    double.TryParse(match.Groups["zyl"].Value.Replace(" ", string.Empty).Replace(".", ","), out double zyl) &&
                    int.TryParse(match.Groups["axis"].Value, out int axis))
                {
                    sph = sph + zyl; // Neue Sphäre
                    zyl = zyl * -1; // Neuer Zylinder
                    if (axis >= 90) { axis -= 90; } else { axis += 90; } // Neue Achse
                    text = text.Replace(match.Value, "S=" + Utilities.MedistarRefWert(sph) + " Z=" + Utilities.MedistarRefWert(zyl) + "*" + string.Format("{0,3:0}", axis));
                    medistarRef = true;
                }
                match = match.NextMatch();
            }

            foreach (DataRow row in dataTable.AsEnumerable().Where(row => row.Field<string>("Text").Equals(text))) { row.Delete(); }
            DataRow dr = dataTable.NewRow();
            dr[0] = DateTime.Now;
            dr[1] = type;
            dr[2] = text;
            dr[3] = text.Length; // "Char(s)"
            dr[4] = text.Split(separatorArray, StringSplitOptions.RemoveEmptyEntries).Length; // "Word(s)"
            dataTable.Rows.InsertAt(dr, 0);
            if (dataTable.Rows.Count > maxItems) { dataTable.Rows[^1].Delete(); }
            dataTable.AcceptChanges();
            lboxTable.Clear();
            lboxTable = Utilities.DataTable2LBoxDataTable(dataTable, maxDisplayChars);
            listBox.BeginUpdate();
            listBox.Items.Clear();
            foreach (DataRow row in lboxTable.Rows) { listBox.Items.Add(row); }
            listBox.SelectedIndex = 0;
            listBox.EndUpdate();
            btnDeleteAll.Enabled = true;

            if (visualResponse) //  && !ignoreClipboardFeedback
            {
                SplashForm frmSplash = new(text, 1000); // using geht nur mit ShowDialog
                NativeMethods.ShowWindow(frmSplash.Handle, NativeMethods.SW_SHOWNOACTIVATE); // ohne TopMost!
            }
            if (acousticResponse && File.Exists(wavPath)) //  && !ignoreClipboardFeedback
            {
                using SoundPlayer player = new(wavPath);
                player.Play();
            }
            //ignoreClipboardFeedback = false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.D1 | Keys.Control:
                case Keys.NumPad1 | Keys.Control:
                    {
                        if (listBox.Items.Count > 0) { SendText(0); };
                        return true;
                    }
                case Keys.D2 | Keys.Control:
                case Keys.NumPad2 | Keys.Control:
                    {
                        if (listBox.Items.Count > 1) { SendText(1); };
                        return true;
                    }
                case Keys.D3 | Keys.Control:
                case Keys.NumPad3 | Keys.Control:
                    {
                        if (listBox.Items.Count > 2) { SendText(2); };
                        return true;
                    }
                case Keys.D4 | Keys.Control:
                case Keys.NumPad4 | Keys.Control:
                    {
                        if (listBox.Items.Count > 3) { SendText(3); };
                        return true;
                    }
                case Keys.D5 | Keys.Control:
                case Keys.NumPad5 | Keys.Control:
                    {
                        if (listBox.Items.Count > 4) { SendText(4); };
                        return true;
                    }
                case Keys.D6 | Keys.Control:
                case Keys.NumPad6 | Keys.Control:
                    {
                        if (listBox.Items.Count > 5) { SendText(5); };
                        return true;
                    }
                case Keys.D7 | Keys.Control:
                case Keys.NumPad7 | Keys.Control:
                    {
                        if (listBox.Items.Count > 6) { SendText(6); };
                        return true;
                    }
                case Keys.D8 | Keys.Control:
                case Keys.NumPad8 | Keys.Control:
                    {
                        if (listBox.Items.Count > 7) { SendText(7); };
                        return true;
                    }
                case Keys.D9 | Keys.Control:
                case Keys.NumPad9 | Keys.Control:
                    {
                        if (listBox.Items.Count > 8) { SendText(8); };
                        return true;
                    }
                case Keys.D0 | Keys.Control:
                case Keys.NumPad0 | Keys.Control:
                    {
                        if (listBox.Items.Count > 9) { SendText(9); };
                        return true;
                    }
                case Keys.F2 | Keys.Control | Keys.Shift: { Utilities.StartFile(xmlPath); return true; }
                case Keys.F | Keys.Control:
                    {
                        if (tabControl.SelectedIndex == 0) { tbSearch.Focus(); }
                        else if (tabControl.SelectedIndex == 1) { snippetSearchBox.TextBox.Focus(); }
                        return true;
                    }
                case Keys.Delete | Keys.Control:
                    {
                        if (tabControl.SelectedIndex == 0 && listBox.Items.Count > 0) { BtnDeleteAll_Click(null, null); }
                        return true;
                    }
                case Keys.I | Keys.Control:
                    {
                        if (tabControl.SelectedIndex == 0 && listBox.Items.Count > 0) { SnippetToolStripMenuItem_Click(null, null); }
                        return true;
                    }
                case Keys.V | Keys.Control:
                    {
                        tabControl.SelectedIndex = 0;
                        listBox.Focus();
                        return true;
                    }
                case Keys.S | Keys.Control:
                    {
                        tabControl.SelectedIndex = 1;
                        treeView.Focus();
                        return true;
                    }
                case Keys.E | Keys.Control:
                    {
                        tabControl.SelectedIndex = 2;
                        btnStandardSize.Focus();
                        return true;
                    }
                case Keys.D | Keys.Control:
                    {
                        tabControl.SelectedIndex = 1;
                        treeView.SelectedNode = treeView.Nodes[0];
                        treeView.Nodes[0].EnsureVisible();
                        treeView.Nodes[0].Expand();
                        return true;
                    }
                case Keys.T | Keys.Control:
                    {
                        tabControl.SelectedIndex = 1;
                        treeView.SelectedNode = treeView.Nodes[1];
                        treeView.Nodes[1].EnsureVisible();
                        treeView.Nodes[1].Expand();
                        return true;
                    }
                case Keys.Z | Keys.Control:
                    {
                        tabControl.SelectedIndex = 1;
                        treeView.SelectedNode = treeView.Nodes[2];
                        treeView.Nodes[2].EnsureVisible();
                        treeView.Nodes[2].Expand();
                        return true;
                    }
                case Keys.Home | Keys.Alt:
                    {
                        newButton.Checked = !newButton.Checked;
                        return true;
                    }
                case Keys.Enter | Keys.Alt:
                    {
                        if (listBox.SelectedIndex != -1) { PropertyToolStripMenuItem_Click(null, null); }
                        return true;
                    }
                case Keys.Escape | Keys.Shift:
                case Keys.F4 | Keys.Alt:
                    {
                        Application.Exit();
                        return true;
                    }
                case Keys.Delete:
                    {
                        if (listBox.Focused && listBox.SelectedIndex != -1) { DeleteToolStripMenuItem_Click(null, null); return true; }
                        else { return false; }
                    }
                case Keys.Escape:
                    {
                        bool isEditing = false;
                        TreeNode treeNode = null;
                        foreach (TreeNode child in treeView.Nodes.Cast<TreeNode>().SelectMany(node => node.Nodes.Cast<TreeNode>().Where(child => child.IsEditing))) { treeNode = child; isEditing = true; }
                        if (treeView.Visible && isEditing) { treeNode.EndEdit(true); return true; }
                        else if (snippetSearchBox.Focused) { snippetSearchBox.Clear(); return true; }
                        else if (!tbSearch.Focused) { Hide(); return true; }
                        else { return false; }
                    }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_CLIPBOARDUPDATE)
            {
                if (!ignoreClipboardChange)
                {
                    try
                    {
                        if (Clipboard.ContainsText())
                        {
                            string data = Clipboard.GetText(); // string containing the UnicodeText data or an empty string if no UnicodeText data is available on the Clipboard.
                            if (!string.IsNullOrEmpty(data)) { InsertClipboard("text", data); }
                        }
                        else if (Clipboard.ContainsFileDropList())
                        {
                            string[] filesArray = Clipboard.GetFileDropList().Cast<string>().ToArray();
                            if (filesArray?.Length > 0) { InsertClipboard("file", string.Join(Environment.NewLine, filesArray)); }
                        }
                        else if (Clipboard.ContainsImage())
                        {
                            using Image image = Clipboard.GetImage();
                            if (image != null)
                            {
                                Rectangle activeScreenDimensions = Screen.FromControl(this).Bounds; // aktiver Bildschirm bei Multibetrieb
                                int foo = activeScreenDimensions.Height > 1350 ? 300 : activeScreenDimensions.Height > 1150 ? 200 : 100;
                                if (image.Width > activeScreenDimensions.Width - foo || image.Height > activeScreenDimensions.Height - foo)
                                {
                                    float ratio = Math.Min((float)(activeScreenDimensions.Width - foo) / image.Width, (float)(activeScreenDimensions.Height - foo) / image.Height);
                                    int newWidth = (int)(image.Width * ratio);
                                    int newHeight = (int)(image.Height * ratio);

                                    //MessageBox.Show("Screen: " + activeScreenDimensions.Width.ToString() + " x " + activeScreenDimensions.Height.ToString() + Environment.NewLine +
                                    //    "Image: " + image.Width.ToString() + " x " + image.Height.ToString() + Environment.NewLine +
                                    //    "Neu: " + newWidth.ToString() + " x " + newHeight.ToString() + Environment.NewLine +
                                    //    "Ratio: " + ratio.ToString());

                                    SplashForm frmSplash = new("Thumbnail statt Original!", 1000); // using geht nur mit ShowDialog
                                    NativeMethods.ShowWindow(frmSplash.Handle, NativeMethods.SW_SHOWNOACTIVATE); // ohne TopMost!


                                    using Image resizedImage = image.GetThumbnailImage(newWidth, newWidth * image.Height / image.Width, null, IntPtr.Zero);
                                    InsertClipboard("image", Utilities.ImageToBase64String(resizedImage));
                                }
                                else { InsertClipboard("image", Utilities.ImageToBase64String(image)); }
                            }
                        }
                        else if (Clipboard.ContainsAudio())
                        {
                            Stream data = Clipboard.GetAudioStream();
                            if (data != null) { Utilities.ErrorMsgTaskDlg(Handle, data.ToString() + Environment.NewLine + "Not yet implemented"); }
                        }
                    }
                    catch { }
                }
                else { ignoreClipboardChange = false; }
                m.Result = IntPtr.Zero;
            }
            //else if (m.Msg == NativeMethods.WM_CONTEXTMENU) { Console.Beep(); m.Result = IntPtr.Zero; return; }

            else if (m.Msg == NativeMethods.WM_HOTKEY && m.WParam == NativeMethods.HOTKEY_ID0)
            {
                NativeMethods.SendKeysCopy();
                Thread.Sleep(10);
                Application.DoEvents();
                DataRow foundRow = dataTable.Rows[0];
                if (foundRow != null && foundRow["Type"].ToString().Equals("text", StringComparison.Ordinal) && !string.IsNullOrEmpty(foundRow["Text"].ToString()))
                {
                    selectedString = foundRow["Text"].ToString();
                    selecedRow = foundRow;
                    RemoveLineBreaksToolStripMenuItem_Click(null, null);
                }
            }

            else if (m.Msg == NativeMethods.WM_HOTKEY && (m.WParam == NativeMethods.HOTKEY_ID1 || m.WParam == NativeMethods.HOTKEY_ID2)) // Send as plain text
            {
                if (Clipboard.ContainsText())
                {
                    ignoreClipboardChange = true;

                    //if (!NativeMethods.SetForegroundWindow(NativeMethods.lastActiveWindow) || NativeMethods.GetForegroundWindow() == IntPtr.Zero) { Console.Beep(); }
                    Clipboard.SetText(Clipboard.GetText(TextDataFormat.UnicodeText));

                    if (m.WParam == NativeMethods.HOTKEY_ID1) { NativeMethods.SendKeyUp(NativeMethods.KeyCode.KEY_V); }
                    else if (m.WParam == NativeMethods.HOTKEY_ID2) { NativeMethods.SendKeyUp(NativeMethods.KeyCode.VK_INSERT); }
                    NativeMethods.SendKeyUp(NativeMethods.KeyCode.VK_LCONTROL);
                    NativeMethods.SendKeyUp(NativeMethods.KeyCode.VK_LSHIFT); // Andernfalls wird SendKeysPaste erst nach dem Loslassen erfolgen

                    if (!NativeMethods.SendKeysPaste())
                    {
                        Utilities.ErrorMsgTaskDlg(Handle, "Es ist ein Fehler aufgetreten.\nVersuchen Sie es noch einmal.");
                    }
                    NativeMethods.SendKeyUp(NativeMethods.KeyCode.VK_LCONTROL);
                    NativeMethods.SendKeyUp(NativeMethods.KeyCode.VK_LSHIFT); // Ermöglicht, dass die Modifier-Tasten gedrückt bleiben
                    NativeMethods.SendKeyDown(NativeMethods.KeyCode.VK_LCONTROL);
                    NativeMethods.SendKeyDown(NativeMethods.KeyCode.VK_LSHIFT); // Ermöglicht, dass die Modifier-Tasten gedrückt bleiben
                }
                else { Utilities.ErrorMsgTaskDlg(Handle, "Die Zwischenablage enthält keinen Text!"); }
            }
            else if (m.Msg == NativeMethods.WM_USER) { decimalPlaces = m.WParam.ToInt32(); } // form ClipCalc
            else if (m.Msg == NativeMethods.WM_CLIPEDIT_MSG || m.Msg == NativeMethods.WM_CLIPCALC_MSG)
            {
                if (dontHide) { dontHide = newButton.Checked = false; }
                //m.Result = IntPtr.Zero;
            }
            else { base.WndProc(ref m); }
        }

        private XElement SaveConfiguration()
        {
            XElement element = new("Configuration");
            element.Add(new XElement("MaxItems", maxItems));
            element.Add(new XElement("DecimalPlaces", decimalPlaces));
            element.Add(new XElement("PasswdExcld", passwdExcld.ToString()));
            element.Add(new XElement("CopyNoBreak", copyNoBreak.ToString()));
            element.Add(new XElement("PasteAsPlain", plainText.ToString()));
            element.Add(new XElement("VisualResponse", visualResponse.ToString()));
            element.Add(new XElement("AcousticResponse", acousticResponse.ToString()));
            element.Add(new XElement("AltTabRWin", altTabRWin.ToString()));
            element.Add(new XElement("AltTabApps", altTabApps.ToString()));
            element.Add(new XElement("AltTabXBtn", altTabXBtn.ToString()));
            element.Add(new XElement("DatesExpanded", treeView.Nodes["Dates"].IsExpanded));
            element.Add(new XElement("SnippetsExpanded", treeView.Nodes["Snippets"].IsExpanded));
            element.Add(new XElement("SymbolsExpanded", treeView.Nodes["Symbols"].IsExpanded));
            element.Add(new XElement("FormSize", Width.ToString() + "," + Height.ToString()));
            element.Add(new XElement("ImagePath", imagePath));
            return element;
        }

        private void CopyStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["FrmClipEdit"] is FrmClipEdit frmClipEdit && frmClipEdit?.Handle == NativeMethods.lastActiveWindow)
            {
                if (frmClipEdit.ClipEditTextBox.SelectedText is string select && select.Length > 0)
                {
                    InsertClipboard("text", select);
                    ignoreClipboardChange = true;
                    Clipboard.SetText(select);
                    NativeMethods.SetForegroundWindow(NativeMethods.lastActiveWindow);
                }
            }
            else
            {
                if (NativeMethods.SetForegroundWindow(NativeMethods.lastActiveWindow)) { NativeMethods.SendKeysCopy(); } //SendKeys.SendWait("^(c)");
            }
            Show(); // auf InsertClipboard warten
            timer.Enabled = true; //new Thread(new ThreadStart(ThreadJob)) { IsBackground = true }.Start(); // führte auf langsamem PC zum Absturz
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) { Application.Exit(); }

        private void ShowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            tabControl.SelectedIndex = 0;
        }

        private void SendText(int index = -1, bool file2txt = false)
        {
            try
            { //SendKeys.SendWait("%{ESC}"); // Alt+Escape
                ignoreClipboardChange = true;
                Clipboard.Clear();
                index = index == -1 ? listBox.SelectedIndex : index;
                DataRow foundRow = dataTable.AsEnumerable().SingleOrDefault(r => r.Field<DateTime>("Time").Equals(lboxTable.Rows[index]["Time"]));
                if (foundRow != null)
                {
                    //if (Utilities.IsEditOpen && Application.OpenForms["FrmClipEdit"] is FrmClipEdit frmClipEdit) { frmClipEdit.TopMost = false; }
                    if (file2txt)
                    {
                        foundRow["Type"] = "text";
                        file2txt = false;
                    }
                    ignoreClipboardChange = true;
                    if (foundRow["Type"].ToString().Equals("image"))
                    {
                        using Image image = Image.FromStream(new MemoryStream(Convert.FromBase64String(foundRow["Text"].ToString())));
                        if (image == null) // || !Utilities.SetClipboardImage(image)
                        {
                            Utilities.ErrorMsgTaskDlg(Handle, "Es ist ein Fehler aufgetreten.\nVersuchen Sie es noch einmal.");
                            //Show();
                            return;
                        }
                        else { Clipboard.SetImage(image); }
                    }
                    else if (foundRow["Type"].ToString().Equals("file"))
                    {
                        System.Collections.Specialized.StringCollection stC = [];
                        stC.AddRange((string[])foundRow["Text"].ToString().Split(Environment.NewLine.ToCharArray())); //string[] lines = foundRow["Text"].ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                        //MessageBox.Show(stC.Count.ToString());
                        if (stC == null) // !Utilities.SetClipboardFiles(stC))
                        {
                            Utilities.ErrorMsgTaskDlg(Handle, "Es ist ein Fehler aufgetreten.\nVersuchen Sie es noch einmal.");
                            //Show();
                            return;
                        }
                        else
                        {
                            try
                            {
                                Clipboard.SetFileDropList(stC);
                                //Thread t = new(() => { Clipboard.SetFileDropList(stC); });  //>Call from an STA thread
                                //t.SetApartmentState(ApartmentState.STA);
                                //t.Start(); //Clipboard.SetFileDropList(stC);

                                //while (!t.IsAlive) { Thread.Sleep(1); }  // Wait for thread to get started
                                //Thread.Sleep(1); // Wait a tick more (@see: http://scn.sap.com/thread/45710)
                                //t.Join(); //wait for the dialog thread to finish
                            }
                            catch (Exception) { }
                        }
                    }
                    else
                    {
                        if (!Utilities.SetClipboardText(foundRow["Text"].ToString()))
                        {
                            Utilities.ErrorMsgTaskDlg(Handle, "Es ist ein Fehler aufgetreten.\nVersuchen Sie es noch einmal.");
                            //Show();
                            return;
                        }
                    }
                    Hide(); // Hiding is equivalent to setting the Visible property to false
                    Application.DoEvents();

                    //dataTable.Rows.RemoveAt(index);
                    //dataTable.AcceptChanges();
                    //lboxTable = Utilities.DataTable2LBoxDataTable(dataTable, maxDisplayChars);

                    if (!NativeMethods.SendKeysPaste())
                    {
                        Utilities.ErrorMsgTaskDlg(Handle, "Es ist ein Fehler aufgetreten.\nVersuchen Sie es noch einmal.");
                    }
                }
                else { Utilities.ErrorMsgTaskDlg(Handle, "Der Text wurde nicht gefunden!"); }
            }
            catch (Exception ex) when (ex is NullReferenceException || ex is ArgumentException) { }
        }

        private void SendSnippet(string text)
        {
            try
            {
                Hide(); // Hiding is equivalent to setting the Visible property to false
                ignoreClipboardChange = true;
                Clipboard.Clear();
                ignoreClipboardChange = true;
                if (!Utilities.SetClipboardText(text))
                {
                    Utilities.ErrorMsgTaskDlg(Handle, "Es ist ein Fehler aufgetreten.\nVersuchen Sie es noch einmal.");
                    Show();
                }
                if (!NativeMethods.SendKeysPaste())
                {
                    Utilities.ErrorMsgTaskDlg(Handle, "Es ist ein Fehler aufgetreten.\nVersuchen Sie es noch einmal.");
                    Show();
                }
            }
            catch (Exception ex) when (ex is NullReferenceException) { }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = listBox.SelectedIndex;
            dataTable.Rows[index].Delete();
            dataTable.AcceptChanges();
            listBox.BeginUpdate();
            lboxTable = Utilities.DataTable2LBoxDataTable(dataTable, maxDisplayChars);
            listBox.Items.Clear();
            foreach (DataRow row in lboxTable.Rows) { listBox.Items.Add(row); }
            if (listBox.Items.Count > 0) { listBox.SelectedIndex = index == 0 ? 0 : index - 1; }
            listBox.EndUpdate();
            if (listBox.Items.Count == 0)
            {
                toolStripStatusLabel.Text = Utilities.CountListBox(listBox); // kein DrawItem
                btnDeleteAll.Enabled = false;
            }
            else { listBox.Focus(); }
        }

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (listBox.SelectedIndex == -1) { e.Cancel = true; }
            else if (lboxTable.Rows[listBox.SelectedIndex]["Type"].ToString().Equals("image"))
            {
                snippetToolStripMenuItem.Text = "Anzeigen";
                file2TxtStripMenuItem.Visible = tsConvertSubMenu.Visible = toolStripSeparator5.Visible = false;
            }
            else if (lboxTable.Rows[listBox.SelectedIndex]["Type"].ToString().Equals("file"))
            {
                snippetToolStripMenuItem.Visible = toolStripSeparator6.Visible = tsConvertSubMenu.Visible = toolStripSeparator5.Visible = false;
                file2TxtStripMenuItem.Visible = true;
            }
            else
            {
                snippetToolStripMenuItem.Text = "Sammeln";
                snippetToolStripMenuItem.Visible = tsConvertSubMenu.Visible = toolStripSeparator5.Visible = toolStripSeparator6.Visible = true;
                file2TxtStripMenuItem.Visible = false;
            }
        }

        private void CbAutoStart_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbAutoStart.Checked) { Utilities.SetAutoStart(appName, assLctn); }
            else { Utilities.UnSetAutoStart(appName); }
        }

        private void LblTbClear_Click(object sender, EventArgs e) { tbSearch.Clear(); }

        private void TsButtonNew_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode is not TreeNode node || node.IsEditing) { return; }
            if (node.Parent != null) { node = node.Parent; }
            TreeNode newChildNode = new() { Name = $"{node.Name[..^1]}999", Text = string.Empty };
            node.Nodes.Add(newChildNode);
            for (int i = 0; i < node.Nodes.Count; i++) { node.Nodes[i].Name = Regex.Replace(node.Nodes[i].Name, @"\d+$", i.ToString()); } // tidy up node names
            treeView.SelectedNode = null;
            node.ExpandAll();
            newChildNode.BeginEdit();
            toolStripStatusLabel.Text = Utilities.CountChildNodes(treeView);
        }

        private void TsButtonDelete_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode is not TreeNode node || node.Parent == null || node.IsEditing) { return; }
            int parentIndex = treeView.Nodes.IndexOf(node.Parent);
            deletedNodeTuple = new(parentIndex, treeView.Nodes[parentIndex].Nodes.IndexOf(node), node);
            node.Remove();
            tsRestoreBtn.Enabled = true;
            treeViewCache = Utilities.CloneTreeView(treeView);
            toolStripStatusLabel.Text = "Drücken Sie <Strg+Z> um das Element wiederherzustellen.";
        }

        private void TsButtonMoveUp_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode is not TreeNode node || node.IsEditing) { return; }
            TreeNode parent = node.Parent;
            if (parent != null)
            {
                int index = parent.Nodes.IndexOf(node);
                if (index > 0)
                {
                    parent.Nodes.RemoveAt(index);
                    parent.Nodes.Insert(index - 1, node);
                    node.TreeView.SelectedNode = node;
                    treeViewCache = Utilities.CloneTreeView(treeView);
                }
            }
        }

        private void TsButtonMoveDn_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode is not TreeNode node || node.IsEditing) { return; }
            TreeNode parent = node.Parent;
            if (parent != null)
            {
                int index = parent.Nodes.IndexOf(node);
                if (index < parent.Nodes.Count - 1)
                {
                    parent.Nodes.RemoveAt(index);
                    parent.Nodes.Insert(index + 1, node);
                    node.TreeView.SelectedNode = node;
                    treeViewCache = Utilities.CloneTreeView(treeView);
                }
            }
        }

        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                ProcessStartInfo psi = new("taskschd.msc") { UseShellExecute = true };
                Process.Start(psi);
            }
            catch (Exception ex) when (ex is Win32Exception || ex is InvalidOperationException) { Utilities.ErrorMsgTaskDlg(Handle, ex.Message); }
        }

        private void CbxMaxItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxMaxItems.Visible && cbxMaxItems.Focused)
            {
                maxItems = int.Parse(cbxMaxItems.Text);
                for (int i = dataTable.Rows.Count - 1; i >= maxItems; i--)
                {
                    dataTable.Rows[i].Delete();
                }
                dataTable.AcceptChanges();
                lboxTable = Utilities.DataTable2LBoxDataTable(dataTable, maxDisplayChars);
                listBox.BeginUpdate();
                listBox.Items.Clear();
                foreach (DataRow row in lboxTable.Rows) { listBox.Items.Add(row); }
                listBox.EndUpdate();
            }
        }

        private void BtnStandardSize_Click(object sender, EventArgs e)
        {
            tabControl.SelectedIndex = 0;
            Size = new Size(346, 602);
        }

        private void RestartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitApplicationJob();
            Application.Restart();
        }

        private void FrmClipMenu_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            if (!shownTaskDialog)
            {
                shownTaskDialog = dontHide = true;
                Utilities.HelpMsgTaskDlg(Handle, Icon);
                shownTaskDialog = dontHide = false;
                listBox.Focus();
            }
        }

        private void FrmClipMenu_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            hlpevent.Handled = true;
            if (!shownTaskDialog)
            {
                shownTaskDialog = dontHide = true;
                Utilities.HelpMsgTaskDlg(Handle, Icon);
                shownTaskDialog = dontHide = false;
                listBox.Focus();
            }
        }

        private void SendToolStripMenuItem_Click(object sender, EventArgs e) { SendText(); }

        private void SnippetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex == -1) { return; }
            DataRow foundRow = dataTable.AsEnumerable().SingleOrDefault(r => r.Field<DateTime>("Time").Equals(lboxTable.Rows[listBox.SelectedIndex]["Time"]));
            if (foundRow != null)
            {
                if (foundRow["Type"].ToString().Equals("image"))
                {
                    Image image = Image.FromStream(new MemoryStream(Convert.FromBase64String(foundRow["Text"].ToString())));
                    if (image != null)
                    {
                        dontHide = true;
                        using ImageView f = new(image, imagePath);
                        f.ShowDialog();
                        //imagePath = f.PictureUserPath;
                    }
                }
                else
                {
                    string foundText = foundRow["Text"].ToString();
                    TreeNode snippetNode = treeView.Nodes.Find(foundText.Length.Equals(1) && !char.IsDigit(foundText[0]) && !char.IsLetter(foundText[0]) ? "Symbols" : DateTime.TryParse(foundText, out _) ? "Dates" : "Snippets", true)[0];
                    if (snippetNode != null)
                    {
                        dontHide = true;
                        string userInput = string.Empty;
                        InputBox inputBox = new(foundText[0].ToString(), "Bitte geben Sie einen ToolTipText ein:");
                        if (inputBox.ShowDialog(this) == DialogResult.OK) { userInput = inputBox.InputTextBox.Text; }
                        dontHide = false;
                        TreeNode newChildNode = new() { Name = $"{snippetNode.Name[..^1]}999", Text = foundText, ToolTipText = userInput };
                        snippetNode.Nodes.Add(newChildNode);
                        for (int i = 0; i < snippetNode.Nodes.Count; i++) { snippetNode.Nodes[i].Name = Regex.Replace(snippetNode.Nodes[i].Name, @"\d+$", i.ToString()); } // tidy up node names
                        treeView.SelectedNode = newChildNode;
                        snippetNode.ExpandAll();
                        tabControl.SelectedIndex = 1;  //toolStripStatusLabel.Text = Utilities.CountChildNodes(treeView);
                    }
                }
            }
        }

        private void PropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox.SelectedIndex < 0) { return; }
            DataRow foundRow = dataTable.AsEnumerable().SingleOrDefault(r => r.Field<DateTime>("Time").Equals(lboxTable.Rows[listBox.SelectedIndex]["Time"]));
            if (foundRow != null)
            {
                dontHide = true;
                bool isImage = foundRow["Type"].ToString().Equals("image");
                bool isFiles = foundRow["Type"].ToString().Equals("file");
                string foo = string.Empty;
                string bar = string.Empty;
                string exp = string.Empty;
                Image image = null;
                if (isImage)
                {
                    image = Image.FromStream(new MemoryStream(Convert.FromBase64String(foundRow["Text"].ToString())));
                    if (image != null)
                    {
                        foo = image.Width + "×" + image.Height;
                        bar = image.RawFormat.ToString();
                        exp = image.PixelFormat.ToString() + ", " + (int)Math.Round(image.HorizontalResolution, 0) + " Pixels/Inch";
                    }
                    else { isImage = false; }
                }
                string message =  // char tab = '\u0009'; PadRight(15, '\t') - funktioniert alles nicht => keine TabZeichen in TaskDialogPage
                "Datum:".PadRight(25, '\u2009') + ((DateTime)foundRow["Time"]).ToString("dd.MM.yyyy") + Environment.NewLine +
                "Uhrzeit:".PadRight(26, '\u2009') + ((DateTime)foundRow["Time"]).ToString("hh:mm:ss") + Environment.NewLine +
                (isImage ?
                "Größe:".PadRight(27, '\u2009') + foo + Environment.NewLine +
                "Format:".PadRight(26, '\u2009') + bar
                : "Zeichen:".PadRight(25, '\u2009') + ((int)foundRow["Char"]).ToString("N0") + Environment.NewLine +
                "Wörter:".PadRight(26, '\u2009') + ((int)foundRow["Word"]).ToString("N0"));
                TaskDialogCommandLinkButton buttonPrev = new("&Vorheriger Eintrag", enabled: listBox.SelectedIndex > 0, allowCloseDialog: true);
                TaskDialogCommandLinkButton buttonNext = new("&Nächster Eintrag", enabled: listBox.SelectedIndex < listBox.Items.Count - 1, allowCloseDialog: true);
                TaskDialogButton buttonClose = TaskDialogButton.Close;
                TaskDialogPage page = new()
                {
                    Caption = appName + " - " + (listBox.SelectedIndex + 1).ToString(),
                    Icon = isImage ? new TaskDialogIcon(Icon.FromHandle(new Bitmap(image).GetHicon())) : new TaskDialogIcon(Icon),
                    Heading = message,
                    SizeToContent = true,
                    AllowCancel = true,
                    Expander = new TaskDialogExpander(isImage ? exp : Utilities.PrepareText2ListBox(foundRow["Text"].ToString(), maxDisplayChars)) { Position = TaskDialogExpanderPosition.AfterFootnote },
                    Buttons = { buttonPrev, buttonNext, buttonClose },
                    DefaultButton = buttonClose
                };
                page.Expander.Expanded = propertyExpanderExpanded;
                TaskDialogButton result = TaskDialog.ShowDialog(this, page);
                if (result == buttonPrev)
                {
                    propertyExpanderExpanded = page.Expander.Expanded;
                    listBox.SelectedIndex--;
                    PropertyToolStripMenuItem_Click(null, null);
                }
                else if (result == buttonNext)
                {
                    propertyExpanderExpanded = page.Expander.Expanded;
                    listBox.SelectedIndex++;
                    PropertyToolStripMenuItem_Click(null, null);
                }
                else { propertyExpanderExpanded = true; }
                dontHide = false;
            }
        }

        private void CkbRegex_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbRegex.Focused) { passwdExcld = ckbRegex.Checked; }
        }

        private void ToolStripStatusLabel_Click(object sender, EventArgs e)
        {
            if (toolStripStatusLabel.IsLink) { Utilities.StartFile(xmlPath); }  // beim ersten Start ist die Datei noch nicht gespeichert
        }

        private void BtnDeleteAll_Click(object sender, EventArgs e)
        {
            listBox.Items.Clear();
            dataTable.Clear();
            lboxTable.Clear();
            XDocument xDocument = XDocument.Load(xmlPath);
            xDocument.Descendants("Clips").Remove(); //foreach (XElement xElement in xDocument.Root.Descendants("Clips")) { xElement.RemoveAll(); }
            xDocument.Save(xmlPath);
            btnDeleteAll.Enabled = false;
            tbSearch.Clear();
            toolStripStatusLabel.Text = "Die gespeicherten Einträge wurden ebenfalls gelöscht.";
        }

        private void TsRestoreBtn_Click(object sender, EventArgs e)
        {
            if (deletedNodeTuple != null)
            {
                treeView.Nodes[deletedNodeTuple.Item1].Nodes.Insert(deletedNodeTuple.Item2, deletedNodeTuple.Item3);
                deletedNodeTuple = null;
                tsRestoreBtn.Enabled = false;
                toolStripStatusLabel.Text = Utilities.CountChildNodes(treeView);
                treeViewCache = Utilities.CloneTreeView(treeView);
            }
            else { Console.Beep(); }
        }

        private void SnippetSearchBox_TextChanged(object sender, EventArgs e)
        {
            treeView.BeginUpdate();
            treeView.Nodes.Clear();
            foreach (TreeNode node in treeViewCache.Nodes) { treeView.Nodes.Add((TreeNode)node.Clone()); }
            int nodes = 0;
            if (snippetSearchBox.Text.Length > 0)
            {
                for (int i = treeView.Nodes.Count; i > 0; i--)
                {
                    if (Utilities.NodeFiltering(treeView.Nodes[i - 1], snippetSearchBox.Text)) { nodes++; }
                }
                for (int i = nodes; i > 0; i--) { treeView.Nodes[i - 1].ExpandAll(); }
            }
            treeView.EndUpdate();
        }

        private void SnippetSearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && treeView.Nodes.Count > 0 && treeView.Nodes[0].Nodes.Count > 0)
            {
                treeView.Focus();
                treeView.SelectedNode = treeView.Nodes[0].Nodes[0];
                e.Handled = e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (snippetSearchBox.TextLength == 0) { treeView.Focus(); }
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null)  // Selektion aufgehoben
            {
                Console.Beep();
                if (Utilities.AllNodesExpanded(treeView)) { tsButtonExpand.Enabled = false; tsButtonCollapse.Enabled = true; }
                else if (Utilities.NoNodesExpanded(treeView)) { tsButtonCollapse.Enabled = false; tsButtonExpand.Enabled = true; }
            }
            else
            {
                if (e.Node.IsSelected && (e.Node.IsExpanded || (e.Node.Parent != null && e.Node.Parent.IsExpanded))) { tsButtonExpand.Enabled = false; tsButtonCollapse.Enabled = true; }
                else if (e.Node.IsSelected && ((e.Node.Parent != null && !e.Node.Parent.IsExpanded) || !e.Node.IsExpanded)) { tsButtonCollapse.Enabled = false; tsButtonExpand.Enabled = true; }
            }
            tsButtonNew.Enabled = e.Node.IsSelected;
            tsButtonDelete.Enabled = removeToolStripMenuItem.Enabled = tsButtonMoveUp.Enabled = tsButtonMoveDn.Enabled = upToolStripMenuItem.Enabled = downToolStripMenuItem.Enabled = e.Node.Parent != null;
            if (e.Node.Parent != null && e.Node.Parent.Nodes.IndexOf(e.Node) == 0) { upToolStripMenuItem.Enabled = tsButtonMoveUp.Enabled = false; }
            else if (e.Node.Parent != null && e.Node.Parent.Nodes.IndexOf(e.Node) == e.Node.Parent.Nodes.Count - 1) { downToolStripMenuItem.Enabled = tsButtonMoveDn.Enabled = false; }
        }

        private void TreeView_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            tsButtonCollapse.Enabled = !Utilities.NoNodesExpanded(treeView);
            tsButtonExpand.Enabled = true;
            //if (treeView.SelectedNode != null) { tsButtonCollapse.Enabled = false; }
        }

        private void TreeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            tsButtonExpand.Enabled = !Utilities.AllNodesExpanded(treeView);
            tsButtonCollapse.Enabled = true;
            //if (treeView.SelectedNode != null) { tsButtonExpand.Enabled = false; }
        }

        private void TsButtonExpand_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView.SelectedNode;
            if (selectedNode != null)
            {
                if (selectedNode.Parent != null) { selectedNode.Parent.Expand(); }
                else { selectedNode.Expand(); }
            }
            else { treeView.ExpandAll(); }
        }

        private void TsButtonCollapse_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = treeView.SelectedNode;
            if (selectedNode != null)
            {
                if (selectedNode.Parent != null) { selectedNode.Parent.Collapse(); }
                else { selectedNode.Collapse(); }
            }
            else { treeView.CollapseAll(); }
        }

        private void FirstLetterUpperToolStripMenuItem_Click(object sender, EventArgs e)
        { //Clipboard.SetText(Regex.Replace(clipString, @"(?<!\w)\b(\w)(\w*)\b", "$1.ToUpper() + $2"));
            selecedRow.SetField("Text", CultureInfo.InvariantCulture.TextInfo.ToTitleCase(selectedString.ToLower()));
            AcceptChanges(selectedIndex);
        }

        private void LowerCaseLettersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selecedRow.SetField("Text", selectedString.ToLowerInvariant());
            AcceptChanges(selectedIndex);
        }

        private void UpperCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selecedRow.SetField("Text", selectedString.ToUpperInvariant());
            AcceptChanges(selectedIndex);
        }

        private void RemoveSpacesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = Regex.Replace(selectedString, @"[ |\t]", ""); // Space und Tabulator
            selecedRow.SetField("Text", text);
            selecedRow.SetField("Char", text.Length);
            selecedRow.SetField("Word", text.Split(separatorArray, StringSplitOptions.RemoveEmptyEntries).Length); // "Word(s)"
            AcceptChanges(selectedIndex);

        }

        private void RemoveLineBreaksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = Regex.Replace(Regex.Replace(selectedString, @"\t|\r\n?|\n", " "), @"[ ]{2,}", " ").Trim();
            selecedRow.SetField("Text", text);
            selecedRow.SetField("Char", text.Length);
            selecedRow.SetField("Word", text.Split(separatorArray, StringSplitOptions.RemoveEmptyEntries).Length); // "Word(s)"
            AcceptChanges(selectedIndex);
        }

        private void RemoveAllWhiteSpaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = new(selectedString.Where(c => !char.IsWhiteSpace(c)).ToArray());
            selecedRow.SetField("Text", text);
            selecedRow.SetField("Char", text.Length);
            selecedRow.SetField("Word", text.Split(separatorArray, StringSplitOptions.RemoveEmptyEntries).Length); // "Word(s)"
            AcceptChanges(selectedIndex);
        }

        private void AcceptChanges(int selectedRowIndex)
        {
            dataTable.AcceptChanges();
            lboxTable = Utilities.DataTable2LBoxDataTable(dataTable, maxDisplayChars);
            listBox.BeginUpdate();
            listBox.Items.Clear();
            foreach (DataRow row in lboxTable.Rows) { listBox.Items.Add(row); }
            listBox.SelectedIndex = selectedRowIndex;
            listBox.EndUpdate();
            ignoreClipboardChange = true;
            Clipboard.SetText(selecedRow["Text"].ToString());
        }

        private void CalcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Utilities.IsCalcOpen) { Application.OpenForms["FrmClipCalc"]?.Close(); }
            using FrmClipCalc f = new(Utilities.RemoveInvalidCalculationChars(selectedString), decimalPlaces);
            f.ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(f.Result)) { SendSnippet(f.Result.TrimStart('~')); }
            }
            decimalPlaces = f.DecPlaces;
        }

        private void GetSelectedRowText()
        {
            selectedIndex = listBox.SelectedIndex;
            DataRow foundRow = dataTable.AsEnumerable().SingleOrDefault(r => r.Field<DateTime>("Time").Equals(lboxTable.Rows[listBox.SelectedIndex]["Time"]));
            if (selectedIndex >= 0 && foundRow != null && foundRow["Type"].ToString().Equals("text", StringComparison.Ordinal) && !string.IsNullOrEmpty(foundRow["Text"].ToString()))
            {
                selectedString = foundRow["Text"].ToString();
                selecedRow = foundRow;
            }
            else
            {
                selectedString = string.Empty;
                selecedRow = null;
            }
        }

        private void ClipMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (listBox.SelectedIndex == -1) { e.Cancel = true; }
            else
            { //Mnemonic: Control Panel -> Ease of Access -> Change how your keyboard works -> Underline keyboard shortcuts and access keys.
                GetSelectedRowText();
                calcToolStripMenuItem.Visible = tsSeparatorCalc.Visible = selectedString.Any(char.IsDigit);
                linkToolStripMenuItem.Visible = linkToolStripSeparator.Visible = new Regex(@"^((http|https)://|www\.)\S+$").IsMatch(selectedString);
            }
        }

        private void CheckBoxCopyNoBreak_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCopyNoBreak.Focused) { copyNoBreak = checkBoxCopyNoBreak.Checked; }
        }

        private void CheckBoxPlainText_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPlainText.Focused) { plainText = checkBoxPlainText.Checked; }
        }

        private void CheckBoxRWin_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRWin.Focused) { altTabRWin = checkBoxRWin.Checked; }
        }

        private void CheckBoxMenuKey_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMenuKey.Focused) { altTabApps = checkBoxMenuKey.Checked; }
        }

        private void CheckBoxXButton_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxXButton.Focused) { altTabXBtn = checkBoxXButton.Checked; }
        }

        private void GoogleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo psi = new("https://www.google.com/search?q=" + Regex.Replace(Regex.Replace(selectedString, "\r?\n", ""), @"(^\s+|\s+$)", "$1")) { UseShellExecute = true };
                Process.Start(psi);
            }
            catch (Exception ex) when (ex is Win32Exception || ex is InvalidOperationException) { Utilities.ErrorMsgTaskDlg(Handle, ex.Message); }
        }

        private void WikipediaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo psi = new("https://de.wikipedia.org/wiki/Spezial:Suche?search=" + Regex.Replace(Regex.Replace(selectedString, "\r?\n", ""), @"(^\s+|\s+$)", "$1")) { UseShellExecute = true };
                Process.Start(psi);
            }
            catch (Exception ex) when (ex is Win32Exception || ex is InvalidOperationException) { Utilities.ErrorMsgTaskDlg(Handle, ex.Message); }
        }

        private void TranslatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo psi = new("https://translate.google.com/translate_t?hl=en&sl=en&tl=de&q=" + Regex.Replace(Regex.Replace(selectedString, "\r?\n", ""), @"(^\s+|\s+$)", "$1")) { UseShellExecute = true };
                Process.Start(psi);
            }
            catch (Exception ex) when (ex is Win32Exception || ex is InvalidOperationException) { Utilities.ErrorMsgTaskDlg(Handle, ex.Message); }
        }

        private void ÜbersetzerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo psi = new("https://translate.google.com/translate_t?hl=de&sl=de&tl=en&q=" + Regex.Replace(Regex.Replace(selectedString, "\r?\n", ""), @"(^\s+|\s+$)", "$1")) { UseShellExecute = true };
                Process.Start(psi);
            }
            catch (Exception ex) when (ex is Win32Exception || ex is InvalidOperationException) { Utilities.ErrorMsgTaskDlg(Handle, ex.Message); }
        }

        private void LinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo psi = new(Regex.Replace(Regex.Replace(selectedString, "\r?\n", ""), @"(^\s+|\s+$)", "$1")) { UseShellExecute = true };
                Process.Start(psi);
            }
            catch (Exception ex) when (ex is Win32Exception || ex is InvalidOperationException) { Utilities.ErrorMsgTaskDlg(Handle, ex.Message); }
        }

        private void NewButton_CheckedChanged(object sender, EventArgs e) { dontHide = newButton.Checked; }
        private void NewButton_MouseEnter(object sender, EventArgs e) { newButton.ForeColor = Color.White; }
        private void NewButton_MouseLeave(object sender, EventArgs e) { newButton.ForeColor = Color.Black; }

        private void EditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Utilities.IsEditOpen) { Application.OpenForms["FrmClipEdit"]?.Activate(); }
            else { new FrmClipEdit(dataTable.Rows.Count > 0 && dataTable.Rows[0]["Type"].ToString().Equals("text") ? dataTable.Rows[0]["Text"].ToString() : string.Empty, medistarRef).Show(); }
        }

        private void RechnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Utilities.IsCalcOpen) { Application.OpenForms["FrmClipCalc"]?.Activate(); } // if (Application.OpenForms["FrmClipCalc"] == null)
            else { StartClipCalc(); }
        }

        private void StartClipCalc()
        {
            string clipString = Clipboard.ContainsText() ? Clipboard.GetText() : null;
            clipString = !string.IsNullOrEmpty(clipString) && clipString.Any(char.IsDigit) ? clipString.Trim() : string.Empty;
            if (clipString.Length > 0) { clipString = Utilities.RemoveInvalidCalculationChars(clipString); }
            FrmClipCalc frm = new(clipString, decimalPlaces);
            frm.Show();
            frm.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - frm.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - frm.Height) / 2);
        }

        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Visible = !Visible;
                if (Visible) { tabControl.SelectedIndex = 0; } //  s. FrmClipMenu_VisibleChanged BringToFront(); Activate(); 
            }
        }

        private void EditToolStripMenuItem_Click(object sender, EventArgs e) { treeView.SelectedNode?.BeginEdit(); }
        private void UpToolStripMenuItem_Click(object sender, EventArgs e) { TsButtonMoveUp_Click(null, null); }
        private void DownToolStripMenuItem_Click(object sender, EventArgs e) { TsButtonMoveDn_Click(null, null); }
        private void RemoveToolStripMenuItem_Click(object sender, EventArgs e) { TsButtonDelete_Click(null, null); }

        private void CbxVisualResponse_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxVisualResponse.Focused) { visualResponse = cbxVisualResponse.Checked; }
        }

        private void CbxAcousticResponse_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxAcousticResponse.Focused) { acousticResponse = cbxAcousticResponse.Checked; }
        }

        private void File2TxtStripMenuItem_Click(object sender, EventArgs e) { SendText(-1, true); }

        private void SnipMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            TreeNode selectedNode = treeView.SelectedNode;
            linkSeparatorSnipMenu.Visible = openLinkSnipMenuItem.Visible = selectedNode != null && new Regex(@"^((http|https)://|www\.)\S+$").IsMatch(selectedNode.Text);
        }

        private void OpenLinkSnipMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo psi = new(treeView.SelectedNode.Text) { UseShellExecute = true };
                Process.Start(psi);
            }
            catch (Exception ex) when (ex is Win32Exception || ex is InvalidOperationException) { Utilities.ErrorMsgTaskDlg(Handle, ex.Message); }
        }
    }
}