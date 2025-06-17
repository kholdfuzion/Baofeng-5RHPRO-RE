using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using CPS_5RH.Data;

namespace CPS_5RH;

public class Frm_MDC_Para : Form
{
	private DataApp tdata = null;

	private static int posIdx = 0;

	private IContainer components = null;

	private CheckBox ckBox_DecTone;

	private TabControl Tab_MdcPara;

	private TabPage tabP1;

	private TabPage tabP2;

	private TabPage tabP3;

	private TabPage tabP4;

	private TabPage tabP5;

	private NumericUpDown num_EncSync;

	private Label label3;

	private NumericUpDown num_DecRst;

	private Label label6;

	private NumericUpDown num_SqlDly;

	private Label label5;

	private CheckBox ckBox_ReqTone;

	private CheckBox ckBox_Ctrl;

	private NumericUpDown num_PreTim;

	private Label label2;

	private Label label1;

	private Label label4;

	private Panel panel1;

	private TextBox tb_EncID;

	private ComboBox cobox_DecSync;

	private GroupBox grp1;

	public Frm_MDC_Para(DataApp data)
	{
		InitializeComponent();
		tdata = data;
	}

	public void MdcParaDataDisp(int pos)
	{
		posIdx = pos;
		bingDingTheControls(pos);
		tb_EncID.Text = tdata.dataMdcPara[posIdx].EncID.ToString("0000");
	}

