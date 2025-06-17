using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using CPS_5RH.Data;

namespace CPS_5RH;

public class Frm_Emerg : Form
{
	private IContainer components = null;

	private ComboBox coBox_GrpNo;

	private ComboBox coBox_EmergMode;

	private ComboBox coBox_EmergType;

	private Label label7;

	private Label label6;

	private Label label5;

	private Label label9;

	private TabControl Tab_SysCtrl;

	private TabPage tabP1;

	private TabPage tabP2;

	private TabPage tabP3;

	private TabPage tabP4;

	private TabPage tabP5;

	private TabPage tabP6;

	private TabPage tabP7;

	private TabPage tabP8;

	private TabPage tabP9;

	private TabPage tabP10;

	private ComboBox coBox_Cycle;

	private Label label8;

	private Label label4;

	private ComboBox coBox_SelChn;

	private Label label3;

	private Label label2;

	private Label label1;

	private NumericUpDown num_TxTim;

	private NumericUpDown num_ExigTim;

	private NumericUpDown num_RxTim;

	private Panel panel1;

	private ComboBox coBox_ExgChn;

	private GroupBox grp1;

	private static bool initFlg = false;

	public static bool nodeFlg = false;

	private DataApp tdata = null;

	private static List<string> DtmfList = new List<string> { "" };

