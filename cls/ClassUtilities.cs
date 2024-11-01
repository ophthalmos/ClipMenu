using Microsoft.Win32;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ClipMenu
{
    internal static class Utilities
    {
        //private static IntPtr currentPowerRequest;

        //internal static void LogEvent(string message = "", string logPath = "")
        //{
        //    if (string.IsNullOrEmpty(logPath)) { return; }
        //    try
        //    {
        //        if (NativeMethods.lastActiveWindow == IntPtr.Zero) { Console.Beep(); return; }
        //        StringBuilder sb = new(256);
        //        int charsCopied = NativeMethods.GetWindowText(NativeMethods.lastActiveWindow, sb, sb.Capacity);
        //        using StreamWriter writer = new(logPath, true, Encoding.UTF8); // Datei erstellen oder öffnen
        //        //using StreamWriter writer = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        //        //    Application.ProductName, Application.ProductName + ".log"), true, Encoding.UTF8); // Datei erstellen oder öffnen
        //        writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + (message.Length > 0 ? " | " + message : "") +
        //            (charsCopied > 0 ? string.Concat(" | ", sb.ToString(0, charsCopied)) : ""));
        //        writer.Flush();
        //    }
        //    catch { }
        //}

        internal static void HelpMsgTaskDlg(IntPtr hwnd, Icon icon)
        {
            string foot = "              © 2023-" + GetBuildDate().ToString("yyyy") + " Wilhelm Happe, Version " + Assembly.GetExecutingAssembly().GetName().Version.ToString() + " (" + GetBuildDate().ToString("d") + ")";
            string msg = "Tastenkombinationen:" + Environment.NewLine +
                "¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯" + Environment.NewLine +
                "Strg+Win+V:        Anzeigen des Programmfensters" + Environment.NewLine +
                "Strg+Win+C:        dito, sendet aber zuvor Strg+C" + Environment.NewLine +
                "Strg+Win+Einfg:  ClipEditor, Topmost-Zwischenablage" + Environment.NewLine +
                "Strg+Win+R:         ClipCalc, einfacher Taschenrechner" + Environment.NewLine +
                "Shift+Strg+V:        Text ohne Formatierungen einfügen" + Environment.NewLine + Environment.NewLine +
                "Suchfunktion:" + Environment.NewLine +
                "¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯" + Environment.NewLine +
                "Das Suchfeld wird automatisch aktiviert, wenn getippt wird." + Environment.NewLine + Environment.NewLine +
                "Datenübernahme:" + Environment.NewLine +
                "¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯" + Environment.NewLine +
                "Drücken Sie die Enter oder führen Sie einen Doppelklick aus." + Environment.NewLine +
                "Die ersten Einträge können mit Strg+Zahl selektiert werden.";
            TaskDialog.ShowDialog(hwnd, new TaskDialogPage() { Caption = Application.ProductName + " - Hilfe", Text = msg, Icon = new TaskDialogIcon(icon), AllowCancel = true, Buttons = { TaskDialogButton.OK }, Footnote = foot });
        }

        internal static void ErrorMsgTaskDlg(IntPtr hwnd, string message, TaskDialogIcon taskDialogIcon = null)
        {
            taskDialogIcon = taskDialogIcon ?? TaskDialogIcon.Error;
            TaskDialog.ShowDialog(hwnd, new TaskDialogPage() { Caption = Application.ProductName, SizeToContent = true, Text = message, Icon = taskDialogIcon, AllowCancel = true, Buttons = { TaskDialogButton.OK } });
        }

        internal static DataTable GetDataTable(int maxTextLength)
        {
            DataTable dt = new("Clips");
            DataColumn col1 = dt.Columns.Add("Time", typeof(DateTime));
            col1.DefaultValue = DateTime.Now;
            col1.AllowDBNull = false;
            DataColumn col2 = dt.Columns.Add("Type", typeof(string));
            col2.DefaultValue = null;
            DataColumn col3 = dt.Columns.Add("Text", typeof(string));
            col3.DefaultValue = string.Empty;
            col3.MaxLength = maxTextLength;
            DataColumn col4 = dt.Columns.Add("Char", typeof(int));
            col4.DefaultValue = 0;
            DataColumn col5 = dt.Columns.Add("Word", typeof(int));
            col5.DefaultValue = 0;
            return dt;
        }

        internal static DataTable DataTable2LBoxDataTable(DataTable dt, int maxLength)
        {
            DataTable newTable = dt.Clone();
            foreach (DataRow row in dt.Rows)
            {
                DataRow newRow = newTable.NewRow();
                newRow.ItemArray = row.ItemArray; // Kopieren der Spaltenwerte
                if (!newRow["Type"].ToString().Equals("image")) { newRow["Text"] = PrepareText2ListBox(newRow["Text"].ToString(), maxLength); }
                newTable.Rows.Add(newRow);
            }
            return newTable;
        }

        internal static string MedistarRefWert(double zahl) { return ((zahl >= 0 ? "+" : "-") + string.Format("{0,5:F2}", Math.Abs(zahl))).Replace(",", "."); }

        internal static string LimitedSubstr(string text, int maxLength)
        {
            if (text.Length <= maxLength) { return text; }
            int lastSpaceIndex = text.LastIndexOf(' ', maxLength - 1);
            if (lastSpaceIndex != -1 && lastSpaceIndex > maxLength / 2) { return text[..lastSpaceIndex] + "…"; }
            else { return text[..maxLength] + "…"; }
        }

        internal static string PrepareText2ListBox(string text, int maxLength)
        {
            text = text.Replace("  ", " ").Trim();
            text = new Regex(@"^[^\S\n]+(.+)$", RegexOptions.Multiline).Replace(text, "$1");
            if (text.Length > maxLength)
            {
                text = text[..maxLength];
                if (text.LastIndexOf('\n') > maxLength - 60) { text = text[..text.LastIndexOf('\n')]; }
                text = text + Environment.NewLine + "…"; ;
            }
            return text;
        }

        internal static string ImageToBase64String(Image image)
        {
            using MemoryStream ms = new();
            image.Save(ms, ImageFormat.Png);
            return Convert.ToBase64String(ms.ToArray());
        }

        public static string RemoveInvalidCalculationChars(string text)
        {
            if (new Regex(@"^0[×|x][a-zA-Z0-9]+$").Match(text.Trim()).Success) { return text.Trim(); }
            else
            {
                text = new string(text.Where(c => char.IsDigit(c) || c == '.' || c == ',' || c == '+' || c == '-' || c == '(' || c == ')' || c == '*' ||
                    c == '/' || c == '÷' || c == '×' || c == 'x' || c == '^' || c == ':').ToArray());
                return text.Replace('.', ',').Replace('÷', '/').Replace(':', '/').Replace("\n", " "); ;
            }
        }

        //public static string RemoveInvalidXmlChars(string text)
        //{
        //    if (text == null || text.Length == 0) { return text; }
        //    StringBuilder result = null; // a bit complicated, but avoids memory usage if not necessary
        //    for (int i = 0; i < text.Length; i++)
        //    {
        //        char ch = text[i];
        //        if (XmlConvert.IsXmlChar(ch)) { result?.Append(ch); }
        //        else if (result == null)
        //        {
        //            result = new StringBuilder();
        //            result.Append(text.AsSpan(0, i));
        //        }
        //    }
        //    if (result == null) { return text; } // no invalid xml chars detected - return original text
        //    else { return result.ToString(); }
        //}

        internal static Tuple<string, bool> GetDateTimeNowFormatted(string dateString)
        {
            // Formate, die nicht durch GetAllDateTimePatterns geprüft werden
            if (DateTime.TryParseExact(dateString, "d.M.", CultureInfo.CurrentCulture, DateTimeStyles.None, out _)) { return Tuple.Create(DateTime.Now.ToString("d.M."), true); }
            if (DateTime.TryParseExact(dateString, "d.M.yy", CultureInfo.CurrentCulture, DateTimeStyles.None, out _)) { return Tuple.Create(DateTime.Now.ToString("d.M.yy"), true); }
            if (DateTime.TryParseExact(dateString, "dddd, d. MMMM yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out _)) { return Tuple.Create(DateTime.Now.ToString("dddd, d. MMMM yyyy"), true); }
            if (DateTime.TryParseExact(dateString, "dddd, 'den' d. MMMM yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out _)) { return Tuple.Create(DateTime.Now.ToString("dddd, 'den' d. MMMM yyyy"), true); }
            // Standardformate (GetAllDateTimePatterns)
            DateTimeFormatInfo dateTimeFormatInfo = CultureInfo.CurrentCulture.DateTimeFormat;
            string[] formats = dateTimeFormatInfo.GetAllDateTimePatterns();
            foreach (string format in formats)
            {
                if (DateTime.TryParseExact(dateString, format, CultureInfo.CurrentCulture, DateTimeStyles.None, out _)) { return Tuple.Create(DateTime.Now.ToString(format), true); }
            }
            return Tuple.Create(dateString, false); // ohne Änderung
        }

        internal static void StartFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    ProcessStartInfo psi = new(filePath) { UseShellExecute = true, WorkingDirectory = Path.GetDirectoryName(filePath) };
                    Process.Start(psi);
                }
            }
            catch (Exception ex) when (ex is Win32Exception || ex is InvalidOperationException) { ErrorMsgTaskDlg(Application.OpenForms[0].Handle, ex.Message); }
        }

        internal static List<XElement> CreateXmlElementList(TreeNodeCollection treeNodes)
        {
            List<XElement> elements = [];
            foreach (TreeNode treeNode in treeNodes)
            {
                if (string.IsNullOrEmpty(treeNode.Name)) { continue; }
                XElement element = new(treeNode.Name);
                if (treeNode.Parent != null) { element.Value = treeNode.Text + (string.IsNullOrEmpty(treeNode.ToolTipText) ? "" : "|" + treeNode.ToolTipText); } 
                else { element.Add(CreateXmlElementList(treeNode.Nodes)); } // d.h. Funktion wird ein zweites Mal durchlaufen 
                elements.Add(element);
            }
            return elements;
        }

        internal static bool NodeFiltering(TreeNode node, string texto)
        {
            bool result = false;
            if (node.Nodes.Count == 0)
            {
                if (node.Text.Contains(texto, StringComparison.CurrentCultureIgnoreCase)) { result = true; }
                else { node.Remove(); }
            }
            else
            {
                for (int i = node.Nodes.Count; i > 0; i--)
                {
                    if (NodeFiltering(node.Nodes[i - 1], texto)) { result = true; }
                }
                if (!result) { node.Remove(); }
            }
            return result;
        }

        internal static TreeView CloneTreeView(TreeView treeView)
        {
            TreeView cloned = new();
            foreach (TreeNode node in treeView.Nodes) { cloned.Nodes.Add((TreeNode)node.Clone()); }
            return cloned;
        }

        internal static bool AllNodesExpanded(TreeView tv)
        {
            foreach (TreeNode node in tv.Nodes)
            {
                if (!node.IsExpanded) { return false; }
            }
            return true;
        }

        internal static bool NoNodesExpanded(TreeView tv)
        {
            foreach (TreeNode node in tv.Nodes)
            {
                if (node.IsExpanded) { return false; }
            }
            return true;
        }

        internal static string CountChildNodes(TreeView tv)
        {
            int count = 0;
            if (tv.Nodes.Count > 0)
            {
                for (int i = 0; i < tv.Nodes.Count; i++) { count += tv.Nodes[i].GetNodeCount(false); }
            }
            return count == 0 ? string.Empty : count == 1 ? "Ein Eintrag" : $"{count} Einträge";
        }

        internal static string CountListBox(ListBox lb)
        {
            int count = lb.Items.Count;
            return count == 0 ? "Keine Einträge" : count == 1 ? "Ein Eintrag" : $"{count} Einträge";
        }

        public static int EndsWithNumber(string source)
        {// gibt Anzahl der zur Dezimalzahl gehörigen Zeichen zurück
            string pattern;
            //string decimalSeparator = Regex.Escape(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            string decimalSeparator = "[.,]"; // Komma und Punkt erlauben
            // ggf. unäres Minus + Zahl + ggf. Kommastellen + ggf. Leerraum ggf. mit schließenden Klammern am Ende
            pattern = @"(?<!\d+\s*)-?\d{1,}" + decimalSeparator + @"{0,1}\d{0,}[\s*\)]*$";
            Match match = Regex.Match(source, pattern);
            if (match.Success) { return match.Length; }
            else { return 0; }
        }

        public static bool IsClipboardEmpty()
        {
            return !typeof(DataFormats).GetFields(BindingFlags.Public | BindingFlags.Static).Select(f => f.Name).Any(Clipboard.ContainsData);
        }

        internal static bool MaybePassword(string text) { return new Regex(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*\W)(?!.* ).{8,18}$").IsMatch(text); }
        /* Password must contain one digit, one lowercase, one uppercase letter, one special character, no space, and it must be 8-18 characters long. */

        internal static bool SetClipboardText(string text)
        {
            try
            {// It retries 5 times with 250 milliseconds between each retry
                Clipboard.SetDataObject(text, false, 5, 250);
                return true;
            }
            catch (Exception ex) when (ex is ExternalException) { return false; }
        }

        //internal static bool SetClipboardImage(Image img)
        //{
        //    try
        //    {// It retries 5 times with 250 milliseconds between each retry
        //        Clipboard.SetDataObject(img, false, 5, 250);
        //        return true;
        //    }
        //    catch (Exception ex) when (ex is ExternalException) { return false; }
        //}

        //internal static bool SetClipboardFiles(System.Collections.Specialized.StringCollection files)
        //{
        //    try
        //    {// It retries 5 times with 250 milliseconds between each retry
        //        Clipboard.SetDataObject(files, false, 5, 250);
        //        return true;
        //    }
        //    catch (Exception ex) when (ex is ExternalException) { return false; }
        //}

        public static bool IsInnoSetupValid(string assemblyLocation)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\ClipMenu_is1");
            if (key == null) return false;
            string value = (string)key.GetValue("UninstallString");
            if (value == null) return false;
            else if (Debugger.IsAttached) { return true; } // run by Visual Studio
            else { return assemblyLocation.Equals(RemoveFromEnd(value.Trim('"'), "\\unins000.exe")); } // "C:\Program Files\ClipMenu\unins000.exe"
        }

        private static string RemoveFromEnd(string str, string toRemove) { return str.EndsWith(toRemove) ? str[..^toRemove.Length] : str; }

        internal static string Truncate(string text, int maxLength = 40, string truncationSuffix = "…")
        {
            return text.Length > maxLength ? text[..maxLength] + truncationSuffix : text;
        }

        internal static bool IsAutoStartEnabled(string taskName)
        {
            ProcessStartInfo start = new()
            {
                FileName = "schtasks.exe", // Specify exe name.
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = "/query /TN \"" + taskName + "\"",
                RedirectStandardOutput = true
            };
            using Process process = Process.Start(start);
            using StreamReader reader = process.StandardOutput;
            string stdout = reader.ReadToEnd();
            if (stdout.Contains(taskName)) { return true; }
            else { return false; }
        }

        internal static void SetAutoStart(string appName, string assemblyLocation)
        {
            new Process()
            {
                StartInfo = {
                  UseShellExecute = false,
                  FileName = "SCHTASKS.exe",
                  RedirectStandardError = true,
                  RedirectStandardOutput = true,
                  CreateNoWindow = true,
                  WindowStyle = ProcessWindowStyle.Hidden,
                  Arguments = string.Format(@"/Create /F /RL HIGHEST /SC ONLOGON /DELAY 0000:45 /TN " + appName + " /TR \"'" + assemblyLocation + "'\"")
               }
            }.Start();
        }

        internal static void UnSetAutoStart(string taskName)
        {
            new Process()
            {
                StartInfo = {
                  UseShellExecute = false,
                  FileName = "SCHTASKS.exe",
                  RedirectStandardError = true,
                  RedirectStandardOutput = true,
                  CreateNoWindow = true,
                  WindowStyle = ProcessWindowStyle.Hidden,
                  Arguments = string.Format(@"/Delete /F /TN " + taskName)
               }
            }.Start();
        }

        internal static DateTime GetBuildDate()
        { //s. <SourceRevisionId>build$([System.DateTime]::UtcNow.ToString("yyyyMMddHHmmss"))</SourceRevisionId> in ClipMenu.csproj
            const string BuildVersionMetadataPrefix = "+build";
            AssemblyInformationalVersionAttribute attribute = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            if (attribute?.InformationalVersion != null)
            {
                string value = attribute.InformationalVersion;
                int index = value.IndexOf(BuildVersionMetadataPrefix);
                if (index > 0)
                {
                    value = value[(index + BuildVersionMetadataPrefix.Length)..];
                    if (DateTime.TryParseExact(value, "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result)) { return result; }
                }
            }
            return default;
        }

        //internal static void SuppressStandby(IntPtr hwnd)
        //{
        //    if (currentPowerRequest != IntPtr.Zero) // Clear current power request if there is any.
        //    {
        //        NativeMethods.PowerClearRequest(currentPowerRequest, NativeMethods.PowerRequestType.PowerRequestSystemRequired);
        //        currentPowerRequest = IntPtr.Zero;
        //    }
        //    NativeMethods.POWER_REQUEST_CONTEXT pContext;  // Create new power request.
        //    pContext.Flags = NativeMethods.POWER_REQUEST_CONTEXT_SIMPLE_STRING;
        //    pContext.Version = NativeMethods.POWER_REQUEST_CONTEXT_VERSION;
        //    pContext.SimpleReasonString = "Standby suppressed by PowerAvailabilityRequests.exe"; // shown when the command "powercfg -requests" is executed.
        //    currentPowerRequest = NativeMethods.PowerCreateRequest(ref pContext);
        //    if (currentPowerRequest == IntPtr.Zero) { ErrorMsgTaskDlg(hwnd, "Failed to create power request."); }
        //    if (!NativeMethods.PowerSetRequest(currentPowerRequest, NativeMethods.PowerRequestType.PowerRequestSystemRequired))
        //    {
        //        ErrorMsgTaskDlg(hwnd, "Failed to set power request.");
        //        currentPowerRequest = IntPtr.Zero;
        //    }
        //}

        //internal static void EnableStandby(IntPtr hwnd)
        //{
        //    if (currentPowerRequest != IntPtr.Zero)
        //    {
        //        if (!NativeMethods.PowerClearRequest(currentPowerRequest, NativeMethods.PowerRequestType.PowerRequestSystemRequired))
        //        {
        //            ErrorMsgTaskDlg(hwnd, "Failed to clear power request.");
        //            currentPowerRequest = IntPtr.Zero;
        //        }
        //        else { currentPowerRequest = IntPtr.Zero; }
        //    }
        //}

        public static List<string> SearchHistory { get; set; }

        // Statische Klasse zum Verwalten des Zustands von Form2 // public static class FormManager // {
        private static bool clipCalcIsOpen = false;
        private static bool clipEditIsOpen = false;

        public static bool IsCalcOpen
        {
            get { return clipCalcIsOpen; }
            set { clipCalcIsOpen = value; }
        }
        public static bool IsEditOpen
        {
            get { return clipEditIsOpen; }
            set { clipEditIsOpen = value; }
        }
    }
}
