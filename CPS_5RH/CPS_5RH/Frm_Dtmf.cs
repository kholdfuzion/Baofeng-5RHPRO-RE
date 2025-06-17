using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using CPS_5RH.Data;

namespace CPS_5RH;

public class Frm_Dtmf : Form
{
	private IContainer components = null;

	private TabControl Tab_Dtmf;

	private TabPage tab_ParaPage;

	private TabPage tab_EncPage;

	private ComboBox coBox_PttidPause;

	private Label label1;

	private TextBox tb_Eot;

	private TextBox tb_Bot;

	private ComboBox coBox_Grpcode;

	private Label lbl6;

	private Label lbl4;

	private Label lbl3;

	private Label lbl2;

	private Label lbl9;

	private Label lbl8;

	private Label lbl7;

	private NumericUpDown Num_CodeDly;

	private NumericUpDown Num_PreTim;

	private NumericUpDown Num_FirstCodeTim;

	private CheckBox ckBox_ANI;

	private Label label2;

	private Label label4;

	private ComboBox coBox_DecRsp;

	private Label label3;

	private ComboBox coBox_Sep;

	private Label label5;

	private Label label7;

	private Label label6;

	private TextBox tb_ID;

	private TextBox tb_Kill;

	private TextBox tb_Stun;

	private ComboBox coBox_CodeSpeed;

	private DataGridView dgv_DtmfEnc;

	private DataGridViewTextBoxColumn Col_DtmfNo;

	private DataGridViewTextBoxColumn Col_DtmfEnc;

	private Label label8;

	private ComboBox coBox_SideTone;

	private ComboBox Cobox_ResetTim;

	private DataApp tdata = null;

