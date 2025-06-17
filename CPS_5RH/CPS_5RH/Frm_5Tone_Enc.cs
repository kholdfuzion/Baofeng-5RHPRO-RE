using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using CPS_5RH.Data;
using DevComponents.DotNetBar.Controls;

namespace CPS_5RH;

public class Frm_5Tone_Enc : Form
{
	private DataApp tdata = null;

	private static string[] CallItems = new string[4];

	private DataGridViewTextBoxEditingControl dgvTxt = null;

	private DataGridViewTextBoxEditingControl dgvName = null;

	private IContainer components = null;

	private Label label5;

	private Label label6;

	private ComboBox coBox_TimS;

	private Label lbl4;

	private TextBox tb_Bot;

	private DataGridViewTextBoxColumn Col_CH;

	private DataGridViewTextBoxColumn Col_RxFreq;

	private DataGridViewTextBoxColumn Col_TxFreq;

	private DataGridViewX dgv_5Tone;

	private GroupBox groupBox1;

	private ComboBox coBox_StandS;

	private ComboBox coBox_StandE;

	private Label label1;

	private Label label2;

	private ComboBox coBox_TimE;

	private TextBox tb_Eot;

	private Label label3;

	private GroupBox grp1;

	private DataGridViewTextBoxColumn Col_ID;

	private DataGridViewComboBoxExColumn Col_Stand;

	private DataGridViewComboBoxExColumn Col_Tim;

	private DataGridViewTextBoxColumn Col_Call;

	private DataGridViewTextBoxColumn Col_Name;

	public Frm_5Tone_Enc(DataApp data)
	{
		InitializeComponent();
		tdata = data;
	}

