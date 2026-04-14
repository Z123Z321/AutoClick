using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AutoClicker
{
    /// <summary>
    /// 全局热键辅助类 - 使用Windows API实现全局快捷键
    /// </summary>
    public class HotkeyHelper : IDisposable
    {
        #region Windows API

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public const int WM_HOTKEY = 0x0312;

        // 修饰键
        public const uint MOD_ALT = 0x0001;
        public const uint MOD_CONTROL = 0x0002;
        public const uint MOD_SHIFT = 0x0004;
        public const uint MOD_WIN = 0x0008;
        public const uint MOD_NOREPEAT = 0x4000;

        // 虚拟键码
        public const uint VK_F5 = 0x70;
        public const uint VK_F6 = 0x71;
        public const uint VK_F7 = 0x72;
        public const uint VK_F8 = 0x73;
        public const uint VK_F9 = 0x78;
        public const uint VK_F10 = 0x79;
        public const uint VK_F11 = 0x7A;
        public const uint VK_F12 = 0x7B;

        #endregion

        #region 字段

        private IntPtr _hWnd;
        private int _currentId = 0;
        private bool _isRegistered = false;
        private int _hotkeyId = 9000;

        public event EventHandler<int> HotkeyPressed;

        #endregion

        #region 属性

        public static int WM_HOTKEY_MSG => WM_HOTKEY;

        #endregion

        #region 构造函数

        public HotkeyHelper(IntPtr hWnd)
        {
            _hWnd = hWnd;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 注册全局热键
        /// </summary>
        /// <param name="modifiers">修饰键组合 (MOD_CONTROL | MOD_SHIFT | MOD_ALT)</param>
        /// <param name="key">虚拟键码</param>
        /// <returns>是否注册成功</returns>
        public bool Register(uint modifiers, uint key)
        {
            Unregister();

            bool result = RegisterHotKey(_hWnd, _hotkeyId, modifiers | MOD_NOREPEAT, key);
            if (result)
            {
                _currentId = _hotkeyId;
                _isRegistered = true;
            }
            return result;
        }

        /// <summary>
        /// 注销热键
        /// </summary>
        public void Unregister()
        {
            if (_isRegistered && _currentId != 0)
            {
                UnregisterHotKey(_hWnd, _currentId);
                _currentId = 0;
                _isRegistered = false;
            }
        }

        /// <summary>
        /// 处理热键消息
        /// </summary>
        public void ProcessMessage(int id)
        {
            if (id == _hotkeyId)
            {
                HotkeyPressed?.Invoke(this, id);
            }
        }

        #endregion

        #region IDisposable

        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Unregister();
                }
                _disposed = true;
            }
        }

        ~HotkeyHelper()
        {
            Dispose(false);
        }

        #endregion
    }
}
