using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using CPS_5RH.Data;

namespace CPS_5RH;

public class FormProgressBar : Form
{
	private delegate void delgHandleCommunicationResult(PROMPT result);

	private delegate void delgUpdateProgress(string text, int value);

	public static int Baudrate = 9600;

	public string portName = null;

	public static string Pwd = "";

	public static string NewPwd = "";

	public static bool FlgNew = false;

	private string language = "";

	private SerialPort sP;

	public DataApp appData = null;

	private Thread threadCommunication;

	private Thread threadProgress;

	private DataProtocol DPT = null;

	private string strFile = string.Empty;

	public static byte[] BmpPicData = new byte[163840];

	public static int BmpPicLen = 0;

	private IContainer components = null;

	private ProgressBar progressBar;

	private Button pB_btnStart;

	private Button pB_btnCancel;

	private Label lab_progress;

	private ComboBox cbB_Port;

	private Label lbl_Port;

	private Label lbl_Baute;

	private ComboBox cbB_Baute;

	private TextBox txSend;

	private Button btn_LoadPic;

	private CheckBox ckBox_SetPwd;

	private TextBox tBox_NewPwd;

	private TextBox tBox_Pwd;

	private Label label2;

	private Label label1;

	public FormProgressBar(DataApp buf)
	{
		InitializeComponent();
		appData = buf;
		language = FormMain.lang;
		sP = new SerialPort();
		if (language == "English")
		{
			pB_btnCancel.Text = "Cancel(&C)";
			pB_btnStart.Text = "Start(&S)";
			lbl_Port.Text = "Port:";
			lbl_Baute.Text = "BaudRate:";
			label1.Text = "Password:";
			label2.Text = "New Password:";
			ckBox_SetPwd.Text = "Set";
			btn_LoadPic.Text = "Load File";
		}
		FlgNew = false;
		Pwd = "";
		NewPwd = "";
	}

	private void DispControl(int mode)
	{
		if (mode == 1)
		{
			lbl_Port.Visible = false;
			cbB_Port.Visible = false;
		}
		else
		{
			lbl_Port.Visible = true;
			cbB_Port.Visible = true;
		}
	}

	private bool ConfigComPort(SerialPort sP)
	{
		try
		{
			sP.PortName = cbB_Port.Text;
			sP.BaudRate = Baudrate;
			sP.DataBits = 8;
			sP.Parity = Parity.None;
			sP.StopBits = StopBits.One;
			sP.WriteBufferSize = 8192;
			sP.ReadBufferSize = 8192;
			sP.DtrEnable = base.Enabled;
			sP.RtsEnable = base.Enabled;
			sP.Open();
			return true;
		}
		catch
		{
			if (language == "Chinese")
			{
				MessageBox.Show("串口打开失败!");
			}
			else
			{
				MessageBox.Show("Failure!");
			}
			return false;
		}
	}

	private bool ChangeComPort()
	{
		try
		{
			if (sP.IsOpen)
			{
				sP.Close();
			}
			if (Baudrate == 19200)
			{
				Baudrate = 115200;
			}
			else
			{
				Baudrate = 19200;
			}
			Settings.Baudrate = Baudrate;
			Settings.Save();
			sP.BaudRate = Baudrate;
			sP.Open();
			return true;
		}
		catch
		{
			return false;
		}
	}

	private void CloseComPort(SerialPort sP)
	{
		if (sP.IsOpen)
		{
			sP.Close();
			sP.Dispose();
		}
	}

	private void Task_ProcessCommunication()
	{
		if (Text == "读频" || Text == "Read")
		{
			DPT = new DataProtocol(sP, appData, OPT_DIR.DIR_READ);
		}
		else if (Text == "写频" || Text == "Write")
		{
			DPT = new DataProtocol(sP, appData, OPT_DIR.DIR_WRITE);
		}
		else if (Text == "开机图片" || Text == "Boot Picture")
		{
			DPT = new DataProtocol(sP, appData, OPT_DIR.DIR_BOOT_PIC);
		}
		PROMPT result = DPT.DoIt();
		Invoke(new delgHandleCommunicationResult(handleCommunicationResult), result);
	}

