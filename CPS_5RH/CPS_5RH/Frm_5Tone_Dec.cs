using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CPS_5RH.Data;

namespace CPS_5RH;

public class Frm_5Tone_Dec : Form
{
	private DatFiveToneDecInfo tdata = null;

	private DatFiveToneEncInfo edata = null;

	private DataGridViewTextBoxEditingControl dgvTxt = null;

	private IContainer components = null;

	private Label label7;

	private TextBox tb_Did;

	private Label label4;

	private ComboBox coBox_DecRsp;

	private CheckBox ckBox_Sw;

	private Label lbl6;

	private Label label1;

	private Label label2;

	private ComboBox coBox_DecStand;

	private Label label5;

	private ComboBox coBox_DecToneTim;

	private Label label8;

	private NumericUpDown num_PreTim;

	private Label label9;

	private NumericUpDown num_CodeDly;

	private ComboBox coBox_PttPause;

	private Label label10;

	private NumericUpDown num_FirstCodeTim;

	private Label label11;

	private Label label12;

	private ComboBox coBox_StopCode;

	private Label label13;

	private NumericUpDown Num_StopTim;

	private NumericUpDown Num_DecCodeTim;

	private ComboBox Cobox_ResetTim;

	private GroupBox grp1;

	public Frm_5Tone_Dec(DatFiveToneDecInfo data, DatFiveToneEncInfo encdata)
	{
		InitializeComponent();
		tdata = data;
		edata = encdata;
	}

