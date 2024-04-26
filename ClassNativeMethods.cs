using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

namespace ClipMenu
{
    internal static class NativeMethods
    {
        //public static FrmClipEdit _clipEditForm;

        //private static FrmClipMenu _mainForm;

        internal const int EC_LEFTMARGIN = 0x1;
        internal const int EM_SETMARGINS = 0xD3;

        //internal const int WM_HOTKEY = 0x312;
        internal const int WM_CLIPBOARDUPDATE = 0x031D;
        //internal const int HOTKEY_ID1 = 1;
        //internal const int HOTKEY_ID2 = 2;
        private const int GWL_EXSTYLE = -20;
        private const int GWL_STYLE = -16;
        private const int GW_OWNER = 4;
        private const uint WS_EX_TOOLWINDOW = 0x00000080;
        private const uint WS_EX_APPWINDOW = 0x40000; // provides a taskbar button for a window that would otherwise lack one
        //private const uint WS_EX_TOPMOST = 0x00000008;
        private const int DWMWA_CLOAKED = 14;

        private const int WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX;
        private const int WS_OVERLAPPED = 0x00000000;
        private const int WS_CAPTION = 0x00C00000;
        private const int WS_SYSMENU = 0x00080000;
        private const int WS_THICKFRAME = 0x00040000;
        private const int WS_MINIMIZEBOX = 0x00020000;
        private const int WS_MAXIMIZEBOX = 0x00010000;

        //internal const int HWND_BROADCAST = 0xffff;
        //private const int WM_COPY = 0x0301;
        //private const int WM_PASTE = 0x0302;
        private const int WM_KEYDOWN = 0x0100; // 256
        //private const int WM_KEYUP = 0x0101;
        //private const int VK_CONTROL = 0x11;
        //private const int VK_C = 0x43;
        internal const int VK_LCONTROL = 0xA2;
        internal const int VK_LSHIFT = 0xA0; //	Linke UMSCHALTTASTE
        //internal const int VK_CONTROL = 0x11; // STRG-Taste
        internal const int VK_LWIN = 0x5B;
        //private const int VK_XBUTTON1 = 0x05;
        private const int XBUTTON1 = 0x0001;
        private const int WM_XBUTTONDOWN = 0x20B;
        private const int WM_XBUTTONUP = 0x020C;
        private const int HC_ACTION = 0;
        //private const int GW_HWNDPREV = 3;
        internal const int WM_NCACTIVATE = 0x0086;

        private const int WH_KEYBOARD_LL = 13;
        private const int WH_MOUSE_LL = 14;
        private static IntPtr _hookIDKeyboard = IntPtr.Zero;
        private static IntPtr _hookIDMouse = IntPtr.Zero;
        internal const int WM_USER = 0x400;
        internal const uint KEYEVENTF_KEYDOWN = 0x0000;
        internal const uint KEYEVENTF_KEYUP = 0x0002;
        internal const uint KEYEVENTF_EXTENDEDKEY = 0x0001;

        internal const uint EVENT_SYSTEM_FOREGROUND = 3;
        internal const uint WINEVENT_OUTOFCONTEXT = 0;
        internal const int WINEVENT_SKIPOWNPROCESS = 2;

        internal const int WM_HOTKEY = 0x312;
        internal const int HOTKEY_ID = 0x0312; // 0; // 42;


        internal delegate bool EnumWindowsCallback(IntPtr hWnd, IntPtr lParam); // stimmt mit 2x IntPtr
        internal static ArrayList windowHandles = [];

        internal static readonly List<IntPtr> hwndList = new(3);

        internal enum Modifiers : uint { Alt = 0x0001, Control = 0x0002, Shift = 0x0004, Win = 0x0008 }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool AddClipboardFormatListener(IntPtr hwnd);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

