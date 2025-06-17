using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Radio;

public class BasicControl : UserControl, IViewData
{
    private IContainer components;

    private Label lblApo;

    private Label lblSavingDelay;

    private Label lblSaving;

    private Label lblVoxDelay;

    private Label lblVox;

    private ComboBox cmbVox;

    private Label lblSql;

    private ComboBox cmbSql;

    private Label lblKeyLock;

    private ComboBox cmbKeyLock;

    private Label lblBusyLock;

    private ComboBox cmbBusyLock;

    private Label lblVoiceLang;

    private ComboBox cmbVoiceLang;

    private Label lblBeep;

    private ComboBox cmbBeep;

    private ComboBox cmbApo;

    private ComboBox cmbSavingDelay;

    private ComboBox cmbSaving;

    private ComboBox cmbVoxDelay;

    private Label lblVoxSwitch;

    private ComboBox cmbVoxSwitch;

    public BasicControl()
    {
        InitializeComponent();
    }

    private void BasicControl_Load(object sender, EventArgs e)
    {
        DataToView();
    }

    public void InitView()
    {
        cmbBeep.Init(Lang.SZ_BEEP);
        cmbVoiceLang.Init(Lang.SZ_VOICE_LANG);
        cmbBusyLock.Init(Lang.SZ_BUSY_LOCK);
        cmbKeyLock.Init(Lang.SZ_KEY_LOCK);
        cmbSql.Init(1, 9, 1);
        cmbVox.Init(Lang.SZ_OFF, 1, 9, 1);
        cmbVoxDelay.Init(1, 30, 1, 0.1);
        cmbSaving.Init(Lang.SZ_SAVING);
        cmbSavingDelay.Init(5, 25, 5);
        cmbApo.Init(Lang.SZ_APO);
        cmbVoxSwitch.Init(Lang.SZ_VOX_SWITCH);
    }

    public void DataToView()
    {
        InitView();
        BasicModel BasicModel = Cps.GetInstance().BasicModel;
        cmbBeep.SetCurSel(BasicModel.Beep);
        cmbVoiceLang.SetCurSel(BasicModel.VoiceLang);
        cmbBusyLock.SetCurSel(BasicModel.BusyLock);
        cmbKeyLock.SetCurSel(BasicModel.KeyLock);
        cmbSql.SetCurSel(BasicModel.Sql - 1);
        cmbVox.SetCurSel(BasicModel.Vox);
        cmbVoxDelay.Text = BasicModel.VoxDelay;
        cmbSaving.SetCurSel(BasicModel.Saving);
        cmbSavingDelay.Text = BasicModel.SavingDelay;
        cmbApo.SetCurSel(BasicModel.Apo);
        cmbVoxSwitch.SetCurSel(BasicModel.VoxSwitch);
    }

    public void ViewToData()
    {
        BasicModel BasicModel = Cps.GetInstance().BasicModel;
        BasicModel.Beep = cmbBeep.SelectedIndex;
        BasicModel.VoiceLang = cmbVoiceLang.SelectedIndex;
        BasicModel.BusyLock = cmbBusyLock.SelectedIndex;
        BasicModel.KeyLock = cmbKeyLock.SelectedIndex;
        BasicModel.Sql = cmbSql.SelectedIndex + 1;
        BasicModel.Vox = cmbVox.SelectedIndex;
        BasicModel.VoxDelay = cmbVoxDelay.Text;
        BasicModel.Saving = cmbSaving.SelectedIndex;
        BasicModel.SavingDelay = cmbSavingDelay.Text;
        BasicModel.Apo = cmbApo.SelectedIndex;
        BasicModel.VoxSwitch = cmbVoxSwitch.SelectedIndex;
    }

