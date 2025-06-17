using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using CPS_5RH.Data;
using DevComponents.DotNetBar.Controls;

namespace CPS_5RH;

public class Frm_MdcSys : Form
{
	private DataApp tdata = null;

	private IContainer components = null;

	private DataGridViewX dgv_MdcSys;

	private DataGridViewComboBoxExColumn Col_Genral;

	private DataGridViewComboBoxExColumn Col_PttID;

	private GroupBox grp1;

	public Frm_MdcSys(DataApp data)
	{
		InitializeComponent();
		tdata = data;
	}

	private void Frm_MdcSys_Load(object sender, EventArgs e)
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
		ComponentResourceManager crm = new ComponentResourceManager(typeof(Frm_MdcSys));
		foreach (DataGridViewColumn col in dgv_MdcSys.Columns)
		{
			crm.ApplyResources(col, col.Name);
		}
		LanguageSel.LoadLanguage(this, typeof(Frm_MdcSys));
		for (int i = 0; i < 5; i++)
		{
			LanguageSel.ElseCombobox.TryGetValue((i + 7).ToString(), out tmp);
			strItems[i] = tmp;
		}
		dgv_MdcSys.Rows.Clear();
		dgv_MdcSys.Rows.Add(5);
		Col_Genral.Items[0] = strItems[0];
		Col_Genral.Items[1] = strItems[1];
		Col_Genral.Items[2] = strItems[2];
		Col_Genral.Items[3] = strItems[3];
		Col_Genral.Items[4] = strItems[4];
	}

	private void dgv_MdcSys_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		int rowIndex = dgv_MdcSys.CurrentCell.RowIndex;
		int columnIndex = dgv_MdcSys.CurrentCell.ColumnIndex;
		if (rowIndex != -1 && columnIndex != -1 && dgv_MdcSys.CurrentCell != null)
		{
			switch (columnIndex)
			{
			case 0:
			{
				int getval = tdata.dataMdcDecInfo.GetSysList(rowIndex) & 0xF;
				int setval = Col_Genral.Items.IndexOf(dgv_MdcSys.CurrentCell.Value);
				tdata.dataMdcDecInfo.SetSysList(rowIndex, (byte)(getval | (setval << 4)));
				break;
			}
			case 1:
			{
				int getval = tdata.dataMdcDecInfo.GetSysList(rowIndex) & 0xF0;
				int setval = Col_PttID.Items.IndexOf(dgv_MdcSys.CurrentCell.Value);
				tdata.dataMdcDecInfo.SetSysList(rowIndex, (byte)(setval | getval));
				break;
			}
			}
		}
	}

	public void MdcSysDisp()
	{
		for (int i = 0; i < 5; i++)
		{
			byte val = tdata.dataMdcDecInfo.GetSysList(i);
			byte IDX1 = (byte)(val >> 4);
			byte IDX2 = (byte)(val & 0xF);
			if (IDX1 > 4)
			{
				IDX1 = 0;
			}
			if (IDX2 > 4)
			{
				IDX2 = 0;
			}
			dgv_MdcSys.Rows[i].HeaderCell.Value = (i + 1).ToString();
			dgv_MdcSys.Rows[i].Cells[0].Value = Col_Genral.Items[IDX1].ToString();
			dgv_MdcSys.Rows[i].Cells[1].Value = Col_PttID.Items[IDX2].ToString();
		}
	}

	private void dgv_MdcSys_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPS_5RH.Frm_MdcSys));
		this.dgv_MdcSys = new DevComponents.DotNetBar.Controls.DataGridViewX();
		this.Col_Genral = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
		this.Col_PttID = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
		this.grp1 = new System.Windows.Forms.GroupBox();
		((System.ComponentModel.ISupportInitialize)this.dgv_MdcSys).BeginInit();
		this.grp1.SuspendLayout();
		base.SuspendLayout();
		this.dgv_MdcSys.AllowUserToAddRows = false;
		this.dgv_MdcSys.AllowUserToDeleteRows = false;
		this.dgv_MdcSys.AllowUserToResizeColumns = false;
		this.dgv_MdcSys.AllowUserToResizeRows = false;
		this.dgv_MdcSys.BackgroundColor = System.Drawing.SystemColors.Control;
		this.dgv_MdcSys.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
		this.dgv_MdcSys.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgv_MdcSys.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgv_MdcSys.ColumnHeadersHeight = 25;
		this.dgv_MdcSys.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgv_MdcSys.Columns.AddRange(this.Col_Genral, this.Col_PttID);
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgv_MdcSys.DefaultCellStyle = dataGridViewCellStyle4;
		this.dgv_MdcSys.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dgv_MdcSys.GridColor = System.Drawing.Color.FromArgb(208, 215, 229);
		this.dgv_MdcSys.Location = new System.Drawing.Point(26, 19);
		this.dgv_MdcSys.Margin = new System.Windows.Forms.Padding(2);
		this.dgv_MdcSys.MultiSelect = false;
		this.dgv_MdcSys.Name = "dgv_MdcSys";
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgv_MdcSys.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
		this.dgv_MdcSys.RowHeadersWidth = 60;
		this.dgv_MdcSys.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.dgv_MdcSys.RowsDefaultCellStyle = dataGridViewCellStyle6;
		this.dgv_MdcSys.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.dgv_MdcSys.RowTemplate.Height = 23;
		this.dgv_MdcSys.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
		this.dgv_MdcSys.Size = new System.Drawing.Size(264, 283);
		this.dgv_MdcSys.TabIndex = 47;
		this.dgv_MdcSys.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgv_MdcSys_CellEndEdit);
		this.dgv_MdcSys.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgv_MdcSys_CellContentClick);
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Col_Genral.DefaultCellStyle = dataGridViewCellStyle7;
		this.Col_Genral.DropDownHeight = 106;
		this.Col_Genral.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.Col_Genral.DropDownWidth = 121;
		this.Col_Genral.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.Col_Genral.HeaderText = "通用列表";
		this.Col_Genral.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.Col_Genral.IntegralHeight = false;
		this.Col_Genral.ItemHeight = 20;
		this.Col_Genral.Items.AddRange(new object[5] { "列表 1", "列表 2", "列表 3", "列表 4", "列表 5" });
		this.Col_Genral.Name = "Col_Genral";
		this.Col_Genral.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_Genral.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Col_Genral.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_PttID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Col_PttID.DefaultCellStyle = dataGridViewCellStyle8;
		this.Col_PttID.DisplayMember = "Text";
		this.Col_PttID.DropDownHeight = 106;
		this.Col_PttID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.Col_PttID.DropDownWidth = 121;
		this.Col_PttID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.Col_PttID.HeaderText = "PTT ID";
		this.Col_PttID.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.Col_PttID.IntegralHeight = false;
		this.Col_PttID.ItemHeight = 20;
		this.Col_PttID.Items.AddRange(new object[5] { "PTT ID 1", "PTT ID 2", "PTT ID 3", "PTT ID 4", "PTT ID 5" });
		this.Col_PttID.Name = "Col_PttID";
		this.Col_PttID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_PttID.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Col_PttID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.grp1.Controls.Add(this.dgv_MdcSys);
		this.grp1.Location = new System.Drawing.Point(23, 23);
		this.grp1.Name = "grp1";
		this.grp1.Size = new System.Drawing.Size(415, 319);
		this.grp1.TabIndex = 48;
		this.grp1.TabStop = false;
		this.grp1.Text = "MDC 系统";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(461, 353);
		base.Controls.Add(this.grp1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Frm_MdcSys";
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		base.Load += new System.EventHandler(Frm_MdcSys_Load);
		((System.ComponentModel.ISupportInitialize)this.dgv_MdcSys).EndInit();
		this.grp1.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
