using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Radio;

public class TotControl : UserControl, IViewData
{
	private IContainer components;

	private Label lblTotRekey;

	private ComboBox cmbTotRekey;

	private Label lblTotPreAlert;

	private ComboBox cmbTotPreAlert;

	private Label lblTot;

	private ComboBox cmbTot;

	public TotModel TotModel { get; set; }

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
		this.lblTotRekey = new System.Windows.Forms.Label();
		this.cmbTotRekey = new System.Windows.Forms.ComboBox();
		this.lblTotPreAlert = new System.Windows.Forms.Label();
		this.cmbTotPreAlert = new System.Windows.Forms.ComboBox();
		this.lblTot = new System.Windows.Forms.Label();
		this.cmbTot = new System.Windows.Forms.ComboBox();
		base.SuspendLayout();
		this.lblTotRekey.Location = new System.Drawing.Point(20, 80);
		this.lblTotRekey.Name = "lblTotRekey";
		this.lblTotRekey.Size = new System.Drawing.Size(160, 20);
		this.lblTotRekey.TabIndex = 4;
		this.lblTotRekey.Text = "TOT key again";
		this.lblTotRekey.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblTotRekey.Visible = false;
		this.cmbTotRekey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbTotRekey.FormattingEnabled = true;
		this.cmbTotRekey.Location = new System.Drawing.Point(190, 80);
		this.cmbTotRekey.MaxDropDownItems = 10;
		this.cmbTotRekey.Name = "cmbTotRekey";
		this.cmbTotRekey.Size = new System.Drawing.Size(140, 20);
		this.cmbTotRekey.TabIndex = 5;
		this.cmbTotRekey.Visible = false;
		this.lblTotPreAlert.Location = new System.Drawing.Point(20, 50);
		this.lblTotPreAlert.Name = "lblTotPreAlert";
		this.lblTotPreAlert.Size = new System.Drawing.Size(160, 20);
		this.lblTotPreAlert.TabIndex = 2;
		this.lblTotPreAlert.Text = "TOT Early warning";
		this.lblTotPreAlert.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblTotPreAlert.Visible = false;
		this.cmbTotPreAlert.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbTotPreAlert.FormattingEnabled = true;
		this.cmbTotPreAlert.Location = new System.Drawing.Point(190, 50);
		this.cmbTotPreAlert.MaxDropDownItems = 10;
		this.cmbTotPreAlert.Name = "cmbTotPreAlert";
		this.cmbTotPreAlert.Size = new System.Drawing.Size(140, 20);
		this.cmbTotPreAlert.TabIndex = 3;
		this.cmbTotPreAlert.Visible = false;
		this.lblTot.Location = new System.Drawing.Point(20, 20);
		this.lblTot.Name = "lblTot";
		this.lblTot.Size = new System.Drawing.Size(160, 20);
		this.lblTot.TabIndex = 0;
		this.lblTot.Text = "TOT";
		this.lblTot.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblTot.Visible = false;
		this.cmbTot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbTot.FormattingEnabled = true;
		this.cmbTot.Location = new System.Drawing.Point(190, 20);
		this.cmbTot.MaxDropDownItems = 10;
		this.cmbTot.Name = "cmbTot";
		this.cmbTot.Size = new System.Drawing.Size(140, 20);
		this.cmbTot.TabIndex = 1;
		this.cmbTot.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.cmbTot);
		base.Controls.Add(this.cmbTotPreAlert);
		base.Controls.Add(this.lblTot);
		base.Controls.Add(this.cmbTotRekey);
		base.Controls.Add(this.lblTotPreAlert);
		base.Controls.Add(this.lblTotRekey);
		base.Name = "TotControl";
		base.Size = new System.Drawing.Size(350, 120);
		base.Load += new System.EventHandler(TotControl_Load);
		base.ResumeLayout(false);
	}

	public TotControl()
	{
		InitializeComponent();
	}

	private void TotControl_Load(object sender, EventArgs e)
	{
		InitView();
		DataToView();
	}

	public void InitView()
	{
		cmbTot.Init(Lang.SZ_OFF, 30, 600, 30);
		cmbTotPreAlert.Init(Lang.SZ_OFF, 10, 60, 5);
		cmbTotRekey.Init("", 10, 60, 5);
	}

	public void DataToView()
	{
		TotModel TotModel = Cps.GetInstance().TotModel;
		InitView();
		cmbTot.Text = TotModel.Tot;
		cmbTotPreAlert.Text = TotModel.TotPreAlert;
		cmbTotRekey.Text = TotModel.TotRekey;
	}

	public void ViewToData()
	{
		TotModel TotModel = Cps.GetInstance().TotModel;
		TotModel.Tot = cmbTot.Text;
		TotModel.TotPreAlert = cmbTotPreAlert.Text;
		TotModel.TotRekey = cmbTotRekey.Text;
	}

	public void LoadLanguageText(string section)
	{
		foreach (Control control in base.Controls)
		{
			LangHelper.ApplyControl(section, control);
		}
	}
}
