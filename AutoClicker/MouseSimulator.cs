using System;
using System.Runtime.InteropServices;

namespace AutoClicker
{
    /// <summary>
    /// 鼠标模拟类 - 使用Windows API实现鼠标点击和键盘输入
    /// </summary>
    public static class MouseSimulator
    {
        #region Windows API - 鼠标

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        // 鼠标事件标志
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        private const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const uint MOUSEEVENTF_MIDDLEUP = 0x0040;

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
        }

        #endregion

        #region Windows API - 键盘

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        private const uint KEYEVENTF_KEYDOWN = 0x0000;
        private const uint KEYEVENTF_KEYUP = 0x0002;
        private const uint KEYEVENTF_EXTENDEDKEY = 0x0001;

        // 虚拟键码
        public const byte VK_LBUTTON = 0x01;
        public const byte VK_RBUTTON = 0x02;
        public const byte VK_CANCEL = 0x03;
        public const byte VK_MBUTTON = 0x04;
        public const byte VK_BACK = 0x08;
        public const byte VK_TAB = 0x09;
        public const byte VK_SHIFT = 0x10;
        public const byte VK_CONTROL = 0x11;
        public const byte VK_MENU = 0x12; // ALT
        public const byte VK_PAUSE = 0x13;
        public const byte VK_CAPITAL = 0x14;
        public const byte VK_ESCAPE = 0x1B;
        public const byte VK_SPACE = 0x20;
        public const byte VK_PRIOR = 0x21;
        public const byte VK_NEXT = 0x22;
        public const byte VK_END = 0x23;
        public const byte VK_HOME = 0x24;
        public const byte VK_LEFT = 0x25;
        public const byte VK_UP = 0x26;
        public const byte VK_RIGHT = 0x27;
        public const byte VK_DOWN = 0x28;
        public const byte VK_PRINT = 0x2A;
        public const byte VK_EXECUTE = 0x2B;
        public const byte VK_SNAPSHOT = 0x2C;
        public const byte VK_INSERT = 0x2D;
        public const byte VK_DELETE = 0x2E;
        public const byte VK_HELP = 0x2F;
        public const byte VK_LWIN = 0x5B;
        public const byte VK_RWIN = 0x5C;
        public const byte VK_APPS = 0x5D;
        public const byte VK_SLEEP = 0x5F;
        public const byte VK_NUMPAD0 = 0x60;
        public const byte VK_NUMPAD1 = 0x61;
        public const byte VK_NUMPAD2 = 0x62;
        public const byte VK_NUMPAD3 = 0x63;
        public const byte VK_NUMPAD4 = 0x64;
        public const byte VK_NUMPAD5 = 0x65;
        public const byte VK_NUMPAD6 = 0x66;
        public const byte VK_NUMPAD7 = 0x67;
        public const byte VK_NUMPAD8 = 0x68;
        public const byte VK_NUMPAD9 = 0x69;
        public const byte VK_MULTIPLY = 0x6A;
        public const byte VK_ADD = 0x6B;
        public const byte VK_SEPARATOR = 0x6C;
        public const byte VK_SUBTRACT = 0x6D;
        public const byte VK_DECIMAL = 0x6E;
        public const byte VK_DIVIDE = 0x6F;
        public const byte VK_F1 = 0x70;
        public const byte VK_F2 = 0x71;
        public const byte VK_F3 = 0x72;
        public const byte VK_F4 = 0x73;
        public const byte VK_F5 = 0x74;
        public const byte VK_F6 = 0x75;
        public const byte VK_F7 = 0x76;
        public const byte VK_F8 = 0x77;
        public const byte VK_F9 = 0x78;
        public const byte VK_F10 = 0x79;
        public const byte VK_F11 = 0x7A;
        public const byte VK_F12 = 0x7B;

        #endregion

        #region 枚举

        /// <summary>
        /// 鼠标按钮类型
        /// </summary>
        public enum MouseButton
        {
            Left,
            Right,
            Middle
        }

        /// <summary>
        /// 操作类型
        /// </summary>
        public enum ActionType
        {
            MouseLeft,      // 鼠标左键
            MouseRight,     // 鼠标右键
            MouseMiddle,    // 鼠标中键
            CtrlLeft,      // Ctrl+左键
            ShiftLeft,     // Shift+左键
            AltLeft,       // Alt+左键
            CtrlRight,     // Ctrl+右键
            ShiftRight,    // Shift+右键
            AltRight,      // Alt+右键
            CtrlMiddle,    // Ctrl+中键
            ShiftMiddle,   // Shift+中键
            AltMiddle,     // Alt+中键
            KeyPress       // 纯键盘按键
        }

        #endregion

        #region 公开方法

