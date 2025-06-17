using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CPS_5RH.Data;
using DevComponents.DotNetBar.Controls;

namespace CPS_5RH;

public class Frm_5Tone_Infocode : Form
{
	private IContainer components = null;

	private DataGridViewX dgv_InfoCd;

	private DataGridViewTextBoxColumn Col_CodeID;

	private DataGridViewComboBoxExColumn Col_Func;

	private DataGridViewComboBoxExColumn Col_DecRsp;

	private DataGridViewTextBoxColumn Col_Name;

	private GroupBox grp1;

	private DatFiveToneInfoCode tdata = null;

	private DataGridViewTextBoxEditingControl dgvTxt = null;

	private DataGridViewTextBoxEditingControl dgvName = null;

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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPS_5RH.Frm_5Tone_Infocode));
		this.dgv_InfoCd = new DevComponents.DotNetBar.Controls.DataGridViewX();
		this.Col_CodeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col_Func = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
		this.Col_DecRsp = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
		this.Col_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.grp1 = new System.Windows.Forms.GroupBox();
		((System.ComponentModel.ISupportInitialize)this.dgv_InfoCd).BeginInit();
		this.grp1.SuspendLayout();
		base.SuspendLayout();
		this.dgv_InfoCd.AllowUserToAddRows = false;
		this.dgv_InfoCd.AllowUserToDeleteRows = false;
		this.dgv_InfoCd.AllowUserToResizeColumns = false;
		this.dgv_InfoCd.AllowUserToResizeRows = false;
		this.dgv_InfoCd.BackgroundColor = System.Drawing.SystemColors.Control;
		this.dgv_InfoCd.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
		this.dgv_InfoCd.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgv_InfoCd.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgv_InfoCd.ColumnHeadersHeight = 25;
		this.dgv_InfoCd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgv_InfoCd.Columns.AddRange(this.Col_CodeID, this.Col_Func, this.Col_DecRsp, this.Col_Name);
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgv_InfoCd.DefaultCellStyle = dataGridViewCellStyle5;
		this.dgv_InfoCd.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dgv_InfoCd.GridColor = System.Drawing.Color.FromArgb(208, 215, 229);
		this.dgv_InfoCd.Location = new System.Drawing.Point(22, 34);
		this.dgv_InfoCd.Margin = new System.Windows.Forms.Padding(2);
		this.dgv_InfoCd.MultiSelect = false;
		this.dgv_InfoCd.Name = "dgv_InfoCd";
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgv_InfoCd.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
		this.dgv_InfoCd.RowHeadersWidth = 60;
		this.dgv_InfoCd.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.dgv_InfoCd.RowsDefaultCellStyle = dataGridViewCellStyle7;
		this.dgv_InfoCd.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.dgv_InfoCd.RowTemplate.Height = 23;
		this.dgv_InfoCd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
		this.dgv_InfoCd.Size = new System.Drawing.Size(489, 350);
		this.dgv_InfoCd.TabIndex = 47;
		this.dgv_InfoCd.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgv_InfoCd_CellEndEdit);
		this.dgv_InfoCd.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgv_InfoCd_CellClick);
		this.dgv_InfoCd.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgv_InfoCd_EditingControlShowing);
		this.Col_CodeID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
		dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White;
		dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
		this.Col_CodeID.DefaultCellStyle = dataGridViewCellStyle8;
		this.Col_CodeID.FillWeight = 70f;
		this.Col_CodeID.HeaderText = "码字ID";
		this.Col_CodeID.MaxInputLength = 5;
		this.Col_CodeID.Name = "Col_CodeID";
		this.Col_CodeID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_CodeID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_Func.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		this.Col_Func.DropDownHeight = 106;
		this.Col_Func.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.Col_Func.DropDownWidth = 121;
		this.Col_Func.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.Col_Func.HeaderText = "功能选项";
		this.Col_Func.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.Col_Func.IntegralHeight = false;
		this.Col_Func.ItemHeight = 16;
		this.Col_Func.Items.AddRange(new object[5] { "静噪", "遥晕", "遥毙", "唤醒", "紧急警报" });
		this.Col_Func.Name = "Col_Func";
		this.Col_Func.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_Func.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Col_Func.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_DecRsp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Col_DecRsp.DefaultCellStyle = dataGridViewCellStyle9;
		this.Col_DecRsp.DisplayMember = "Text";
		this.Col_DecRsp.DropDownHeight = 106;
		this.Col_DecRsp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.Col_DecRsp.DropDownWidth = 121;
		this.Col_DecRsp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.Col_DecRsp.HeaderText = "解码响应提示";
		this.Col_DecRsp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.Col_DecRsp.IntegralHeight = false;
		this.Col_DecRsp.ItemHeight = 20;
		this.Col_DecRsp.Items.AddRange(new object[3] { "无", "提示音", "提示音且回复" });
		this.Col_DecRsp.Name = "Col_DecRsp";
		this.Col_DecRsp.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_DecRsp.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Col_DecRsp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Col_Name.DefaultCellStyle = dataGridViewCellStyle10;
		this.Col_Name.HeaderText = "别名";
		this.Col_Name.MaxInputLength = 6;
		this.Col_Name.Name = "Col_Name";
		this.Col_Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.grp1.Controls.Add(this.dgv_InfoCd);
		this.grp1.Location = new System.Drawing.Point(26, 12);
		this.grp1.Name = "grp1";
		this.grp1.Size = new System.Drawing.Size(534, 407);
		this.grp1.TabIndex = 48;
		this.grp1.TabStop = false;
		this.grp1.Text = "五音信息码";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(582, 448);
		base.Controls.Add(this.grp1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Frm_5Tone_Infocode";
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = "Frm_5Tone_Infocode";
		base.Load += new System.EventHandler(Frm_5Tone_Infocode_Load);
		((System.ComponentModel.ISupportInitialize)this.dgv_InfoCd).EndInit();
		this.grp1.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public Frm_5Tone_Infocode(DatFiveToneInfoCode data)
	{
		InitializeComponent();
		tdata = data;
	}

	private void Frm_5Tone_Infocode_Load(object sender, EventArgs e)
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
		ComponentResourceManager crm = new ComponentResourceManager(typeof(Frm_5Tone_Infocode));
		foreach (DataGridViewColumn col in dgv_InfoCd.Columns)
		{
			crm.ApplyResources(col, col.Name);
		}
		LanguageSel.LoadLanguage(this, typeof(Frm_5Tone_Infocode));
		for (int i = 0; i < 3; i++)
		{
			LanguageSel.ElseCombobox.TryGetValue((i + 1).ToString(), out tmp);
			strItems[i] = tmp;
		}
		for (int i = 0; i < 7; i++)
		{
			LanguageSel.ElseCombobox.TryGetValue((i + 12).ToString(), out tmp);
			strItems[i + 3] = tmp;
		}
		Col_DecRsp.Items[0] = strItems[0];
		Col_DecRsp.Items[1] = strItems[1];
		Col_DecRsp.Items[2] = strItems[2];
		Col_Func.Items[0] = strItems[3];
		Col_Func.Items[1] = strItems[4];
		Col_Func.Items[2] = strItems[5];
		Col_Func.Items[3] = strItems[6];
		Col_Func.Items[4] = strItems[7];
		for (int i = 0; i < 8; i++)
		{
			DataGridViewRow r = new DataGridViewRow();
			r.CreateCells(dgv_InfoCd);
			r.HeaderCell.Value = (i + 1).ToString();
			r.ReadOnly = true;
			r.Cells[0].ReadOnly = false;
			dgv_InfoCd.Rows.Add(r);
			int len = tdata.GetCdLen(i);
			if (len > 0 && len <= 12)
			{
				DispDgvTbl(i);
			}
		}
		dgv_InfoCd.CurrentCell = null;
	}

	private void Cells_KeyPress(object sender, KeyPressEventArgs e)
	{
		if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
		{
			e.Handled = true;
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

	private void DispDgvTbl(int rowidx)
	{
		dgv_InfoCd.Rows[rowidx].ReadOnly = false;
		dgv_InfoCd.Rows[rowidx].Cells[0].Value = tdata.GetDecID(rowidx);
		int vidx = tdata.GetFunc(rowidx);
		if (vidx > 6)
		{
			vidx = 0;
		}
		dgv_InfoCd.Rows[rowidx].Cells[1].Value = Col_Func.Items[vidx].ToString();
		vidx = tdata.GetRspInfo(rowidx);
		if (vidx > 2)
		{
			vidx = 0;
		}
		dgv_InfoCd.Rows[rowidx].Cells[2].Value = Col_DecRsp.Items[vidx].ToString();
		dgv_InfoCd.Rows[rowidx].Cells[3].Value = tdata.GetDecName(rowidx);
	}

	private void DispNewCodeTbl(int rowIndex, int mode)
	{
		if (mode == 1)
		{
			DispDgvTbl(rowIndex);
			return;
		}
		string[] values = new string[4] { "", "", "", "" };
		dgv_InfoCd.Rows[rowIndex].SetValues(values);
		for (int i = 0; i < 4; i++)
		{
			if (i != 0)
			{
				dgv_InfoCd.Rows[rowIndex].Cells[i].ReadOnly = true;
			}
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

	private void EditingTB_TextChanged(object sender, EventArgs e)
	{
		int rowIndex = dgv_InfoCd.CurrentCell.RowIndex;
		string cellvalue = dgvName.Text;
		if (dgv_InfoCd.CurrentCell.ColumnIndex == 3)
		{
			if (cellvalue == "")
			{
				tdata.SetDecName(rowIndex, "");
				return;
			}
			dgv_InfoCd.Rows[rowIndex].Cells[3].Value = cellvalue;
			tdata.SetDecName(rowIndex, cellvalue);
		}
	}

	private void dgv_InfoCd_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
	{
		switch (dgv_InfoCd.CurrentCell.ColumnIndex)
		{
		case 0:
			dgvTxt = (DataGridViewTextBoxEditingControl)e.Control;
			dgvTxt.SelectAll();
			dgvTxt.KeyPress -= Cells_KeyPress;
			dgvTxt.KeyPress += Cells_KeyPress;
			break;
		case 3:
			dgvName = (DataGridViewTextBoxEditingControl)e.Control;
			dgvName.SelectAll();
			dgvName.KeyPress -= Cells_Ascii_KeyPress;
			dgvName.KeyPress += Cells_Ascii_KeyPress;
			dgvName.TextChanged -= EditingTB_TextChanged;
			dgvName.TextChanged += EditingTB_TextChanged;
			break;
		}
	}

	private void dgv_InfoCd_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		int RowIdx = dgv_InfoCd.CurrentCell.RowIndex;
	}

	private void dgv_InfoCd_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		if (dgv_InfoCd.CurrentCell == null)
		{
			return;
		}
		int rowIndex = dgv_InfoCd.CurrentCell.RowIndex;
		int columnIndex = dgv_InfoCd.CurrentCell.ColumnIndex;
		int val = 0;
		switch (columnIndex)
		{
		case 0:
		{
			if (dgv_InfoCd.Rows[rowIndex].Cells[0].Value == null)
			{
				tdata.SetCdLen(rowIndex, 0);
				tdata.SetDecID(rowIndex, "");
				DispNewCodeTbl(rowIndex, 0);
				break;
			}
			string cellvalue = dgv_InfoCd.Rows[rowIndex].Cells[0].Value.ToString();
			if (tdata.GetCdLen(rowIndex) == 0 && cellvalue.Length > 0)
			{
				cellvalue = Adjust_GetStrVal(cellvalue);
				tdata.SetCdLen(rowIndex, (byte)cellvalue.Length);
				tdata.SetDecID(rowIndex, cellvalue);
				tdata.SetRspInfo(rowIndex, 0);
				tdata.SetFunc(rowIndex, 0);
				tdata.SetDecName(rowIndex, "List " + (rowIndex + 1));
				DispNewCodeTbl(rowIndex, 1);
			}
			else
			{
				tdata.SetCdLen(rowIndex, (byte)cellvalue.Length);
				tdata.SetDecID(rowIndex, cellvalue);
			}
			break;
		}
		case 1:
			val = Col_Func.Items.IndexOf(dgv_InfoCd.CurrentCell.Value);
			tdata.SetFunc(rowIndex, (byte)val);
			break;
		case 2:
			val = Col_DecRsp.Items.IndexOf(dgv_InfoCd.CurrentCell.Value);
			tdata.SetRspInfo(rowIndex, (byte)val);
			break;
		case 3:
			break;
		}
	}

	protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
	{
		if (keyData == Keys.Return)
		{
			SendKeys.Send("{TAB}");
			if (dgv_InfoCd.CurrentCell != null)
			{
				int indexRow = dgv_InfoCd.CurrentCell.RowIndex;
				if (dgv_InfoCd.CurrentCell.ColumnIndex == 0)
				{
					SendKeys.Send("{TAB}");
				}
			}
			return true;
		}
		return base.ProcessCmdKey(ref msg, keyData);
	}
}
