using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Radio;

public class SkipFrequencyControl : UserControl, IViewData
{
    private IContainer components;

    private DataGridView dgvFreq;

    private TextBox txtFreq;

    private TextBox txtOther;

    private Button btnExport;

    private Button btnImport;

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
        this.dgvFreq = new System.Windows.Forms.DataGridView();
        this.txtFreq = new System.Windows.Forms.TextBox();
        this.txtOther = new System.Windows.Forms.TextBox();
        this.btnExport = new System.Windows.Forms.Button();
        this.btnImport = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)this.dgvFreq).BeginInit();
        base.SuspendLayout();
        this.dgvFreq.AllowUserToAddRows = false;
        this.dgvFreq.AllowUserToDeleteRows = false;
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle1.Font = new System.Drawing.Font("Font style", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dgvFreq.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        this.dgvFreq.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvFreq.Location = new System.Drawing.Point(0, 20);
        this.dgvFreq.Name = "dgvFreq";
        this.dgvFreq.ReadOnly = true;
        this.dgvFreq.RowHeadersWidth = 50;
        this.dgvFreq.RowTemplate.Height = 23;
        this.dgvFreq.Size = new System.Drawing.Size(1190, 400);
        this.dgvFreq.TabIndex = 2;
        this.dgvFreq.Scroll += new System.Windows.Forms.ScrollEventHandler(dgvFreq_Scroll);
        this.dgvFreq.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvFreq_CellClick);
        this.dgvFreq.CurrentCellChanged += new System.EventHandler(dgvFreq_CurrentCellChanged);
        this.txtFreq.Location = new System.Drawing.Point(0, 0);
        this.txtFreq.Name = "txtFreq";
        this.txtFreq.Size = new System.Drawing.Size(100, 21);
        this.txtFreq.TabIndex = 1;
        this.txtFreq.Visible = false;
        this.txtFreq.Leave += new System.EventHandler(txtFreq_Leave);
        this.txtFreq.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtFreq_KeyPress);
        this.txtOther.Location = new System.Drawing.Point(335, 483);
        this.txtOther.Name = "txtOther";
        this.txtOther.Size = new System.Drawing.Size(100, 21);
        this.txtOther.TabIndex = 0;
        this.btnExport.Location = new System.Drawing.Point(505, 440);
        this.btnExport.Name = "btnExport";
        this.btnExport.Size = new System.Drawing.Size(75, 25);
        this.btnExport.TabIndex = 3;
        this.btnExport.Text = "Export";
        this.btnExport.UseVisualStyleBackColor = true;
        this.btnExport.Click += new System.EventHandler(btnExport_Click);
        this.btnImport.Location = new System.Drawing.Point(610, 440);
        this.btnImport.Name = "btnImport";
        this.btnImport.Size = new System.Drawing.Size(75, 25);
        this.btnImport.TabIndex = 4;
        this.btnImport.Text = "Import";
        this.btnImport.UseVisualStyleBackColor = true;
        this.btnImport.Click += new System.EventHandler(btnImport_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.Controls.Add(this.btnImport);
        base.Controls.Add(this.btnExport);
        base.Controls.Add(this.txtOther);
        base.Controls.Add(this.txtFreq);
        base.Controls.Add(this.dgvFreq);
        base.Name = "SkipFrequencyControl";
        base.Size = new System.Drawing.Size(1190, 480);
        base.Load += new System.EventHandler(SkipFrequencyControl_Load);
        ((System.ComponentModel.ISupportInitialize)this.dgvFreq).EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }

    public SkipFrequencyControl()
    {
        InitializeComponent();
    }

    private void SkipFrequencyControl_Load(object sender, EventArgs e)
    {
    }

    public void InitDgv()
    {
        dgvFreq.RowCount = 41;
        dgvFreq.ColumnCount = 16;
        for (int i = 1; i <= 41; i++)
        {
            dgvFreq.Rows[i - 1].HeaderCell.Value = i.ToString();
        }
        for (int j = 1; j <= 16; j++)
        {
            dgvFreq.Columns[j - 1].HeaderText = $"{Lang.SZ_GROUP} {j}";
            dgvFreq.Columns[j - 1].Width = 70;
        }
        for (int k = 0; k < dgvFreq.ColumnCount; k++)
        {
            dgvFreq.Columns[k].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
    }

    public void InitView()
    {
        InitDgv();
        txtFreq.MaxLength = 9;
    }

    public void DataToView()
    {
        InitView();
        dgvFreq.CurrentCell = null;
        for (int row = 0; row < dgvFreq.RowCount; row++)
        {
            for (int col = 0; col < dgvFreq.ColumnCount; col++)
            {
                dgvFreq[col, row].Value = Cps.GetInstance().SkipFrequencyModel.Frequencys[row, col];
            }
        }
    }

    public void ViewToData()
    {
        dgvFreq.Focus();
        for (int row = 0; row < dgvFreq.RowCount; row++)
        {
            for (int col = 0; col < dgvFreq.ColumnCount; col++)
            {
                if (dgvFreq[col, row].Value != null)
                {
                    Cps.GetInstance().SkipFrequencyModel.Frequencys[row, col] = dgvFreq[col, row].Value.ToString();
                }
                else
                {
                    Cps.GetInstance().SkipFrequencyModel.Frequencys[row, col] = "";
                }
            }
        }
        dgvFreq.CurrentCell = null;
    }

    public void LoadLanguageText(string section)
    {
        foreach (Control ctrl in base.Controls)
        {
            LangHelper.ApplyControl(section, ctrl);
        }
    }

    private void dgvFreq_CurrentCellChanged(object sender, EventArgs e)
    {
    }

    private void dgvFreq_Scroll(object sender, ScrollEventArgs e)
    {
        dgvFreq.Focus();
        dgvFreq.CurrentCell = null;
    }

    private void txtFreq_Leave(object sender, EventArgs e)
    {
        string freq = txtFreq.Text;
        if (!string.IsNullOrEmpty(freq))
        {
            if (FrequencyHelper.FreqIsValid(freq))
            {
                double freqDouble = 0.0;
                double.TryParse(freq, out freqDouble);
                int freqInt = (int)Common.MulDecimal(freqDouble, 100000.0);
                FrequencyHelper.AdjustFreq(ref freqInt, 500, 625);
                freqDouble = Common.DivDecimal(freqInt, 100000);
                txtFreq.Text = $"{freqDouble:f5}";
            }
            else
            {
                double freqDouble2 = Common.DivDecimal(Cps.GetInstance().ModelModel.MinFreq, 100000);
                txtFreq.Text = $"{freqDouble2:f5}";
            }
        }
        if (dgvFreq.CurrentCell != null)
        {
            dgvFreq.CurrentCell.Value = txtFreq.Text;
        }
        txtFreq.Visible = false;
    }

    private void txtFreq_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == '\r')
        {
            dgvFreq.Focus();
        }
        else if ("0123456789.".IndexOf(e.KeyChar) < 0 && e.KeyChar != '\b')
        {
            e.Handled = true;
        }
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
        SaveFileDialog sfd = new SaveFileDialog();
        sfd.Filter = "Text (*.txt)|*.txt";
        sfd.InitialDirectory = Application.StartupPath;
        DialogResult result = sfd.ShowDialog();
        if (result == DialogResult.OK)
        {
            ViewToData();
            string[] freq = Cps.GetInstance().SkipFrequencyModel.Export();
            string content = string.Join(",", freq);
            File.WriteAllText(sfd.FileName, content);
        }
    }

    private void btnImport_Click(object sender, EventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.Filter = "Text (*.txt)|*.txt";
        ofd.InitialDirectory = Application.StartupPath;
        DialogResult result = ofd.ShowDialog();
        if (result == DialogResult.OK)
        {
            string text = File.ReadAllText(ofd.FileName);
            string[] freq = text.Split(',');
            Cps.GetInstance().SkipFrequencyModel.Import(freq);
            DataToView();
        }
    }

    private void dgvFreq_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0)
        {
            return;
        }
        if (dgvFreq.CurrentCell == null)
        {
            txtFreq.Visible = false;
            return;
        }
        int rowIndex = dgvFreq.CurrentCell.RowIndex;
        int columnIndex = dgvFreq.CurrentCell.ColumnIndex;
        Rectangle cellRect = dgvFreq.GetCellDisplayRectangle(columnIndex, rowIndex, cutOverflow: false);
        cellRect.Offset(dgvFreq.Location);
        txtFreq.Left = cellRect.Left;
        txtFreq.Top = cellRect.Top;
        txtFreq.Width = cellRect.Width;
        txtFreq.Height = cellRect.Height;
        if (dgvFreq.CurrentCell.Value == null)
        {
            txtFreq.Text = "";
        }
        else
        {
            txtFreq.Text = dgvFreq.CurrentCell.Value.ToString();
        }
        txtFreq.Visible = true;
        txtFreq.BringToFront();
        txtFreq.Focus();
        txtFreq.SelectAll();
    }
}
