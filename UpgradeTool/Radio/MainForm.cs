using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Radio;

public class MainForm : Form
{
    private IContainer components;

    private StatusStrip ssrMain;

    private ToolStrip tsrMain;

    private ToolStripSeparator toolStripSeparator2;

    private ToolStripButton btnRead;

    private ToolStripButton btnWrite;

    private ToolStripButton btnPort;

    private ToolStripSeparator toolStripSeparator1;

    private ToolStripButton btnOpen;

    private ChannelControl channelControl;

    private Label File_Name;

    private TextBox File_textBox;

    private ToolStripStatusLabel lblInfo;
    private ToolStripStatusLabel lblPort;

    public static int CurCbr { get; set; }

    public static string CurCom { get; set; }

    public static string CurFileName { get; set; }

    public static string CurPwd { get; set; }
    // In MainForm.cs
    public static string GetRadioSpeed { get; set; }

    // When the combo box changes:
    private void cboProgSpeed_CheckedChanged(object sender, EventArgs e)
    {
        if (cboProgSpeed.SelectedItem != null)
            GetRadioSpeed = cboProgSpeed.SelectedItem.ToString();
        else
            GetRadioSpeed = cboProgSpeed.Text; // fallback if SelectedItem is nullGetRadioSpeed = cboProgSpeed.SelectedItem.ToString();
    }

    public MainForm()
    {
        InitializeComponent();
        // Ensure GetRadioSpeed is initialized to the default value
        cboProgSpeed.SelectedIndex = 0;
        if (cboProgSpeed.SelectedItem != null)
            GetRadioSpeed = cboProgSpeed.SelectedItem.ToString();
        else
            GetRadioSpeed = cboProgSpeed.Text; // fallback if SelectedItem is null
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        Global.EEROM = new byte[524288];
        InitPort();
        RefreshPort();
        InitFileData();
    }

    private void InitPort()
    {
        CurCom = Global.SetupIniHelper.ReadString("Setup", "Com", "Com1");
        CurCbr = Global.SetupIniHelper.ReadInt("Setup", "Baudrate", 115200);
    }

    private void SaveAllPage()
    {
    }

    private void DispAllPage()
    {
    }

    private void SaveCurPage()
    {
    }

    private void DispCurPage()
    {
    }

    private void SavePage(TabPage page)
    {
        if (page == null)
        {
            return;
        }
        foreach (object ctrl in page.Controls)
        {
            if (ctrl is IViewData view)
            {
                view.ViewToData();
            }
        }
    }

    private void DispPage(TabPage page)
    {
        if (page == null)
        {
            return;
        }
        foreach (object ctrl in page.Controls)
        {
            if (ctrl is IViewData view)
            {
                view.DataToView();
            }
        }
    }

    private void tabMain_Deselected(object sender, TabControlEventArgs e)
    {
        SaveCurPage();
    }

    private void tabMain_Selected(object sender, TabControlEventArgs e)
    {
        DispCurPage();
        e.TabPage.Focus();
    }

    private void tsmiAbout_Click(object sender, EventArgs e)
    {
        using AboutForm about = new AboutForm();
        about.ShowDialog();
    }

    private void tsmiPort_Click(object sender, EventArgs e)
    {
        using PortForm port = new PortForm();
        port.ShowDialog();
        RefreshPort();
    }