	private void bingDingTheControls()
	{
		coBox_TimS.DataBindings.Clear();
		coBox_TimE.DataBindings.Clear();
		if (tdata.dataFiveToneEnc.PidStandS > 13)
		{
			tdata.dataFiveToneEnc.PidStandS = 0;
		}
		if (tdata.dataFiveToneEnc.PidStandE > 13)
		{
			tdata.dataFiveToneEnc.PidStandE = 0;
		}
		if (tdata.dataFiveToneEnc.PidCodeTimS > 5)
		{
			tdata.dataFiveToneEnc.PidCodeTimS = 0;
		}
		if (tdata.dataFiveToneEnc.PidCodeTimE > 5)
		{
			tdata.dataFiveToneEnc.PidCodeTimE = 0;
		}
		coBox_TimS.DataBindings.Add("SelectedIndex", tdata.dataFiveToneEnc, "PidCodeTimS", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_TimE.DataBindings.Add("SelectedIndex", tdata.dataFiveToneEnc, "PidCodeTimE", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		tb_Bot.Text = tdata.dataFiveToneEnc.PidStart;
		tb_Eot.Text = tdata.dataFiveToneEnc.PidEnd;
		coBox_StandS.SelectedIndex = tdata.dataFiveToneEnc.PidStandS;
		coBox_StandE.SelectedIndex = tdata.dataFiveToneEnc.PidStandE;
	}

	private void Frm_DmrSet_Load(object sender, EventArgs e)
	{
		string[] strItems = new string[5];
		string tmp = "";
		if (FormMain.lang == "Chinese")
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
		}
		ComponentResourceManager crm = new ComponentResourceManager(typeof(Frm_5Tone_Enc));
		foreach (DataGridViewColumn col in dgv_5Tone.Columns)
		{
			crm.ApplyResources(col, col.Name);
		}
		LanguageSel.LoadLanguage(this, typeof(Frm_5Tone_Enc));
		for (int i = 0; i < 5; i++)
		{
			LanguageSel.ElseCombobox.TryGetValue(i.ToString(), out tmp);
			strItems[i] = tmp;
		}
		CallItems[0] = strItems[0];
		CallItems[1] = "ANI";
		CallItems[2] = "PTTID";
		for (int i = 0; i < 100; i++)
		{
			DataGridViewRow r = new DataGridViewRow();
			r.CreateCells(dgv_5Tone);
			r.HeaderCell.Value = (i + 1).ToString();
			r.ReadOnly = true;
			r.Cells[0].ReadOnly = false;
			dgv_5Tone.Rows.Add(r);
		}
		dgv_5Tone.CurrentCell = null;
		tb_Bot.LostFocus -= tb_Bot_LostFocus;
		tb_Bot.LostFocus += tb_Bot_LostFocus;
		tb_Eot.LostFocus -= tb_Eot_LostFocus;
		tb_Eot.LostFocus += tb_Eot_LostFocus;
	}

	private void tb_Eot_LostFocus(object sender, EventArgs e)
	{
		string value = FiltString(((TextBox)sender).Text, tdata.dataFiveToneEnc.PidStandE);
		tdata.dataFiveToneEnc.PidEnd = Adjust_GetStrVal(value);
		tb_Eot.Text = tdata.dataFiveToneEnc.PidEnd;
	}

	private string FiltString(string val, int code)
	{
		string rtval = val;
		if (code == 13 || code == 8 || code == 5)
		{
			rtval = Regex.Replace(rtval, "#", "");
		}
		else if (code == 2)
		{
			rtval = Regex.Replace(rtval, "B|C|D|#", "");
		}
		else if (code == 12 || code == 10)
		{
			rtval = Regex.Replace(rtval, "A|B|C|D|#", "");
		}
		return rtval;
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

	private void tb_Bot_LostFocus(object sender, EventArgs e)
	{
		string value = FiltString(((TextBox)sender).Text, tdata.dataFiveToneEnc.PidStandS);
		tdata.dataFiveToneEnc.PidStart = Adjust_GetStrVal(value);
		tb_Bot.Text = tdata.dataFiveToneEnc.PidStart;
	}

	private void DispDgvTbl(int rowidx)
	{
		string encode = "";
		int vidx = tdata.dataFiveToneEnc.GetEncStand(rowidx);
		if (vidx > 13)
		{
			vidx = 0;
		}
		dgv_5Tone.Rows[rowidx].ReadOnly = false;
		int calltype = tdata.dataFiveToneEnc.GetEncScall(rowidx);
		encode = tdata.dataFiveToneEnc.GetEncID(rowidx);
		dgv_5Tone.Rows[rowidx].Cells[0].Value = encode;
		if (calltype > 0)
		{
			calltype--;
		}
		dgv_5Tone.Rows[rowidx].Cells[1].Value = Col_Stand.Items[vidx].ToString();
		vidx = tdata.dataFiveToneEnc.GetEncCodeTim(rowidx);
		if (vidx > 6)
		{
			vidx = 0;
		}
		dgv_5Tone.Rows[rowidx].Cells[2].Value = Col_Tim.Items[vidx].ToString();
		dgv_5Tone.Rows[rowidx].Cells[3].Value = CallItems[calltype];
		dgv_5Tone.Rows[rowidx].Cells[4].Value = tdata.dataFiveToneEnc.GetEncName(rowidx);
	}

	private void DispNewCodeTbl(int rowIndex, int mode)
	{
		if (mode == 1)
		{
			DispDgvTbl(rowIndex);
			return;
		}
		string[] values = new string[5] { "", "", "", "", "" };
		dgv_5Tone.Rows[rowIndex].SetValues(values);
		for (int i = 0; i < 5; i++)
		{
			if (i != 0)
			{
				dgv_5Tone.Rows[rowIndex].Cells[i].ReadOnly = true;
			}
		}
	}

	private void Cells_KeyPress(object sender, KeyPressEventArgs e)
	{
		if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar < 'a' || e.KeyChar > 'f') && (e.KeyChar < 'A' || e.KeyChar > 'F') && e.KeyChar != '\b' && e.KeyChar != '*' && e.KeyChar != '#')
		{
			e.Handled = true;
		}
		else if (e.KeyChar >= 'a' && e.KeyChar <= 'd')
		{
			e.KeyChar -= ' ';
		}
		else if (e.KeyChar == 'E' || e.KeyChar == 'e')
		{
			e.KeyChar = '*';
		}
		else if (e.KeyChar == 'F' || e.KeyChar == 'f')
		{
			e.KeyChar = '#';
		}
	}

