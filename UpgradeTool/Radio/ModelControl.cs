using System.ComponentModel;
using System.Windows.Forms;

namespace Radio;

public class ModelControl : UserControl, IViewData
{
    private IContainer components;

    private Label lblRange;

    private ComboBox cmbRange;

    public string PreRange { get; set; }

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
        this.lblRange = new System.Windows.Forms.Label();
        this.cmbRange = new System.Windows.Forms.ComboBox();
        base.SuspendLayout();
        this.lblRange.Location = new System.Drawing.Point(20, 20);
        this.lblRange.Name = "lblRange";
        this.lblRange.Size = new System.Drawing.Size(160, 20);
        this.lblRange.TabIndex = 0;
        this.lblRange.Text = "Frequency band range";
        this.lblRange.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.cmbRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbRange.FormattingEnabled = true;
        this.cmbRange.Location = new System.Drawing.Point(190, 20);
        this.cmbRange.Name = "cmbRange";
        this.cmbRange.Size = new System.Drawing.Size(140, 20);
        this.cmbRange.TabIndex = 1;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.Controls.Add(this.cmbRange);
        base.Controls.Add(this.lblRange);
        base.Name = "ModelControl";
        base.Size = new System.Drawing.Size(370, 60);
        base.ResumeLayout(false);
    }

    public ModelControl()
    {
        InitializeComponent();
    }

    public void InitView()
    {
        if (MainForm.CurPwd == "ZWSG18")
        {
            cmbRange.Init(Global.SZ_FREQ_RANGE);
            return;
        }
        int[] indexes = new int[Global.SZ_FREQ_RANGE.Length - 1];
        for (int i = 0; i < indexes.Length; i++)
        {
            indexes[i] = i;
        }
        cmbRange.Init(Global.SZ_FREQ_RANGE, indexes);
    }

    public void DataToView()
    {
        ModelModel model = Cps.GetInstance().ModelModel;
        InitView();
        cmbRange.Text = $"{model.MinFreq / 100000}-{model.MaxFreq / 100000}MHz";
        if (string.IsNullOrEmpty(cmbRange.Text))
        {
            cmbRange.Text = Global.SZ_FREQ_RANGE[0];
            ViewToData();
        }
        PreRange = cmbRange.Text;
    }

    public void ViewToData()
    {
        ModelModel model = Cps.GetInstance().ModelModel;
        ChannelModel chModel = Cps.GetInstance().ChannelModel;
        bool modelChange = false;
        string text = cmbRange.Text;
        if (PreRange != text && text != Global.SZ_FREQ_RANGE[Global.SZ_FREQ_RANGE.Length - 1])
        {
            if (MessageBox.Show(Lang.SZ_INIT_FREQ, "", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) != DialogResult.Yes)
            {
                return;
            }
            chModel.Clear();
            if (Global.SkipFreqHash.Contains(text))
            {
                string[] freq = Global.SkipFreqHash[text].ToString().Split(',');
                SkipFrequencyModel skipFreqModel = Cps.GetInstance().SkipFrequencyModel;
                skipFreqModel.Import(freq);
            }
            PreRange = text;
            modelChange = true;
        }
        text = text.Substring(0, 7);
        string[] freqs = text.Split('-');
        int minFreq = 0;
        int.TryParse(freqs[0], out minFreq);
        model.MinFreq = minFreq * 100000;
        int maxFreq = 0;
        int.TryParse(freqs[1], out maxFreq);
        model.MaxFreq = maxFreq * 100000;
        if (modelChange)
        {
            chModel.InitFirstCh(model.MinFreq);
        }
    }

    public void LoadLanguageText(string section)
    {
        foreach (Control ctrl in base.Controls)
        {
            LangHelper.ApplyControl(section, ctrl);
        }
    }
}
