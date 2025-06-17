using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Radio;

public class CommPrgForm : Form
{
	private IContainer components;

	private Label lblPrompt;

	private ProgressBar prgComm;

	private Button btnCancel;

	private PortComm portComm = new PortComm();

	public int[] START_ADDR = new int[0];

	public int[] END_ADDR = new int[0];

	public bool IsRead { get; set; }

	public bool IsSucess { get; set; }

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
		this.lblPrompt = new System.Windows.Forms.Label();
		this.prgComm = new System.Windows.Forms.ProgressBar();
		this.btnCancel = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.lblPrompt.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
		this.lblPrompt.Location = new System.Drawing.Point(37, 89);
		this.lblPrompt.Name = "lblPrompt";
		this.lblPrompt.Size = new System.Drawing.Size(326, 19);
		this.lblPrompt.TabIndex = 0;
		this.prgComm.Location = new System.Drawing.Point(37, 53);
		this.prgComm.Name = "prgComm";
		this.prgComm.Size = new System.Drawing.Size(326, 23);
		this.prgComm.TabIndex = 1;
		this.btnCancel.Location = new System.Drawing.Point(158, 121);
		this.btnCancel.Name = "btnCancel";
		this.btnCancel.Size = new System.Drawing.Size(75, 23);
		this.btnCancel.TabIndex = 2;
		this.btnCancel.Text = "取消";
		this.btnCancel.UseVisualStyleBackColor = true;
		this.btnCancel.Click += new System.EventHandler(btnCancel_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(401, 161);
		base.Controls.Add(this.btnCancel);
		base.Controls.Add(this.prgComm);
		base.Controls.Add(this.lblPrompt);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Name = "CommPrgForm";
		base.ShowInTaskbar = false;
		base.Load += new System.EventHandler(CommPrgForm_Load);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(CommPrgForm_FormClosing);
		base.ResumeLayout(false);
	}

	public CommPrgForm()
	{
		InitializeComponent();
		LangHelper.ApplyForm(this);
	}

	private void btnCancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void CommPrgForm_Load(object sender, EventArgs e)
	{
		prgComm.Minimum = 0;
		prgComm.Maximum = 100;
		if (IsRead)
		{
			Text = Lang.SZ_READ_DATA;
		}
		else
		{
			Text = Lang.SZ_WRITE_DATA;
		}
		portComm.IsRead = IsRead;
		portComm.START_ADDR = new int[12]
		{
			128, 1792, 1808, 1824, 1840, 1856, 1888, 1920, 1952, 2096,
			2112, 2384
		};
		portComm.END_ADDR = new int[12]
		{
			1728, 1800, 1815, 1832, 1848, 1872, 1896, 1936, 2040, 2104,
			2368, 5200
		};
		portComm.OnFirmwareUpdateProgress += FirmwareUpdateProgressHandler;
		portComm.UpdateFirmware();
	}

	private void FirmwareUpdateProgressHandler(object sender, FirmwareUpdateProgressEventArgs e)
	{
		if (prgComm.InvokeRequired)
		{
			BeginInvoke(new EventHandler<FirmwareUpdateProgressEventArgs>(FirmwareUpdateProgressHandler), sender, e);
			return;
		}
		if (e.Failed)
		{
			if (!string.IsNullOrEmpty(e.Message))
			{
				MessageBox.Show(e.Message, Lang.SZ_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			Close();
			return;
		}
		if (e.Closed)
		{
			Refresh();
			Close();
			return;
		}
		prgComm.Value = (int)e.Percentage;
		lblPrompt.Text = $"{prgComm.Value}%";
		if (e.Percentage == (float)prgComm.Maximum)
		{
			IsSucess = true;
			if (IsRead)
			{
				MessageBox.Show(Lang.SZ_READ_COMPLETED, Lang.SZ_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				MessageBox.Show(Lang.SZ_WRITE_COMPLETED, Lang.SZ_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void CommPrgForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (portComm.ThreadIsValid)
		{
			portComm.CancelComm = true;
			portComm.Join();
		}
	}
}
