using System;
using System.ComponentModel;
using System.Drawing;
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

	private ToolStripButton btnWrite;

	private ToolStripButton btnPort;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripButton btnOpen;

	private ChannelControl channelControl;

	private Label File_Name;

	private TextBox File_textBox;

	private ToolStripStatusLabel lblInfo;

	private ToolStripStatusLabel lblPort;

	private Button btnChineseOpen;

	private Button btnEnglishOpen;

	private TextBox File_textBoxEnglish;

	private Label label1;

	private static int s_data_addr;

	public static int CurCbr { get; set; }

	public static string CurCom { get; set; }

	public static string CurFileName { get; set; }

	public static string CurPwd { get; set; }

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
            this.btnWrite = new System.Windows.Forms.ToolStripButton();
            this.btnPort = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.File_Name = new System.Windows.Forms.Label();
            this.File_textBox = new System.Windows.Forms.TextBox();
            this.btnChineseOpen = new System.Windows.Forms.Button();
            this.btnEnglishOpen = new System.Windows.Forms.Button();
            this.File_textBoxEnglish = new System.Windows.Forms.TextBox();
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
            this.ssrMain.Location = new System.Drawing.Point(0, 147);
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
            this.btnOpen.Visible = false;
            this.btnOpen.Click += new System.EventHandler(this.tsmiOpen_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            this.File_Name.Location = new System.Drawing.Point(31, 66);
            this.File_Name.Name = "File_Name";
            this.File_Name.Size = new System.Drawing.Size(49, 13);
            this.File_Name.TabIndex = 3;
            this.File_Name.Text = "Filename";
            // 
            // File_textBox
            // 
            this.File_textBox.Enabled = false;
            this.File_textBox.Location = new System.Drawing.Point(90, 62);
            this.File_textBox.Name = "File_textBox";
            this.File_textBox.ReadOnly = true;
            this.File_textBox.Size = new System.Drawing.Size(453, 20);
            this.File_textBox.TabIndex = 4;
            // 
            // btnChineseOpen
            // 
            this.btnChineseOpen.Location = new System.Drawing.Point(557, 62);
            this.btnChineseOpen.Name = "btnChineseOpen";
            this.btnChineseOpen.Size = new System.Drawing.Size(75, 25);
            this.btnChineseOpen.TabIndex = 5;
            this.btnChineseOpen.Text = "Open";
            this.btnChineseOpen.UseVisualStyleBackColor = true;
            this.btnChineseOpen.Click += new System.EventHandler(this.btnChineseOpen_Click);
            // 
            // btnEnglishOpen
            // 
            this.btnEnglishOpen.Location = new System.Drawing.Point(557, 91);
            this.btnEnglishOpen.Name = "btnEnglishOpen";
            this.btnEnglishOpen.Size = new System.Drawing.Size(75, 25);
            this.btnEnglishOpen.TabIndex = 8;
            this.btnEnglishOpen.Text = "Open";
            this.btnEnglishOpen.UseVisualStyleBackColor = true;
            this.btnEnglishOpen.Visible = false;
            this.btnEnglishOpen.Click += new System.EventHandler(this.btnEnglishOpen_Click);
            // 
            // File_textBoxEnglish
            // 
            this.File_textBoxEnglish.Enabled = false;
            this.File_textBoxEnglish.Location = new System.Drawing.Point(90, 91);
            this.File_textBoxEnglish.Name = "File_textBoxEnglish";
            this.File_textBoxEnglish.ReadOnly = true;
            this.File_textBoxEnglish.Size = new System.Drawing.Size(453, 20);
            this.File_textBoxEnglish.TabIndex = 7;
            this.File_textBoxEnglish.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Font ";
            this.label1.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 169);
            this.Controls.Add(this.btnEnglishOpen);
            this.Controls.Add(this.File_textBoxEnglish);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnChineseOpen);
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

	public MainForm()
	{
		InitializeComponent();
	}

	private void MainForm_Load(object sender, EventArgs e)
	{
		Global.EEROM = new byte[458752];
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
		foreach (object control in page.Controls)
		{
			if (control is IViewData view)
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
		foreach (object control in page.Controls)
		{
			if (control is IViewData view)
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
			DispAllPage();
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
			ofdMain.InitialDirectory = Application.StartupPath;
			ofdMain.Multiselect = true;
			if (ofdMain.ShowDialog() != DialogResult.OK || string.IsNullOrEmpty(ofdMain.FileName))
			{
				return;
			}
			for (int i = 0; !string.IsNullOrEmpty(ofdMain.FileNames[i]); i++)
			{
				FileStream fs = new FileStream(ofdMain.FileNames[i], FileMode.Open);
				StreamReader sr = new StreamReader(fs, Encoding.Default);
				for (string temp = sr.ReadLine(); temp != null; temp = sr.ReadLine())
				{
					_ = temp != "";
				}
				fs.Close();
				byte[] dat = File.ReadAllBytes(ofdMain.FileNames[i]);
				Array.Copy(dat, 0, Global.EEROM, 0, Math.Min(dat.Length, Global.EEROM.Length));
			}
			CurFileName = ofdMain.FileName;
			File_textBox.Text = CurFileName;
			DispAllPage();
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
			if (sfdMain.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(sfdMain.FileName))
			{
				File.WriteAllBytes(bytes: DataToByte(), path: sfdMain.FileName);
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
		byte[] eerom = new byte[458752];
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
		if (ofd.ShowDialog() == DialogResult.OK)
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

	private void btnChineseOpen_Click(object sender, EventArgs e)
	{
		int addr = 0;
		try
		{
            LeaveDataGridView();
            OpenFileDialog ofdMain = new OpenFileDialog();
			ofdMain.InitialDirectory = Application.StartupPath;
			ofdMain.Multiselect = true;
			if (ofdMain.ShowDialog() != DialogResult.OK || string.IsNullOrEmpty(ofdMain.FileName))
			{
				return;
			}
			int i = 0;
			byte[] tempcode = new byte[4];
			uint data_cnt = 0u;
            //CurFileName = ofdMain.FileName;
            foreach (string fileName in ofdMain.FileNames)
            {
				int num = 0;
				FileStream fs = new FileStream(fileName, FileMode.Open);
				StreamReader sr = new StreamReader(fs, Encoding.Default);
				for (string temp = sr.ReadLine(); temp != null; temp = sr.ReadLine())
				{
					int index = temp.IndexOf("0x");
					if (index != -1)
					{
						index += 2;
						while (temp.Length != 0)
						{
							string data = temp.Substring(index, 1);
							tempcode = Encoding.Default.GetBytes(data);
							if (tempcode[0] <= 57)
							{
								num = tempcode[0] - 48;
							}
							else if (tempcode[0] >= 65 && tempcode[0] <= 90)
							{
								num = tempcode[0] - 65 + 10;
							}
							else if (tempcode[0] >= 97 && tempcode[0] <= 122)
							{
								num = tempcode[0] - 97 + 10;
							}
							index++;
							data = temp.Substring(index, 1);
							tempcode = Encoding.Default.GetBytes(data);
							num <<= 4;
							if (tempcode[0] <= 57)
							{
								num |= tempcode[0] - 48;
							}
							else if (tempcode[0] >= 65 && tempcode[0] <= 90)
							{
								num |= tempcode[0] - 65 + 10;
							}
							else if (tempcode[0] >= 97 && tempcode[0] <= 122)
							{
								num |= tempcode[0] - 97 + 10;
							}
							Global.EEROM[data_cnt] = (byte)(num & 0xFF);
							data_cnt++;
							index += 2;
							if (index > temp.Length)
							{
								break;
							}
							temp = temp.Substring(index, temp.Length - index);
							index = temp.IndexOf("0x");
							if (index == -1)
							{
								break;
							}
							index += 2;
						}
					}
				}
				fs.Close();
			}
			CurFileName = ofdMain.FileName;
			File_textBox.Text = CurFileName;
			DispAllPage();
		}
		catch (Exception)
		{
            s_data_addr = addr;
		}
	}

	private void btnEnglishOpen_Click(object sender, EventArgs e)
	{
		try
		{
			OpenFileDialog ofdMain = new OpenFileDialog();
			ofdMain.InitialDirectory = Application.StartupPath;
			ofdMain.Multiselect = true;
			if (ofdMain.ShowDialog() != DialogResult.OK || string.IsNullOrEmpty(ofdMain.FileName))
			{
				return;
			}
			int i = 0;
			_ = s_data_addr;
			int data_cnt = 0;
			while (!string.IsNullOrEmpty(ofdMain.FileNames[i]))
			{
				FileStream fs = new FileStream(ofdMain.FileNames[i], FileMode.Open);
				StreamReader sr = new StreamReader(fs, Encoding.Default);
				for (string temp = sr.ReadLine(); temp != null; temp = sr.ReadLine())
				{
					Encoding utf_8 = Encoding.GetEncoding("UTF-8");
					byte[] convert_data_buf = Encoding.Convert(utf_8, Encoding.GetEncoding("gb2312"), utf_8.GetBytes(temp));
					for (i = 0; i < convert_data_buf.Length; i++)
					{
						Global.EEROM[i + data_cnt] = convert_data_buf[i];
					}
					data_cnt += i;
				}
				fs.Close();
				fs = new FileStream("55.txt", FileMode.CreateNew);
				int shift = 0;
				byte[] buffer = new byte[2097152];
				byte[] buffer2 = new byte[5];
				for (uint k = 0u; k < data_cnt; k++)
				{
					if (k % 2 == 0)
					{
						string test = "0x" + ((int)Global.EEROM[k]).ToString("x2");
						buffer2 = Encoding.Default.GetBytes(test);
						buffer[shift++] = buffer2[0];
						buffer[shift++] = buffer2[1];
						buffer[shift++] = buffer2[2];
						buffer[shift++] = buffer2[3];
					}
					else
					{
						string test = ((int)Global.EEROM[k]).ToString("x2") + ",";
						buffer2 = Encoding.Default.GetBytes(test);
						buffer[shift++] = buffer2[0];
						buffer[shift++] = buffer2[1];
						buffer[shift++] = buffer2[2];
					}
					if ((k + 1) % 16 == 0)
					{
						buffer[shift++] = 13;
					}
				}
				fs.Write(buffer, 0, shift);
				fs.Close();
			}
			CurFileName = ofdMain.FileName;
			File_textBox.Text = CurFileName;
			DispAllPage();
		}
		catch (Exception)
		{
		}
	}
}
