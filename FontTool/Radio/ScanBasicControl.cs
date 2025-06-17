using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Radio;

public class ScanBasicControl : UserControl, IViewData
{
	private IContainer components;

	private Label lblTxDelay;

	private ComboBox cmbTxDelay;

	private Label lblRxDelay;

	private ComboBox cmbRxDelay;

	private Label lblBackTime;

	private ComboBox cmbBackTime;

	private Label lblPriorityCh;

	private ComboBox cmbPriorityCh;

	private Label lblPriorityScan;

	private ComboBox cmbPriorityScan;

	private Label lblScanMode;

	private ComboBox cmbScanMode;

	private Label lblRevertCh;

	private ComboBox cmbRevertCh;

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
		this.lblTxDelay = new System.Windows.Forms.Label();
		this.cmbTxDelay = new System.Windows.Forms.ComboBox();
		this.lblRxDelay = new System.Windows.Forms.Label();
		this.cmbRxDelay = new System.Windows.Forms.ComboBox();
		this.lblBackTime = new System.Windows.Forms.Label();
		this.cmbBackTime = new System.Windows.Forms.ComboBox();
		this.lblPriorityCh = new System.Windows.Forms.Label();
		this.cmbPriorityCh = new System.Windows.Forms.ComboBox();
		this.lblPriorityScan = new System.Windows.Forms.Label();
		this.cmbPriorityScan = new System.Windows.Forms.ComboBox();
		this.lblScanMode = new System.Windows.Forms.Label();
		this.cmbScanMode = new System.Windows.Forms.ComboBox();
		this.lblRevertCh = new System.Windows.Forms.Label();
		this.cmbRevertCh = new System.Windows.Forms.ComboBox();
		base.SuspendLayout();
		this.lblTxDelay.Location = new System.Drawing.Point(20, 170);
		this.lblTxDelay.Name = "lblTxDelay";
		this.lblTxDelay.Size = new System.Drawing.Size(160, 20);
		this.lblTxDelay.TabIndex = 10;
		this.lblTxDelay.Text = "发射恢复延迟时间";
		this.lblTxDelay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.cmbTxDelay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbTxDelay.FormattingEnabled = true;
		this.cmbTxDelay.Location = new System.Drawing.Point(190, 170);
		this.cmbTxDelay.Name = "cmbTxDelay";
		this.cmbTxDelay.Size = new System.Drawing.Size(140, 20);
		this.cmbTxDelay.TabIndex = 11;
		this.lblRxDelay.Location = new System.Drawing.Point(20, 140);
		this.lblRxDelay.Name = "lblRxDelay";
		this.lblRxDelay.Size = new System.Drawing.Size(160, 20);
		this.lblRxDelay.TabIndex = 8;
		this.lblRxDelay.Text = "接收恢复延迟时间";
		this.lblRxDelay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.cmbRxDelay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbRxDelay.FormattingEnabled = true;
		this.cmbRxDelay.Location = new System.Drawing.Point(190, 140);
		this.cmbRxDelay.Name = "cmbRxDelay";
		this.cmbRxDelay.Size = new System.Drawing.Size(140, 20);
		this.cmbRxDelay.TabIndex = 9;
		this.lblBackTime.Location = new System.Drawing.Point(20, 110);
		this.lblBackTime.Name = "lblBackTime";
		this.lblBackTime.Size = new System.Drawing.Size(160, 20);
		this.lblBackTime.TabIndex = 6;
		this.lblBackTime.Text = "回扫时间";
		this.lblBackTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.cmbBackTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbBackTime.FormattingEnabled = true;
		this.cmbBackTime.Location = new System.Drawing.Point(190, 110);
		this.cmbBackTime.Name = "cmbBackTime";
		this.cmbBackTime.Size = new System.Drawing.Size(140, 20);
		this.cmbBackTime.TabIndex = 7;
		this.lblPriorityCh.Location = new System.Drawing.Point(20, 80);
		this.lblPriorityCh.Name = "lblPriorityCh";
		this.lblPriorityCh.Size = new System.Drawing.Size(160, 20);
		this.lblPriorityCh.TabIndex = 4;
		this.lblPriorityCh.Text = "优先扫描信道号";
		this.lblPriorityCh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.cmbPriorityCh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbPriorityCh.FormattingEnabled = true;
		this.cmbPriorityCh.Location = new System.Drawing.Point(190, 80);
		this.cmbPriorityCh.Name = "cmbPriorityCh";
		this.cmbPriorityCh.Size = new System.Drawing.Size(140, 20);
		this.cmbPriorityCh.TabIndex = 5;
		this.lblPriorityScan.Location = new System.Drawing.Point(20, 50);
		this.lblPriorityScan.Name = "lblPriorityScan";
		this.lblPriorityScan.Size = new System.Drawing.Size(160, 20);
		this.lblPriorityScan.TabIndex = 2;
		this.lblPriorityScan.Text = "优先扫描";
		this.lblPriorityScan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.cmbPriorityScan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbPriorityScan.FormattingEnabled = true;
		this.cmbPriorityScan.Location = new System.Drawing.Point(190, 50);
		this.cmbPriorityScan.Name = "cmbPriorityScan";
		this.cmbPriorityScan.Size = new System.Drawing.Size(140, 20);
		this.cmbPriorityScan.TabIndex = 3;
		this.lblScanMode.Location = new System.Drawing.Point(20, 20);
		this.lblScanMode.Name = "lblScanMode";
		this.lblScanMode.Size = new System.Drawing.Size(160, 20);
		this.lblScanMode.TabIndex = 0;
		this.lblScanMode.Text = "扫描模式";
		this.lblScanMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.cmbScanMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbScanMode.FormattingEnabled = true;
		this.cmbScanMode.Location = new System.Drawing.Point(190, 20);
		this.cmbScanMode.Name = "cmbScanMode";
		this.cmbScanMode.Size = new System.Drawing.Size(140, 20);
		this.cmbScanMode.TabIndex = 1;
		this.lblRevertCh.Location = new System.Drawing.Point(20, 200);
		this.lblRevertCh.Name = "lblRevertCh";
		this.lblRevertCh.Size = new System.Drawing.Size(160, 20);
		this.lblRevertCh.TabIndex = 12;
		this.lblRevertCh.Text = "返回信道类型";
		this.lblRevertCh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.cmbRevertCh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbRevertCh.FormattingEnabled = true;
		this.cmbRevertCh.Location = new System.Drawing.Point(190, 200);
		this.cmbRevertCh.Name = "cmbRevertCh";
		this.cmbRevertCh.Size = new System.Drawing.Size(140, 20);
		this.cmbRevertCh.TabIndex = 13;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.cmbScanMode);
		base.Controls.Add(this.cmbPriorityScan);
		base.Controls.Add(this.lblScanMode);
		base.Controls.Add(this.cmbPriorityCh);
		base.Controls.Add(this.lblPriorityScan);
		base.Controls.Add(this.cmbBackTime);
		base.Controls.Add(this.lblPriorityCh);
		base.Controls.Add(this.cmbRxDelay);
		base.Controls.Add(this.lblBackTime);
		base.Controls.Add(this.cmbRevertCh);
		base.Controls.Add(this.cmbTxDelay);
		base.Controls.Add(this.lblRevertCh);
		base.Controls.Add(this.lblRxDelay);
		base.Controls.Add(this.lblTxDelay);
		base.Name = "ScanBasicControl";
		base.Size = new System.Drawing.Size(350, 240);
		base.Load += new System.EventHandler(ScanBasicControl_Load);
		base.ResumeLayout(false);
	}

	public ScanBasicControl()
	{
		InitializeComponent();
	}

	private void ScanBasicControl_Load(object sender, EventArgs e)
	{
		DataToView();
	}

	public void InitView()
	{
		cmbScanMode.Init(Lang.SZ_SCAN_MODE);
		cmbPriorityScan.Init(Lang.SZ_PRIORITY_SCAN);
		cmbPriorityCh.Init(1, 16, 1);
		cmbBackTime.Init(5, 50, 1, 0.1);
		cmbRxDelay.Init(1, 50, 1, 0.1);
		cmbTxDelay.Init(1, 50, 1, 0.1);
		cmbRevertCh.Init(Lang.SZ_REVERT_CH);
	}

	public void DataToView()
	{
		InitView();
		ScanBasicModel ScanBasicModel = Cps.GetInstance().ScanBasicModel;
		cmbScanMode.SetCurSel(ScanBasicModel.ScanMode);
		cmbPriorityScan.SetCurSel(ScanBasicModel.PriorityScan);
		cmbPriorityCh.Text = ScanBasicModel.PriorityCh;
		cmbBackTime.Text = ScanBasicModel.BackTime;
		cmbRxDelay.Text = ScanBasicModel.RxDelay;
		cmbTxDelay.Text = ScanBasicModel.TxDelay;
		cmbRevertCh.SetCurSel(ScanBasicModel.RevertCh);
	}

	public void ViewToData()
	{
		ScanBasicModel ScanBasicModel = Cps.GetInstance().ScanBasicModel;
		ScanBasicModel.ScanMode = cmbScanMode.SelectedIndex;
		ScanBasicModel.PriorityScan = cmbPriorityScan.SelectedIndex;
		ScanBasicModel.PriorityCh = cmbPriorityCh.Text;
		ScanBasicModel.BackTime = cmbBackTime.Text;
		ScanBasicModel.RxDelay = cmbRxDelay.Text;
		ScanBasicModel.TxDelay = cmbTxDelay.Text;
		ScanBasicModel.RevertCh = cmbRevertCh.SelectedIndex;
	}

	public void LoadLanguageText(string section)
	{
		foreach (Control control in base.Controls)
		{
			LangHelper.ApplyControl(section, control);
		}
	}
}
