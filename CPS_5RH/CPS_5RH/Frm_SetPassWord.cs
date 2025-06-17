using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using CPS_5RH.Properties;

namespace CPS_5RH;

public class Frm_SetPassWord : Form
{
	private IContainer components = null;

	private TextBox tBox_Pwd;

	private Label label1;

	private Button pB_btnCancel;

	private Button pB_btnStart;

	private static int setmode = 0;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPS_5RH.Frm_SetPassWord));
		this.tBox_Pwd = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.pB_btnCancel = new System.Windows.Forms.Button();
		this.pB_btnStart = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.tBox_Pwd.Location = new System.Drawing.Point(137, 37);
		this.tBox_Pwd.MaxLength = 8;
		this.tBox_Pwd.Name = "tBox_Pwd";
		this.tBox_Pwd.Size = new System.Drawing.Size(119, 21);
		this.tBox_Pwd.TabIndex = 10;
		this.label1.Location = new System.Drawing.Point(32, 40);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(99, 18);
		this.label1.TabIndex = 6;
		this.label1.Text = "密码:";
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
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(306, 133);
		base.Controls.Add(this.pB_btnCancel);
		base.Controls.Add(this.pB_btnStart);
		base.Controls.Add(this.tBox_Pwd);
		base.Controls.Add(this.label1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "Frm_SetPassWord";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "设置编程密码";
		base.Load += new System.EventHandler(Frm_SetPassWord_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public Frm_SetPassWord(int mode)
	{
		InitializeComponent();
		setmode = mode;
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
		ComponentResourceManager crm = new ComponentResourceManager(typeof(Frm_SetPassWord));
		LanguageSel.LoadLanguage(this, typeof(Frm_SetPassWord));
		if (setmode == 0)
		{
			tBox_Pwd.Text = CPS_5RH.Properties.Settings.Default.ProgramPwd;
		}
		else if (setmode == 1 || setmode == 2)
		{
			if (FormMain.lang == "Chinese")
			{
				Text = "输入密码";
			}
			else
			{
				Text = "Password";
			}
		}
	}

	private void pB_btnStart_Click(object sender, EventArgs e)
	{
		string datetime = "";
		if (setmode == 0)
		{
			CPS_5RH.Properties.Settings.Default.ProgramPwd = tBox_Pwd.Text;
			CPS_5RH.Properties.Settings.Default.Save();
			base.DialogResult = DialogResult.OK;
		}
		else if (setmode == 1)
		{
			datetime = DateTime.Now.ToString("yyyyMMdd");
			if (tBox_Pwd.Text == datetime)
			{
				base.DialogResult = DialogResult.OK;
			}
		}
		else if (setmode == 2)
		{
			datetime = DateTime.Now.ToString("ddMMyyyy");
			if (tBox_Pwd.Text == datetime)
			{
				base.DialogResult = DialogResult.OK;
			}
		}
	}

	private void pB_btnCancel_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
	}
}