        [DllImport("user32.dll")]
        internal extern static int EnumWindows(EnumWindowsCallback hwnd, IntPtr lParam);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetLastActivePopup(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern IntPtr GetWindow(IntPtr hWnd, int flags);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")] // Win32 does not support GetWindowLongPtr directly. Problem wird durch Abfrage umgangen (s.u.)
        internal static extern int GetWindowLongPtr64(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("DwmApi.dll")]
        internal static extern int DwmGetWindowAttribute(IntPtr hwnd, int dwAttributeToGet, out int pvAttributeValue, int cbAttribute);

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        //internal static string GetWindowProcessName(IntPtr handle)
        //{
        //    GetWindowThreadProcessId(handle, out int processId);
        //    Process p = Process.GetProcessById(processId);
        //    return p.ProcessName;
        //}

        internal static event KeyEventHandler KeyDown;
        //public static event KeyEventHandler KeyUp;

        internal static string GetWindowClass(IntPtr handle)
        {
            const int nChars = 256;
            StringBuilder Buff = new(nChars);
            if (GetClassName(handle, Buff, nChars) > 0) { return Buff.ToString(); }
            return null;
        }

        internal static string GetWindowTitle(IntPtr handle)
        {
            const int nChars = 256;
            StringBuilder Buff = new(nChars);
            if (GetWindowText(handle, Buff, nChars) > 0) { return Buff.ToString(); }
            return null;
        }

        //internal static bool IsWindowTopmost(IntPtr hWnd)
        //{
        //    int es = GetWindowLongPtr64(hWnd, GWL_EXSTYLE);
        //    return (es & WS_EX_TOPMOST) == WS_EX_TOPMOST;
        //}

        internal static bool ActivateForegroundWindow(IntPtr exclHWnd)
        {
            IntPtr activeAppWindow = GetForegroundWindow();
            GetWindowThreadProcessId(activeAppWindow, out int processId1); // bei Rechtsklick auf TrayIcon
            GetWindowThreadProcessId(exclHWnd, out int processId2);        // ClipAction Strg+Win+C
            if (activeAppWindow.Equals(IntPtr.Zero) || processId1.Equals(processId2) || GetWindowClass(activeAppWindow).Equals("Shell_TrayWnd")) { activeAppWindow = GetLastWinHandle(exclHWnd); }
            if (activeAppWindow != IntPtr.Zero)
            {
                IntPtr prevAppWindow = GetLastActivePopup(activeAppWindow);
                activeAppWindow = IsWindowVisible(prevAppWindow) ? prevAppWindow : activeAppWindow;
                bool foo = false;
                Task.Run(() => { foo = SetForegroundWindow(activeAppWindow); }).Wait(); // .Net 4.5 or later.
                if (foo) { Thread.Sleep(20); return true; }
                else { return ActivateForegroundWindow(activeAppWindow); }
            }
            return false;
        }

        internal static IntPtr GetLastWinHandle(IntPtr exclHwnd)
        {
            IntPtr ownerHwnd = IntPtr.Zero;
            IntPtr foundHwnd = IntPtr.Zero;
            windowHandles.Clear();
            EnumWindows(new EnumWindowsCallback(EnumCallback), IntPtr.Zero);
            foreach (IntPtr indexHwnd in windowHandles)
            {
                if (IsWindowVisible(indexHwnd) && indexHwnd != exclHwnd)
                {
                    ownerHwnd = indexHwnd;
                    do
                    {
                        ownerHwnd = GetWindow(ownerHwnd, GW_OWNER);
                    } while (!IntPtr.Zero.Equals(GetWindow(ownerHwnd, GW_OWNER)));
                    ownerHwnd = ownerHwnd != IntPtr.Zero ? ownerHwnd : indexHwnd;
                    if (GetLastActivePopup(ownerHwnd) == indexHwnd)
                    {
                        int es = GetWindowLongPtr64(indexHwnd, GWL_EXSTYLE);
                        if ((!(((es & WS_EX_TOOLWINDOW) == WS_EX_TOOLWINDOW) && ((es & WS_EX_APPWINDOW) != WS_EX_APPWINDOW))) && !IsInvisibleWin10BackgroundAppWindow(indexHwnd))
                        { // WS_EX_APPWINDOW Forces a top-level window onto the taskbar when the window is visible.
                            foundHwnd = indexHwnd;
                            break;
                        }
                    }
                }
            }
            return foundHwnd;
        }

        private static bool EnumCallback(IntPtr hWnd, IntPtr lParam)
        {
            windowHandles.Add(hWnd);
            return true;
        }

        private static bool IsInvisibleWin10BackgroundAppWindow(IntPtr hWindow)
        {
            int hr = DwmGetWindowAttribute(hWindow, DWMWA_CLOAKED, out int cloakedVal, sizeof(int));
            if (hr != 0) { cloakedVal = 0; } // returns S_OK (which is zero) on success. Otherwise, it returns an HRESULT error code
            return cloakedVal != 0;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            internal uint vkCode;
            internal uint scanCode;
            internal uint flags;
            internal uint time;
            internal UIntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        //[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        //private static extern bool IsWindow(IntPtr hWnd);

        //[DllImport("user32.dll")]
        //private static extern IntPtr GetTopWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern short GetKeyState(int nVirtKey);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool GetAsyncKeyState(int nVirtKey);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool UnhookWindowsHookEx(IntPtr idHook);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        private delegate int LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        internal static nint RegisterAltTabRWin() { return _hookIDKeyboard = SetWindowsHookEx(WH_KEYBOARD_LL, LowLevelKeyboardHookProc, IntPtr.Zero, 0); }
        internal static void UnregisterAltTabRWin() { UnhookWindowsHookEx(_hookIDKeyboard); }
        internal static nint RegisterAltTabXBtn() { return _hookIDMouse = SetWindowsHookEx(WH_MOUSE_LL, LowLevelMouseHookProc, IntPtr.Zero, 0); }
        internal static void UnregisterAltTabXBtn() { UnhookWindowsHookEx(_hookIDMouse); }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Unicode)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        internal static void SendKeysCopy()
        {
            SendKeyDown(KeyCode.VK_RCONTROL);
            //SendKeyDown(KeyCode.VK_INSERT);
            SendKeyDown(KeyCode.KEY_C);
            Thread.Sleep(70);
            //SendKeyUp(KeyCode.VK_INSERT);
            SendKeyUp(KeyCode.KEY_C);
            SendKeyUp(KeyCode.VK_RCONTROL);
        }

        internal static void SendKeysPaste()
        {
            SendKeyDown(KeyCode.VK_CONTROL);
            SendKeyDown(KeyCode.KEY_V);
            //SendKeyDown(KeyCode.VK_SHIFT);
            //SendKeyDown(KeyCode.VK_INSERT);
            Thread.Sleep(70);
            //SendKeyUp(KeyCode.VK_INSERT);
            //SendKeyUp(KeyCode.VK_SHIFT);
            SendKeyUp(KeyCode.KEY_V);
            SendKeyUp(KeyCode.VK_CONTROL);
            //Thread.Sleep(50);
        }

        internal static void SendKeysAltTab()
        {
            SendKeyDown(KeyCode.VK_LMENU);
            SendKeyDown(KeyCode.VK_TAB);
            //Thread.Sleep(10);  // nicht nötig und nicht erwünscht (führt zur Anzeige der AltTab-Seite)
            SendKeyUp(KeyCode.VK_TAB);
            SendKeyUp(KeyCode.VK_LMENU);
        }

        private static int LowLevelKeyboardHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == WM_KEYDOWN) // || wParam == WM_SYSKEYUP)) WM_SYSKEYUP is necessary to trap Alt-key combinations
            {
                KBDLLHOOKSTRUCT kbStruct = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                Keys keys = (Keys)kbStruct.vkCode;
                if (keys == Keys.V && (GetKeyState(VK_LCONTROL) & 0x8000) != 0 && (GetKeyState(VK_LWIN) & 0x8000) != 0)
                {
                    SendKeyUp(KeyCode.KEY_V);  // hier nicht unbeding nötig
                    SendKeyUp(KeyCode.VK_LCONTROL);  // hier nicht unbeding nötig
                    SendKeyUp(KeyCode.VK_LWIN);  // hier nicht unbeding nötig
                    KeyDown(Application.OpenForms["FrmClipMenu"], new KeyEventArgs(keys));
                    return 1;
                }
                else if (keys == Keys.C && (GetKeyState(VK_LCONTROL) & 0x8000) != 0 && (GetKeyState(VK_LWIN) & 0x8000) != 0)
                {
                    SendKeyUp(KeyCode.KEY_C);
                    SendKeyUp(KeyCode.VK_LCONTROL);
                    SendKeyUp(KeyCode.VK_LWIN);
                    KeyDown(Application.OpenForms["FrmClipMenu"], new KeyEventArgs(keys)); //PostMessage(Application.OpenForms["FrmClipMenu"].Handle, WM_USER + 1, IntPtr.Zero, IntPtr.Zero);
                    return 1;
                }
                else if (keys == Keys.R && (GetKeyState(VK_LCONTROL) & 0x8000) != 0 && (GetKeyState(VK_LWIN) & 0x8000) != 0)
                {
                    KeyDown(Application.OpenForms["FrmClipMenu"], new KeyEventArgs(keys));
                    return 1;
                }
                else if (keys == Keys.Insert && (GetKeyState(VK_LCONTROL) & 0x8000) != 0 && (GetKeyState(VK_LWIN) & 0x8000) != 0)
                { //FrmClipEdit f = Application.OpenForms["FrmClipEdit"] as FrmClipEdit;
                    KeyDown(Application.OpenForms["FrmClipMenu"], new KeyEventArgs(keys));
                    return 1;
                }
                else if (keys == Keys.RWin && (Application.OpenForms?["FrmClipMenu"] as FrmClipMenu).AltTabRWin) // ShiftAltTab ergibt sich von selbst, wenn User Shift gleichzeitig drückt
                {
                    KeyDown(Application.OpenForms["FrmClipMenu"], new KeyEventArgs(keys));
                    return 1;
                }
            }
            else if (nCode >= 0 && wParam == WM_KEYDOWN)
            {
                KBDLLHOOKSTRUCT kbStruct = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                Keys keys = (Keys)kbStruct.vkCode;
                if (keys == Keys.RWin && (Application.OpenForms?["FrmClipMenu"] as FrmClipMenu).AltTabRWin) { return 1; }
                else if (keys == Keys.R && (GetKeyState(VK_LCONTROL) & 0x8000) != 0 && (GetKeyState(VK_LWIN) & 0x8000) != 0) { return 1; }
                else if (keys == Keys.Insert && (GetKeyState(VK_LCONTROL) & 0x8000) != 0 && (GetKeyState(VK_LWIN) & 0x8000) != 0) { return 1; }
            }
            return CallNextHookEx(_hookIDKeyboard, nCode, wParam, lParam);
        }

