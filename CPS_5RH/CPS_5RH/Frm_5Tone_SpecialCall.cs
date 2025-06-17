using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CPS_5RH.Data;

namespace CPS_5RH;

public class Frm_5Tone_SpecialCall : Form
{
	private DatFiveToneEncInfo fdata = null;

	private DatFiveToneDecInfo fdata1 = null;

	private int posAddr;

	private byte calltype;

	private string callcontent = "";

	private string items0 = "";

	private IContainer components = null;

	private ComboBox cobox_CallType;

	private Label lbl_Type;

	private Label lbl_Des;

	private TextBox tb_Des;

	private ComboBox coBox_Sep;

	private Label lbl_Serp;

	private Label lbl_Cont;

	private TextBox tb_Conent;

	private Button btn_OK;

	private Button btn_Cancel;

	public Frm_5Tone_SpecialCall(DatFiveToneEncInfo fivedata, DatFiveToneDecInfo fivedec, int idx)
	{
		InitializeComponent();
		fdata = fivedata;
		fdata1 = fivedec;
		posAddr = idx;
		cobox_CallType.SelectedIndex = 0;
	}

	private void Cells_KeyPress(object sender, KeyPressEventArgs e)
	{
		string str = ((TextBox)sender).Text;
		e.Handled = e.KeyChar < '0' || e.KeyChar > '9';
		if (e.KeyChar == '\b')
		{
			e.Handled = false;
		}
	}

