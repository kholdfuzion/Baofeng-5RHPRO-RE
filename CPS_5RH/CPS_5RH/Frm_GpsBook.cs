using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using CPS_5RH.Data;

namespace CPS_5RH;

public class Frm_GpsBook : Form
{
	private DataApp tdata = null;

	private DataGridViewTextBoxEditingControl dgvTxt = null;

	private DataGridViewTextBoxEditingControl dgvName = null;

	private IContainer components = null;

	private DataGridView dgv_GpsBook;

	private DataGridViewTextBoxColumn Col_No1;

	private DataGridViewTextBoxColumn Col_Num1;

	private DataGridViewTextBoxColumn Col_PbName;

	private DataGridViewTextBoxColumn Col_No2;

	private DataGridViewTextBoxColumn Col_Num2;

	private DataGridViewTextBoxColumn Col_Name2;

	private GroupBox grp_Title;

	public Frm_GpsBook(DataApp data)
	{
		InitializeComponent();
		tdata = data;
	}

	public void GpsBookDataDisp()
	{
		int i = 0;
		int total = 0;
		string str = string.Empty;
		try
		{
			for (i = 0; i < DatGpsBook.MAX_GpsBook_Num; i++)
			{
				if (tdata.dataGpsBook[i].UseFlg == 1 && tdata.dataGpsBook[i].CodeID != 0)
				{
					int idx = i / 2;
					if (i % 2 == 0)
					{
						dgv_GpsBook.Rows[idx].Cells[1].Value = tdata.dataGpsBook[i].CodeID;
						dgv_GpsBook.Rows[idx].Cells[2].Value = tdata.dataGpsBook[i].CodeName;
					}
					else
					{
						dgv_GpsBook.Rows[idx].Cells[4].Value = tdata.dataGpsBook[i].CodeID;
						dgv_GpsBook.Rows[idx].Cells[5].Value = tdata.dataGpsBook[i].CodeName;
					}
				}
			}
		}
		catch
		{
		}
	}

	private void tBox_Name_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = false;
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

	private void PhoneView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
	{
		if (dgv_GpsBook.CurrentCell != null)
		{
			int col = dgv_GpsBook.CurrentCell.ColumnIndex;
			if (col == 1 || col == 4)
			{
				dgvTxt = (DataGridViewTextBoxEditingControl)e.Control;
				dgvTxt.SelectAll();
				dgvTxt.KeyPress -= Cells_KeyPress;
				dgvTxt.KeyPress += Cells_KeyPress;
				dgvTxt.LostFocus -= EditingTB_LostFocus;
				dgvTxt.LostFocus += EditingTB_LostFocus;
			}
			else if (col == 2 || col == 5)
			{
				dgvName = (DataGridViewTextBoxEditingControl)e.Control;
				dgvName.SelectAll();
				dgvName.KeyPress -= tBox_Name_KeyPress;
				dgvName.KeyPress += tBox_Name_KeyPress;
				dgvName.TextChanged -= EditingTBName_TextChanged;
				dgvName.TextChanged += EditingTBName_TextChanged;
			}
		}
	}

	private int GetOnlyOneID(int newNum)
	{
		bool mark = false;
		int i;
		for (i = 0; i < DatGpsBook.MAX_GpsBook_Num; i++)
		{
			if (tdata.dataGpsBook[i].UseFlg == 1 && newNum == tdata.dataGpsBook[i].CodeID)
			{
				mark = true;
				break;
			}
		}
		if (mark)
		{
			for (i = 1; i <= 255; i++)
			{
				int j;
				for (j = 0; j < DatGpsBook.MAX_GpsBook_Num && (tdata.dataGpsBook[j].UseFlg != 1 || i != tdata.dataGpsBook[j].CodeID); j++)
				{
				}
				if (j == DatGpsBook.MAX_GpsBook_Num)
				{
					return i;
				}
			}
		}
		else
		{
			i = newNum;
		}
		return i;
	}

	private void UpDate_SettingData(int gpsid)
	{
		if (tdata.dataGenSetInfor.GpsID == gpsid)
		{
			tdata.dataGenSetInfor.GpsID = 0;
		}
	}

