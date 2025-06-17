using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using CPS_5RH.Data;

namespace CPS_5RH;

public class Frm_Devinfo : Form
{
	private IContainer components = null;

	private Label lbl5;

	private Label lbl7;

	private TextBox tBox_Hver;

	private TextBox tBox_Date;

	private Label lbl4;

	private Label lbl1;

	private Label lbl3;

	private TextBox tBox_DevName;

	private TextBox tBox_SoftVer;

	private TextBox tBox_FreqRange;

	private TextBox tBox_TxFreqRange;

	private Label label1;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPS_5RH.Frm_Devinfo));
		this.lbl5 = new System.Windows.Forms.Label();
		this.lbl7 = new System.Windows.Forms.Label();
		this.lbl4 = new System.Windows.Forms.Label();
		this.tBox_Hver = new System.Windows.Forms.TextBox();
		this.tBox_Date = new System.Windows.Forms.TextBox();
		this.lbl1 = new System.Windows.Forms.Label();
		this.lbl3 = new System.Windows.Forms.Label();
		this.tBox_DevName = new System.Windows.Forms.TextBox();
		this.tBox_SoftVer = new System.Windows.Forms.TextBox();
		this.tBox_FreqRange = new System.Windows.Forms.TextBox();
		this.tBox_TxFreqRange = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.lbl5.Location = new System.Drawing.Point(28, 77);
		this.lbl5.Name = "lbl5";
		this.lbl5.Size = new System.Drawing.Size(160, 20);
		this.lbl5.TabIndex = 0;
		this.lbl5.Text = "接收频段范围[MHz]";
		this.lbl5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl7.Location = new System.Drawing.Point(21, 252);
		this.lbl7.Name = "lbl7";
		this.lbl7.Size = new System.Drawing.Size(167, 20);
		this.lbl7.TabIndex = 1;
		this.lbl7.Text = "上次编程时间";
		this.lbl7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl4.Location = new System.Drawing.Point(31, 222);
		this.lbl4.Name = "lbl4";
		this.lbl4.Size = new System.Drawing.Size(158, 20);
		this.lbl4.TabIndex = 2;
		this.lbl4.Text = "硬件版本号";
		this.lbl4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tBox_Hver.Location = new System.Drawing.Point(192, 222);
		this.tBox_Hver.Name = "tBox_Hver";
		this.tBox_Hver.ReadOnly = true;
		this.tBox_Hver.Size = new System.Drawing.Size(127, 21);
		this.tBox_Hver.TabIndex = 10;
		this.tBox_Date.Location = new System.Drawing.Point(192, 252);
		this.tBox_Date.Name = "tBox_Date";
		this.tBox_Date.ReadOnly = true;
		this.tBox_Date.Size = new System.Drawing.Size(127, 21);
		this.tBox_Date.TabIndex = 9;
		this.lbl1.Location = new System.Drawing.Point(87, 25);
		this.lbl1.Name = "lbl1";
		this.lbl1.Size = new System.Drawing.Size(100, 20);
		this.lbl1.TabIndex = 3;
		this.lbl1.Text = "设备名称";
		this.lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl3.Location = new System.Drawing.Point(33, 192);
		this.lbl3.Name = "lbl3";
		this.lbl3.Size = new System.Drawing.Size(156, 20);
		this.lbl3.TabIndex = 6;
		this.lbl3.Text = "软件版本号";
		this.lbl3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tBox_DevName.Location = new System.Drawing.Point(192, 25);
		this.tBox_DevName.Name = "tBox_DevName";
		this.tBox_DevName.ReadOnly = true;
		this.tBox_DevName.Size = new System.Drawing.Size(127, 21);
		this.tBox_DevName.TabIndex = 12;
		this.tBox_SoftVer.Location = new System.Drawing.Point(192, 192);
		this.tBox_SoftVer.Name = "tBox_SoftVer";
		this.tBox_SoftVer.ReadOnly = true;
		this.tBox_SoftVer.Size = new System.Drawing.Size(127, 21);
		this.tBox_SoftVer.TabIndex = 14;
		this.tBox_FreqRange.Enabled = false;
		this.tBox_FreqRange.Location = new System.Drawing.Point(192, 55);
		this.tBox_FreqRange.Multiline = true;
		this.tBox_FreqRange.Name = "tBox_FreqRange";
		this.tBox_FreqRange.ReadOnly = true;
		this.tBox_FreqRange.Size = new System.Drawing.Size(167, 60);
		this.tBox_FreqRange.TabIndex = 15;
		this.tBox_TxFreqRange.Enabled = false;
		this.tBox_TxFreqRange.Location = new System.Drawing.Point(192, 125);
		this.tBox_TxFreqRange.Multiline = true;
		this.tBox_TxFreqRange.Name = "tBox_TxFreqRange";
		this.tBox_TxFreqRange.ReadOnly = true;
		this.tBox_TxFreqRange.Size = new System.Drawing.Size(167, 60);
		this.tBox_TxFreqRange.TabIndex = 16;
		this.label1.Location = new System.Drawing.Point(29, 148);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(160, 20);
		this.label1.TabIndex = 17;
		this.label1.Text = "发射频段范围[MHz]";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(475, 295);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.tBox_TxFreqRange);
		base.Controls.Add(this.tBox_FreqRange);
		base.Controls.Add(this.tBox_SoftVer);
		base.Controls.Add(this.tBox_DevName);
		base.Controls.Add(this.lbl3);
		base.Controls.Add(this.lbl1);
		base.Controls.Add(this.lbl4);
		base.Controls.Add(this.tBox_Hver);
		base.Controls.Add(this.lbl7);
		base.Controls.Add(this.tBox_Date);
		base.Controls.Add(this.lbl5);
		this.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Frm_Devinfo";
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		base.Load += new System.EventHandler(Frm_Devinfo_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public Frm_Devinfo(DataApp data)
	{
		InitializeComponent();
		tdata = data;
	}

	private void Frm_Devinfo_Load(object sender, EventArgs e)
	{
		if (FormMain.lang == "Chinese")
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
		}
		LanguageSel.LoadLanguage(this, typeof(Frm_Devinfo));
	}

	public void DeviceDataDisp()
	{
		int cnt = 0;
		int intfreq = 0;
		double tmpfreq = 0.0;
		string strfreq = "";
		tBox_DevName.Text = tdata.dataDevice.DevName;
		tBox_SoftVer.Text = tdata.dataDevice.SoftVer;
		tBox_Hver.Text = tdata.dataDevice.HardVer;
		tBox_Date.Text = tdata.dataDevice.Pdate;
		tBox_FreqRange.Text = "";
		for (int i = 0; i < 4; i++)
		{
			intfreq = tdata.dataDevice.GetRxStartFreq(i);
			if (intfreq > 100000)
			{
				strfreq = ((double)intfreq / 100000.0).ToString("0.000000");
				TextBox textBox = tBox_FreqRange;
				textBox.Text = textBox.Text + strfreq + " - ";
				strfreq = ((double)tdata.dataDevice.GetRxEndFreq(i) / 100000.0).ToString("0.000000");
				tBox_FreqRange.Text += strfreq;
				if (i < 3)
				{
					tBox_FreqRange.Text += "\r\n";
				}
				cnt++;
				if (cnt >= tdata.dataDevice.FreqRxPt)
				{
					break;
				}
			}
		}
		cnt = 0;
		tBox_TxFreqRange.Text = "";
		for (int i = 0; i < 4; i++)
		{
			intfreq = tdata.dataDevice.GetTxStartFreq(i);
			if (intfreq > 100000)
			{
				strfreq = ((double)intfreq / 100000.0).ToString("0.000000");
				TextBox textBox2 = tBox_TxFreqRange;
				textBox2.Text = textBox2.Text + strfreq + " - ";
				strfreq = ((double)tdata.dataDevice.GetTxEndFreq(i) / 100000.0).ToString("0.000000");
				tBox_TxFreqRange.Text += strfreq;
				if (i < 3)
				{
					tBox_TxFreqRange.Text += "\r\n";
				}
				cnt++;
				if (cnt >= tdata.dataDevice.FreqTxPt)
				{
					break;
				}
			}
		}
	}
}
