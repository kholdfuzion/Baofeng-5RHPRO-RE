using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using CPS_5RH.Data;
using DevComponents.DotNetBar.Controls;

namespace CPS_5RH;

public class Frm_MDC_Dec : Form
{
	private IContainer components = null;

	private DataGridViewX dgv_MdcDec;

	private GroupBox grp1;

	private DataGridViewCheckBoxColumn Col_Enable;

	private DataGridViewTextBoxColumn Col_CodeID;

	private DataGridViewComboBoxExColumn Col_DecRsp;

	private DataGridViewTextBoxColumn Col_Name;

	private DatMdcDecInfo tdata = null;

	private DataGridViewTextBoxEditingControl dgvTxt = new DataGridViewTextBoxEditingControl();

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPS_5RH.Frm_MDC_Dec));
		this.dgv_MdcDec = new DevComponents.DotNetBar.Controls.DataGridViewX();
		this.grp1 = new System.Windows.Forms.GroupBox();
		this.Col_Enable = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.Col_CodeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col_DecRsp = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
		this.Col_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
		((System.ComponentModel.ISupportInitialize)this.dgv_MdcDec).BeginInit();
		this.grp1.SuspendLayout();
		base.SuspendLayout();
		this.dgv_MdcDec.AllowUserToAddRows = false;
		this.dgv_MdcDec.AllowUserToDeleteRows = false;
		this.dgv_MdcDec.AllowUserToResizeColumns = false;
		this.dgv_MdcDec.AllowUserToResizeRows = false;
		this.dgv_MdcDec.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
		this.dgv_MdcDec.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgv_MdcDec.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgv_MdcDec.ColumnHeadersHeight = 25;
		this.dgv_MdcDec.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgv_MdcDec.Columns.AddRange(this.Col_Enable, this.Col_CodeID, this.Col_DecRsp, this.Col_Name);
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgv_MdcDec.DefaultCellStyle = dataGridViewCellStyle5;
		this.dgv_MdcDec.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dgv_MdcDec.GridColor = System.Drawing.Color.FromArgb(208, 215, 229);
		this.dgv_MdcDec.Location = new System.Drawing.Point(33, 29);
		this.dgv_MdcDec.Margin = new System.Windows.Forms.Padding(2);
		this.dgv_MdcDec.MultiSelect = false;
		this.dgv_MdcDec.Name = "dgv_MdcDec";
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgv_MdcDec.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
		this.dgv_MdcDec.RowHeadersWidth = 60;
		this.dgv_MdcDec.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.dgv_MdcDec.RowsDefaultCellStyle = dataGridViewCellStyle7;
		this.dgv_MdcDec.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.dgv_MdcDec.RowTemplate.Height = 23;
		this.dgv_MdcDec.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
		this.dgv_MdcDec.Size = new System.Drawing.Size(489, 350);
		this.dgv_MdcDec.TabIndex = 46;
		this.dgv_MdcDec.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgv_MdcDec_CellEndEdit);
		this.dgv_MdcDec.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgv_MdcDec_CellClick);
		this.dgv_MdcDec.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgv_MdcDec_EditingControlShowing);
		this.grp1.Controls.Add(this.dgv_MdcDec);
		this.grp1.Location = new System.Drawing.Point(21, 12);
		this.grp1.Name = "grp1";
		this.grp1.Size = new System.Drawing.Size(571, 399);
		this.grp1.TabIndex = 47;
		this.grp1.TabStop = false;
		this.grp1.Text = "MDC 解码";
		this.Col_Enable.HeaderText = "开启";
		this.Col_Enable.Name = "Col_Enable";
		this.Col_Enable.ReadOnly = true;
		this.Col_Enable.Width = 60;
		this.Col_CodeID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
		dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
		dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.White;
		dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
		this.Col_CodeID.DefaultCellStyle = dataGridViewCellStyle8;
		this.Col_CodeID.FillWeight = 70f;
		this.Col_CodeID.HeaderText = "发码ID";
		this.Col_CodeID.MaxInputLength = 4;
		this.Col_CodeID.Name = "Col_CodeID";
		this.Col_CodeID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_CodeID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
		this.Col_DecRsp.Items.AddRange(new object[7] { "NULL", "Pi", "PiPi", "Pi-", "PiPiPiPiPi", "PiRo1", "PiRo2" });
		this.Col_DecRsp.Name = "Col_DecRsp";
		this.Col_DecRsp.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_DecRsp.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Col_DecRsp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Col_Name.DefaultCellStyle = dataGridViewCellStyle10;
		this.Col_Name.HeaderText = "别名";
		this.Col_Name.MaxInputLength = 12;
		this.Col_Name.Name = "Col_Name";
		this.Col_Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(614, 425);
		base.Controls.Add(this.grp1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Frm_MDC_Dec";
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = "Frm_MDC_Dec";
		base.Load += new System.EventHandler(Frm_MDC_Dec_Load);
		((System.ComponentModel.ISupportInitialize)this.dgv_MdcDec).EndInit();
		this.grp1.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public Frm_MDC_Dec(DatMdcDecInfo data)
	{
		InitializeComponent();
		tdata = data;
	}

	private void Frm_MDC_Dec_Load(object sender, EventArgs e)
	{
		if (FormMain.lang == "Chinese")
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
		}
		ComponentResourceManager crm = new ComponentResourceManager(typeof(Frm_MDC_Dec));
		foreach (DataGridViewColumn col in dgv_MdcDec.Columns)
		{
			crm.ApplyResources(col, col.Name);
		}
		LanguageSel.LoadLanguage(this, typeof(Frm_MDC_Dec));
		for (int i = 0; i < 100; i++)
		{
			DataGridViewRow r = new DataGridViewRow();
			r.CreateCells(dgv_MdcDec);
			r.HeaderCell.Value = (i + 1).ToString();
			r.ReadOnly = true;
			r.Cells[0].ReadOnly = false;
			dgv_MdcDec.Rows.Add(r);
			DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgv_MdcDec.Rows[i].Cells[0];
			bool flag = Convert.ToBoolean(checkCell.Value);
			if (tdata.GetTblEn(i) == 0)
			{
				checkCell.Value = false;
				DispNewCodeTbl(i, 0);
			}
			else
			{
				checkCell.Value = true;
				DispNewCodeTbl(i, 1);
			}
		}
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
		int vidx = tdata.GetDecRsp(rowidx);
		if (vidx > 6)
		{
			vidx = 0;
		}
		dgv_MdcDec.Rows[rowidx].Cells[1].ReadOnly = false;
		dgv_MdcDec.Rows[rowidx].Cells[2].ReadOnly = false;
		dgv_MdcDec.Rows[rowidx].Cells[3].ReadOnly = false;
		dgv_MdcDec.Rows[rowidx].Cells[1].Value = tdata.GetDecID(rowidx);
		dgv_MdcDec.Rows[rowidx].Cells[2].Value = Col_DecRsp.Items[vidx].ToString();
		dgv_MdcDec.Rows[rowidx].Cells[3].Value = tdata.GetDecName(rowidx);
	}

	private void DispNewCodeTbl(int rowIndex, int mode)
	{
		if (mode == 1)
		{
			DispDgvTbl(rowIndex);
			return;
		}
		for (int i = 1; i < 4; i++)
		{
			dgv_MdcDec.Rows[rowIndex].Cells[i].Value = "";
			dgv_MdcDec.Rows[rowIndex].Cells[i].ReadOnly = true;
		}
	}

	private void dgv_MdcDec_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
	{
		switch (dgv_MdcDec.CurrentCell.ColumnIndex)
		{
		case 1:
			dgvTxt = (DataGridViewTextBoxEditingControl)e.Control;
			dgvTxt.SelectAll();
			dgvTxt.KeyPress -= Cells_KeyPress;
			dgvTxt.KeyPress += Cells_KeyPress;
			break;
		case 3:
			dgvName = (DataGridViewTextBoxEditingControl)e.Control;
			dgvName.SelectAll();
			dgvTxt.KeyPress -= Cells_KeyPress;
			dgvName.TextChanged -= EditingTB_TextChanged;
			dgvName.TextChanged += EditingTB_TextChanged;
			break;
		}
	}

	private void EditingTB_TextChanged(object sender, EventArgs e)
	{
		int rowIndex = dgv_MdcDec.CurrentCell.RowIndex;
		string value = "";
		string tmp = (sender as TextBox).Text;
		if (dgv_MdcDec.CurrentCell.ColumnIndex != 3)
		{
			return;
		}
		dgv_MdcDec.CurrentCell.Value = tmp;
		if (tmp == "")
		{
			tdata.SetDecName(rowIndex, "");
			return;
		}
		value = ((sender as TextBox).Text = DataApp.StrFormat(tmp, 12));
		tdata.SetDecName(rowIndex, value);
		if (DataApp.GetLength(value) >= 12)
		{
			dgvName.SelectionStart = tmp.Length;
		}
	}

	private void dgv_MdcDec_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		int columnIndex = e.ColumnIndex;
		int rowIndex = e.RowIndex;
		string cellvalue = "";
		if (rowIndex == -1 || columnIndex == -1 || dgv_MdcDec.CurrentCell == null)
		{
			return;
		}
		cellvalue = ((!(FormMain.lang == "Chinese")) ? "Contact " : "联系人 ");
		if (columnIndex == 0)
		{
			DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgv_MdcDec.Rows[rowIndex].Cells[0];
			if (Convert.ToBoolean(checkCell.Value))
			{
				checkCell.Value = false;
				tdata.SetTblEn(rowIndex, 0);
				tdata.SetDecID(rowIndex, "");
				tdata.SetDecName(rowIndex, "");
				tdata.SetDecRsp(rowIndex, 0);
				DispNewCodeTbl(rowIndex, 0);
			}
			else
			{
				checkCell.Value = true;
				tdata.SetTblEn(rowIndex, 1);
				tdata.SetDecID(rowIndex, "1234");
				cellvalue += rowIndex + 1;
				tdata.SetDecName(rowIndex, cellvalue);
				DispNewCodeTbl(rowIndex, 1);
			}
		}
	}

	private void dgv_MdcDec_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		if (dgv_MdcDec.CurrentCell == null)
		{
			return;
		}
		int rowIndex = dgv_MdcDec.CurrentCell.RowIndex;
		int columnIndex = dgv_MdcDec.CurrentCell.ColumnIndex;
		int val = 0;
		switch (columnIndex)
		{
		case 0:
			break;
		case 1:
		{
			if (dgv_MdcDec.Rows[rowIndex].Cells[1].Value == null)
			{
				dgv_MdcDec.Rows[rowIndex].Cells[1].Value = tdata.GetDecID(rowIndex);
				break;
			}
			string cellvalue = dgv_MdcDec.Rows[rowIndex].Cells[1].Value.ToString().PadLeft(4, '0');
			tdata.SetDecID(rowIndex, cellvalue);
			dgv_MdcDec.Rows[rowIndex].Cells[1].Value = cellvalue;
			break;
		}
		case 2:
			val = Col_DecRsp.Items.IndexOf(dgv_MdcDec.CurrentCell.Value);
			tdata.SetDecRsp(rowIndex, (byte)val);
			break;
		case 3:
			break;
		}
	}
}
