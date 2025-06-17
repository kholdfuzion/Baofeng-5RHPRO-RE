using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Radio;

public class DtmfControl : UserControl, IViewData
{
	private IContainer components;

	private DtmfBasicControl dtmfBasicControl;

	private DtmfContactControl dtmfContactControl;

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
		this.dtmfContactControl = new Radio.DtmfContactControl();
		this.dtmfBasicControl = new Radio.DtmfBasicControl();
		base.SuspendLayout();
		this.dtmfContactControl.Location = new System.Drawing.Point(370, 0);
		this.dtmfContactControl.Name = "dtmfContactControl";
		this.dtmfContactControl.Size = new System.Drawing.Size(300, 440);
		this.dtmfContactControl.TabIndex = 1;
		this.dtmfBasicControl.Location = new System.Drawing.Point(0, 0);
		this.dtmfBasicControl.Name = "dtmfBasicControl";
		this.dtmfBasicControl.Size = new System.Drawing.Size(370, 330);
		this.dtmfBasicControl.TabIndex = 0;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.dtmfContactControl);
		base.Controls.Add(this.dtmfBasicControl);
		base.Name = "DtmfControl";
		base.Size = new System.Drawing.Size(670, 440);
		base.Load += new System.EventHandler(DtmfControl_Load);
		base.ResumeLayout(false);
	}

	public DtmfControl()
	{
		InitializeComponent();
	}

	public void InitView()
	{
		foreach (object control in base.Controls)
		{
			if (control is IViewData view)
			{
				view.InitView();
			}
		}
	}

	public void DataToView()
	{
		foreach (object control in base.Controls)
		{
			if (control is IViewData view)
			{
				view.DataToView();
			}
		}
	}

	public void ViewToData()
	{
		foreach (object control in base.Controls)
		{
			if (control is IViewData view)
			{
				view.ViewToData();
			}
		}
	}

	public void LoadLanguageText(string section)
	{
		foreach (Control control in base.Controls)
		{
			LangHelper.ApplyControl(section, control);
		}
	}

	private void DtmfControl_Load(object sender, EventArgs e)
	{
	}
}
