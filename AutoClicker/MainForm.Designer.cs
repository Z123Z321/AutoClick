namespace AutoClicker
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.grpSettings = new System.Windows.Forms.GroupBox();
            this.grpHotkey = new System.Windows.Forms.GroupBox();
            this.btnUpdateHotkey = new System.Windows.Forms.Button();
            this.lblHotkeyStatus = new System.Windows.Forms.Label();
            this.chkShift = new System.Windows.Forms.CheckBox();
            this.chkAlt = new System.Windows.Forms.CheckBox();
            this.chkCtrl = new System.Windows.Forms.CheckBox();
            this.rdoF9 = new System.Windows.Forms.RadioButton();
            this.rdoF10 = new System.Windows.Forms.RadioButton();
            this.rdoF11 = new System.Windows.Forms.RadioButton();
            this.rdoF12 = new System.Windows.Forms.RadioButton();
            this.rdoF5 = new System.Windows.Forms.RadioButton();
            this.rdoF6 = new System.Windows.Forms.RadioButton();
            this.rdoF7 = new System.Windows.Forms.RadioButton();
            this.rdoF8 = new System.Windows.Forms.RadioButton();
            this.grpInterval = new System.Windows.Forms.GroupBox();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.lblMs = new System.Windows.Forms.Label();
            this.lblIntervalTip = new System.Windows.Forms.Label();
            this.grpClickCount = new System.Windows.Forms.GroupBox();
            this.txtClickCount = new System.Windows.Forms.TextBox();
            this.lblTimes = new System.Windows.Forms.Label();
            this.lblInfiniteTip = new System.Windows.Forms.Label();
            this.grpMultiplePoints = new System.Windows.Forms.GroupBox();
            this.lblPointsTip = new System.Windows.Forms.Label();
            this.chkMousePath = new System.Windows.Forms.CheckBox();
            this.btnClearPoints = new System.Windows.Forms.Button();
            this.btnRemovePoint = new System.Windows.Forms.Button();
            this.btnAddPoint = new System.Windows.Forms.Button();
            this.lstPoints = new System.Windows.Forms.ListBox();
            this.chkMultiplePoints = new System.Windows.Forms.CheckBox();
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.lblTotalClicks = new System.Windows.Forms.Label();
            this.lblClickCountValue = new System.Windows.Forms.Label();
            this.btnResetCount = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblAbout = new System.Windows.Forms.Label();
            this.grpSettings.SuspendLayout();
            this.grpHotkey.SuspendLayout();
            this.grpInterval.SuspendLayout();
            this.grpClickCount.SuspendLayout();
            this.grpMultiplePoints.SuspendLayout();
            this.grpStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSettings
            // 
            this.grpSettings.Controls.Add(this.grpHotkey);
            this.grpSettings.Controls.Add(this.grpInterval);
            this.grpSettings.Controls.Add(this.grpClickCount);
            this.grpSettings.Controls.Add(this.grpMultiplePoints);
            this.grpSettings.Location = new System.Drawing.Point(18, 70);
            this.grpSettings.Margin = new System.Windows.Forms.Padding(4);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Padding = new System.Windows.Forms.Padding(4);
            this.grpSettings.Size = new System.Drawing.Size(480, 720);
            this.grpSettings.TabIndex = 0;
            this.grpSettings.TabStop = false;
            this.grpSettings.Text = "设置";
            // 
            // grpHotkey
            // 
            this.grpHotkey.Controls.Add(this.btnUpdateHotkey);
            this.grpHotkey.Controls.Add(this.lblHotkeyStatus);
            this.grpHotkey.Controls.Add(this.chkShift);
            this.grpHotkey.Controls.Add(this.chkAlt);
            this.grpHotkey.Controls.Add(this.chkCtrl);
            this.grpHotkey.Controls.Add(this.rdoF9);
            this.grpHotkey.Controls.Add(this.rdoF10);
            this.grpHotkey.Controls.Add(this.rdoF11);
            this.grpHotkey.Controls.Add(this.rdoF12);
            this.grpHotkey.Controls.Add(this.rdoF5);
            this.grpHotkey.Controls.Add(this.rdoF6);
            this.grpHotkey.Controls.Add(this.rdoF7);
            this.grpHotkey.Controls.Add(this.rdoF8);
            this.grpHotkey.Location = new System.Drawing.Point(22, 25);
            this.grpHotkey.Margin = new System.Windows.Forms.Padding(4);
            this.grpHotkey.Name = "grpHotkey";
            this.grpHotkey.Padding = new System.Windows.Forms.Padding(4);
            this.grpHotkey.Size = new System.Drawing.Size(435, 135);
            this.grpHotkey.TabIndex = 8;
            this.grpHotkey.TabStop = false;
            this.grpHotkey.Text = "快捷键设置";
            // 
            // btnUpdateHotkey
            // 
            this.btnUpdateHotkey.Location = new System.Drawing.Point(300, 99);
            this.btnUpdateHotkey.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdateHotkey.Name = "btnUpdateHotkey";
            this.btnUpdateHotkey.Size = new System.Drawing.Size(135, 34);
            this.btnUpdateHotkey.TabIndex = 0;
            this.btnUpdateHotkey.Text = "应用快捷键";
            this.btnUpdateHotkey.UseVisualStyleBackColor = true;
            this.btnUpdateHotkey.Click += new System.EventHandler(this.btnUpdateHotkey_Click);
            // 
            // lblHotkeyStatus
            // 
            this.lblHotkeyStatus.AutoSize = true;
            this.lblHotkeyStatus.ForeColor = System.Drawing.Color.Green;
            this.lblHotkeyStatus.Location = new System.Drawing.Point(315, 38);
            this.lblHotkeyStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHotkeyStatus.Name = "lblHotkeyStatus";
            this.lblHotkeyStatus.Size = new System.Drawing.Size(98, 18);
            this.lblHotkeyStatus.TabIndex = 1;
            this.lblHotkeyStatus.Text = "热键已注册";
            // 
            // chkShift
            // 
            this.chkShift.AutoSize = true;
            this.chkShift.Location = new System.Drawing.Point(172, 38);
            this.chkShift.Margin = new System.Windows.Forms.Padding(4);
            this.chkShift.Name = "chkShift";
            this.chkShift.Size = new System.Drawing.Size(79, 22);
            this.chkShift.TabIndex = 2;
            this.chkShift.Text = "Shift";
            this.chkShift.UseVisualStyleBackColor = true;
            // 
            // chkAlt
            // 
            this.chkAlt.AutoSize = true;
            this.chkAlt.Location = new System.Drawing.Point(98, 38);
            this.chkAlt.Margin = new System.Windows.Forms.Padding(4);
            this.chkAlt.Name = "chkAlt";
            this.chkAlt.Size = new System.Drawing.Size(61, 22);
            this.chkAlt.TabIndex = 3;
            this.chkAlt.Text = "Alt";
            this.chkAlt.UseVisualStyleBackColor = true;
            // 
            // chkCtrl
            // 
            this.chkCtrl.AutoSize = true;
            this.chkCtrl.Checked = true;
            this.chkCtrl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCtrl.Location = new System.Drawing.Point(22, 38);
            this.chkCtrl.Margin = new System.Windows.Forms.Padding(4);
            this.chkCtrl.Name = "chkCtrl";
            this.chkCtrl.Size = new System.Drawing.Size(70, 22);
            this.chkCtrl.TabIndex = 4;
            this.chkCtrl.Text = "Ctrl";
            this.chkCtrl.UseVisualStyleBackColor = true;
            // 
            // rdoF9
            // 
            this.rdoF9.AutoSize = true;
            this.rdoF9.Checked = true;
            this.rdoF9.Location = new System.Drawing.Point(22, 75);
            this.rdoF9.Margin = new System.Windows.Forms.Padding(4);
            this.rdoF9.Name = "rdoF9";
            this.rdoF9.Size = new System.Drawing.Size(51, 22);
            this.rdoF9.TabIndex = 5;
            this.rdoF9.TabStop = true;
            this.rdoF9.Text = "F9";
            this.rdoF9.UseVisualStyleBackColor = true;
            // 
            // rdoF10
            // 
            this.rdoF10.AutoSize = true;
            this.rdoF10.Location = new System.Drawing.Point(98, 75);
            this.rdoF10.Margin = new System.Windows.Forms.Padding(4);
            this.rdoF10.Name = "rdoF10";
            this.rdoF10.Size = new System.Drawing.Size(60, 22);
            this.rdoF10.TabIndex = 6;
            this.rdoF10.Text = "F10";
            this.rdoF10.UseVisualStyleBackColor = true;
            // 
            // rdoF11
            // 
            this.rdoF11.AutoSize = true;
            this.rdoF11.Location = new System.Drawing.Point(172, 75);
            this.rdoF11.Margin = new System.Windows.Forms.Padding(4);
            this.rdoF11.Name = "rdoF11";
            this.rdoF11.Size = new System.Drawing.Size(60, 22);
            this.rdoF11.TabIndex = 7;
            this.rdoF11.Text = "F11";
            this.rdoF11.UseVisualStyleBackColor = true;
            // 
            // rdoF12
            // 
            this.rdoF12.AutoSize = true;
            this.rdoF12.Location = new System.Drawing.Point(248, 75);
            this.rdoF12.Margin = new System.Windows.Forms.Padding(4);
            this.rdoF12.Name = "rdoF12";
            this.rdoF12.Size = new System.Drawing.Size(60, 22);
            this.rdoF12.TabIndex = 8;
            this.rdoF12.Text = "F12";
            this.rdoF12.UseVisualStyleBackColor = true;
            // 
            // rdoF5
            // 
            this.rdoF5.AutoSize = true;
            this.rdoF5.Location = new System.Drawing.Point(22, 105);
            this.rdoF5.Margin = new System.Windows.Forms.Padding(4);
            this.rdoF5.Name = "rdoF5";
            this.rdoF5.Size = new System.Drawing.Size(51, 22);
            this.rdoF5.TabIndex = 9;
            this.rdoF5.Text = "F5";
            this.rdoF5.UseVisualStyleBackColor = true;
            // 
            // rdoF6
            // 
            this.rdoF6.AutoSize = true;
            this.rdoF6.Location = new System.Drawing.Point(98, 105);
            this.rdoF6.Margin = new System.Windows.Forms.Padding(4);
            this.rdoF6.Name = "rdoF6";
            this.rdoF6.Size = new System.Drawing.Size(51, 22);
            this.rdoF6.TabIndex = 10;
            this.rdoF6.Text = "F6";
            this.rdoF6.UseVisualStyleBackColor = true;
            // 
            // rdoF7
            // 
            this.rdoF7.AutoSize = true;
            this.rdoF7.Location = new System.Drawing.Point(172, 105);
            this.rdoF7.Margin = new System.Windows.Forms.Padding(4);
            this.rdoF7.Name = "rdoF7";
            this.rdoF7.Size = new System.Drawing.Size(51, 22);
            this.rdoF7.TabIndex = 11;
            this.rdoF7.Text = "F7";
            this.rdoF7.UseVisualStyleBackColor = true;
            // 
            // rdoF8
            // 
            this.rdoF8.AutoSize = true;
            this.rdoF8.Location = new System.Drawing.Point(248, 105);
            this.rdoF8.Margin = new System.Windows.Forms.Padding(4);
            this.rdoF8.Name = "rdoF8";
            this.rdoF8.Size = new System.Drawing.Size(51, 22);
            this.rdoF8.TabIndex = 12;
            this.rdoF8.Text = "F8";
            this.rdoF8.UseVisualStyleBackColor = true;
            // 
            // grpInterval
            // 
            this.grpInterval.Controls.Add(this.txtInterval);
            this.grpInterval.Controls.Add(this.lblMs);
            this.grpInterval.Controls.Add(this.lblIntervalTip);
            this.grpInterval.Location = new System.Drawing.Point(22, 170);
            this.grpInterval.Margin = new System.Windows.Forms.Padding(4);
            this.grpInterval.Name = "grpInterval";
            this.grpInterval.Padding = new System.Windows.Forms.Padding(4);
            this.grpInterval.Size = new System.Drawing.Size(435, 75);
            this.grpInterval.TabIndex = 10;
            this.grpInterval.TabStop = false;
            this.grpInterval.Text = "点击间隔";
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(22, 38);
            this.txtInterval.Margin = new System.Windows.Forms.Padding(4);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(118, 28);
            this.txtInterval.TabIndex = 0;
            this.txtInterval.Text = "100";
            this.txtInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInterval_KeyPress);
            // 
            // lblMs
            // 
            this.lblMs.AutoSize = true;
            this.lblMs.Location = new System.Drawing.Point(150, 42);
            this.lblMs.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMs.Name = "lblMs";
            this.lblMs.Size = new System.Drawing.Size(44, 18);
            this.lblMs.TabIndex = 1;
            this.lblMs.Text = "毫秒";
            // 
            // lblIntervalTip
            // 
            this.lblIntervalTip.AutoSize = true;
            this.lblIntervalTip.ForeColor = System.Drawing.Color.Gray;
            this.lblIntervalTip.Location = new System.Drawing.Point(195, 42);
            this.lblIntervalTip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIntervalTip.Name = "lblIntervalTip";
            this.lblIntervalTip.Size = new System.Drawing.Size(233, 18);
            this.lblIntervalTip.TabIndex = 2;
            this.lblIntervalTip.Text = "(最小值为1ms，建议≥10ms)";
            // 
            // grpClickCount
            // 
            this.grpClickCount.Controls.Add(this.txtClickCount);
            this.grpClickCount.Controls.Add(this.lblTimes);
            this.grpClickCount.Controls.Add(this.lblInfiniteTip);
            this.grpClickCount.Location = new System.Drawing.Point(22, 255);
            this.grpClickCount.Margin = new System.Windows.Forms.Padding(4);
            this.grpClickCount.Name = "grpClickCount";
            this.grpClickCount.Padding = new System.Windows.Forms.Padding(4);
            this.grpClickCount.Size = new System.Drawing.Size(435, 75);
            this.grpClickCount.TabIndex = 11;
            this.grpClickCount.TabStop = false;
            this.grpClickCount.Text = "循环次数限制";
            // 
            // txtClickCount
            // 
            this.txtClickCount.Location = new System.Drawing.Point(22, 38);
            this.txtClickCount.Margin = new System.Windows.Forms.Padding(4);
            this.txtClickCount.Name = "txtClickCount";
            this.txtClickCount.Size = new System.Drawing.Size(118, 28);
            this.txtClickCount.TabIndex = 0;
            this.txtClickCount.Text = "0";
            this.txtClickCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtClickCount_KeyPress);
            // 
            // lblTimes
            // 
            this.lblTimes.AutoSize = true;
            this.lblTimes.Location = new System.Drawing.Point(150, 42);
            this.lblTimes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTimes.Name = "lblTimes";
            this.lblTimes.Size = new System.Drawing.Size(62, 18);
            this.lblTimes.TabIndex = 1;
            this.lblTimes.Text = "次循环";
            // 
            // lblInfiniteTip
            // 
            this.lblInfiniteTip.AutoSize = true;
            this.lblInfiniteTip.ForeColor = System.Drawing.Color.Gray;
            this.lblInfiniteTip.Location = new System.Drawing.Point(232, 42);
            this.lblInfiniteTip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInfiniteTip.Name = "lblInfiniteTip";
            this.lblInfiniteTip.Size = new System.Drawing.Size(152, 18);
            this.lblInfiniteTip.TabIndex = 2;
            this.lblInfiniteTip.Text = "(0 表示无限循环)";
            // 
            // grpMultiplePoints
            // 
            this.grpMultiplePoints.Controls.Add(this.lblPointsTip);
            this.grpMultiplePoints.Controls.Add(this.chkMousePath);
            this.grpMultiplePoints.Controls.Add(this.btnClearPoints);
            this.grpMultiplePoints.Controls.Add(this.btnRemovePoint);
            this.grpMultiplePoints.Controls.Add(this.btnAddPoint);
            this.grpMultiplePoints.Controls.Add(this.lstPoints);
            this.grpMultiplePoints.Controls.Add(this.chkMultiplePoints);
            this.grpMultiplePoints.Location = new System.Drawing.Point(22, 340);
            this.grpMultiplePoints.Margin = new System.Windows.Forms.Padding(4);
            this.grpMultiplePoints.Name = "grpMultiplePoints";
            this.grpMultiplePoints.Padding = new System.Windows.Forms.Padding(4);
            this.grpMultiplePoints.Size = new System.Drawing.Size(435, 360);
            this.grpMultiplePoints.TabIndex = 12;
            this.grpMultiplePoints.TabStop = false;
            this.grpMultiplePoints.Text = "多点位循环点击";
            // 
            // lblPointsTip
            // 
            this.lblPointsTip.AutoSize = true;
            this.lblPointsTip.ForeColor = System.Drawing.Color.Gray;
            this.lblPointsTip.Location = new System.Drawing.Point(16, 338);
            this.lblPointsTip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPointsTip.Name = "lblPointsTip";
            this.lblPointsTip.Size = new System.Drawing.Size(260, 18);
            this.lblPointsTip.TabIndex = 6;
            this.lblPointsTip.Text = "提示: 双击点位可修改操作类型";
            // 
            // chkMousePath
            // 
            this.chkMousePath.AutoSize = true;
            this.chkMousePath.Checked = true;
            this.chkMousePath.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMousePath.Location = new System.Drawing.Point(235, 29);
            this.chkMousePath.Margin = new System.Windows.Forms.Padding(4);
            this.chkMousePath.Name = "chkMousePath";
            this.chkMousePath.Size = new System.Drawing.Size(178, 22);
            this.chkMousePath.TabIndex = 1;
            this.chkMousePath.Text = "启用鼠标路径模拟";
            this.chkMousePath.UseVisualStyleBackColor = true;
            this.chkMousePath.CheckedChanged += new System.EventHandler(this.chkMousePath_CheckedChanged);
            // 
            // btnClearPoints
            // 
            this.btnClearPoints.Location = new System.Drawing.Point(312, 248);
            this.btnClearPoints.Margin = new System.Windows.Forms.Padding(4);
            this.btnClearPoints.Name = "btnClearPoints";
            this.btnClearPoints.Size = new System.Drawing.Size(115, 32);
            this.btnClearPoints.TabIndex = 5;
            this.btnClearPoints.Text = "清空列表";
            this.btnClearPoints.UseVisualStyleBackColor = true;
            this.btnClearPoints.Click += new System.EventHandler(this.btnClearPoints_Click);
            // 
            // btnRemovePoint
            // 
            this.btnRemovePoint.Location = new System.Drawing.Point(312, 179);
            this.btnRemovePoint.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemovePoint.Name = "btnRemovePoint";
            this.btnRemovePoint.Size = new System.Drawing.Size(115, 32);
            this.btnRemovePoint.TabIndex = 4;
            this.btnRemovePoint.Text = "删除选中";
            this.btnRemovePoint.UseVisualStyleBackColor = true;
            this.btnRemovePoint.Click += new System.EventHandler(this.btnRemovePoint_Click);
            // 
            // btnAddPoint
            // 
            this.btnAddPoint.Location = new System.Drawing.Point(312, 114);
            this.btnAddPoint.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddPoint.Name = "btnAddPoint";
            this.btnAddPoint.Size = new System.Drawing.Size(115, 32);
            this.btnAddPoint.TabIndex = 3;
            this.btnAddPoint.Text = "添加点位";
            this.btnAddPoint.UseVisualStyleBackColor = true;
            this.btnAddPoint.Click += new System.EventHandler(this.btnAddPoint_Click);
            // 
            // lstPoints
            // 
            this.lstPoints.FormattingEnabled = true;
            this.lstPoints.ItemHeight = 18;
            this.lstPoints.Location = new System.Drawing.Point(19, 114);
            this.lstPoints.Margin = new System.Windows.Forms.Padding(4);
            this.lstPoints.Name = "lstPoints";
            this.lstPoints.Size = new System.Drawing.Size(275, 220);
            this.lstPoints.TabIndex = 2;
            this.lstPoints.SelectedIndexChanged += new System.EventHandler(this.lstPoints_SelectedIndexChanged);
            this.lstPoints.DoubleClick += new System.EventHandler(this.lstPoints_DoubleClick);
            // 
            // chkMultiplePoints
            // 
            this.chkMultiplePoints.AutoSize = true;
            this.chkMultiplePoints.Checked = true;
            this.chkMultiplePoints.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMultiplePoints.Location = new System.Drawing.Point(22, 29);
            this.chkMultiplePoints.Margin = new System.Windows.Forms.Padding(4);
            this.chkMultiplePoints.Name = "chkMultiplePoints";
            this.chkMultiplePoints.Size = new System.Drawing.Size(160, 22);
            this.chkMultiplePoints.TabIndex = 0;
            this.chkMultiplePoints.Text = "启用多点位模式";
            this.chkMultiplePoints.UseVisualStyleBackColor = true;
            this.chkMultiplePoints.CheckedChanged += new System.EventHandler(this.chkMultiplePoints_CheckedChanged);
            // 
            // grpStatus
            // 
            this.grpStatus.Controls.Add(this.lblTotalClicks);
            this.grpStatus.Controls.Add(this.lblClickCountValue);
            this.grpStatus.Controls.Add(this.btnResetCount);
            this.grpStatus.Controls.Add(this.lblStatus);
            this.grpStatus.Location = new System.Drawing.Point(18, 798);
            this.grpStatus.Margin = new System.Windows.Forms.Padding(4);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Padding = new System.Windows.Forms.Padding(4);
            this.grpStatus.Size = new System.Drawing.Size(480, 105);
            this.grpStatus.TabIndex = 1;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "状态";
            // 
            // lblTotalClicks
            // 
            this.lblTotalClicks.AutoSize = true;
            this.lblTotalClicks.Location = new System.Drawing.Point(22, 72);
            this.lblTotalClicks.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalClicks.Name = "lblTotalClicks";
            this.lblTotalClicks.Size = new System.Drawing.Size(107, 18);
            this.lblTotalClicks.TabIndex = 0;
            this.lblTotalClicks.Text = "已点击次数:";
            // 
            // lblClickCountValue
            // 
            this.lblClickCountValue.AutoSize = true;
            this.lblClickCountValue.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblClickCountValue.Location = new System.Drawing.Point(135, 69);
            this.lblClickCountValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClickCountValue.Name = "lblClickCountValue";
            this.lblClickCountValue.Size = new System.Drawing.Size(23, 25);
            this.lblClickCountValue.TabIndex = 1;
            this.lblClickCountValue.Text = "0";
            // 
            // btnResetCount
            // 
            this.btnResetCount.Location = new System.Drawing.Point(322, 60);
            this.btnResetCount.Margin = new System.Windows.Forms.Padding(4);
            this.btnResetCount.Name = "btnResetCount";
            this.btnResetCount.Size = new System.Drawing.Size(135, 34);
            this.btnResetCount.TabIndex = 2;
            this.btnResetCount.Text = "重置计数";
            this.btnResetCount.UseVisualStyleBackColor = true;
            this.btnResetCount.Click += new System.EventHandler(this.btnResetCount_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(22, 38);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(66, 25);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "已停止";
            // 
            // btnStartStop
            // 
            this.btnStartStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnStartStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartStop.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnStartStop.ForeColor = System.Drawing.Color.White;
            this.btnStartStop.Location = new System.Drawing.Point(18, 910);
            this.btnStartStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(480, 75);
            this.btnStartStop.TabIndex = 2;
            this.btnStartStop.Text = "开始(F9)";
            this.btnStartStop.UseVisualStyleBackColor = false;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(165, 18);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(178, 42);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "鼠标连点器";
            // 
            // lblAbout
            // 
            this.lblAbout.AutoSize = true;
            this.lblAbout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblAbout.ForeColor = System.Drawing.Color.Blue;
            this.lblAbout.Location = new System.Drawing.Point(420, 33);
            this.lblAbout.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(80, 18);
            this.lblAbout.TabIndex = 4;
            this.lblAbout.Text = "关于作者";
            this.lblAbout.Click += new System.EventHandler(this.lblAbout_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 1000);
            this.Controls.Add(this.lblAbout);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.grpStatus);
            this.Controls.Add(this.grpSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "鼠标连点器 v2.0";
            this.grpSettings.ResumeLayout(false);
            this.grpHotkey.ResumeLayout(false);
            this.grpHotkey.PerformLayout();
            this.grpInterval.ResumeLayout(false);
            this.grpInterval.PerformLayout();
            this.grpClickCount.ResumeLayout(false);
            this.grpClickCount.PerformLayout();
            this.grpMultiplePoints.ResumeLayout(false);
            this.grpMultiplePoints.PerformLayout();
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSettings;
        private System.Windows.Forms.GroupBox grpHotkey;
        private System.Windows.Forms.Button btnUpdateHotkey;
        private System.Windows.Forms.Label lblHotkeyStatus;
        private System.Windows.Forms.CheckBox chkShift;
        private System.Windows.Forms.CheckBox chkAlt;
        private System.Windows.Forms.CheckBox chkCtrl;
        private System.Windows.Forms.RadioButton rdoF9;
        private System.Windows.Forms.RadioButton rdoF10;
        private System.Windows.Forms.RadioButton rdoF11;
        private System.Windows.Forms.RadioButton rdoF12;
        private System.Windows.Forms.RadioButton rdoF5;
        private System.Windows.Forms.RadioButton rdoF6;
        private System.Windows.Forms.RadioButton rdoF7;
        private System.Windows.Forms.RadioButton rdoF8;
        private System.Windows.Forms.GroupBox grpInterval;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Label lblMs;
        private System.Windows.Forms.Label lblIntervalTip;
        private System.Windows.Forms.GroupBox grpClickCount;
        private System.Windows.Forms.TextBox txtClickCount;
        private System.Windows.Forms.Label lblTimes;
        private System.Windows.Forms.Label lblInfiniteTip;
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.Label lblTotalClicks;
        private System.Windows.Forms.Label lblClickCountValue;
        private System.Windows.Forms.Button btnResetCount;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblAbout;
        
        // 多点位相关控件
        private System.Windows.Forms.GroupBox grpMultiplePoints;
        private System.Windows.Forms.CheckBox chkMultiplePoints;
        private System.Windows.Forms.CheckBox chkMousePath;
        private System.Windows.Forms.ListBox lstPoints;
        private System.Windows.Forms.Button btnAddPoint;
        private System.Windows.Forms.Button btnRemovePoint;
        private System.Windows.Forms.Button btnClearPoints;
        private System.Windows.Forms.Label lblPointsTip;
    }
}
