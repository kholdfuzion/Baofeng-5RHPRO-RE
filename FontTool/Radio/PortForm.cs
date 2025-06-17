using System;
using System.ComponentModel;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace Radio;

public class PortForm : Form
{
	private IContainer components;

	private Label lblPort;

	private ComboBox cmbPort;

	private Button btnCancel;

	private Button btnOK;

	private Button btnRefresh;

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
		this.lblPort = new System.Windows.Forms.Label();
		this.cmbPort = new System.Windows.Forms.ComboBox();
		this.btnCancel = new System.Windows.Forms.Button();
		this.btnOK = new System.Windows.Forms.Button();
		this.btnRefresh = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.lblPort.Location = new System.Drawing.Point(38, 60);
		this.lblPort.Name = "lblPort";
		this.lblPort.Size = new System.Drawing.Size(55, 20);
		this.lblPort.TabIndex = 0;
		this.lblPort.Text = "Port";
		this.lblPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.cmbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbPort.FormattingEnabled = true;
		this.cmbPort.Location = new System.Drawing.Point(106, 60);
		this.cmbPort.Name = "cmbPort";
		this.cmbPort.Size = new System.Drawing.Size(93, 20);
		this.cmbPort.TabIndex = 1;
		this.btnCancel.Location = new System.Drawing.Point(192, 121);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(75, 23);
		this.btnCancel.TabIndex = 3;
		this.btnCancel.Text = "Cancel";
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnOK.Location = new System.Drawing.Point(68, 121);
		this.btnOK.Name = "btnOK";
		this.btnOK.Size = new System.Drawing.Size(75, 23);
		this.btnOK.TabIndex = 2;
		this.btnOK.Text = "OK";
		this.btnOK.UseVisualStyleBackColor = true;
		this.btnOK.Click += new System.EventHandler(btnOK_Click);
		this.btnRefresh.Location = new System.Drawing.Point(221, 58);
		this.btnRefresh.Name = "btnRefresh";
		this.btnRefresh.Size = new System.Drawing.Size(75, 22);
		this.btnRefresh.TabIndex = 4;
		this.btnRefresh.Text = "Refresh";
		this.btnRefresh.UseVisualStyleBackColor = true;
		this.btnRefresh.Click += new System.EventHandler(btnRefresh_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(335, 214);
		base.Controls.Add(this.btnRefresh);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.btnOK);
		base.Controls.Add(this.cmbPort);
		base.Controls.Add(this.lblPort);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "PortForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Serial port settings";
		base.Load += new System.EventHandler(FrmComSet_Load);
		base.ResumeLayout(false);
	}

	public PortForm()
	{
		InitializeComponent();
		LangHelper.ApplyForm(this);
	}

	private void InitCom()
	{
		cmbPort.Items.Clear();
		string[] portNames = SerialPort.GetPortNames();
		foreach (string name in portNames)
		{
			cmbPort.Items.Add(name);
		}
	}

	private void FrmComSet_Load(object sender, EventArgs e)
	{
		InitCom();
		cmbPort.SelectedItem = MainForm.CurCom;
	}

	private void btnOK_Click(object sender, EventArgs e)
	{
		try
		{
			MainForm.CurCom = cmbPort.SelectedItem.ToString();
			Global.SetupIniHelper.WriteString("Setup", "Com", MainForm.CurCom);
			Close();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnRefresh_Click(object sender, EventArgs e)
	{
		InitCom();
	}
}