	private static List<string> ChnList = new List<string> { "" };

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPS_5RH.Frm_Emerg));
		this.label9 = new System.Windows.Forms.Label();
		this.coBox_GrpNo = new System.Windows.Forms.ComboBox();
		this.coBox_EmergMode = new System.Windows.Forms.ComboBox();
		this.coBox_EmergType = new System.Windows.Forms.ComboBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.Tab_SysCtrl = new System.Windows.Forms.TabControl();
		this.tabP1 = new System.Windows.Forms.TabPage();
		this.panel1 = new System.Windows.Forms.Panel();
		this.coBox_ExgChn = new System.Windows.Forms.ComboBox();
		this.num_TxTim = new System.Windows.Forms.NumericUpDown();
		this.num_ExigTim = new System.Windows.Forms.NumericUpDown();
		this.num_RxTim = new System.Windows.Forms.NumericUpDown();
		this.coBox_Cycle = new System.Windows.Forms.ComboBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.coBox_SelChn = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.tabP2 = new System.Windows.Forms.TabPage();
		this.tabP3 = new System.Windows.Forms.TabPage();
		this.tabP4 = new System.Windows.Forms.TabPage();
		this.tabP5 = new System.Windows.Forms.TabPage();
		this.tabP6 = new System.Windows.Forms.TabPage();
		this.tabP7 = new System.Windows.Forms.TabPage();
		this.tabP8 = new System.Windows.Forms.TabPage();
		this.tabP9 = new System.Windows.Forms.TabPage();
		this.tabP10 = new System.Windows.Forms.TabPage();
		this.grp1 = new System.Windows.Forms.GroupBox();
		this.Tab_SysCtrl.SuspendLayout();
		this.tabP1.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.num_TxTim).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.num_ExigTim).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.num_RxTim).BeginInit();
		this.grp1.SuspendLayout();
		base.SuspendLayout();
		this.label9.Location = new System.Drawing.Point(31, 95);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(172, 20);
		this.label9.TabIndex = 9;
		this.label9.Text = "报警时间(s)";
		this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_GrpNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_GrpNo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_GrpNo.FormattingEnabled = true;
		this.coBox_GrpNo.Items.AddRange(new object[2] { "无", "选定的" });
		this.coBox_GrpNo.Location = new System.Drawing.Point(209, 70);
		this.coBox_GrpNo.Name = "coBox_GrpNo";
		this.coBox_GrpNo.Size = new System.Drawing.Size(74, 20);
		this.coBox_GrpNo.TabIndex = 7;
		this.coBox_GrpNo.SelectedIndexChanged += new System.EventHandler(coBox_GrpNo_SelectedIndexChanged);
		this.coBox_EmergMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_EmergMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_EmergMode.FormattingEnabled = true;
		this.coBox_EmergMode.Items.AddRange(new object[3] { "无", "DTMF", "五音" });
		this.coBox_EmergMode.Location = new System.Drawing.Point(209, 45);
		this.coBox_EmergMode.Name = "coBox_EmergMode";
		this.coBox_EmergMode.Size = new System.Drawing.Size(74, 20);
		this.coBox_EmergMode.TabIndex = 6;
		this.coBox_EmergMode.SelectedIndexChanged += new System.EventHandler(coBox_EmergMode_SelectedIndexChanged);
		this.coBox_EmergType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_EmergType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_EmergType.FormattingEnabled = true;
		this.coBox_EmergType.Items.AddRange(new object[4] { "本机报警", "发送 ENI 和背景音(本地无报警音)", "发送 ENI 和报警音(本地无报警音)", "发送 ENI 和报警音(本地有报警音)" });
		this.coBox_EmergType.Location = new System.Drawing.Point(209, 15);
		this.coBox_EmergType.Name = "coBox_EmergType";
		this.coBox_EmergType.Size = new System.Drawing.Size(200, 20);
		this.coBox_EmergType.TabIndex = 5;
		this.coBox_EmergType.SelectedIndexChanged += new System.EventHandler(coBox_EmergType_SelectedIndexChanged);
		this.label7.Location = new System.Drawing.Point(63, 70);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(140, 20);
		this.label7.TabIndex = 3;
		this.label7.Text = "紧急识别码组号";
		this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label6.Location = new System.Drawing.Point(61, 45);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(142, 20);
		this.label6.TabIndex = 2;
		this.label6.Text = "紧急识别码类型";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label5.Location = new System.Drawing.Point(103, 15);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(100, 20);
		this.label5.TabIndex = 1;
		this.label5.Text = "紧急警报";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.Tab_SysCtrl.Controls.Add(this.tabP1);
		this.Tab_SysCtrl.Controls.Add(this.tabP2);
		this.Tab_SysCtrl.Controls.Add(this.tabP3);
		this.Tab_SysCtrl.Controls.Add(this.tabP4);
		this.Tab_SysCtrl.Controls.Add(this.tabP5);
		this.Tab_SysCtrl.Controls.Add(this.tabP6);
		this.Tab_SysCtrl.Controls.Add(this.tabP7);
		this.Tab_SysCtrl.Controls.Add(this.tabP8);
		this.Tab_SysCtrl.Controls.Add(this.tabP9);
		this.Tab_SysCtrl.Controls.Add(this.tabP10);
		this.Tab_SysCtrl.Location = new System.Drawing.Point(24, 20);
		this.Tab_SysCtrl.Name = "Tab_SysCtrl";
		this.Tab_SysCtrl.Padding = new System.Drawing.Point(10, 5);
		this.Tab_SysCtrl.SelectedIndex = 0;
		this.Tab_SysCtrl.Size = new System.Drawing.Size(576, 312);
		this.Tab_SysCtrl.TabIndex = 17;
		this.Tab_SysCtrl.SelectedIndexChanged += new System.EventHandler(Tab_SysCtrl_SelectedIndexChanged);
		this.tabP1.BackColor = System.Drawing.Color.Transparent;
		this.tabP1.Controls.Add(this.panel1);
		this.tabP1.Location = new System.Drawing.Point(4, 26);
		this.tabP1.Name = "tabP1";
		this.tabP1.Size = new System.Drawing.Size(568, 282);
		this.tabP1.TabIndex = 0;
		this.tabP1.Text = "系统1";
		this.tabP1.UseVisualStyleBackColor = true;
		this.panel1.BackColor = System.Drawing.SystemColors.Control;
		this.panel1.Controls.Add(this.coBox_ExgChn);
		this.panel1.Controls.Add(this.num_TxTim);
		this.panel1.Controls.Add(this.coBox_EmergType);
		this.panel1.Controls.Add(this.num_ExigTim);
		this.panel1.Controls.Add(this.coBox_GrpNo);
		this.panel1.Controls.Add(this.num_RxTim);
		this.panel1.Controls.Add(this.label7);
		this.panel1.Controls.Add(this.coBox_Cycle);
		this.panel1.Controls.Add(this.label6);
		this.panel1.Controls.Add(this.label8);
		this.panel1.Controls.Add(this.coBox_EmergMode);
		this.panel1.Controls.Add(this.label9);
		this.panel1.Controls.Add(this.label4);
		this.panel1.Controls.Add(this.label5);
		this.panel1.Controls.Add(this.coBox_SelChn);
		this.panel1.Controls.Add(this.label1);
		this.panel1.Controls.Add(this.label3);
		this.panel1.Controls.Add(this.label2);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(568, 282);
		this.panel1.TabIndex = 28;
		this.coBox_ExgChn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_ExgChn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_ExgChn.FormattingEnabled = true;
		this.coBox_ExgChn.Items.AddRange(new object[2] { "分配信道", "选择信道" });
		this.coBox_ExgChn.Location = new System.Drawing.Point(209, 222);
		this.coBox_ExgChn.Name = "coBox_ExgChn";
		this.coBox_ExgChn.Size = new System.Drawing.Size(111, 20);
		this.coBox_ExgChn.TabIndex = 30;
		this.coBox_ExgChn.SelectedIndexChanged += new System.EventHandler(coBox_ExgChn_SelectedIndexChanged);
		this.num_TxTim.Location = new System.Drawing.Point(209, 120);
		this.num_TxTim.Maximum = new decimal(new int[4] { 255, 0, 0, 0 });
		this.num_TxTim.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.num_TxTim.Name = "num_TxTim";
		this.num_TxTim.Size = new System.Drawing.Size(74, 21);
		this.num_TxTim.TabIndex = 27;
		this.num_TxTim.Value = new decimal(new int[4] { 5, 0, 0, 0 });
		this.num_ExigTim.Location = new System.Drawing.Point(209, 95);
		this.num_ExigTim.Maximum = new decimal(new int[4] { 255, 0, 0, 0 });
		this.num_ExigTim.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.num_ExigTim.Name = "num_ExigTim";
		this.num_ExigTim.Size = new System.Drawing.Size(74, 21);
		this.num_ExigTim.TabIndex = 26;
		this.num_ExigTim.Value = new decimal(new int[4] { 5, 0, 0, 0 });
		this.num_RxTim.Location = new System.Drawing.Point(209, 145);
		this.num_RxTim.Maximum = new decimal(new int[4] { 255, 0, 0, 0 });
		this.num_RxTim.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.num_RxTim.Name = "num_RxTim";
		this.num_RxTim.Size = new System.Drawing.Size(74, 21);
		this.num_RxTim.TabIndex = 25;
		this.num_RxTim.Value = new decimal(new int[4] { 5, 0, 0, 0 });
		this.coBox_Cycle.DropDownHeight = 120;
		this.coBox_Cycle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Cycle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Cycle.FormattingEnabled = true;
		this.coBox_Cycle.IntegralHeight = false;
		this.coBox_Cycle.Items.AddRange(new object[256]
		{
			"持续", "1", "2", "3", "4", "5", "6", "7", "8", "9",
			"10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
			"20", "21", "22", "23", "24", "25", "26", "27", "28", "29",
			"30", "31", "32", "33", "34", "35", "36", "37", "38", "39",
			"40", "41", "42", "43", "44", "45", "46", "47", "48", "49",
			"50", "51", "52", "53", "54", "55", "56", "57", "58", "59",
			"60", "61", "62", "63", "64", "65", "66", "67", "68", "69",
			"70", "71", "72", "73", "74", "75", "76", "77", "78", "79",
			"80", "81", "82", "83", "84", "85", "86", "87", "88", "89",
			"90", "91", "92", "93", "94", "95", "96", "97", "98", "99",
			"100", "101", "102", "103", "104", "105", "106", "107", "108", "109",
			"110", "111", "112", "113", "114", "115", "116", "117", "118", "119",
			"120", "121", "122", "123", "124", "125", "126", "127", "128", "129",
			"130", "131", "132", "133", "134", "135", "136", "137", "138", "139",
			"140", "141", "142", "143", "144", "145", "146", "147", "148", "149",
			"150", "151", "152", "153", "154", "155", "156", "157", "158", "159",
			"160", "161", "162", "163", "164", "165", "166", "167", "168", "169",
			"170", "171", "172", "173", "174", "175", "176", "177", "178", "179",
			"180", "181", "182", "183", "184", "185", "186", "187", "188", "189",
			"190", "191", "192", "193", "194", "195", "196", "197", "198", "199",
			"200", "201", "202", "203", "204", "205", "206", "207", "208", "209",
			"210", "211", "212", "213", "214", "215", "216", "217", "218", "219",
			"220", "221", "222", "223", "224", "225", "226", "227", "228", "229",
			"230", "231", "232", "233", "234", "235", "236", "237", "238", "239",
			"240", "241", "242", "243", "244", "245", "246", "247", "248", "249",
			"250", "251", "252", "253", "254", "255"
		});
		this.coBox_Cycle.Location = new System.Drawing.Point(209, 171);
		this.coBox_Cycle.Name = "coBox_Cycle";
		this.coBox_Cycle.Size = new System.Drawing.Size(111, 20);
		this.coBox_Cycle.TabIndex = 24;
		this.label8.Location = new System.Drawing.Point(31, 171);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(172, 20);
		this.label8.TabIndex = 23;
		this.label8.Text = "周期";
		this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label4.Location = new System.Drawing.Point(31, 222);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(172, 20);
		this.label4.TabIndex = 21;
		this.label4.Text = "紧急区域信道号";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_SelChn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_SelChn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_SelChn.FormattingEnabled = true;
		this.coBox_SelChn.Items.AddRange(new object[2] { "分配信道", "选择信道" });
		this.coBox_SelChn.Location = new System.Drawing.Point(209, 197);
		this.coBox_SelChn.Name = "coBox_SelChn";
		this.coBox_SelChn.Size = new System.Drawing.Size(111, 20);
		this.coBox_SelChn.TabIndex = 20;
		this.coBox_SelChn.SelectedIndexChanged += new System.EventHandler(coBox_SelChn_SelectedIndexChanged);
		this.label1.Location = new System.Drawing.Point(31, 120);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(172, 20);
		this.label1.TabIndex = 15;
		this.label1.Text = "发射时间(s)";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label3.Location = new System.Drawing.Point(31, 197);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(172, 20);
		this.label3.TabIndex = 19;
		this.label3.Text = "信道选择";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label2.Location = new System.Drawing.Point(31, 145);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(172, 20);
		this.label2.TabIndex = 17;
		this.label2.Text = "接收时间(s)";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tabP2.BackColor = System.Drawing.SystemColors.Control;
		this.tabP2.Location = new System.Drawing.Point(4, 26);
		this.tabP2.Name = "tabP2";
		this.tabP2.Size = new System.Drawing.Size(568, 282);
		this.tabP2.TabIndex = 1;
		this.tabP2.Text = "系统2";
		this.tabP3.BackColor = System.Drawing.SystemColors.Control;
		this.tabP3.Location = new System.Drawing.Point(4, 26);
		this.tabP3.Name = "tabP3";
		this.tabP3.Size = new System.Drawing.Size(568, 282);
		this.tabP3.TabIndex = 2;
		this.tabP3.Text = "系统3";
		this.tabP4.BackColor = System.Drawing.SystemColors.Control;
		this.tabP4.Location = new System.Drawing.Point(4, 26);
		this.tabP4.Name = "tabP4";
		this.tabP4.Size = new System.Drawing.Size(568, 282);
		this.tabP4.TabIndex = 3;
		this.tabP4.Text = "系统4";
		this.tabP5.BackColor = System.Drawing.SystemColors.Control;
		this.tabP5.Location = new System.Drawing.Point(4, 26);
		this.tabP5.Name = "tabP5";
		this.tabP5.Size = new System.Drawing.Size(568, 282);
		this.tabP5.TabIndex = 4;
		this.tabP5.Text = "系统5";
		this.tabP6.BackColor = System.Drawing.SystemColors.Control;
		this.tabP6.ForeColor = System.Drawing.SystemColors.ControlText;
		this.tabP6.Location = new System.Drawing.Point(4, 26);
		this.tabP6.Name = "tabP6";
		this.tabP6.Size = new System.Drawing.Size(568, 282);
		this.tabP6.TabIndex = 5;
		this.tabP6.Text = "系统6";
		this.tabP7.BackColor = System.Drawing.SystemColors.Control;
		this.tabP7.Location = new System.Drawing.Point(4, 26);
		this.tabP7.Name = "tabP7";
		this.tabP7.Size = new System.Drawing.Size(568, 282);
		this.tabP7.TabIndex = 6;
		this.tabP7.Text = "系统7";
		this.tabP8.BackColor = System.Drawing.SystemColors.Control;
		this.tabP8.Location = new System.Drawing.Point(4, 26);
		this.tabP8.Name = "tabP8";
		this.tabP8.Size = new System.Drawing.Size(568, 282);
		this.tabP8.TabIndex = 7;
		this.tabP8.Text = "系统8";
		this.tabP9.BackColor = System.Drawing.SystemColors.Control;
		this.tabP9.Location = new System.Drawing.Point(4, 26);
		this.tabP9.Name = "tabP9";
		this.tabP9.Size = new System.Drawing.Size(568, 282);
		this.tabP9.TabIndex = 8;
		this.tabP9.Text = "系统9";
		this.tabP10.BackColor = System.Drawing.SystemColors.Control;
		this.tabP10.Location = new System.Drawing.Point(4, 26);
		this.tabP10.Name = "tabP10";
		this.tabP10.Size = new System.Drawing.Size(568, 282);
		this.tabP10.TabIndex = 9;
		this.tabP10.Text = "系统10";
		this.grp1.Controls.Add(this.Tab_SysCtrl);
		this.grp1.Location = new System.Drawing.Point(23, 17);
		this.grp1.Name = "grp1";
		this.grp1.Size = new System.Drawing.Size(623, 358);
		this.grp1.TabIndex = 18;
		this.grp1.TabStop = false;
		this.grp1.Text = "紧急警报";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(678, 399);
		base.Controls.Add(this.grp1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Frm_Emerg";
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = "Frm_Emerg";
		base.Load += new System.EventHandler(Frm_Emerg_Load);
		base.Activated += new System.EventHandler(Frm_Emerg_Activated);
		this.Tab_SysCtrl.ResumeLayout(false);
		this.tabP1.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.num_TxTim).EndInit();
		((System.ComponentModel.ISupportInitialize)this.num_ExigTim).EndInit();
		((System.ComponentModel.ISupportInitialize)this.num_RxTim).EndInit();
		this.grp1.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public Frm_Emerg(DataApp data)
	{
		InitializeComponent();
		tdata = data;
	}

	private void Frm_Emerg_Load(object sender, EventArgs e)
	{
		string[] strItems = new string[10];
		string tmp = "";
		if (FormMain.lang == "Chinese")
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
		}
		LanguageSel.LoadLanguage(this, typeof(Frm_Emerg));
		for (int i = 0; i < 10; i++)
		{
			LanguageSel.EmergCombobox.TryGetValue(i.ToString(), out tmp);
			strItems[i] = tmp;
		}
		coBox_EmergType.Items[0] = strItems[0];
		coBox_EmergType.Items[1] = strItems[1];
		coBox_EmergType.Items[2] = strItems[2];
		coBox_EmergType.Items[3] = strItems[3];
		coBox_EmergMode.Items[0] = strItems[4];
		coBox_EmergMode.Items[2] = strItems[5];
		coBox_GrpNo.Items[0] = strItems[4];
		coBox_GrpNo.Items[1] = strItems[6];
		coBox_SelChn.Items[0] = strItems[7];
		coBox_SelChn.Items[1] = strItems[8];
		coBox_Cycle.Items[0] = strItems[9];
	}

	private void DispEmergTypeControl(int mode)
	{
		if (mode == 0)
		{
			coBox_EmergMode.Enabled = false;
			coBox_GrpNo.Enabled = false;
			num_TxTim.Enabled = false;
			num_RxTim.Enabled = false;
			num_ExigTim.Enabled = true;
			coBox_SelChn.Enabled = false;
			coBox_ExgChn.Enabled = false;
			coBox_Cycle.Enabled = false;
		}
		else
		{
			coBox_EmergMode.Enabled = true;
			coBox_GrpNo.Enabled = true;
			num_TxTim.Enabled = true;
			num_RxTim.Enabled = true;
			num_ExigTim.Enabled = false;
			coBox_SelChn.Enabled = true;
			coBox_ExgChn.Enabled = true;
			coBox_Cycle.Enabled = true;
		}
	}

	private void DispSelChnControl(int mode)
	{
		if (mode == 0 && coBox_EmergType.SelectedIndex != 0)
		{
			label4.Visible = true;
			coBox_ExgChn.Visible = true;
		}
		else
		{
			label4.Visible = false;
			coBox_ExgChn.Visible = false;
		}
	}

	private void BindRlyChn()
	{
		string name = string.Empty;
		coBox_ExgChn.DataSource = null;
		ChnList.Clear();
		int total = 0;
		for (int i = 0; i < DatChannelInfo.Max_Chn_Num; i++)
		{
			if (tdata.dataChnInfor[i].UseFlg == 1)
			{
				int zone = i / DataApp.Zone_Max_Chn_Num + 1;
				int chn = i % DataApp.Zone_Max_Chn_Num + 1;
				name = zone + " - " + chn;
				ChnList.Add(name);
				total++;
				if (total >= DatChannelInfo.ChnTotal)
				{
					break;
				}
			}
		}
		coBox_ExgChn.DataSource = ChnList;
	}

	private void BindEmergGrp(int pos, int mode)
	{
		int len = 0;
		int match = tdata.dataEmergInfor[pos].GrpNo;
		bool mflg = false;
		string name = string.Empty;
		nodeFlg = false;
		coBox_GrpNo.DataSource = null;
		DtmfList.Clear();
		LanguageSel.EmergCombobox.TryGetValue("4", out name);
		DtmfList.Add(name);
		if (tdata.dataEmergInfor[pos].Mode == 1)
		{
			for (int i = 0; i < 16; i++)
			{
				if (tdata.dataDtmfEncInfor.GetEncLen(i) > 0)
				{
					DtmfList.Add((i + 1).ToString());
					if (match == i + 1)
					{
						mflg = true;
					}
				}
			}
		}
		else if (tdata.dataEmergInfor[pos].Mode == 2)
		{
			for (int i = 0; i < 100; i++)
			{
				len = tdata.dataFiveToneEnc.GetEncCodeLen(i);
				if (len > 0 && len <= 24)
				{
					DtmfList.Add((i + 1).ToString());
					if (match == i + 1)
					{
						mflg = true;
					}
				}
			}
		}
		coBox_GrpNo.DataSource = DtmfList;
		if (mode == 1 && !mflg)
		{
			if (DtmfList.Count > 1)
			{
				tdata.dataEmergInfor[pos].GrpNo = Convert.ToByte(DtmfList[1]);
				coBox_GrpNo.Text = DtmfList[1];
			}
			else
			{
				tdata.dataEmergInfor[pos].GrpNo = 0;
				coBox_GrpNo.Text = DtmfList[0];
			}
		}
		nodeFlg = true;
	}

	private void bingDingTheControls(int pos)
	{
		coBox_EmergType.DataBindings.Clear();
		coBox_EmergMode.DataBindings.Clear();
		num_ExigTim.DataBindings.Clear();
		num_TxTim.DataBindings.Clear();
		num_RxTim.DataBindings.Clear();
		coBox_SelChn.DataBindings.Clear();
		coBox_Cycle.DataBindings.Clear();
		if (tdata.dataEmergInfor[pos].Type > 3)
		{
			tdata.dataEmergInfor[pos].Type = 0;
		}
		if (tdata.dataEmergInfor[pos].Mode > 2)
		{
			tdata.dataEmergInfor[pos].Mode = 0;
		}
		if (tdata.dataEmergInfor[pos].ChSel > 2)
		{
			tdata.dataEmergInfor[pos].ChSel = 0;
		}
		if (tdata.dataEmergInfor[pos].ExgTime == 0)
		{
			tdata.dataEmergInfor[pos].ExgTime = 1;
		}
		if (tdata.dataEmergInfor[pos].Zone > 10)
		{
			tdata.dataEmergInfor[pos].Zone = 0;
		}
		if (tdata.dataEmergInfor[pos].Chn > 64)
		{
			tdata.dataEmergInfor[pos].Chn = 0;
		}
		coBox_EmergMode.SelectedIndex = tdata.dataEmergInfor[pos].Mode;
		coBox_EmergType.DataBindings.Add("SelectedIndex", tdata.dataEmergInfor[pos], "Type", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		num_ExigTim.DataBindings.Add("Value", tdata.dataEmergInfor[pos], "ExgTime", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		num_TxTim.DataBindings.Add("Value", tdata.dataEmergInfor[pos], "TxTime", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		num_RxTim.DataBindings.Add("Value", tdata.dataEmergInfor[pos], "RxTime", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_SelChn.DataBindings.Add("SelectedIndex", tdata.dataEmergInfor[pos], "ChSel", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_Cycle.DataBindings.Add("SelectedIndex", tdata.dataEmergInfor[pos], "Duration", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
	}

	private void CoBox_Chn_Disp()
	{
		string name = "";
		int idx2 = tdata.dataEmergInfor[Tab_SysCtrl.SelectedIndex].Chn;
		if (idx2 > 63)
		{
			idx2 = 0;
		}
		idx2++;
		int idx3 = tdata.dataEmergInfor[Tab_SysCtrl.SelectedIndex].Zone;
		if (idx3 > 9)
		{
			idx3 = 0;
		}
		name = idx3 + 1 + " - " + idx2;
		if (coBox_ExgChn.Items.IndexOf(name) >= 0)
		{
			coBox_ExgChn.SelectedItem = name;
		}
		else
		{
			coBox_ExgChn.SelectedIndex = 0;
		}
	}

	private void CoBox_EmergGrpNo_Disp(int pos)
	{
		int idx = tdata.dataEmergInfor[pos].GrpNo;
		if (idx == 0)
		{
			coBox_GrpNo.Text = DtmfList[0];
			return;
		}
		string tmp = idx.ToString();
		if (coBox_GrpNo.Items.IndexOf(tmp) > 0)
		{
			coBox_GrpNo.Text = tmp;
		}
	}

	public void ExigDataDisp(int pos)
	{
		initFlg = false;
		bingDingTheControls(pos);
		CoBox_EmergGrpNo_Disp(pos);
		initFlg = true;
		CoBox_Chn_Disp();
	}

	private void Tab_SysCtrl_SelectedIndexChanged(object sender, EventArgs e)
	{
		Tab_SysCtrl.Controls.Remove(panel1);
		switch (Tab_SysCtrl.SelectedIndex)
		{
		default:
			return;
		case 0:
			tabP1.Controls.Add(panel1);
			break;
		case 1:
			tabP2.Controls.Add(panel1);
			break;
		case 2:
			tabP3.Controls.Add(panel1);
			break;
		case 3:
			tabP4.Controls.Add(panel1);
			break;
		case 4:
			tabP5.Controls.Add(panel1);
			break;
		case 5:
			tabP6.Controls.Add(panel1);
			break;
		case 6:
			tabP7.Controls.Add(panel1);
			break;
		case 7:
			tabP8.Controls.Add(panel1);
			break;
		case 8:
			tabP9.Controls.Add(panel1);
			break;
		case 9:
			tabP10.Controls.Add(panel1);
			break;
		}
		ExigDataDisp(Tab_SysCtrl.SelectedIndex);
	}

	private void coBox_EmergType_SelectedIndexChanged(object sender, EventArgs e)
	{
		DispEmergTypeControl(coBox_EmergType.SelectedIndex);
		DispSelChnControl(coBox_SelChn.SelectedIndex);
	}

	private void coBox_SelChn_SelectedIndexChanged(object sender, EventArgs e)
	{
		DispSelChnControl(coBox_SelChn.SelectedIndex);
	}

	private void coBox_EmergMode_SelectedIndexChanged(object sender, EventArgs e)
	{
		tdata.dataEmergInfor[Tab_SysCtrl.SelectedIndex].Mode = (byte)coBox_EmergMode.SelectedIndex;
		BindEmergGrp(Tab_SysCtrl.SelectedIndex, 1);
	}

	private void Frm_Emerg_Activated(object sender, EventArgs e)
	{
		int pos = Tab_SysCtrl.SelectedIndex;
		initFlg = false;
		BindEmergGrp(pos, 0);
		BindRlyChn();
		initFlg = true;
		ExigDataDisp(pos);
	}

	private void coBox_ExgChn_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (initFlg)
		{
			string seltext = coBox_ExgChn.Text;
			int idx = seltext.IndexOf(" ");
			int value = Convert.ToByte(seltext.Substring(0, idx)) - 1;
			tdata.dataEmergInfor[Tab_SysCtrl.SelectedIndex].Zone = (byte)value;
			value = Convert.ToByte(seltext.Substring(idx + 3)) - 1;
			tdata.dataEmergInfor[Tab_SysCtrl.SelectedIndex].Chn = (byte)value;
		}
	}

	private void coBox_GrpNo_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (initFlg && nodeFlg)
		{
			if (coBox_GrpNo.SelectedIndex == 0)
			{
				tdata.dataEmergInfor[Tab_SysCtrl.SelectedIndex].GrpNo = 0;
				return;
			}
			int idx = Convert.ToByte(coBox_GrpNo.Text);
			tdata.dataEmergInfor[Tab_SysCtrl.SelectedIndex].GrpNo = (byte)idx;
		}
	}
}
