using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Radio;

public class DtmfContactControl : UserControl, IViewData
{
    private IContainer components;

    private DataGridView dgvContact;

    private DataGridViewTextBoxColumn Column1;

    private TextBox txtCode;

    public DtmfContactControl()
    {
        InitializeComponent();
        InitView();
    }

    private void DtmfContactControl_Load(object sender, EventArgs e)
    {
    }

    private void InitDgv()
    {
        dgvContact.RowCount = 16;
        for (int i = 1; i < 17; i++)
        {
            dgvContact.Rows[i - 1].HeaderCell.Value = i.ToString();
        }
        for (int j = 0; j < dgvContact.ColumnCount; j++)
        {
            dgvContact.Columns[j].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
    }

    public void InitView()
    {
        txtCode.MaxLength = 16;
        InitDgv();
    }

    public void DataToView()
    {
        dgvContact.CurrentCell = null;
        for (int i = 0; i < dgvContact.RowCount; i++)
        {
            dgvContact[0, i].Value = Cps.GetInstance().DtmfContactModel.Contacts[i];
        }
    }

    public void ViewToData()
    {
        dgvContact.Focus();
        for (int i = 0; i < dgvContact.RowCount; i++)
        {
            if (dgvContact[0, i].Value != null)
            {
                Cps.GetInstance().DtmfContactModel.Contacts[i] = dgvContact[0, i].Value.ToString();
            }
            else
            {
                Cps.GetInstance().DtmfContactModel.Contacts[i] = "";
            }
        }
    }

    public void LoadLanguageText(string section)
    {
        foreach (Control ctrl in base.Controls)
        {
            LangHelper.ApplyControl(section, ctrl);
        }
    }

    private void dgvContact_CurrentCellChanged(object sender, EventArgs e)
    {
    }

    private void dgvContact_Scroll(object sender, ScrollEventArgs e)
    {
        dgvContact.Focus();
        dgvContact.CurrentCell = null;
    }

    private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
    {
        if ("0123456789ABCD*#".IndexOf(char.ToUpper(e.KeyChar)) < 0 && e.KeyChar != '\b')
        {
            e.Handled = true;
        }
    }

    private void txtCode_Leave(object sender, EventArgs e)
    {
        if (dgvContact.CurrentCell != null)
        {
            dgvContact.CurrentCell.Value = txtCode.Text;
        }
        txtCode.Visible = false;
    }

    private void dgvContact_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }
        if (dgvContact.CurrentCell == null)
        {
            txtCode.Visible = false;
            return;
        }
        int rowIndex = dgvContact.CurrentCell.RowIndex;
        int columnIndex = dgvContact.CurrentCell.ColumnIndex;
        Rectangle cellRect = dgvContact.GetCellDisplayRectangle(columnIndex, rowIndex, cutOverflow: false);
        cellRect.Offset(dgvContact.Location);
        txtCode.Left = cellRect.Left;
        txtCode.Top = cellRect.Top;
        txtCode.Width = cellRect.Width;
        txtCode.Height = cellRect.Height;
        if (dgvContact.CurrentCell.Value == null)
        {
            txtCode.Text = "";
        }
        else
        {
            txtCode.Text = dgvContact.CurrentCell.Value.ToString();
        }
        txtCode.Visible = true;
        txtCode.BringToFront();
        txtCode.Focus();
        txtCode.SelectAll();
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
        this.dgvContact = new System.Windows.Forms.DataGridView();
        this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.txtCode = new System.Windows.Forms.TextBox();
        ((System.ComponentModel.ISupportInitialize)this.dgvContact).BeginInit();
        base.SuspendLayout();
        this.dgvContact.AllowUserToAddRows = false;
        this.dgvContact.AllowUserToDeleteRows = false;
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle1.Font = new System.Drawing.Font("Font style", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dgvContact.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        this.dgvContact.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvContact.Columns.AddRange(this.Column1);
        this.dgvContact.Location = new System.Drawing.Point(20, 20);
        this.dgvContact.Name = "dgvContact";
        this.dgvContact.ReadOnly = true;
        this.dgvContact.RowHeadersWidth = 50;
        this.dgvContact.RowTemplate.Height = 23;
        this.dgvContact.Size = new System.Drawing.Size(260, 400);
        this.dgvContact.TabIndex = 0;
        this.dgvContact.Scroll += new System.Windows.Forms.ScrollEventHandler(dgvContact_Scroll);
        this.dgvContact.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvContact_CellClick);
        this.dgvContact.CurrentCellChanged += new System.EventHandler(dgvContact_CurrentCellChanged);
        dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
        this.Column1.HeaderText = "Code words";
        this.Column1.Name = "Column1";
        this.Column1.ReadOnly = true;
        this.Column1.Width = 200;
        this.txtCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
        this.txtCode.Location = new System.Drawing.Point(0, 0);
        this.txtCode.Name = "txtCode";
        this.txtCode.Size = new System.Drawing.Size(100, 21);
        this.txtCode.TabIndex = 1;
        this.txtCode.Visible = false;
        this.txtCode.Leave += new System.EventHandler(txtCode_Leave);
        this.txtCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCode_KeyPress);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.Controls.Add(this.txtCode);
        base.Controls.Add(this.dgvContact);
        base.Name = "DtmfContactControl";
        base.Size = new System.Drawing.Size(300, 440);
        base.Load += new System.EventHandler(DtmfContactControl_Load);
        ((System.ComponentModel.ISupportInitialize)this.dgvContact).EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