    public void LoadLanguageText(string section)
    {
        foreach (Control ctrl in base.Controls)
        {
            LangHelper.ApplyControl(section, ctrl);
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
        this.lblApo = new System.Windows.Forms.Label();
        this.lblSavingDelay = new System.Windows.Forms.Label();
        this.lblSaving = new System.Windows.Forms.Label();
        this.lblVoxDelay = new System.Windows.Forms.Label();
        this.lblVox = new System.Windows.Forms.Label();
        this.cmbVox = new System.Windows.Forms.ComboBox();
        this.lblSql = new System.Windows.Forms.Label();
        this.cmbSql = new System.Windows.Forms.ComboBox();
        this.lblKeyLock = new System.Windows.Forms.Label();
        this.cmbKeyLock = new System.Windows.Forms.ComboBox();
        this.lblBusyLock = new System.Windows.Forms.Label();
        this.cmbBusyLock = new System.Windows.Forms.ComboBox();
        this.lblVoiceLang = new System.Windows.Forms.Label();
        this.cmbVoiceLang = new System.Windows.Forms.ComboBox();
        this.lblBeep = new System.Windows.Forms.Label();
        this.cmbBeep = new System.Windows.Forms.ComboBox();
        this.cmbApo = new System.Windows.Forms.ComboBox();
        this.cmbSavingDelay = new System.Windows.Forms.ComboBox();
        this.cmbSaving = new System.Windows.Forms.ComboBox();
        this.cmbVoxDelay = new System.Windows.Forms.ComboBox();
        this.lblVoxSwitch = new System.Windows.Forms.Label();
        this.cmbVoxSwitch = new System.Windows.Forms.ComboBox();
        base.SuspendLayout();
        this.lblApo.Location = new System.Drawing.Point(20, 290);
        this.lblApo.Name = "lblApo";
        this.lblApo.Size = new System.Drawing.Size(160, 20);
        this.lblApo.TabIndex = 18;
        this.lblApo.Text = "Automatic shutdown";
        this.lblApo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.lblApo.Visible = false;
        this.lblSavingDelay.Location = new System.Drawing.Point(20, 230);
        this.lblSavingDelay.Name = "lblSavingDelay";
        this.lblSavingDelay.Size = new System.Drawing.Size(160, 20);
        this.lblSavingDelay.TabIndex = 16;
        this.lblSavingDelay.Text = "Power saving mode delay on time";
        this.lblSavingDelay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.lblSavingDelay.Visible = false;
        this.lblSaving.Location = new System.Drawing.Point(20, 200);
        this.lblSaving.Name = "lblSaving";
        this.lblSaving.Size = new System.Drawing.Size(160, 20);
        this.lblSaving.TabIndex = 14;
        this.lblSaving.Text = "Power saving function";
        this.lblSaving.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.lblVoxDelay.Location = new System.Drawing.Point(20, 170);
        this.lblVoxDelay.Name = "lblVoxDelay";
        this.lblVoxDelay.Size = new System.Drawing.Size(160, 20);
        this.lblVoxDelay.TabIndex = 12;
        this.lblVoxDelay.Text = "Voice-controlled delay time";
        this.lblVoxDelay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.lblVox.Location = new System.Drawing.Point(20, 140);
        this.lblVox.Name = "lblVox";
        this.lblVox.Size = new System.Drawing.Size(160, 20);
        this.lblVox.TabIndex = 10;
        this.lblVox.Text = "Voice-controlled level";
        this.lblVox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.cmbVox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbVox.FormattingEnabled = true;
        this.cmbVox.Location = new System.Drawing.Point(190, 140);
        this.cmbVox.Name = "cmbVox";
        this.cmbVox.Size = new System.Drawing.Size(140, 20);
        this.cmbVox.TabIndex = 11;
        this.lblSql.Location = new System.Drawing.Point(20, 80);
        this.lblSql.Name = "lblSql";
        this.lblSql.Size = new System.Drawing.Size(160, 20);
        this.lblSql.TabIndex = 8;
        this.lblSql.Text = "Squelch level";
        this.lblSql.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.cmbSql.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbSql.FormattingEnabled = true;
        this.cmbSql.Location = new System.Drawing.Point(190, 80);
        this.cmbSql.Name = "cmbSql";
        this.cmbSql.Size = new System.Drawing.Size(140, 20);
        this.cmbSql.TabIndex = 9;
        this.lblKeyLock.Location = new System.Drawing.Point(20, 321);
        this.lblKeyLock.Name = "lblKeyLock";
        this.lblKeyLock.Size = new System.Drawing.Size(160, 20);
        this.lblKeyLock.TabIndex = 6;
        this.lblKeyLock.Text = "Keyboard lock";
        this.lblKeyLock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.lblKeyLock.Visible = false;
        this.cmbKeyLock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbKeyLock.FormattingEnabled = true;
        this.cmbKeyLock.Location = new System.Drawing.Point(190, 320);
        this.cmbKeyLock.Name = "cmbKeyLock";
        this.cmbKeyLock.Size = new System.Drawing.Size(140, 20);
        this.cmbKeyLock.TabIndex = 7;
        this.cmbKeyLock.Visible = false;
        this.lblBusyLock.Location = new System.Drawing.Point(20, 50);
        this.lblBusyLock.Name = "lblBusyLock";
        this.lblBusyLock.Size = new System.Drawing.Size(160, 20);
        this.lblBusyLock.TabIndex = 4;
        this.lblBusyLock.Text = "Busy banned";
        this.lblBusyLock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.cmbBusyLock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbBusyLock.FormattingEnabled = true;
        this.cmbBusyLock.Location = new System.Drawing.Point(190, 50);
        this.cmbBusyLock.Name = "cmbBusyLock";
        this.cmbBusyLock.Size = new System.Drawing.Size(140, 20);
        this.cmbBusyLock.TabIndex = 5;
        this.lblVoiceLang.Location = new System.Drawing.Point(19, 261);
        this.lblVoiceLang.Name = "lblVoiceLang";
        this.lblVoiceLang.Size = new System.Drawing.Size(160, 20);
        this.lblVoiceLang.TabIndex = 2;
        this.lblVoiceLang.Text = "Language broadcast type";
        this.lblVoiceLang.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.lblVoiceLang.Visible = false;
        this.cmbVoiceLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbVoiceLang.FormattingEnabled = true;
        this.cmbVoiceLang.Location = new System.Drawing.Point(189, 260);
        this.cmbVoiceLang.Name = "cmbVoiceLang";
        this.cmbVoiceLang.Size = new System.Drawing.Size(140, 20);
        this.cmbVoiceLang.TabIndex = 3;
        this.cmbVoiceLang.Visible = false;
        this.lblBeep.Location = new System.Drawing.Point(20, 20);
        this.lblBeep.Name = "lblBeep";
        this.lblBeep.Size = new System.Drawing.Size(160, 20);
        this.lblBeep.TabIndex = 0;
        this.lblBeep.Text = "Prompt sound";
        this.lblBeep.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.cmbBeep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbBeep.FormattingEnabled = true;
        this.cmbBeep.Location = new System.Drawing.Point(190, 20);
        this.cmbBeep.Name = "cmbBeep";
        this.cmbBeep.Size = new System.Drawing.Size(140, 20);
        this.cmbBeep.TabIndex = 1;
        this.cmbApo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbApo.FormattingEnabled = true;
        this.cmbApo.Location = new System.Drawing.Point(190, 290);
        this.cmbApo.Name = "cmbApo";
        this.cmbApo.Size = new System.Drawing.Size(140, 20);
        this.cmbApo.TabIndex = 19;
        this.cmbApo.Visible = false;
        this.cmbSavingDelay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbSavingDelay.FormattingEnabled = true;
        this.cmbSavingDelay.Location = new System.Drawing.Point(190, 230);
        this.cmbSavingDelay.Name = "cmbSavingDelay";
        this.cmbSavingDelay.Size = new System.Drawing.Size(140, 20);
        this.cmbSavingDelay.TabIndex = 17;
        this.cmbSavingDelay.Visible = false;
        this.cmbSaving.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbSaving.FormattingEnabled = true;
        this.cmbSaving.Location = new System.Drawing.Point(190, 200);
        this.cmbSaving.Name = "cmbSaving";
        this.cmbSaving.Size = new System.Drawing.Size(140, 20);
        this.cmbSaving.TabIndex = 15;
        this.cmbVoxDelay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbVoxDelay.FormattingEnabled = true;
        this.cmbVoxDelay.Location = new System.Drawing.Point(190, 170);
        this.cmbVoxDelay.Name = "cmbVoxDelay";
        this.cmbVoxDelay.Size = new System.Drawing.Size(140, 20);
        this.cmbVoxDelay.TabIndex = 13;
        this.lblVoxSwitch.Location = new System.Drawing.Point(20, 110);
        this.lblVoxSwitch.Name = "lblVoxSwitch";
        this.lblVoxSwitch.Size = new System.Drawing.Size(160, 20);
        this.lblVoxSwitch.TabIndex = 20;
        this.lblVoxSwitch.Text = "Voice-controlled switch";
        this.lblVoxSwitch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        this.cmbVoxSwitch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbVoxSwitch.FormattingEnabled = true;
        this.cmbVoxSwitch.Location = new System.Drawing.Point(190, 110);
        this.cmbVoxSwitch.Name = "cmbVoxSwitch";
        this.cmbVoxSwitch.Size = new System.Drawing.Size(140, 20);
        this.cmbVoxSwitch.TabIndex = 21;
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.Controls.Add(this.cmbBeep);
        base.Controls.Add(this.cmbVoiceLang);
        base.Controls.Add(this.lblBeep);
        base.Controls.Add(this.cmbVoxDelay);
        base.Controls.Add(this.cmbBusyLock);
        base.Controls.Add(this.cmbSaving);
        base.Controls.Add(this.lblVoiceLang);
        base.Controls.Add(this.cmbKeyLock);
        base.Controls.Add(this.cmbSavingDelay);
        base.Controls.Add(this.lblBusyLock);
        base.Controls.Add(this.cmbSql);
        base.Controls.Add(this.cmbApo);
        base.Controls.Add(this.lblKeyLock);
        base.Controls.Add(this.cmbVox);
        base.Controls.Add(this.lblSql);
        base.Controls.Add(this.lblVox);
        base.Controls.Add(this.lblVoxDelay);
        base.Controls.Add(this.lblSaving);
        base.Controls.Add(this.lblSavingDelay);
        base.Controls.Add(this.lblApo);
        base.Controls.Add(this.cmbVoxSwitch);
        base.Controls.Add(this.lblVoxSwitch);
        base.Name = "BasicControl";
        base.Size = new System.Drawing.Size(350, 351);
        base.Load += new System.EventHandler(BasicControl_Load);
        base.ResumeLayout(false);
    }
}
