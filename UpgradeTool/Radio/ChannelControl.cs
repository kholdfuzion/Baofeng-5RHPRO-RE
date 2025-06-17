using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Radio;

public class ChannelControl : UserControl, IViewData
{
    private IContainer components;

    private DataGridView dgvFreq;

    private TextBox txtRxFreq;

    private TextBox txtTxFreq;

    private TextBox txtName;

    private ComboBox cmbTone;

    private ComboBox cmbOther;

    private TextBox txtOther;

    private DataGridViewTextBoxColumn colRxFreq;

    private DataGridViewTextBoxColumn colTxFreq;

    private DataGridViewTextBoxColumn colRxTone;

    private DataGridViewTextBoxColumn colTxTone;

    private DataGridViewTextBoxColumn colPower;

    private DataGridViewTextBoxColumn colBandwidth;

    private DataGridViewTextBoxColumn colScan;

    private DataGridViewTextBoxColumn colSqlMode;

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
        this.txtRxFreq = new System.Windows.Forms.TextBox();
        this.txtTxFreq = new System.Windows.Forms.TextBox();
        this.txtName = new System.Windows.Forms.TextBox();
        this.cmbTone = new System.Windows.Forms.ComboBox();
        this.cmbOther = new System.Windows.Forms.ComboBox();
        this.txtOther = new System.Windows.Forms.TextBox();
        this.colRxFreq = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colTxFreq = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colRxTone = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colTxTone = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colPower = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colBandwidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colScan = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.colSqlMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
        ((System.ComponentModel.ISupportInitialize)this.dgvFreq).BeginInit();
        base.SuspendLayout();
        this.dgvFreq.AllowUserToAddRows = false;
        this.dgvFreq.AllowUserToDeleteRows = false;
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
        dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
        dataGridViewCellStyle1.Font = new System.Drawing.Font("Font style", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
        dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(128, 128, 255);
        dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
        dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
        this.dgvFreq.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        this.dgvFreq.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvFreq.Columns.AddRange(this.colRxFreq, this.colTxFreq, this.colRxTone, this.colTxTone, this.colPower, this.colBandwidth, this.colScan, this.colSqlMode);
        this.dgvFreq.Location = new System.Drawing.Point(20, 20);
        this.dgvFreq.Name = "dgvFreq";
        this.dgvFreq.ReadOnly = true;
        this.dgvFreq.RowHeadersWidth = 50;
        this.dgvFreq.RowTemplate.Height = 23;
        this.dgvFreq.Size = new System.Drawing.Size(682, 500);
        this.dgvFreq.TabIndex = 6;
        this.dgvFreq.Scroll += new System.Windows.Forms.ScrollEventHandler(dgvFreq_Scroll);
        this.dgvFreq.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvFreq_CellClick);
        this.dgvFreq.CurrentCellChanged += new System.EventHandler(dgvFreq_CurrentCellChanged);
        this.txtRxFreq.Location = new System.Drawing.Point(0, 0);
        this.txtRxFreq.Name = "txtRxFreq";
        this.txtRxFreq.Size = new System.Drawing.Size(64, 21);
        this.txtRxFreq.TabIndex = 1;
        this.txtRxFreq.Visible = false;
        this.txtRxFreq.Leave += new System.EventHandler(txtRxFreq_Leave);
        this.txtRxFreq.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtRxFreq_KeyPress);
        this.txtTxFreq.Location = new System.Drawing.Point(94, 0);
        this.txtTxFreq.Name = "txtTxFreq";
        this.txtTxFreq.Size = new System.Drawing.Size(64, 21);
        this.txtTxFreq.TabIndex = 2;
        this.txtTxFreq.Visible = false;
        this.txtTxFreq.Leave += new System.EventHandler(txtTxFreq_Leave);
        this.txtTxFreq.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtTxFreq_KeyPress);
        this.txtName.Location = new System.Drawing.Point(176, 0);
        this.txtName.Name = "txtName";
        this.txtName.Size = new System.Drawing.Size(72, 21);
        this.txtName.TabIndex = 3;
        this.txtName.Visible = false;
        this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(txtName_KeyDown);
        this.txtName.Leave += new System.EventHandler(txtName_Leave);
        this.txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtName_KeyPress);
        this.cmbTone.FormattingEnabled = true;
        this.cmbTone.Location = new System.Drawing.Point(278, 0);
        this.cmbTone.Name = "cmbTone";
        this.cmbTone.Size = new System.Drawing.Size(89, 20);
        this.cmbTone.TabIndex = 4;
        this.cmbTone.Visible = false;
        this.cmbTone.Leave += new System.EventHandler(cmbTone_Leave);
        this.cmbOther.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbOther.FormattingEnabled = true;
        this.cmbOther.Location = new System.Drawing.Point(397, 0);
        this.cmbOther.Name = "cmbOther";
        this.cmbOther.Size = new System.Drawing.Size(87, 20);
        this.cmbOther.TabIndex = 5;
        this.cmbOther.Visible = false;
        this.cmbOther.Leave += new System.EventHandler(cmbOther_Leave);
        this.txtOther.Location = new System.Drawing.Point(508, -24);
        this.txtOther.Name = "txtOther";
        this.txtOther.Size = new System.Drawing.Size(100, 21);
        this.txtOther.TabIndex = 0;
        this.colRxFreq.HeaderText = "RxFreq";
        this.colRxFreq.Name = "colRxFreq";
        this.colRxFreq.ReadOnly = true;
        this.colRxFreq.Width = 85;
        this.colTxFreq.HeaderText = "TxFreq";
        this.colTxFreq.Name = "colTxFreq";
        this.colTxFreq.ReadOnly = true;
        this.colTxFreq.Width = 85;
        this.colRxTone.HeaderText = "RxTone";
        this.colRxTone.Name = "colRxTone";
        this.colRxTone.ReadOnly = true;
        this.colRxTone.Width = 70;
        this.colTxTone.HeaderText = "TxTone";
        this.colTxTone.Name = "colTxTone";
        this.colTxTone.ReadOnly = true;
        this.colTxTone.Width = 70;
        this.colPower.HeaderText = "Power";
        this.colPower.Name = "colPower";
        this.colPower.ReadOnly = true;
        this.colPower.Width = 45;
        this.colBandwidth.HeaderText = "Bandwidth";
        this.colBandwidth.Name = "colBandwidth";
        this.colBandwidth.ReadOnly = true;
        this.colBandwidth.Width = 65;
        this.colScan.HeaderText = "Scan";
        this.colScan.Name = "colScan";
        this.colScan.ReadOnly = true;
        this.colScan.Width = 65;
        this.colSqlMode.HeaderText = "SqlMode";
        this.colSqlMode.Name = "colSqlMode";
        this.colSqlMode.ReadOnly = true;
        this.colSqlMode.Width = 125;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.Controls.Add(this.txtOther);
        base.Controls.Add(this.cmbOther);
        base.Controls.Add(this.cmbTone);
        base.Controls.Add(this.txtName);
        base.Controls.Add(this.txtTxFreq);
        base.Controls.Add(this.txtRxFreq);
        base.Controls.Add(this.dgvFreq);
        base.Name = "ChannelControl";
        base.Size = new System.Drawing.Size(1266, 535);
        base.Load += new System.EventHandler(ChannelControl_Load);
        ((System.ComponentModel.ISupportInitialize)this.dgvFreq).EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }

    public ChannelControl()
    {
        InitializeComponent();
        InitView();
        if (!base.DesignMode)
        {
            InitTone();
        }
    }

    private void ChannelControl_Load(object sender, EventArgs e)
    {
    }

    public void InitDgv()
    {
        dgvFreq.RowCount = 100;
        for (int i = 1; i <= 100; i++)
        {
            dgvFreq.Rows[i - 1].HeaderCell.Value = i.ToString();
        }
        int[] width = new int[8] { 85, 85, 70, 70, 45, 65, 65, 125 };
        for (int j = 0; j < 8; j++)
        {
            dgvFreq.Columns[j].Width = width[j];
        }
        for (int k = 0; k < dgvFreq.ColumnCount; k++)
        {
            dgvFreq.Columns[k].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
        DataGridViewCellStyle style = new DataGridViewCellStyle();
        style.ForeColor = Color.FromArgb(0, 0, 155);
        style.BackColor = Color.FromArgb(193, 255, 255);
        foreach (DataGridViewColumn col in dgvFreq.Columns)
        {
            col.HeaderCell.Style = style;
        }
        foreach (DataGridViewRow row in (IEnumerable)dgvFreq.Rows)
        {
            row.HeaderCell.Style = style;
        }
        dgvFreq.TopLeftHeaderCell.Style = style;
        dgvFreq.EnableHeadersVisualStyles = false;
    }

    private void InitTone()
    {
        string tone = "";
        cmbTone.Items.Clear();
        cmbTone.Items.Add(Lang.SZ_NONE);
        StreamReader fileTone = new StreamReader(Application.StartupPath + "\\Tone.txt", Encoding.Default);
        while ((tone = fileTone.ReadLine()) != null)
        {
            cmbTone.Items.Add(tone);
        }
        fileTone.Close();
    }

    public void InitView()
    {
        InitDgv();
        txtName.MaxLength = 0;
        txtRxFreq.MaxLength = 9;
        txtTxFreq.MaxLength = 9;
    }

    public void DataToView()
    {
        dgvFreq.CurrentCell = null;
        cmbTone.Items[0] = Lang.SZ_NONE;
        for (int row = 0; row < dgvFreq.RowCount; row++)
        {
            for (int col = 0; col < dgvFreq.ColumnCount; col++)
            {
                dgvFreq[col, row].Value = Cps.GetInstance().ChannelModel.Channels[row].Items[col];
            }
            ControlDgvBackground(row);
        }
        dgvFreq.ClearSelection();
    }

    public void ViewToData()
    {
        dgvFreq.Focus();
        for (int row = 0; row < dgvFreq.RowCount; row++)
        {
            for (int col = 0; col < dgvFreq.ColumnCount; col++)
            {
                if (dgvFreq[col, row].Value != null && !string.IsNullOrEmpty(dgvFreq[col, row].Value.ToString()))
                {
                    Cps.GetInstance().ChannelModel.Channels[row].Items[col] = dgvFreq[col, row].Value.ToString();
                }
                else
                {
                    Cps.GetInstance().ChannelModel.Channels[row].Items[col] = "";
                }
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

    private Control GetControlByColumn(int colIndex)
    {
        switch (colIndex)
        {
            case 0:
                return txtRxFreq;
            case 1:
                return txtTxFreq;
            case 2:
            case 3:
                return cmbTone;
            case 4:
                cmbOther.Init(Lang.SZ_POWER);
                break;
            case 5:
                cmbOther.Init(Lang.SZ_BANDWIDTH);
                break;
            case 6:
                cmbOther.Init(Lang.SZ_SCAN);
                break;
            case 7:
                cmbOther.Init(Lang.SZ_SQL_MODE);
                break;
        }
        return cmbOther;
    }

    private void dgvFreq_CurrentCellChanged(object sender, EventArgs e)
    {
        if (dgvFreq.CurrentCell == null)
        {
            txtRxFreq.Visible = false;
            txtTxFreq.Visible = false;
            cmbTone.Visible = false;
            cmbOther.Visible = false;
            txtName.Visible = false;
            return;
        }
        int rowIndex = dgvFreq.CurrentCell.RowIndex;
        int columnIndex = dgvFreq.CurrentCell.ColumnIndex;
        Rectangle cellRect = dgvFreq.GetCellDisplayRectangle(columnIndex, rowIndex, cutOverflow: false);
        if (dgvFreq[0, rowIndex].Value != null && (!string.IsNullOrEmpty(dgvFreq[0, rowIndex].Value.ToString()) || columnIndex == 0))
        {
            Control ctrl = GetControlByColumn(columnIndex);
            cellRect.Offset(dgvFreq.Location);
            ctrl.Left = cellRect.Left;
            ctrl.Top = cellRect.Top;
            ctrl.Width = cellRect.Width;
            ctrl.Height = cellRect.Height;
            if (dgvFreq.CurrentCell.Value == null)
            {
                ctrl.Text = "";
            }
            else
            {
                ctrl.Text = dgvFreq.CurrentCell.Value.ToString();
            }
            ctrl.Visible = true;
            ctrl.Focus();
            ctrl.BringToFront();
            if (ctrl is TextBox txt)
            {
                txt.SelectAll();
            }
        }
    }

    private void dgvFreq_Scroll(object sender, ScrollEventArgs e)
    {
        txtRxFreq.Visible = false;
        txtTxFreq.Visible = false;
        cmbTone.Visible = false;
        cmbOther.Visible = false;
        txtName.Visible = false;
        dgvFreq.CurrentCell = null;
    }

    private void txtName_Leave(object sender, EventArgs e)
    {
        if (dgvFreq.CurrentCell != null)
        {
            dgvFreq.CurrentCell.Value = txtName.Text;
        }
        txtName.Visible = false;
    }

    private void txtName_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != ' ')
        {
            e.Handled = true;
        }
    }

    private void txtRxFreq_Leave(object sender, EventArgs e)
    {
        string freq = txtRxFreq.Text;
        int rowIndex = dgvFreq.CurrentCell.RowIndex;
        _ = dgvFreq.CurrentCell.ColumnIndex;
        string rxFreq = "";
        rxFreq = ((dgvFreq[0, rowIndex].Value != null) ? dgvFreq[0, rowIndex].Value.ToString() : "");
        if (!string.IsNullOrEmpty(freq))
        {
            if (FrequencyHelper.FreqIsValid(freq))
            {
                double freqDouble = 0.0;
                double.TryParse(freq, out freqDouble);
                int freqInt = (int)Common.MulDecimal(freqDouble, 100000.0);
                FrequencyHelper.AdjustFreq(ref freqInt, 500, 625);
                freqDouble = Common.DivDecimal(freqInt, 100000);
                txtRxFreq.Text = $"{freqDouble:f5}";
            }
            else
            {
                double freqDouble2 = Common.DivDecimal(Cps.GetInstance().ModelModel.MinFreq, 100000);
                txtRxFreq.Text = $"{freqDouble2:f5}";
            }
            if (string.IsNullOrEmpty(rxFreq))
            {
                dgvFreq[1, rowIndex].Value = txtRxFreq.Text;
                dgvFreq[2, rowIndex].Value = Lang.SZ_NONE;
                dgvFreq[3, rowIndex].Value = Lang.SZ_NONE;
                dgvFreq[4, rowIndex].Value = Lang.SZ_POWER[1];
                dgvFreq[5, rowIndex].Value = Lang.SZ_BANDWIDTH[1];
                dgvFreq[6, rowIndex].Value = Lang.SZ_SCAN[0];
                dgvFreq[7, rowIndex].Value = Lang.SZ_SQL_MODE[0];
            }
        }
        else
        {
            for (int col = 1; col < dgvFreq.ColumnCount; col++)
            {
                dgvFreq[col, rowIndex].Value = "";
            }
        }
        dgvFreq.CurrentCell.Value = txtRxFreq.Text;
        txtRxFreq.Visible = false;
        ControlDgvBackground(rowIndex);
    }

    private void txtRxFreq_KeyPress(object sender, KeyPressEventArgs e)
    {
        if ("0123456789.".IndexOf(char.ToUpper(e.KeyChar)) < 0 && !char.IsControl(e.KeyChar))
        {
            e.Handled = true;
        }
    }

    private void txtTxFreq_KeyPress(object sender, KeyPressEventArgs e)
    {
        if ("0123456789.".IndexOf(char.ToUpper(e.KeyChar)) < 0 && !char.IsControl(e.KeyChar))
        {
            e.Handled = true;
        }
    }

    private void txtTxFreq_Leave(object sender, EventArgs e)
    {
        string freq = txtTxFreq.Text;
        if (!string.IsNullOrEmpty(freq))
        {
            if (FrequencyHelper.FreqIsValid(freq))
            {
                double freqDouble = 0.0;
                double.TryParse(freq, out freqDouble);
                int freqInt = (int)Common.MulDecimal(freqDouble, 100000.0);
                FrequencyHelper.AdjustFreq(ref freqInt, 500, 625);
                freqDouble = Common.DivDecimal(freqInt, 100000);
                txtTxFreq.Text = $"{freqDouble:f5}";
            }
            else
            {
                double freqDouble2 = Common.DivDecimal(Cps.GetInstance().ModelModel.MinFreq, 100000);
                txtTxFreq.Text = $"{freqDouble2:f5}";
            }
        }
        if (dgvFreq.CurrentCell != null)
        {
            dgvFreq.CurrentCell.Value = txtTxFreq.Text;
        }
        txtTxFreq.Visible = false;
    }

    private void cmbTone_Leave(object sender, EventArgs e)
    {
        if (dgvFreq.CurrentCell != null)
        {
            VerifyTone();
            int rowIndex = dgvFreq.CurrentCell.RowIndex;
            int colIndex = dgvFreq.CurrentCell.ColumnIndex;
            if (colIndex == 2)
            {
                if (cmbTone.Text == Lang.SZ_NONE)
                {
                    dgvFreq[6, rowIndex].Value = Lang.SZ_NONE;
                }
                else if (dgvFreq.CurrentCell.Value.ToString() == Lang.SZ_NONE)
                {
                    dgvFreq[6, rowIndex].Value = Lang.SZ_SQL_MODE[1];
                }
            }
            dgvFreq.CurrentCell.Value = cmbTone.Text;
        }
        cmbTone.Visible = false;
    }

    private void cmbOther_Leave(object sender, EventArgs e)
    {
        _ = dgvFreq.CurrentCell.RowIndex;
        _ = dgvFreq.CurrentCell.ColumnIndex;
        if (dgvFreq.CurrentCell != null)
        {
            dgvFreq.CurrentCell.Value = cmbOther.Text;
        }
        cmbOther.Visible = false;
    }

    private void ControlDgvBackground(int rowIndex)
    {
        dgvFreq[2, rowIndex].Style.BackColor = dgvFreq.DefaultCellStyle.BackColor;
        dgvFreq[3, rowIndex].Style.BackColor = dgvFreq.DefaultCellStyle.BackColor;
        dgvFreq[5, rowIndex].Style.BackColor = dgvFreq.DefaultCellStyle.BackColor;
        dgvFreq[6, rowIndex].Style.BackColor = dgvFreq.DefaultCellStyle.BackColor;
    }

    private void Verify()
    {
        bool firstValid = false;
        int rxFreqDec = 0;
        for (int row = 0; row < dgvFreq.RowCount; row++)
        {
            if (dgvFreq[0, row].Value != null)
            {
                dgvFreq[0, row].Value.ToString();
                if (!firstValid)
                {
                    firstValid = true;
                }
            }
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
            txtRxFreq.Visible = false;
            txtTxFreq.Visible = false;
            cmbTone.Visible = false;
            cmbOther.Visible = false;
            txtName.Visible = false;
            return;
        }
        int rowIndex = dgvFreq.CurrentCell.RowIndex;
        int columnIndex = dgvFreq.CurrentCell.ColumnIndex;
        Rectangle cellRect = dgvFreq.GetCellDisplayRectangle(columnIndex, rowIndex, cutOverflow: false);
        if ((dgvFreq[0, rowIndex].Value != null && !string.IsNullOrEmpty(dgvFreq[0, rowIndex].Value.ToString())) || columnIndex == 0)
        {
            Control ctrl = GetControlByColumn(columnIndex);
            cellRect.Offset(dgvFreq.Location);
            ctrl.Left = cellRect.Left;
            ctrl.Top = cellRect.Top;
            ctrl.Width = cellRect.Width;
            ctrl.Height = cellRect.Height;
            if (dgvFreq.CurrentCell.Value == null)
            {
                ctrl.Text = "";
            }
            else
            {
                ctrl.Text = dgvFreq.CurrentCell.Value.ToString();
            }
            ctrl.Visible = true;
            ctrl.Focus();
            ctrl.BringToFront();
            if (ctrl is TextBox txt)
            {
                txt.SelectAll();
            }
        }
    }

    private void VerifyTone()
    {
        ushort value = 16;
        string tmp = string.Empty;
        string text = cmbTone.Text;
        try
        {
            if (text == Lang.SZ_NONE || string.IsNullOrEmpty(text))
            {
                cmbTone.Text = Lang.SZ_NONE;
                return;
            }
            string patter = "D[0-7]{3}N$";
            Regex reg = new Regex(patter);
            if (reg.IsMatch(text))
            {
                tmp = text.Substring(1, 3);
                value = Convert.ToUInt16(tmp, 8);
                if (value < 777)
                {
                    return;
                }
                cmbTone.Text = Lang.SZ_NONE;
            }
            patter = "D[0-7]{3}I$";
            reg = new Regex(patter);
            if (reg.IsMatch(text))
            {
                tmp = text.Substring(1, 3);
                value = Convert.ToUInt16(tmp, 8);
                if (value < 777)
                {
                    return;
                }
                cmbTone.Text = Lang.SZ_NONE;
            }
            double valDouble = double.Parse(text);
            if (valDouble >= 60.0 && valDouble < 260.0)
            {
                cmbTone.Text = valDouble.ToString("0.0");
            }
            else
            {
                cmbTone.Text = Lang.SZ_NONE;
            }
        }
        catch (Exception)
        {
            cmbTone.Text = Lang.SZ_NONE;
        }
    }

    private void txtName_KeyDown(object sender, KeyEventArgs e)
    {
        if (!e.Control || e.KeyCode != Keys.V)
        {
            return;
        }
        string content = Clipboard.GetText();
        if (string.IsNullOrEmpty(content))
        {
            return;
        }
        string text = content;
        foreach (char c in text)
        {
            if (!char.IsLetterOrDigit(c) && c != '-' && c != ' ')
            {
                e.SuppressKeyPress = true;
                break;
            }
        }
    }
}