	private void Cells_Ascii_KeyPress(object sender, KeyPressEventArgs e)
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

	private DialogResult ResetSpecialCall()
	{
		if (FormMain.lang == "Chinese")
		{
			return MessageBox.Show("重新设置呼叫ID", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		}
		return MessageBox.Show("Will you Reset Special Call?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
	}

	private void dgv_5Tone_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		int RowIdx = dgv_5Tone.CurrentCell.RowIndex;
		int columnIndex = dgv_5Tone.CurrentCell.ColumnIndex;
		if (RowIdx == -1 || columnIndex == -1)
		{
			return;
		}
		if (tdata.dataFiveToneEnc.GetEncCodeLen(RowIdx) == 0)
		{
			dgv_5Tone.CurrentCell = dgv_5Tone.Rows[RowIdx].Cells[0];
			return;
		}
		switch (columnIndex)
		{
		case 0:
			if (tdata.dataFiveToneEnc.GetEncScall(RowIdx) != 0)
			{
				if (ResetSpecialCall() == DialogResult.Yes)
				{
					dgv_5Tone.CurrentCell = null;
					string cellvalue = "123";
					tdata.dataFiveToneEnc.SetEncCodeLen(RowIdx, (byte)cellvalue.Length);
					tdata.dataFiveToneEnc.SetEncID(RowIdx, cellvalue);
					tdata.dataFiveToneEnc.SetEncScall(RowIdx, 0);
					DispDgvTbl(RowIdx);
				}
				else
				{
					dgv_5Tone.CurrentCell = null;
				}
			}
			break;
		case 3:
		{
			Frm_5Tone_SpecialCall fcall = new Frm_5Tone_SpecialCall(tdata.dataFiveToneEnc, tdata.dataFiveToneDec, RowIdx);
			fcall.ShowDialog();
			if (fcall.DialogResult == DialogResult.OK)
			{
				DispDgvTbl(RowIdx);
			}
			break;
		}
		}
	}

	private void dgv_5Tone_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
	{
		switch (dgv_5Tone.CurrentCell.ColumnIndex)
		{
		case 0:
			dgvTxt = (DataGridViewTextBoxEditingControl)e.Control;
			dgvTxt.SelectAll();
			dgvTxt.KeyPress -= Cells_KeyPress;
			dgvTxt.KeyPress += Cells_KeyPress;
			break;
		case 4:
			if (dgvTxt != null)
			{
				dgvTxt.KeyPress -= Cells_KeyPress;
			}
			dgvName = (DataGridViewTextBoxEditingControl)e.Control;
			dgvName.SelectAll();
			dgvName.KeyPress -= Cells_Ascii_KeyPress;
			dgvName.KeyPress += Cells_Ascii_KeyPress;
			break;
		}
	}

	private void dgv_5Tone_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		if (dgv_5Tone.CurrentCell == null)
		{
			return;
		}
		int rowIndex = dgv_5Tone.CurrentCell.RowIndex;
		int columnIndex = dgv_5Tone.CurrentCell.ColumnIndex;
		int val = 0;
		switch (columnIndex)
		{
		case 0:
		{
			if (dgv_5Tone.Rows[rowIndex].Cells[0].Value == null)
			{
				tdata.dataFiveToneEnc.SetEncCodeLen(rowIndex, 0);
				tdata.dataFiveToneEnc.SetEncID(rowIndex, "");
				tdata.dataFiveToneEnc.SetTblEn(rowIndex, 0);
				tdata.dataFiveToneEnc.SetEncStand(rowIndex, 0);
				UpDate_EmergData(rowIndex);
				DispNewCodeTbl(rowIndex, 0);
				break;
			}
			string cellvalue = FiltString(dgv_5Tone.Rows[rowIndex].Cells[0].Value.ToString(), tdata.dataFiveToneEnc.GetEncStand(rowIndex));
			if (tdata.dataFiveToneEnc.GetTblEn(rowIndex) == 0)
			{
				if (!(cellvalue == ""))
				{
					cellvalue = Adjust_GetStrVal(cellvalue);
					tdata.dataFiveToneEnc.SetEncCodeLen(rowIndex, (byte)cellvalue.Length);
					tdata.dataFiveToneEnc.SetEncID(rowIndex, cellvalue);
					tdata.dataFiveToneEnc.SetEncCodeTim(rowIndex, 0);
					tdata.dataFiveToneEnc.SetEncScall(rowIndex, 0);
					tdata.dataFiveToneEnc.SetTblEn(rowIndex, 1);
					tdata.dataFiveToneEnc.SetEncName(rowIndex, "List " + (rowIndex + 1));
					DispNewCodeTbl(rowIndex, 1);
				}
			}
			else
			{
				if (cellvalue == "")
				{
					cellvalue = "123";
				}
				cellvalue = Adjust_GetStrVal(cellvalue);
				tdata.dataFiveToneEnc.SetEncCodeLen(rowIndex, (byte)cellvalue.Length);
				tdata.dataFiveToneEnc.SetEncID(rowIndex, cellvalue);
				dgv_5Tone.Rows[rowIndex].Cells[0].Value = cellvalue;
			}
			break;
		}
		case 1:
		{
			val = Col_Stand.Items.IndexOf(dgv_5Tone.CurrentCell.Value);
			if (tdata.dataFiveToneEnc.GetEncScall(rowIndex) == 2)
			{
				val = tdata.dataFiveToneEnc.GetEncStand(rowIndex);
				dgv_5Tone.Rows[rowIndex].Cells[1].Value = Col_Stand.Items[val].ToString();
				break;
			}
			tdata.dataFiveToneEnc.SetEncStand(rowIndex, (byte)val);
			string cellvalue = FiltString(tdata.dataFiveToneEnc.GetEncID(rowIndex), val);
			if (cellvalue == "")
			{
				cellvalue = "123";
			}
			tdata.dataFiveToneEnc.SetEncID(rowIndex, cellvalue);
			dgv_5Tone.Rows[rowIndex].Cells[0].Value = cellvalue;
			break;
		}
		case 2:
			val = Col_Tim.Items.IndexOf(dgv_5Tone.CurrentCell.Value);
			tdata.dataFiveToneEnc.SetEncCodeTim(rowIndex, (byte)val);
			break;
		case 3:
			break;
		case 4:
		{
			if (dgv_5Tone.Rows[rowIndex].Cells[4].Value == null)
			{
				tdata.dataFiveToneEnc.SetEncName(rowIndex, "");
				break;
			}
			string cellvalue = dgv_5Tone.Rows[rowIndex].Cells[4].Value.ToString();
			tdata.dataFiveToneEnc.SetEncName(rowIndex, cellvalue);
			break;
		}
		}
	}

