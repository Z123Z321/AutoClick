using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace AutoClicker
{
    public partial class MainForm : Form
    {
        #region Windows API

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        private const int WH_MOUSE_LL = 14;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_RBUTTONDOWN = 0x0204;
        private const int WM_MBUTTONDOWN = 0x0207;

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public int mouseData;
            public int flags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        #endregion

        #region 数据结构

        /// <summary>
        /// 点击点信息
        /// </summary>
        public class ClickPoint
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Interval { get; set; } = 0; // 点击前等待时间
            public int ClickLimit { get; set; } = 0; // 该点位的点击次数限制，0表示无限
            public int CurrentClickCount { get; set; } = 0; // 当前已点击次数（运行时使用）
            public MouseSimulator.MouseButton Button { get; set; } = MouseSimulator.MouseButton.Left;
            public MouseSimulator.ActionType ActionType { get; set; } = MouseSimulator.ActionType.MouseLeft; // 操作类型
            public byte KeyCode { get; set; } = 0; // 键盘按键码（用于KeyPress类型）
            public string KeyName { get; set; } = ""; // 按键名称显示
            public string Name { get; set; } = "";

            public ClickPoint() { }

            public ClickPoint(int x, int y, string name = "")
            {
                X = x;
                Y = y;
                Name = name;
                ActionType = MouseSimulator.ActionType.MouseLeft;
            }

            /// <summary>
            /// 获取操作的显示名称
            /// </summary>
            public string GetActionDisplayName()
            {
                if (ActionType == MouseSimulator.ActionType.KeyPress && !string.IsNullOrEmpty(KeyName))
                {
                    return KeyName;
                }
                return MouseSimulator.GetActionTypeName(ActionType);
            }
        }

        #endregion

        #region 字段

        private bool _isRunning = false;
        private Thread _clickThread;
        private HotkeyHelper _hotkeyHelper;
        private int _clickCount = 0;
        private int _currentPointIndex = 0;
        private int _cycleCount = 0; // 当前循环次数（多点位模式使用）

        // 多点位相关
        private List<ClickPoint> _clickPoints = new List<ClickPoint>();
        // 多点位模式始终启用，移除了单点模式

        // 全局鼠标钩子相关
        private IntPtr _mouseHookId = IntPtr.Zero;
        private LowLevelMouseProc _mouseProc;
        private ManualResetEvent _recordingEvent;
        private Point _recordedPoint;
        private bool _isRecording = false;

        // 鼠标移动模拟相关
        private Random _random = new Random();
        private bool _useMousePath = true; // 默认启用鼠标路径模拟

        #endregion

        #region 构造函数

        public MainForm()
        {
            InitializeComponent();
            
            // 确保多点位组始终可用
            grpMultiplePoints.Enabled = true;
            
            InitializeHotkey();
            LoadSettings();
            UpdateStatus();
            UpdatePointsList();
        }

        #endregion

        #region 鼠标路径模拟

        /// <summary>
        /// 使用贝塞尔曲线生成平滑的鼠标移动路径
        /// </summary>
        private List<Point> GenerateMousePath(Point start, Point end, int steps = 20)
        {
            List<Point> path = new List<Point>();

            // 添加随机偏移量，使轨迹更自然
            int offsetX = _random.Next(-50, 51);
            int offsetY = _random.Next(-50, 51);

            // 控制点偏移
            Point control1 = new Point(
                start.X + (end.X - start.X) / 3 + offsetX,
                start.Y + (end.Y - start.Y) / 3 + offsetY
            );
            Point control2 = new Point(
                start.X + 2 * (end.X - start.X) / 3 - offsetX,
                start.Y + 2 * (end.Y - start.Y) / 3 - offsetY
            );

            for (int i = 0; i <= steps; i++)
            {
                double t = (double)i / steps;
                double x = Math.Pow(1 - t, 3) * start.X +
                           3 * Math.Pow(1 - t, 2) * t * control1.X +
                           3 * (1 - t) * Math.Pow(t, 2) * control2.X +
                           Math.Pow(t, 3) * end.X;
                double y = Math.Pow(1 - t, 3) * start.Y +
                           3 * Math.Pow(1 - t, 2) * t * control1.Y +
                           3 * (1 - t) * Math.Pow(t, 2) * control2.Y +
                           Math.Pow(t, 3) * end.Y;

                path.Add(new Point((int)x, (int)y));
            }

            return path;
        }

        /// <summary>
        /// 模拟鼠标沿路径移动
        /// </summary>
        private void MoveMouseAlongPath(List<Point> path)
        {
            foreach (var point in path)
            {
                SetCursorPos(point.X, point.Y);
                // 添加微小延迟，模拟真实鼠标移动
                Thread.Sleep(_random.Next(5, 15));
            }
        }

        /// <summary>
        /// 移动到目标位置（可选是否使用路径）
        /// </summary>
        private void MoveToPosition(int x, int y)
        {
            if (_useMousePath)
            {
                GetCursorPos(out POINT currentPos);
                var path = GenerateMousePath(new Point(currentPos.X, currentPos.Y), new Point(x, y));
                MoveMouseAlongPath(path);
            }
            else
            {
                SetCursorPos(x, y);
                Thread.Sleep(10);
            }
        }

        #endregion

        #region 全局鼠标录制

        private IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && _isRecording)
            {
                MSLLHOOKSTRUCT hookStruct = Marshal.PtrToStructure<MSLLHOOKSTRUCT>(lParam);
                int msg = wParam.ToInt32();

                if (msg == WM_LBUTTONDOWN || msg == WM_RBUTTONDOWN || msg == WM_MBUTTONDOWN)
                {
                    GetCursorPos(out POINT cursorPos);
                    _recordedPoint = new Point(cursorPos.X, cursorPos.Y);
                    _recordingEvent.Set();
                    return (IntPtr)1;
                }
            }
            return CallNextHookEx(_mouseHookId, nCode, wParam, lParam);
        }

        private void StartGlobalMouseHook()
        {
            _mouseProc = MouseHookCallback;
            using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                _mouseHookId = SetWindowsHookEx(WH_MOUSE_LL, _mouseProc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private void StopGlobalMouseHook()
        {
            if (_mouseHookId != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_mouseHookId);
                _mouseHookId = IntPtr.Zero;
            }
        }

        #endregion

        #region 热键初始化

        private void InitializeHotkey()
        {
            _hotkeyHelper = new HotkeyHelper(this.Handle);
            _hotkeyHelper.HotkeyPressed += (s, id) =>
            {
                this.Invoke(new Action(() =>
                {
                    if (_isRunning)
                        StopClicking();
                    else
                        StartClicking();
                }));
            };

            RegisterHotkey();
        }

        private void RegisterHotkey()
        {
            _hotkeyHelper.Unregister();

            uint modifiers = 0;
            if (chkCtrl.Checked) modifiers |= HotkeyHelper.MOD_CONTROL;
            if (chkAlt.Checked) modifiers |= HotkeyHelper.MOD_ALT;
            if (chkShift.Checked) modifiers |= HotkeyHelper.MOD_SHIFT;

            if (modifiers == 0)
            {
                MessageBox.Show("请至少选择一个修饰键(Ctrl/Alt/Shift)", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            uint key = GetSelectedKey();
            if (key == 0)
            {
                MessageBox.Show("请选择一个快捷键", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!_hotkeyHelper.Register(modifiers, key))
            {
                MessageBox.Show("热键注册失败，可能已被其他程序占用！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                lblHotkeyStatus.Text = "热键已注册";
                lblHotkeyStatus.ForeColor = Color.Green;
            }
        }

        private uint GetSelectedKey()
        {
            if (rdoF9.Checked) return HotkeyHelper.VK_F9;
            if (rdoF10.Checked) return HotkeyHelper.VK_F10;
            if (rdoF11.Checked) return HotkeyHelper.VK_F11;
            if (rdoF12.Checked) return HotkeyHelper.VK_F12;
            if (rdoF5.Checked) return HotkeyHelper.VK_F5;
            if (rdoF6.Checked) return HotkeyHelper.VK_F6;
            if (rdoF7.Checked) return HotkeyHelper.VK_F7;
            if (rdoF8.Checked) return HotkeyHelper.VK_F8;
            return 0;
        }

        #endregion

        #region 点击核心逻辑

        private void StartClicking()
        {
            if (_isRunning) return;

            // 检查是否有点位
            if (_clickPoints.Count == 0)
            {
                MessageBox.Show("请先添加至少一个点位！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _isRunning = true;
            _clickCount = 0;
            _currentPointIndex = 0;
            _cycleCount = 0;
            
            // 初始化每个点位的当前点击次数
            foreach (var point in _clickPoints)
            {
                point.CurrentClickCount = 0;
            }
            
            UpdateStatus();

            _clickThread = new Thread(() =>
            {
                while (_isRunning)
                {
                    try
                    {
                        if (_clickPoints.Count > 0)
                        {
                            // 多点位模式：每个点位各点击1次，然后进入下一次循环
                            int totalCycles = GetClickCount(); // 总循环次数
                            
                            // 检查是否完成所有点位的点击
                            if (_currentPointIndex >= _clickPoints.Count)
                            {
                                // 一个循环完成，重置到第一个点位
                                _cycleCount++;
                                _currentPointIndex = 0;
                                
                                // 检查是否达到循环次数限制
                                if (totalCycles > 0 && _cycleCount >= totalCycles)
                                {
                                    break; // 达到循环次数限制，停止
                                }
                                
                                // 继续下一个循环
                                continue;
                            }
                            
                            ClickPoint point = _clickPoints[_currentPointIndex];
                            
                            // 移动到位置
                            MoveToPosition(point.X, point.Y);
                            
                            // 等待间隔
                            if (point.Interval > 0)
                            {
                                Thread.Sleep(point.Interval);
                            }
                            
                            // 执行操作（根据ActionType）
                            if (point.ActionType == MouseSimulator.ActionType.KeyPress)
                            {
                                // 纯键盘按键
                                MouseSimulator.DoKeyPress(point.KeyCode);
                            }
                            else
                            {
                                // 鼠标点击或组合键
                                MouseSimulator.DoAction(point.ActionType);
                            }
                            Interlocked.Increment(ref _clickCount);
                            point.CurrentClickCount++;
                            UpdateClickCount();
                            
                            // 移动到下一个点位
                            _currentPointIndex++;
                            
                            // 主间隔
                            Thread.Sleep(GetInterval());
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new Action(() =>
                        {
                            MessageBox.Show("执行出错: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }));
                        break;
                    }
                }
                
                // 点击完成，停止
                this.Invoke(new Action(() => StopClicking()));
            });

            _clickThread.IsBackground = true;
            _clickThread.Start();

            btnStartStop.Text = "停止(F9)";
            btnStartStop.BackColor = Color.FromArgb(220, 53, 69);
        }

        private void StopClicking()
        {
            _isRunning = false;

            if (_clickThread != null && _clickThread.IsAlive)
            {
                _clickThread.Join(500);
            }

            UpdateStatus();
            btnStartStop.Text = "开始(F9)";
            btnStartStop.BackColor = Color.FromArgb(40, 167, 69);
        }

        // 多点位模式始终启用，移除了单点模式
        private int GetInterval()
        {
            if (int.TryParse(txtInterval.Text, out int interval))
            {
                if (interval < 1) interval = 1;
                return interval;
            }
            return 100;
        }

        private int GetClickCount()
        {
            if (int.TryParse(txtClickCount.Text, out int count))
            {
                if (count < 0) count = 0;
                return count;
            }
            return 0; // 0表示无限
        }

        private void UpdateClickCount()
        {
            if (lblClickCountValue.InvokeRequired)
            {
                lblClickCountValue.Invoke(new Action(() =>
                {
                    lblClickCountValue.Text = _clickCount.ToString();
                    CheckAndStopIfLimitReached();
                }));
            }
            else
            {
                lblClickCountValue.Text = _clickCount.ToString();
                CheckAndStopIfLimitReached();
            }
        }

        private void CheckAndStopIfLimitReached()
        {
            // 多点位模式下，循环次数限制已在循环中处理
            // 无需额外检查
        }

        #endregion

        #region 多点位管理

        private void UpdatePointsList()
        {
            lstPoints.Items.Clear();
            for (int i = 0; i < _clickPoints.Count; i++)
            {
                var p = _clickPoints[i];
                string name = string.IsNullOrEmpty(p.Name) ? $"点{i + 1}" : p.Name;
                string actionName = p.GetActionDisplayName();
                lstPoints.Items.Add($"{name}: ({p.X}, {p.Y}) [{actionName}]");
            }
            // 更新按钮状态
            UpdateMultiplePointsUIState();
        }

        private void AddPoint(int x, int y)
        {
            var point = new ClickPoint(x, y, $"点{_clickPoints.Count + 1}");
            // 默认使用左键，后续双击可修改
            point.ActionType = MouseSimulator.ActionType.MouseLeft;
            _clickPoints.Add(point);
            UpdatePointsList();
            UpdateMultiplePointsUIState();
        }

        private void btnAddPoint_Click(object sender, EventArgs e)
        {
            if (_isRecording)
            {
                _isRecording = false;
                btnAddPoint.Text = "添加点位";
                btnAddPoint.BackColor = SystemColors.Control;
                this.Cursor = Cursors.Default;
                StopGlobalMouseHook();
            }
            else
            {
                _isRecording = true;
                btnAddPoint.Text = "取消录制";
                btnAddPoint.BackColor = Color.Orange;
                this.Cursor = Cursors.Cross;

                _recordingEvent = new ManualResetEvent(false);
                StartGlobalMouseHook();

                Thread recordingThread = new Thread(() =>
                {
                    bool completed = _recordingEvent.WaitOne(30000);
                    StopGlobalMouseHook();

                    this.Invoke(new Action(() =>
                    {
                        if (completed)
                        {
                            AddPoint(_recordedPoint.X, _recordedPoint.Y);
                        }

                        _isRecording = false;
                        btnAddPoint.Text = "添加点位";
                        btnAddPoint.BackColor = SystemColors.Control;
                        this.Cursor = Cursors.Default;
                    }));
                });
                recordingThread.IsBackground = true;
                recordingThread.Start();
            }
        }

        private void btnRemovePoint_Click(object sender, EventArgs e)
        {
            if (lstPoints.SelectedIndex >= 0)
            {
                _clickPoints.RemoveAt(lstPoints.SelectedIndex);
                UpdatePointsList();
            }
        }

        private void btnClearPoints_Click(object sender, EventArgs e)
        {
            _clickPoints.Clear();
            UpdatePointsList();
        }

        private void chkMousePath_CheckedChanged(object sender, EventArgs e)
        {
            _useMousePath = chkMousePath.Checked;
        }

        private void chkMultiplePoints_CheckedChanged(object sender, EventArgs e)
        {
            // 多点位模式始终启用，此复选框保留但功能已简化
            UpdateMultiplePointsUIState();
        }

        #endregion

        #region UI更新

        private void UpdateStatus()
        {
            if (lblStatus.InvokeRequired)
            {
                lblStatus.Invoke(new Action(UpdateStatus));
                return;
            }

            if (_isRunning)
            {
                lblStatus.Text = "运行中";
                lblStatus.ForeColor = Color.Green;
                grpSettings.Enabled = false;
            }
            else
            {
                lblStatus.Text = "已停止";
                lblStatus.ForeColor = Color.Gray;
                grpSettings.Enabled = true;
                
                // 恢复多点位相关控件状态
                UpdateMultiplePointsUIState();
            }
        }

        private void LoadSettings()
        {
            txtInterval.Text = "100";
            txtClickCount.Text = "0";
            chkCtrl.Checked = true;
            rdoF9.Checked = true;
            chkMousePath.Checked = true;
            
            // 初始化多点位控件状态
            UpdateMultiplePointsUIState();
        }

        /// <summary>
        /// 更新多点位相关UI控件的状态
        /// </summary>
        private void UpdateMultiplePointsUIState()
        {
            // 复选框始终可用
            chkMultiplePoints.Enabled = true;
            chkMousePath.Enabled = true;
            
            // 添加点位按钮始终可用
            btnAddPoint.Enabled = true;
            
            // 根据点位列表是否有内容决定其他按钮状态
            bool hasPoints = _clickPoints.Count > 0;
            lstPoints.Enabled = true; // 列表始终可用，方便查看
            btnRemovePoint.Enabled = hasPoints; // 有点位时启用删除按钮
            btnClearPoints.Enabled = hasPoints; // 有点位时启用清空按钮
        }

        #endregion

        #region 事件处理

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == HotkeyHelper.WM_HOTKEY)
            {
                _hotkeyHelper.ProcessMessage(m.WParam.ToInt32());
            }
            base.WndProc(ref m);
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (_isRunning)
                StopClicking();
            else
                StartClicking();
        }



        private void btnUpdateHotkey_Click(object sender, EventArgs e)
        {
            RegisterHotkey();
        }

        private void btnResetCount_Click(object sender, EventArgs e)
        {
            _clickCount = 0;
            lblClickCountValue.Text = "0";
        }

        private void txtInterval_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtClickCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }



        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            StopClicking();
            StopGlobalMouseHook();
            _hotkeyHelper?.Dispose();
            base.OnFormClosing(e);
        }

        private void lblAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("鼠标连点器 v2.0\n\n功能特性:\n• 支持左键/右键/中键点击\n• 可设置点击间隔(毫秒)\n• 支持固定位置点击\n• 全局快捷键控制\n• 点击次数限制\n• 实时显示点击统计\n• 多点位循环点击\n• 鼠标路径模拟(防检测)\n\n作者: AutoClicker Team", "关于");
        }

        private void lstPoints_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 单击选中点位，无需特殊处理
            // 操作类型通过双击修改
        }

        private void lstPoints_DoubleClick(object sender, EventArgs e)
        {
            // 双击点位弹出操作类型选择对话框
            if (lstPoints.SelectedIndex >= 0 && lstPoints.SelectedIndex < _clickPoints.Count)
            {
                var point = _clickPoints[lstPoints.SelectedIndex];
                ShowActionTypeDialog(point);
            }
        }

        private void ShowActionTypeDialog(ClickPoint point)
        {
            // 创建对话框
            Form dialog = new Form()
            {
                Text = $"修改点位操作 - {point.Name}",
                Size = new Size(320, 320),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                AcceptButton = null
            };

            Label lblPrompt = new Label()
            {
                Text = "选择操作类型：",
                Location = new Point(15, 20),
                AutoSize = true
            };

            ComboBox cboAction = new ComboBox()
            {
                Location = new Point(15, 50),
                Size = new Size(280, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // 添加操作类型选项（与 MouseSimulator.ActionType 枚举顺序一致）
            string[] actionNames = new string[]
            {
                "左键点击",
                "右键点击",
                "中键点击",
                "Ctrl+左键",
                "Shift+左键",
                "Alt+左键",
                "Ctrl+右键",
                "Shift+右键",
                "Alt+右键",
                "Ctrl+中键",
                "Shift+中键",
                "Alt+中键",
                "按键(E, A, 1等)"
            };

            foreach (var name in actionNames)
            {
                cboAction.Items.Add(name);
            }
            cboAction.SelectedIndex = (int)point.ActionType;

            Label lblKey = new Label()
            {
                Text = "按键：",
                Location = new Point(15, 90),
                AutoSize = true,
                Visible = point.ActionType == MouseSimulator.ActionType.KeyPress
            };

            TextBox txtKey = new TextBox()
            {
                Location = new Point(60, 87),
                Size = new Size(50, 25),
                Text = point.KeyName,
                MaxLength = 1,
                Visible = point.ActionType == MouseSimulator.ActionType.KeyPress
            };

            // 根据选择显示/隐藏按键输入
            cboAction.SelectedIndexChanged += (s, args) =>
            {
                bool isKeyPress = cboAction.SelectedIndex == 12; // KeyPress 是第13个选项
                lblKey.Visible = isKeyPress;
                txtKey.Visible = isKeyPress;
            };

            Button btnOK = new Button()
            {
                Text = "确定",
                Location = new Point(110, 250),
                Size = new Size(80, 30),
                DialogResult = DialogResult.OK
            };

            Button btnCancel = new Button()
            {
                Text = "取消",
                Location = new Point(200, 250),
                Size = new Size(80, 30),
                DialogResult = DialogResult.Cancel
            };

            btnOK.Click += (s, args) =>
            {
                point.ActionType = (MouseSimulator.ActionType)cboAction.SelectedIndex;
                if (point.ActionType == MouseSimulator.ActionType.KeyPress)
                {
                    string key = txtKey.Text.ToUpper();
                    point.KeyName = string.IsNullOrEmpty(key) ? "E" : key;
                    point.KeyCode = MouseSimulator.GetVirtualKeyCode(point.KeyName);
                }
                UpdatePointsList();
                dialog.Close();
            };


            dialog.Controls.AddRange(new Control[] { lblPrompt, cboAction, lblKey, txtKey, btnOK, btnCancel });
            dialog.ShowDialog();
        }
        #endregion
    }
}