	private void EditingTB_LostFocus(object sender, EventArgs e)
	{
		int rol = dgv_GpsBook.CurrentCell.RowIndex;
		int col = dgv_GpsBook.CurrentCell.ColumnIndex;
		string val = (sender as TextBox).Text;
		int num = 0;
		int lastnum = 0;
		if (col != 1 && col != 4)
		{
			return;
		}
		int idx = ((col != 1) ? (rol * 2 + 1) : (rol * 2));
		try
		{
			if (val == "")
			{
				if (tdata.dataGpsBook[idx].UseFlg == 1)
				{
					if (DatGpsBook.GpsBook_Total <= 1)
					{
						(sender as TextBox).Text = tdata.dataGpsBook[idx].CodeID.ToString();
						if (col != 1)
						{
						}
						return;
					}
					DatGpsBook.GpsBook_Total--;
					UpDate_SettingData(idx + 1);
					tdata.dataGpsBook[idx].UseFlg = 0;
					tdata.dataGpsBook[idx].CodeID = 0;
					tdata.dataGpsBook[idx].CodeName = "";
				}
				if (col == 1)
				{
					dgv_GpsBook.Rows[rol].Cells[2].Value = "";
				}
				else
				{
					dgv_GpsBook.Rows[rol].Cells[5].Value = "";
				}
				return;
			}
			num = Convert.ToInt32(val);
			lastnum = num;
			if (num > 255)
			{
				num = 255;
			}
			if (num == 0)
			{
				num = 1;
			}
			num = GetOnlyOneID(num);
			if (tdata.dataGpsBook[idx].UseFlg == 1)
			{
				if (lastnum == tdata.dataGpsBook[idx].CodeID)
				{
					return;
				}
				tdata.dataGpsBook[idx].CodeID = (byte)num;
			}
			else
			{
				DatGpsBook.GpsBook_Total++;
				tdata.dataGpsBook[idx].UseFlg = 1;
				tdata.dataGpsBook[idx].CodeID = (byte)num;
				val = "Contact " + (idx + 1);
				tdata.dataGpsBook[idx].CodeName = val;
				if (col == 1)
				{
					dgv_GpsBook.Rows[rol].Cells[2].Value = val;
				}
				else
				{
					dgv_GpsBook.Rows[rol].Cells[5].Value = val;
				}
			}
			(sender as TextBox).Text = num.ToString();
			dgv_GpsBook.CurrentCell.Value = num.ToString();
		}
		catch
		{
		}
	}