        /// <summary>
        /// 执行单次点击
        /// </summary>
        public static void Click(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    LeftClick();
                    break;
                case MouseButton.Right:
                    RightClick();
                    break;
                case MouseButton.Middle:
                    MiddleClick();
                    break;
            }
        }

        /// <summary>
        /// 移动到指定位置并点击
        /// </summary>
        public static void MoveAndClick(int x, int y, MouseButton button)
        {
            SetCursorPos(x, y);
            System.Threading.Thread.Sleep(10);
            Click(button);
        }

        /// <summary>
        /// 移动到指定位置并执行操作
        /// </summary>
        public static void MoveAndDoAction(int x, int y, ActionType actionType, byte keyCode = 0)
        {
            SetCursorPos(x, y);
            System.Threading.Thread.Sleep(10);

            switch (actionType)
            {
                case ActionType.MouseLeft:
                    LeftClick();
                    break;
                case ActionType.MouseRight:
                    RightClick();
                    break;
                case ActionType.MouseMiddle:
                    MiddleClick();
                    break;
                case ActionType.CtrlLeft:
                    CtrlLeftClick();
                    break;
                case ActionType.ShiftLeft:
                    ShiftLeftClick();
                    break;
                case ActionType.AltLeft:
                    AltLeftClick();
                    break;
                case ActionType.CtrlRight:
                    CtrlRightClick();
                    break;
                case ActionType.ShiftRight:
                    ShiftRightClick();
                    break;
                case ActionType.AltRight:
                    AltRightClick();
                    break;
                case ActionType.CtrlMiddle:
                    CtrlMiddleClick();
                    break;
                case ActionType.ShiftMiddle:
                    ShiftMiddleClick();
                    break;
                case ActionType.AltMiddle:
                    AltMiddleClick();
                    break;
                case ActionType.KeyPress:
                    if (keyCode > 0)
                        DoKeyPress(keyCode);
                    break;
            }
        }

        /// <summary>
        /// 获取当前鼠标位置
        /// </summary>
        public static System.Drawing.Point GetCursorPosition()
        {
            GetCursorPos(out POINT point);
            return new System.Drawing.Point(point.X, point.Y);
        }

        /// <summary>
        /// 获取操作类型的显示名称
        /// </summary>
        public static string GetActionTypeName(ActionType actionType)
        {
            switch (actionType)
            {
                case ActionType.MouseLeft: return "左键";
                case ActionType.MouseRight: return "右键";
                case ActionType.MouseMiddle: return "中键";
                case ActionType.CtrlLeft: return "Ctrl+左键";
                case ActionType.ShiftLeft: return "Shift+左键";
                case ActionType.AltLeft: return "Alt+左键";
                case ActionType.CtrlRight: return "Ctrl+右键";
                case ActionType.ShiftRight: return "Shift+右键";
                case ActionType.AltRight: return "Alt+右键";
                case ActionType.CtrlMiddle: return "Ctrl+中键";
                case ActionType.ShiftMiddle: return "Shift+中键";
                case ActionType.AltMiddle: return "Alt+中键";
                case ActionType.KeyPress: return "按键";
                default: return "左键";
            }
        }

        /// <summary>
        /// 执行指定的操作类型
        /// </summary>
        public static void DoAction(ActionType actionType)
        {
            switch (actionType)
            {
                case ActionType.MouseLeft:
                    LeftClick();
                    break;
                case ActionType.MouseRight:
                    RightClick();
                    break;
                case ActionType.MouseMiddle:
                    MiddleClick();
                    break;
                case ActionType.CtrlLeft:
                    CtrlLeftClick();
                    break;
                case ActionType.ShiftLeft:
                    ShiftLeftClick();
                    break;
                case ActionType.AltLeft:
                    AltLeftClick();
                    break;
                case ActionType.CtrlRight:
                    CtrlRightClick();
                    break;
                case ActionType.ShiftRight:
                    ShiftRightClick();
                    break;
                case ActionType.AltRight:
                    AltRightClick();
                    break;
                case ActionType.CtrlMiddle:
                    CtrlMiddleClick();
                    break;
                case ActionType.ShiftMiddle:
                    ShiftMiddleClick();
                    break;
                case ActionType.AltMiddle:
                    AltMiddleClick();
                    break;
            }
        }

        /// <summary>
        /// 按下一个键并释放
        /// </summary>
        public static void DoKeyPress(byte keyCode)
        {
            keybd_event(keyCode, 0, KEYEVENTF_KEYDOWN, 0);
            System.Threading.Thread.Sleep(10);
            keybd_event(keyCode, 0, KEYEVENTF_KEYUP, 0);
        }

        /// <summary>
        /// 获取常用按键的虚拟键码
        /// </summary>
        public static byte GetVirtualKeyCode(string keyName)
        {
            switch (keyName.ToUpper())
            {
                case "A": return 0x41;
                case "B": return 0x42;
                case "C": return 0x43;
                case "D": return 0x44;
                case "E": return 0x45;
                case "F": return 0x46;
                case "G": return 0x47;
                case "H": return 0x48;
                case "I": return 0x49;
                case "J": return 0x4A;
                case "K": return 0x4B;
                case "L": return 0x4C;
                case "M": return 0x4D;
                case "N": return 0x4E;
                case "O": return 0x4F;
                case "P": return 0x50;
                case "Q": return 0x51;
                case "R": return 0x52;
                case "S": return 0x53;
                case "T": return 0x54;
                case "U": return 0x55;
                case "V": return 0x56;
                case "W": return 0x57;
                case "X": return 0x58;
                case "Y": return 0x59;
                case "Z": return 0x5A;
                case "0": return 0x30;
                case "1": return 0x31;
                case "2": return 0x32;
                case "3": return 0x33;
                case "4": return 0x34;
                case "5": return 0x35;
                case "6": return 0x36;
                case "7": return 0x37;
                case "8": return 0x38;
                case "9": return 0x39;
                case "F1": return VK_F1;
                case "F2": return VK_F2;
                case "F3": return VK_F3;
                case "F4": return VK_F4;
                case "F5": return VK_F5;
                case "F6": return VK_F6;
                case "F7": return VK_F7;
                case "F8": return VK_F8;
                case "F9": return VK_F9;
                case "F10": return VK_F10;
                case "F11": return VK_F11;
                case "F12": return VK_F12;
                case "SPACE": return VK_SPACE;
                case "ENTER": return 0x0D;
                case "TAB": return VK_TAB;
                case "ESCAPE": return VK_ESCAPE;
                case "ESC": return VK_ESCAPE;
                case "SHIFT": return VK_SHIFT;
                case "CTRL": return VK_CONTROL;
                case "CONTROL": return VK_CONTROL;
                case "ALT": return VK_MENU;
                default: return 0;
            }
        }

        /// <summary>
        /// 根据名称获取ActionType
        /// </summary>
        public static ActionType GetActionTypeByName(string name)
        {
            switch (name)
            {
                case "左键": return ActionType.MouseLeft;
                case "右键": return ActionType.MouseRight;
                case "中键": return ActionType.MouseMiddle;
                case "Ctrl+左键": return ActionType.CtrlLeft;
                case "Shift+左键": return ActionType.ShiftLeft;
                case "Alt+左键": return ActionType.AltLeft;
                case "Ctrl+右键": return ActionType.CtrlRight;
                case "Shift+右键": return ActionType.ShiftRight;
                case "Alt+右键": return ActionType.AltRight;
                case "Ctrl+中键": return ActionType.CtrlMiddle;
                case "Shift+中键": return ActionType.ShiftMiddle;
                case "Alt+中键": return ActionType.AltMiddle;
                case "按键": return ActionType.KeyPress;
                default: return ActionType.MouseLeft;
            }
        }

        #endregion

        #region 私有方法

        private static void LeftClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(10);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        private static void RightClick()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(10);
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }

        private static void MiddleClick()
        {
            mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(10);
            mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
        }

        private static void CtrlLeftClick()
        {
            // 按下Ctrl
            keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYDOWN, 0);
            System.Threading.Thread.Sleep(10);
            // 按下左键
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(10);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(10);
            // 释放Ctrl
            keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYUP, 0);
        }

        private static void ShiftLeftClick()
        {
            // 按下Shift
            keybd_event(VK_SHIFT, 0, KEYEVENTF_KEYDOWN, 0);
            System.Threading.Thread.Sleep(10);
            // 按下左键
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(10);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(10);
            // 释放Shift
            keybd_event(VK_SHIFT, 0, KEYEVENTF_KEYUP, 0);
        }

        private static void AltLeftClick()
        {
            // 按下Alt
            keybd_event(VK_MENU, 0, KEYEVENTF_KEYDOWN, 0);
            System.Threading.Thread.Sleep(10);
            // 按下左键
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(10);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(10);
            // 释放Alt
            keybd_event(VK_MENU, 0, KEYEVENTF_KEYUP, 0);
        }

        private static void CtrlRightClick()
        {
            keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYDOWN, 0);
            System.Threading.Thread.Sleep(10);
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(10);
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(10);
            keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYUP, 0);
        }

        private static void ShiftRightClick()
        {
            keybd_event(VK_SHIFT, 0, KEYEVENTF_KEYDOWN, 0);
            System.Threading.Thread.Sleep(10);
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(10);
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(10);
            keybd_event(VK_SHIFT, 0, KEYEVENTF_KEYUP, 0);
        }

        private static void AltRightClick()
        {
            keybd_event(VK_MENU, 0, KEYEVENTF_KEYDOWN, 0);
            System.Threading.Thread.Sleep(10);
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(10);
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(10);
            keybd_event(VK_MENU, 0, KEYEVENTF_KEYUP, 0);
        }

        private static void CtrlMiddleClick()
        {
            keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYDOWN, 0);
            System.Threading.Thread.Sleep(10);
            mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(10);
            mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(10);
            keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYUP, 0);
        }

        private static void ShiftMiddleClick()
        {
            keybd_event(VK_SHIFT, 0, KEYEVENTF_KEYDOWN, 0);
            System.Threading.Thread.Sleep(10);
            mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(10);
            mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(10);
            keybd_event(VK_SHIFT, 0, KEYEVENTF_KEYUP, 0);
        }

        private static void AltMiddleClick()
        {
            keybd_event(VK_MENU, 0, KEYEVENTF_KEYDOWN, 0);
            System.Threading.Thread.Sleep(10);
            mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(10);
            mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(10);
            keybd_event(VK_MENU, 0, KEYEVENTF_KEYUP, 0);
        }

        #endregion
    }
}