        private static int LowLevelMouseHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode == HC_ACTION && wParam == WM_XBUTTONDOWN) // if (nCode >= 0)
            {
                MSLLHOOKSTRUCT mouseHookStructEx = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                if (mouseHookStructEx.mouseData == 131072) // 0x20000 
                {
                    SendKeyDown(KeyCode.VK_LMENU);
                    SendKeyDown(KeyCode.VK_TAB);
                    SendKeyUp(KeyCode.VK_TAB);
                    SendKeyUp(KeyCode.VK_LMENU);
                    return 1; // Den Hook-Prozess beenden, um weitere Verarbeitung zu verhindern
                }
                else if (mouseHookStructEx.mouseData == 65536) // 0x10000
                {
                    SendKeyDown(KeyCode.VK_LSHIFT);
                    SendKeyDown(KeyCode.VK_LMENU);
                    SendKeyDown(KeyCode.VK_TAB);
                    SendKeyUp(KeyCode.VK_TAB);
                    SendKeyUp(KeyCode.VK_LMENU);
                    SendKeyUp(KeyCode.VK_LSHIFT);
                    return 1; // Den Hook-Prozess beenden, um weitere Verarbeitung zu verhindern
                }
            }
            else if (nCode == HC_ACTION && wParam == WM_XBUTTONUP) { return 1; } // BrowserBack- and BrowserForward-Funktion abstellen
            return CallNextHookEx(_hookIDMouse, nCode, wParam, lParam); // Den Hook-Prozess an die nächste Instanz weiterleiten
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint numberOfInputs, INPUT[] inputs, int sizeOfInputStructure);

        internal static void SendKeyDown(KeyCode keyCode) // wird auch in ClipMenu.cs verwendet
        {
            INPUT input = new() { Type = 1 };
            input.Data.Keyboard = new KEYBDINPUT { Vk = (ushort)keyCode, Scan = 0, Flags = 0, Time = 0, ExtraInfo = IntPtr.Zero };
            INPUT[] inputs = [input];
            if (SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT))) == 0) { throw new Exception(); }
        }

        internal static void SendKeyUp(KeyCode keyCode) // wird auch in ClipMenu.cs verwendet
        {
            INPUT input = new() { Type = 1 };
            input.Data.Keyboard = new KEYBDINPUT { Vk = (ushort)keyCode, Scan = 0, Flags = 2, Time = 0, ExtraInfo = IntPtr.Zero };
            INPUT[] inputs = [input];
            if (SendInput(1, inputs, Marshal.SizeOf(typeof(INPUT))) == 0) { throw new Exception(); }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct INPUT
        {
            public uint Type;
            public MOUSEKEYBDHARDWAREINPUT Data;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct MOUSEKEYBDHARDWAREINPUT
        {
            [FieldOffset(0)]
            public HARDWAREINPUT Hardware;
            [FieldOffset(0)]
            public KEYBDINPUT Keyboard;
            [FieldOffset(0)]
            public MOUSEINPUT Mouse;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct HARDWAREINPUT
        {
            public uint Msg;
            public ushort ParamL;
            public ushort ParamH;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct KEYBDINPUT
        {
            public ushort Vk;
            public ushort Scan;
            public uint Flags;
            public uint Time;
            public IntPtr ExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct MOUSEINPUT
        {
            public int X;
            public int Y;
            public uint MouseData;
            public uint Flags;
            public uint Time;
            public IntPtr ExtraInfo;
        }

        public enum KeyCode : ushort
        {
            KEY_C = 0x43,
            KEY_V = 0x56,
            KEY_R = 0x52,
            VK_INSERT = 0x2D,
            VK_CONTROL = 0x11,
            VK_LCONTROL = 0xA2,
            VK_RCONTROL = 0xA3,
            VK_SHIFT = 0x10,
            VK_LSHIFT = 0xA0,
            VK_RSHIFT = 0xA1,
            VK_LMENU = 0xA4,
            VK_TAB = 0x09,
            VK_LWIN = 0x5B,
            VK_RWIN = 0x5C
        }

        // Importieren der Win32-APIs
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hModWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern int UnhookWinEvent(IntPtr hWinEventHook);

        // Delegatentyp für den WinEventProc-Callback
        public delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        internal static void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            if (hwnd != IntPtr.Zero && idObject == 0x00000000 && idChild == 0 && eventType == EVENT_SYSTEM_FOREGROUND && IsWindowVisible(hwnd))
            {
                IntPtr owner = hwnd;
                do { owner = GetWindow(owner, GW_OWNER); }
                while (!IntPtr.Zero.Equals(GetWindow(owner, GW_OWNER)));
                owner = owner != IntPtr.Zero ? owner : hwnd;
                IntPtr last = GetLastActivePopup(owner);
                if (last != IntPtr.Zero && IsWindowVisible(last) || last == hwnd)
                {
                    hwnd = last;
                    int es = GetWindowLongPtr64(hwnd, GWL_EXSTYLE);
                    if (!((es & WS_OVERLAPPEDWINDOW) != WS_OVERLAPPEDWINDOW)) //  !(es & WS_EX_Topmost)
                    {
                        if (hwndList.Count == 0 || !hwndList[0].Equals(hwnd)) { hwndList.Insert(0, hwnd); } //Clipboard.SetText(GetWindowTitle(hwnd) + (hwndList.Count > 1 ? Environment.NewLine + GetWindowTitle(hwndList[1]) : string.Empty));
                    }
                }
            }
        }

    }
}