	private void Frm_Encrypt_Load(object sender, EventArgs e)
	{
		string[] strItems = new string[4];
		string tmp = "";
		if (FormMain.lang == "Chinese")
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
		}
		LanguageSel.LoadLanguage(this, typeof(Frm_5Tone_Dec));
		for (int i = 0; i < 4; i++)
		{
			LanguageSel.ElseCombobox.TryGetValue(i.ToString(), out tmp);
			strItems[i] = tmp;
		}
		ComponentResourceManager crm = new ComponentResourceManager(typeof(Frm_5Tone_Dec));
		coBox_DecRsp.Items[0] = strItems[0];
		coBox_DecRsp.Items[1] = strItems[2];
		coBox_DecRsp.Items[2] = strItems[3];
		coBox_PttPause.Items[0] = strItems[1];
		coBox_StopCode.Items[0] = strItems[0];
		tb_Did.LostFocus -= tb_Did_LostFocus;
		tb_Did.LostFocus += tb_Did_LostFocus;
	}

	private string Adjust_GetStrVal(string val)
	{
		ASCIIEncoding code = new ASCIIEncoding();
		byte[] tmB = code.GetBytes(val);
		if (tmB.Length > 1)
		{
			for (int i = 0; i < tmB.Length - 1; i++)
			{
				if (tmB[i] == tmB[i + 1])
				{
					tmB[i + 1] = 42;
				}
			}
		}
		return code.GetString(tmB, 0, tmB.Length);
	}

	private void UpdateEncInfo_PTTID(string ecode)
	{
		string tmpcode = "";
		for (int i = 0; i < DatFiveToneEncInfo.Enc_List_Num; i++)
		{
			if (edata.GetEncScall(i) == 3 && edata.GetEncCodeLen(i) == 5)
			{
				edata.SetEncCodeLen(i, (byte)ecode.Length);
				edata.SetEncID(i, ecode);
			}
			else if (edata.GetEncScall(i) == 2 && edata.GetEncCodeLen(i) == 11)
			{
				tmpcode = edata.GetEncID(i).Substring(0, 6) + ecode;
				edata.SetEncCodeLen(i, (byte)tmpcode.Length);
				edata.SetEncID(i, tmpcode);
			}
		}
	}

	private void tb_Did_LostFocus(object sender, EventArgs e)
	{
		string idval = tb_Did.Text;
		if (idval == "")
		{
			tb_Did.Text = tdata.Did;
			return;
		}
		for (int i = idval.Length; i < 5; i++)
		{
			idval = idval.Insert(0, "0");
		}
		idval = Adjust_GetStrVal(idval);
		tdata.Did = idval;
		tb_Did.Text = tdata.Did;
		UpdateEncInfo_PTTID(idval);
	}

	private void BingData()
	{
		ckBox_Sw.DataBindings.Clear();
		num_PreTim.DataBindings.Clear();
		num_CodeDly.DataBindings.Clear();
		Cobox_ResetTim.DataBindings.Clear();
		num_FirstCodeTim.DataBindings.Clear();
		Num_StopTim.DataBindings.Clear();
		Num_DecCodeTim.DataBindings.Clear();
		coBox_DecRsp.DataBindings.Clear();
		coBox_DecStand.DataBindings.Clear();
		coBox_DecToneTim.DataBindings.Clear();
		coBox_PttPause.DataBindings.Clear();
		coBox_StopCode.DataBindings.Clear();
		if (tdata.DecRsp > 2)
		{
			tdata.DecRsp = 0;
		}
		if (tdata.DecStand > 13)
		{
			tdata.DecStand = 0;
		}
		if (tdata.DecToneTim > 5)
		{
			tdata.DecToneTim = 0;
		}
		if (tdata.PttIDPause > 75)
		{
			tdata.PttIDPause = 0;
		}
		if (tdata.StopCode > 4)
		{
			tdata.StopCode = 0;
		}
		if (tdata.ResetTime > 15)
		{
			tdata.ResetTime = 0;
		}
		if (tdata.DecCodetime > 2000)
		{
			tdata.DecCodetime = 0;
		}
		if (tdata.FirstCodeTim < 70)
		{
			tdata.FirstCodeTim = 70;
		}
		if (tdata.StopCodetime == 0)
		{
			tdata.StopCodetime = 10;
		}
		ckBox_Sw.DataBindings.Add("Checked", tdata, "FiveAni", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		num_PreTim.DataBindings.Add("Value", tdata, "PreTime", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		num_CodeDly.DataBindings.Add("Value", tdata, "CodeDly", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		Cobox_ResetTim.DataBindings.Add("SelectedIndex", tdata, "ResetTime", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		num_FirstCodeTim.DataBindings.Add("Value", tdata, "FirstCodeTim", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		Num_StopTim.DataBindings.Add("Value", tdata, "StopCodetime", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		Num_DecCodeTim.DataBindings.Add("Value", tdata, "DecCodetime", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_DecRsp.DataBindings.Add("SelectedIndex", tdata, "DecRsp", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_DecStand.DataBindings.Add("SelectedIndex", tdata, "DecStand", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_DecToneTim.DataBindings.Add("SelectedIndex", tdata, "DecToneTim", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_PttPause.DataBindings.Add("SelectedIndex", tdata, "PttIDPause", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_StopCode.DataBindings.Add("SelectedIndex", tdata, "StopCode", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
	}

	private void Cells_KeyPress(object sender, KeyPressEventArgs e)
	{
		if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar < 'a' || e.KeyChar > 'f') && (e.KeyChar < 'A' || e.KeyChar > 'F') && e.KeyChar != '\b' && e.KeyChar != '*' && e.KeyChar != '#')
		{
			e.Handled = true;
		}
		else if (e.KeyChar >= 'a' && e.KeyChar <= 'd')
		{
			e.KeyChar -= ' ';
		}
		else if (e.KeyChar == 'E' || e.KeyChar == 'e')
		{
			e.KeyChar = '*';
		}
		else if (e.KeyChar == 'F' || e.KeyChar == 'f')
		{
			e.KeyChar = '#';
		}
	}

	private void Cells_IDKeyPress(object sender, KeyPressEventArgs e)
	{
		if (char.IsNumber(e.KeyChar) || e.KeyChar == '\b')
		{
			e.Handled = false;
		}
		else
		{
			e.Handled = true;
		}
	}

	public void DispEncryptData()
	{
		BingData();
		tb_Did.Text = tdata.Did;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPS_5RH.Frm_5Tone_Dec));
		this.label7 = new System.Windows.Forms.Label();
		this.tb_Did = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.coBox_DecRsp = new System.Windows.Forms.ComboBox();
		this.ckBox_Sw = new System.Windows.Forms.CheckBox();
		this.lbl6 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.coBox_DecStand = new System.Windows.Forms.ComboBox();
		this.label5 = new System.Windows.Forms.Label();
		this.coBox_DecToneTim = new System.Windows.Forms.ComboBox();
		this.label8 = new System.Windows.Forms.Label();
		this.num_PreTim = new System.Windows.Forms.NumericUpDown();
		this.label9 = new System.Windows.Forms.Label();
		this.num_CodeDly = new System.Windows.Forms.NumericUpDown();
		this.coBox_PttPause = new System.Windows.Forms.ComboBox();
		this.label10 = new System.Windows.Forms.Label();
		this.num_FirstCodeTim = new System.Windows.Forms.NumericUpDown();
		this.label11 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.coBox_StopCode = new System.Windows.Forms.ComboBox();
		this.label13 = new System.Windows.Forms.Label();
		this.Num_StopTim = new System.Windows.Forms.NumericUpDown();
		this.Num_DecCodeTim = new System.Windows.Forms.NumericUpDown();
		this.Cobox_ResetTim = new System.Windows.Forms.ComboBox();
		this.grp1 = new System.Windows.Forms.GroupBox();
		((System.ComponentModel.ISupportInitialize)this.num_PreTim).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.num_CodeDly).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.num_FirstCodeTim).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Num_StopTim).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Num_DecCodeTim).BeginInit();
		this.grp1.SuspendLayout();
		base.SuspendLayout();
		this.label7.Location = new System.Drawing.Point(79, 109);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(100, 18);
		this.label7.TabIndex = 57;
		this.label7.Text = "自身ID码";
		this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tb_Did.Location = new System.Drawing.Point(185, 106);
		this.tb_Did.MaxLength = 5;
		this.tb_Did.Name = "tb_Did";
		this.tb_Did.Size = new System.Drawing.Size(99, 21);
		this.tb_Did.TabIndex = 56;
		this.tb_Did.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Cells_IDKeyPress);
		this.label4.Location = new System.Drawing.Point(79, 30);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(100, 18);
		this.label4.TabIndex = 55;
		this.label4.Text = "解码响应";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_DecRsp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_DecRsp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_DecRsp.FormattingEnabled = true;
		this.coBox_DecRsp.Items.AddRange(new object[3] { "无", "提示音", "提示音且回复" });
		this.coBox_DecRsp.Location = new System.Drawing.Point(185, 28);
		this.coBox_DecRsp.Name = "coBox_DecRsp";
		this.coBox_DecRsp.Size = new System.Drawing.Size(99, 20);
		this.coBox_DecRsp.TabIndex = 54;
		this.ckBox_Sw.Location = new System.Drawing.Point(350, 28);
		this.ckBox_Sw.Name = "ckBox_Sw";
		this.ckBox_Sw.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.ckBox_Sw.Size = new System.Drawing.Size(109, 18);
		this.ckBox_Sw.TabIndex = 51;
		this.ckBox_Sw.Text = "侧音";
		this.ckBox_Sw.UseVisualStyleBackColor = true;
		this.lbl6.Location = new System.Drawing.Point(57, 216);
		this.lbl6.Name = "lbl6";
		this.lbl6.Size = new System.Drawing.Size(122, 18);
		this.lbl6.TabIndex = 43;
		this.lbl6.Text = "自动复位时间(秒)";
		this.lbl6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label1.Location = new System.Drawing.Point(309, 108);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(126, 18);
		this.label1.TabIndex = 35;
		this.label1.Text = "解码时间[ms]";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label2.Location = new System.Drawing.Point(79, 56);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(100, 18);
		this.label2.TabIndex = 59;
		this.label2.Text = "解码标准";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_DecStand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_DecStand.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_DecStand.FormattingEnabled = true;
		this.coBox_DecStand.Items.AddRange(new object[14]
		{
			"ZVEI1", "ZVEI2", "ZVEI3", "PZVEI", "DZVEI", "PDZVEI", "CCIR1", "CCIR2", "PCCIR", "EEA",
			"EURO", "NATEL", "MODAT", "CCITT"
		});
		this.coBox_DecStand.Location = new System.Drawing.Point(185, 54);
		this.coBox_DecStand.Name = "coBox_DecStand";
		this.coBox_DecStand.Size = new System.Drawing.Size(99, 20);
		this.coBox_DecStand.TabIndex = 58;
		this.label5.Location = new System.Drawing.Point(79, 82);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(100, 18);
		this.label5.TabIndex = 61;
		this.label5.Text = "解码音长[ms]";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_DecToneTim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_DecToneTim.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_DecToneTim.FormattingEnabled = true;
		this.coBox_DecToneTim.Items.AddRange(new object[6] { "70", "80", "90", "100", "110", "120" });
		this.coBox_DecToneTim.Location = new System.Drawing.Point(185, 80);
		this.coBox_DecToneTim.Name = "coBox_DecToneTim";
		this.coBox_DecToneTim.Size = new System.Drawing.Size(99, 20);
		this.coBox_DecToneTim.TabIndex = 60;
		this.label8.Location = new System.Drawing.Point(9, 137);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(170, 18);
		this.label8.TabIndex = 63;
		this.label8.Text = "预载波时间(毫秒)";
		this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.num_PreTim.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.num_PreTim.Location = new System.Drawing.Point(185, 133);
		this.num_PreTim.Maximum = new decimal(new int[4] { 2550, 0, 0, 0 });
		this.num_PreTim.Minimum = new decimal(new int[4] { 10, 0, 0, 0 });
		this.num_PreTim.Name = "num_PreTim";
		this.num_PreTim.Size = new System.Drawing.Size(99, 21);
		this.num_PreTim.TabIndex = 62;
		this.num_PreTim.Value = new decimal(new int[4] { 30, 0, 0, 0 });
		this.label9.Location = new System.Drawing.Point(24, 163);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(155, 18);
		this.label9.TabIndex = 65;
		this.label9.Text = "发码后延迟时间(毫秒)";
		this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.num_CodeDly.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.num_CodeDly.Location = new System.Drawing.Point(185, 160);
		this.num_CodeDly.Maximum = new decimal(new int[4] { 2550, 0, 0, 0 });
		this.num_CodeDly.Minimum = new decimal(new int[4] { 10, 0, 0, 0 });
		this.num_CodeDly.Name = "num_CodeDly";
		this.num_CodeDly.Size = new System.Drawing.Size(99, 21);
		this.num_CodeDly.TabIndex = 64;
		this.num_CodeDly.Value = new decimal(new int[4] { 2550, 0, 0, 0 });
		this.coBox_PttPause.DropDownHeight = 120;
		this.coBox_PttPause.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_PttPause.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_PttPause.FormattingEnabled = true;
		this.coBox_PttPause.IntegralHeight = false;
		this.coBox_PttPause.Items.AddRange(new object[72]
		{
			"无", "5", "6", "7", "8", "9", "10", "11", "12", "13",
			"14", "15", "16", "17", "18", "19", "20", "21", "22", "23",
			"24", "25", "26", "27", "28", "29", "30", "31", "32", "33",
			"34", "35", "36", "37", "38", "39", "40", "41", "42", "43",
			"44", "45", "46", "47", "48", "49", "50", "51", "52", "53",
			"54", "55", "56", "57", "58", "59", "60", "61", "62", "63",
			"64", "65", "66", "67", "68", "69", "70", "71", "72", "73",
			"74", "75"
		});
		this.coBox_PttPause.Location = new System.Drawing.Point(185, 187);
		this.coBox_PttPause.Name = "coBox_PttPause";
		this.coBox_PttPause.Size = new System.Drawing.Size(99, 20);
		this.coBox_PttPause.TabIndex = 67;
		this.label10.Location = new System.Drawing.Point(26, 189);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(153, 18);
		this.label10.TabIndex = 66;
		this.label10.Text = "PTT ID暂停时间[s]";
		this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.num_FirstCodeTim.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.num_FirstCodeTim.Location = new System.Drawing.Point(185, 240);
		this.num_FirstCodeTim.Maximum = new decimal(new int[4] { 2550, 0, 0, 0 });
		this.num_FirstCodeTim.Minimum = new decimal(new int[4] { 70, 0, 0, 0 });
		this.num_FirstCodeTim.Name = "num_FirstCodeTim";
		this.num_FirstCodeTim.Size = new System.Drawing.Size(99, 21);
		this.num_FirstCodeTim.TabIndex = 68;
		this.num_FirstCodeTim.Value = new decimal(new int[4] { 100, 0, 0, 0 });
		this.label11.Location = new System.Drawing.Point(7, 242);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(172, 18);
		this.label11.TabIndex = 69;
		this.label11.Text = "首码数码时间(毫秒)";
		this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label12.Location = new System.Drawing.Point(335, 56);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(100, 18);
		this.label12.TabIndex = 71;
		this.label12.Text = "停顿码";
		this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_StopCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_StopCode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_StopCode.FormattingEnabled = true;
		this.coBox_StopCode.Items.AddRange(new object[5] { "无", "B", "C", "D", "F" });
		this.coBox_StopCode.Location = new System.Drawing.Point(441, 54);
		this.coBox_StopCode.Name = "coBox_StopCode";
		this.coBox_StopCode.Size = new System.Drawing.Size(99, 20);
		this.coBox_StopCode.TabIndex = 70;
		this.label13.Location = new System.Drawing.Point(307, 82);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(128, 18);
		this.label13.TabIndex = 73;
		this.label13.Text = "停顿时间[ms]";
		this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.Num_StopTim.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.Num_StopTim.Location = new System.Drawing.Point(441, 79);
		this.Num_StopTim.Maximum = new decimal(new int[4] { 2550, 0, 0, 0 });
		this.Num_StopTim.Minimum = new decimal(new int[4] { 10, 0, 0, 0 });
		this.Num_StopTim.Name = "Num_StopTim";
		this.Num_StopTim.Size = new System.Drawing.Size(99, 21);
		this.Num_StopTim.TabIndex = 74;
		this.Num_StopTim.Value = new decimal(new int[4] { 30, 0, 0, 0 });
		this.Num_DecCodeTim.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.Num_DecCodeTim.Location = new System.Drawing.Point(441, 106);
		this.Num_DecCodeTim.Maximum = new decimal(new int[4] { 2000, 0, 0, 0 });
		this.Num_DecCodeTim.Name = "Num_DecCodeTim";
		this.Num_DecCodeTim.Size = new System.Drawing.Size(99, 21);
		this.Num_DecCodeTim.TabIndex = 75;
		this.Num_DecCodeTim.Value = new decimal(new int[4] { 30, 0, 0, 0 });
		this.Cobox_ResetTim.DropDownHeight = 120;
		this.Cobox_ResetTim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.Cobox_ResetTim.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.Cobox_ResetTim.FormattingEnabled = true;
		this.Cobox_ResetTim.IntegralHeight = false;
		this.Cobox_ResetTim.Items.AddRange(new object[16]
		{
			"10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
			"20", "21", "22", "23", "24", "25"
		});
		this.Cobox_ResetTim.Location = new System.Drawing.Point(185, 214);
		this.Cobox_ResetTim.Name = "Cobox_ResetTim";
		this.Cobox_ResetTim.Size = new System.Drawing.Size(99, 20);
		this.Cobox_ResetTim.TabIndex = 76;
		this.grp1.Controls.Add(this.label4);
		this.grp1.Controls.Add(this.Cobox_ResetTim);
		this.grp1.Controls.Add(this.label1);
		this.grp1.Controls.Add(this.Num_DecCodeTim);
		this.grp1.Controls.Add(this.lbl6);
		this.grp1.Controls.Add(this.Num_StopTim);
		this.grp1.Controls.Add(this.ckBox_Sw);
		this.grp1.Controls.Add(this.label13);
		this.grp1.Controls.Add(this.coBox_DecRsp);
		this.grp1.Controls.Add(this.label12);
		this.grp1.Controls.Add(this.tb_Did);
		this.grp1.Controls.Add(this.coBox_StopCode);
		this.grp1.Controls.Add(this.label7);
		this.grp1.Controls.Add(this.num_FirstCodeTim);
		this.grp1.Controls.Add(this.coBox_DecStand);
		this.grp1.Controls.Add(this.label11);
		this.grp1.Controls.Add(this.label2);
		this.grp1.Controls.Add(this.coBox_PttPause);
		this.grp1.Controls.Add(this.coBox_DecToneTim);
		this.grp1.Controls.Add(this.label10);
		this.grp1.Controls.Add(this.label5);
		this.grp1.Controls.Add(this.label9);
		this.grp1.Controls.Add(this.num_PreTim);
		this.grp1.Controls.Add(this.num_CodeDly);
		this.grp1.Controls.Add(this.label8);
		this.grp1.Location = new System.Drawing.Point(26, 15);
		this.grp1.Name = "grp1";
		this.grp1.Size = new System.Drawing.Size(560, 352);
		this.grp1.TabIndex = 77;
		this.grp1.TabStop = false;
		this.grp1.Text = "五音解码";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoScroll = true;
		base.ClientSize = new System.Drawing.Size(610, 392);
		base.Controls.Add(this.grp1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Frm_5Tone_Dec";
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		base.Load += new System.EventHandler(Frm_Encrypt_Load);
		((System.ComponentModel.ISupportInitialize)this.num_PreTim).EndInit();
		((System.ComponentModel.ISupportInitialize)this.num_CodeDly).EndInit();
		((System.ComponentModel.ISupportInitialize)this.num_FirstCodeTim).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Num_StopTim).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Num_DecCodeTim).EndInit();
		this.grp1.ResumeLayout(false);
		this.grp1.PerformLayout();
		base.ResumeLayout(false);
	}
}
