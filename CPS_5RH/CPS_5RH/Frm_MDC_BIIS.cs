using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using CPS_5RH.Data;

namespace CPS_5RH;

public class Frm_MDC_BIIS : Form
{
	private DataApp tdata = null;

	private IContainer components = null;

	private NumericUpDown Num_Pretime;

	private Label label7;

	private Label label4;

	private ComboBox coBox_ZoneCode;

	private Label lbl3;

	private CheckBox ckBox_Stone;

	private Label lbl7;

	private NumericUpDown Num_Sync;

	private Label label1;

	private TextBox tb_GrpID;

	private TextBox tb_SelfID;

	private GroupBox grp_Title;

	public Frm_MDC_BIIS(DataApp data)
	{
		InitializeComponent();
		tdata = data;
		bingDingTheControls();
	}

	private void Frm_KeySet_Load(object sender, EventArgs e)
	{
		if (FormMain.lang == "Chinese")
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
		}
		LanguageSel.LoadLanguage(this, typeof(Frm_MDC_BIIS));
		ComponentResourceManager crm = new ComponentResourceManager(typeof(Frm_MDC_BIIS));
		tb_GrpID.LostFocus -= tb_GrpID_LostFocus;
		tb_GrpID.LostFocus += tb_GrpID_LostFocus;
		tb_SelfID.LostFocus -= tb_SelfID_LostFocus;
		tb_SelfID.LostFocus += tb_SelfID_LostFocus;
	}

	private void bingDingTheControls()
	{
		if (tdata.dataMdcBiis.ZoneCode > 31)
		{
			tdata.dataMdcBiis.ZoneCode = 0;
		}
		ckBox_Stone.DataBindings.Add("Checked", tdata.dataMdcBiis, "ToneSw", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		Num_Sync.DataBindings.Add("Value", tdata.dataMdcBiis, "Sync", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		Num_Pretime.DataBindings.Add("Value", tdata.dataMdcBiis, "PreTime", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_ZoneCode.DataBindings.Add("SelectedIndex", tdata.dataMdcBiis, "ZoneCode", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		tb_SelfID.Text = GetFormatStr(tdata.dataMdcBiis.SelfID);
		tb_GrpID.Text = GetFormatStr(tdata.dataMdcBiis.GrpID);
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

	private int Adjust_GetVal(string val)
	{
		int intFreq;
		try
		{
			intFreq = Convert.ToInt32(val);
			if (intFreq > 4095)
			{
				intFreq = 4095;
			}
		}
		catch
		{
			intFreq = 1;
		}
		return intFreq;
	}

	private string GetFormatStr(int inputid)
	{
		string value = inputid.ToString();
		for (int i = value.Length; i < 4; i++)
		{
			value = value.Insert(0, "0");
		}
		return value;
	}

	private void tb_GrpID_LostFocus(object sender, EventArgs e)
	{
		string value = ((TextBox)sender).Text;
		tdata.dataMdcBiis.GrpID = (ushort)Adjust_GetVal(value);
		tb_GrpID.Text = GetFormatStr(tdata.dataMdcBiis.GrpID);
	}

	private void tb_SelfID_LostFocus(object sender, EventArgs e)
	{
		string value = ((TextBox)sender).Text;
		tdata.dataMdcBiis.SelfID = (ushort)Adjust_GetVal(value);
		tb_SelfID.Text = GetFormatStr(tdata.dataMdcBiis.SelfID);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPS_5RH.Frm_MDC_BIIS));
		this.Num_Pretime = new System.Windows.Forms.NumericUpDown();
		this.label7 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.coBox_ZoneCode = new System.Windows.Forms.ComboBox();
		this.lbl3 = new System.Windows.Forms.Label();
		this.ckBox_Stone = new System.Windows.Forms.CheckBox();
		this.lbl7 = new System.Windows.Forms.Label();
		this.Num_Sync = new System.Windows.Forms.NumericUpDown();
		this.label1 = new System.Windows.Forms.Label();
		this.tb_GrpID = new System.Windows.Forms.TextBox();
		this.tb_SelfID = new System.Windows.Forms.TextBox();
		this.grp_Title = new System.Windows.Forms.GroupBox();
		((System.ComponentModel.ISupportInitialize)this.Num_Pretime).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Num_Sync).BeginInit();
		this.grp_Title.SuspendLayout();
		base.SuspendLayout();
		this.Num_Pretime.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.Num_Pretime.Location = new System.Drawing.Point(207, 152);
		this.Num_Pretime.Maximum = new decimal(new int[4] { 2550, 0, 0, 0 });
		this.Num_Pretime.Name = "Num_Pretime";
		this.Num_Pretime.Size = new System.Drawing.Size(99, 21);
		this.Num_Pretime.TabIndex = 58;
		this.Num_Pretime.Value = new decimal(new int[4] { 1000, 0, 0, 0 });
		this.label7.Location = new System.Drawing.Point(102, 62);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(100, 18);
		this.label7.TabIndex = 57;
		this.label7.Text = "本机ID";
		this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label4.Location = new System.Drawing.Point(100, 182);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(100, 18);
		this.label4.TabIndex = 55;
		this.label4.Text = "区域码";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_ZoneCode.DropDownHeight = 100;
		this.coBox_ZoneCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_ZoneCode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_ZoneCode.FormattingEnabled = true;
		this.coBox_ZoneCode.IntegralHeight = false;
		this.coBox_ZoneCode.Items.AddRange(new object[32]
		{
			"-", "SCV", "P", "CZ/SK", "CY", "CH", "BG/H/ROM", "GR", "DK", "GB",
			"I", "SF", "B", "N", "AND/FR", "D", "F", "NL", "S", "（-）",
			"YU", "PL", "SMR", "E", "IS", "A", "FL", "L", "TR", "M/SLO/CRO",
			"IRL", "MC"
		});
		this.coBox_ZoneCode.Location = new System.Drawing.Point(207, 182);
		this.coBox_ZoneCode.Name = "coBox_ZoneCode";
		this.coBox_ZoneCode.Size = new System.Drawing.Size(99, 20);
		this.coBox_ZoneCode.TabIndex = 54;
		this.lbl3.Location = new System.Drawing.Point(102, 92);
		this.lbl3.Name = "lbl3";
		this.lbl3.Size = new System.Drawing.Size(100, 18);
		this.lbl3.TabIndex = 41;
		this.lbl3.Text = "组呼ID";
		this.lbl3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.ckBox_Stone.Location = new System.Drawing.Point(113, 32);
		this.ckBox_Stone.Name = "ckBox_Stone";
		this.ckBox_Stone.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.ckBox_Stone.Size = new System.Drawing.Size(109, 18);
		this.ckBox_Stone.TabIndex = 52;
		this.ckBox_Stone.Text = "侧音";
		this.ckBox_Stone.UseVisualStyleBackColor = true;
		this.lbl7.Location = new System.Drawing.Point(26, 152);
		this.lbl7.Name = "lbl7";
		this.lbl7.Size = new System.Drawing.Size(174, 18);
		this.lbl7.TabIndex = 49;
		this.lbl7.Text = "预载波时间(ms)";
		this.lbl7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.Num_Sync.Hexadecimal = true;
		this.Num_Sync.Location = new System.Drawing.Point(207, 122);
		this.Num_Sync.Maximum = new decimal(new int[4] { 61440, 0, 0, 0 });
		this.Num_Sync.Name = "Num_Sync";
		this.Num_Sync.Size = new System.Drawing.Size(99, 21);
		this.Num_Sync.TabIndex = 48;
		this.Num_Sync.Value = new decimal(new int[4] { 1000, 0, 0, 0 });
		this.label1.Location = new System.Drawing.Point(100, 122);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(100, 18);
		this.label1.TabIndex = 37;
		this.label1.Text = "同步头";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tb_GrpID.Location = new System.Drawing.Point(207, 92);
		this.tb_GrpID.MaxLength = 4;
		this.tb_GrpID.Name = "tb_GrpID";
		this.tb_GrpID.Size = new System.Drawing.Size(99, 21);
		this.tb_GrpID.TabIndex = 59;
		this.tb_GrpID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Freq_Cells_KeyPress);
		this.tb_SelfID.Location = new System.Drawing.Point(208, 62);
		this.tb_SelfID.MaxLength = 4;
		this.tb_SelfID.Name = "tb_SelfID";
		this.tb_SelfID.Size = new System.Drawing.Size(99, 21);
		this.tb_SelfID.TabIndex = 60;
		this.tb_SelfID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Freq_Cells_KeyPress);
		this.grp_Title.Controls.Add(this.ckBox_Stone);
		this.grp_Title.Controls.Add(this.tb_SelfID);
		this.grp_Title.Controls.Add(this.label1);
		this.grp_Title.Controls.Add(this.tb_GrpID);
		this.grp_Title.Controls.Add(this.Num_Sync);
		this.grp_Title.Controls.Add(this.Num_Pretime);
		this.grp_Title.Controls.Add(this.lbl7);
		this.grp_Title.Controls.Add(this.label7);
		this.grp_Title.Controls.Add(this.lbl3);
		this.grp_Title.Controls.Add(this.label4);
		this.grp_Title.Controls.Add(this.coBox_ZoneCode);
		this.grp_Title.Location = new System.Drawing.Point(28, 12);
		this.grp_Title.Name = "grp_Title";
		this.grp_Title.Size = new System.Drawing.Size(363, 238);
		this.grp_Title.TabIndex = 61;
		this.grp_Title.TabStop = false;
		this.grp_Title.Text = "BIIS码设置";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.ClientSize = new System.Drawing.Size(455, 262);
		base.Controls.Add(this.grp_Title);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Frm_MDC_BIIS";
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = " BIIS码";
		base.Load += new System.EventHandler(Frm_KeySet_Load);
		((System.ComponentModel.ISupportInitialize)this.Num_Pretime).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Num_Sync).EndInit();
		this.grp_Title.ResumeLayout(false);
		this.grp_Title.PerformLayout();
		base.ResumeLayout(false);
	}
}