	private void dgv_GpsBook_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgv_GpsBook.CurrentCell == null)
		{
			return;
		}
		int indexCol = e.ColumnIndex;
		int indexRow = e.RowIndex;
		if (indexRow != -1 && indexCol != -1)
		{
			if (indexCol == 2 && dgv_GpsBook.Rows[indexRow].Cells[1].Value == null)
			{
				dgv_GpsBook.CurrentCell = dgv_GpsBook.Rows[indexRow].Cells[1];
			}
			else if (indexCol == 5 && dgv_GpsBook.Rows[indexRow].Cells[4].Value == null)
			{
				dgv_GpsBook.CurrentCell = dgv_GpsBook.Rows[indexRow].Cells[4];
			}
		}
	}

	private void EditingTBName_TextChanged(object sender, EventArgs e)
	{
		int rol = dgv_GpsBook.CurrentCell.RowIndex;
		int col = dgv_GpsBook.CurrentCell.ColumnIndex;
		string val = (sender as TextBox).Text;
		if (col != 2 && col != 5)
		{
			return;
		}
		int idx = ((col != 2) ? (rol * 2 + 1) : (rol * 2));
		if (val == "")
		{
			tdata.dataGpsBook[idx].CodeName = "";
		}
		else if (tdata.dataGpsBook[idx].UseFlg == 1)
		{
			string tmp = ((sender as TextBox).Text = DataApp.StrFormat(val, 14));
			tdata.dataGpsBook[idx].CodeName = tmp;
			if (DataApp.GetLength(tmp) >= 14)
			{
				dgvName.SelectionStart = tmp.Length;
			}
		}
		else
		{
			(sender as TextBox).Text = "";
		}
	}

	private void Frm_Pbook_FormClosing(object sender, FormClosingEventArgs e)
	{
		switch (e.CloseReason)
		{
		case CloseReason.UserClosing:
			if (base.Visible)
			{
				e.Cancel = true;
				Hide();
			}
			break;
		case CloseReason.MdiFormClosing:
			break;
		}
	}

	public static Frm_GpsBook getInstance(FormMain father, DataApp data)
	{
		Frm_GpsBook form = new Frm_GpsBook(data);
		form.MdiParent = father;
		return form;
	}

	private void Frm_Pbook_Load(object sender, EventArgs e)
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
		ComponentResourceManager crm = new ComponentResourceManager(typeof(Frm_GpsBook));
		foreach (DataGridViewColumn col in dgv_GpsBook.Columns)
		{
			crm.ApplyResources(col, col.Name);
		}
		LanguageSel.LoadLanguage(this, typeof(Frm_GpsBook));
		dgv_GpsBook.Rows.Add(40);
		for (int i = 0; i < 40; i++)
		{
			dgv_GpsBook.Rows[i].Cells[0].Value = i * 2 + 1;
			dgv_GpsBook.Rows[i].Cells[3].Value = i * 2 + 2;
		}
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPS_5RH.Frm_GpsBook));
		this.dgv_GpsBook = new System.Windows.Forms.DataGridView();
		this.Col_No1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col_Num1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col_PbName = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col_No2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col_Num2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col_Name2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.grp_Title = new System.Windows.Forms.GroupBox();
		((System.ComponentModel.ISupportInitialize)this.dgv_GpsBook).BeginInit();
		this.grp_Title.SuspendLayout();
		base.SuspendLayout();
		this.dgv_GpsBook.AllowUserToAddRows = false;
		this.dgv_GpsBook.AllowUserToDeleteRows = false;
		this.dgv_GpsBook.AllowUserToResizeColumns = false;
		this.dgv_GpsBook.AllowUserToResizeRows = false;
		this.dgv_GpsBook.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgv_GpsBook.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightBlue;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgv_GpsBook.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgv_GpsBook.ColumnHeadersHeight = 30;
		this.dgv_GpsBook.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgv_GpsBook.Columns.AddRange(this.Col_No1, this.Col_Num1, this.Col_PbName, this.Col_No2, this.Col_Num2, this.Col_Name2);
		this.dgv_GpsBook.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dgv_GpsBook.Location = new System.Drawing.Point(41, 20);
		this.dgv_GpsBook.MultiSelect = false;
		this.dgv_GpsBook.Name = "dgv_GpsBook";
		this.dgv_GpsBook.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle8.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgv_GpsBook.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
		this.dgv_GpsBook.RowHeadersVisible = false;
		this.dgv_GpsBook.RowHeadersWidth = 50;
		this.dgv_GpsBook.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgv_GpsBook.RowsDefaultCellStyle = dataGridViewCellStyle9;
		this.dgv_GpsBook.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.dgv_GpsBook.RowTemplate.Height = 30;
		this.dgv_GpsBook.Size = new System.Drawing.Size(520, 400);
		this.dgv_GpsBook.TabIndex = 10;
		this.dgv_GpsBook.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgv_GpsBook_CellClick);
		this.dgv_GpsBook.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(PhoneView_EditingControlShowing);
		this.Col_No1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.ButtonFace;
		this.Col_No1.DefaultCellStyle = dataGridViewCellStyle10;
		this.Col_No1.FillWeight = 155.3119f;
		this.Col_No1.HeaderText = "序号";
		this.Col_No1.MinimumWidth = 60;
		this.Col_No1.Name = "Col_No1";
		this.Col_No1.ReadOnly = true;
		this.Col_No1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_No1.Width = 60;
		this.Col_Num1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Col_Num1.DefaultCellStyle = dataGridViewCellStyle11;
		this.Col_Num1.FillWeight = 8.898569f;
		this.Col_Num1.HeaderText = "ID";
		this.Col_Num1.MaxInputLength = 3;
		this.Col_Num1.MinimumWidth = 60;
		this.Col_Num1.Name = "Col_Num1";
		this.Col_Num1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.Col_Num1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_Num1.Width = 60;
		this.Col_PbName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		this.Col_PbName.DataPropertyName = "col1";
		dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Col_PbName.DefaultCellStyle = dataGridViewCellStyle12;
		this.Col_PbName.FillWeight = 192.4995f;
		this.Col_PbName.HeaderText = "名称";
		this.Col_PbName.MaxInputLength = 14;
		this.Col_PbName.MinimumWidth = 130;
		this.Col_PbName.Name = "Col_PbName";
		this.Col_PbName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_PbName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_PbName.Width = 130;
		this.Col_No2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.ButtonFace;
		this.Col_No2.DefaultCellStyle = dataGridViewCellStyle13;
		this.Col_No2.FillWeight = 8.94606f;
		this.Col_No2.HeaderText = "序号";
		this.Col_No2.MinimumWidth = 60;
		this.Col_No2.Name = "Col_No2";
		this.Col_No2.ReadOnly = true;
		this.Col_No2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_No2.Width = 60;
		dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Col_Num2.DefaultCellStyle = dataGridViewCellStyle14;
		this.Col_Num2.FillWeight = 10.83308f;
		this.Col_Num2.HeaderText = "ID";
		this.Col_Num2.MaxInputLength = 3;
		this.Col_Num2.MinimumWidth = 60;
		this.Col_Num2.Name = "Col_Num2";
		this.Col_Num2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.Col_Num2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_Name2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Col_Name2.DefaultCellStyle = dataGridViewCellStyle15;
		this.Col_Name2.FillWeight = 235.4398f;
		this.Col_Name2.HeaderText = "名称";
		this.Col_Name2.MaxInputLength = 14;
		this.Col_Name2.MinimumWidth = 130;
		this.Col_Name2.Name = "Col_Name2";
		this.Col_Name2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.grp_Title.Controls.Add(this.dgv_GpsBook);
		this.grp_Title.Location = new System.Drawing.Point(25, 21);
		this.grp_Title.Name = "grp_Title";
		this.grp_Title.Size = new System.Drawing.Size(592, 433);
		this.grp_Title.TabIndex = 11;
		this.grp_Title.TabStop = false;
		this.grp_Title.Text = "常用联系人";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(694, 481);
		base.Controls.Add(this.grp_Title);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Frm_GpsBook";
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		base.Load += new System.EventHandler(Frm_Pbook_Load);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Frm_Pbook_FormClosing);
		((System.ComponentModel.ISupportInitialize)this.dgv_GpsBook).EndInit();
		this.grp_Title.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