	private void bingDingTheControls(int pos)
	{
		ckBox_Ctrl.DataBindings.Clear();
		ckBox_DecTone.DataBindings.Clear();
		num_PreTim.DataBindings.Clear();
		num_SqlDly.DataBindings.Clear();
		num_DecRst.DataBindings.Clear();
		num_EncSync.DataBindings.Clear();
		cobox_DecSync.DataBindings.Clear();
		if (tdata.dataMdcPara[pos].DecSync > 11)
		{
			tdata.dataMdcPara[pos].DecSync = 0;
		}
		ckBox_Ctrl.DataBindings.Add("Checked", tdata.dataMdcPara[pos], "CtrlSw", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		ckBox_DecTone.DataBindings.Add("Checked", tdata.dataMdcPara[pos], "DecTone", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		num_PreTim.DataBindings.Add("Value", tdata.dataMdcPara[pos], "PreTim", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		num_SqlDly.DataBindings.Add("Value", tdata.dataMdcPara[pos], "SqlDly", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		num_DecRst.DataBindings.Add("Value", tdata.dataMdcPara[pos], "DecRst", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		num_EncSync.DataBindings.Add("Value", tdata.dataMdcPara[pos], "EncSync", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		cobox_DecSync.DataBindings.Add("SelectedIndex", tdata.dataMdcPara[pos], "DecSync", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
	}

	private void Tab_MdcPara_SelectedIndexChanged(object sender, EventArgs e)
	{
		Tab_MdcPara.Controls.Remove(panel1);
		switch (Tab_MdcPara.SelectedIndex)
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
		}
		MdcParaDataDisp(Tab_MdcPara.SelectedIndex);
	}

	private void Frm_MDC_Para_Load(object sender, EventArgs e)
	{
		if (FormMain.lang == "Chinese")
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
		}
		LanguageSel.LoadLanguage(this, typeof(Frm_MDC_Para));
		ComponentResourceManager crm = new ComponentResourceManager(typeof(Frm_MDC_Para));
		tb_EncID.LostFocus -= tb_EncID_LostFocus;
		tb_EncID.LostFocus += tb_EncID_LostFocus;
	}

	private int Adjust_GetVal(string val)
	{
		int intFreq;
		try
		{
			intFreq = Convert.ToInt32(val);
			if (intFreq > 9999)
			{
				intFreq = 9999;
			}
		}
		catch
		{
			intFreq = 1;
		}
		return intFreq;
	}

	private void tb_EncID_LostFocus(object sender, EventArgs e)
	{
		string value = ((TextBox)sender).Text;
		tdata.dataMdcPara[posIdx].EncID = (ushort)Adjust_GetVal(value);
		tb_EncID.Text = tdata.dataMdcPara[posIdx].EncID.ToString("0000");
	}

	private void Freq_Cells_KeyPress(object sender, KeyPressEventArgs e)
	{
		string str = ((TextBox)sender).Text;
		e.Handled = e.KeyChar < '0' || e.KeyChar > '9';
		if (e.KeyChar == '\b')
		{
			e.Handled = false;
		}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPS_5RH.Frm_MDC_Para));
		this.ckBox_DecTone = new System.Windows.Forms.CheckBox();
		this.Tab_MdcPara = new System.Windows.Forms.TabControl();
		this.tabP1 = new System.Windows.Forms.TabPage();
		this.panel1 = new System.Windows.Forms.Panel();
		this.cobox_DecSync = new System.Windows.Forms.ComboBox();
		this.tb_EncID = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.num_EncSync = new System.Windows.Forms.NumericUpDown();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.num_PreTim = new System.Windows.Forms.NumericUpDown();
		this.num_DecRst = new System.Windows.Forms.NumericUpDown();
		this.ckBox_Ctrl = new System.Windows.Forms.CheckBox();
		this.label6 = new System.Windows.Forms.Label();
		this.ckBox_ReqTone = new System.Windows.Forms.CheckBox();
		this.num_SqlDly = new System.Windows.Forms.NumericUpDown();
		this.label5 = new System.Windows.Forms.Label();
		this.tabP2 = new System.Windows.Forms.TabPage();
		this.tabP3 = new System.Windows.Forms.TabPage();
		this.tabP4 = new System.Windows.Forms.TabPage();
		this.tabP5 = new System.Windows.Forms.TabPage();
		this.grp1 = new System.Windows.Forms.GroupBox();
		this.Tab_MdcPara.SuspendLayout();
		this.tabP1.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.num_EncSync).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.num_PreTim).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.num_DecRst).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.num_SqlDly).BeginInit();
		this.grp1.SuspendLayout();
		base.SuspendLayout();
		this.ckBox_DecTone.Location = new System.Drawing.Point(349, 46);
		this.ckBox_DecTone.Name = "ckBox_DecTone";
		this.ckBox_DecTone.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.ckBox_DecTone.Size = new System.Drawing.Size(97, 20);
		this.ckBox_DecTone.TabIndex = 1;
		this.ckBox_DecTone.Text = "MDC解码声";
		this.ckBox_DecTone.UseVisualStyleBackColor = true;
		this.ckBox_DecTone.Visible = false;
		this.Tab_MdcPara.Controls.Add(this.tabP1);
		this.Tab_MdcPara.Controls.Add(this.tabP2);
		this.Tab_MdcPara.Controls.Add(this.tabP3);
		this.Tab_MdcPara.Controls.Add(this.tabP4);
		this.Tab_MdcPara.Controls.Add(this.tabP5);
		this.Tab_MdcPara.Location = new System.Drawing.Point(19, 27);
		this.Tab_MdcPara.Name = "Tab_MdcPara";
		this.Tab_MdcPara.Padding = new System.Drawing.Point(10, 5);
		this.Tab_MdcPara.SelectedIndex = 0;
		this.Tab_MdcPara.Size = new System.Drawing.Size(477, 297);
		this.Tab_MdcPara.TabIndex = 18;
		this.Tab_MdcPara.SelectedIndexChanged += new System.EventHandler(Tab_MdcPara_SelectedIndexChanged);
		this.tabP1.BackColor = System.Drawing.Color.Transparent;
		this.tabP1.Controls.Add(this.panel1);
		this.tabP1.Location = new System.Drawing.Point(4, 26);
		this.tabP1.Name = "tabP1";
		this.tabP1.Size = new System.Drawing.Size(469, 267);
		this.tabP1.TabIndex = 0;
		this.tabP1.Text = "列表1";
		this.tabP1.UseVisualStyleBackColor = true;
		this.panel1.BackColor = System.Drawing.SystemColors.Control;
		this.panel1.Controls.Add(this.cobox_DecSync);
		this.panel1.Controls.Add(this.tb_EncID);
		this.panel1.Controls.Add(this.ckBox_DecTone);
		this.panel1.Controls.Add(this.label4);
		this.panel1.Controls.Add(this.label1);
		this.panel1.Controls.Add(this.num_EncSync);
		this.panel1.Controls.Add(this.label2);
		this.panel1.Controls.Add(this.label3);
		this.panel1.Controls.Add(this.num_PreTim);
		this.panel1.Controls.Add(this.num_DecRst);
		this.panel1.Controls.Add(this.ckBox_Ctrl);
		this.panel1.Controls.Add(this.label6);
		this.panel1.Controls.Add(this.ckBox_ReqTone);
		this.panel1.Controls.Add(this.num_SqlDly);
		this.panel1.Controls.Add(this.label5);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(469, 267);
		this.panel1.TabIndex = 48;
		this.cobox_DecSync.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobox_DecSync.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cobox_DecSync.FormattingEnabled = true;
		this.cobox_DecSync.Items.AddRange(new object[12]
		{
			"29", "30", "31", "32", "33", "34", "35", "36", "37", "38",
			"39", "40"
		});
		this.cobox_DecSync.Location = new System.Drawing.Point(238, 172);
		this.cobox_DecSync.Name = "cobox_DecSync";
		this.cobox_DecSync.Size = new System.Drawing.Size(64, 20);
		this.cobox_DecSync.TabIndex = 49;
		this.tb_EncID.Location = new System.Drawing.Point(238, 19);
		this.tb_EncID.MaxLength = 4;
		this.tb_EncID.Name = "tb_EncID";
		this.tb_EncID.Size = new System.Drawing.Size(64, 21);
		this.tb_EncID.TabIndex = 48;
		this.tb_EncID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Freq_Cells_KeyPress);
		this.label4.Location = new System.Drawing.Point(60, 172);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(172, 18);
		this.label4.TabIndex = 46;
		this.label4.Text = "解码同步头个数";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label1.Location = new System.Drawing.Point(60, 22);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(172, 18);
		this.label1.TabIndex = 28;
		this.label1.Text = "编码ID";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.num_EncSync.Location = new System.Drawing.Point(238, 142);
		this.num_EncSync.Maximum = new decimal(new int[4] { 255, 0, 0, 0 });
		this.num_EncSync.Name = "num_EncSync";
		this.num_EncSync.Size = new System.Drawing.Size(64, 21);
		this.num_EncSync.TabIndex = 45;
		this.num_EncSync.Value = new decimal(new int[4] { 5, 0, 0, 0 });
		this.label2.Location = new System.Drawing.Point(60, 52);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(172, 18);
		this.label2.TabIndex = 29;
		this.label2.Text = "预载波时间(ms)";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label3.Location = new System.Drawing.Point(60, 142);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(172, 18);
		this.label3.TabIndex = 44;
		this.label3.Text = "编码同步头个数";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.num_PreTim.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.num_PreTim.Location = new System.Drawing.Point(238, 52);
		this.num_PreTim.Maximum = new decimal(new int[4] { 2550, 0, 0, 0 });
		this.num_PreTim.Name = "num_PreTim";
		this.num_PreTim.Size = new System.Drawing.Size(64, 21);
		this.num_PreTim.TabIndex = 36;
		this.num_PreTim.Value = new decimal(new int[4] { 5, 0, 0, 0 });
		this.num_DecRst.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.num_DecRst.Location = new System.Drawing.Point(238, 112);
		this.num_DecRst.Maximum = new decimal(new int[4] { 2550, 0, 0, 0 });
		this.num_DecRst.Name = "num_DecRst";
		this.num_DecRst.Size = new System.Drawing.Size(64, 21);
		this.num_DecRst.TabIndex = 43;
		this.num_DecRst.Value = new decimal(new int[4] { 5, 0, 0, 0 });
		this.ckBox_Ctrl.Location = new System.Drawing.Point(349, 20);
		this.ckBox_Ctrl.Name = "ckBox_Ctrl";
		this.ckBox_Ctrl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.ckBox_Ctrl.Size = new System.Drawing.Size(97, 20);
		this.ckBox_Ctrl.TabIndex = 38;
		this.ckBox_Ctrl.Text = "MDC控制";
		this.ckBox_Ctrl.UseVisualStyleBackColor = true;
		this.ckBox_Ctrl.Visible = false;
		this.label6.Location = new System.Drawing.Point(60, 112);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(172, 18);
		this.label6.TabIndex = 42;
		this.label6.Text = "MDC解码复位时间(ms)";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.ckBox_ReqTone.Location = new System.Drawing.Point(349, 219);
		this.ckBox_ReqTone.Name = "ckBox_ReqTone";
		this.ckBox_ReqTone.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.ckBox_ReqTone.Size = new System.Drawing.Size(97, 20);
		this.ckBox_ReqTone.TabIndex = 39;
		this.ckBox_ReqTone.Text = "请求音";
		this.ckBox_ReqTone.UseVisualStyleBackColor = true;
		this.ckBox_ReqTone.Visible = false;
		this.num_SqlDly.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.num_SqlDly.Location = new System.Drawing.Point(238, 82);
		this.num_SqlDly.Maximum = new decimal(new int[4] { 2550, 0, 0, 0 });
		this.num_SqlDly.Name = "num_SqlDly";
		this.num_SqlDly.Size = new System.Drawing.Size(64, 21);
		this.num_SqlDly.TabIndex = 41;
		this.num_SqlDly.Value = new decimal(new int[4] { 5, 0, 0, 0 });
		this.label5.Location = new System.Drawing.Point(60, 82);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(172, 18);
		this.label5.TabIndex = 40;
		this.label5.Text = "SQL延迟时间(ms)";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tabP2.BackColor = System.Drawing.SystemColors.Control;
		this.tabP2.Location = new System.Drawing.Point(4, 26);
		this.tabP2.Name = "tabP2";
		this.tabP2.Size = new System.Drawing.Size(469, 267);
		this.tabP2.TabIndex = 1;
		this.tabP2.Text = "列表2";
		this.tabP3.BackColor = System.Drawing.SystemColors.Control;
		this.tabP3.Location = new System.Drawing.Point(4, 26);
		this.tabP3.Name = "tabP3";
		this.tabP3.Size = new System.Drawing.Size(469, 267);
		this.tabP3.TabIndex = 2;
		this.tabP3.Text = "列表3";
		this.tabP4.BackColor = System.Drawing.SystemColors.Control;
		this.tabP4.Location = new System.Drawing.Point(4, 26);
		this.tabP4.Name = "tabP4";
		this.tabP4.Size = new System.Drawing.Size(469, 267);
		this.tabP4.TabIndex = 3;
		this.tabP4.Text = "列表4";
		this.tabP5.BackColor = System.Drawing.SystemColors.Control;
		this.tabP5.Location = new System.Drawing.Point(4, 26);
		this.tabP5.Name = "tabP5";
		this.tabP5.Size = new System.Drawing.Size(469, 267);
		this.tabP5.TabIndex = 4;
		this.tabP5.Text = "列表5";
		this.grp1.Controls.Add(this.Tab_MdcPara);
		this.grp1.Location = new System.Drawing.Point(17, 12);
		this.grp1.Name = "grp1";
		this.grp1.Size = new System.Drawing.Size(525, 355);
		this.grp1.TabIndex = 19;
		this.grp1.TabStop = false;
		this.grp1.Text = "MDC 参数";
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.AutoScroll = true;
		base.ClientSize = new System.Drawing.Size(565, 391);
		base.Controls.Add(this.grp1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Frm_MDC_Para";
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		base.Load += new System.EventHandler(Frm_MDC_Para_Load);
		this.Tab_MdcPara.ResumeLayout(false);
		this.tabP1.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.num_EncSync).EndInit();
		((System.ComponentModel.ISupportInitialize)this.num_PreTim).EndInit();
		((System.ComponentModel.ISupportInitialize)this.num_DecRst).EndInit();
		((System.ComponentModel.ISupportInitialize)this.num_SqlDly).EndInit();
		this.grp1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