	private void Task_GetProgress()
	{
		byte ptcnt = 0;
		int valProgress = 0;
		string content = null;
		while (DPT == null)
		{
			Thread.Sleep(100);
		}
		while (!DPT.flagTransmitting)
		{
			Thread.Sleep(100);
		}
		while (DPT.flagTransmitting)
		{
			switch (DPT.state)
			{
			case STATE.STATE_HANDSHAKE1:
			case STATE.STATE_HANDSHAKE2:
			case STATE.STATE_HANDSHAKE3:
			case STATE.STATE_HANDSHAKE4:
			case STATE.STATE_HANDSHAKE5:
			case STATE.STATE_HANDSHAKE6:
				content = ((!(language == "Chinese")) ? "hand shake..." : "握手...");
				Invoke(new delgUpdateProgress(updateProgress), content, valProgress);
				break;
			case STATE.STATE_READ1:
			case STATE.STATE_READ2:
				content = ((!(language == "Chinese")) ? "progress" : "进度");
				ptcnt++;
				switch (ptcnt)
				{
				case 1:
					content += ".  ";
					break;
				case 2:
					content += ".. ";
					break;
				default:
					ptcnt = 0;
					content += "...";
					break;
				}
				valProgress = Convert.ToInt32((double)(DPT.rxCnt * 100 / DataProtocol.DpDataLen));
				Invoke(new delgUpdateProgress(updateProgress), content + valProgress + "%", valProgress);
				break;
			case STATE.STATE_WRITE1:
			case STATE.STATE_WRITE2:
				content = ((!(language == "Chinese")) ? "progress" : "进度");
				ptcnt++;
				switch (ptcnt)
				{
				case 1:
					content += ".  ";
					break;
				case 2:
					content += ".. ";
					break;
				default:
					ptcnt = 0;
					content += "...";
					break;
				}
				valProgress = Convert.ToInt32((!(Text == "开机图片") && !(Text == "Boot Picture")) ? ((double)(DPT.rxCnt * 100 / DataProtocol.DpDataLen)) : ((double)(DPT.rxCnt * 100 / BmpPicLen)));
				Invoke(new delgUpdateProgress(updateProgress), content + valProgress + "%", valProgress);
				break;
			}
			Thread.Sleep(300);
		}
	}

	private void pB_btnStart_Click(object sender, EventArgs e)
	{
		pB_btnCancel.Enabled = false;
		pB_btnStart.Enabled = false;
		if (ConfigComPort(sP))
		{
			DispControl(1);
			threadCommunication = new Thread(Task_ProcessCommunication);
			threadCommunication.Start();
			threadProgress = new Thread(Task_GetProgress);
			threadProgress.Start();
		}
		else
		{
			pB_btnCancel.Enabled = true;
			pB_btnStart.Enabled = true;
		}
	}

	private void pB_btnCancel_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void FormProgressBar_FormClosing(object sender, FormClosingEventArgs e)
	{
		try
		{
			if (DPT != null)
			{
				while (DPT.flagTransmitting)
				{
					DPT.flagTransmitting = false;
					Thread.Sleep(20);
				}
			}
			CloseComPort(sP);
			if (threadCommunication != null)
			{
				threadCommunication.Abort();
			}
			if (threadProgress != null)
			{
				threadProgress.Abort();
			}
		}
		catch (Exception)
		{
		}
	}

