using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using CPS_5RH.Data;

namespace CPS_5RH;

public class Frm_MDC_PTT : Form
{
	private IContainer components = null;

	private TabControl Tab_MdcPttID;

	private TabPage tabP1;

	private CheckBox ckBox_EotEn;

	private CheckBox ckBox_BotEn;

	private CheckBox ckBox_DecEn;

	private NumericUpDown num_EotTim;

	private Label label3;

	private NumericUpDown num_BotTim;

	private Label label6;

	private CheckBox ckBox_EncEn;

	private CheckBox ckBox_TxTone;

	private CheckBox ckBox_RxTone;

	private TabPage tabP2;

	private TabPage tabP3;

	private TabPage tabP4;

	private TabPage tabP5;

	private Panel panel1;

	private GroupBox grp1;

	private DataApp tdata = null;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPS_5RH.Frm_MDC_PTT));
		this.Tab_MdcPttID = new System.Windows.Forms.TabControl();
		this.tabP1 = new System.Windows.Forms.TabPage();
		this.panel1 = new System.Windows.Forms.Panel();
		this.ckBox_EncEn = new System.Windows.Forms.CheckBox();
		this.ckBox_EotEn = new System.Windows.Forms.CheckBox();
		this.ckBox_RxTone = new System.Windows.Forms.CheckBox();
		this.ckBox_BotEn = new System.Windows.Forms.CheckBox();
		this.ckBox_TxTone = new System.Windows.Forms.CheckBox();
		this.ckBox_DecEn = new System.Windows.Forms.CheckBox();
		this.label6 = new System.Windows.Forms.Label();
		this.num_EotTim = new System.Windows.Forms.NumericUpDown();
		this.num_BotTim = new System.Windows.Forms.NumericUpDown();
		this.label3 = new System.Windows.Forms.Label();
		this.tabP2 = new System.Windows.Forms.TabPage();
		this.tabP3 = new System.Windows.Forms.TabPage();
		this.tabP4 = new System.Windows.Forms.TabPage();
		this.tabP5 = new System.Windows.Forms.TabPage();
		this.grp1 = new System.Windows.Forms.GroupBox();
		this.Tab_MdcPttID.SuspendLayout();
		this.tabP1.SuspendLayout();
		this.panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.num_EotTim).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.num_BotTim).BeginInit();
		this.grp1.SuspendLayout();
		base.SuspendLayout();
		this.Tab_MdcPttID.Controls.Add(this.tabP1);
		this.Tab_MdcPttID.Controls.Add(this.tabP2);
		this.Tab_MdcPttID.Controls.Add(this.tabP3);
		this.Tab_MdcPttID.Controls.Add(this.tabP4);
		this.Tab_MdcPttID.Controls.Add(this.tabP5);
		this.Tab_MdcPttID.Location = new System.Drawing.Point(24, 20);
		this.Tab_MdcPttID.Name = "Tab_MdcPttID";
		this.Tab_MdcPttID.Padding = new System.Drawing.Point(10, 5);
		this.Tab_MdcPttID.SelectedIndex = 0;
		this.Tab_MdcPttID.Size = new System.Drawing.Size(458, 296);
		this.Tab_MdcPttID.TabIndex = 19;
		this.Tab_MdcPttID.SelectedIndexChanged += new System.EventHandler(Tab_MdcPttID_SelectedIndexChanged);
		this.tabP1.BackColor = System.Drawing.Color.Transparent;
		this.tabP1.Controls.Add(this.panel1);
		this.tabP1.Location = new System.Drawing.Point(4, 26);
		this.tabP1.Name = "tabP1";
		this.tabP1.Size = new System.Drawing.Size(450, 266);
		this.tabP1.TabIndex = 0;
		this.tabP1.Text = "列表1";
		this.tabP1.UseVisualStyleBackColor = true;
		this.panel1.BackColor = System.Drawing.SystemColors.Control;
		this.panel1.Controls.Add(this.ckBox_EncEn);
		this.panel1.Controls.Add(this.ckBox_EotEn);
		this.panel1.Controls.Add(this.ckBox_RxTone);
		this.panel1.Controls.Add(this.ckBox_BotEn);
		this.panel1.Controls.Add(this.ckBox_TxTone);
		this.panel1.Controls.Add(this.ckBox_DecEn);
		this.panel1.Controls.Add(this.label6);
		this.panel1.Controls.Add(this.num_EotTim);
		this.panel1.Controls.Add(this.num_BotTim);
		this.panel1.Controls.Add(this.label3);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel1.Location = new System.Drawing.Point(0, 0);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(450, 266);
		this.panel1.TabIndex = 51;
		this.ckBox_EncEn.Location = new System.Drawing.Point(104, 65);
		this.ckBox_EncEn.Name = "ckBox_EncEn";
		this.ckBox_EncEn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.ckBox_EncEn.Size = new System.Drawing.Size(161, 20);
		this.ckBox_EncEn.TabIndex = 39;
		this.ckBox_EncEn.Text = "PTT ID 编码允许";
		this.ckBox_EncEn.UseVisualStyleBackColor = true;
		this.ckBox_EotEn.Location = new System.Drawing.Point(104, 140);
		this.ckBox_EotEn.Name = "ckBox_EotEn";
		this.ckBox_EotEn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.ckBox_EotEn.Size = new System.Drawing.Size(161, 20);
		this.ckBox_EotEn.TabIndex = 50;
		this.ckBox_EotEn.Text = "PTT ID 下线码允许";
		this.ckBox_EotEn.UseVisualStyleBackColor = true;
		this.ckBox_RxTone.Location = new System.Drawing.Point(75, 40);
		this.ckBox_RxTone.Name = "ckBox_RxTone";
		this.ckBox_RxTone.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.ckBox_RxTone.Size = new System.Drawing.Size(190, 20);
		this.ckBox_RxTone.TabIndex = 1;
		this.ckBox_RxTone.Text = "解码提示音";
		this.ckBox_RxTone.UseVisualStyleBackColor = true;
		this.ckBox_BotEn.Location = new System.Drawing.Point(104, 115);
		this.ckBox_BotEn.Name = "ckBox_BotEn";
		this.ckBox_BotEn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.ckBox_BotEn.Size = new System.Drawing.Size(161, 20);
		this.ckBox_BotEn.TabIndex = 49;
		this.ckBox_BotEn.Text = "PTT ID 上线码允许";
		this.ckBox_BotEn.UseVisualStyleBackColor = true;
		this.ckBox_TxTone.Location = new System.Drawing.Point(88, 15);
		this.ckBox_TxTone.Name = "ckBox_TxTone";
		this.ckBox_TxTone.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.ckBox_TxTone.Size = new System.Drawing.Size(177, 20);
		this.ckBox_TxTone.TabIndex = 38;
		this.ckBox_TxTone.Text = "发射提示音";
		this.ckBox_TxTone.UseVisualStyleBackColor = true;
		this.ckBox_DecEn.Location = new System.Drawing.Point(104, 90);
		this.ckBox_DecEn.Name = "ckBox_DecEn";
		this.ckBox_DecEn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.ckBox_DecEn.Size = new System.Drawing.Size(161, 20);
		this.ckBox_DecEn.TabIndex = 48;
		this.ckBox_DecEn.Text = "PTT ID 解码允许";
		this.ckBox_DecEn.UseVisualStyleBackColor = true;
		this.label6.Location = new System.Drawing.Point(73, 170);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(172, 18);
		this.label6.TabIndex = 42;
		this.label6.Text = "上线码显示时间(ms)";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.num_EotTim.Increment = new decimal(new int[4] { 100, 0, 0, 0 });
		this.num_EotTim.Location = new System.Drawing.Point(251, 200);
		this.num_EotTim.Maximum = new decimal(new int[4] { 60000, 0, 0, 0 });
		this.num_EotTim.Name = "num_EotTim";
		this.num_EotTim.Size = new System.Drawing.Size(63, 21);
		this.num_EotTim.TabIndex = 45;
		this.num_EotTim.Value = new decimal(new int[4] { 5, 0, 0, 0 });
		this.num_BotTim.Increment = new decimal(new int[4] { 100, 0, 0, 0 });
		this.num_BotTim.Location = new System.Drawing.Point(251, 170);
		this.num_BotTim.Maximum = new decimal(new int[4] { 60000, 0, 0, 0 });
		this.num_BotTim.Name = "num_BotTim";
		this.num_BotTim.Size = new System.Drawing.Size(63, 21);
		this.num_BotTim.TabIndex = 43;
		this.num_BotTim.Value = new decimal(new int[4] { 5, 0, 0, 0 });
		this.label3.Location = new System.Drawing.Point(73, 200);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(172, 18);
		this.label3.TabIndex = 44;
		this.label3.Text = "下线码显示时间(ms)";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tabP2.BackColor = System.Drawing.Color.Transparent;
		this.tabP2.Location = new System.Drawing.Point(4, 26);
		this.tabP2.Name = "tabP2";
		this.tabP2.Size = new System.Drawing.Size(450, 266);
		this.tabP2.TabIndex = 1;
		this.tabP2.Text = "列表2";
		this.tabP2.UseVisualStyleBackColor = true;
		this.tabP3.BackColor = System.Drawing.Color.Transparent;
		this.tabP3.Location = new System.Drawing.Point(4, 26);
		this.tabP3.Name = "tabP3";
		this.tabP3.Size = new System.Drawing.Size(450, 266);
		this.tabP3.TabIndex = 2;
		this.tabP3.Text = "列表3";
		this.tabP3.UseVisualStyleBackColor = true;
		this.tabP4.BackColor = System.Drawing.Color.Transparent;
		this.tabP4.Location = new System.Drawing.Point(4, 26);
		this.tabP4.Name = "tabP4";
		this.tabP4.Size = new System.Drawing.Size(450, 266);
		this.tabP4.TabIndex = 3;
		this.tabP4.Text = "列表4";
		this.tabP4.UseVisualStyleBackColor = true;
		this.tabP5.BackColor = System.Drawing.Color.Transparent;
		this.tabP5.Location = new System.Drawing.Point(4, 26);
		this.tabP5.Name = "tabP5";
		this.tabP5.Size = new System.Drawing.Size(450, 266);
		this.tabP5.TabIndex = 4;
		this.tabP5.Text = "列表5";
		this.tabP5.UseVisualStyleBackColor = true;
		this.grp1.Controls.Add(this.Tab_MdcPttID);
		this.grp1.Location = new System.Drawing.Point(24, 12);
		this.grp1.Name = "grp1";
		this.grp1.Size = new System.Drawing.Size(509, 340);
		this.grp1.TabIndex = 20;
		this.grp1.TabStop = false;
		this.grp1.Text = "MDC PTT ID";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(567, 378);
		base.Controls.Add(this.grp1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Frm_MDC_PTT";
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = "MDC_PTTID";
		base.Load += new System.EventHandler(Frm_Roaming_Load);
		this.Tab_MdcPttID.ResumeLayout(false);
		this.tabP1.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.num_EotTim).EndInit();
		((System.ComponentModel.ISupportInitialize)this.num_BotTim).EndInit();
		this.grp1.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public Frm_MDC_PTT(DataApp data)
	{
		InitializeComponent();
		tdata = data;
	}

	private void Frm_Roaming_Load(object sender, EventArgs e)
	{
		if (FormMain.lang == "Chinese")
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
		}
		LanguageSel.LoadLanguage(this, typeof(Frm_MDC_PTT));
		ComponentResourceManager crm = new ComponentResourceManager(typeof(Frm_MDC_PTT));
	}

	public void DispMcdPtt(int pos)
	{
		ckBox_TxTone.DataBindings.Clear();
		ckBox_RxTone.DataBindings.Clear();
		ckBox_EncEn.DataBindings.Clear();
		ckBox_DecEn.DataBindings.Clear();
		ckBox_BotEn.DataBindings.Clear();
		ckBox_EotEn.DataBindings.Clear();
		num_BotTim.DataBindings.Clear();
		num_EotTim.DataBindings.Clear();
		if (tdata.dataMdcPttID[pos].BotTime > 60000)
		{
			tdata.dataMdcPttID[pos].BotTime = 0;
		}
		if (tdata.dataMdcPttID[pos].EotTime > 60000)
		{
			tdata.dataMdcPttID[pos].EotTime = 0;
		}
		ckBox_TxTone.DataBindings.Add("Checked", tdata.dataMdcPttID[pos], "TxTone", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		ckBox_RxTone.DataBindings.Add("Checked", tdata.dataMdcPttID[pos], "RxTone", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		ckBox_EncEn.DataBindings.Add("Checked", tdata.dataMdcPttID[pos], "EncEn", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		ckBox_DecEn.DataBindings.Add("Checked", tdata.dataMdcPttID[pos], "DecEn", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		ckBox_BotEn.DataBindings.Add("Checked", tdata.dataMdcPttID[pos], "BotEn", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		ckBox_EotEn.DataBindings.Add("Checked", tdata.dataMdcPttID[pos], "EotEn", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		num_BotTim.DataBindings.Add("Value", tdata.dataMdcPttID[pos], "BotTime", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		num_EotTim.DataBindings.Add("Value", tdata.dataMdcPttID[pos], "EotTime", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
	}

	private void Tab_MdcPttID_SelectedIndexChanged(object sender, EventArgs e)
	{
		Tab_MdcPttID.Controls.Remove(panel1);
		switch (Tab_MdcPttID.SelectedIndex)
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
		DispMcdPtt(Tab_MdcPttID.SelectedIndex);
	}
}