	private DataGridViewTextBoxEditingControl dgvTxt = null;

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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPS_5RH.Frm_Dtmf));
		this.Tab_Dtmf = new System.Windows.Forms.TabControl();
		this.tab_ParaPage = new System.Windows.Forms.TabPage();
		this.Cobox_ResetTim = new System.Windows.Forms.ComboBox();
		this.label8 = new System.Windows.Forms.Label();
		this.coBox_SideTone = new System.Windows.Forms.ComboBox();
		this.coBox_CodeSpeed = new System.Windows.Forms.ComboBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.tb_ID = new System.Windows.Forms.TextBox();
		this.tb_Kill = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.tb_Stun = new System.Windows.Forms.TextBox();
		this.coBox_DecRsp = new System.Windows.Forms.ComboBox();
		this.lbl4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.lbl3 = new System.Windows.Forms.Label();
		this.coBox_Sep = new System.Windows.Forms.ComboBox();
		this.tb_Eot = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.tb_Bot = new System.Windows.Forms.TextBox();
		this.ckBox_ANI = new System.Windows.Forms.CheckBox();
		this.lbl9 = new System.Windows.Forms.Label();
		this.Num_FirstCodeTim = new System.Windows.Forms.NumericUpDown();
		this.lbl8 = new System.Windows.Forms.Label();
		this.lbl6 = new System.Windows.Forms.Label();
		this.lbl7 = new System.Windows.Forms.Label();
		this.Num_PreTim = new System.Windows.Forms.NumericUpDown();
		this.lbl2 = new System.Windows.Forms.Label();
		this.Num_CodeDly = new System.Windows.Forms.NumericUpDown();
		this.coBox_PttidPause = new System.Windows.Forms.ComboBox();
		this.coBox_Grpcode = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.tab_EncPage = new System.Windows.Forms.TabPage();
		this.dgv_DtmfEnc = new System.Windows.Forms.DataGridView();
		this.Col_DtmfNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col_DtmfEnc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Tab_Dtmf.SuspendLayout();
		this.tab_ParaPage.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Num_FirstCodeTim).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Num_PreTim).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Num_CodeDly).BeginInit();
		this.tab_EncPage.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgv_DtmfEnc).BeginInit();
		base.SuspendLayout();
		this.Tab_Dtmf.Controls.Add(this.tab_ParaPage);
		this.Tab_Dtmf.Controls.Add(this.tab_EncPage);
		this.Tab_Dtmf.Location = new System.Drawing.Point(12, 12);
		this.Tab_Dtmf.Name = "Tab_Dtmf";
		this.Tab_Dtmf.SelectedIndex = 0;
		this.Tab_Dtmf.Size = new System.Drawing.Size(618, 420);
		this.Tab_Dtmf.TabIndex = 0;
		this.Tab_Dtmf.SelectedIndexChanged += new System.EventHandler(Tab_Dtmf_SelectedIndexChanged);
		this.tab_ParaPage.BackColor = System.Drawing.SystemColors.Control;
		this.tab_ParaPage.Controls.Add(this.Cobox_ResetTim);
		this.tab_ParaPage.Controls.Add(this.label8);
		this.tab_ParaPage.Controls.Add(this.coBox_SideTone);
		this.tab_ParaPage.Controls.Add(this.coBox_CodeSpeed);
		this.tab_ParaPage.Controls.Add(this.label5);
		this.tab_ParaPage.Controls.Add(this.label7);
		this.tab_ParaPage.Controls.Add(this.label6);
		this.tab_ParaPage.Controls.Add(this.tb_ID);
		this.tab_ParaPage.Controls.Add(this.tb_Kill);
		this.tab_ParaPage.Controls.Add(this.label4);
		this.tab_ParaPage.Controls.Add(this.tb_Stun);
		this.tab_ParaPage.Controls.Add(this.coBox_DecRsp);
		this.tab_ParaPage.Controls.Add(this.lbl4);
		this.tab_ParaPage.Controls.Add(this.label3);
		this.tab_ParaPage.Controls.Add(this.lbl3);
		this.tab_ParaPage.Controls.Add(this.coBox_Sep);
		this.tab_ParaPage.Controls.Add(this.tb_Eot);
		this.tab_ParaPage.Controls.Add(this.label2);
		this.tab_ParaPage.Controls.Add(this.tb_Bot);
		this.tab_ParaPage.Controls.Add(this.ckBox_ANI);
		this.tab_ParaPage.Controls.Add(this.lbl9);
		this.tab_ParaPage.Controls.Add(this.Num_FirstCodeTim);
		this.tab_ParaPage.Controls.Add(this.lbl8);
		this.tab_ParaPage.Controls.Add(this.lbl6);
		this.tab_ParaPage.Controls.Add(this.lbl7);
		this.tab_ParaPage.Controls.Add(this.Num_PreTim);
		this.tab_ParaPage.Controls.Add(this.lbl2);
		this.tab_ParaPage.Controls.Add(this.Num_CodeDly);
		this.tab_ParaPage.Controls.Add(this.coBox_PttidPause);
		this.tab_ParaPage.Controls.Add(this.coBox_Grpcode);
		this.tab_ParaPage.Controls.Add(this.label1);
		this.tab_ParaPage.Location = new System.Drawing.Point(4, 22);
		this.tab_ParaPage.Name = "tab_ParaPage";
		this.tab_ParaPage.Padding = new System.Windows.Forms.Padding(3);
		this.tab_ParaPage.Size = new System.Drawing.Size(610, 394);
		this.tab_ParaPage.TabIndex = 0;
		this.tab_ParaPage.Text = "DTMF 参数";
		this.Cobox_ResetTim.DropDownHeight = 120;
		this.Cobox_ResetTim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.Cobox_ResetTim.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.Cobox_ResetTim.FormattingEnabled = true;
		this.Cobox_ResetTim.IntegralHeight = false;
		this.Cobox_ResetTim.Items.AddRange(new object[16]
		{
			"10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
			"20", "21", "22", "23", "24", "25"
		});
		this.Cobox_ResetTim.Location = new System.Drawing.Point(456, 66);
		this.Cobox_ResetTim.Name = "Cobox_ResetTim";
		this.Cobox_ResetTim.Size = new System.Drawing.Size(99, 20);
		this.Cobox_ResetTim.TabIndex = 45;
		this.label8.Location = new System.Drawing.Point(350, 42);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(100, 18);
		this.label8.TabIndex = 38;
		this.label8.Text = "DTMF 侧音";
		this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_SideTone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_SideTone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_SideTone.FormattingEnabled = true;
		this.coBox_SideTone.Items.AddRange(new object[2] { "关闭", "打开" });
		this.coBox_SideTone.Location = new System.Drawing.Point(456, 40);
		this.coBox_SideTone.Name = "coBox_SideTone";
		this.coBox_SideTone.Size = new System.Drawing.Size(99, 20);
		this.coBox_SideTone.TabIndex = 37;
		this.coBox_CodeSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_CodeSpeed.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_CodeSpeed.FormattingEnabled = true;
		this.coBox_CodeSpeed.Items.AddRange(new object[5] { "50", "100", "200", "300", "500" });
		this.coBox_CodeSpeed.Location = new System.Drawing.Point(194, 41);
		this.coBox_CodeSpeed.Name = "coBox_CodeSpeed";
		this.coBox_CodeSpeed.Size = new System.Drawing.Size(99, 20);
		this.coBox_CodeSpeed.TabIndex = 35;
		this.label5.Location = new System.Drawing.Point(88, 303);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(100, 18);
		this.label5.TabIndex = 14;
		this.label5.Text = "遥毙码";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label7.Location = new System.Drawing.Point(88, 195);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(100, 18);
		this.label7.TabIndex = 34;
		this.label7.Text = "本机ID码";
		this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label6.Location = new System.Drawing.Point(88, 276);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(100, 18);
		this.label6.TabIndex = 13;
		this.label6.Text = "遥晕码";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tb_ID.Location = new System.Drawing.Point(194, 192);
		this.tb_ID.MaxLength = 3;
		this.tb_ID.Name = "tb_ID";
		this.tb_ID.Size = new System.Drawing.Size(99, 21);
		this.tb_ID.TabIndex = 33;
		this.tb_ID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Cells_IDKeyPress);
		this.tb_Kill.Location = new System.Drawing.Point(194, 300);
		this.tb_Kill.MaxLength = 11;
		this.tb_Kill.Name = "tb_Kill";
		this.tb_Kill.Size = new System.Drawing.Size(108, 21);
		this.tb_Kill.TabIndex = 12;
		this.tb_Kill.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Cells_KeyPress);
		this.label4.Location = new System.Drawing.Point(350, 151);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(100, 18);
		this.label4.TabIndex = 32;
		this.label4.Text = "解码响应";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tb_Stun.Location = new System.Drawing.Point(194, 273);
		this.tb_Stun.MaxLength = 11;
		this.tb_Stun.Name = "tb_Stun";
		this.tb_Stun.Size = new System.Drawing.Size(108, 21);
		this.tb_Stun.TabIndex = 11;
		this.tb_Stun.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Cells_KeyPress);
		this.coBox_DecRsp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_DecRsp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_DecRsp.FormattingEnabled = true;
		this.coBox_DecRsp.Items.AddRange(new object[3] { "无", "提示音", "提示音并回复" });
		this.coBox_DecRsp.Location = new System.Drawing.Point(456, 149);
		this.coBox_DecRsp.Name = "coBox_DecRsp";
		this.coBox_DecRsp.Size = new System.Drawing.Size(99, 20);
		this.coBox_DecRsp.TabIndex = 31;
		this.lbl4.Location = new System.Drawing.Point(88, 249);
		this.lbl4.Name = "lbl4";
		this.lbl4.Size = new System.Drawing.Size(100, 18);
		this.lbl4.TabIndex = 10;
		this.lbl4.Text = "PTT ID 下线码";
		this.lbl4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label3.Location = new System.Drawing.Point(350, 95);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(100, 18);
		this.label3.TabIndex = 30;
		this.label3.Text = "分隔符";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl3.Location = new System.Drawing.Point(88, 222);
		this.lbl3.Name = "lbl3";
		this.lbl3.Size = new System.Drawing.Size(100, 18);
		this.lbl3.TabIndex = 9;
		this.lbl3.Text = "PTT ID 上线码";
		this.lbl3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_Sep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Sep.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Sep.FormattingEnabled = true;
		this.coBox_Sep.Items.AddRange(new object[6] { "A", "B", "C", "D", "*", "#" });
		this.coBox_Sep.Location = new System.Drawing.Point(456, 93);
		this.coBox_Sep.Name = "coBox_Sep";
		this.coBox_Sep.Size = new System.Drawing.Size(99, 20);
		this.coBox_Sep.TabIndex = 29;
		this.coBox_Sep.SelectedIndexChanged += new System.EventHandler(coBox_Sep_SelectedIndexChanged);
		this.tb_Eot.Location = new System.Drawing.Point(194, 246);
		this.tb_Eot.MaxLength = 16;
		this.tb_Eot.Name = "tb_Eot";
		this.tb_Eot.Size = new System.Drawing.Size(108, 21);
		this.tb_Eot.TabIndex = 5;
		this.tb_Eot.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Cells_KeyPress);
		this.label2.Location = new System.Drawing.Point(14, 41);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(170, 18);
		this.label2.TabIndex = 28;
		this.label2.Text = "发码速率[毫秒]";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tb_Bot.Location = new System.Drawing.Point(194, 219);
		this.tb_Bot.MaxLength = 16;
		this.tb_Bot.Name = "tb_Bot";
		this.tb_Bot.Size = new System.Drawing.Size(108, 21);
		this.tb_Bot.TabIndex = 4;
		this.tb_Bot.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Cells_KeyPress);
		this.ckBox_ANI.Location = new System.Drawing.Point(99, 17);
		this.ckBox_ANI.Name = "ckBox_ANI";
		this.ckBox_ANI.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.ckBox_ANI.Size = new System.Drawing.Size(109, 18);
		this.ckBox_ANI.TabIndex = 26;
		this.ckBox_ANI.Text = "DTMF ANI";
		this.ckBox_ANI.UseVisualStyleBackColor = true;
		this.lbl9.Location = new System.Drawing.Point(33, 123);
		this.lbl9.Name = "lbl9";
		this.lbl9.Size = new System.Drawing.Size(155, 18);
		this.lbl9.TabIndex = 21;
		this.lbl9.Text = "发码后延迟时间[毫秒]";
		this.lbl9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.Num_FirstCodeTim.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.Num_FirstCodeTim.Location = new System.Drawing.Point(194, 67);
		this.Num_FirstCodeTim.Maximum = new decimal(new int[4] { 2500, 0, 0, 0 });
		this.Num_FirstCodeTim.Name = "Num_FirstCodeTim";
		this.Num_FirstCodeTim.Size = new System.Drawing.Size(99, 21);
		this.Num_FirstCodeTim.TabIndex = 13;
		this.Num_FirstCodeTim.Value = new decimal(new int[4] { 100, 0, 0, 0 });
		this.lbl8.Location = new System.Drawing.Point(18, 97);
		this.lbl8.Name = "lbl8";
		this.lbl8.Size = new System.Drawing.Size(170, 18);
		this.lbl8.TabIndex = 20;
		this.lbl8.Text = "预载波时间[毫秒]";
		this.lbl8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl6.Location = new System.Drawing.Point(328, 69);
		this.lbl6.Name = "lbl6";
		this.lbl6.Size = new System.Drawing.Size(122, 18);
		this.lbl6.TabIndex = 12;
		this.lbl6.Text = "自动复位时间[秒]";
		this.lbl6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl7.Location = new System.Drawing.Point(16, 69);
		this.lbl7.Name = "lbl7";
		this.lbl7.Size = new System.Drawing.Size(172, 18);
		this.lbl7.TabIndex = 19;
		this.lbl7.Text = "首码数码时间[毫秒]";
		this.lbl7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.Num_PreTim.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.Num_PreTim.Location = new System.Drawing.Point(194, 93);
		this.Num_PreTim.Maximum = new decimal(new int[4] { 2500, 0, 0, 0 });
		this.Num_PreTim.Name = "Num_PreTim";
		this.Num_PreTim.Size = new System.Drawing.Size(99, 21);
		this.Num_PreTim.TabIndex = 14;
		this.Num_PreTim.Value = new decimal(new int[4] { 30, 0, 0, 0 });
		this.lbl2.Location = new System.Drawing.Point(350, 123);
		this.lbl2.Name = "lbl2";
		this.lbl2.Size = new System.Drawing.Size(100, 18);
		this.lbl2.TabIndex = 8;
		this.lbl2.Text = "组呼码";
		this.lbl2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.Num_CodeDly.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.Num_CodeDly.Location = new System.Drawing.Point(194, 120);
		this.Num_CodeDly.Maximum = new decimal(new int[4] { 2500, 0, 0, 0 });
		this.Num_CodeDly.Minimum = new decimal(new int[4] { 10, 0, 0, 0 });
		this.Num_CodeDly.Name = "Num_CodeDly";
		this.Num_CodeDly.Size = new System.Drawing.Size(99, 21);
		this.Num_CodeDly.TabIndex = 15;
		this.Num_CodeDly.Value = new decimal(new int[4] { 250, 0, 0, 0 });
		this.coBox_PttidPause.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_PttidPause.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_PttidPause.FormattingEnabled = true;
		this.coBox_PttidPause.Items.AddRange(new object[16]
		{
			"0", "5", "10", "15", "20", "25", "30", "35", "40", "45",
			"50", "55", "60", "65", "70", "75"
		});
		this.coBox_PttidPause.Location = new System.Drawing.Point(194, 147);
		this.coBox_PttidPause.Name = "coBox_PttidPause";
		this.coBox_PttidPause.Size = new System.Drawing.Size(99, 20);
		this.coBox_PttidPause.TabIndex = 2;
		this.coBox_Grpcode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Grpcode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Grpcode.FormattingEnabled = true;
		this.coBox_Grpcode.Items.AddRange(new object[7] { "无", "A", "B", "C", "D", "*", "#" });
		this.coBox_Grpcode.Location = new System.Drawing.Point(456, 121);
		this.coBox_Grpcode.Name = "coBox_Grpcode";
		this.coBox_Grpcode.Size = new System.Drawing.Size(99, 20);
		this.coBox_Grpcode.TabIndex = 3;
		this.coBox_Grpcode.SelectedIndexChanged += new System.EventHandler(coBox_Grpcode_SelectedIndexChanged);
		this.label1.Location = new System.Drawing.Point(35, 149);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(153, 18);
		this.label1.TabIndex = 0;
		this.label1.Text = "PTT ID暂停时间[秒]";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tab_EncPage.BackColor = System.Drawing.SystemColors.Control;
		this.tab_EncPage.Controls.Add(this.dgv_DtmfEnc);
		this.tab_EncPage.Location = new System.Drawing.Point(4, 22);
		this.tab_EncPage.Name = "tab_EncPage";
		this.tab_EncPage.Padding = new System.Windows.Forms.Padding(3);
		this.tab_EncPage.Size = new System.Drawing.Size(610, 394);
		this.tab_EncPage.TabIndex = 1;
		this.tab_EncPage.Text = "编码列表";
		this.dgv_DtmfEnc.AllowUserToAddRows = false;
		this.dgv_DtmfEnc.AllowUserToDeleteRows = false;
		this.dgv_DtmfEnc.AllowUserToResizeColumns = false;
		this.dgv_DtmfEnc.AllowUserToResizeRows = false;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgv_DtmfEnc.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgv_DtmfEnc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgv_DtmfEnc.Columns.AddRange(this.Col_DtmfNo, this.Col_DtmfEnc);
		this.dgv_DtmfEnc.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dgv_DtmfEnc.Location = new System.Drawing.Point(35, 26);
		this.dgv_DtmfEnc.MultiSelect = false;
		this.dgv_DtmfEnc.Name = "dgv_DtmfEnc";
		this.dgv_DtmfEnc.RowHeadersVisible = false;
		this.dgv_DtmfEnc.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.dgv_DtmfEnc.RowTemplate.Height = 23;
		this.dgv_DtmfEnc.Size = new System.Drawing.Size(512, 289);
		this.dgv_DtmfEnc.TabIndex = 0;
		this.dgv_DtmfEnc.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgv_DtmfEnc_EditingControlShowing);
		this.Col_DtmfNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.Col_DtmfNo.DefaultCellStyle = dataGridViewCellStyle2;
		this.Col_DtmfNo.FillWeight = 20f;
		this.Col_DtmfNo.HeaderText = "序号";
		this.Col_DtmfNo.Name = "Col_DtmfNo";
		this.Col_DtmfNo.ReadOnly = true;
		this.Col_DtmfNo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_DtmfNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_DtmfEnc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Col_DtmfEnc.DefaultCellStyle = dataGridViewCellStyle3;
		this.Col_DtmfEnc.FillWeight = 80f;
		this.Col_DtmfEnc.HeaderText = "编码";
		this.Col_DtmfEnc.MaxInputLength = 16;
		this.Col_DtmfEnc.Name = "Col_DtmfEnc";
		this.Col_DtmfEnc.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_DtmfEnc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(641, 436);
		base.Controls.Add(this.Tab_Dtmf);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Frm_Dtmf";
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = "Frm_Dtmf";
		base.Load += new System.EventHandler(Frm_Dtmf_Load);
		this.Tab_Dtmf.ResumeLayout(false);
		this.tab_ParaPage.ResumeLayout(false);
		this.tab_ParaPage.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.Num_FirstCodeTim).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Num_PreTim).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Num_CodeDly).EndInit();
		this.tab_EncPage.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgv_DtmfEnc).EndInit();
		base.ResumeLayout(false);
	}

	public Frm_Dtmf(DataApp data)
	{
		InitializeComponent();
		tdata = data;
	}

	private void Tab_Dtmf_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (Tab_Dtmf.SelectedIndex == 1)
		{
			DtmfEncTabDisp();
		}
	}

	public void DtmfDataDisp()
	{
		bangDingSysData(0);
		tb_ID.Text = tdata.dataDtmfSysInfor.Did;
		tb_Stun.Text = tdata.dataDtmfSysInfor.Stun;
		tb_Kill.Text = tdata.dataDtmfSysInfor.Kill;
	}

	private void bangDingSysData(int pos)
	{
		ckBox_ANI.DataBindings.Clear();
		coBox_SideTone.DataBindings.Clear();
		coBox_CodeSpeed.DataBindings.Clear();
		Num_FirstCodeTim.DataBindings.Clear();
		Num_PreTim.DataBindings.Clear();
		Num_CodeDly.DataBindings.Clear();
		Cobox_ResetTim.DataBindings.Clear();
		coBox_PttidPause.DataBindings.Clear();
		coBox_Sep.DataBindings.Clear();
		coBox_Grpcode.DataBindings.Clear();
		coBox_DecRsp.DataBindings.Clear();
		tb_Bot.DataBindings.Clear();
		tb_Eot.DataBindings.Clear();
		if (tdata.dataDtmfSysInfor.CodeSpeed > 4)
		{
			tdata.dataDtmfSysInfor.CodeSpeed = 0;
		}
		if (tdata.dataDtmfSysInfor.PttIDPause > 15)
		{
			tdata.dataDtmfSysInfor.PttIDPause = 0;
		}
		if (tdata.dataDtmfSysInfor.SepCode > 5)
		{
			tdata.dataDtmfSysInfor.SepCode = 0;
		}
		if (tdata.dataDtmfSysInfor.GrpCode > 6)
		{
			tdata.dataDtmfSysInfor.GrpCode = 0;
		}
		if (tdata.dataDtmfSysInfor.DecRsp > 2)
		{
			tdata.dataDtmfSysInfor.DecRsp = 0;
		}
		if (tdata.dataDtmfSysInfor.FirstCodeTim > 2500)
		{
			tdata.dataDtmfSysInfor.FirstCodeTim = 0;
		}
		if (tdata.dataDtmfSysInfor.PreTime > 2500)
		{
			tdata.dataDtmfSysInfor.PreTime = 0;
		}
		if (tdata.dataDtmfSysInfor.CodeDly > 2500 || tdata.dataDtmfSysInfor.CodeDly < 10)
		{
			tdata.dataDtmfSysInfor.CodeDly = 10;
		}
		if (tdata.dataDtmfSysInfor.ResetTime > 15)
		{
			tdata.dataDtmfSysInfor.ResetTime = 0;
		}
		ckBox_ANI.DataBindings.Add("Checked", tdata.dataDtmfSysInfor, "DtmfSw", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_SideTone.DataBindings.Add("SelectedIndex", tdata.dataDtmfSysInfor, "DtmfTone", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_CodeSpeed.DataBindings.Add("SelectedIndex", tdata.dataDtmfSysInfor, "CodeSpeed", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		Num_FirstCodeTim.DataBindings.Add("Value", tdata.dataDtmfSysInfor, "FirstCodeTim", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		Num_PreTim.DataBindings.Add("Value", tdata.dataDtmfSysInfor, "PreTime", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		Num_CodeDly.DataBindings.Add("Value", tdata.dataDtmfSysInfor, "CodeDly", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		Cobox_ResetTim.DataBindings.Add("SelectedIndex", tdata.dataDtmfSysInfor, "ResetTime", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_PttidPause.DataBindings.Add("SelectedIndex", tdata.dataDtmfSysInfor, "PttIDPause", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_Sep.DataBindings.Add("SelectedIndex", tdata.dataDtmfSysInfor, "SepCode", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_Grpcode.DataBindings.Add("SelectedIndex", tdata.dataDtmfSysInfor, "GrpCode", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_DecRsp.DataBindings.Add("SelectedIndex", tdata.dataDtmfSysInfor, "DecRsp", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		tb_Bot.DataBindings.Add("Text", tdata.dataDtmfSysInfor, "Bot", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		tb_Eot.DataBindings.Add("Text", tdata.dataDtmfSysInfor, "Eot", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
	}

	private void DtmfEncTabDisp()
	{
		for (int i = 0; i < 16; i++)
		{
			if (tdata.dataDtmfEncInfor.GetEncLen(i) > 0)
			{
				dgv_DtmfEnc.Rows[i].Cells[1].Value = tdata.dataDtmfEncInfor.GetEncCode(i);
			}
		}
	}

	private void SetDTMF_UseFlg(int idx, byte val)
	{
		int tmp = tdata.dataDtmfEncInfor.UseFlg;
		tdata.dataDtmfEncInfor.UseFlg = (ushort)((val != 0) ? (tmp | (1 << idx)) : (tmp & ~(1 << idx)));
	}

	private void UpDate_EmergData(int ros)
	{
		for (int i = 0; i < 10; i++)
		{
			if (tdata.dataEmergInfor[i].Mode == 1 && tdata.dataEmergInfor[i].GrpNo == ros + 1)
			{
				tdata.dataEmergInfor[i].GrpNo = 0;
			}
		}
	}

	private void dgv_DtmfEnc_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		string code = "";
		if (dgv_DtmfEnc.CurrentCell == null)
		{
			return;
		}
		int rowIndex = dgv_DtmfEnc.CurrentCell.RowIndex;
		if (dgv_DtmfEnc.CurrentCell.ColumnIndex == 1)
		{
			if (dgv_DtmfEnc.Rows[rowIndex].Cells[1].Value == null)
			{
				tdata.dataDtmfEncInfor.SetEncLen(rowIndex, 0);
				tdata.dataDtmfEncInfor.SetEncCode(rowIndex, "");
				UpDate_EmergData(rowIndex);
				SetDTMF_UseFlg(rowIndex, 0);
			}
			else
			{
				code = dgv_DtmfEnc.Rows[rowIndex].Cells[1].Value.ToString();
				tdata.dataDtmfEncInfor.SetEncLen(rowIndex, code.Length);
				tdata.dataDtmfEncInfor.SetEncCode(rowIndex, code);
				SetDTMF_UseFlg(rowIndex, 1);
			}
		}
	}

	private void dgv_DtmfEnc_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
	{
		if (dgv_DtmfEnc.CurrentCell.ColumnIndex == 1)
		{
			dgvTxt = (DataGridViewTextBoxEditingControl)e.Control;
			dgvTxt.SelectAll();
			dgvTxt.KeyPress -= Cells_KeyPress;
			dgvTxt.KeyPress += Cells_KeyPress;
			dgvTxt.TextChanged -= EditingTB_TextChanged;
			dgvTxt.TextChanged += EditingTB_TextChanged;
		}
	}

	private void Cells_KeyPress(object sender, KeyPressEventArgs e)
	{
		if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar < 'a' || e.KeyChar > 'd') && (e.KeyChar < 'A' || e.KeyChar > 'D') && e.KeyChar != '\b' && e.KeyChar != '*' && e.KeyChar != '#')
		{
			e.Handled = true;
		}
		else if (e.KeyChar >= 'a' && e.KeyChar <= 'd')
		{
			e.KeyChar -= ' ';
		}
	}

	private void Cells_IDKeyPress(object sender, KeyPressEventArgs e)
	{
		if (char.IsNumber(e.KeyChar) || e.KeyChar == '\b')
		{
			e.Handled = false;
		}
		else
		{
			e.Handled = true;
		}
	}

	private void EditingTB_TextChanged(object sender, EventArgs e)
	{
		string code = "";
		int rowIndex = dgv_DtmfEnc.CurrentCell.RowIndex;
		dgv_DtmfEnc.CurrentCell.Value = (sender as TextBox).Text;
		if (dgv_DtmfEnc.Rows[rowIndex].Cells[1].Value == null)
		{
			tdata.dataDtmfEncInfor.SetEncLen(rowIndex, 0);
			tdata.dataDtmfEncInfor.SetEncCode(rowIndex, "");
			UpDate_EmergData(rowIndex);
			SetDTMF_UseFlg(rowIndex, 0);
			return;
		}
		code = Convert.ToString(dgv_DtmfEnc.CurrentCell.Value);
		if (code == "")
		{
			tdata.dataDtmfEncInfor.SetEncLen(rowIndex, 0);
			tdata.dataDtmfEncInfor.SetEncCode(rowIndex, "");
			UpDate_EmergData(rowIndex);
			SetDTMF_UseFlg(rowIndex, 0);
		}
		else
		{
			tdata.dataDtmfEncInfor.SetEncLen(rowIndex, code.Length);
			tdata.dataDtmfEncInfor.SetEncCode(rowIndex, code);
			SetDTMF_UseFlg(rowIndex, 1);
		}
	}

	private void Frm_Dtmf_Load(object sender, EventArgs e)
	{
		string[] strItems = new string[8];
		string tmp = "";
		string tmp2 = "";
		dgv_DtmfEnc.Rows.Add(16);
		for (int i = 0; i < 16; i++)
		{
			dgv_DtmfEnc.Rows[i].Cells[0].Value = (i + 1).ToString();
		}
		if (FormMain.lang == "Chinese")
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
		}
		ComponentResourceManager crm = new ComponentResourceManager(typeof(Frm_Dtmf));
		foreach (DataGridViewColumn col in dgv_DtmfEnc.Columns)
		{
			crm.ApplyResources(col, col.Name);
		}
		LanguageSel.LoadLanguage(this, typeof(Frm_Dtmf));
		for (int i = 0; i < 4; i++)
		{
			LanguageSel.ElseCombobox.TryGetValue(i.ToString(), out tmp);
			strItems[i] = tmp;
			LanguageSel.GenCoBox.TryGetValue(i.ToString(), out tmp2);
			strItems[i + 4] = tmp2;
		}
		coBox_Grpcode.Items[0] = strItems[0];
		coBox_PttidPause.Items[0] = strItems[1];
		coBox_DecRsp.Items[0] = strItems[0];
		coBox_DecRsp.Items[1] = strItems[2];
		coBox_DecRsp.Items[2] = strItems[3];
		coBox_SideTone.Items[0] = strItems[4];
		coBox_SideTone.Items[1] = strItems[5];
		tb_ID.LostFocus -= tb_ID_LostFocus;
		tb_ID.LostFocus += tb_ID_LostFocus;
		tb_Stun.LostFocus -= tb_Stun_LostFocus;
		tb_Stun.LostFocus += tb_Stun_LostFocus;
		tb_Kill.LostFocus -= tb_Kill_LostFocus;
		tb_Kill.LostFocus += tb_Kill_LostFocus;
	}

	private void tb_ID_LostFocus(object sender, EventArgs e)
	{
		string idval = tb_ID.Text;
		if (idval == "")
		{
			tb_ID.Text = tdata.dataDtmfSysInfor.Did;
			return;
		}
		for (int i = idval.Length; i < 3; i++)
		{
			idval = idval.Insert(0, "0");
		}
		tdata.dataDtmfSysInfor.Did = idval;
		tb_ID.Text = tdata.dataDtmfSysInfor.Did;
	}

	private void coBox_Grpcode_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (coBox_Grpcode.SelectedIndex == coBox_Sep.SelectedIndex + 1)
		{
			coBox_Grpcode.SelectedIndex = 0;
		}
	}

	private void coBox_Sep_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (coBox_Grpcode.SelectedIndex == coBox_Sep.SelectedIndex + 1)
		{
			coBox_Grpcode.SelectedIndex = 0;
		}
	}

	private void tb_Stun_LostFocus(object sender, EventArgs e)
	{
		string idval = tb_Stun.Text;
		if (idval == "")
		{
			tdata.dataDtmfSysInfor.Stun = "";
		}
		else if (string.Compare(idval, tdata.dataDtmfSysInfor.Kill) == 0)
		{
			tb_Stun.Text = tdata.dataDtmfSysInfor.Stun;
		}
		else
		{
			tdata.dataDtmfSysInfor.Stun = idval;
		}
	}

	private void tb_Kill_LostFocus(object sender, EventArgs e)
	{
		string idval = tb_Kill.Text;
		if (idval == "")
		{
			tdata.dataDtmfSysInfor.Kill = "";
		}
		else if (string.Compare(idval, tdata.dataDtmfSysInfor.Stun) == 0)
		{
			tb_Kill.Text = tdata.dataDtmfSysInfor.Kill;
		}
		else
		{
			tdata.dataDtmfSysInfor.Kill = idval;
		}
	}
}