	private void handleCommunicationResult(PROMPT result)
	{
		switch (result)
		{
		case PROMPT.SUCCESS:
			if (Text == "读频" || Text == "Read")
			{
				appData = DPT.bufForData;
				base.DialogResult = DialogResult.OK;
				break;
			}
			if (language == "Chinese")
			{
				lab_progress.Text = "成功!";
			}
			else
			{
				lab_progress.Text = "Success!";
			}
			progressBar.Value = 100;
			if (Text == "写频" || Text == "Write")
			{
				if (language == "Chinese")
				{
					MessageBox.Show("写频成功", "提示", MessageBoxButtons.OK);
				}
				else
				{
					MessageBox.Show("Success", "Tip", MessageBoxButtons.OK);
				}
			}
			else if (language == "Chinese")
			{
				MessageBox.Show("加载成功", "提示", MessageBoxButtons.OK);
			}
			else
			{
				MessageBox.Show("Success", "Tip", MessageBoxButtons.OK);
			}
			break;
		case PROMPT.ILLEGAL_DATA:
			if (language == "Chinese")
			{
				lab_progress.Text = "无效数据!";
			}
			else
			{
				lab_progress.Text = "Illeage Data!";
			}
			break;
		case PROMPT.PWD_ERR:
			if (language == "Chinese")
			{
				lab_progress.Text = "密码错误!";
			}
			else
			{
				lab_progress.Text = "Password Err!";
			}
			break;
		case PROMPT.FREQBAND_ERR:
			if (language == "Chinese")
			{
				lab_progress.Text = "频段信息错误!";
			}
			else
			{
				lab_progress.Text = "Frequency Range Err!";
			}
			break;
		case PROMPT.FAIL:
			if (language == "Chinese")
			{
				lab_progress.Text = "失败!";
			}
			else
			{
				lab_progress.Text = "Failure!";
			}
			break;
		}
		threadCommunication.Abort();
		threadProgress.Abort();
		CloseComPort(sP);
		pB_btnCancel.Enabled = true;
		pB_btnStart.Enabled = true;
		DispControl(0);
	}

	private void updateProgress(string text, int value)
	{
		lab_progress.Text = text;
		progressBar.Value = value;
	}

	private void tB_Pwd_TextChanged(object sender, EventArgs e)
	{
		Pwd = tBox_Pwd.Text;
	}

