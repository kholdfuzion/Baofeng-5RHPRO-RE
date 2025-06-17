using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace CPS_5RH;

public class Frm_SetFreqBand : Form
{
	private IContainer components = null;

	private Label label1;

	private Button pB_btnCancel;

	private Button pB_btnStart;

	private ComboBox coBox_FreqBand;

	public Frm_SetFreqBand()
	{
		InitializeComponent();
	}

	private void Frm_SetPassWord_Load(object sender, EventArgs e)
	{
		if (FormMain.lang == "Chinese")
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
		}
		ComponentResourceManager crm = new ComponentResourceManager(typeof(Frm_SetFreqBand));
		LanguageSel.LoadLanguage(this, typeof(Frm_SetFreqBand));
		if (FormMain.lang == "Chinese")
		{
			coBox_FreqBand.Items[0] = "工厂标准";
			coBox_FreqBand.Items[1] = "中国标准";
			coBox_FreqBand.Items[2] = "美版标准";
			coBox_FreqBand.Items[3] = "加拿大标准";
			coBox_FreqBand.Items[4] = "欧盟标准";
			coBox_FreqBand.Items[5] = "印尼标准";
			coBox_FreqBand.Items[6] = "CEGT 业余";
			coBox_FreqBand.Items[7] = "印尼业余";
			coBox_FreqBand.Items[8] = "菲律宾标准";
		}
		else
		{
			coBox_FreqBand.Items[0] = "Factory";
			coBox_FreqBand.Items[1] = "SRRC";
			coBox_FreqBand.Items[2] = "FCC";
			coBox_FreqBand.Items[3] = "IC";
			coBox_FreqBand.Items[4] = "CE";
			coBox_FreqBand.Items[5] = "Indonesia";
			coBox_FreqBand.Items[6] = "Europe-CEGT";
			coBox_FreqBand.Items[7] = "Indonesia-Pro";
			coBox_FreqBand.Items[8] = "Philippines";
		}
		int index = Settings.FreqBand;
		if (index <= 8)
		{
			coBox_FreqBand.SelectedIndex = index;
		}
		else
		{
			coBox_FreqBand.SelectedIndex = 0;
		}
	}

	private void pB_btnStart_Click(object sender, EventArgs e)
	{
		int idx = coBox_FreqBand.SelectedIndex;
		if (idx != Settings.FreqBand)
		{
			Settings.FreqBand = (byte)idx;
			Settings.Save();
			base.DialogResult = DialogResult.OK;
		}
		else
		{
			base.DialogResult = DialogResult.Cancel;
		}
	}

	private void pB_btnCancel_Click(object sender, EventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPS_5RH.Frm_SetFreqBand));
		this.label1 = new System.Windows.Forms.Label();
		this.pB_btnCancel = new System.Windows.Forms.Button();
		this.pB_btnStart = new System.Windows.Forms.Button();
		this.coBox_FreqBand = new System.Windows.Forms.ComboBox();
		base.SuspendLayout();
		this.label1.Location = new System.Drawing.Point(5, 40);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(126, 18);
		this.label1.TabIndex = 6;
		this.label1.Text = "频率范围:";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.pB_btnCancel.AutoSize = true;
		this.pB_btnCancel.Location = new System.Drawing.Point(189, 82);
		this.pB_btnCancel.Margin = new System.Windows.Forms.Padding(2);
		this.pB_btnCancel.Name = "pB_btnCancel";
		this.pB_btnCancel.Size = new System.Drawing.Size(67, 22);
		this.pB_btnCancel.TabIndex = 9;
		this.pB_btnCancel.Text = "取消";
		this.pB_btnCancel.UseVisualStyleBackColor = true;
		this.pB_btnCancel.Click += new System.EventHandler(pB_btnCancel_Click);
		this.pB_btnStart.AutoSize = true;
		this.pB_btnStart.Location = new System.Drawing.Point(67, 82);
		this.pB_btnStart.Margin = new System.Windows.Forms.Padding(2);
		this.pB_btnStart.Name = "pB_btnStart";
		this.pB_btnStart.Size = new System.Drawing.Size(67, 22);
		this.pB_btnStart.TabIndex = 8;
		this.pB_btnStart.Text = "确定";
		this.pB_btnStart.UseVisualStyleBackColor = true;
		this.pB_btnStart.Click += new System.EventHandler(pB_btnStart_Click);
		this.coBox_FreqBand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_FreqBand.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_FreqBand.FormattingEnabled = true;
		this.coBox_FreqBand.Items.AddRange(new object[9] { "工厂标准", "中国标准", "美版标准", "加拿大标准", "欧盟标准", "印尼标准", "CEGT 业余", "印尼认证", "菲律宾标准" });
		this.coBox_FreqBand.Location = new System.Drawing.Point(135, 40);
		this.coBox_FreqBand.Name = "coBox_FreqBand";
		this.coBox_FreqBand.Size = new System.Drawing.Size(133, 20);
		this.coBox_FreqBand.TabIndex = 11;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(306, 133);
		base.Controls.Add(this.coBox_FreqBand);
		base.Controls.Add(this.pB_btnCancel);
		base.Controls.Add(this.pB_btnStart);
		base.Controls.Add(this.label1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "Frm_SetFreqBand";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "设置频率范围";
		base.Load += new System.EventHandler(Frm_SetPassWord_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
