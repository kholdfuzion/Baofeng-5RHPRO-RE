using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Radio;

public class AboutForm : Form
{
	private IContainer components;

	private Label lblVersion;

	private Label lblCompany;

	private Button btnClose;

	private PictureBox picLogo;

	public int CompanyClickCount { get; set; }

	public int VersionClickCount { get; set; }

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
		this.lblVersion = new System.Windows.Forms.Label();
		this.lblCompany = new System.Windows.Forms.Label();
		this.btnClose = new System.Windows.Forms.Button();
		this.picLogo = new System.Windows.Forms.PictureBox();
		((System.ComponentModel.ISupportInitialize)this.picLogo).BeginInit();
		base.SuspendLayout();
		this.lblVersion.Location = new System.Drawing.Point(157, 20);
		this.lblVersion.Name = "lblVersion";
		this.lblVersion.Size = new System.Drawing.Size(278, 20);
		this.lblVersion.TabIndex = 0;
		this.lblVersion.Text = "v1.0.0";
		this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lblVersion.Click += new System.EventHandler(lblVersion_Click);
		this.lblCompany.Location = new System.Drawing.Point(157, 55);
		this.lblCompany.Name = "lblCompany";
		this.lblCompany.Size = new System.Drawing.Size(278, 20);
		this.lblCompany.TabIndex = 0;
		this.lblCompany.Text = "Company";
		this.lblCompany.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.lblCompany.Click += new System.EventHandler(lblCompany_Click);
		this.btnClose.Location = new System.Drawing.Point(259, 313);
		this.btnClose.Name = "btnClose";
		this.btnClose.Size = new System.Drawing.Size(75, 23);
		this.btnClose.TabIndex = 1;
		this.btnClose.Text = "Close";
		this.btnClose.UseVisualStyleBackColor = true;
		this.btnClose.Click += new System.EventHandler(btnClose_Click);
		this.picLogo.Location = new System.Drawing.Point(17, 99);
		this.picLogo.Name = "picLogo";
		this.picLogo.Size = new System.Drawing.Size(560, 200);
		this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
		this.picLogo.TabIndex = 2;
		this.picLogo.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(593, 348);
		base.Controls.Add(this.picLogo);
		base.Controls.Add(this.btnClose);
		base.Controls.Add(this.lblCompany);
		base.Controls.Add(this.lblVersion);
		base.Name = "AboutForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "About";
		base.Load += new System.EventHandler(AboutForm_Load);
		((System.ComponentModel.ISupportInitialize)this.picLogo).EndInit();
		base.ResumeLayout(false);
	}

	public AboutForm()
	{
		InitializeComponent();
		LangHelper.ApplyForm(this);
	}

	private void btnClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void AboutForm_Load(object sender, EventArgs e)
	{
		CompanyClickCount = 0;
		lblVersion.Text = Global.SetupIniHelper.ReadString("Info", "Version", "v1.0.0");
		lblCompany.Text = Global.SetupIniHelper.ReadString("Info", "Company", "---");
		if (File.Exists(Application.StartupPath + "\\Logo.bmp"))
		{
			picLogo.Image = Image.FromFile(Application.StartupPath + "\\Logo.bmp");
		}
	}

	private void lblCompany_Click(object sender, EventArgs e)
	{
		CompanyClickCount++;
		if (CompanyClickCount == 3)
		{
			CompanyClickCount = 0;
			using PasswordForm password = new PasswordForm();
			password.ShowDialog();
		}
	}

	private void lblVersion_Click(object sender, EventArgs e)
	{
		VersionClickCount++;
		if (VersionClickCount == 3)
		{
			VersionClickCount = 0;
			MainForm.CurPwd = "";
		}
	}
}