	private void UpDate_EmergData(int ros)
	{
		for (int i = 0; i < 10; i++)
		{
			if (tdata.dataEmergInfor[i].Mode == 2 && tdata.dataEmergInfor[i].GrpNo == ros + 1)
			{
				tdata.dataEmergInfor[i].GrpNo = 0;
			}
		}
	}

	public void Disp5ToneEncData()
	{
		bingDingTheControls();
		for (int i = 0; i < 100; i++)
		{
			if (tdata.dataFiveToneEnc.GetTblEn(i) == 1)
			{
				DispDgvTbl(i);
			}
		}
	}

	private void Frm_5Tone_Enc_Activated(object sender, EventArgs e)
	{
		Disp5ToneEncData();
	}

	private void coBox_StandS_SelectedIndexChanged(object sender, EventArgs e)
	{
		tdata.dataFiveToneEnc.PidStandS = (byte)coBox_StandS.SelectedIndex;
		string value = FiltString(tdata.dataFiveToneEnc.PidStart, tdata.dataFiveToneEnc.PidStandS);
		tdata.dataFiveToneEnc.PidStart = Adjust_GetStrVal(value);
		tb_Bot.Text = tdata.dataFiveToneEnc.PidStart;
	}

	private void coBox_StandE_SelectedIndexChanged(object sender, EventArgs e)
	{
		tdata.dataFiveToneEnc.PidStandE = (byte)coBox_StandE.SelectedIndex;
		string value = FiltString(tdata.dataFiveToneEnc.PidEnd, tdata.dataFiveToneEnc.PidStandE);
		tdata.dataFiveToneEnc.PidEnd = Adjust_GetStrVal(value);
		tb_Eot.Text = tdata.dataFiveToneEnc.PidEnd;
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPS_5RH.Frm_5Tone_Enc));
		this.label5 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.coBox_TimS = new System.Windows.Forms.ComboBox();
		this.lbl4 = new System.Windows.Forms.Label();
		this.tb_Bot = new System.Windows.Forms.TextBox();
		this.Col_CH = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col_RxFreq = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col_TxFreq = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dgv_5Tone = new DevComponents.DotNetBar.Controls.DataGridViewX();
		this.Col_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col_Stand = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
		this.Col_Tim = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
		this.Col_Call = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.coBox_StandE = new System.Windows.Forms.ComboBox();
		this.coBox_StandS = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.coBox_TimE = new System.Windows.Forms.ComboBox();
		this.tb_Eot = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.grp1 = new System.Windows.Forms.GroupBox();
		((System.ComponentModel.ISupportInitialize)this.dgv_5Tone).BeginInit();
		this.groupBox1.SuspendLayout();
		this.grp1.SuspendLayout();
		base.SuspendLayout();
		this.label5.Location = new System.Drawing.Point(279, 19);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(83, 20);
		this.label5.TabIndex = 4;
		this.label5.Text = "标准";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label6.Location = new System.Drawing.Point(434, 20);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(82, 20);
		this.label6.TabIndex = 5;
		this.label6.Text = "时间(ms)";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_TimS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_TimS.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_TimS.FormattingEnabled = true;
		this.coBox_TimS.Items.AddRange(new object[6] { "70", "80", "90", "100", "110", "120" });
		this.coBox_TimS.Location = new System.Drawing.Point(521, 19);
		this.coBox_TimS.Name = "coBox_TimS";
		this.coBox_TimS.Size = new System.Drawing.Size(70, 20);
		this.coBox_TimS.TabIndex = 23;
		this.lbl4.Location = new System.Drawing.Point(52, 21);
		this.lbl4.Name = "lbl4";
		this.lbl4.Size = new System.Drawing.Size(85, 18);
		this.lbl4.TabIndex = 38;
		this.lbl4.Text = "开始码";
		this.lbl4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tb_Bot.Location = new System.Drawing.Point(143, 21);
		this.tb_Bot.MaxLength = 20;
		this.tb_Bot.Name = "tb_Bot";
		this.tb_Bot.Size = new System.Drawing.Size(130, 21);
		this.tb_Bot.TabIndex = 36;
		this.tb_Bot.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Cells_KeyPress);
		this.Col_CH.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
		dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightGray;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
		this.Col_CH.DefaultCellStyle = dataGridViewCellStyle1;
		this.Col_CH.FillWeight = 70f;
		this.Col_CH.HeaderText = "信道";
		this.Col_CH.Name = "Col_CH";
		this.Col_CH.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_CH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_CH.Width = 60;
		this.Col_RxFreq.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		this.Col_RxFreq.HeaderText = "接收频率(MHz)";
		this.Col_RxFreq.MaxInputLength = 9;
		this.Col_RxFreq.Name = "Col_RxFreq";
		this.Col_RxFreq.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_RxFreq.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_RxFreq.Width = 120;
		this.Col_TxFreq.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		this.Col_TxFreq.HeaderText = "发射频率(MHz)";
		this.Col_TxFreq.MaxInputLength = 9;
		this.Col_TxFreq.Name = "Col_TxFreq";
		this.Col_TxFreq.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_TxFreq.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_TxFreq.Width = 120;
		this.dgv_5Tone.AllowUserToAddRows = false;
		this.dgv_5Tone.AllowUserToDeleteRows = false;
		this.dgv_5Tone.AllowUserToResizeColumns = false;
		this.dgv_5Tone.AllowUserToResizeRows = false;
		this.dgv_5Tone.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgv_5Tone.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
		this.dgv_5Tone.ColumnHeadersHeight = 30;
		this.dgv_5Tone.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgv_5Tone.Columns.AddRange(this.Col_ID, this.Col_Stand, this.Col_Tim, this.Col_Call, this.Col_Name);
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgv_5Tone.DefaultCellStyle = dataGridViewCellStyle6;
		this.dgv_5Tone.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dgv_5Tone.GridColor = System.Drawing.Color.FromArgb(208, 215, 229);
		this.dgv_5Tone.Location = new System.Drawing.Point(24, 114);
		this.dgv_5Tone.Margin = new System.Windows.Forms.Padding(2);
		this.dgv_5Tone.MultiSelect = false;
		this.dgv_5Tone.Name = "dgv_5Tone";
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle7.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgv_5Tone.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
		this.dgv_5Tone.RowHeadersWidth = 60;
		this.dgv_5Tone.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.dgv_5Tone.RowsDefaultCellStyle = dataGridViewCellStyle8;
		this.dgv_5Tone.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.dgv_5Tone.RowTemplate.Height = 23;
		this.dgv_5Tone.Size = new System.Drawing.Size(604, 369);
		this.dgv_5Tone.TabIndex = 60;
		this.dgv_5Tone.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgv_5Tone_CellEndEdit);
		this.dgv_5Tone.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgv_5Tone_CellClick);
		this.dgv_5Tone.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgv_5Tone_EditingControlShowing);
		this.Col_ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
		dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.White;
		dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
		this.Col_ID.DefaultCellStyle = dataGridViewCellStyle9;
		this.Col_ID.FillWeight = 70f;
		this.Col_ID.HeaderText = "码字ID";
		this.Col_ID.MaxInputLength = 24;
		this.Col_ID.Name = "Col_ID";
		this.Col_ID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_ID.Width = 170;
		this.Col_Stand.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Col_Stand.DefaultCellStyle = dataGridViewCellStyle10;
		this.Col_Stand.DisplayMember = "Text";
		this.Col_Stand.DropDownHeight = 106;
		this.Col_Stand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.Col_Stand.DropDownWidth = 121;
		this.Col_Stand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.Col_Stand.HeaderText = "标准";
		this.Col_Stand.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.Col_Stand.IntegralHeight = false;
		this.Col_Stand.ItemHeight = 20;
		this.Col_Stand.Items.AddRange(new object[14]
		{
			"ZVEI1", "ZVEI2", "ZVEI3", "PZVEI", "DZVEI", "PDZVEI", "CCIR1", "CCIR2", "PCCIR", "EEA",
			"EURO", "NATEL", "MODAT", "CCITT"
		});
		this.Col_Stand.Name = "Col_Stand";
		this.Col_Stand.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_Stand.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Col_Stand.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_Stand.Width = 70;
		this.Col_Tim.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		this.Col_Tim.DisplayMember = "Text";
		this.Col_Tim.DropDownHeight = 106;
		this.Col_Tim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.Col_Tim.DropDownWidth = 121;
		this.Col_Tim.FillWeight = 65f;
		this.Col_Tim.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.Col_Tim.HeaderText = "时间(ms)";
		this.Col_Tim.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.Col_Tim.IntegralHeight = false;
		this.Col_Tim.ItemHeight = 20;
		this.Col_Tim.Items.AddRange(new object[6] { "70", "80", "90", "100", "110", "120" });
		this.Col_Tim.Name = "Col_Tim";
		this.Col_Tim.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_Tim.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Col_Tim.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_Tim.Width = 70;
		this.Col_Call.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		this.Col_Call.HeaderText = "特殊呼叫";
		this.Col_Call.Name = "Col_Call";
		this.Col_Call.ReadOnly = true;
		this.Col_Call.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.Col_Call.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_Call.Width = 80;
		this.Col_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Col_Name.DefaultCellStyle = dataGridViewCellStyle11;
		this.Col_Name.HeaderText = "别名";
		this.Col_Name.MaxInputLength = 8;
		this.Col_Name.Name = "Col_Name";
		this.Col_Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.groupBox1.Controls.Add(this.coBox_StandE);
		this.groupBox1.Controls.Add(this.coBox_StandS);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.coBox_TimE);
		this.groupBox1.Controls.Add(this.tb_Eot);
		this.groupBox1.Controls.Add(this.coBox_TimS);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.tb_Bot);
		this.groupBox1.Controls.Add(this.lbl4);
		this.groupBox1.Location = new System.Drawing.Point(24, 20);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(604, 83);
		this.groupBox1.TabIndex = 46;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "PTT ID 编码";
		this.coBox_StandE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_StandE.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_StandE.FormattingEnabled = true;
		this.coBox_StandE.Items.AddRange(new object[14]
		{
			"ZVEI1", "ZVEI2", "ZVEI3", "PZVEI", "DZVEI", "PDZVEI", "CCIR1", "CCIR2", "PCCIR", "EEA",
			"EURO", "NATEL", "MODAT", "CCITT"
		});
		this.coBox_StandE.Location = new System.Drawing.Point(368, 46);
		this.coBox_StandE.Name = "coBox_StandE";
		this.coBox_StandE.Size = new System.Drawing.Size(70, 20);
		this.coBox_StandE.TabIndex = 59;
		this.coBox_StandE.SelectedIndexChanged += new System.EventHandler(coBox_StandE_SelectedIndexChanged);
		this.coBox_StandS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_StandS.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_StandS.FormattingEnabled = true;
		this.coBox_StandS.Items.AddRange(new object[14]
		{
			"ZVEI1", "ZVEI2", "ZVEI3", "PZVEI", "DZVEI", "PDZVEI", "CCIR1", "CCIR2", "PCCIR", "EEA",
			"EURO", "NATEL", "MODAT", "CCITT"
		});
		this.coBox_StandS.Location = new System.Drawing.Point(368, 20);
		this.coBox_StandS.Name = "coBox_StandS";
		this.coBox_StandS.Size = new System.Drawing.Size(70, 20);
		this.coBox_StandS.TabIndex = 59;
		this.coBox_StandS.SelectedIndexChanged += new System.EventHandler(coBox_StandS_SelectedIndexChanged);
		this.label1.Location = new System.Drawing.Point(279, 45);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(83, 20);
		this.label1.TabIndex = 4;
		this.label1.Text = "标准";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label2.Location = new System.Drawing.Point(432, 46);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(84, 20);
		this.label2.TabIndex = 5;
		this.label2.Text = "时间(ms)";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_TimE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_TimE.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_TimE.FormattingEnabled = true;
		this.coBox_TimE.Items.AddRange(new object[6] { "70", "80", "90", "100", "110", "120" });
		this.coBox_TimE.Location = new System.Drawing.Point(521, 45);
		this.coBox_TimE.Name = "coBox_TimE";
		this.coBox_TimE.Size = new System.Drawing.Size(70, 20);
		this.coBox_TimE.TabIndex = 23;
		this.tb_Eot.Location = new System.Drawing.Point(143, 47);
		this.tb_Eot.MaxLength = 20;
		this.tb_Eot.Name = "tb_Eot";
		this.tb_Eot.Size = new System.Drawing.Size(130, 21);
		this.tb_Eot.TabIndex = 36;
		this.tb_Eot.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Cells_KeyPress);
		this.label3.Location = new System.Drawing.Point(50, 50);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(87, 18);
		this.label3.TabIndex = 38;
		this.label3.Text = "结束码";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.grp1.Controls.Add(this.dgv_5Tone);
		this.grp1.Controls.Add(this.groupBox1);
		this.grp1.Location = new System.Drawing.Point(27, 12);
		this.grp1.Name = "grp1";
		this.grp1.Size = new System.Drawing.Size(670, 489);
		this.grp1.TabIndex = 61;
		this.grp1.TabStop = false;
		this.grp1.Text = "五音编码";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(723, 513);
		base.Controls.Add(this.grp1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Frm_5Tone_Enc";
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = "五音编码";
		base.Load += new System.EventHandler(Frm_DmrSet_Load);
		base.Activated += new System.EventHandler(Frm_5Tone_Enc_Activated);
		((System.ComponentModel.ISupportInitialize)this.dgv_5Tone).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.grp1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
