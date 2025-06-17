using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CPS_5RH;

public class FormAbout : Form
{
	private IContainer components = null;

	private PictureBox pictureBox1;

	private Label lbl_Name;

	private Label lbl_Version;

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPS_5RH.FormAbout));
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.lbl_Name = new System.Windows.Forms.Label();
		this.lbl_Version = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
		this.pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
		this.pictureBox1.InitialImage = null;
		this.pictureBox1.Location = new System.Drawing.Point(7, 17);
		this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(87, 68);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
		this.pictureBox1.TabIndex = 0;
		this.pictureBox1.TabStop = false;
		this.pictureBox1.Click += new System.EventHandler(FormAbout_Click);
		this.lbl_Name.AutoSize = true;
		this.lbl_Name.BackColor = System.Drawing.SystemColors.Control;
		this.lbl_Name.ForeColor = System.Drawing.Color.Black;
		this.lbl_Name.Location = new System.Drawing.Point(110, 55);
		this.lbl_Name.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.lbl_Name.Name = "lbl_Name";
		this.lbl_Name.Size = new System.Drawing.Size(53, 12);
		this.lbl_Name.TabIndex = 1;
		this.lbl_Name.Text = "(C) 2025";
		this.lbl_Name.Click += new System.EventHandler(FormAbout_Click);
		this.lbl_Version.AutoSize = true;
		this.lbl_Version.BackColor = System.Drawing.SystemColors.Control;
		this.lbl_Version.ForeColor = System.Drawing.Color.Black;
		this.lbl_Version.Location = new System.Drawing.Point(110, 33);
		this.lbl_Version.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.lbl_Version.Name = "lbl_Version";
		this.lbl_Version.Size = new System.Drawing.Size(83, 12);
		this.lbl_Version.TabIndex = 2;
		this.lbl_Version.Text = "Revision 1.19";
		this.lbl_Version.Click += new System.EventHandler(FormAbout_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(326, 110);
		base.Controls.Add(this.lbl_Version);
		base.Controls.Add(this.lbl_Name);
		base.Controls.Add(this.pictureBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Margin = new System.Windows.Forms.Padding(2);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormAbout";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "关于";
		base.Load += new System.EventHandler(FormAbout_Load);
		base.Click += new System.EventHandler(FormAbout_Click);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public FormAbout()
	{
		InitializeComponent();
	}

	private void FormAbout_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void FormAbout_Load(object sender, EventArgs e)
	{
		if (!(FormMain.lang == "Chinese"))
		{
			Text = "About";
		}
	}
}
