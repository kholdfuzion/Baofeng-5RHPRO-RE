using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using CPS_5RH.Data;

namespace CPS_5RH;

public class Frm_ZoneConfig : Form
{
	private static int posIdx = 0;

	public static bool nodeFlg = false;

	private static bool initFlg = false;

	private DataApp tdata = null;

	private IContainer components = null;

	private DataGridView ZoneConfigView;

	private DataGridViewTextBoxColumn Col_ZName;

	private DataGridViewTextBoxColumn Col_ValidCnt;

	private DataGridViewTextBoxColumn Col_MaxCnt;

	public Frm_ZoneConfig(DataApp data)
	{
		InitializeComponent();
		tdata = data;
	}

	private void Frm_Zone_Load(object sender, EventArgs e)
	{
		if (FormMain.lang == "Chinese")
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
			ZoneConfigView.TopLeftHeaderCell.Value = "区域";
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
			ZoneConfigView.TopLeftHeaderCell.Value = "Zone";
		}
		ComponentResourceManager crm = new ComponentResourceManager(typeof(Frm_ZoneConfig));
		foreach (DataGridViewColumn col in ZoneConfigView.Columns)
		{
			crm.ApplyResources(col, col.Name);
		}
		LanguageSel.LoadLanguage(this, typeof(Frm_ZoneConfig));
		ZoneConfigView.Rows.Add(DataApp.Zone_Max_Num);
		for (int i = 0; i < DataApp.Zone_Max_Num; i++)
		{
			ZoneConfigView.Rows[i].HeaderCell.Value = (i + 1).ToString();
		}
	}

	public void ZoneDataDisp(int pos)
	{
		int chnum = 0;
		initFlg = false;
		posIdx = pos + 1;
		for (int i = 0; i < DataApp.Zone_Max_Num; i++)
		{
			chnum = 0;
			int idx = i * DataApp.Zone_Max_Chn_Num;
			for (int j = 0; j < DataApp.Zone_Max_Chn_Num; j++)
			{
				if (tdata.dataChnInfor[idx + j].UseFlg == 1)
				{
					chnum++;
				}
			}
			if (chnum > 0)
			{
				ZoneConfigView.Rows[i].Cells[0].Value = tdata.dataZoneInfor[i].ZoneName;
				ZoneConfigView.Rows[i].Cells[1].Value = chnum.ToString();
				ZoneConfigView.Rows[i].Cells[2].Value = DataApp.Zone_Max_Chn_Num;
			}
			else
			{
				ZoneConfigView.Rows[i].Cells[0].Value = "";
				ZoneConfigView.Rows[i].Cells[1].Value = "";
				ZoneConfigView.Rows[i].Cells[2].Value = "";
			}
		}
		initFlg = true;
		ZoneConfigView.CurrentCell = ZoneConfigView.Rows[0].Cells[2];
	}

	public bool JudgeIsZoneValid(int zID)
	{
		for (int i = 0; i < DataApp.Zone_Max_Num; i++)
		{
			if (tdata.dataZoneInfor[i].ChnNum > 0 && zID == i)
			{
				return true;
			}
		}
		return false;
	}

	public static Frm_ZoneConfig getInstance(FormMain father, DataApp tdata)
	{
		Frm_ZoneConfig form = new Frm_ZoneConfig(tdata);
		form.MdiParent = father;
		return form;
	}

	private bool JubgeIsSameName(string name, int pos)
	{
		bool cmp = false;
		for (int i = 0; i < DataApp.Zone_Max_Num; i++)
		{
			if (string.Compare(name, tdata.dataZoneInfor[i].ZoneName) == 0 && i != pos)
			{
				cmp = true;
				break;
			}
		}
		return cmp;
	}

	private void EditingTBName_TextChanged(object sender, EventArgs e)
	{
		string ZN = "";
		if (ZoneConfigView.CurrentCell.ColumnIndex == 0)
		{
			FormMain cfrm = (FormMain)base.ParentForm;
			int rol = ZoneConfigView.CurrentCell.RowIndex;
			ZoneConfigView.CurrentCell.Value = (sender as TextBox).Text;
			if (ZoneConfigView.Rows[rol].Cells[0].Value != null && !(ZoneConfigView.Rows[rol].Cells[0].Value.ToString() == "") && !JubgeIsSameName(ZoneConfigView.CurrentRow.Cells[0].Value.ToString(), rol))
			{
				ZN = DataApp.StrFormat(ZoneConfigView.CurrentRow.Cells[0].Value.ToString(), 14);
				tdata.dataZoneInfor[rol].ZoneName = ZN;
				ZoneConfigView.CurrentRow.Cells[0].Value = ZN;
				cfrm.SetNodeZoneConfigName(ZN, 2, rol + 1);
			}
		}
	}

	private void ZoneConfigView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
	{
		if (ZoneConfigView.CurrentCell != null)
		{
			int rol = ZoneConfigView.CurrentCell.RowIndex;
			int col = ZoneConfigView.CurrentCell.ColumnIndex;
			if (col != 2 && col == 0)
			{
				TextBox EditingTBName = e.Control as TextBox;
				EditingTBName.TextChanged -= EditingTBName_TextChanged;
				EditingTBName.TextChanged += EditingTBName_TextChanged;
			}
		}
	}

	private void ZoneConfigView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		if (ZoneConfigView.CurrentCell == null)
		{
			return;
		}
		int rol = ZoneConfigView.CurrentCell.RowIndex;
		int col = ZoneConfigView.CurrentCell.ColumnIndex;
		FormMain cfrm = (FormMain)base.ParentForm;
		if (col == 0)
		{
			if (ZoneConfigView.Rows[rol].Cells[0].Value == null)
			{
				tdata.dataZoneInfor[rol].ZoneName = "";
				cfrm.SetNodeZoneConfigName(tdata.dataZoneInfor[rol].ZoneName, 2, rol + 1);
			}
			else if (ZoneConfigView.Rows[rol].Cells[0].Value.ToString() == "")
			{
				tdata.dataZoneInfor[rol].ZoneName = "";
				cfrm.SetNodeZoneConfigName(tdata.dataZoneInfor[rol].ZoneName, 2, rol + 1);
			}
			else
			{
				ZoneConfigView.CurrentRow.Cells[0].Value = tdata.dataZoneInfor[rol].ZoneName;
			}
		}
	}

	private void Frm_Zone_Activated(object sender, EventArgs e)
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPS_5RH.Frm_ZoneConfig));
		this.ZoneConfigView = new System.Windows.Forms.DataGridView();
		this.Col_ZName = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col_ValidCnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col_MaxCnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
		((System.ComponentModel.ISupportInitialize)this.ZoneConfigView).BeginInit();
		base.SuspendLayout();
		this.ZoneConfigView.AllowUserToAddRows = false;
		this.ZoneConfigView.AllowUserToDeleteRows = false;
		this.ZoneConfigView.AllowUserToResizeColumns = false;
		this.ZoneConfigView.AllowUserToResizeRows = false;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.ZoneConfigView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.ZoneConfigView.ColumnHeadersHeight = 24;
		this.ZoneConfigView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.ZoneConfigView.Columns.AddRange(this.Col_ZName, this.Col_ValidCnt, this.Col_MaxCnt);
		this.ZoneConfigView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.ZoneConfigView.Location = new System.Drawing.Point(22, 22);
		this.ZoneConfigView.MultiSelect = false;
		this.ZoneConfigView.Name = "ZoneConfigView";
		this.ZoneConfigView.RowHeadersWidth = 60;
		this.ZoneConfigView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.ZoneConfigView.RowsDefaultCellStyle = dataGridViewCellStyle5;
		this.ZoneConfigView.RowTemplate.Height = 24;
		this.ZoneConfigView.Size = new System.Drawing.Size(458, 301);
		this.ZoneConfigView.TabIndex = 11;
		this.ZoneConfigView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(ZoneConfigView_CellEndEdit);
		this.ZoneConfigView.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(ZoneConfigView_EditingControlShowing);
		this.Col_ZName.DataPropertyName = "col1";
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Col_ZName.DefaultCellStyle = dataGridViewCellStyle6;
		this.Col_ZName.FillWeight = 165.1779f;
		this.Col_ZName.HeaderText = "区域名称";
		this.Col_ZName.MaxInputLength = 14;
		this.Col_ZName.Name = "Col_ZName";
		this.Col_ZName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_ZName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_ZName.Width = 187;
		this.Col_ValidCnt.DataPropertyName = "col3";
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Col_ValidCnt.DefaultCellStyle = dataGridViewCellStyle7;
		this.Col_ValidCnt.FillWeight = 76.48254f;
		this.Col_ValidCnt.HeaderText = "有效信道数";
		this.Col_ValidCnt.MaxInputLength = 8;
		this.Col_ValidCnt.Name = "Col_ValidCnt";
		this.Col_ValidCnt.ReadOnly = true;
		this.Col_ValidCnt.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_ValidCnt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_ValidCnt.Width = 101;
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Col_MaxCnt.DefaultCellStyle = dataGridViewCellStyle8;
		this.Col_MaxCnt.FillWeight = 82.19741f;
		this.Col_MaxCnt.HeaderText = "最大信道数";
		this.Col_MaxCnt.Name = "Col_MaxCnt";
		this.Col_MaxCnt.ReadOnly = true;
		this.Col_MaxCnt.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_MaxCnt.Width = 108;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(506, 350);
		base.Controls.Add(this.ZoneConfigView);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Frm_ZoneConfig";
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = "Frm_Zone";
		base.Load += new System.EventHandler(Frm_Zone_Load);
		((System.ComponentModel.ISupportInitialize)this.ZoneConfigView).EndInit();
		base.ResumeLayout(false);
	}
}