	private void Cont_Cells_KeyPress(object sender, KeyPressEventArgs e)
	{
		if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar < 'a' || e.KeyChar > 'z') && (e.KeyChar < 'A' || e.KeyChar > 'Z') && e.KeyChar != '\b')
		{
			e.Handled = true;
		}
		else if (e.KeyChar >= 'a' && e.KeyChar <= 'z')
		{
			e.KeyChar -= ' ';
		}
	}

	private void cobox_CallType_SelectedIndexChanged(object sender, EventArgs e)
	{
		calltype = (byte)cobox_CallType.SelectedIndex;
		switch (cobox_CallType.SelectedIndex)
		{
		case 1:
			lbl_Des.Visible = true;
			tb_Des.Visible = true;
			lbl_Cont.Visible = false;
			tb_Conent.Visible = false;
			lbl_Serp.Visible = true;
			coBox_Sep.Visible = true;
			FiltString(fdata.GetEncStand(posAddr));
			tb_Des.Text = "12345";
			coBox_Sep.SelectedIndex = 0;
			break;
		case 2:
			lbl_Des.Visible = false;
			tb_Des.Visible = false;
			lbl_Cont.Visible = false;
			tb_Conent.Visible = false;
			lbl_Serp.Visible = false;
			coBox_Sep.Visible = false;
			break;
		default:
			lbl_Des.Visible = false;
			tb_Des.Visible = false;
			lbl_Cont.Visible = false;
			tb_Conent.Visible = false;
			lbl_Serp.Visible = false;
			coBox_Sep.Visible = false;
			break;
		}
	}

	private void FiltString(int code)
	{
		coBox_Sep.Items.Clear();
		coBox_Sep.Items.Add(items0);
		if (code == 13 || code == 8 || code == 5)
		{
			coBox_Sep.Items.Add("A");
			coBox_Sep.Items.Add("B");
			coBox_Sep.Items.Add("C");
			coBox_Sep.Items.Add("D");
			coBox_Sep.Items.Add("*");
			return;
		}
		switch (code)
		{
		case 2:
			coBox_Sep.Items.Add("A");
			coBox_Sep.Items.Add("*");
			break;
		default:
			if (code != 10)
			{
				coBox_Sep.Items.Add("A");
				coBox_Sep.Items.Add("B");
				coBox_Sep.Items.Add("C");
				coBox_Sep.Items.Add("D");
				coBox_Sep.Items.Add("*");
				coBox_Sep.Items.Add("#");
				break;
			}
			goto case 12;
		case 12:
			coBox_Sep.Items.Add("*");
			break;
		}
	}

	private string Adjust_GetStrVal(string val)
	{
		ASCIIEncoding code = new ASCIIEncoding();
		byte[] tmB = code.GetBytes(val);
		if (tmB.Length > 1)
		{
			for (int i = 0; i < tmB.Length - 1; i++)
			{
				if (tmB[i] == tmB[i + 1])
				{
					tmB[i + 1] = 42;
				}
			}
		}
		return code.GetString(tmB, 0, tmB.Length);
	}

	private void btn_OK_Click(object sender, EventArgs e)
	{
		string tmp = "";
		if (calltype == 1)
		{
			callcontent = tb_Des.Text.PadLeft(5, '0');
			if (coBox_Sep.SelectedIndex > 0)
			{
				callcontent += coBox_Sep.Text;
				callcontent += fdata1.Did;
			}
			callcontent = Adjust_GetStrVal(callcontent);
			fdata.SetEncScall(posAddr, (byte)(calltype + 1));
			fdata.SetEncCodeLen(posAddr, (byte)callcontent.Length);
			fdata.SetEncID(posAddr, callcontent);
		}
		else if (calltype != fdata.GetEncScall(posAddr))
		{
			callcontent = fdata1.Did;
			fdata.SetEncCodeLen(posAddr, (byte)callcontent.Length);
			fdata.SetEncID(posAddr, callcontent);
			fdata.SetEncScall(posAddr, (byte)(calltype + 1));
		}
		base.DialogResult = DialogResult.OK;
	}

	private void btn_Cancel_Click(object sender, EventArgs e)
	{
		base.DialogResult = DialogResult.Cancel;
	}

	private void Frm_5Tone_SpecialCall_Load(object sender, EventArgs e)
	{
		string[] strItems = new string[10];
		string tmp = "";
		if (FormMain.lang == "Chinese")
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
		}
		ComponentResourceManager crm = new ComponentResourceManager(typeof(Frm_5Tone_SpecialCall));
		LanguageSel.LoadLanguage(this, typeof(Frm_5Tone_SpecialCall));
		for (int i = 0; i < 5; i++)
		{
			LanguageSel.ElseCombobox.TryGetValue(i.ToString(), out tmp);
			strItems[i] = tmp;
		}
		cobox_CallType.Items[0] = strItems[0];
		items0 = strItems[0];
	}

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
		this.cobox_CallType = new System.Windows.Forms.ComboBox();
		this.lbl_Type = new System.Windows.Forms.Label();
		this.lbl_Des = new System.Windows.Forms.Label();
		this.tb_Des = new System.Windows.Forms.TextBox();
		this.coBox_Sep = new System.Windows.Forms.ComboBox();
		this.lbl_Serp = new System.Windows.Forms.Label();
		this.lbl_Cont = new System.Windows.Forms.Label();
		this.tb_Conent = new System.Windows.Forms.TextBox();
		this.btn_OK = new System.Windows.Forms.Button();
		this.btn_Cancel = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.cobox_CallType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cobox_CallType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cobox_CallType.FormattingEnabled = true;
		this.cobox_CallType.Items.AddRange(new object[3] { "无", "ANI", "PTTID" });
		this.cobox_CallType.Location = new System.Drawing.Point(182, 35);
		this.cobox_CallType.Name = "cobox_CallType";
		this.cobox_CallType.Size = new System.Drawing.Size(88, 20);
		this.cobox_CallType.TabIndex = 0;
		this.cobox_CallType.SelectedIndexChanged += new System.EventHandler(cobox_CallType_SelectedIndexChanged);
		this.lbl_Type.Location = new System.Drawing.Point(45, 35);
		this.lbl_Type.Name = "lbl_Type";
		this.lbl_Type.Size = new System.Drawing.Size(133, 20);
		this.lbl_Type.TabIndex = 1;
		this.lbl_Type.Text = "呼叫类型";
		this.lbl_Type.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl_Des.Location = new System.Drawing.Point(47, 61);
		this.lbl_Des.Name = "lbl_Des";
		this.lbl_Des.Size = new System.Drawing.Size(131, 20);
		this.lbl_Des.TabIndex = 2;
		this.lbl_Des.Text = "对方码";
		this.lbl_Des.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tb_Des.Location = new System.Drawing.Point(182, 61);
		this.tb_Des.MaxLength = 5;
		this.tb_Des.Name = "tb_Des";
		this.tb_Des.Size = new System.Drawing.Size(88, 21);
		this.tb_Des.TabIndex = 3;
		this.tb_Des.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Cells_KeyPress);
		this.coBox_Sep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Sep.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Sep.FormattingEnabled = true;
		this.coBox_Sep.Items.AddRange(new object[7] { "无", "A", "B", "C", "D", "*", "#" });
		this.coBox_Sep.Location = new System.Drawing.Point(182, 88);
		this.coBox_Sep.Name = "coBox_Sep";
		this.coBox_Sep.Size = new System.Drawing.Size(88, 20);
		this.coBox_Sep.TabIndex = 4;
		this.lbl_Serp.Location = new System.Drawing.Point(27, 88);
		this.lbl_Serp.Name = "lbl_Serp";
		this.lbl_Serp.Size = new System.Drawing.Size(149, 20);
		this.lbl_Serp.TabIndex = 5;
		this.lbl_Serp.Text = "分隔符";
		this.lbl_Serp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl_Cont.Location = new System.Drawing.Point(45, 89);
		this.lbl_Cont.Name = "lbl_Cont";
		this.lbl_Cont.Size = new System.Drawing.Size(131, 20);
		this.lbl_Cont.TabIndex = 6;
		this.lbl_Cont.Text = "数传信息";
		this.lbl_Cont.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tb_Conent.Location = new System.Drawing.Point(182, 89);
		this.tb_Conent.MaxLength = 16;
		this.tb_Conent.Name = "tb_Conent";
		this.tb_Conent.Size = new System.Drawing.Size(88, 21);
		this.tb_Conent.TabIndex = 7;
		this.tb_Conent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Cont_Cells_KeyPress);
		this.btn_OK.Location = new System.Drawing.Point(102, 126);
		this.btn_OK.Name = "btn_OK";
		this.btn_OK.Size = new System.Drawing.Size(75, 23);
		this.btn_OK.TabIndex = 8;
		this.btn_OK.Text = "确定";
		this.btn_OK.UseVisualStyleBackColor = true;
		this.btn_OK.Click += new System.EventHandler(btn_OK_Click);
		this.btn_Cancel.Location = new System.Drawing.Point(228, 126);
		this.btn_Cancel.Name = "btn_Cancel";
		this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
		this.btn_Cancel.TabIndex = 9;
		this.btn_Cancel.Text = "取消";
		this.btn_Cancel.UseVisualStyleBackColor = true;
		this.btn_Cancel.Click += new System.EventHandler(btn_Cancel_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(370, 172);
		base.Controls.Add(this.btn_Cancel);
		base.Controls.Add(this.btn_OK);
		base.Controls.Add(this.tb_Conent);
		base.Controls.Add(this.lbl_Cont);
		base.Controls.Add(this.lbl_Serp);
		base.Controls.Add(this.coBox_Sep);
		base.Controls.Add(this.tb_Des);
		base.Controls.Add(this.lbl_Des);
		base.Controls.Add(this.lbl_Type);
		base.Controls.Add(this.cobox_CallType);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
		base.Name = "Frm_5Tone_SpecialCall";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "特殊呼叫";
		base.Load += new System.EventHandler(Frm_5Tone_SpecialCall_Load);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
