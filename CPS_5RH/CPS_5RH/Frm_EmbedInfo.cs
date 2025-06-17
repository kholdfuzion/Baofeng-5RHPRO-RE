using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using CPS_5RH.Data;

namespace CPS_5RH;

public class Frm_EmbedInfo : Form
{
	private DataApp tdata = null;

	private IContainer components = null;

	private GroupBox groupBox1;

	private GroupBox groupBox2;

	private CheckBox checkBox3;

	private CheckBox checkBox2;

	private CheckBox checkBox1;

	private GroupBox groupBox3;

	private CheckBox checkBox9;

	private CheckBox checkBox5;

	private CheckBox checkBox6;

	private CheckBox checkBox7;

	private CheckBox checkBox8;

	private CheckBox checkBox4;

	private GroupBox groupBox4;

	private GroupBox groupBox5;

	private CheckBox cBox_Noise;

	private CheckBox cBox_Falldown;

	private CheckBox cBox_Bluet;

	private CheckBox cBox_Flight;

	private CheckBox cBox_Gps;

	private GroupBox groupBox6;

	private CheckBox cBox_DTMF;

	private CheckBox cBox_MDC;

	private CheckBox cBox_FiveTone;

	private CheckBox cBox_TwoTone;

	private Label lbl_2Tone;

	private Label label8;

	private Label label7;

	private Label label6;

	private Label label5;

	private Label label4;

	private Label label3;

	private Label label2;

	private Label label1;

	private CheckBox checkBox10;

	private CheckBox checkBox11;

	private Label label9;

	private CheckBox cBox_Zone;

	private Button btn_ok;

	private Button btn_Cancel;

	private Label lbl_PkeyCnt;

	private ComboBox cBox_Pkey;

	private GroupBox groupBox7;

	private GroupBox groupBox8;

	private CheckBox checkBox12;

	private CheckBox checkBox13;

	private CheckBox checkBox14;

	private GroupBox groupBox9;

	private CheckBox cBox_M350;

	private CheckBox cBox_M500;

	private CheckBox cBox_M200;

	private Label label12;

	private Label label11;

	private Label label10;

	public Frm_EmbedInfo(DataApp data)
	{
		InitializeComponent();
		tdata = data;
	}

