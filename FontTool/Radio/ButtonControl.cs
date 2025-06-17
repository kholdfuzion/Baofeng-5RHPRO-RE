using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Radio;

public class ButtonControl : UserControl, IViewData
{
	private IContainer components;

	private Label lblSk2Long;

	private ComboBox cmbSk2Long;

	private Label lblSk2Short;

	private ComboBox cmbSk2Short;

	private Label lblSk1Long;

	private ComboBox cmbSk1Long;

	private Label lblSk1Short;

	private ComboBox cmbSk1Short;

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
		this.lblSk2Long = new System.Windows.Forms.Label();
		this.cmbSk2Long = new System.Windows.Forms.ComboBox();
		this.lblSk2Short = new System.Windows.Forms.Label();
		this.cmbSk2Short = new System.Windows.Forms.ComboBox();
		this.lblSk1Long = new System.Windows.Forms.Label();
		this.cmbSk1Long = new System.Windows.Forms.ComboBox();
		this.lblSk1Short = new System.Windows.Forms.Label();
		this.cmbSk1Short = new System.Windows.Forms.ComboBox();
		base.SuspendLayout();
		this.lblSk2Long.Location = new System.Drawing.Point(20, 110);
		this.lblSk2Long.Name = "lblSk2Long";
		this.lblSk2Long.Size = new System.Drawing.Size(160, 20);
		this.lblSk2Long.TabIndex = 6;
		this.lblSk2Long.Text = "SK2 长按";
		this.lblSk2Long.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblSk2Long.Visible = false;
		this.cmbSk2Long.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbSk2Long.FormattingEnabled = true;
		this.cmbSk2Long.Location = new System.Drawing.Point(190, 110);
		this.cmbSk2Long.MaxDropDownItems = 10;
		this.cmbSk2Long.Name = "cmbSk2Long";
		this.cmbSk2Long.Size = new System.Drawing.Size(140, 20);
		this.cmbSk2Long.TabIndex = 7;
		this.cmbSk2Long.Visible = false;
		this.lblSk2Short.Location = new System.Drawing.Point(20, 80);
		this.lblSk2Short.Name = "lblSk2Short";
		this.lblSk2Short.Size = new System.Drawing.Size(160, 20);
		this.lblSk2Short.TabIndex = 4;
		this.lblSk2Short.Text = "SK2 短按";
		this.lblSk2Short.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblSk2Short.Visible = false;
		this.cmbSk2Short.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbSk2Short.FormattingEnabled = true;
		this.cmbSk2Short.Location = new System.Drawing.Point(190, 80);
		this.cmbSk2Short.MaxDropDownItems = 10;
		this.cmbSk2Short.Name = "cmbSk2Short";
		this.cmbSk2Short.Size = new System.Drawing.Size(140, 20);
		this.cmbSk2Short.TabIndex = 5;
		this.cmbSk2Short.Visible = false;
		this.lblSk1Long.Location = new System.Drawing.Point(20, 50);
		this.lblSk1Long.Name = "lblSk1Long";
		this.lblSk1Long.Size = new System.Drawing.Size(160, 20);
		this.lblSk1Long.TabIndex = 2;
		this.lblSk1Long.Text = "SK1 长按";
		this.lblSk1Long.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblSk1Long.Visible = false;
		this.cmbSk1Long.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbSk1Long.FormattingEnabled = true;
		this.cmbSk1Long.Location = new System.Drawing.Point(190, 50);
		this.cmbSk1Long.MaxDropDownItems = 10;
		this.cmbSk1Long.Name = "cmbSk1Long";
		this.cmbSk1Long.Size = new System.Drawing.Size(140, 20);
		this.cmbSk1Long.TabIndex = 3;
		this.cmbSk1Long.Visible = false;
		this.lblSk1Short.Location = new System.Drawing.Point(20, 20);
		this.lblSk1Short.Name = "lblSk1Short";
		this.lblSk1Short.Size = new System.Drawing.Size(160, 20);
		this.lblSk1Short.TabIndex = 0;
		this.lblSk1Short.Text = "SK1 短按";
		this.lblSk1Short.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lblSk1Short.Visible = false;
		this.cmbSk1Short.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbSk1Short.FormattingEnabled = true;
		this.cmbSk1Short.Location = new System.Drawing.Point(190, 20);
		this.cmbSk1Short.MaxDropDownItems = 10;
		this.cmbSk1Short.Name = "cmbSk1Short";
		this.cmbSk1Short.Size = new System.Drawing.Size(140, 20);
		this.cmbSk1Short.TabIndex = 1;
		this.cmbSk1Short.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.cmbSk1Short);
		base.Controls.Add(this.cmbSk1Long);
		base.Controls.Add(this.lblSk1Short);
		base.Controls.Add(this.cmbSk2Short);
		base.Controls.Add(this.lblSk1Long);
		base.Controls.Add(this.cmbSk2Long);
		base.Controls.Add(this.lblSk2Short);
		base.Controls.Add(this.lblSk2Long);
		base.Name = "ButtonControl";
		base.Size = new System.Drawing.Size(350, 150);
		base.Load += new System.EventHandler(ButtonControl_Load);
		base.ResumeLayout(false);
	}

	public ButtonControl()
	{
		InitializeComponent();
	}

	private void ButtonControl_Load(object sender, EventArgs e)
	{
	}

	public void InitView()
	{
		int i = 0;
		cmbSk1Short.Items.Clear();
		cmbSk1Long.Items.Clear();
		cmbSk2Short.Items.Clear();
		cmbSk2Long.Items.Clear();
		for (i = 0; i < Lang.SZ_BUTTON_KEY.Length; i++)
		{
			if (i != 7 && i != 8)
			{
				cmbSk1Short.Items.Add(Lang.SZ_BUTTON_KEY[i]);
				cmbSk1Long.Items.Add(Lang.SZ_BUTTON_KEY[i]);
				cmbSk2Short.Items.Add(Lang.SZ_BUTTON_KEY[i]);
				cmbSk2Long.Items.Add(Lang.SZ_BUTTON_KEY[i]);
			}
		}
	}

	public void DataToView()
	{
		ButtonModel ButtonModel = Cps.GetInstance().ButtonModel;
		InitView();
		cmbSk1Short.Text = ButtonModel.Sk1Short;
		cmbSk1Long.Text = ButtonModel.Sk1Long;
		cmbSk2Short.Text = ButtonModel.Sk2Short;
		cmbSk2Long.Text = ButtonModel.Sk2Long;
	}

	public void ViewToData()
	{
		ButtonModel ButtonModel = Cps.GetInstance().ButtonModel;
		ButtonModel.Sk1Short = cmbSk1Short.Text;
		ButtonModel.Sk1Long = cmbSk1Long.Text;
		ButtonModel.Sk2Short = cmbSk2Short.Text;
		ButtonModel.Sk2Long = cmbSk2Long.Text;
	}

	public void LoadLanguageText(string section)
	{
		foreach (Control control in base.Controls)
		{
			LangHelper.ApplyControl(section, control);
		}
	}
}
