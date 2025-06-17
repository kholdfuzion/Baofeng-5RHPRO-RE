using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Radio;

public class PasswordForm : Form
{
	private IContainer components;

	private Label lblPwd;

	private TextBox txtPwd;

	private Button btnOk;

	private Button btnCancel;

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
		this.lblPwd = new System.Windows.Forms.Label();
		this.btnOk = new System.Windows.Forms.Button();
		this.btnCancel = new System.Windows.Forms.Button();
		this.txtPwd = new System.Windows.Forms.TextBox();
		base.SuspendLayout();
		this.lblPwd.Location = new System.Drawing.Point(30, 47);
		this.lblPwd.Name = "lblPwd";
		this.lblPwd.Size = new System.Drawing.Size(73, 21);
		this.lblPwd.TabIndex = 0;
		this.lblPwd.Text = "Password";
		this.lblPwd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnOk.Location = new System.Drawing.Point(45, 102);
		this.btnOk.Name = "btnOk";
		this.btnOk.Size = new System.Drawing.Size(75, 23);
		this.btnOk.TabIndex = 2;
		this.btnOk.Text = "OK";
		this.btnOk.UseVisualStyleBackColor = true;
		this.btnOk.Click += new System.EventHandler(btnOk_Click);
		this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnCancel.Location = new System.Drawing.Point(159, 102);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(75, 23);
		this.btnCancel.TabIndex = 3;
		this.btnCancel.Text = "Cancel";
		this.btnCancel.UseVisualStyleBackColor = true;
		this.txtPwd.Location = new System.Drawing.Point(109, 47);
		this.txtPwd.Name = "txtPwd";
		this.txtPwd.PasswordChar = '*';
		this.txtPwd.Size = new System.Drawing.Size(129, 21);
		this.txtPwd.TabIndex = 1;
		base.AcceptButton = this.btnOk;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnCancel;
		base.ClientSize = new System.Drawing.Size(268, 153);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.btnOk);
		base.Controls.Add(this.txtPwd);
		base.Controls.Add(this.lblPwd);
		base.Name = "PasswordForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Password";
		base.Load += new System.EventHandler(PasswordForm_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public PasswordForm()
	{
		InitializeComponent();
		LangHelper.ApplyForm(this);
	}

	private void PasswordForm_Load(object sender, EventArgs e)
	{
		txtPwd.MaxLength = 16;
	}

	private void btnOk_Click(object sender, EventArgs e)
	{
		MainForm.CurPwd = txtPwd.Text;
	}
}
