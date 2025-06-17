using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using CPS_5RH.Data;
using DevComponents.DotNetBar.Controls;

namespace CPS_5RH;

public class Frm_Scan : Form
{
	private IContainer components = null;

	private Label label4;

	private Label label6;

	private ComboBox coBox_ReturnChType;

	private ComboBox coBox_ScanRange;

	private Label label7;

	private Label label8;

	private ComboBox coBox_ScanMode;

	private Label label1;

	private Label label2;

	private Label label9;

	private CheckBox ckBox_PriScan;

	private DataGridViewX dgvScan;

	private DataGridViewTextBoxColumn Col_UpFreq;

	private DataGridViewTextBoxColumn Col_DownFreq;

	private ComboBox coBox_BackScanA;

	private ComboBox coBox_RxResume;

	private ComboBox coBox_TxResume;

	private ComboBox coBox_PriCh;

	private GroupBox grp1;

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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPS_5RH.Frm_Scan));
		this.label4 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.coBox_ReturnChType = new System.Windows.Forms.ComboBox();
		this.coBox_ScanRange = new System.Windows.Forms.ComboBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.coBox_ScanMode = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.ckBox_PriScan = new System.Windows.Forms.CheckBox();
		this.dgvScan = new DevComponents.DotNetBar.Controls.DataGridViewX();
		this.Col_UpFreq = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col_DownFreq = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coBox_BackScanA = new System.Windows.Forms.ComboBox();
		this.coBox_RxResume = new System.Windows.Forms.ComboBox();
		this.coBox_TxResume = new System.Windows.Forms.ComboBox();
		this.coBox_PriCh = new System.Windows.Forms.ComboBox();
		this.grp1 = new System.Windows.Forms.GroupBox();
		((System.ComponentModel.ISupportInitialize)this.dgvScan).BeginInit();
		this.grp1.SuspendLayout();
		base.SuspendLayout();
		this.label4.Location = new System.Drawing.Point(266, 116);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(161, 18);
		this.label4.TabIndex = 9;
		this.label4.Text = "返回信道类型";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label6.Location = new System.Drawing.Point(301, 90);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(126, 18);
		this.label6.TabIndex = 11;
		this.label6.Text = "信道扫描范围";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_ReturnChType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_ReturnChType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_ReturnChType.FormattingEnabled = true;
		this.coBox_ReturnChType.Items.AddRange(new object[6] { "选定", "选定+当前通话", "最后接收通话信道", "最后使用信道", "优先信道", "优先信道+当前通话" });
		this.coBox_ReturnChType.Location = new System.Drawing.Point(433, 116);
		this.coBox_ReturnChType.Name = "coBox_ReturnChType";
		this.coBox_ReturnChType.Size = new System.Drawing.Size(132, 20);
		this.coBox_ReturnChType.TabIndex = 12;
		this.coBox_ScanRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_ScanRange.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_ScanRange.FormattingEnabled = true;
		this.coBox_ScanRange.Items.AddRange(new object[2] { "全部", "内存扫描" });
		this.coBox_ScanRange.Location = new System.Drawing.Point(433, 90);
		this.coBox_ScanRange.Name = "coBox_ScanRange";
		this.coBox_ScanRange.Size = new System.Drawing.Size(121, 20);
		this.coBox_ScanRange.TabIndex = 14;
		this.label7.Location = new System.Drawing.Point(4, 59);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(164, 18);
		this.label7.TabIndex = 17;
		this.label7.Text = "回扫时间[s]";
		this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label8.Location = new System.Drawing.Point(297, 63);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(133, 18);
		this.label8.TabIndex = 18;
		this.label8.Text = "优先扫描信道号";
		this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_ScanMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_ScanMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_ScanMode.FormattingEnabled = true;
		this.coBox_ScanMode.Items.AddRange(new object[3] { "TO", "CO", "SO" });
		this.coBox_ScanMode.Location = new System.Drawing.Point(171, 33);
		this.coBox_ScanMode.Name = "coBox_ScanMode";
		this.coBox_ScanMode.Size = new System.Drawing.Size(89, 20);
		this.coBox_ScanMode.TabIndex = 20;
		this.label1.Location = new System.Drawing.Point(65, 33);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(100, 18);
		this.label1.TabIndex = 1;
		this.label1.Text = "扫描模式";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label2.Location = new System.Drawing.Point(4, 86);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(164, 18);
		this.label2.TabIndex = 22;
		this.label2.Text = "接收恢复延迟时间[s]";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label9.Location = new System.Drawing.Point(1, 116);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(164, 18);
		this.label9.TabIndex = 25;
		this.label9.Text = "发射恢复延迟时间[s]";
		this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.ckBox_PriScan.Location = new System.Drawing.Point(336, 33);
		this.ckBox_PriScan.Name = "ckBox_PriScan";
		this.ckBox_PriScan.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.ckBox_PriScan.Size = new System.Drawing.Size(104, 24);
		this.ckBox_PriScan.TabIndex = 26;
		this.ckBox_PriScan.Text = "优先扫描";
		this.ckBox_PriScan.UseVisualStyleBackColor = true;
		this.dgvScan.AllowUserToAddRows = false;
		this.dgvScan.AllowUserToDeleteRows = false;
		this.dgvScan.AllowUserToResizeColumns = false;
		this.dgvScan.AllowUserToResizeRows = false;
		this.dgvScan.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvScan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvScan.ColumnHeadersHeight = 25;
		this.dgvScan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvScan.Columns.AddRange(this.Col_UpFreq, this.Col_DownFreq);
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvScan.DefaultCellStyle = dataGridViewCellStyle2;
		this.dgvScan.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dgvScan.GridColor = System.Drawing.Color.FromArgb(208, 215, 229);
		this.dgvScan.Location = new System.Drawing.Point(137, 159);
		this.dgvScan.Margin = new System.Windows.Forms.Padding(2);
		this.dgvScan.MultiSelect = false;
		this.dgvScan.Name = "dgvScan";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvScan.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
		this.dgvScan.RowHeadersWidth = 50;
		this.dgvScan.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.dgvScan.RowsDefaultCellStyle = dataGridViewCellStyle4;
		this.dgvScan.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.dgvScan.RowTemplate.Height = 20;
		this.dgvScan.Size = new System.Drawing.Size(303, 239);
		this.dgvScan.TabIndex = 27;
		this.dgvScan.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvScan_CellEndEdit);
		this.dgvScan.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgvScan_EditingControlShowing);
		this.Col_UpFreq.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.Col_UpFreq.HeaderText = "上限频率(MHz)";
		this.Col_UpFreq.MaxInputLength = 3;
		this.Col_UpFreq.Name = "Col_UpFreq";
		this.Col_UpFreq.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_UpFreq.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_DownFreq.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		this.Col_DownFreq.HeaderText = "下限频率(MHz)";
		this.Col_DownFreq.MaxInputLength = 3;
		this.Col_DownFreq.Name = "Col_DownFreq";
		this.Col_DownFreq.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_DownFreq.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_DownFreq.Width = 120;
		this.coBox_BackScanA.DropDownHeight = 120;
		this.coBox_BackScanA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_BackScanA.DropDownWidth = 89;
		this.coBox_BackScanA.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_BackScanA.FormattingEnabled = true;
		this.coBox_BackScanA.IntegralHeight = false;
		this.coBox_BackScanA.Items.AddRange(new object[46]
		{
			"0.5", "0.6", "0.7", "0.8", "0.9", "1.0", "1.1", "1.2", "1.3", "1.4",
			"1.5", "1.6", "1.7", "1.8", "1.9", "2.0", "2.1", "2.2", "2.3", "2.4",
			"2.5", "2.6", "2.7", "2.8", "2.9", "3.0", "3.1", "3.2", "3.3", "3.4",
			"3.5", "3.6", "3.7", "3.8", "3.9", "4.0", "4.1", "4.2", "4.3", "4.4",
			"4.5", "4.6", "4.7", "4.8", "4.9", "5.0"
		});
		this.coBox_BackScanA.Location = new System.Drawing.Point(171, 59);
		this.coBox_BackScanA.Name = "coBox_BackScanA";
		this.coBox_BackScanA.Size = new System.Drawing.Size(89, 20);
		this.coBox_BackScanA.TabIndex = 35;
		this.coBox_RxResume.DropDownHeight = 120;
		this.coBox_RxResume.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_RxResume.DropDownWidth = 89;
		this.coBox_RxResume.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_RxResume.FormattingEnabled = true;
		this.coBox_RxResume.IntegralHeight = false;
		this.coBox_RxResume.Items.AddRange(new object[50]
		{
			"0.1", "0.2", "0.3", "0.4", "0.5", "0.6", "0.7", "0.8", "0.9", "1.0",
			"1.1", "1.2", "1.3", "1.4", "1.5", "1.6", "1.7", "1.8", "1.9", "2.0",
			"2.1", "2.2", "2.3", "2.4", "2.5", "2.6", "2.7", "2.8", "2.9", "3.0",
			"3.1", "3.2", "3.3", "3.4", "3.5", "3.6", "3.7", "3.8", "3.9", "4.0",
			"4.1", "4.2", "4.3", "4.4", "4.5", "4.6", "4.7", "4.8", "4.9", "5.0"
		});
		this.coBox_RxResume.Location = new System.Drawing.Point(171, 87);
		this.coBox_RxResume.Name = "coBox_RxResume";
		this.coBox_RxResume.Size = new System.Drawing.Size(89, 20);
		this.coBox_RxResume.TabIndex = 38;
		this.coBox_TxResume.DropDownHeight = 120;
		this.coBox_TxResume.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_TxResume.DropDownWidth = 89;
		this.coBox_TxResume.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_TxResume.FormattingEnabled = true;
		this.coBox_TxResume.IntegralHeight = false;
		this.coBox_TxResume.Items.AddRange(new object[50]
		{
			"0.1", "0.2", "0.3", "0.4", "0.5", "0.6", "0.7", "0.8", "0.9", "1.0",
			"1.1", "1.2", "1.3", "1.4", "1.5", "1.6", "1.7", "1.8", "1.9", "2.0",
			"2.1", "2.2", "2.3", "2.4", "2.5", "2.6", "2.7", "2.8", "2.9", "3.0",
			"3.1", "3.2", "3.3", "3.4", "3.5", "3.6", "3.7", "3.8", "3.9", "4.0",
			"4.1", "4.2", "4.3", "4.4", "4.5", "4.6", "4.7", "4.8", "4.9", "5.0"
		});
		this.coBox_TxResume.Location = new System.Drawing.Point(171, 116);
		this.coBox_TxResume.Name = "coBox_TxResume";
		this.coBox_TxResume.Size = new System.Drawing.Size(89, 20);
		this.coBox_TxResume.TabIndex = 39;
		this.coBox_PriCh.DropDownHeight = 120;
		this.coBox_PriCh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_PriCh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_PriCh.FormattingEnabled = true;
		this.coBox_PriCh.IntegralHeight = false;
		this.coBox_PriCh.Items.AddRange(new object[64]
		{
			"1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
			"11", "12", "13", "14", "15", "16", "17", "18", "19", "20",
			"21", "22", "23", "24", "25", "26", "27", "28", "29", "30",
			"31", "32", "33", "34", "35", "36", "37", "38", "39", "40",
			"41", "42", "43", "44", "45", "46", "47", "48", "49", "50",
			"51", "52", "53", "54", "55", "56", "57", "58", "59", "60",
			"61", "62", "63", "64"
		});
		this.coBox_PriCh.Location = new System.Drawing.Point(433, 63);
		this.coBox_PriCh.Name = "coBox_PriCh";
		this.coBox_PriCh.Size = new System.Drawing.Size(121, 20);
		this.coBox_PriCh.TabIndex = 40;
		this.grp1.Controls.Add(this.coBox_ScanMode);
		this.grp1.Controls.Add(this.coBox_PriCh);
		this.grp1.Controls.Add(this.label4);
		this.grp1.Controls.Add(this.coBox_TxResume);
		this.grp1.Controls.Add(this.label6);
		this.grp1.Controls.Add(this.coBox_RxResume);
		this.grp1.Controls.Add(this.coBox_ReturnChType);
		this.grp1.Controls.Add(this.coBox_BackScanA);
		this.grp1.Controls.Add(this.coBox_ScanRange);
		this.grp1.Controls.Add(this.dgvScan);
		this.grp1.Controls.Add(this.label7);
		this.grp1.Controls.Add(this.ckBox_PriScan);
		this.grp1.Controls.Add(this.label8);
		this.grp1.Controls.Add(this.label9);
		this.grp1.Controls.Add(this.label1);
		this.grp1.Controls.Add(this.label2);
		this.grp1.Location = new System.Drawing.Point(12, 12);
		this.grp1.Name = "grp1";
		this.grp1.Size = new System.Drawing.Size(583, 405);
		this.grp1.TabIndex = 41;
		this.grp1.TabStop = false;
		this.grp1.Text = "扫描设置";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(607, 429);
		base.Controls.Add(this.grp1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Frm_Scan";
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = "Frm_Scan";
		base.Load += new System.EventHandler(Frm_Scan_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvScan).EndInit();
		this.grp1.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public Frm_Scan(DataApp data)
	{
		InitializeComponent();
		tdata = data;
	}

	private void bingDingTheControls()
	{
		if (tdata.dataScanInfor.ScanMode > 2)
		{
			tdata.dataScanInfor.ScanMode = 0;
		}
		if (tdata.dataScanInfor.ScanRange > 1)
		{
			tdata.dataScanInfor.ScanRange = 0;
		}
		if (tdata.dataScanInfor.RtnChType > 5)
		{
			tdata.dataScanInfor.RtnChType = 0;
		}
		if (tdata.dataScanInfor.PrioChannel > 63)
		{
			tdata.dataScanInfor.PrioChannel = 0;
		}
		coBox_ScanMode.DataBindings.Add("SelectedIndex", tdata.dataScanInfor, "ScanMode", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_ScanRange.DataBindings.Add("SelectedIndex", tdata.dataScanInfor, "ScanRange", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_ReturnChType.DataBindings.Add("SelectedIndex", tdata.dataScanInfor, "RtnChType", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_BackScanA.DataBindings.Add("SelectedIndex", tdata.dataScanInfor, "BackScanTim", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_RxResume.DataBindings.Add("SelectedIndex", tdata.dataScanInfor, "RxResume", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_TxResume.DataBindings.Add("SelectedIndex", tdata.dataScanInfor, "TxResume", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_PriCh.DataBindings.Add("SelectedIndex", tdata.dataScanInfor, "PrioChannel", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		ckBox_PriScan.DataBindings.Add("Checked", tdata.dataScanInfor, "PrioScan", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
	}

	private void Cells_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == '\b')
		{
			e.Handled = false;
			if (dgvTxt.Text.Length == 0 && e.KeyChar == '.')
			{
				e.Handled = true;
			}
		}
		else
		{
			e.Handled = true;
		}
	}

	private void HandUpFreq(int rowIndex)
	{
		string value = "";
		int dwfreq = tdata.dataScanInfor.GetDwFreq(rowIndex);
		if (dgvScan.CurrentCell.Value == null)
		{
			value = (tdata.dataScanInfor.GetUpFreq(rowIndex) / 100000).ToString("0.00000");
			dgvScan.Rows[rowIndex].Cells[0].Value = value;
			return;
		}
		if (dgvScan.CurrentCell.Value.ToString() == "")
		{
			value = (tdata.dataScanInfor.GetUpFreq(rowIndex) / 100000).ToString("0.00000");
			dgvScan.Rows[rowIndex].Cells[0].Value = value;
			return;
		}
		value = Convert.ToString(dgvScan.CurrentCell.Value);
		int bufForFreq_Int = (int)tdata.dataDevice.Adjust_Freq(value);
		value = bufForFreq_Int.ToString("0.00000");
		int intfreq = Convert.ToInt32(bufForFreq_Int * 100000);
		if (intfreq <= dwfreq)
		{
			tdata.dataScanInfor.SetUpFreq(rowIndex, intfreq);
			dgvScan.Rows[rowIndex].Cells[0].Value = value;
		}
		else
		{
			tdata.dataScanInfor.SetUpFreq(rowIndex, dwfreq);
			value = (dwfreq / 100000).ToString("0.00000");
			dgvScan.Rows[rowIndex].Cells[0].Value = value;
		}
	}

	private void HandDwFreq(int rowIndex)
	{
		string value = "";
		int upfreq = tdata.dataScanInfor.GetUpFreq(rowIndex);
		if (dgvScan.CurrentCell.Value == null)
		{
			value = (tdata.dataScanInfor.GetDwFreq(rowIndex) / 100000).ToString("0.00000");
			dgvScan.Rows[rowIndex].Cells[1].Value = value;
			return;
		}
		if (dgvScan.CurrentCell.Value.ToString() == "")
		{
			value = (tdata.dataScanInfor.GetDwFreq(rowIndex) / 100000).ToString("0.00000");
			dgvScan.Rows[rowIndex].Cells[1].Value = value;
			return;
		}
		value = Convert.ToString(dgvScan.CurrentCell.Value);
		int bufForFreq_Int = (int)tdata.dataDevice.Adjust_Freq(value);
		value = bufForFreq_Int.ToString("0.00000");
		int intfreq = Convert.ToInt32(bufForFreq_Int * 100000);
		if (intfreq >= upfreq)
		{
			tdata.dataScanInfor.SetDwFreq(rowIndex, intfreq);
			dgvScan.Rows[rowIndex].Cells[1].Value = value;
		}
		else
		{
			tdata.dataScanInfor.SetDwFreq(rowIndex, upfreq);
			value = (upfreq / 100000).ToString("0.00000");
			dgvScan.Rows[rowIndex].Cells[1].Value = value;
		}
	}

	private void dgvScan_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
	{
		switch (dgvScan.CurrentCell.ColumnIndex)
		{
		case 1:
		case 2:
			dgvTxt = (DataGridViewTextBoxEditingControl)e.Control;
			dgvTxt.SelectAll();
			dgvTxt.KeyPress += Cells_KeyPress;
			break;
		}
	}

	private void dgvScan_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvScan.CurrentCell != null)
		{
			int rowIndex = dgvScan.CurrentCell.RowIndex;
			switch (dgvScan.CurrentCell.ColumnIndex)
			{
			case 0:
				HandUpFreq(rowIndex);
				break;
			case 1:
				HandDwFreq(rowIndex);
				break;
			}
		}
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (keyData == Keys.Return)
		{
			SendKeys.Send("{TAB}");
			if (dgvScan.CurrentCell != null)
			{
				int indexRow = dgvScan.CurrentCell.RowIndex;
				int indexCol = dgvScan.CurrentCell.ColumnIndex;
				if (indexCol == 0 || indexCol == 1)
				{
					SendKeys.Send("{TAB}");
				}
			}
			return true;
		}
		return base.ProcessCmdKey(ref msg, keyData);
	}

	public static Frm_Scan getInstance(FormMain father, DataApp tdata)
	{
		Frm_Scan form = new Frm_Scan(tdata);
		form.MdiParent = father;
		return form;
	}

	private void Frm_Scan_Load(object sender, EventArgs e)
	{
		string value = "";
		string[] strItems = new string[12];
		string tmp = "";
		if (FormMain.lang == "Chinese")
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
		}
		ComponentResourceManager crm = new ComponentResourceManager(typeof(Frm_Scan));
		foreach (DataGridViewColumn col in dgvScan.Columns)
		{
			crm.ApplyResources(col, col.Name);
		}
		LanguageSel.LoadLanguage(this, typeof(Frm_Scan));
		for (int i = 0; i < 12; i++)
		{
			LanguageSel.ScanCoBox.TryGetValue(i.ToString(), out tmp);
			strItems[i] = tmp;
		}
		coBox_ScanMode.Items[0] = strItems[9];
		coBox_ScanMode.Items[1] = strItems[10];
		coBox_ScanMode.Items[2] = strItems[11];
		coBox_ScanRange.Items[0] = strItems[0];
		coBox_ScanRange.Items[1] = strItems[1];
		coBox_ReturnChType.Items[0] = strItems[3];
		coBox_ReturnChType.Items[1] = strItems[4];
		coBox_ReturnChType.Items[2] = strItems[5];
		coBox_ReturnChType.Items[3] = strItems[6];
		coBox_ReturnChType.Items[4] = strItems[7];
		coBox_ReturnChType.Items[5] = strItems[8];
		for (int i = 0; i < 1; i++)
		{
			DataGridViewRow r = new DataGridViewRow();
			r.CreateCells(dgvScan);
			r.HeaderCell.Value = (i + 1).ToString();
			dgvScan.Rows.Add(r);
			value = ((double)tdata.dataScanInfor.GetUpFreq(i) / 100000.0).ToString("0.00000");
			dgvScan.Rows[i].Cells[0].Value = value;
			value = ((double)tdata.dataScanInfor.GetDwFreq(i) / 100000.0).ToString("0.00000");
			dgvScan.Rows[i].Cells[1].Value = value;
		}
		dgvScan.CurrentCell = null;
		bingDingTheControls();
	}
}