    private void tsmiRead_Click(object sender, EventArgs e)
    {
        LeaveDataGridView();
        CommPrgForm comm = new CommPrgForm();
        comm.StartPosition = FormStartPosition.CenterParent;
        comm.IsRead = true;
        comm.ShowDialog();
        if (comm.IsSucess)
        {
            DispAllPage(); // Display the read data
            btnRead.Visible = true; // Make the Save button visible
            btnRead.Enabled = true; // Enable the Save button
            MessageBox.Show("Firmware read successfully. You can now save it.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            MessageBox.Show("Failed to read firmware. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void tsmiWrite_Click(object sender, EventArgs e)
    {
        LeaveDataGridView();
        SaveCurPage();
        CommPrgForm comm = new CommPrgForm();
        comm.StartPosition = FormStartPosition.CenterParent;
        comm.IsRead = false;
        comm.ShowDialog();
        _ = comm.IsSucess;
    }

    private void InitFileData()
    {
    }

    private void tsmiOpen_Click(object sender, EventArgs e)
    {
        try
        {
            LeaveDataGridView();
            OpenFileDialog ofdMain = new OpenFileDialog();
            ofdMain.Filter = "Update File (*.dat)|*.dat";
            ofdMain.InitialDirectory = Application.StartupPath;
            DialogResult result = ofdMain.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrEmpty(ofdMain.FileName))
            {
                byte[] dat = File.ReadAllBytes(ofdMain.FileName);
                Array.Copy(dat, 0, Global.EEROM, 0, Math.Min(dat.Length, Global.EEROM.Length));
                CurFileName = ofdMain.FileName;
                File_textBox.Text = CurFileName;
                DispAllPage();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void tsmiExit_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void tsmiNew_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show(Lang.SZ_INIT_DATA, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
        {
            LeaveDataGridView();
            InitFileData();
            CurFileName = "";
        }
    }

    private void tsmiSave_Click(object sender, EventArgs e)
    {
        try
        {
            LeaveDataGridView();
            SaveFileDialog sfdMain = new SaveFileDialog();
            sfdMain.Filter = "Data (*.dat)|*.dat";
            if (string.IsNullOrEmpty(CurFileName))
            {
                sfdMain.FileName = DateTime.Now.ToString("yyMMdd_HHmmss") + ".dat";
                sfdMain.InitialDirectory = Application.StartupPath;
            }
            else
            {
                sfdMain.InitialDirectory = Path.GetDirectoryName(CurFileName);
                sfdMain.FileName = Path.GetFileName(CurFileName);
            }
            SaveAllPage();
            DialogResult result = sfdMain.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrEmpty(sfdMain.FileName))
            {
                byte[] dat = DataToByte();
                File.WriteAllBytes(sfdMain.FileName, dat);
                CurFileName = sfdMain.FileName;
                MessageBox.Show(Lang.SZ_SAVE_DONE, Lang.SZ_PROMPT, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    public static byte[] DataToByte()
    {
        byte[] tmp = null;
        byte[] eerom = new byte[524288];
        eerom.Reset(byte.MaxValue);
        try
        {
            tmp = Cps.GetInstance().ChannelModel.DataToBytes();
            Array.Copy(tmp, 0, eerom, 128, tmp.Length);
            tmp = Cps.GetInstance().ChannelIndexModel.DataToBytes();
            Array.Copy(tmp, 0, eerom, 1856, tmp.Length);
            tmp = Cps.GetInstance().TotModel.DataToBytes();
            Array.Copy(tmp, 0, eerom, 1824, tmp.Length);
            tmp = Cps.GetInstance().ButtonModel.DataToBytes();
            Array.Copy(tmp, 0, eerom, 1840, tmp.Length);
            tmp = Cps.GetInstance().BasicModel.DataToBytes();
            Array.Copy(tmp, 0, eerom, 1800, tmp.Length);
            tmp = Cps.GetInstance().DtmfContactModel.DataToBytes();
            Array.Copy(tmp, 0, eerom, 2112, tmp.Length);
            tmp = Cps.GetInstance().DtmfContactIndexModel.DataToBytes();
            Array.Copy(tmp, 0, eerom, 2096, tmp.Length);
            tmp = Cps.GetInstance().DtmfBasicModel.DataToBytes();
            Array.Copy(tmp, 0, eerom, 1952, tmp.Length);
            tmp = Cps.GetInstance().ScanBasicModel.DataToBytes();
            Array.Copy(tmp, 0, eerom, 1888, tmp.Length);
            tmp = Cps.GetInstance().ScanIndexModel.DataToBytes();
            Array.Copy(tmp, 0, eerom, 1920, tmp.Length);
            tmp = Cps.GetInstance().SkipFrequencyModel.DataToBytes();
            Array.Copy(tmp, 0, eerom, 2384, tmp.Length);
            tmp = Cps.GetInstance().ModelModel.DataToBytes();
            Array.Copy(tmp, 0, eerom, 1792, tmp.Length);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        return eerom;
    }

    public static void ByteToData(byte[] eerom)
    {
        byte[] tmp = null;
        try
        {
            tmp = new byte[8];
            Array.Copy(eerom, 1792, tmp, 0, tmp.Length);
            Cps.GetInstance().ModelModel.BytesToData(tmp);
            tmp = new byte[1600];
            Array.Copy(eerom, 128, tmp, 0, tmp.Length);
            Cps.GetInstance().ChannelModel.BytesToData(tmp);
            tmp = new byte[8];
            Array.Copy(eerom, 1824, tmp, 0, tmp.Length);
            Cps.GetInstance().TotModel.BytesToData(tmp);
            tmp = new byte[8];
            Array.Copy(eerom, 1840, tmp, 0, tmp.Length);
            Cps.GetInstance().ButtonModel.BytesToData(tmp);
            tmp = new byte[16];
            Array.Copy(eerom, 1800, tmp, 0, tmp.Length);
            Cps.GetInstance().BasicModel.BytesToData(tmp);
            tmp = new byte[256];
            Array.Copy(eerom, 2112, tmp, 0, tmp.Length);
            Cps.GetInstance().DtmfContactModel.BytesToData(tmp);
            tmp = new byte[88];
            Array.Copy(eerom, 1952, tmp, 0, tmp.Length);
            Cps.GetInstance().DtmfBasicModel.BytesToData(tmp);
            tmp = new byte[8];
            Array.Copy(eerom, 1888, tmp, 0, tmp.Length);
            Cps.GetInstance().ScanBasicModel.BytesToData(tmp);
            tmp = new byte[16];
            Array.Copy(eerom, 1920, tmp, 0, tmp.Length);
            Cps.GetInstance().ScanIndexModel.BytesToData(tmp);
            tmp = new byte[2816];
            Array.Copy(eerom, 2384, tmp, 0, tmp.Length);
            Cps.GetInstance().SkipFrequencyModel.BytesToData(tmp);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void RefreshPort()
    {
        lblPort.Text = $"Port: {CurCom}";
    }

    private void tsmiChinese_Click(object sender, EventArgs e)
    {
        if (!(Global.CurLang == "Chinese"))
        {
            SaveAllPage();
            byte[] data = DataToByte();
            ChangeLang("Chinese");
            ByteToData(data);
            DispAllPage();
        }
    }

    private void tsmiEnglish_Click(object sender, EventArgs e)
    {
        if (!(Global.CurLang == "English"))
        {
            SaveAllPage();
            byte[] data = DataToByte();
            ChangeLang("English");
            ByteToData(data);
            DispAllPage();
        }
    }

    private void ChangeLang(string lang)
    {
        Global.CurLang = lang;
        Global.SetupIniHelper.WriteString("Setup", "CurLang", lang);
        LangHelper.LangIniHelper.IniPath = Application.StartupPath + Path.DirectorySeparatorChar + Global.CurLang + ".ini";
        LangHelper.ApplyCommon(typeof(Lang));
        LangHelper.ApplyForm(this);
    }

    private void MainForm_Resize(object sender, EventArgs e)
    {
        if (base.WindowState != FormWindowState.Normal)
        {
            _ = base.WindowState;
            _ = 2;
        }
    }

    private void tsmiFileEncrypt_Click(object sender, EventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.Filter = "Text (*.txt)|*.txt";
        ofd.InitialDirectory = Application.StartupPath;
        DialogResult result = ofd.ShowDialog();
        if (result == DialogResult.OK)
        {
            string text = File.ReadAllText(ofd.FileName);
            byte[] data = Encoding.ASCII.GetBytes(text);
            for (int i = 0; i < data.Length; i++)
            {
                data[i] ^= 165;
            }
            File.WriteAllBytes(Path.ChangeExtension(ofd.FileName, ".bin"), data);
        }
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = false;
    }

    private void LeaveDataGridView()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ssrMain = new System.Windows.Forms.StatusStrip();
            this.lblInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblPort = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsrMain = new System.Windows.Forms.ToolStrip();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRead = new System.Windows.Forms.ToolStripButton();
            this.btnWrite = new System.Windows.Forms.ToolStripButton();
            this.btnPort = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.File_Name = new System.Windows.Forms.Label();
            this.File_textBox = new System.Windows.Forms.TextBox();
            this.cboProgSpeed = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ssrMain.SuspendLayout();
            this.tsrMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // ssrMain
            // 
            this.ssrMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblInfo,
            this.lblPort});
            this.ssrMain.Location = new System.Drawing.Point(0, 129);
            this.ssrMain.Name = "ssrMain";
            this.ssrMain.Size = new System.Drawing.Size(644, 22);
            this.ssrMain.TabIndex = 1;
            this.ssrMain.Text = "statusStrip1";
            // 
            // lblInfo
            // 
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(22, 17);
            this.lblInfo.Text = "---";
            // 
            // lblPort
            // 
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(22, 17);
            this.lblPort.Text = "---";
            // 
            // tsrMain
            // 
            this.tsrMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpen,
            this.toolStripSeparator2,
            this.btnRead,
            this.btnWrite,
            this.btnPort,
            this.toolStripSeparator1});
            this.tsrMain.Location = new System.Drawing.Point(0, 0);
            this.tsrMain.Name = "tsrMain";
            this.tsrMain.Size = new System.Drawing.Size(644, 25);
            this.tsrMain.TabIndex = 2;
            this.tsrMain.Text = "toolStrip1";
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(23, 22);
            this.btnOpen.Text = "Open";
            this.btnOpen.Click += new System.EventHandler(this.tsmiOpen_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnRead
            // 
            this.btnRead.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRead.Image = ((System.Drawing.Image)(resources.GetObject("btnRead.Image")));
            this.btnRead.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(23, 22);
            this.btnRead.Text = "Read";
            this.btnRead.Visible = false;
            this.btnRead.Click += new System.EventHandler(this.tsmiRead_Click);
            // 
            // btnWrite
            // 
            this.btnWrite.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnWrite.Image = ((System.Drawing.Image)(resources.GetObject("btnWrite.Image")));
            this.btnWrite.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(23, 22);
            this.btnWrite.Text = "Write";
            this.btnWrite.Click += new System.EventHandler(this.tsmiWrite_Click);
            // 
            // btnPort
            // 
            this.btnPort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPort.Image = ((System.Drawing.Image)(resources.GetObject("btnPort.Image")));
            this.btnPort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPort.Name = "btnPort";
            this.btnPort.Size = new System.Drawing.Size(23, 22);
            this.btnPort.Text = "Port";
            this.btnPort.Click += new System.EventHandler(this.tsmiPort_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // File_Name
            // 
            this.File_Name.AutoSize = true;
            this.File_Name.Location = new System.Drawing.Point(75, 66);
            this.File_Name.Name = "File_Name";
            this.File_Name.Size = new System.Drawing.Size(61, 13);
            this.File_Name.TabIndex = 3;
            this.File_Name.Text = "Update file:";
            // 
            // File_textBox
            // 
            this.File_textBox.Enabled = false;
            this.File_textBox.Location = new System.Drawing.Point(134, 62);
            this.File_textBox.Name = "File_textBox";
            this.File_textBox.ReadOnly = true;
            this.File_textBox.Size = new System.Drawing.Size(453, 20);
            this.File_textBox.TabIndex = 4;
            // 
            // cboProgSpeed
            // 
            this.cboProgSpeed.FormattingEnabled = true;
            this.cboProgSpeed.Items.AddRange(new object[] {
            "115200",
            //"57600",
            "38400",
            //"19200",
            //"9600",
            //"4800"
            });
            this.cboProgSpeed.Location = new System.Drawing.Point(186, 93);
            this.cboProgSpeed.Name = "cboProgSpeed";
            this.cboProgSpeed.Size = new System.Drawing.Size(121, 21);
            this.cboProgSpeed.TabIndex = 6;
            this.cboProgSpeed.SelectedIndexChanged += new System.EventHandler(this.cboProgSpeed_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(78, 101);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Programming Speed:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 151);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboProgSpeed);
            this.Controls.Add(this.File_textBox);
            this.Controls.Add(this.File_Name);
            this.Controls.Add(this.tsrMain);
            this.Controls.Add(this.ssrMain);
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.ssrMain.ResumeLayout(false);
            this.ssrMain.PerformLayout();
            this.tsrMain.ResumeLayout(false);
            this.tsrMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }


    private ComboBox cboProgSpeed;
    private Label label1;

    private void cboProgSpeed_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboProgSpeed.SelectedItem != null)
            GetRadioSpeed = cboProgSpeed.SelectedItem.ToString();
        else
            GetRadioSpeed = cboProgSpeed.Text; // fallback if SelectedItem is null
    }
}