	private void Frm_EmbedInfo_Load(object sender, EventArgs e)
	{
		if (FormMain.lang == "Chinese")
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
		}
		ComponentResourceManager crm = new ComponentResourceManager(typeof(Frm_EmbedInfo));
		LanguageSel.LoadLanguage(this, typeof(Frm_EmbedInfo));
		if (FormMain.lang == "Chinese")
		{
			Text = "嵌入信息";
		}
		else
		{
			Text = "Embed information";
		}
		cBox_TwoTone.Checked = tdata.dataDevice.En2Tone == 1;
		cBox_FiveTone.Checked = tdata.dataDevice.En5Tone == 1;
		cBox_DTMF.Checked = tdata.dataDevice.EnDTMF == 1;
		cBox_MDC.Checked = tdata.dataDevice.EnMdc1200 == 1;
		cBox_Gps.Checked = tdata.dataDevice.EnGps == 1;
		cBox_Flight.Checked = tdata.dataDevice.EnFlight == 1;
		cBox_Bluet.Checked = tdata.dataDevice.EnBT == 1;
		cBox_Falldown.Checked = tdata.dataDevice.EnFalldn == 1;
		cBox_Noise.Checked = tdata.dataDevice.EnNoise == 1;
		cBox_Zone.Checked = tdata.dataDevice.EnZone == 1;
		cBox_M200.Checked = tdata.dataDevice.Dis200m == 1;
		cBox_M350.Checked = tdata.dataDevice.Dis350m == 1;
		cBox_M500.Checked = tdata.dataDevice.Dis500m == 1;
		cBox_Pkey.SelectedIndex = tdata.dataDevice.PkeyCnt;
	}

	private void Tabpage1bingDing(DataApp tdata)
	{
		cBox_TwoTone.DataBindings.Add("Checked", tdata.dataDevice, "En2Tone", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cBox_FiveTone.DataBindings.Add("Checked", tdata.dataDevice, "En5Tone", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cBox_DTMF.DataBindings.Add("Checked", tdata.dataDevice, "EnDTMF", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cBox_MDC.DataBindings.Add("Checked", tdata.dataDevice, "EnMdc1200", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cBox_Gps.DataBindings.Add("Checked", tdata.dataDevice, "EnGps", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cBox_Flight.DataBindings.Add("Checked", tdata.dataDevice, "EnFlight", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cBox_Bluet.DataBindings.Add("Checked", tdata.dataDevice, "EnBT", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cBox_Falldown.DataBindings.Add("Checked", tdata.dataDevice, "EnFalldn", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cBox_Noise.DataBindings.Add("Checked", tdata.dataDevice, "EnNoise", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cBox_Zone.DataBindings.Add("Checked", tdata.dataDevice, "EnZone", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
	}

	private DialogResult suggestionForSaveFile()
	{
		if (FormMain.lang == "Chinese")
		{
			return MessageBox.Show("将清除所有数据?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		}
		return MessageBox.Show("Will clear all data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
	}

	private void btn_ok_Click(object sender, EventArgs e)
	{
		if (suggestionForSaveFile() == DialogResult.Yes)
		{
			tdata.dataDevice.En2Tone = (byte)(cBox_TwoTone.Checked ? 1u : 0u);
			tdata.dataDevice.En5Tone = (byte)(cBox_FiveTone.Checked ? 1u : 0u);
			tdata.dataDevice.EnDTMF = (byte)(cBox_DTMF.Checked ? 1u : 0u);
			tdata.dataDevice.EnMdc1200 = (byte)(cBox_MDC.Checked ? 1u : 0u);
			tdata.dataDevice.EnGps = (byte)(cBox_Gps.Checked ? 1u : 0u);
			tdata.dataDevice.EnFlight = (byte)(cBox_Flight.Checked ? 1u : 0u);
			tdata.dataDevice.EnBT = (byte)(cBox_Bluet.Checked ? 1u : 0u);
			tdata.dataDevice.EnFalldn = (byte)(cBox_Falldown.Checked ? 1u : 0u);
			tdata.dataDevice.EnNoise = (byte)(cBox_Noise.Checked ? 1u : 0u);
			tdata.dataDevice.EnZone = (byte)(cBox_Zone.Checked ? 1u : 0u);
			tdata.dataDevice.Dis200m = (byte)(cBox_M200.Checked ? 1u : 0u);
			tdata.dataDevice.Dis350m = (byte)(cBox_M350.Checked ? 1u : 0u);
			tdata.dataDevice.Dis500m = (byte)(cBox_M500.Checked ? 1u : 0u);
			tdata.dataDevice.PkeyCnt = (byte)cBox_Pkey.SelectedIndex;
			base.DialogResult = DialogResult.OK;
		}
	}

	private void btn_Cancel_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
	}

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPS_5RH.Frm_EmbedInfo));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.checkBox10 = new System.Windows.Forms.CheckBox();
		this.checkBox9 = new System.Windows.Forms.CheckBox();
		this.checkBox5 = new System.Windows.Forms.CheckBox();
		this.checkBox6 = new System.Windows.Forms.CheckBox();
		this.checkBox7 = new System.Windows.Forms.CheckBox();
		this.checkBox8 = new System.Windows.Forms.CheckBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.checkBox11 = new System.Windows.Forms.CheckBox();
		this.checkBox4 = new System.Windows.Forms.CheckBox();
		this.checkBox3 = new System.Windows.Forms.CheckBox();
		this.checkBox2 = new System.Windows.Forms.CheckBox();
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.groupBox7 = new System.Windows.Forms.GroupBox();
		this.lbl_PkeyCnt = new System.Windows.Forms.Label();
		this.cBox_Pkey = new System.Windows.Forms.ComboBox();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.label9 = new System.Windows.Forms.Label();
		this.cBox_Zone = new System.Windows.Forms.CheckBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.cBox_Noise = new System.Windows.Forms.CheckBox();
		this.cBox_Falldown = new System.Windows.Forms.CheckBox();
		this.cBox_Bluet = new System.Windows.Forms.CheckBox();
		this.cBox_Flight = new System.Windows.Forms.CheckBox();
		this.cBox_Gps = new System.Windows.Forms.CheckBox();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.lbl_2Tone = new System.Windows.Forms.Label();
		this.cBox_DTMF = new System.Windows.Forms.CheckBox();
		this.cBox_MDC = new System.Windows.Forms.CheckBox();
		this.cBox_FiveTone = new System.Windows.Forms.CheckBox();
		this.cBox_TwoTone = new System.Windows.Forms.CheckBox();
		this.btn_ok = new System.Windows.Forms.Button();
		this.btn_Cancel = new System.Windows.Forms.Button();
		this.groupBox8 = new System.Windows.Forms.GroupBox();
		this.checkBox12 = new System.Windows.Forms.CheckBox();
		this.checkBox13 = new System.Windows.Forms.CheckBox();
		this.checkBox14 = new System.Windows.Forms.CheckBox();
		this.groupBox9 = new System.Windows.Forms.GroupBox();
		this.cBox_M350 = new System.Windows.Forms.CheckBox();
		this.cBox_M500 = new System.Windows.Forms.CheckBox();
		this.cBox_M200 = new System.Windows.Forms.CheckBox();
		this.label10 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.groupBox1.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		this.groupBox4.SuspendLayout();
		this.groupBox7.SuspendLayout();
		this.groupBox5.SuspendLayout();
		this.groupBox6.SuspendLayout();
		this.groupBox8.SuspendLayout();
		this.groupBox9.SuspendLayout();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.groupBox8);
		this.groupBox1.Controls.Add(this.groupBox3);
		this.groupBox1.Controls.Add(this.groupBox2);
		this.groupBox1.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.groupBox1.Location = new System.Drawing.Point(25, 25);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(347, 285);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "对讲机具有功能";
		this.groupBox3.Controls.Add(this.checkBox10);
		this.groupBox3.Controls.Add(this.checkBox9);
		this.groupBox3.Controls.Add(this.checkBox5);
		this.groupBox3.Controls.Add(this.checkBox6);
		this.groupBox3.Controls.Add(this.checkBox7);
		this.groupBox3.Controls.Add(this.checkBox8);
		this.groupBox3.Location = new System.Drawing.Point(18, 110);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(311, 81);
		this.groupBox3.TabIndex = 1;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "其他";
		this.checkBox10.AutoSize = true;
		this.checkBox10.Checked = true;
		this.checkBox10.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox10.Enabled = false;
		this.checkBox10.Location = new System.Drawing.Point(187, 49);
		this.checkBox10.Name = "checkBox10";
		this.checkBox10.Size = new System.Drawing.Size(51, 21);
		this.checkBox10.TabIndex = 5;
		this.checkBox10.Text = "区域";
		this.checkBox10.UseVisualStyleBackColor = true;
		this.checkBox9.AutoSize = true;
		this.checkBox9.Checked = true;
		this.checkBox9.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox9.Enabled = false;
		this.checkBox9.Location = new System.Drawing.Point(121, 49);
		this.checkBox9.Name = "checkBox9";
		this.checkBox9.Size = new System.Drawing.Size(51, 21);
		this.checkBox9.TabIndex = 4;
		this.checkBox9.Text = "降噪";
		this.checkBox9.UseVisualStyleBackColor = true;
		this.checkBox5.AutoSize = true;
		this.checkBox5.Checked = true;
		this.checkBox5.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox5.Enabled = false;
		this.checkBox5.Location = new System.Drawing.Point(31, 49);
		this.checkBox5.Name = "checkBox5";
		this.checkBox5.Size = new System.Drawing.Size(75, 21);
		this.checkBox5.TabIndex = 3;
		this.checkBox5.Text = "倒地告警";
		this.checkBox5.UseVisualStyleBackColor = true;
		this.checkBox6.AutoSize = true;
		this.checkBox6.Checked = true;
		this.checkBox6.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox6.Enabled = false;
		this.checkBox6.Location = new System.Drawing.Point(121, 22);
		this.checkBox6.Name = "checkBox6";
		this.checkBox6.Size = new System.Drawing.Size(51, 21);
		this.checkBox6.TabIndex = 2;
		this.checkBox6.Text = "蓝牙";
		this.checkBox6.UseVisualStyleBackColor = true;
		this.checkBox7.AutoSize = true;
		this.checkBox7.Checked = true;
		this.checkBox7.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox7.Enabled = false;
		this.checkBox7.Location = new System.Drawing.Point(187, 22);
		this.checkBox7.Name = "checkBox7";
		this.checkBox7.Size = new System.Drawing.Size(63, 21);
		this.checkBox7.TabIndex = 1;
		this.checkBox7.Text = "手电筒";
		this.checkBox7.UseVisualStyleBackColor = true;
		this.checkBox8.AutoSize = true;
		this.checkBox8.Checked = true;
		this.checkBox8.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox8.Enabled = false;
		this.checkBox8.Location = new System.Drawing.Point(32, 22);
		this.checkBox8.Name = "checkBox8";
		this.checkBox8.Size = new System.Drawing.Size(50, 21);
		this.checkBox8.TabIndex = 0;
		this.checkBox8.Text = "GPS";
		this.checkBox8.UseVisualStyleBackColor = true;
		this.groupBox2.Controls.Add(this.checkBox11);
		this.groupBox2.Controls.Add(this.checkBox4);
		this.groupBox2.Controls.Add(this.checkBox3);
		this.groupBox2.Controls.Add(this.checkBox2);
		this.groupBox2.Controls.Add(this.checkBox1);
		this.groupBox2.Location = new System.Drawing.Point(18, 23);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(311, 81);
		this.groupBox2.TabIndex = 0;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "信令类型";
		this.checkBox11.AutoSize = true;
		this.checkBox11.Checked = true;
		this.checkBox11.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox11.Enabled = false;
		this.checkBox11.Location = new System.Drawing.Point(211, 49);
		this.checkBox11.Name = "checkBox11";
		this.checkBox11.Size = new System.Drawing.Size(80, 21);
		this.checkBox11.TabIndex = 4;
		this.checkBox11.Text = "BDC1200";
		this.checkBox11.UseVisualStyleBackColor = true;
		this.checkBox4.AutoSize = true;
		this.checkBox4.Checked = true;
		this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox4.Enabled = false;
		this.checkBox4.Location = new System.Drawing.Point(31, 49);
		this.checkBox4.Name = "checkBox4";
		this.checkBox4.Size = new System.Drawing.Size(61, 21);
		this.checkBox4.TabIndex = 3;
		this.checkBox4.Text = "DTMF";
		this.checkBox4.UseVisualStyleBackColor = true;
		this.checkBox3.AutoSize = true;
		this.checkBox3.Checked = true;
		this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox3.Enabled = false;
		this.checkBox3.Location = new System.Drawing.Point(121, 49);
		this.checkBox3.Name = "checkBox3";
		this.checkBox3.Size = new System.Drawing.Size(84, 21);
		this.checkBox3.TabIndex = 2;
		this.checkBox3.Text = "MDC1200";
		this.checkBox3.UseVisualStyleBackColor = true;
		this.checkBox2.AutoSize = true;
		this.checkBox2.Checked = true;
		this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox2.Enabled = false;
		this.checkBox2.Location = new System.Drawing.Point(121, 22);
		this.checkBox2.Name = "checkBox2";
		this.checkBox2.Size = new System.Drawing.Size(51, 21);
		this.checkBox2.TabIndex = 1;
		this.checkBox2.Text = "五音";
		this.checkBox2.UseVisualStyleBackColor = true;
		this.checkBox1.AutoSize = true;
		this.checkBox1.Checked = true;
		this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox1.Enabled = false;
		this.checkBox1.Location = new System.Drawing.Point(32, 22);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(51, 21);
		this.checkBox1.TabIndex = 0;
		this.checkBox1.Text = "两音";
		this.checkBox1.UseVisualStyleBackColor = true;
		this.groupBox4.Controls.Add(this.groupBox9);
		this.groupBox4.Controls.Add(this.groupBox7);
		this.groupBox4.Controls.Add(this.groupBox5);
		this.groupBox4.Controls.Add(this.groupBox6);
		this.groupBox4.Font = new System.Drawing.Font("微软雅黑", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		this.groupBox4.Location = new System.Drawing.Point(404, 25);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(347, 338);
		this.groupBox4.TabIndex = 1;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "功能开启状态";
		this.groupBox7.Controls.Add(this.lbl_PkeyCnt);
		this.groupBox7.Controls.Add(this.cBox_Pkey);
		this.groupBox7.Location = new System.Drawing.Point(18, 260);
		this.groupBox7.Name = "groupBox7";
		this.groupBox7.Size = new System.Drawing.Size(311, 55);
		this.groupBox7.TabIndex = 4;
		this.groupBox7.TabStop = false;
		this.lbl_PkeyCnt.Location = new System.Drawing.Point(33, 21);
		this.lbl_PkeyCnt.Name = "lbl_PkeyCnt";
		this.lbl_PkeyCnt.Size = new System.Drawing.Size(175, 21);
		this.lbl_PkeyCnt.TabIndex = 3;
		this.lbl_PkeyCnt.Text = "可编程键个数:";
		this.lbl_PkeyCnt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.cBox_Pkey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cBox_Pkey.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cBox_Pkey.FormattingEnabled = true;
		this.cBox_Pkey.Items.AddRange(new object[4] { "1", "2", "3", "4" });
		this.cBox_Pkey.Location = new System.Drawing.Point(212, 19);
		this.cBox_Pkey.Name = "cBox_Pkey";
		this.cBox_Pkey.Size = new System.Drawing.Size(63, 25);
		this.cBox_Pkey.TabIndex = 2;
		this.groupBox5.Controls.Add(this.label9);
		this.groupBox5.Controls.Add(this.cBox_Zone);
		this.groupBox5.Controls.Add(this.label8);
		this.groupBox5.Controls.Add(this.label7);
		this.groupBox5.Controls.Add(this.label6);
		this.groupBox5.Controls.Add(this.label5);
		this.groupBox5.Controls.Add(this.label4);
		this.groupBox5.Controls.Add(this.cBox_Noise);
		this.groupBox5.Controls.Add(this.cBox_Falldown);
		this.groupBox5.Controls.Add(this.cBox_Bluet);
		this.groupBox5.Controls.Add(this.cBox_Flight);
		this.groupBox5.Controls.Add(this.cBox_Gps);
		this.groupBox5.Location = new System.Drawing.Point(18, 110);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(311, 81);
		this.groupBox5.TabIndex = 1;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "其他";
		this.label9.Location = new System.Drawing.Point(236, 47);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(57, 18);
		this.label9.TabIndex = 12;
		this.label9.Text = "区域";
		this.cBox_Zone.AutoSize = true;
		this.cBox_Zone.Location = new System.Drawing.Point(219, 49);
		this.cBox_Zone.Name = "cBox_Zone";
		this.cBox_Zone.Size = new System.Drawing.Size(15, 14);
		this.cBox_Zone.TabIndex = 11;
		this.cBox_Zone.UseVisualStyleBackColor = true;
		this.label8.Location = new System.Drawing.Point(48, 47);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(57, 18);
		this.label8.TabIndex = 10;
		this.label8.Text = "倒地告警";
		this.label7.Location = new System.Drawing.Point(138, 47);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(57, 18);
		this.label7.TabIndex = 9;
		this.label7.Text = "降噪";
		this.label6.Location = new System.Drawing.Point(138, 20);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(57, 18);
		this.label6.TabIndex = 8;
		this.label6.Text = "蓝牙";
		this.label5.Location = new System.Drawing.Point(236, 21);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(57, 18);
		this.label5.TabIndex = 7;
		this.label5.Text = "手电筒";
		this.label4.Location = new System.Drawing.Point(49, 19);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(57, 18);
		this.label4.TabIndex = 6;
		this.label4.Text = "GPS";
		this.cBox_Noise.AutoSize = true;
		this.cBox_Noise.Location = new System.Drawing.Point(121, 49);
		this.cBox_Noise.Name = "cBox_Noise";
		this.cBox_Noise.Size = new System.Drawing.Size(15, 14);
		this.cBox_Noise.TabIndex = 4;
		this.cBox_Noise.UseVisualStyleBackColor = true;
		this.cBox_Falldown.AutoSize = true;
		this.cBox_Falldown.Location = new System.Drawing.Point(31, 49);
		this.cBox_Falldown.Name = "cBox_Falldown";
		this.cBox_Falldown.Size = new System.Drawing.Size(15, 14);
		this.cBox_Falldown.TabIndex = 3;
		this.cBox_Falldown.UseVisualStyleBackColor = true;
		this.cBox_Bluet.AutoSize = true;
		this.cBox_Bluet.Location = new System.Drawing.Point(121, 23);
		this.cBox_Bluet.Name = "cBox_Bluet";
		this.cBox_Bluet.Size = new System.Drawing.Size(15, 14);
		this.cBox_Bluet.TabIndex = 2;
		this.cBox_Bluet.UseVisualStyleBackColor = true;
		this.cBox_Flight.AutoSize = true;
		this.cBox_Flight.Location = new System.Drawing.Point(219, 24);
		this.cBox_Flight.Name = "cBox_Flight";
		this.cBox_Flight.Size = new System.Drawing.Size(15, 14);
		this.cBox_Flight.TabIndex = 1;
		this.cBox_Flight.UseVisualStyleBackColor = true;
		this.cBox_Gps.AutoSize = true;
		this.cBox_Gps.Location = new System.Drawing.Point(32, 22);
		this.cBox_Gps.Name = "cBox_Gps";
		this.cBox_Gps.Size = new System.Drawing.Size(15, 14);
		this.cBox_Gps.TabIndex = 0;
		this.cBox_Gps.UseVisualStyleBackColor = true;
		this.groupBox6.Controls.Add(this.label3);
		this.groupBox6.Controls.Add(this.label2);
		this.groupBox6.Controls.Add(this.label1);
		this.groupBox6.Controls.Add(this.lbl_2Tone);
		this.groupBox6.Controls.Add(this.cBox_DTMF);
		this.groupBox6.Controls.Add(this.cBox_MDC);
		this.groupBox6.Controls.Add(this.cBox_FiveTone);
		this.groupBox6.Controls.Add(this.cBox_TwoTone);
		this.groupBox6.Location = new System.Drawing.Point(18, 23);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(311, 81);
		this.groupBox6.TabIndex = 0;
		this.groupBox6.TabStop = false;
		this.groupBox6.Text = "信令类型";
		this.label3.Location = new System.Drawing.Point(139, 47);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(87, 18);
		this.label3.TabIndex = 6;
		this.label3.Text = "MDC1200";
		this.label2.Location = new System.Drawing.Point(49, 47);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(57, 18);
		this.label2.TabIndex = 5;
		this.label2.Text = "DTMF";
		this.label1.Location = new System.Drawing.Point(139, 20);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(57, 18);
		this.label1.TabIndex = 4;
		this.label1.Text = "五音";
		this.lbl_2Tone.Location = new System.Drawing.Point(49, 20);
		this.lbl_2Tone.Name = "lbl_2Tone";
		this.lbl_2Tone.Size = new System.Drawing.Size(57, 18);
		this.lbl_2Tone.TabIndex = 2;
		this.lbl_2Tone.Text = "两音";
		this.cBox_DTMF.AutoSize = true;
		this.cBox_DTMF.Location = new System.Drawing.Point(31, 49);
		this.cBox_DTMF.Name = "cBox_DTMF";
		this.cBox_DTMF.Size = new System.Drawing.Size(15, 14);
		this.cBox_DTMF.TabIndex = 3;
		this.cBox_DTMF.UseVisualStyleBackColor = true;
		this.cBox_MDC.AutoSize = true;
		this.cBox_MDC.Location = new System.Drawing.Point(121, 49);
		this.cBox_MDC.Name = "cBox_MDC";
		this.cBox_MDC.Size = new System.Drawing.Size(15, 14);
		this.cBox_MDC.TabIndex = 2;
		this.cBox_MDC.UseVisualStyleBackColor = true;
		this.cBox_FiveTone.AutoSize = true;
		this.cBox_FiveTone.Location = new System.Drawing.Point(121, 22);
		this.cBox_FiveTone.Name = "cBox_FiveTone";
		this.cBox_FiveTone.Size = new System.Drawing.Size(15, 14);
		this.cBox_FiveTone.TabIndex = 1;
		this.cBox_FiveTone.UseVisualStyleBackColor = true;
		this.cBox_TwoTone.AutoSize = true;
		this.cBox_TwoTone.Location = new System.Drawing.Point(32, 22);
		this.cBox_TwoTone.Name = "cBox_TwoTone";
		this.cBox_TwoTone.Size = new System.Drawing.Size(15, 14);
		this.cBox_TwoTone.TabIndex = 0;
		this.cBox_TwoTone.UseVisualStyleBackColor = true;
		this.btn_ok.Location = new System.Drawing.Point(275, 393);
		this.btn_ok.Name = "btn_ok";
		this.btn_ok.Size = new System.Drawing.Size(75, 23);
		this.btn_ok.TabIndex = 2;
		this.btn_ok.Text = "确定";
		this.btn_ok.UseVisualStyleBackColor = true;
		this.btn_ok.Click += new System.EventHandler(btn_ok_Click);
		this.btn_Cancel.Location = new System.Drawing.Point(393, 393);
		this.btn_Cancel.Name = "btn_Cancel";
		this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
		this.btn_Cancel.TabIndex = 3;
		this.btn_Cancel.Text = "取消";
		this.btn_Cancel.UseVisualStyleBackColor = true;
		this.btn_Cancel.Click += new System.EventHandler(btn_Cancel_Click);
		this.groupBox8.Controls.Add(this.checkBox12);
		this.groupBox8.Controls.Add(this.checkBox13);
		this.groupBox8.Controls.Add(this.checkBox14);
		this.groupBox8.Location = new System.Drawing.Point(18, 199);
		this.groupBox8.Name = "groupBox8";
		this.groupBox8.Size = new System.Drawing.Size(311, 55);
		this.groupBox8.TabIndex = 2;
		this.groupBox8.TabStop = false;
		this.groupBox8.Text = "发射允许";
		this.checkBox12.AutoSize = true;
		this.checkBox12.Checked = true;
		this.checkBox12.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox12.Enabled = false;
		this.checkBox12.Location = new System.Drawing.Point(121, 22);
		this.checkBox12.Name = "checkBox12";
		this.checkBox12.Size = new System.Drawing.Size(75, 21);
		this.checkBox12.TabIndex = 5;
		this.checkBox12.Text = "350MHz";
		this.checkBox12.UseVisualStyleBackColor = true;
		this.checkBox13.AutoSize = true;
		this.checkBox13.Checked = true;
		this.checkBox13.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox13.Enabled = false;
		this.checkBox13.Location = new System.Drawing.Point(211, 22);
		this.checkBox13.Name = "checkBox13";
		this.checkBox13.Size = new System.Drawing.Size(75, 21);
		this.checkBox13.TabIndex = 4;
		this.checkBox13.Text = "500MHz";
		this.checkBox13.UseVisualStyleBackColor = true;
		this.checkBox14.AutoSize = true;
		this.checkBox14.Checked = true;
		this.checkBox14.CheckState = System.Windows.Forms.CheckState.Checked;
		this.checkBox14.Enabled = false;
		this.checkBox14.Location = new System.Drawing.Point(31, 22);
		this.checkBox14.Name = "checkBox14";
		this.checkBox14.Size = new System.Drawing.Size(75, 21);
		this.checkBox14.TabIndex = 3;
		this.checkBox14.Text = "200MHz";
		this.checkBox14.UseVisualStyleBackColor = true;
		this.groupBox9.Controls.Add(this.label12);
		this.groupBox9.Controls.Add(this.label11);
		this.groupBox9.Controls.Add(this.label10);
		this.groupBox9.Controls.Add(this.cBox_M350);
		this.groupBox9.Controls.Add(this.cBox_M500);
		this.groupBox9.Controls.Add(this.cBox_M200);
		this.groupBox9.Location = new System.Drawing.Point(18, 199);
		this.groupBox9.Name = "groupBox9";
		this.groupBox9.Size = new System.Drawing.Size(311, 55);
		this.groupBox9.TabIndex = 5;
		this.groupBox9.TabStop = false;
		this.groupBox9.Text = "发射允许";
		this.cBox_M350.AutoSize = true;
		this.cBox_M350.Checked = true;
		this.cBox_M350.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cBox_M350.Location = new System.Drawing.Point(122, 25);
		this.cBox_M350.Name = "cBox_M350";
		this.cBox_M350.Size = new System.Drawing.Size(15, 14);
		this.cBox_M350.TabIndex = 5;
		this.cBox_M350.UseVisualStyleBackColor = true;
		this.cBox_M500.AutoSize = true;
		this.cBox_M500.Checked = true;
		this.cBox_M500.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cBox_M500.Location = new System.Drawing.Point(211, 25);
		this.cBox_M500.Name = "cBox_M500";
		this.cBox_M500.Size = new System.Drawing.Size(15, 14);
		this.cBox_M500.TabIndex = 4;
		this.cBox_M500.UseVisualStyleBackColor = true;
		this.cBox_M200.AutoSize = true;
		this.cBox_M200.Checked = true;
		this.cBox_M200.CheckState = System.Windows.Forms.CheckState.Checked;
		this.cBox_M200.Location = new System.Drawing.Point(31, 25);
		this.cBox_M200.Name = "cBox_M200";
		this.cBox_M200.Size = new System.Drawing.Size(15, 14);
		this.cBox_M200.TabIndex = 3;
		this.cBox_M200.UseVisualStyleBackColor = true;
		this.label10.Location = new System.Drawing.Point(48, 23);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(57, 18);
		this.label10.TabIndex = 7;
		this.label10.Text = "200MHz";
		this.label11.Location = new System.Drawing.Point(138, 23);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(57, 18);
		this.label11.TabIndex = 8;
		this.label11.Text = "350MHz";
		this.label12.Location = new System.Drawing.Point(229, 23);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(57, 18);
		this.label12.TabIndex = 9;
		this.label12.Text = "500MHz";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(763, 437);
		base.Controls.Add(this.btn_Cancel);
		base.Controls.Add(this.btn_ok);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.groupBox1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "Frm_EmbedInfo";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "嵌入信息";
		base.Load += new System.EventHandler(Frm_EmbedInfo_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox7.ResumeLayout(false);
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		this.groupBox6.ResumeLayout(false);
		this.groupBox6.PerformLayout();
		this.groupBox8.ResumeLayout(false);
		this.groupBox8.PerformLayout();
		this.groupBox9.ResumeLayout(false);
		this.groupBox9.PerformLayout();
		base.ResumeLayout(false);
	}
}