	private void tB_Pwd_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar < '\u007f')
		{
			e.Handled = false;
		}
		else
		{
			e.Handled = true;
		}
	}

	private void FormProgressBar_Load(object sender, EventArgs e)
	{
		string[] str = SerialPort.GetPortNames();
		if (str.Length > 0)
		{
			cbB_Port.Items.Clear();
			cbB_Port.Items.AddRange(str);
			if (portName != null || portName != "")
			{
				int index = cbB_Port.Items.IndexOf(portName);
				if (index != -1)
				{
					cbB_Port.SelectedIndex = index;
				}
				else
				{
					cbB_Port.SelectedIndex = 0;
				}
			}
			else
			{
				cbB_Port.SelectedIndex = str.Length - 1;
			}
		}
		if (Text == "读频" || Text == "Read")
		{
			label2.Visible = false;
			tBox_NewPwd.Visible = false;
			ckBox_SetPwd.Visible = false;
		}
		else
		{
			label2.Visible = true;
			tBox_NewPwd.Visible = true;
			ckBox_SetPwd.Visible = true;
			tBox_NewPwd.Enabled = false;
		}
		if (Text == "开机图片" || Text == "Boot Picture")
		{
			txSend.Visible = true;
			btn_LoadPic.Visible = true;
		}
		else
		{
			txSend.Visible = false;
			btn_LoadPic.Visible = false;
		}
		int indexBaute = cbB_Baute.Items.IndexOf(Settings.Baudrate.ToString());
		if (indexBaute != -1)
		{
			cbB_Baute.SelectedIndex = indexBaute;
		}
		else
		{
			cbB_Baute.SelectedIndex = 0;
		}
	}

	private void ckBox_SetPwd_CheckedChanged(object sender, EventArgs e)
	{
		if (ckBox_SetPwd.Checked)
		{
			FlgNew = true;
			tBox_NewPwd.Enabled = true;
		}
		else
		{
			FlgNew = false;
			tBox_NewPwd.Enabled = false;
		}
	}

	private void tBox_NewPwd_TextChanged(object sender, EventArgs e)
	{
		NewPwd = tBox_NewPwd.Text;
	}

	private void cbB_Baute_SelectedIndexChanged(object sender, EventArgs e)
	{
		switch (cbB_Baute.SelectedIndex)
		{
		case 1:
			Baudrate = 57600;
			break;
		case 2:
			Baudrate = 38400;
			break;
		case 3:
			Baudrate = 19200;
			break;
		case 4:
			Baudrate = 9600;
			break;
		default:
			Baudrate = 115200;
			break;
		}
		Settings.Baudrate = Baudrate;
		Settings.Save();
	}

	private void cbB_Port_SelectedIndexChanged(object sender, EventArgs e)
	{
		portName = cbB_Port.Text;
		FormMain.portName = portName;
		Settings.PortName = portName;
		Settings.Save();
	}

	private void btn_LoadPic_Click(object sender, EventArgs e)
	{
		string file_types = "文件(*.bmp)|*.BMP;||";
		OpenFileDialog dlg = new OpenFileDialog();
		dlg.Filter = file_types;
		dlg.Multiselect = false;
		if (dlg.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		strFile = dlg.FileName;
		if (!File.Exists(strFile))
		{
			MessageBox.Show("文件不存在");
			return;
		}
		try
		{
			if (Convert24To16Bit(strFile))
			{
				txSend.Text = strFile;
				return;
			}
			txSend.Text = "";
			MessageBox.Show("图片位深度或尺寸不匹配!");
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private ushort ReversalHighLowByte(ushort val)
	{
		byte[] arrSrc = BitConverter.GetBytes(val);
		byte[] arrDst = new byte[arrSrc.Length];
		arrDst[0] = arrSrc[1];
		arrDst[1] = arrSrc[0];
		return BitConverter.ToUInt16(arrDst, 0);
	}

	private unsafe bool Convert24To16Bit(string inputPath)
	{
		Bitmap source = new Bitmap(inputPath);
		if ((source.Height != 128 || source.Width != 160) && (source.Height != 240 || source.Width != 240) && (source.Height != 320 || source.Width != 240))
		{
			return false;
		}
		if (source.PixelFormat == PixelFormat.Format24bppRgb)
		{
			Bitmap destination = new Bitmap(source.Width, source.Height, PixelFormat.Format16bppRgb565);
			BitmapData sourceData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
			BitmapData destinationData = destination.LockBits(new Rectangle(0, 0, destination.Width, destination.Height), ImageLockMode.WriteOnly, PixelFormat.Format16bppRgb565);
			try
			{
				byte* sourcePtr = (byte*)(void*)sourceData.Scan0;
				ushort* destinationPtr = (ushort*)(void*)destinationData.Scan0;
				for (int y = 0; y < sourceData.Height; y++)
				{
					for (int x = 0; x < sourceData.Width; x++)
					{
						ushort pixel565 = ReversalHighLowByte((ushort)((*(sourcePtr++) >> 3) | (*(sourcePtr++) >> 2 << 5) | (*(sourcePtr++) >> 3 << 11)));
						*(destinationPtr++) = pixel565;
					}
					sourcePtr += sourceData.Stride - sourceData.Width * 3;
					destinationPtr += destinationData.Stride / 2 - destinationData.Width;
				}
			}
			finally
			{
				source.UnlockBits(sourceData);
				destination.UnlockBits(destinationData);
			}
			byte[] bmp24rgb = ConvertBmpToData(destination);
			BmpPicLen = bmp24rgb.Length;
			Array.Copy(bmp24rgb, 0, BmpPicData, 0, BmpPicLen);
			return true;
		}
		if (source.PixelFormat == PixelFormat.Format16bppRgb565)
		{
			byte[] bmp16rgb = ConvertBmpToData(source);
			BmpPicLen = bmp16rgb.Length;
			Array.Copy(bmp16rgb, 0, BmpPicData, 0, BmpPicLen);
			return true;
		}
		return false;
	}

	private byte[] ConvertBmpToData(Bitmap bmp)
	{
		int dataSize = bmp.Width * bmp.Height * 2;
		byte[] data = new byte[dataSize];
		BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format16bppRgb565);
		try
		{
			Marshal.Copy(bmpData.Scan0, data, 0, dataSize);
		}
		finally
		{
			bmp.UnlockBits(bmpData);
		}
		return data;
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
		this.progressBar = new System.Windows.Forms.ProgressBar();
		this.pB_btnStart = new System.Windows.Forms.Button();
		this.pB_btnCancel = new System.Windows.Forms.Button();
		this.lab_progress = new System.Windows.Forms.Label();
		this.cbB_Port = new System.Windows.Forms.ComboBox();
		this.lbl_Port = new System.Windows.Forms.Label();
		this.lbl_Baute = new System.Windows.Forms.Label();
		this.cbB_Baute = new System.Windows.Forms.ComboBox();
		this.txSend = new System.Windows.Forms.TextBox();
		this.btn_LoadPic = new System.Windows.Forms.Button();
		this.ckBox_SetPwd = new System.Windows.Forms.CheckBox();
		this.tBox_NewPwd = new System.Windows.Forms.TextBox();
		this.tBox_Pwd = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.progressBar.BackColor = System.Drawing.SystemColors.Control;
		this.progressBar.Location = new System.Drawing.Point(22, 90);
		this.progressBar.Margin = new System.Windows.Forms.Padding(2);
		this.progressBar.Name = "progressBar";
		this.progressBar.Size = new System.Drawing.Size(338, 25);
		this.progressBar.TabIndex = 0;
		this.pB_btnStart.AutoSize = true;
		this.pB_btnStart.Location = new System.Drawing.Point(87, 133);
		this.pB_btnStart.Margin = new System.Windows.Forms.Padding(2);
		this.pB_btnStart.Name = "pB_btnStart";
		this.pB_btnStart.Size = new System.Drawing.Size(67, 22);
		this.pB_btnStart.TabIndex = 1;
		this.pB_btnStart.Text = "开始";
		this.pB_btnStart.UseVisualStyleBackColor = true;
		this.pB_btnStart.Click += new System.EventHandler(pB_btnStart_Click);
		this.pB_btnCancel.AutoSize = true;
		this.pB_btnCancel.Location = new System.Drawing.Point(226, 133);
		this.pB_btnCancel.Margin = new System.Windows.Forms.Padding(2);
		this.pB_btnCancel.Name = "pB_btnCancel";
		this.pB_btnCancel.Size = new System.Drawing.Size(67, 22);
		this.pB_btnCancel.TabIndex = 2;
		this.pB_btnCancel.Text = "取消";
		this.pB_btnCancel.UseVisualStyleBackColor = true;
		this.pB_btnCancel.Click += new System.EventHandler(pB_btnCancel_Click);
		this.lab_progress.Location = new System.Drawing.Point(24, 68);
		this.lab_progress.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.lab_progress.Name = "lab_progress";
		this.lab_progress.Size = new System.Drawing.Size(148, 12);
		this.lab_progress.TabIndex = 3;
		this.lab_progress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.cbB_Port.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbB_Port.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cbB_Port.FormattingEnabled = true;
		this.cbB_Port.Location = new System.Drawing.Point(76, 9);
		this.cbB_Port.Name = "cbB_Port";
		this.cbB_Port.Size = new System.Drawing.Size(78, 20);
		this.cbB_Port.TabIndex = 9;
		this.cbB_Port.SelectedIndexChanged += new System.EventHandler(cbB_Port_SelectedIndexChanged);
		this.lbl_Port.Location = new System.Drawing.Point(24, 8);
		this.lbl_Port.Name = "lbl_Port";
		this.lbl_Port.Size = new System.Drawing.Size(50, 20);
		this.lbl_Port.TabIndex = 10;
		this.lbl_Port.Text = "端口:";
		this.lbl_Port.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl_Baute.Location = new System.Drawing.Point(200, 7);
		this.lbl_Baute.Name = "lbl_Baute";
		this.lbl_Baute.Size = new System.Drawing.Size(50, 20);
		this.lbl_Baute.TabIndex = 12;
		this.lbl_Baute.Text = "波特率:";
		this.lbl_Baute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl_Baute.Visible = false;
		this.cbB_Baute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbB_Baute.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cbB_Baute.FormattingEnabled = true;
		this.cbB_Baute.Items.AddRange(new object[5] { "115200", "57600", "38400", "19200", "9600" });
		this.cbB_Baute.Location = new System.Drawing.Point(252, 8);
		this.cbB_Baute.Name = "cbB_Baute";
		this.cbB_Baute.Size = new System.Drawing.Size(78, 20);
		this.cbB_Baute.TabIndex = 11;
		this.cbB_Baute.Visible = false;
		this.cbB_Baute.SelectedIndexChanged += new System.EventHandler(cbB_Baute_SelectedIndexChanged);
		this.txSend.Location = new System.Drawing.Point(22, 37);
		this.txSend.Name = "txSend";
		this.txSend.ReadOnly = true;
		this.txSend.Size = new System.Drawing.Size(279, 21);
		this.txSend.TabIndex = 13;
		this.btn_LoadPic.AutoSize = true;
		this.btn_LoadPic.Location = new System.Drawing.Point(306, 37);
		this.btn_LoadPic.Margin = new System.Windows.Forms.Padding(2);
		this.btn_LoadPic.Name = "btn_LoadPic";
		this.btn_LoadPic.Size = new System.Drawing.Size(67, 22);
		this.btn_LoadPic.TabIndex = 14;
		this.btn_LoadPic.Text = "加载文件";
		this.btn_LoadPic.UseVisualStyleBackColor = true;
		this.btn_LoadPic.Click += new System.EventHandler(btn_LoadPic_Click);
		this.ckBox_SetPwd.AutoSize = true;
		this.ckBox_SetPwd.Location = new System.Drawing.Point(256, 202);
		this.ckBox_SetPwd.Name = "ckBox_SetPwd";
		this.ckBox_SetPwd.Size = new System.Drawing.Size(48, 16);
		this.ckBox_SetPwd.TabIndex = 8;
		this.ckBox_SetPwd.Text = "写入";
		this.ckBox_SetPwd.UseVisualStyleBackColor = true;
		this.ckBox_SetPwd.Visible = false;
		this.ckBox_SetPwd.CheckedChanged += new System.EventHandler(ckBox_SetPwd_CheckedChanged);
		this.tBox_NewPwd.Location = new System.Drawing.Point(140, 197);
		this.tBox_NewPwd.MaxLength = 8;
		this.tBox_NewPwd.Name = "tBox_NewPwd";
		this.tBox_NewPwd.Size = new System.Drawing.Size(110, 21);
		this.tBox_NewPwd.TabIndex = 7;
		this.tBox_NewPwd.UseSystemPasswordChar = true;
		this.tBox_NewPwd.Visible = false;
		this.tBox_NewPwd.TextChanged += new System.EventHandler(tBox_NewPwd_TextChanged);
		this.tBox_NewPwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tB_Pwd_KeyPress);
		this.tBox_Pwd.Location = new System.Drawing.Point(140, 175);
		this.tBox_Pwd.MaxLength = 8;
		this.tBox_Pwd.Name = "tBox_Pwd";
		this.tBox_Pwd.Size = new System.Drawing.Size(110, 21);
		this.tBox_Pwd.TabIndex = 5;
		this.tBox_Pwd.UseSystemPasswordChar = true;
		this.tBox_Pwd.Visible = false;
		this.tBox_Pwd.TextChanged += new System.EventHandler(tB_Pwd_TextChanged);
		this.tBox_Pwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tB_Pwd_KeyPress);
		this.label2.Location = new System.Drawing.Point(35, 200);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(100, 18);
		this.label2.TabIndex = 6;
		this.label2.Text = "新密码:";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label2.Visible = false;
		this.label1.Location = new System.Drawing.Point(35, 182);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(100, 18);
		this.label1.TabIndex = 4;
		this.label1.Text = "密码:";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label1.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(384, 166);
		base.Controls.Add(this.btn_LoadPic);
		base.Controls.Add(this.txSend);
		base.Controls.Add(this.lbl_Baute);
		base.Controls.Add(this.cbB_Baute);
		base.Controls.Add(this.lbl_Port);
		base.Controls.Add(this.cbB_Port);
		base.Controls.Add(this.ckBox_SetPwd);
		base.Controls.Add(this.tBox_NewPwd);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.tBox_Pwd);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.lab_progress);
		base.Controls.Add(this.pB_btnCancel);
		base.Controls.Add(this.pB_btnStart);
		base.Controls.Add(this.progressBar);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.Margin = new System.Windows.Forms.Padding(2);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "FormProgressBar";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "进度";
		base.Load += new System.EventHandler(FormProgressBar_Load);
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormProgressBar_FormClosing);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
