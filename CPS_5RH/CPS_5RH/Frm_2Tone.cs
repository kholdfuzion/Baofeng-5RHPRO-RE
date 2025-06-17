using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using CPS_5RH.Data;

namespace CPS_5RH;

public class Frm_2Tone : Form
{
	private IContainer components = null;

	private TabControl Tab_2Tone;

	private TabPage tab_P1;

	private Label label7;

	private Label label6;

	private TextBox tb_Atone;

	private Label label4;

	private TextBox tb_Dtone;

	private ComboBox coBox_Rsp;

	private Label lbl4;

	private Label label3;

	private Label lbl3;

	private TextBox tb_Ctone;

	private TextBox tb_Btone;

	private CheckBox ckBox_Stone;

	private Label lbl9;

	private Label lbl8;

	private Label lbl6;

	private Label lbl7;

	private Label label1;

	private TabPage tab_P2;

	private DataGridView dgvTwo;

	private ComboBox coBox_Decfomat;

	private ComboBox Cobox_ResetTim;

	private ComboBox coBox_CodeInt;

	private ComboBox coBox_FirstCode;

	private ComboBox coBox_SecondCode;

	private ComboBox coBox_CodeDur;

	private DataGridViewTextBoxColumn Col_No;

	private DataGridViewTextBoxColumn Col_A;

	private DataGridViewTextBoxColumn Col_B;

	private DataGridViewTextBoxColumn Col_Enc;

	private DataApp tdata = null;

	private static bool initflg = false;

	private DataGridViewTextBoxEditingControl dgvName = null;

