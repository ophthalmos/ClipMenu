namespace ClipMenu
{
    partial class FrmClipMenu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            TreeNode treeNode1 = new TreeNode("1.3.");
            TreeNode treeNode2 = new TreeNode("1.3.24");
            TreeNode treeNode3 = new TreeNode("01.03.24");
            TreeNode treeNode4 = new TreeNode("1.3.2024");
            TreeNode treeNode5 = new TreeNode("01.03.2024");
            TreeNode treeNode6 = new TreeNode("1. März 2024");
            TreeNode treeNode7 = new TreeNode("Sonntag, den 3. März 2024");
            TreeNode treeNode8 = new TreeNode("Daten", new TreeNode[] { treeNode1, treeNode2, treeNode3, treeNode4, treeNode5, treeNode6, treeNode7 });
            TreeNode treeNode9 = new TreeNode("Viele Grüße");
            TreeNode treeNode10 = new TreeNode("Texte", new TreeNode[] { treeNode9 });
            TreeNode treeNode11 = new TreeNode("😊");
            TreeNode treeNode12 = new TreeNode("Zeichen", new TreeNode[] { treeNode11 });
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmClipMenu));
            listBox = new ListBox();
            contextMenuStrip = new ContextMenuStrip(components);
            tsConvertSubMenu = new ToolStripMenuItem();
            clipMenuStrip = new ContextMenuStrip(components);
            firstLetterUpperToolStripMenuItem = new ToolStripMenuItem();
            lowerCaseLettersToolStripMenuItem = new ToolStripMenuItem();
            upperCaseToolStripMenuItem = new ToolStripMenuItem();
            removeSpacesToolStripMenuItem = new ToolStripMenuItem();
            removeLineBreaksToolStripMenuItem = new ToolStripMenuItem();
            tsSeparatorCalc = new ToolStripSeparator();
            calcToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator11 = new ToolStripSeparator();
            googleToolStripMenuItem = new ToolStripMenuItem();
            wikipediaToolStripMenuItem = new ToolStripMenuItem();
            translatorToolStripMenuItem = new ToolStripMenuItem();
            übersetzerToolStripMenuItem = new ToolStripMenuItem();
            linkToolStripSeparator = new ToolStripSeparator();
            linkToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            sendToolStripMenuItem = new ToolStripMenuItem();
            file2TxtStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator9 = new ToolStripSeparator();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator6 = new ToolStripSeparator();
            snippetToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator7 = new ToolStripSeparator();
            propertyToolStripMenuItem = new ToolStripMenuItem();
            tabControl = new TabControl();
            tabPage1 = new TabPage();
            panelTop = new Panel();
            btnDeleteAll = new Button();
            lblTbClear = new Label();
            tbSearch = new TextBox();
            tabPage2 = new TabPage();
            treeView = new TreeView();
            snipMenuStrip = new ContextMenuStrip(components);
            editToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator12 = new ToolStripSeparator();
            upToolStripMenuItem = new ToolStripMenuItem();
            downToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator13 = new ToolStripSeparator();
            removeToolStripMenuItem = new ToolStripMenuItem();
            toolStrip = new ToolStrip();
            tsButtonNew = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            tsButtonMoveUp = new ToolStripButton();
            tsButtonMoveDn = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            tsButtonDelete = new ToolStripButton();
            tsRestoreBtn = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            tsButtonExpAll = new ToolStripButton();
            tsButtonExpNone = new ToolStripButton();
            toolStripSeparator8 = new ToolStripSeparator();
            snippetSearchBox = new ToolStripTextBox();
            tabPage3 = new TabPage();
            gbxClipboardChange = new GroupBox();
            cbxAcousticResponse = new CheckBox();
            cbxVisualResponse = new CheckBox();
            groupBox = new GroupBox();
            checkBoxCopyNoBreak = new CheckBox();
            checkBoxMenuKey = new CheckBox();
            checkBoxPlainText = new CheckBox();
            checkBoxXButton = new CheckBox();
            checkBoxRWin = new CheckBox();
            lblMaxComment = new Label();
            lblPasswords = new Label();
            lblAutostart = new Label();
            ckbRegex = new CheckBox();
            btnStandardSize = new Button();
            lblMaxItems = new Label();
            cbxMaxItems = new ComboBox();
            linkLabel = new LinkLabel();
            ckbAutoStart = new CheckBox();
            notifyIcon = new NotifyIcon(components);
            trayMenuStrip = new ContextMenuStrip(components);
            editorToolStripMenuItem = new ToolStripMenuItem();
            rechnerToolStripMenuItem = new ToolStripMenuItem();
            copyStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator10 = new ToolStripSeparator();
            showToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator = new ToolStripSeparator();
            restartToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            toolStripStatusLabel = new ToolStripStatusLabel();
            toolTip = new ToolTip(components);
            timer = new System.Windows.Forms.Timer(components);
            removeAllWhiteSpaceToolStripMenuItem = new ToolStripMenuItem();
            contextMenuStrip.SuspendLayout();
            clipMenuStrip.SuspendLayout();
            tabControl.SuspendLayout();
            tabPage1.SuspendLayout();
            panelTop.SuspendLayout();
            tabPage2.SuspendLayout();
            snipMenuStrip.SuspendLayout();
            toolStrip.SuspendLayout();
            tabPage3.SuspendLayout();
            gbxClipboardChange.SuspendLayout();
            groupBox.SuspendLayout();
            trayMenuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // listBox
            // 
            listBox.ContextMenuStrip = contextMenuStrip;
            listBox.Dock = DockStyle.Fill;
            listBox.DrawMode = DrawMode.OwnerDrawVariable;
            listBox.Font = new Font("Segoe UI", 10F);
            listBox.FormattingEnabled = true;
            listBox.ItemHeight = 15;
            listBox.Location = new Point(3, 36);
            listBox.Name = "listBox";
            listBox.ScrollAlwaysVisible = true;
            listBox.Size = new Size(316, 472);
            listBox.TabIndex = 2;
            listBox.DrawItem += ListBox_DrawItem;
            listBox.MeasureItem += ListBox_MeasureItem;
            listBox.Enter += ListBox_Enter;
            listBox.KeyDown += ListBox_KeyDown;
            listBox.KeyPress += ListBox_KeyPress;
            listBox.Leave += ListBox_Leave;
            listBox.MouseDoubleClick += ListBox_MouseDoubleClick;
            listBox.MouseDown += ListBox_MouseDown;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { tsConvertSubMenu, toolStripSeparator5, sendToolStripMenuItem, file2TxtStripMenuItem, toolStripSeparator9, deleteToolStripMenuItem, toolStripSeparator6, snippetToolStripMenuItem, toolStripSeparator7, propertyToolStripMenuItem });
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(228, 160);
            contextMenuStrip.Opening += ContextMenuStrip_Opening;
            // 
            // tsConvertSubMenu
            // 
            tsConvertSubMenu.DropDown = clipMenuStrip;
            tsConvertSubMenu.Image = Properties.Resources.EditTask;
            tsConvertSubMenu.ImageTransparentColor = Color.Transparent;
            tsConvertSubMenu.Name = "tsConvertSubMenu";
            tsConvertSubMenu.ShortcutKeyDisplayString = "F2";
            tsConvertSubMenu.Size = new Size(227, 22);
            tsConvertSubMenu.Text = "&Verarbeiten";
            // 
            // clipMenuStrip
            // 
            clipMenuStrip.Font = new Font("Segoe UI", 10F);
            clipMenuStrip.Items.AddRange(new ToolStripItem[] { firstLetterUpperToolStripMenuItem, lowerCaseLettersToolStripMenuItem, upperCaseToolStripMenuItem, removeSpacesToolStripMenuItem, removeLineBreaksToolStripMenuItem, removeAllWhiteSpaceToolStripMenuItem, tsSeparatorCalc, calcToolStripMenuItem, toolStripSeparator11, googleToolStripMenuItem, wikipediaToolStripMenuItem, translatorToolStripMenuItem, übersetzerToolStripMenuItem, linkToolStripSeparator, linkToolStripMenuItem });
            clipMenuStrip.Name = "clipMenuStrip";
            clipMenuStrip.Size = new Size(240, 332);
            clipMenuStrip.Opening += ClipMenuStrip_Opening;
            // 
            // firstLetterUpperToolStripMenuItem
            // 
            firstLetterUpperToolStripMenuItem.Font = new Font("Segoe UI", 10F);
            firstLetterUpperToolStripMenuItem.Image = Properties.Resources.CaseSensitive_16x;
            firstLetterUpperToolStripMenuItem.ImageTransparentColor = Color.White;
            firstLetterUpperToolStripMenuItem.Name = "firstLetterUpperToolStripMenuItem";
            firstLetterUpperToolStripMenuItem.Size = new Size(239, 24);
            firstLetterUpperToolStripMenuItem.Text = "&Erster Buchstabe Groß";
            firstLetterUpperToolStripMenuItem.Click += FirstLetterUpperToolStripMenuItem_Click;
            // 
            // lowerCaseLettersToolStripMenuItem
            // 
            lowerCaseLettersToolStripMenuItem.Font = new Font("Segoe UI", 10F);
            lowerCaseLettersToolStripMenuItem.Image = Properties.Resources.edit_lowercase;
            lowerCaseLettersToolStripMenuItem.ImageTransparentColor = Color.Transparent;
            lowerCaseLettersToolStripMenuItem.Name = "lowerCaseLettersToolStripMenuItem";
            lowerCaseLettersToolStripMenuItem.Size = new Size(239, 24);
            lowerCaseLettersToolStripMenuItem.Text = "&kleinbuchstaben";
            lowerCaseLettersToolStripMenuItem.Click += LowerCaseLettersToolStripMenuItem_Click;
            // 
            // upperCaseToolStripMenuItem
            // 
            upperCaseToolStripMenuItem.Image = Properties.Resources.edit_uppercase;
            upperCaseToolStripMenuItem.Name = "upperCaseToolStripMenuItem";
            upperCaseToolStripMenuItem.Size = new Size(239, 24);
            upperCaseToolStripMenuItem.Text = "GRO&SSBUCHSTABEN";
            upperCaseToolStripMenuItem.Click += UpperCaseToolStripMenuItem_Click;
            // 
            // removeSpacesToolStripMenuItem
            // 
            removeSpacesToolStripMenuItem.Image = Properties.Resources.String_16x;
            removeSpacesToolStripMenuItem.Name = "removeSpacesToolStripMenuItem";
            removeSpacesToolStripMenuItem.Size = new Size(239, 24);
            removeSpacesToolStripMenuItem.Text = "&Leerzeichen entfernen";
            removeSpacesToolStripMenuItem.Click += RemoveSpacesToolStripMenuItem_Click;
            // 
            // removeLineBreaksToolStripMenuItem
            // 
            removeLineBreaksToolStripMenuItem.Image = Properties.Resources.RemoveCommand_16x;
            removeLineBreaksToolStripMenuItem.Name = "removeLineBreaksToolStripMenuItem";
            removeLineBreaksToolStripMenuItem.Size = new Size(239, 24);
            removeLineBreaksToolStripMenuItem.Text = "&Zeilenumbrüche entfernen";
            removeLineBreaksToolStripMenuItem.Click += RemoveLineBreaksToolStripMenuItem_Click;
            // 
            // tsSeparatorCalc
            // 
            tsSeparatorCalc.Name = "tsSeparatorCalc";
            tsSeparatorCalc.Size = new Size(236, 6);
            // 
            // calcToolStripMenuItem
            // 
            calcToolStripMenuItem.Image = Properties.Resources.calculator__arrow;
            calcToolStripMenuItem.Name = "calcToolStripMenuItem";
            calcToolStripMenuItem.Size = new Size(239, 24);
            calcToolStripMenuItem.Text = "Be&rechnen…";
            calcToolStripMenuItem.Click += CalcToolStripMenuItem_Click;
            // 
            // toolStripSeparator11
            // 
            toolStripSeparator11.Name = "toolStripSeparator11";
            toolStripSeparator11.Size = new Size(236, 6);
            // 
            // googleToolStripMenuItem
            // 
            googleToolStripMenuItem.Image = Properties.Resources.SearchWebHS;
            googleToolStripMenuItem.Name = "googleToolStripMenuItem";
            googleToolStripMenuItem.Size = new Size(239, 24);
            googleToolStripMenuItem.Text = "&Google-Suche";
            googleToolStripMenuItem.Click += GoogleToolStripMenuItem_Click;
            // 
            // wikipediaToolStripMenuItem
            // 
            wikipediaToolStripMenuItem.Image = Properties.Resources.SearchWebHS;
            wikipediaToolStripMenuItem.Name = "wikipediaToolStripMenuItem";
            wikipediaToolStripMenuItem.Size = new Size(239, 24);
            wikipediaToolStripMenuItem.Text = "&Wikipedia-Suche";
            wikipediaToolStripMenuItem.Click += WikipediaToolStripMenuItem_Click;
            // 
            // translatorToolStripMenuItem
            // 
            translatorToolStripMenuItem.Image = Properties.Resources.TranslateDocument_16x;
            translatorToolStripMenuItem.Name = "translatorToolStripMenuItem";
            translatorToolStripMenuItem.Size = new Size(239, 24);
            translatorToolStripMenuItem.Text = "&Translator (engl. → dt.)";
            translatorToolStripMenuItem.Click += TranslatorToolStripMenuItem_Click;
            // 
            // übersetzerToolStripMenuItem
            // 
            übersetzerToolStripMenuItem.Image = Properties.Resources.TranslateDocument_16x;
            übersetzerToolStripMenuItem.Name = "übersetzerToolStripMenuItem";
            übersetzerToolStripMenuItem.Size = new Size(239, 24);
            übersetzerToolStripMenuItem.Text = "&Übersetzer (dt. → engl.)";
            übersetzerToolStripMenuItem.Click += ÜbersetzerToolStripMenuItem_Click;
            // 
            // linkToolStripSeparator
            // 
            linkToolStripSeparator.Name = "linkToolStripSeparator";
            linkToolStripSeparator.Size = new Size(236, 6);
            // 
            // linkToolStripMenuItem
            // 
            linkToolStripMenuItem.Image = Properties.Resources.WebInsertHyperlinkHS;
            linkToolStripMenuItem.Name = "linkToolStripMenuItem";
            linkToolStripMenuItem.Size = new Size(239, 24);
            linkToolStripMenuItem.Text = "&Hyperlink öffnen";
            linkToolStripMenuItem.Click += LinkToolStripMenuItem_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(224, 6);
            // 
            // sendToolStripMenuItem
            // 
            sendToolStripMenuItem.Image = Properties.Resources.clipboard_paste;
            sendToolStripMenuItem.ImageTransparentColor = Color.White;
            sendToolStripMenuItem.Name = "sendToolStripMenuItem";
            sendToolStripMenuItem.ShortcutKeyDisplayString = "Enter";
            sendToolStripMenuItem.Size = new Size(227, 22);
            sendToolStripMenuItem.Text = "&Übertragen";
            sendToolStripMenuItem.Click += SendToolStripMenuItem_Click;
            // 
            // file2TxtStripMenuItem
            // 
            file2TxtStripMenuItem.Name = "file2TxtStripMenuItem";
            file2TxtStripMenuItem.Size = new Size(227, 22);
            file2TxtStripMenuItem.Text = "Dateinamen statt Dateiobjekt";
            file2TxtStripMenuItem.Click += File2TxtStripMenuItem_Click;
            // 
            // toolStripSeparator9
            // 
            toolStripSeparator9.Name = "toolStripSeparator9";
            toolStripSeparator9.Size = new Size(224, 6);
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Image = Properties.Resources.clipboard__minus;
            deleteToolStripMenuItem.ImageTransparentColor = Color.White;
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.ShortcutKeyDisplayString = "Entf";
            deleteToolStripMenuItem.Size = new Size(227, 22);
            deleteToolStripMenuItem.Text = "&Löschen";
            deleteToolStripMenuItem.Click += DeleteToolStripMenuItem_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(224, 6);
            // 
            // snippetToolStripMenuItem
            // 
            snippetToolStripMenuItem.Image = Properties.Resources.clipboard__arrow;
            snippetToolStripMenuItem.ImageTransparentColor = Color.White;
            snippetToolStripMenuItem.Name = "snippetToolStripMenuItem";
            snippetToolStripMenuItem.ShortcutKeyDisplayString = "Strg+I";
            snippetToolStripMenuItem.Size = new Size(227, 22);
            snippetToolStripMenuItem.Text = "&Sammeln";
            snippetToolStripMenuItem.Click += SnippetToolStripMenuItem_Click;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new Size(224, 6);
            // 
            // propertyToolStripMenuItem
            // 
            propertyToolStripMenuItem.Image = Properties.Resources.clipboard_task;
            propertyToolStripMenuItem.Name = "propertyToolStripMenuItem";
            propertyToolStripMenuItem.ShortcutKeyDisplayString = "Alt+Enter";
            propertyToolStripMenuItem.Size = new Size(227, 22);
            propertyToolStripMenuItem.Text = "&Eigenschaften";
            propertyToolStripMenuItem.Click += PropertyToolStripMenuItem_Click;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPage1);
            tabControl.Controls.Add(tabPage2);
            tabControl.Controls.Add(tabPage3);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Segoe UI", 10F);
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(330, 541);
            tabControl.TabIndex = 0;
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(listBox);
            tabPage1.Controls.Add(panelTop);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(322, 511);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Verlauf";
            tabPage1.ToolTipText = "Strg+V";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // panelTop
            // 
            panelTop.Controls.Add(btnDeleteAll);
            panelTop.Controls.Add(lblTbClear);
            panelTop.Controls.Add(tbSearch);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(3, 3);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(316, 33);
            panelTop.TabIndex = 4;
            // 
            // btnDeleteAll
            // 
            btnDeleteAll.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            btnDeleteAll.Enabled = false;
            btnDeleteAll.Location = new Point(210, 2);
            btnDeleteAll.Name = "btnDeleteAll";
            btnDeleteAll.Size = new Size(107, 27);
            btnDeleteAll.TabIndex = 4;
            btnDeleteAll.Text = "Alle löschen";
            toolTip.SetToolTip(btnDeleteAll, "Strg+Entf");
            btnDeleteAll.UseVisualStyleBackColor = true;
            btnDeleteAll.Click += BtnDeleteAll_Click;
            // 
            // lblTbClear
            // 
            lblTbClear.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            lblTbClear.Font = new Font("Segoe UI", 10F);
            lblTbClear.Location = new Point(181, 4);
            lblTbClear.Name = "lblTbClear";
            lblTbClear.Size = new Size(23, 21);
            lblTbClear.TabIndex = 3;
            lblTbClear.Text = "🗙";
            lblTbClear.TextAlign = ContentAlignment.MiddleCenter;
            lblTbClear.Visible = false;
            lblTbClear.Click += LblTbClear_Click;
            // 
            // tbSearch
            // 
            tbSearch.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbSearch.Location = new Point(0, 3);
            tbSearch.Name = "tbSearch";
            tbSearch.PlaceholderText = "Suche";
            tbSearch.Size = new Size(206, 25);
            tbSearch.TabIndex = 1;
            toolTip.SetToolTip(tbSearch, "Strg+F");
            tbSearch.TextChanged += TbSearch_TextChanged;
            tbSearch.KeyDown += TbSearch_KeyDown;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(treeView);
            tabPage2.Controls.Add(toolStrip);
            tabPage2.Location = new Point(4, 26);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(322, 511);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Sammlung";
            toolTip.SetToolTip(tabPage2, "Strg+S");
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // treeView
            // 
            treeView.ContextMenuStrip = snipMenuStrip;
            treeView.Dock = DockStyle.Fill;
            treeView.LabelEdit = true;
            treeView.Location = new Point(3, 29);
            treeView.Name = "treeView";
            treeNode1.Name = "Date1";
            treeNode1.Text = "1.3.";
            treeNode2.Name = "Date2";
            treeNode2.Text = "1.3.24";
            treeNode3.Name = "Date3";
            treeNode3.Text = "01.03.24";
            treeNode4.Name = "Date4";
            treeNode4.Text = "1.3.2024";
            treeNode5.Name = "Date5";
            treeNode5.Text = "01.03.2024";
            treeNode6.Name = "Date6";
            treeNode6.Text = "1. März 2024";
            treeNode7.Name = "Date7";
            treeNode7.Text = "Sonntag, den 3. März 2024";
            treeNode8.Name = "Dates";
            treeNode8.Text = "Daten";
            treeNode9.Name = "Snippet0";
            treeNode9.Text = "Viele Grüße";
            treeNode10.Name = "Snippets";
            treeNode10.Text = "Texte";
            treeNode11.Name = "Symbol0";
            treeNode11.Text = "😊";
            treeNode12.Name = "Symbols";
            treeNode12.Text = "Zeichen";
            treeView.Nodes.AddRange(new TreeNode[] { treeNode8, treeNode10, treeNode12 });
            treeView.ShowNodeToolTips = true;
            treeView.Size = new Size(316, 479);
            treeView.TabIndex = 0;
            treeView.BeforeLabelEdit += TreeView_BeforeLabelEdit;
            treeView.AfterLabelEdit += TreeView_AfterLabelEdit;
            treeView.AfterCollapse += TreeView_AfterCollapse;
            treeView.AfterExpand += TreeView_AfterExpand;
            treeView.AfterSelect += TreeView_AfterSelect;
            treeView.NodeMouseClick += TreeView_NodeMouseClick;
            treeView.NodeMouseDoubleClick += TreeView_NodeMouseDoubleClick;
            treeView.KeyDown += TreeView_KeyDown;
            // 
            // snipMenuStrip
            // 
            snipMenuStrip.Items.AddRange(new ToolStripItem[] { editToolStripMenuItem, toolStripSeparator12, upToolStripMenuItem, downToolStripMenuItem, toolStripSeparator13, removeToolStripMenuItem });
            snipMenuStrip.Name = "snipMenuStrip";
            snipMenuStrip.Size = new Size(158, 104);
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.Image = Properties.Resources.CaseSensitive_16x;
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.ShortcutKeyDisplayString = "F2";
            editToolStripMenuItem.Size = new Size(157, 22);
            editToolStripMenuItem.Text = "Bearbeiten";
            editToolStripMenuItem.Click += EditToolStripMenuItem_Click;
            // 
            // toolStripSeparator12
            // 
            toolStripSeparator12.Name = "toolStripSeparator12";
            toolStripSeparator12.Size = new Size(154, 6);
            // 
            // upToolStripMenuItem
            // 
            upToolStripMenuItem.Image = Properties.Resources.Upload_gray_16x;
            upToolStripMenuItem.Name = "upToolStripMenuItem";
            upToolStripMenuItem.ShortcutKeyDisplayString = "Alt+↑";
            upToolStripMenuItem.Size = new Size(157, 22);
            upToolStripMenuItem.Text = "Aufwärts";
            upToolStripMenuItem.Click += UpToolStripMenuItem_Click;
            // 
            // downToolStripMenuItem
            // 
            downToolStripMenuItem.Image = Properties.Resources.Download_grey_16x;
            downToolStripMenuItem.Name = "downToolStripMenuItem";
            downToolStripMenuItem.ShortcutKeyDisplayString = "Alt+↓";
            downToolStripMenuItem.Size = new Size(157, 22);
            downToolStripMenuItem.Text = "Abwärts";
            downToolStripMenuItem.Click += DownToolStripMenuItem_Click;
            // 
            // toolStripSeparator13
            // 
            toolStripSeparator13.Name = "toolStripSeparator13";
            toolStripSeparator13.Size = new Size(154, 6);
            // 
            // removeToolStripMenuItem
            // 
            removeToolStripMenuItem.Image = Properties.Resources.DeleteHS;
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            removeToolStripMenuItem.ShortcutKeyDisplayString = "Entf";
            removeToolStripMenuItem.Size = new Size(157, 22);
            removeToolStripMenuItem.Text = "Löschen";
            removeToolStripMenuItem.Click += RemoveToolStripMenuItem_Click;
            // 
            // toolStrip
            // 
            toolStrip.Font = new Font("Segoe UI", 10F);
            toolStrip.Items.AddRange(new ToolStripItem[] { tsButtonNew, toolStripSeparator2, tsButtonMoveUp, tsButtonMoveDn, toolStripSeparator1, tsButtonDelete, tsRestoreBtn, toolStripSeparator3, tsButtonExpAll, tsButtonExpNone, toolStripSeparator8, snippetSearchBox });
            toolStrip.LayoutStyle = ToolStripLayoutStyle.Flow;
            toolStrip.Location = new Point(3, 3);
            toolStrip.Name = "toolStrip";
            toolStrip.Size = new Size(316, 26);
            toolStrip.TabIndex = 1;
            // 
            // tsButtonNew
            // 
            tsButtonNew.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsButtonNew.Enabled = false;
            tsButtonNew.Name = "tsButtonNew";
            tsButtonNew.Size = new Size(32, 23);
            tsButtonNew.Text = "🆕";
            tsButtonNew.ToolTipText = "Neuer Eintrag";
            tsButtonNew.Click += TsButtonNew_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 23);
            // 
            // tsButtonMoveUp
            // 
            tsButtonMoveUp.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsButtonMoveUp.Enabled = false;
            tsButtonMoveUp.Name = "tsButtonMoveUp";
            tsButtonMoveUp.Size = new Size(25, 23);
            tsButtonMoveUp.Text = "🡅";
            tsButtonMoveUp.ToolTipText = "Aufwärts";
            tsButtonMoveUp.Click += TsButtonMoveUp_Click;
            // 
            // tsButtonMoveDn
            // 
            tsButtonMoveDn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsButtonMoveDn.Enabled = false;
            tsButtonMoveDn.Name = "tsButtonMoveDn";
            tsButtonMoveDn.Size = new Size(25, 23);
            tsButtonMoveDn.Text = "🡇";
            tsButtonMoveDn.ToolTipText = "Abwärts";
            tsButtonMoveDn.Click += TsButtonMoveDn_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 23);
            // 
            // tsButtonDelete
            // 
            tsButtonDelete.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsButtonDelete.Enabled = false;
            tsButtonDelete.Name = "tsButtonDelete";
            tsButtonDelete.Size = new Size(32, 23);
            tsButtonDelete.Text = "🗑";
            tsButtonDelete.ToolTipText = "Eintrag löschen";
            tsButtonDelete.Click += TsButtonDelete_Click;
            // 
            // tsRestoreBtn
            // 
            tsRestoreBtn.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsRestoreBtn.Enabled = false;
            tsRestoreBtn.Name = "tsRestoreBtn";
            tsRestoreBtn.Size = new Size(32, 23);
            tsRestoreBtn.Text = "🔙";
            tsRestoreBtn.ToolTipText = "Rückgängig";
            tsRestoreBtn.Click += TsRestoreBtn_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 23);
            // 
            // tsButtonExpAll
            // 
            tsButtonExpAll.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsButtonExpAll.Name = "tsButtonExpAll";
            tsButtonExpAll.Size = new Size(32, 23);
            tsButtonExpAll.Text = "➕";
            tsButtonExpAll.ToolTipText = "Expand";
            tsButtonExpAll.Click += TsButtonExpAll_Click;
            // 
            // tsButtonExpNone
            // 
            tsButtonExpNone.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsButtonExpNone.Name = "tsButtonExpNone";
            tsButtonExpNone.Size = new Size(32, 23);
            tsButtonExpNone.Text = "➖";
            tsButtonExpNone.ToolTipText = "Collapse";
            tsButtonExpNone.Click += TsButtonExpNone_Click;
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.Name = "toolStripSeparator8";
            toolStripSeparator8.Size = new Size(6, 23);
            // 
            // snippetSearchBox
            // 
            snippetSearchBox.Alignment = ToolStripItemAlignment.Right;
            snippetSearchBox.Name = "snippetSearchBox";
            snippetSearchBox.Size = new Size(79, 23);
            snippetSearchBox.KeyDown += SnippetSearchBox_KeyDown;
            snippetSearchBox.TextChanged += SnippetSearchBox_TextChanged;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(gbxClipboardChange);
            tabPage3.Controls.Add(groupBox);
            tabPage3.Controls.Add(lblMaxComment);
            tabPage3.Controls.Add(lblPasswords);
            tabPage3.Controls.Add(lblAutostart);
            tabPage3.Controls.Add(ckbRegex);
            tabPage3.Controls.Add(btnStandardSize);
            tabPage3.Controls.Add(lblMaxItems);
            tabPage3.Controls.Add(cbxMaxItems);
            tabPage3.Controls.Add(linkLabel);
            tabPage3.Controls.Add(ckbAutoStart);
            tabPage3.Location = new Point(4, 26);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(322, 511);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Einstellungen";
            tabPage3.ToolTipText = "Strg+E";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // gbxClipboardChange
            // 
            gbxClipboardChange.Controls.Add(cbxAcousticResponse);
            gbxClipboardChange.Controls.Add(cbxVisualResponse);
            gbxClipboardChange.Font = new Font("Segoe UI", 9F);
            gbxClipboardChange.Location = new Point(8, 248);
            gbxClipboardChange.Name = "gbxClipboardChange";
            gbxClipboardChange.Size = new Size(306, 47);
            gbxClipboardChange.TabIndex = 14;
            gbxClipboardChange.TabStop = false;
            gbxClipboardChange.Text = "Signalisierung neuer Verlaufeinträge";
            // 
            // cbxAcousticResponse
            // 
            cbxAcousticResponse.AutoSize = true;
            cbxAcousticResponse.Font = new Font("Segoe UI", 10F);
            cbxAcousticResponse.Location = new Point(123, 20);
            cbxAcousticResponse.Name = "cbxAcousticResponse";
            cbxAcousticResponse.Size = new Size(84, 23);
            cbxAcousticResponse.TabIndex = 1;
            cbxAcousticResponse.Text = "akustisch";
            cbxAcousticResponse.UseVisualStyleBackColor = true;
            cbxAcousticResponse.CheckedChanged += CbxAcousticResponse_CheckedChanged;
            // 
            // cbxVisualResponse
            // 
            cbxVisualResponse.AutoSize = true;
            cbxVisualResponse.Font = new Font("Segoe UI", 10F);
            cbxVisualResponse.Location = new Point(13, 20);
            cbxVisualResponse.Name = "cbxVisualResponse";
            cbxVisualResponse.Size = new Size(65, 23);
            cbxVisualResponse.TabIndex = 0;
            cbxVisualResponse.Text = "visuell";
            cbxVisualResponse.UseVisualStyleBackColor = true;
            cbxVisualResponse.CheckedChanged += CbxVisualResponse_CheckedChanged;
            // 
            // groupBox
            // 
            groupBox.Controls.Add(checkBoxCopyNoBreak);
            groupBox.Controls.Add(checkBoxMenuKey);
            groupBox.Controls.Add(checkBoxPlainText);
            groupBox.Controls.Add(checkBoxXButton);
            groupBox.Controls.Add(checkBoxRWin);
            groupBox.Font = new Font("Segoe UI", 9F);
            groupBox.Location = new Point(8, 301);
            groupBox.Name = "groupBox";
            groupBox.Size = new Size(306, 158);
            groupBox.TabIndex = 13;
            groupBox.TabStop = false;
            groupBox.Text = "Zusätzliche Funktionen (Änderung nach Neustart)";
            // 
            // checkBoxCopyNoBreak
            // 
            checkBoxCopyNoBreak.AutoSize = true;
            checkBoxCopyNoBreak.Font = new Font("Segoe UI", 10F);
            checkBoxCopyNoBreak.Location = new Point(13, 20);
            checkBoxCopyNoBreak.Name = "checkBoxCopyNoBreak";
            checkBoxCopyNoBreak.Size = new Size(277, 23);
            checkBoxCopyNoBreak.TabIndex = 4;
            checkBoxCopyNoBreak.Text = "Strg+Shift+C: Zeilenumbrüche entfernen";
            checkBoxCopyNoBreak.UseVisualStyleBackColor = true;
            checkBoxCopyNoBreak.CheckedChanged += CheckBoxCopyNoBreak_CheckedChanged;
            // 
            // checkBoxMenuKey
            // 
            checkBoxMenuKey.AutoSize = true;
            checkBoxMenuKey.Font = new Font("Segoe UI", 10F);
            checkBoxMenuKey.Location = new Point(13, 130);
            checkBoxMenuKey.Name = "checkBoxMenuKey";
            checkBoxMenuKey.Size = new Size(246, 23);
            checkBoxMenuKey.TabIndex = 3;
            checkBoxMenuKey.Text = "Menü-/Anwendungstaste als AltTab";
            checkBoxMenuKey.UseVisualStyleBackColor = true;
            checkBoxMenuKey.CheckedChanged += CheckBoxMenuKey_CheckedChanged;
            // 
            // checkBoxPlainText
            // 
            checkBoxPlainText.AutoSize = true;
            checkBoxPlainText.Font = new Font("Segoe UI", 10F);
            checkBoxPlainText.Location = new Point(13, 47);
            checkBoxPlainText.Name = "checkBoxPlainText";
            checkBoxPlainText.Size = new Size(278, 23);
            checkBoxPlainText.TabIndex = 2;
            checkBoxPlainText.Text = "Strg+Shift+V: Text unformatiert einfügen";
            checkBoxPlainText.UseVisualStyleBackColor = true;
            checkBoxPlainText.CheckedChanged += CheckBoxPlainText_CheckedChanged;
            // 
            // checkBoxXButton
            // 
            checkBoxXButton.AutoSize = true;
            checkBoxXButton.Font = new Font("Segoe UI", 10F);
            checkBoxXButton.Location = new Point(13, 74);
            checkBoxXButton.Name = "checkBoxXButton";
            checkBoxXButton.Size = new Size(266, 23);
            checkBoxXButton.TabIndex = 1;
            checkBoxXButton.Text = "XButton1: AltTab, XButton2: ShiftAltTab";
            checkBoxXButton.UseVisualStyleBackColor = true;
            checkBoxXButton.CheckedChanged += CheckBoxXButton_CheckedChanged;
            // 
            // checkBoxRWin
            // 
            checkBoxRWin.AutoSize = true;
            checkBoxRWin.Font = new Font("Segoe UI", 10F);
            checkBoxRWin.Location = new Point(13, 101);
            checkBoxRWin.Name = "checkBoxRWin";
            checkBoxRWin.Size = new Size(264, 23);
            checkBoxRWin.TabIndex = 0;
            checkBoxRWin.Text = "rechte Window-Taste für AltTab nutzen";
            checkBoxRWin.UseVisualStyleBackColor = true;
            checkBoxRWin.CheckedChanged += CheckBoxRWin_CheckedChanged;
            // 
            // lblMaxComment
            // 
            lblMaxComment.Font = new Font("Segoe UI", 9F);
            lblMaxComment.Location = new Point(13, 97);
            lblMaxComment.Name = "lblMaxComment";
            lblMaxComment.Size = new Size(301, 38);
            lblMaxComment.TabIndex = 12;
            lblMaxComment.Text = "Wenn das Programm zeitweise zögerlich reagiert (z.B. bei der Suche), sollten Sie die Anzahl  reduzieren.";
            // 
            // lblPasswords
            // 
            lblPasswords.Font = new Font("Segoe UI", 9F);
            lblPasswords.Location = new Point(13, 161);
            lblPasswords.Name = "lblPasswords";
            lblPasswords.Size = new Size(301, 82);
            lblPasswords.TabIndex = 11;
            lblPasswords.Text = resources.GetString("lblPasswords.Text");
            // 
            // lblAutostart
            // 
            lblAutostart.Font = new Font("Segoe UI", 9F);
            lblAutostart.Location = new Point(13, 32);
            lblAutostart.Name = "lblAutostart";
            lblAutostart.Size = new Size(301, 37);
            lblAutostart.TabIndex = 10;
            lblAutostart.Text = "Das Programm benötigt Administratorrechte, um Ein-\r\nträge in anderen Anwendungen einfügen zu können.";
            // 
            // ckbRegex
            // 
            ckbRegex.AutoSize = true;
            ckbRegex.Font = new Font("Segoe UI", 10F);
            ckbRegex.Location = new Point(8, 139);
            ckbRegex.Name = "ckbRegex";
            ckbRegex.Size = new Size(269, 23);
            ckbRegex.TabIndex = 9;
            ckbRegex.Text = "Passwörter ausschließen (experimentell)";
            ckbRegex.UseVisualStyleBackColor = true;
            ckbRegex.CheckedChanged += CkbRegex_CheckedChanged;
            // 
            // btnStandardSize
            // 
            btnStandardSize.Enabled = false;
            btnStandardSize.Font = new Font("Segoe UI", 10F);
            btnStandardSize.Location = new Point(8, 465);
            btnStandardSize.Name = "btnStandardSize";
            btnStandardSize.Size = new Size(306, 34);
            btnStandardSize.TabIndex = 8;
            btnStandardSize.Text = "Fenster auf Standardgröße zurücksetzen";
            btnStandardSize.TextAlign = ContentAlignment.MiddleLeft;
            btnStandardSize.UseVisualStyleBackColor = true;
            btnStandardSize.Click += BtnStandardSize_Click;
            // 
            // lblMaxItems
            // 
            lblMaxItems.AutoSize = true;
            lblMaxItems.Font = new Font("Segoe UI", 10F);
            lblMaxItems.Location = new Point(5, 72);
            lblMaxItems.Name = "lblMaxItems";
            lblMaxItems.Size = new Size(220, 19);
            lblMaxItems.TabIndex = 3;
            lblMaxItems.Text = "Anzahl der gespeicherten Einträge:";
            // 
            // cbxMaxItems
            // 
            cbxMaxItems.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxMaxItems.Font = new Font("Segoe UI", 10F);
            cbxMaxItems.FormattingEnabled = true;
            cbxMaxItems.Items.AddRange(new object[] { "10", "20", "30", "40", "50", "60", "70", "80", "90", "100" });
            cbxMaxItems.Location = new Point(234, 69);
            cbxMaxItems.MaxLength = 2;
            cbxMaxItems.Name = "cbxMaxItems";
            cbxMaxItems.Size = new Size(60, 25);
            cbxMaxItems.TabIndex = 2;
            cbxMaxItems.SelectedIndexChanged += CbxMaxItems_SelectedIndexChanged;
            // 
            // linkLabel
            // 
            linkLabel.AutoSize = true;
            linkLabel.Font = new Font("Segoe UI", 10F);
            linkLabel.Location = new Point(178, 11);
            linkLabel.Name = "linkLabel";
            linkLabel.Size = new Size(118, 19);
            linkLabel.TabIndex = 1;
            linkLabel.TabStop = true;
            linkLabel.Text = "Aufgabenplanung";
            linkLabel.LinkClicked += LinkLabel_LinkClicked;
            // 
            // ckbAutoStart
            // 
            ckbAutoStart.AutoSize = true;
            ckbAutoStart.Font = new Font("Segoe UI", 10F);
            ckbAutoStart.Location = new Point(8, 10);
            ckbAutoStart.Name = "ckbAutoStart";
            ckbAutoStart.Size = new Size(174, 23);
            ckbAutoStart.TabIndex = 0;
            ckbAutoStart.Text = "Automatischer Start per";
            ckbAutoStart.UseVisualStyleBackColor = false;
            ckbAutoStart.CheckedChanged += CbAutoStart_CheckedChanged;
            // 
            // notifyIcon
            // 
            notifyIcon.ContextMenuStrip = trayMenuStrip;
            notifyIcon.Icon = (Icon)resources.GetObject("notifyIcon.Icon");
            notifyIcon.Text = "ClipMenu";
            notifyIcon.Visible = true;
            notifyIcon.MouseClick += NotifyIcon_MouseClick;
            // 
            // trayMenuStrip
            // 
            trayMenuStrip.Items.AddRange(new ToolStripItem[] { editorToolStripMenuItem, rechnerToolStripMenuItem, copyStripMenuItem, toolStripSeparator10, showToolStripMenuItem, toolStripSeparator, restartToolStripMenuItem, toolStripSeparator4, exitToolStripMenuItem });
            trayMenuStrip.Name = "trayMenuStrip";
            trayMenuStrip.Size = new Size(208, 154);
            // 
            // editorToolStripMenuItem
            // 
            editorToolStripMenuItem.Image = Properties.Resources.EditInput_16x;
            editorToolStripMenuItem.Name = "editorToolStripMenuItem";
            editorToolStripMenuItem.ShortcutKeyDisplayString = "Strg+Win+Einfg";
            editorToolStripMenuItem.Size = new Size(207, 22);
            editorToolStripMenuItem.Text = "Clip&Edit";
            editorToolStripMenuItem.Click += EditorToolStripMenuItem_Click;
            // 
            // rechnerToolStripMenuItem
            // 
            rechnerToolStripMenuItem.Image = Properties.Resources.calculator__arrow;
            rechnerToolStripMenuItem.Name = "rechnerToolStripMenuItem";
            rechnerToolStripMenuItem.ShortcutKeyDisplayString = "Strg+Win+R";
            rechnerToolStripMenuItem.Size = new Size(207, 22);
            rechnerToolStripMenuItem.Text = "Clip&Calc";
            rechnerToolStripMenuItem.Click += RechnerToolStripMenuItem_Click;
            // 
            // copyStripMenuItem
            // 
            copyStripMenuItem.Font = new Font("Segoe UI", 9F);
            copyStripMenuItem.Image = Properties.Resources.clipboard__arrow;
            copyStripMenuItem.ImageTransparentColor = Color.White;
            copyStripMenuItem.Name = "copyStripMenuItem";
            copyStripMenuItem.ShortcutKeyDisplayString = "Strg+Win+C";
            copyStripMenuItem.Size = new Size(207, 22);
            copyStripMenuItem.Text = "Clip&Action";
            copyStripMenuItem.Click += CopyStripMenuItem_Click;
            // 
            // toolStripSeparator10
            // 
            toolStripSeparator10.Name = "toolStripSeparator10";
            toolStripSeparator10.Size = new Size(204, 6);
            // 
            // showToolStripMenuItem
            // 
            showToolStripMenuItem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            showToolStripMenuItem.Image = Properties.Resources.clipboard_task;
            showToolStripMenuItem.ImageTransparentColor = Color.White;
            showToolStripMenuItem.Name = "showToolStripMenuItem";
            showToolStripMenuItem.ShortcutKeyDisplayString = "Strg+Win+V";
            showToolStripMenuItem.Size = new Size(207, 22);
            showToolStripMenuItem.Text = "Clip&Menu";
            showToolStripMenuItem.Click += ShowToolStripMenuItem_Click;
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(204, 6);
            // 
            // restartToolStripMenuItem
            // 
            restartToolStripMenuItem.Image = Properties.Resources.restart_16x;
            restartToolStripMenuItem.ImageTransparentColor = Color.White;
            restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            restartToolStripMenuItem.Size = new Size(207, 22);
            restartToolStripMenuItem.Text = "&Neustarten";
            restartToolStripMenuItem.Click += RestartToolStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(204, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Image = Properties.Resources.close_16x16;
            exitToolStripMenuItem.ImageTransparentColor = Color.White;
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(207, 22);
            exitToolStripMenuItem.Text = "&Beenden";
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel });
            statusStrip.Location = new Point(0, 541);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(330, 22);
            statusStrip.TabIndex = 4;
            statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            toolStripStatusLabel.Name = "toolStripStatusLabel";
            toolStripStatusLabel.Size = new Size(315, 17);
            toolStripStatusLabel.Spring = true;
            toolStripStatusLabel.Click += ToolStripStatusLabel_Click;
            // 
            // timer
            // 
            timer.Interval = 200;
            timer.Tick += Timer_Tick;
            // 
            // removeAllWhiteSpaceToolStripMenuItem
            // 
            removeAllWhiteSpaceToolStripMenuItem.Image = Properties.Resources.StatusBlockedOutline_16x;
            removeAllWhiteSpaceToolStripMenuItem.Name = "removeAllWhiteSpaceToolStripMenuItem";
            removeAllWhiteSpaceToolStripMenuItem.Size = new Size(239, 24);
            removeAllWhiteSpaceToolStripMenuItem.Text = "&All Whitespace entfernen";
            removeAllWhiteSpaceToolStripMenuItem.Click += RemoveAllWhiteSpaceToolStripMenuItem_Click;
            // 
            // FrmClipMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(330, 563);
            Controls.Add(tabControl);
            Controls.Add(statusStrip);
            Font = new Font("Segoe UI", 10F);
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(346, 570);
            Name = "FrmClipMenu";
            ShowInTaskbar = false;
            Text = "ClipMenu";
            TopMost = true;
            HelpButtonClicked += FrmClipMenu_HelpButtonClicked;
            Deactivate += FrmClipMenu_Deactivate;
            FormClosing += FrmClipMenu_FormClosing;
            Load += FrmClipMenu_Load;
            Shown += FrmClipMenu_Shown;
            SizeChanged += FrmClipMenu_SizeChanged;
            VisibleChanged += FrmClipMenu_VisibleChanged;
            HelpRequested += FrmClipMenu_HelpRequested;
            contextMenuStrip.ResumeLayout(false);
            clipMenuStrip.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            snipMenuStrip.ResumeLayout(false);
            toolStrip.ResumeLayout(false);
            toolStrip.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            gbxClipboardChange.ResumeLayout(false);
            gbxClipboardChange.PerformLayout();
            groupBox.ResumeLayout(false);
            groupBox.PerformLayout();
            trayMenuStrip.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listBox;
        private TabControl tabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TextBox tbSearch;
        private NotifyIcon notifyIcon;
        private ContextMenuStrip trayMenuStrip;
        private ToolStripMenuItem showToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripMenuItem exitToolStripMenuItem;
        private TabPage tabPage3;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private CheckBox ckbAutoStart;
        private TreeView treeView;
        private ToolStrip toolStrip;
        private Label lblTbClear;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripStatusLabel;
        private ToolStripButton tsButtonNew;
        private ToolStripButton tsButtonDelete;
        private ToolStripButton tsButtonMoveUp;
        private ToolStripButton tsButtonMoveDn;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton tsButtonExpAll;
        private ToolStripButton tsButtonExpNone;
        private LinkLabel linkLabel;
        private Label lblMaxItems;
        private ComboBox cbxMaxItems;
        private Button btnStandardSize;
        private ToolStripMenuItem restartToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem sendToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolTip toolTip;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem snippetToolStripMenuItem;
        private CheckBox ckbRegex;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem propertyToolStripMenuItem;
        private Panel panelTop;
        private Button btnDeleteAll;
        private Label lblMaxComment;
        private Label lblPasswords;
        private Label lblAutostart;
        private ToolStripButton tsRestoreBtn;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripTextBox snippetSearchBox;
        private ToolStripMenuItem firstLetterUpperToolStripMenuItem;
        private ToolStripMenuItem lowerCaseLettersToolStripMenuItem;
        private ContextMenuStrip clipMenuStrip;
        private ToolStripSeparator toolStripSeparator9;
        private ToolStripMenuItem tsConvertSubMenu;
        private ToolStripMenuItem upperCaseToolStripMenuItem;
        private ToolStripSeparator tsSeparatorCalc;
        private ToolStripMenuItem calcToolStripMenuItem;
        private GroupBox groupBox;
        private CheckBox checkBoxXButton;
        private CheckBox checkBoxRWin;
        private ToolStripSeparator toolStripSeparator11;
        private ToolStripMenuItem googleToolStripMenuItem;
        private ToolStripMenuItem wikipediaToolStripMenuItem;
        private ToolStripMenuItem translatorToolStripMenuItem;
        private ToolStripMenuItem übersetzerToolStripMenuItem;
        private System.Windows.Forms.Timer timer;
        private ToolStripMenuItem editorToolStripMenuItem;
        private ToolStripMenuItem rechnerToolStripMenuItem;
        private CheckBox checkBoxPlainText;
        private ToolStripMenuItem removeLineBreaksToolStripMenuItem;
        private ToolStripMenuItem copyStripMenuItem;
        private ToolStripSeparator toolStripSeparator10;
        private ContextMenuStrip snipMenuStrip;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator12;
        private ToolStripMenuItem upToolStripMenuItem;
        private ToolStripMenuItem downToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator13;
        private ToolStripMenuItem removeToolStripMenuItem;
        private GroupBox gbxClipboardChange;
        private CheckBox cbxVisualResponse;
        private CheckBox cbxAcousticResponse;
        private CheckBox checkBoxMenuKey;
        private ToolStripMenuItem file2TxtStripMenuItem;
        private ToolStripSeparator linkToolStripSeparator;
        private ToolStripMenuItem linkToolStripMenuItem;
        private CheckBox checkBoxCopyNoBreak;
        private ToolStripMenuItem removeSpacesToolStripMenuItem;
        private ToolStripMenuItem removeAllWhiteSpaceToolStripMenuItem;
    }
}
