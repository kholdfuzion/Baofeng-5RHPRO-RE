using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Radio;

public class DtmfBasicControl : UserControl, IViewData
{
	private IContainer components;

	private Label lblKillCode;

	private TextBox txtKillCode;

	private Label lblStunCode;

	private TextBox txtStunCode;

	private Label lblDownCode;

	private TextBox txtDownCode;

	private Label lblUpCode;

	private TextBox txtUpCode;

	private Label lblTxDelay;

	private ComboBox cmbTxDelay;

	private Label lblSideTone;

	private ComboBox cmbSideTone;

	private Label lblPttidPause;

	private ComboBox cmbPttidPause;

	private Label lblFirstDigit;

	private ComboBox cmbFirstDigit;

	private Label lblPreCarrier;

	private ComboBox cmbPreCarrier;

	private Label lblRate;

	private ComboBox cmbRate;

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
		this.lblKillCode = new System.Windows.Forms.Label();
		this.txtKillCode = new System.Windows.Forms.TextBox();
		this.lblStunCode = new System.Windows.Forms.Label();
		this.txtStunCode = new System.Windows.Forms.TextBox();
		this.lblDownCode = new System.Windows.Forms.Label();
		this.txtDownCode = new System.Windows.Forms.TextBox();
		this.lblUpCode = new System.Windows.Forms.Label();
		this.txtUpCode = new System.Windows.Forms.TextBox();
		this.lblTxDelay = new System.Windows.Forms.Label();
		this.cmbTxDelay = new System.Windows.Forms.ComboBox();
		this.lblSideTone = new System.Windows.Forms.Label();
		this.cmbSideTone = new System.Windows.Forms.ComboBox();
		this.lblPttidPause = new System.Windows.Forms.Label();
		this.cmbPttidPause = new System.Windows.Forms.ComboBox();
		this.lblFirstDigit = new System.Windows.Forms.Label();
		this.cmbFirstDigit = new System.Windows.Forms.ComboBox();
		this.lblPreCarrier = new System.Windows.Forms.Label();
		this.cmbPreCarrier = new System.Windows.Forms.ComboBox();
		this.lblRate = new System.Windows.Forms.Label();
		this.cmbRate = new System.Windows.Forms.ComboBox();
		base.SuspendLayout();
		this.lblKillCode.Location = new System.Drawing.Point(20, 290);
		this.lblKillCode.Name = "lblKillCode";
		this.lblKillCode.Size = new System.Drawing.Size(160, 20);
		this.lblKillCode.TabIndex = 18;
		this.lblKillCode.Text = "遥毙码";
		this.lblKillCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtKillCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtKillCode.Location = new System.Drawing.Point(190, 290);
		this.txtKillCode.Name = "txtKillCode";
		this.txtKillCode.Size = new System.Drawing.Size(160, 21);
		this.txtKillCode.TabIndex = 19;
		this.txtKillCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDtmfCode_KeyPress);
		this.lblStunCode.Location = new System.Drawing.Point(20, 260);
		this.lblStunCode.Name = "lblStunCode";
		this.lblStunCode.Size = new System.Drawing.Size(160, 20);
		this.lblStunCode.TabIndex = 16;
		this.lblStunCode.Text = "遥晕码";
		this.lblStunCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtStunCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtStunCode.Location = new System.Drawing.Point(190, 260);
		this.txtStunCode.Name = "txtStunCode";
		this.txtStunCode.Size = new System.Drawing.Size(160, 21);
		this.txtStunCode.TabIndex = 17;
		this.txtStunCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDtmfCode_KeyPress);
		this.lblDownCode.Location = new System.Drawing.Point(20, 230);
		this.lblDownCode.Name = "lblDownCode";
		this.lblDownCode.Size = new System.Drawing.Size(160, 20);
		this.lblDownCode.TabIndex = 14;
		this.lblDownCode.Text = "下线码";
		this.lblDownCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtDownCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDownCode.Location = new System.Drawing.Point(190, 230);
		this.txtDownCode.Name = "txtDownCode";
		this.txtDownCode.Size = new System.Drawing.Size(160, 21);
		this.txtDownCode.TabIndex = 15;
		this.txtDownCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDtmfCode_KeyPress);
		this.lblUpCode.Location = new System.Drawing.Point(20, 200);
		this.lblUpCode.Name = "lblUpCode";
		this.lblUpCode.Size = new System.Drawing.Size(160, 20);
		this.lblUpCode.TabIndex = 12;
		this.lblUpCode.Text = "上线码";
		this.lblUpCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtUpCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtUpCode.Location = new System.Drawing.Point(190, 200);
		this.txtUpCode.Name = "txtUpCode";
		this.txtUpCode.Size = new System.Drawing.Size(160, 21);
		this.txtUpCode.TabIndex = 13;
		this.txtUpCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDtmfCode_KeyPress);
		this.lblTxDelay.Location = new System.Drawing.Point(20, 170);
		this.lblTxDelay.Name = "lblTxDelay";
		this.lblTxDelay.Size = new System.Drawing.Size(160, 20);
		this.lblTxDelay.TabIndex = 10;
		this.lblTxDelay.Text = "发码后延迟时间";
		this.lblTxDelay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.cmbTxDelay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbTxDelay.FormattingEnabled = true;
		this.cmbTxDelay.Location = new System.Drawing.Point(190, 170);
		this.cmbTxDelay.Name = "cmbTxDelay";
		this.cmbTxDelay.Size = new System.Drawing.Size(160, 20);
		this.cmbTxDelay.TabIndex = 11;
		this.lblSideTone.Location = new System.Drawing.Point(20, 140);
		this.lblSideTone.Name = "lblSideTone";
		this.lblSideTone.Size = new System.Drawing.Size(160, 20);
		this.lblSideTone.TabIndex = 8;
		this.lblSideTone.Text = "侧音";
		this.lblSideTone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.cmbSideTone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbSideTone.FormattingEnabled = true;
		this.cmbSideTone.Location = new System.Drawing.Point(190, 140);
		this.cmbSideTone.Name = "cmbSideTone";
		this.cmbSideTone.Size = new System.Drawing.Size(160, 20);
		this.cmbSideTone.TabIndex = 9;
		this.lblPttidPause.Location = new System.Drawing.Point(20, 110);
		this.lblPttidPause.Name = "lblPttidPause";
		this.lblPttidPause.Size = new System.Drawing.Size(160, 20);
		this.lblPttidPause.TabIndex = 6;
		this.lblPttidPause.Text = "PTTID 暂停";
		this.lblPttidPause.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.cmbPttidPause.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbPttidPause.FormattingEnabled = true;
		this.cmbPttidPause.Location = new System.Drawing.Point(190, 110);
		this.cmbPttidPause.Name = "cmbPttidPause";
		this.cmbPttidPause.Size = new System.Drawing.Size(160, 20);
		this.cmbPttidPause.TabIndex = 7;
		this.lblFirstDigit.Location = new System.Drawing.Point(20, 80);
		this.lblFirstDigit.Name = "lblFirstDigit";
		this.lblFirstDigit.Size = new System.Drawing.Size(160, 20);
		this.lblFirstDigit.TabIndex = 4;
		this.lblFirstDigit.Text = "首位数码时间";
		this.lblFirstDigit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.cmbFirstDigit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbFirstDigit.FormattingEnabled = true;
		this.cmbFirstDigit.Location = new System.Drawing.Point(190, 80);
		this.cmbFirstDigit.Name = "cmbFirstDigit";
		this.cmbFirstDigit.Size = new System.Drawing.Size(160, 20);
		this.cmbFirstDigit.TabIndex = 5;
		this.lblPreCarrier.Location = new System.Drawing.Point(20, 50);
		this.lblPreCarrier.Name = "lblPreCarrier";
		this.lblPreCarrier.Size = new System.Drawing.Size(160, 20);
		this.lblPreCarrier.TabIndex = 2;
		this.lblPreCarrier.Text = "预载波时间";
		this.lblPreCarrier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.cmbPreCarrier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbPreCarrier.FormattingEnabled = true;
		this.cmbPreCarrier.Location = new System.Drawing.Point(190, 50);
		this.cmbPreCarrier.Name = "cmbPreCarrier";
		this.cmbPreCarrier.Size = new System.Drawing.Size(160, 20);
		this.cmbPreCarrier.TabIndex = 3;
		this.lblRate.Location = new System.Drawing.Point(20, 20);
		this.lblRate.Name = "lblRate";
		this.lblRate.Size = new System.Drawing.Size(160, 20);
		this.lblRate.TabIndex = 0;
		this.lblRate.Text = "速率";
		this.lblRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.cmbRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbRate.FormattingEnabled = true;
		this.cmbRate.Location = new System.Drawing.Point(190, 20);
		this.cmbRate.Name = "cmbRate";
		this.cmbRate.Size = new System.Drawing.Size(160, 20);
		this.cmbRate.TabIndex = 1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.cmbRate);
		base.Controls.Add(this.cmbPreCarrier);
		base.Controls.Add(this.lblRate);
		base.Controls.Add(this.cmbFirstDigit);
		base.Controls.Add(this.lblPreCarrier);
		base.Controls.Add(this.cmbPttidPause);
		base.Controls.Add(this.lblFirstDigit);
		base.Controls.Add(this.cmbSideTone);
		base.Controls.Add(this.lblPttidPause);
		base.Controls.Add(this.cmbTxDelay);
		base.Controls.Add(this.lblSideTone);
		base.Controls.Add(this.lblTxDelay);
		base.Controls.Add(this.txtUpCode);
		base.Controls.Add(this.lblUpCode);
		base.Controls.Add(this.txtDownCode);
		base.Controls.Add(this.lblDownCode);
		base.Controls.Add(this.txtStunCode);
		base.Controls.Add(this.lblStunCode);
		base.Controls.Add(this.txtKillCode);
		base.Controls.Add(this.lblKillCode);
		base.Name = "DtmfBasicControl";
		base.Size = new System.Drawing.Size(370, 330);
		base.Load += new System.EventHandler(DtmfBasicControl_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	public DtmfBasicControl()
	{
		InitializeComponent();
	}

	private void DtmfBasicControl_Load(object sender, EventArgs e)
	{
	}

	public void InitView()
	{
		cmbRate.Init(Lang.SZ_RATE);
		cmbPreCarrier.Init(0, 2500, 10);
		cmbTxDelay.Init(0, 2500, 10);
		cmbFirstDigit.Init(0, 2500, 10);
		cmbPttidPause.Init(Lang.SZ_OFF, 5, 75, 1);
		cmbSideTone.Init(Lang.SZ_SIDE_TONE);
		txtUpCode.MaxLength = 16;
		txtDownCode.MaxLength = 16;
		txtStunCode.MaxLength = 16;
		txtKillCode.MaxLength = 16;
	}

	public void DataToView()
	{
		DtmfBasicModel DtmfBasicModel = Cps.GetInstance().DtmfBasicModel;
		InitView();
		cmbRate.SetCurSel(DtmfBasicModel.Rate);
		cmbPreCarrier.Text = DtmfBasicModel.PreCarrier;
		cmbFirstDigit.Text = DtmfBasicModel.FirstDigit;
		cmbTxDelay.Text = DtmfBasicModel.TxDelay;
		cmbPttidPause.Text = DtmfBasicModel.PttidPause;
		cmbSideTone.SetCurSel(DtmfBasicModel.SideTone);
		txtUpCode.Text = DtmfBasicModel.UpCode;
		txtDownCode.Text = DtmfBasicModel.DownCode;
		txtStunCode.Text = DtmfBasicModel.StunCode;
		txtKillCode.Text = DtmfBasicModel.KillCode;
	}

	public void ViewToData()
	{
		DtmfBasicModel DtmfBasicModel = Cps.GetInstance().DtmfBasicModel;
		DtmfBasicModel.Rate = cmbRate.SelectedIndex;
		DtmfBasicModel.PreCarrier = cmbPreCarrier.Text;
		DtmfBasicModel.FirstDigit = cmbFirstDigit.Text;
		DtmfBasicModel.TxDelay = cmbTxDelay.Text;
		DtmfBasicModel.PttidPause = cmbPttidPause.Text;
		DtmfBasicModel.SideTone = cmbSideTone.SelectedIndex;
		DtmfBasicModel.UpCode = txtUpCode.Text;
		DtmfBasicModel.DownCode = txtDownCode.Text;
		DtmfBasicModel.StunCode = txtStunCode.Text;
		DtmfBasicModel.KillCode = txtKillCode.Text;
	}

	public void LoadLanguageText(string section)
	{
		foreach (Control control in base.Controls)
		{
			LangHelper.ApplyControl(section, control);
		}
	}

	private void txtDtmfCode_KeyPress(object sender, KeyPressEventArgs e)
	{
		if ("0123456789ABCD*#".IndexOf(char.ToUpper(e.KeyChar)) < 0 && e.KeyChar != '\b')
		{
			e.Handled = true;
		}
	}
}