	private TextBox dgvFreq = null;

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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPS_5RH.Frm_2Tone));
		this.Tab_2Tone = new System.Windows.Forms.TabControl();
		this.tab_P1 = new System.Windows.Forms.TabPage();
		this.coBox_CodeDur = new System.Windows.Forms.ComboBox();
		this.coBox_SecondCode = new System.Windows.Forms.ComboBox();
		this.coBox_FirstCode = new System.Windows.Forms.ComboBox();
		this.coBox_CodeInt = new System.Windows.Forms.ComboBox();
		this.Cobox_ResetTim = new System.Windows.Forms.ComboBox();
		this.coBox_Decfomat = new System.Windows.Forms.ComboBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.tb_Atone = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.tb_Dtone = new System.Windows.Forms.TextBox();
		this.coBox_Rsp = new System.Windows.Forms.ComboBox();
		this.lbl4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.lbl3 = new System.Windows.Forms.Label();
		this.tb_Ctone = new System.Windows.Forms.TextBox();
		this.tb_Btone = new System.Windows.Forms.TextBox();
		this.ckBox_Stone = new System.Windows.Forms.CheckBox();
		this.lbl9 = new System.Windows.Forms.Label();
		this.lbl8 = new System.Windows.Forms.Label();
		this.lbl6 = new System.Windows.Forms.Label();
		this.lbl7 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.tab_P2 = new System.Windows.Forms.TabPage();
		this.dgvTwo = new System.Windows.Forms.DataGridView();
		this.Col_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col_A = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col_B = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col_Enc = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Tab_2Tone.SuspendLayout();
		this.tab_P1.SuspendLayout();
		this.tab_P2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvTwo).BeginInit();
		base.SuspendLayout();
		this.Tab_2Tone.Controls.Add(this.tab_P1);
		this.Tab_2Tone.Controls.Add(this.tab_P2);
		this.Tab_2Tone.Location = new System.Drawing.Point(12, 12);
		this.Tab_2Tone.Name = "Tab_2Tone";
		this.Tab_2Tone.SelectedIndex = 0;
		this.Tab_2Tone.Size = new System.Drawing.Size(618, 420);
		this.Tab_2Tone.TabIndex = 1;
		this.tab_P1.BackColor = System.Drawing.SystemColors.Control;
		this.tab_P1.Controls.Add(this.coBox_CodeDur);
		this.tab_P1.Controls.Add(this.coBox_SecondCode);
		this.tab_P1.Controls.Add(this.coBox_FirstCode);
		this.tab_P1.Controls.Add(this.coBox_CodeInt);
		this.tab_P1.Controls.Add(this.Cobox_ResetTim);
		this.tab_P1.Controls.Add(this.coBox_Decfomat);
		this.tab_P1.Controls.Add(this.label7);
		this.tab_P1.Controls.Add(this.label6);
		this.tab_P1.Controls.Add(this.tb_Atone);
		this.tab_P1.Controls.Add(this.label4);
		this.tab_P1.Controls.Add(this.tb_Dtone);
		this.tab_P1.Controls.Add(this.coBox_Rsp);
		this.tab_P1.Controls.Add(this.lbl4);
		this.tab_P1.Controls.Add(this.label3);
		this.tab_P1.Controls.Add(this.lbl3);
		this.tab_P1.Controls.Add(this.tb_Ctone);
		this.tab_P1.Controls.Add(this.tb_Btone);
		this.tab_P1.Controls.Add(this.ckBox_Stone);
		this.tab_P1.Controls.Add(this.lbl9);
		this.tab_P1.Controls.Add(this.lbl8);
		this.tab_P1.Controls.Add(this.lbl6);
		this.tab_P1.Controls.Add(this.lbl7);
		this.tab_P1.Controls.Add(this.label1);
		this.tab_P1.Location = new System.Drawing.Point(4, 22);
		this.tab_P1.Name = "tab_P1";
		this.tab_P1.Padding = new System.Windows.Forms.Padding(3);
		this.tab_P1.Size = new System.Drawing.Size(610, 394);
		this.tab_P1.TabIndex = 0;
		this.tab_P1.Text = "两音参数设置";
		this.coBox_CodeDur.DropDownHeight = 120;
		this.coBox_CodeDur.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_CodeDur.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_CodeDur.FormattingEnabled = true;
		this.coBox_CodeDur.IntegralHeight = false;
		this.coBox_CodeDur.Items.AddRange(new object[20]
		{
			"0.5", "1.0", "1.5", "2.0", "2.5", "3.0", "3.5", "4.0", "4.5", "5.0",
			"5.5", "6.0", "6.5", "7.0", "7.5", "8.0", "8.5", "9.0", "9.5", "10.0"
		});
		this.coBox_CodeDur.Location = new System.Drawing.Point(194, 95);
		this.coBox_CodeDur.Name = "coBox_CodeDur";
		this.coBox_CodeDur.Size = new System.Drawing.Size(99, 20);
		this.coBox_CodeDur.TabIndex = 50;
		this.coBox_SecondCode.DropDownHeight = 120;
		this.coBox_SecondCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_SecondCode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_SecondCode.FormattingEnabled = true;
		this.coBox_SecondCode.IntegralHeight = false;
		this.coBox_SecondCode.Items.AddRange(new object[20]
		{
			"0.5", "1.0", "1.5", "2.0", "2.5", "3.0", "3.5", "4.0", "4.5", "5.0",
			"5.5", "6.0", "6.5", "7.0", "7.5", "8.0", "8.5", "9.0", "9.5", "10.0"
		});
		this.coBox_SecondCode.Location = new System.Drawing.Point(194, 69);
		this.coBox_SecondCode.Name = "coBox_SecondCode";
		this.coBox_SecondCode.Size = new System.Drawing.Size(99, 20);
		this.coBox_SecondCode.TabIndex = 49;
		this.coBox_FirstCode.DropDownHeight = 120;
		this.coBox_FirstCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_FirstCode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_FirstCode.FormattingEnabled = true;
		this.coBox_FirstCode.IntegralHeight = false;
		this.coBox_FirstCode.Items.AddRange(new object[20]
		{
			"0.5", "1.0", "1.5", "2.0", "2.5", "3.0", "3.5", "4.0", "4.5", "5.0",
			"5.5", "6.0", "6.5", "7.0", "7.5", "8.0", "8.5", "9.0", "9.5", "10.0"
		});
		this.coBox_FirstCode.Location = new System.Drawing.Point(194, 41);
		this.coBox_FirstCode.Name = "coBox_FirstCode";
		this.coBox_FirstCode.Size = new System.Drawing.Size(99, 20);
		this.coBox_FirstCode.TabIndex = 48;
		this.coBox_CodeInt.DropDownHeight = 120;
		this.coBox_CodeInt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_CodeInt.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_CodeInt.FormattingEnabled = true;
		this.coBox_CodeInt.IntegralHeight = false;
		this.coBox_CodeInt.Items.AddRange(new object[21]
		{
			"0", "100", "200", "300", "400", "500", "600", "700", "800", "900",
			"1000", "1100", "1200", "1300", "1400", "1500", "1600", "1700", "1800", "1900",
			"2000"
		});
		this.coBox_CodeInt.Location = new System.Drawing.Point(194, 123);
		this.coBox_CodeInt.Name = "coBox_CodeInt";
		this.coBox_CodeInt.Size = new System.Drawing.Size(99, 20);
		this.coBox_CodeInt.TabIndex = 47;
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
		this.Cobox_ResetTim.Location = new System.Drawing.Point(455, 45);
		this.Cobox_ResetTim.Name = "Cobox_ResetTim";
		this.Cobox_ResetTim.Size = new System.Drawing.Size(99, 20);
		this.Cobox_ResetTim.TabIndex = 46;
		this.coBox_Decfomat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Decfomat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Decfomat.FormattingEnabled = true;
		this.coBox_Decfomat.Items.AddRange(new object[15]
		{
			"A-B", "A-C", "A-D", "B-A", "B-C", "B-D", "C-A", "C-B", "C-D", "D-A",
			"D-B", "D-C", "Long A", "Long B", "Long C"
		});
		this.coBox_Decfomat.Location = new System.Drawing.Point(455, 97);
		this.coBox_Decfomat.Name = "coBox_Decfomat";
		this.coBox_Decfomat.Size = new System.Drawing.Size(99, 20);
		this.coBox_Decfomat.TabIndex = 36;
		this.coBox_Decfomat.SelectedIndexChanged += new System.EventHandler(coBox_Decfomat_SelectedIndexChanged);
		this.label7.Location = new System.Drawing.Point(299, 126);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(150, 18);
		this.label7.TabIndex = 34;
		this.label7.Text = "A 音频率[Hz]";
		this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label6.Location = new System.Drawing.Point(301, 207);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(148, 18);
		this.label6.TabIndex = 13;
		this.label6.Text = "D 音频率[Hz]";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tb_Atone.Location = new System.Drawing.Point(455, 123);
		this.tb_Atone.MaxLength = 4;
		this.tb_Atone.Name = "tb_Atone";
		this.tb_Atone.Size = new System.Drawing.Size(99, 21);
		this.tb_Atone.TabIndex = 33;
		this.tb_Atone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Freq_Cells_KeyPress);
		this.label4.Location = new System.Drawing.Point(349, 73);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(100, 18);
		this.label4.TabIndex = 32;
		this.label4.Text = "解码响应";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tb_Dtone.Location = new System.Drawing.Point(455, 204);
		this.tb_Dtone.MaxLength = 4;
		this.tb_Dtone.Name = "tb_Dtone";
		this.tb_Dtone.Size = new System.Drawing.Size(99, 21);
		this.tb_Dtone.TabIndex = 11;
		this.tb_Dtone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Freq_Cells_KeyPress);
		this.coBox_Rsp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Rsp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Rsp.FormattingEnabled = true;
		this.coBox_Rsp.Items.AddRange(new object[3] { "无", "提示音", "提示音且回复" });
		this.coBox_Rsp.Location = new System.Drawing.Point(455, 71);
		this.coBox_Rsp.Name = "coBox_Rsp";
		this.coBox_Rsp.Size = new System.Drawing.Size(99, 20);
		this.coBox_Rsp.TabIndex = 31;
		this.lbl4.Location = new System.Drawing.Point(303, 180);
		this.lbl4.Name = "lbl4";
		this.lbl4.Size = new System.Drawing.Size(146, 18);
		this.lbl4.TabIndex = 10;
		this.lbl4.Text = "C 音频率[Hz]";
		this.lbl4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label3.Location = new System.Drawing.Point(349, 99);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(100, 18);
		this.label3.TabIndex = 30;
		this.label3.Text = "2Tone解码格式";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl3.Location = new System.Drawing.Point(301, 153);
		this.lbl3.Name = "lbl3";
		this.lbl3.Size = new System.Drawing.Size(148, 18);
		this.lbl3.TabIndex = 9;
		this.lbl3.Text = "B 音频率[Hz]";
		this.lbl3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tb_Ctone.Location = new System.Drawing.Point(455, 177);
		this.tb_Ctone.MaxLength = 4;
		this.tb_Ctone.Name = "tb_Ctone";
		this.tb_Ctone.Size = new System.Drawing.Size(99, 21);
		this.tb_Ctone.TabIndex = 5;
		this.tb_Ctone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Freq_Cells_KeyPress);
		this.tb_Btone.Location = new System.Drawing.Point(455, 150);
		this.tb_Btone.MaxLength = 4;
		this.tb_Btone.Name = "tb_Btone";
		this.tb_Btone.Size = new System.Drawing.Size(99, 21);
		this.tb_Btone.TabIndex = 4;
		this.tb_Btone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Freq_Cells_KeyPress);
		this.ckBox_Stone.Location = new System.Drawing.Point(99, 17);
		this.ckBox_Stone.Name = "ckBox_Stone";
		this.ckBox_Stone.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.ckBox_Stone.Size = new System.Drawing.Size(109, 18);
		this.ckBox_Stone.TabIndex = 26;
		this.ckBox_Stone.Text = "侧音";
		this.ckBox_Stone.UseVisualStyleBackColor = true;
		this.lbl9.Location = new System.Drawing.Point(18, 97);
		this.lbl9.Name = "lbl9";
		this.lbl9.Size = new System.Drawing.Size(170, 18);
		this.lbl9.TabIndex = 21;
		this.lbl9.Text = "长音持续时间[秒]";
		this.lbl9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl8.Location = new System.Drawing.Point(18, 71);
		this.lbl8.Name = "lbl8";
		this.lbl8.Size = new System.Drawing.Size(170, 18);
		this.lbl8.TabIndex = 20;
		this.lbl8.Text = "第二个持续音时间[秒]";
		this.lbl8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl6.Location = new System.Drawing.Point(327, 45);
		this.lbl6.Name = "lbl6";
		this.lbl6.Size = new System.Drawing.Size(122, 18);
		this.lbl6.TabIndex = 12;
		this.lbl6.Text = "自动复位时间[秒]";
		this.lbl6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl7.Location = new System.Drawing.Point(16, 43);
		this.lbl7.Name = "lbl7";
		this.lbl7.Size = new System.Drawing.Size(172, 18);
		this.lbl7.TabIndex = 19;
		this.lbl7.Text = "第一个持续音时间[秒]";
		this.lbl7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label1.Location = new System.Drawing.Point(20, 123);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(168, 18);
		this.label1.TabIndex = 0;
		this.label1.Text = "间隔时间[毫秒]";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tab_P2.BackColor = System.Drawing.SystemColors.Control;
		this.tab_P2.Controls.Add(this.dgvTwo);
		this.tab_P2.Location = new System.Drawing.Point(4, 22);
		this.tab_P2.Name = "tab_P2";
		this.tab_P2.Padding = new System.Windows.Forms.Padding(3);
		this.tab_P2.Size = new System.Drawing.Size(610, 394);
		this.tab_P2.TabIndex = 1;
		this.tab_P2.Text = "编码列表";
		this.dgvTwo.AllowUserToAddRows = false;
		this.dgvTwo.AllowUserToDeleteRows = false;
		this.dgvTwo.AllowUserToResizeColumns = false;
		this.dgvTwo.AllowUserToResizeRows = false;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvTwo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvTwo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvTwo.Columns.AddRange(this.Col_No, this.Col_A, this.Col_B, this.Col_Enc);
		this.dgvTwo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dgvTwo.Location = new System.Drawing.Point(35, 26);
		this.dgvTwo.MultiSelect = false;
		this.dgvTwo.Name = "dgvTwo";
		this.dgvTwo.RowHeadersVisible = false;
		this.dgvTwo.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.dgvTwo.RowTemplate.Height = 23;
		this.dgvTwo.Size = new System.Drawing.Size(528, 289);
		this.dgvTwo.TabIndex = 0;
		this.dgvTwo.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvTwo_CellEndEdit);
		this.dgvTwo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvTwo_CellClick);
		this.dgvTwo.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgvTwo_EditingControlShowing);
		this.Col_No.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(224, 224, 224);
		this.Col_No.DefaultCellStyle = dataGridViewCellStyle2;
		this.Col_No.FillWeight = 20f;
		this.Col_No.HeaderText = "序号";
		this.Col_No.Name = "Col_No";
		this.Col_No.ReadOnly = true;
		this.Col_No.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_No.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Col_A.DefaultCellStyle = dataGridViewCellStyle3;
		this.Col_A.HeaderText = "第一个音频率[Hz]";
		this.Col_A.MaxInputLength = 4;
		this.Col_A.Name = "Col_A";
		this.Col_A.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Col_B.DefaultCellStyle = dataGridViewCellStyle4;
		this.Col_B.HeaderText = "第二个音频率[Hz]";
		this.Col_B.MaxInputLength = 4;
		this.Col_B.Name = "Col_B";
		this.Col_B.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_Enc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Col_Enc.DefaultCellStyle = dataGridViewCellStyle5;
		this.Col_Enc.FillWeight = 80f;
		this.Col_Enc.HeaderText = "呼叫别名";
		this.Col_Enc.MaxInputLength = 12;
		this.Col_Enc.Name = "Col_Enc";
		this.Col_Enc.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_Enc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(657, 465);
		base.Controls.Add(this.Tab_2Tone);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Frm_2Tone";
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		base.Load += new System.EventHandler(Frm_2Tone_Load);
		this.Tab_2Tone.ResumeLayout(false);
		this.tab_P1.ResumeLayout(false);
		this.tab_P1.PerformLayout();
		this.tab_P2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvTwo).EndInit();
		base.ResumeLayout(false);
	}

	public Frm_2Tone(DataApp data)
	{
		InitializeComponent();
		tdata = data;
	}

	private void Frm_2Tone_Load(object sender, EventArgs e)
	{
		string[] strItems = new string[4];
		string tmp = "";
		if (FormMain.lang == "Chinese")
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
		}
		LanguageSel.LoadLanguage(this, typeof(Frm_2Tone));
		ComponentResourceManager crm = new ComponentResourceManager(typeof(Frm_2Tone));
		foreach (DataGridViewColumn col in dgvTwo.Columns)
		{
			crm.ApplyResources(col, col.Name);
		}
		for (int i = 0; i < 4; i++)
		{
			LanguageSel.ElseCombobox.TryGetValue(i.ToString(), out tmp);
			strItems[i] = tmp;
		}
		coBox_Rsp.Items[0] = strItems[0];
		coBox_Rsp.Items[1] = strItems[2];
		coBox_Rsp.Items[2] = strItems[3];
		tb_Atone.LostFocus -= tb_Atone_LostFocus;
		tb_Atone.LostFocus += tb_Atone_LostFocus;
		tb_Btone.LostFocus -= tb_Btone_LostFocus;
		tb_Btone.LostFocus += tb_Btone_LostFocus;
		tb_Ctone.LostFocus -= tb_Ctone_LostFocus;
		tb_Ctone.LostFocus += tb_Ctone_LostFocus;
		tb_Dtone.LostFocus -= tb_Dtone_LostFocus;
		tb_Dtone.LostFocus += tb_Dtone_LostFocus;
		dgvTwo.Rows.Clear();
		dgvTwo.Rows.Add(DatTwoToneInfo.Enc_List_Num);
		dgvTwo.LostFocus += dgvTwo_LostFocus;
	}

	private void dgvTwo_LostFocus(object sender, EventArgs e)
	{
	}

	private void bangDingSysData()
	{
		ckBox_Stone.DataBindings.Clear();
		coBox_FirstCode.DataBindings.Clear();
		coBox_SecondCode.DataBindings.Clear();
		coBox_CodeDur.DataBindings.Clear();
		coBox_CodeInt.DataBindings.Clear();
		Cobox_ResetTim.DataBindings.Clear();
		coBox_Rsp.DataBindings.Clear();
		coBox_Decfomat.DataBindings.Clear();
		tb_Btone.DataBindings.Clear();
		tb_Ctone.DataBindings.Clear();
		tb_Atone.DataBindings.Clear();
		tb_Dtone.DataBindings.Clear();
		if (tdata.dataTwoTone.DecodeRsp > 2)
		{
			tdata.dataTwoTone.DecodeRsp = 0;
		}
		if (tdata.dataTwoTone.DecFormat > 14)
		{
			tdata.dataTwoTone.DecFormat = 0;
		}
		if (tdata.dataTwoTone.FirstTone > 95)
		{
			tdata.dataTwoTone.FirstTone = 0;
		}
		if (tdata.dataTwoTone.SecondTone > 95)
		{
			tdata.dataTwoTone.SecondTone = 0;
		}
		if (tdata.dataTwoTone.ToneDur > 95)
		{
			tdata.dataTwoTone.ToneDur = 0;
		}
		if (tdata.dataTwoTone.ToneInt > 20)
		{
			tdata.dataTwoTone.ToneInt = 0;
		}
		if (tdata.dataTwoTone.ReseTim > 15)
		{
			tdata.dataTwoTone.ReseTim = 0;
		}
		ckBox_Stone.DataBindings.Add("Checked", tdata.dataTwoTone, "SToneSW", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_FirstCode.DataBindings.Add("SelectedIndex", tdata.dataTwoTone, "FirstTone", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_SecondCode.DataBindings.Add("SelectedIndex", tdata.dataTwoTone, "SecondTone", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_CodeDur.DataBindings.Add("SelectedIndex", tdata.dataTwoTone, "ToneDur", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_CodeInt.DataBindings.Add("SelectedIndex", tdata.dataTwoTone, "ToneInt", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		Cobox_ResetTim.DataBindings.Add("SelectedIndex", tdata.dataTwoTone, "ReseTim", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_Rsp.DataBindings.Add("SelectedIndex", tdata.dataTwoTone, "DecodeRsp", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_Decfomat.DataBindings.Add("SelectedIndex", tdata.dataTwoTone, "DecFormat", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		tb_Atone.DataBindings.Add("Text", tdata.dataTwoTone, "Atone", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		tb_Btone.DataBindings.Add("Text", tdata.dataTwoTone, "Btone", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		tb_Ctone.DataBindings.Add("Text", tdata.dataTwoTone, "Ctone", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		tb_Dtone.DataBindings.Add("Text", tdata.dataTwoTone, "Dtone", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
	}

	private static string Adjust_Freq(string val)
	{
		string result = "";
		int intFreq;
		try
		{
			intFreq = Convert.ToInt32(Regex.Replace(val, "[^0-9]+", ""));
			if (intFreq < 288)
			{
				intFreq = 288;
			}
			if (intFreq > 3116)
			{
				intFreq = 3116;
			}
		}
		catch
		{
			intFreq = 288;
		}
		return Convert.ToString(intFreq);
	}

	private void Cells_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar < '\u007f')
		{
			e.Handled = false;
		}
		else
		{
			e.Handled = true;
		}
	}

	private void Freq_Cells_KeyPress(object sender, KeyPressEventArgs e)
	{
		string str = ((TextBox)sender).Text;
		e.Handled = e.KeyChar < '0' || e.KeyChar > '9';
		if (e.KeyChar == '\b')
		{
			e.Handled = false;
		}
	}

	private void dgvTwo_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
	{
		int columnIndex = dgvTwo.CurrentCell.ColumnIndex;
		if (dgvTwo.CurrentCell != null)
		{
			if (columnIndex == 1 || columnIndex == 2)
			{
				initflg = false;
				dgvFreq = e.Control as TextBox;
				dgvFreq.SelectAll();
				dgvFreq.KeyPress -= Freq_Cells_KeyPress;
				dgvFreq.KeyPress += Freq_Cells_KeyPress;
				initflg = true;
			}
			else if (columnIndex == 3)
			{
				dgvName = (DataGridViewTextBoxEditingControl)e.Control;
				dgvName.SelectAll();
				dgvName.KeyPress += Cells_KeyPress;
				dgvName.TextChanged -= EditingTB_TextChanged;
				dgvName.TextChanged += EditingTB_TextChanged;
			}
		}
	}

	private void dgvTwo_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		string code = "";
		string cellval = "";
		if (dgvTwo.CurrentCell == null)
		{
			return;
		}
		int rowIndex = dgvTwo.CurrentCell.RowIndex;
		switch (dgvTwo.CurrentCell.ColumnIndex)
		{
		case 1:
			if (dgvTwo.Rows[rowIndex].Cells[1].Value == null)
			{
				if (tdata.dataTwoTone.GetFreq1(rowIndex) != "")
				{
					tdata.dataTwoTone.SetEncFreq1(rowIndex, "");
					tdata.dataTwoTone.SetEncFreq2(rowIndex, "");
					tdata.dataTwoTone.SetEncName(rowIndex, "");
					dgvTwo.Rows[rowIndex].Cells[2].Value = "";
					dgvTwo.Rows[rowIndex].Cells[3].Value = "";
					dgvTwo.Rows[rowIndex].ReadOnly = true;
					dgvTwo.Rows[rowIndex].Cells[1].ReadOnly = false;
				}
				break;
			}
			code = dgvTwo.Rows[rowIndex].Cells[1].Value.ToString();
			if (code != "")
			{
				cellval = Adjust_Freq(code);
				tdata.dataTwoTone.SetEncFreq1(rowIndex, cellval);
				dgvTwo.Rows[rowIndex].Cells[1].Value = cellval;
				dgvTwo.Rows[rowIndex].ReadOnly = false;
			}
			else if (code == "" && tdata.dataTwoTone.GetFreq1(rowIndex) != "")
			{
				tdata.dataTwoTone.SetEncFreq1(rowIndex, "");
				tdata.dataTwoTone.SetEncFreq2(rowIndex, "");
				tdata.dataTwoTone.SetEncName(rowIndex, "");
				dgvTwo.Rows[rowIndex].Cells[2].Value = "";
				dgvTwo.Rows[rowIndex].Cells[3].Value = "";
				dgvTwo.Rows[rowIndex].ReadOnly = true;
				dgvTwo.Rows[rowIndex].Cells[1].ReadOnly = false;
			}
			break;
		case 2:
			if (dgvTwo.Rows[rowIndex].Cells[2].Value == null)
			{
				tdata.dataTwoTone.SetEncFreq2(rowIndex, "");
				break;
			}
			if (tdata.dataTwoTone.GetFreq1(rowIndex) == "")
			{
				dgvTwo.Rows[rowIndex].Cells[2].Value = "";
				break;
			}
			code = dgvTwo.Rows[rowIndex].Cells[2].Value.ToString();
			if (code != "")
			{
				cellval = Adjust_Freq(code);
				tdata.dataTwoTone.SetEncFreq2(rowIndex, cellval);
				dgvTwo.Rows[rowIndex].Cells[2].Value = cellval;
			}
			break;
		}
	}

	private void tb_Atone_LostFocus(object sender, EventArgs e)
	{
		string value = ((TextBox)sender).Text;
		tb_Atone.Text = Adjust_Freq(value);
		tdata.dataTwoTone.Atone = tb_Atone.Text;
	}

	private void tb_Btone_LostFocus(object sender, EventArgs e)
	{
		string value = ((TextBox)sender).Text;
		tb_Btone.Text = Adjust_Freq(value);
		tdata.dataTwoTone.Btone = tb_Btone.Text;
	}

	private void tb_Ctone_LostFocus(object sender, EventArgs e)
	{
		string value = ((TextBox)sender).Text;
		tb_Ctone.Text = Adjust_Freq(value);
		tdata.dataTwoTone.Ctone = tb_Ctone.Text;
	}

	private void tb_Dtone_LostFocus(object sender, EventArgs e)
	{
		string value = ((TextBox)sender).Text;
		tb_Dtone.Text = Adjust_Freq(value);
		tdata.dataTwoTone.Dtone = tb_Dtone.Text;
	}

	public void TwoToneDataDisp()
	{
		int total = 0;
		bangDingSysData();
		for (int i = 0; i < DatTwoToneInfo.Enc_List_Num; i++)
		{
			dgvTwo.Rows[i].Cells[0].Value = (i + 1).ToString();
			string value = tdata.dataTwoTone.GetFreq1(i);
			if (value != "")
			{
				dgvTwo.Rows[i].ReadOnly = false;
				dgvTwo.Rows[i].Cells[1].Value = value;
				dgvTwo.Rows[i].Cells[2].Value = tdata.dataTwoTone.GetFreq2(i);
				dgvTwo.Rows[i].Cells[3].Value = tdata.dataTwoTone.GetEncName(i);
			}
			else
			{
				dgvTwo.Rows[i].ReadOnly = true;
				dgvTwo.Rows[i].Cells[1].ReadOnly = false;
				dgvTwo.Rows[i].Cells[1].Value = value;
			}
		}
	}

	private void coBox_Decfomat_SelectedIndexChanged(object sender, EventArgs e)
	{
		switch (coBox_Decfomat.SelectedIndex)
		{
		case 0:
		case 3:
			tb_Atone.Enabled = true;
			tb_Btone.Enabled = true;
			tb_Ctone.Enabled = false;
			tb_Dtone.Enabled = false;
			break;
		case 1:
		case 6:
			tb_Atone.Enabled = true;
			tb_Btone.Enabled = false;
			tb_Ctone.Enabled = true;
			tb_Dtone.Enabled = false;
			break;
		case 2:
		case 9:
			tb_Atone.Enabled = true;
			tb_Btone.Enabled = false;
			tb_Ctone.Enabled = false;
			tb_Dtone.Enabled = true;
			break;
		case 4:
		case 7:
			tb_Atone.Enabled = false;
			tb_Btone.Enabled = true;
			tb_Ctone.Enabled = true;
			tb_Dtone.Enabled = false;
			break;
		case 5:
		case 10:
			tb_Atone.Enabled = false;
			tb_Btone.Enabled = true;
			tb_Ctone.Enabled = false;
			tb_Dtone.Enabled = true;
			break;
		case 8:
		case 11:
			tb_Atone.Enabled = false;
			tb_Btone.Enabled = false;
			tb_Ctone.Enabled = true;
			tb_Dtone.Enabled = true;
			break;
		case 12:
			tb_Atone.Enabled = true;
			tb_Btone.Enabled = false;
			tb_Ctone.Enabled = false;
			tb_Dtone.Enabled = false;
			break;
		case 13:
			tb_Atone.Enabled = false;
			tb_Btone.Enabled = true;
			tb_Ctone.Enabled = false;
			tb_Dtone.Enabled = false;
			break;
		case 14:
			tb_Atone.Enabled = false;
			tb_Btone.Enabled = false;
			tb_Ctone.Enabled = true;
			tb_Dtone.Enabled = false;
			break;
		}
	}

	private void EditingTB_TextChanged(object sender, EventArgs e)
	{
		int rowIndex = dgvTwo.CurrentCell.RowIndex;
		string cellvalue = dgvName.Text;
		if (dgvTwo.CurrentCell.ColumnIndex == 3)
		{
			if (cellvalue == "")
			{
				tdata.dataTwoTone.SetEncName(rowIndex, "");
				return;
			}
			dgvTwo.Rows[rowIndex].Cells[3].Value = cellvalue;
			tdata.dataTwoTone.SetEncName(rowIndex, cellvalue);
		}
	}

	private void dgvFreq_TextChanged(object sender, EventArgs e)
	{
		string code = "";
		string cellval = dgvFreq.Text;
		if (!initflg || dgvTwo.CurrentCell == null)
		{
			return;
		}
		int rowIndex = dgvTwo.CurrentCell.RowIndex;
		switch (dgvTwo.CurrentCell.ColumnIndex)
		{
		case 1:
			if (cellval == "")
			{
				try
				{
					if (tdata.dataTwoTone.GetFreq1(rowIndex) != "")
					{
						tdata.dataTwoTone.SetEncFreq1(rowIndex, "");
						tdata.dataTwoTone.SetEncFreq2(rowIndex, "");
						tdata.dataTwoTone.SetEncName(rowIndex, "");
						dgvTwo.Rows[rowIndex].Cells[2].Value = "";
						dgvTwo.Rows[rowIndex].Cells[3].Value = "";
						dgvTwo.Rows[rowIndex].ReadOnly = true;
						dgvTwo.Rows[rowIndex].Cells[1].ReadOnly = false;
					}
					break;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
					break;
				}
			}
			try
			{
				dgvTwo.Rows[rowIndex].ReadOnly = false;
				code = Adjust_Freq(cellval);
				tdata.dataTwoTone.SetEncFreq1(rowIndex, code);
				dgvTwo.Rows[rowIndex].Cells[1].Value = code;
				break;
			}
			catch (Exception ex2)
			{
				MessageBox.Show(ex2.ToString());
				break;
			}
		case 2:
			if (cellval == "")
			{
				tdata.dataTwoTone.SetEncFreq2(rowIndex, "");
				break;
			}
			if (tdata.dataTwoTone.GetFreq1(rowIndex) == "")
			{
				dgvTwo.Rows[rowIndex].Cells[2].Value = "";
				break;
			}
			code = Adjust_Freq(cellval);
			tdata.dataTwoTone.SetEncFreq2(rowIndex, code);
			dgvTwo.Rows[rowIndex].Cells[2].Value = code;
			break;
		}
	}

	private void dgvTwo_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvTwo.CurrentCell != null)
		{
			int indexCol = e.ColumnIndex;
			int indexRow = e.RowIndex;
			if (indexRow != -1 && indexCol != -1 && indexCol == 0)
			{
				dgvTwo.CurrentCell = dgvTwo.Rows[indexRow].Cells[1];
			}
		}
	}
}
