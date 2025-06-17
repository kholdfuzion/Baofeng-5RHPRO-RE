using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CPS_5RH.Data;
using Microsoft.Win32;

namespace CPS_5RH;

public class FormMain : Form
{
	public const byte VER_FACTORY = 0;

	public const byte VER_CUSTOMER = 1;

	public const byte POW_HIGH = 2;

	public const byte POW_NO_MID = 1;

	private const string STR_5R = "5RH Pro";

	private const string STR_K6 = "K6";

	private const string STR_UV39 = "UV39";

	public static bool ReadFlg = false;

	public static string lang = null;

	public static string portName = null;

	public static byte FreqRange = 0;

	private byte[] NodeIdx = new byte[12];

	private byte[] DevArray = new byte[16];

	private DataApp dataApplication = new DataApp();

	private static Frm_Devinfo frmDevInfo = null;

	private static Frm_GenralSet frmGSet = null;

	public static Frm_ZoneChn frmZoneChn = null;

	public static Frm_Chn frmChn = null;

	public static Frm_ZoneConfig frmZone = null;

	public static Frm_VFO frmVFO = null;

	private Frm_MdcSys frmMdcSys = null;

	private Frm_MDC_Para frmMdcPara = null;

	private Frm_MDC_PTT frmMdcPtt = null;

	private Frm_MDC_Dec frmMdcDec = null;

	private static Frm_MDC_BIIS frmMdcBiis = null;

	public static Frm_Scan frmScan = null;

	private static Frm_Dtmf frmDtmf = null;

	private static Frm_2Tone frmTwoTone = null;

	private static Frm_5Tone_Enc frm5ToneEnc = null;

	private static Frm_5Tone_Dec frm5ToneDec = null;

	private static Frm_5Tone_Infocode frm5ToneCodeInfo = null;

	private static Frm_Emerg frmEmerg = null;

	private static Frm_Aprs frmAprs = null;

	private static Frm_GpsBook frmGpsBook = null;

	public static byte VER_TYPE = 0;

	public static byte HIGH_VALID = 1;

	private static string DeviceName = "5RH Pro";

	private string filePath = null;

	private IContainer components = null;

	private System.Windows.Forms.Timer timer1;

	private MenuStrip menuStrip1;

	private ToolStrip toolStrip1;

	private ToolStripMenuItem mS_File;

	private ToolStripMenuItem mS_Program;

	private ToolStripMenuItem mS_Language;

	private ToolStripMenuItem mS_View;

	private ToolStripMenuItem mS_Window;

	private ToolStripMenuItem mS_Help;

	private ToolStripMenuItem mS_View_ToolBar;

	private ToolStripMenuItem mS_View_StatusBar;

	private ToolStripMenuItem mS_View_Tree;

	private TreeView TreeShow;

	private Splitter splitter1;

	private ToolStripMenuItem mS_Window_Cendie;

	private ToolStripMenuItem mS_Window_Pinpu;

	private ToolStripMenuItem mS_File_New;

	private ToolStripMenuItem mS_File_Open;

	private ToolStripMenuItem mS_File_Save;

	private ToolStripMenuItem mS_File_SaveAs;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripMenuItem mS_File_Exit;

	private ToolStripMenuItem mS_Program_Read;

	private ToolStripMenuItem mS_Program_Write;

	private ToolStripMenuItem mS_Program_Password;

	private ToolStripMenuItem mS_Language_cn;

	private ToolStripMenuItem mS_Language_en;

	private ToolStripMenuItem mS_Help_File;

	private ToolStripMenuItem mS_Help_About;

	private ToolStripButton tsBtn_New;

	private ToolStripButton tsBtn_Open;

	private ToolStripButton tsBtn_Save;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripButton tsBtn_Read;

	private ToolStripButton tsBtn_Write;

	private ToolStripButton tsBtn_Com;

	private ImageList imageList1;

	private StatusStrip statusStrip1;

	private ToolStripMenuItem mS_Program_FreqBand;

	private ToolStripMenuItem mS_Program_Embed;

	private ToolStripMenuItem mS_Tools;

	private ToolStripMenuItem mS_Tool_Logo;

	[DllImport("kernel32")]
	private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

	[DllImport("kernel32")]
	private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

	public FormMain()
	{
		PlatformID OSVersionPlateform = Environment.OSVersion.Platform;
		int OSVersionMajor = Environment.OSVersion.Version.Major;
		int OSVersionMinor = Environment.OSVersion.Version.Minor;
		string appPath = Environment.CurrentDirectory + "\\CPS.exe";
		if (OSVersionPlateform == PlatformID.Win32NT && OSVersionMinor == 1 && (OSVersionMajor == 5 || OSVersionMajor == 6))
		{
			RegistryKey key = Registry.CurrentUser.OpenSubKey("SoftWare\\Microsoft\\Windows NT\\CurrentVersion\\AppCompatFlags\\Layers", writable: true);
			if (key == null)
			{
				key = Registry.LocalMachine.CreateSubKey("SoftWare\\Microsoft\\Windows NT\\CurrentVersion\\AppCompatFlags\\Layers");
			}
			if (key.GetValue(appPath) == null)
			{
				key.SetValue(appPath, "WIN2000");
				Application.ExitThread();
				Application.Exit();
				Application.Restart();
				Process.GetCurrentProcess().Kill();
			}
		}
		InitializeComponent();
	}

	private void SetDeviceFunction()
	{
		if (VER_TYPE == 0)
		{
			if (DeviceName == "K6")
			{
				dataApplication.dataDevice.En2Tone = 1;
				dataApplication.dataDevice.En5Tone = 1;
				dataApplication.dataDevice.EnDTMF = 1;
				dataApplication.dataDevice.EnMdc1200 = 1;
				dataApplication.dataDevice.EnBdc1200 = 1;
				dataApplication.dataDevice.EnGps = 0;
				dataApplication.dataDevice.EnBT = 1;
				dataApplication.dataDevice.EnFalldn = 1;
				dataApplication.dataDevice.EnNoise = 0;
				dataApplication.dataDevice.EnFlight = 0;
				dataApplication.dataDevice.EnZone = 0;
				dataApplication.dataDevice.PkeyCnt = 1;
				dataApplication.dataDevice.Dis200m = 1;
				dataApplication.dataDevice.Dis350m = 1;
				dataApplication.dataDevice.Dis500m = 1;
				DataProtocol.flagK6 = true;
			}
			else
			{
				dataApplication.dataDevice.En2Tone = 1;
				dataApplication.dataDevice.En5Tone = 1;
				dataApplication.dataDevice.EnDTMF = 1;
				dataApplication.dataDevice.EnMdc1200 = 1;
				dataApplication.dataDevice.EnBdc1200 = 1;
				dataApplication.dataDevice.EnGps = 1;
				dataApplication.dataDevice.EnBT = 1;
				dataApplication.dataDevice.EnFalldn = 1;
				dataApplication.dataDevice.EnNoise = 1;
				dataApplication.dataDevice.EnFlight = 1;
				dataApplication.dataDevice.EnZone = 1;
				dataApplication.dataDevice.PkeyCnt = 1;
				dataApplication.dataDevice.Dis200m = 1;
				dataApplication.dataDevice.Dis350m = 1;
				dataApplication.dataDevice.Dis500m = 1;
				DataProtocol.flagK6 = false;
			}
		}
		else if (DeviceName == "K6")
		{
			dataApplication.dataDevice.En2Tone = 1;
			dataApplication.dataDevice.En5Tone = 1;
			dataApplication.dataDevice.EnDTMF = 1;
			dataApplication.dataDevice.EnMdc1200 = 1;
			dataApplication.dataDevice.EnBdc1200 = 0;
			dataApplication.dataDevice.EnGps = 0;
			dataApplication.dataDevice.EnBT = 1;
			dataApplication.dataDevice.EnFalldn = 1;
			dataApplication.dataDevice.EnNoise = 0;
			dataApplication.dataDevice.EnFlight = 0;
			dataApplication.dataDevice.EnZone = 0;
			dataApplication.dataDevice.PkeyCnt = 1;
			dataApplication.dataDevice.Dis200m = 1;
			dataApplication.dataDevice.Dis350m = 1;
			dataApplication.dataDevice.Dis500m = 1;
			DataProtocol.flagK6 = true;
		}
		else
		{
			dataApplication.dataDevice.En2Tone = 1;
			dataApplication.dataDevice.En5Tone = 1;
			dataApplication.dataDevice.EnDTMF = 1;
			dataApplication.dataDevice.EnMdc1200 = 1;
			dataApplication.dataDevice.EnBdc1200 = 0;
			dataApplication.dataDevice.EnGps = 1;
			dataApplication.dataDevice.EnBT = 1;
			dataApplication.dataDevice.EnFalldn = 1;
			dataApplication.dataDevice.EnNoise = 0;
			dataApplication.dataDevice.EnFlight = 0;
			dataApplication.dataDevice.EnZone = 1;
			dataApplication.dataDevice.PkeyCnt = 1;
			dataApplication.dataDevice.Dis200m = 1;
			dataApplication.dataDevice.Dis350m = 1;
			dataApplication.dataDevice.Dis500m = 1;
			DataProtocol.flagK6 = false;
		}
	}

	private void FormMain_Load(object sender, EventArgs e)
	{
		LoadSettingFile();
		Text = DeviceName;
		if (lang == "Chinese")
		{
			mS_Language_cn.Checked = true;
		}
		else
		{
			mS_Language_en.Checked = true;
		}
		mS_View_ToolBar.Checked = true;
		mS_View_StatusBar.Checked = true;
		mS_View_Tree.Checked = true;
		DataProtocol.InitbufFullData();
		SetDeviceFunction();
		TreeViewShow_Init();
		SetFreqBandRange();
		changeFormLanguage();
		CreateProKeyItems();
		frmDevInfo = new Frm_Devinfo(dataApplication);
		frmDevInfo.TopLevel = false;
		frmDevInfo.Text = TreeShow.Nodes[0].Nodes[0].Text;
		frmDevInfo.FormBorderStyle = FormBorderStyle.Sizable;
		frmDevInfo.MdiParent = this;
		frmDevInfo.Location = new Point(0, 0);
		frmDevInfo.Show();
		frmZone = Frm_ZoneConfig.getInstance(this, dataApplication);
		frmZoneChn = Frm_ZoneChn.getInstance(this, dataApplication);
		frmVFO = Frm_VFO.getInstance(this, dataApplication);
		frmScan = Frm_Scan.getInstance(this, dataApplication);
		frmGpsBook = Frm_GpsBook.getInstance(this, dataApplication);
		CreateChnTbl();
		CreateZoneTbl(1);
		CreateVFOTbl();
		CreateGpsBook();
		frmDevInfo.DeviceDataDisp();
	}

	public void SetNodeZoneConfigName(string name, int lv, int pos)
	{
		string nodeTxt = "";
		if (lv == 2)
		{
			TreeShow.SelectedNode = TreeShow.Nodes[0].Nodes[2].Nodes[pos];
			TreeShow.SelectedNode.ImageIndex = 1;
			TreeShow.SelectedNode.SelectedImageIndex = 1;
			nodeTxt = pos + ": " + name;
			TreeShow.Nodes[0].Nodes[2].Nodes[pos].Text = nodeTxt;
		}
	}

	private void AddNode(string name, string node, int lv, int of1, int of2, int picIdx)
	{
		TreeNode Node = new TreeNode(name);
		switch (lv)
		{
		case 0:
			TreeShow.Nodes.Add(Node);
			TreeShow.Nodes[of1].ImageIndex = picIdx;
			TreeShow.Nodes[of1].SelectedImageIndex = picIdx;
			break;
		case 1:
			TreeShow.Nodes[of1].Nodes.Add(Node);
			TreeShow.Nodes[of1].Nodes[TreeShow.Nodes[of1].Nodes.Count - 1].ImageIndex = picIdx;
			TreeShow.Nodes[of1].Nodes[TreeShow.Nodes[of1].Nodes.Count - 1].SelectedImageIndex = picIdx;
			TreeShow.Nodes[of1].Nodes[TreeShow.Nodes[of1].Nodes.Count - 1].Name = node;
			break;
		case 2:
			TreeShow.Nodes[0].Nodes[of1].Nodes.Add(Node);
			TreeShow.Nodes[0].Nodes[of1].Nodes[of2].ImageIndex = picIdx;
			TreeShow.Nodes[0].Nodes[of1].Nodes[of2].SelectedImageIndex = picIdx;
			TreeShow.Nodes[0].Nodes[of1].Nodes[of2].Name = node;
			break;
		}
	}

	private void AddNode(string name, string node, int of1, int of2, int of3)
	{
		TreeNode Node = new TreeNode(name);
		TreeShow.Nodes[0].Nodes[of1].Nodes[of2].Nodes.Add(Node);
		TreeShow.Nodes[0].Nodes[of1].Nodes[of2].Nodes[of3].Name = node;
	}

	private void TreeViewShow_Init()
	{
		byte node = 0;
		TreeShow.Nodes.Clear();
		TreeShow.ItemHeight = 22;
		TreeShow.Indent = 18;
		AddNode("CPS", "NodeRoot", 0, 0, 0, 19);
		AddNode("设备信息", "NodeInfo", 1, 0, 0, 2);
		NodeIdx[0] = node;
		node++;
		AddNode("基本设置", "NodeBset", 1, 0, 0, 2);
		NodeIdx[1] = node;
		node++;
		AddNode("存储信道", "NodeMemoryCH", 1, 0, 0, 2);
		NodeIdx[2] = node;
		node++;
		AddNode("VFO参数", "NodeVfo", 1, 0, 0, 2);
		NodeIdx[3] = node;
		node++;
		AddNode("扫描列表", "NodeScan", 1, 0, 0, 2);
		NodeIdx[4] = node;
		node++;
		if (dataApplication.dataDevice.EnDTMF == 1)
		{
			AddNode("DTMF列表", "NodeDtmf", 1, 0, 0, 2);
			NodeIdx[5] = node;
			node++;
		}
		if (dataApplication.dataDevice.En2Tone == 1)
		{
			AddNode("2-Tone", "Node2Tone", 1, 0, 0, 2);
			NodeIdx[6] = node;
			node++;
		}
		if (dataApplication.dataDevice.En5Tone == 1)
		{
			AddNode("5-Tone", "Node5Tone", 1, 0, 0, 2);
			NodeIdx[7] = node;
			node++;
		}
		if (dataApplication.dataDevice.EnMdc1200 == 1)
		{
			AddNode("MDC", "NodeMdc", 1, 0, 0, 2);
			NodeIdx[8] = node;
			node++;
		}
		AddNode("紧急报警", "NodeExg", 1, 0, 0, 2);
		NodeIdx[9] = node;
		node++;
		if (dataApplication.dataDevice.EnGps == 1)
		{
			AddNode("APRS", "NodeAprs", 1, 0, 0, 2);
			NodeIdx[10] = node;
			node++;
		}
		AddNode("常用联系人", "NodeGpsBook", 1, 0, 0, 2);
		NodeIdx[11] = node;
		node++;
		if (dataApplication.dataDevice.EnZone == 1)
		{
			AddNode("区域配置", "NodeZoneConfig", 2, 2, 0, 1);
		}
		AddNode("VFO A", "NodeVFOA", 2, 3, 0, 16);
		AddNode("VFO B", "NodeVFOB", 2, 3, 1, 16);
		if (dataApplication.dataDevice.EnMdc1200 == 1)
		{
			AddNode("MDC系统", "NodeMdc_Sys", 2, NodeIdx[8], 0, 1);
			AddNode("解码设置", "NodeMdc_Dec", 2, NodeIdx[8], 1, 1);
			AddNode("BIIS码参数", "NodeMdc_Biis", 2, NodeIdx[8], 2, 1);
			AddNode("通用设置表", "NodeMdc_Para", NodeIdx[8], 0, 0);
			AddNode("PTT ID 列表", "NodeMdc_Ptt", NodeIdx[8], 0, 1);
		}
		if (dataApplication.dataDevice.En5Tone == 1)
		{
			AddNode("5音编码", "Node5Tone_Enc", 2, NodeIdx[7], 0, 1);
			AddNode("5音解码", "Node5Tone_Dec", 2, NodeIdx[7], 1, 1);
			AddNode("5音信息码", "Node5Tone_InfoCd", 2, NodeIdx[7], 2, 1);
		}
		TreeShow.ExpandAll();
		if (dataApplication.dataDevice.EnZone == 1)
		{
			DataApp.Zone_Max_Chn_Num = 64;
			DataApp.Zone_Max_Num = 10;
			DataApp.Zone_Size = 152;
		}
		else
		{
			DataApp.Zone_Max_Chn_Num = 640;
			DataApp.Zone_Max_Num = 1;
			DataApp.Zone_Size = 1304;
		}
	}

	private void LoadSettingFile()
	{
		try
		{
			portName = Settings.PortName;
			Language_Setting(Settings.LangDef);
			FreqRange = Settings.FreqBand;
		}
		catch
		{
		}
	}

	private void changeFormLanguage()
	{
		if (lang == "Chinese")
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
		}
		ComponentResourceManager crm = new ComponentResourceManager(typeof(FormMain));
		foreach (ToolStripMenuItem c in menuStrip1.Items)
		{
			crm.ApplyResources(c, c.Name);
		}
		for (int i = 0; i < mS_File.DropDownItems.Count; i++)
		{
			if (mS_File.DropDownItems[i] is ToolStripMenuItem)
			{
				ToolStripMenuItem c = mS_File.DropDownItems[i] as ToolStripMenuItem;
				crm.ApplyResources(c, c.Name);
			}
		}
		foreach (ToolStripMenuItem c in mS_Language.DropDownItems)
		{
			crm.ApplyResources(c, c.Name);
		}
		foreach (ToolStripMenuItem c in mS_Program.DropDownItems)
		{
			crm.ApplyResources(c, c.Name);
		}
		foreach (ToolStripMenuItem c in mS_Window.DropDownItems)
		{
			crm.ApplyResources(c, c.Name);
		}
		foreach (ToolStripMenuItem c in mS_Help.DropDownItems)
		{
			crm.ApplyResources(c, c.Name);
		}
		foreach (ToolStripMenuItem c in mS_View.DropDownItems)
		{
			crm.ApplyResources(c, c.Name);
		}
		foreach (ToolStripMenuItem c in mS_Tools.DropDownItems)
		{
			crm.ApplyResources(c, c.Name);
		}
		foreach (ToolStripItem c2 in toolStrip1.Items)
		{
			if (c2.GetType().ToString() == "System.Windows.Forms.ToolStripButton")
			{
				crm.ApplyResources(c2, c2.Name);
			}
		}
		foreach (TreeNode topItem in TreeShow.Nodes)
		{
			foreach (TreeNode item in TreeShow.Nodes[0].Nodes)
			{
				crm.ApplyResources(item, item.Name);
			}
			foreach (TreeNode itemTwo in TreeShow.Nodes[0].Nodes[2].Nodes)
			{
				crm.ApplyResources(itemTwo, itemTwo.Name);
			}
			if (dataApplication.dataDevice.En5Tone == 1)
			{
				foreach (TreeNode itemTwo in TreeShow.Nodes[0].Nodes[NodeIdx[7]].Nodes)
				{
					crm.ApplyResources(itemTwo, itemTwo.Name);
				}
			}
			if (dataApplication.dataDevice.EnMdc1200 != 1)
			{
				continue;
			}
			foreach (TreeNode itemTwo in TreeShow.Nodes[0].Nodes[NodeIdx[8]].Nodes)
			{
				crm.ApplyResources(itemTwo, itemTwo.Name);
			}
			foreach (TreeNode itemTwo in TreeShow.Nodes[0].Nodes[NodeIdx[8]].Nodes[0].Nodes)
			{
				crm.ApplyResources(itemTwo, itemTwo.Name);
			}
		}
	}

	private void DispFrmDevInfo(string fname)
	{
		if (frmDevInfo.IsDisposed)
		{
			frmDevInfo = new Frm_Devinfo(dataApplication);
			frmDevInfo.TopLevel = false;
			frmDevInfo.Text = fname;
			frmDevInfo.FormBorderStyle = FormBorderStyle.Sizable;
			frmDevInfo.MdiParent = this;
			frmDevInfo.Location = new Point(0, 0);
			frmDevInfo.Show();
		}
		frmDevInfo.DeviceDataDisp();
		frmDevInfo.BringToFront();
	}

	private void DispFrmGset(string fname)
	{
		if (frmGSet == null || frmGSet.IsDisposed)
		{
			frmGSet = new Frm_GenralSet(dataApplication);
			frmGSet.TopLevel = false;
			frmGSet.Text = fname;
			frmGSet.FormBorderStyle = FormBorderStyle.Sizable;
			frmGSet.MdiParent = this;
			frmGSet.Location = new Point(0, 0);
			frmGSet.Show();
		}
		frmGSet.BringToFront();
	}

	private void DispFrmMdcSys(string fname)
	{
		if (frmMdcSys == null || frmMdcSys.IsDisposed)
		{
			frmMdcSys = new Frm_MdcSys(dataApplication);
			frmMdcSys.TopLevel = false;
			frmMdcSys.Text = fname;
			frmMdcSys.FormBorderStyle = FormBorderStyle.Sizable;
			frmMdcSys.MdiParent = this;
			frmMdcSys.Location = new Point(0, 0);
			frmMdcSys.Show();
			frmMdcSys.MdcSysDisp();
		}
		frmMdcSys.BringToFront();
	}

	private void DispFrmMdcPara(string fname)
	{
		if (frmMdcPara == null || frmMdcPara.IsDisposed)
		{
			frmMdcPara = new Frm_MDC_Para(dataApplication);
			frmMdcPara.TopLevel = false;
			frmMdcPara.Text = fname;
			frmMdcPara.FormBorderStyle = FormBorderStyle.Sizable;
			frmMdcPara.MdiParent = this;
			frmMdcPara.Location = new Point(0, 0);
			frmMdcPara.Show();
			frmMdcPara.MdcParaDataDisp(0);
		}
		frmMdcPara.BringToFront();
	}

	private void DispFrmMdcDec(string fname)
	{
		if (frmMdcDec == null || frmMdcDec.IsDisposed)
		{
			frmMdcDec = new Frm_MDC_Dec(dataApplication.dataMdcDecInfo);
			frmMdcDec.TopLevel = false;
			frmMdcDec.Text = fname;
			frmMdcDec.FormBorderStyle = FormBorderStyle.Sizable;
			frmMdcDec.MdiParent = this;
			frmMdcDec.Location = new Point(0, 0);
			frmMdcDec.Show();
		}
		frmMdcDec.BringToFront();
	}

	private void DispFrmZone2(string fname, int pos)
	{
		if (pos >= 0)
		{
			Frm_ZoneChn.SetChnPos(pos);
		}
		else
		{
			Frm_ZoneChn.SetChnPos(0);
		}
		if (!frmZoneChn.Created)
		{
			frmZoneChn = new Frm_ZoneChn(dataApplication);
			frmZoneChn.TopLevel = false;
			frmZoneChn.Text = fname;
			frmZoneChn.FormBorderStyle = FormBorderStyle.Sizable;
			frmZoneChn.MdiParent = this;
			frmZoneChn.Location = new Point(0, 0);
			frmZoneChn.Show();
		}
		else
		{
			frmZoneChn.Text = fname;
			if (pos >= 0)
			{
				Frm_ZoneChn.nodeFlg = true;
				frmZoneChn.ZoneChnDataDisp(pos);
			}
			else
			{
				frmZoneChn.ZoneChnDataDisp(0);
			}
		}
		frmZoneChn.BringToFront();
	}

	private void DispFrmVFO(string fname, int pos)
	{
		Frm_VFO.SetNodePos(pos);
		if (!frmVFO.Created)
		{
			frmVFO = new Frm_VFO(dataApplication);
			frmVFO.TopLevel = false;
			frmVFO.Text = fname;
			frmVFO.FormBorderStyle = FormBorderStyle.Sizable;
			frmVFO.MdiParent = this;
			frmVFO.Location = new Point(0, 0);
			frmVFO.Show();
		}
		else
		{
			frmVFO.VFODataDisp(pos, fname);
		}
		frmVFO.BringToFront();
	}

	private void DispFrmMdcBiis(string fname)
	{
		if (frmMdcBiis == null || frmMdcBiis.IsDisposed)
		{
			frmMdcBiis = new Frm_MDC_BIIS(dataApplication);
			frmMdcBiis.TopLevel = false;
			frmMdcBiis.Text = fname;
			frmMdcBiis.FormBorderStyle = FormBorderStyle.Sizable;
			frmMdcBiis.MdiParent = this;
			frmMdcBiis.Location = new Point(0, 0);
			frmMdcBiis.Show();
		}
		frmMdcBiis.BringToFront();
	}

	private void DispFrmZoneConfig(string fname)
	{
		if (!frmZone.Created)
		{
			frmZone = new Frm_ZoneConfig(dataApplication);
			frmZone.TopLevel = false;
			frmZone.Text = fname;
			frmZone.FormBorderStyle = FormBorderStyle.Sizable;
			frmZone.MdiParent = this;
			frmZone.Location = new Point(0, 0);
			frmZone.Show();
		}
		frmZone.ZoneDataDisp(0);
		frmZone.BringToFront();
	}

	private void DispFrm5ToneEnc(string fname)
	{
		if (frm5ToneEnc == null || !frm5ToneEnc.Created)
		{
			frm5ToneEnc = new Frm_5Tone_Enc(dataApplication);
			frm5ToneEnc.TopLevel = false;
			frm5ToneEnc.Text = fname;
			frm5ToneEnc.FormBorderStyle = FormBorderStyle.Sizable;
			frm5ToneEnc.MdiParent = this;
			frm5ToneEnc.Location = new Point(0, 0);
			frm5ToneEnc.Show();
		}
		frm5ToneEnc.BringToFront();
	}

	private void DispFrmAprs(string fname)
	{
		if (frmAprs == null || frmAprs.IsDisposed)
		{
			frmAprs = new Frm_Aprs(dataApplication);
			frmAprs.TopLevel = false;
			frmAprs.Text = fname;
			frmAprs.FormBorderStyle = FormBorderStyle.Sizable;
			frmAprs.MdiParent = this;
			frmAprs.Location = new Point(0, 0);
			frmAprs.Show();
			frmAprs.PbookDataDisp();
		}
		frmAprs.BringToFront();
	}

	private void DispFrmEmerg(string fname)
	{
		if (frmEmerg == null || frmEmerg.IsDisposed)
		{
			frmEmerg = new Frm_Emerg(dataApplication);
			frmEmerg.TopLevel = false;
			frmEmerg.Text = fname;
			frmEmerg.FormBorderStyle = FormBorderStyle.Sizable;
			frmEmerg.MdiParent = this;
			frmEmerg.Location = new Point(0, 0);
			frmEmerg.Show();
		}
		frmEmerg.BringToFront();
	}

	private void DispFrm2Tone(string fname)
	{
		if (frmTwoTone == null || frmTwoTone.IsDisposed)
		{
			frmTwoTone = new Frm_2Tone(dataApplication);
			frmTwoTone.TopLevel = false;
			frmTwoTone.Text = fname;
			frmTwoTone.FormBorderStyle = FormBorderStyle.Sizable;
			frmTwoTone.MdiParent = this;
			frmTwoTone.Location = new Point(0, 0);
			frmTwoTone.Show();
			frmTwoTone.TwoToneDataDisp();
		}
		frmTwoTone.BringToFront();
	}

	private void DispFrm5ToneDec(string fname)
	{
		if (frm5ToneDec == null || frm5ToneDec.IsDisposed)
		{
			frm5ToneDec = new Frm_5Tone_Dec(dataApplication.dataFiveToneDec, dataApplication.dataFiveToneEnc);
			frm5ToneDec.TopLevel = false;
			frm5ToneDec.Text = fname;
			frm5ToneDec.FormBorderStyle = FormBorderStyle.Sizable;
			frm5ToneDec.MdiParent = this;
			frm5ToneDec.Location = new Point(0, 0);
			frm5ToneDec.Show();
			frm5ToneDec.DispEncryptData();
		}
		frm5ToneDec.BringToFront();
	}

	private void DispFrm5ToneCodeInfo(string fname)
	{
		if (frm5ToneCodeInfo == null || frm5ToneCodeInfo.IsDisposed)
		{
			frm5ToneCodeInfo = new Frm_5Tone_Infocode(dataApplication.dataFiveToneInfoCd);
			frm5ToneCodeInfo.TopLevel = false;
			frm5ToneCodeInfo.Text = fname;
			frm5ToneCodeInfo.FormBorderStyle = FormBorderStyle.Sizable;
			frm5ToneCodeInfo.MdiParent = this;
			frm5ToneCodeInfo.Location = new Point(0, 0);
			frm5ToneCodeInfo.Show();
		}
		frm5ToneCodeInfo.BringToFront();
	}

	private void DispFrmScan(string fname)
	{
		if (!frmScan.Created)
		{
			frmScan = new Frm_Scan(dataApplication);
			frmScan.TopLevel = false;
			frmScan.Text = fname;
			frmScan.FormBorderStyle = FormBorderStyle.Sizable;
			frmScan.MdiParent = this;
			frmScan.Location = new Point(0, 0);
			frmScan.Show();
		}
		frmScan.BringToFront();
	}

	private void DispFrmDtmf(string fname)
	{
		if (frmDtmf == null || frmDtmf.IsDisposed)
		{
			frmDtmf = new Frm_Dtmf(dataApplication);
			frmDtmf.TopLevel = false;
			frmDtmf.Text = fname;
			frmDtmf.FormBorderStyle = FormBorderStyle.Sizable;
			frmDtmf.MdiParent = this;
			frmDtmf.Location = new Point(0, 0);
			frmDtmf.Show();
		}
		frmDtmf.DtmfDataDisp();
		frmDtmf.BringToFront();
	}

	private void DispFrmMdcPtt(string fname)
	{
		if (frmMdcPtt == null || frmMdcPtt.IsDisposed)
		{
			frmMdcPtt = new Frm_MDC_PTT(dataApplication);
			frmMdcPtt.TopLevel = false;
			frmMdcPtt.Text = fname;
			frmMdcPtt.FormBorderStyle = FormBorderStyle.Sizable;
			frmMdcPtt.MdiParent = this;
			frmMdcPtt.Location = new Point(0, 0);
			frmMdcPtt.Show();
			frmMdcPtt.DispMcdPtt(0);
		}
		frmMdcPtt.BringToFront();
	}

	private void DispFrmGpsBook(string fname)
	{
		if (!frmGpsBook.Created)
		{
			frmGpsBook = new Frm_GpsBook(dataApplication);
			frmGpsBook.TopLevel = false;
			frmGpsBook.Text = fname;
			frmGpsBook.FormBorderStyle = FormBorderStyle.Sizable;
			frmGpsBook.MdiParent = this;
			frmGpsBook.Location = new Point(0, 0);
			frmGpsBook.Show();
			frmGpsBook.GpsBookDataDisp();
		}
		if (!frmGpsBook.Visible)
		{
			frmGpsBook.Visible = true;
		}
		frmGpsBook.BringToFront();
	}

	private void TreeShow_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
	{
		string nodename = TreeShow.SelectedNode.Name;
		int treeLevel = TreeShow.SelectedNode.Level;
		if (0 == string.Compare(nodename, "NodeInfo"))
		{
			DispFrmDevInfo(e.Node.Text);
		}
		else if (0 == string.Compare(nodename, "NodeBset"))
		{
			DispFrmGset(e.Node.Text);
		}
		else if (0 == string.Compare(nodename, "NodeScan"))
		{
			DispFrmScan(e.Node.Text);
		}
		else if (0 == string.Compare(nodename, "NodeDtmf"))
		{
			DispFrmDtmf(e.Node.Text);
		}
		else if (0 == string.Compare(nodename, "Node2Tone"))
		{
			DispFrm2Tone(e.Node.Text);
		}
		else if (0 == string.Compare(nodename, "NodeExg"))
		{
			DispFrmEmerg(e.Node.Text);
		}
		else if (0 == string.Compare(nodename, "NodeAprs"))
		{
			DispFrmAprs(e.Node.Text);
		}
		else if (0 == string.Compare(nodename, "NodeGpsBook"))
		{
			DispFrmGpsBook(e.Node.Text);
		}
		else if (0 == string.Compare(nodename, "NodeVFOA"))
		{
			DispFrmVFO(e.Node.Text, TreeShow.SelectedNode.Index);
		}
		else if (0 == string.Compare(nodename, "NodeVFOB"))
		{
			DispFrmVFO(e.Node.Text, TreeShow.SelectedNode.Index);
		}
		else if (0 == string.Compare(nodename, "NodeMdc_Sys"))
		{
			DispFrmMdcSys(e.Node.Text);
		}
		else if (0 == string.Compare(nodename, "NodeMdc_Dec"))
		{
			DispFrmMdcDec(e.Node.Text);
		}
		else if (0 == string.Compare(nodename, "NodeMdc_Biis"))
		{
			DispFrmMdcBiis(e.Node.Text);
		}
		else if (0 == string.Compare(nodename, "NodeMdc_Para"))
		{
			DispFrmMdcPara(e.Node.Text);
		}
		else if (0 == string.Compare(nodename, "NodeMdc_Ptt"))
		{
			DispFrmMdcPtt(e.Node.Text);
		}
		else if (0 == string.Compare(nodename, "Node5Tone_Enc"))
		{
			DispFrm5ToneEnc(e.Node.Text);
		}
		else if (0 == string.Compare(nodename, "Node5Tone_Dec"))
		{
			DispFrm5ToneDec(e.Node.Text);
		}
		else if (0 == string.Compare(nodename, "Node5Tone_InfoCd"))
		{
			DispFrm5ToneCodeInfo(e.Node.Text);
		}
		else if (0 == string.Compare(nodename, "NodeZoneConfig"))
		{
			DispFrmZoneConfig(e.Node.Text);
		}
		if (2 != treeLevel || TreeShow.SelectedNode.Parent.Index != 2)
		{
			return;
		}
		if (dataApplication.dataDevice.EnZone == 1)
		{
			switch (TreeShow.SelectedNode.Index)
			{
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 9:
			case 10:
				DispFrmZone2(e.Node.Text, TreeShow.SelectedNode.Index - 1);
				break;
			}
		}
		else
		{
			switch (TreeShow.SelectedNode.Index)
			{
			case 0:
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 9:
				DispFrmZone2(e.Node.Text, TreeShow.SelectedNode.Index);
				break;
			}
		}
	}

	private void TreeShow_MouseDown(object sender, MouseEventArgs e)
	{
		if (sender is TreeView)
		{
			TreeShow.SelectedNode = TreeShow.GetNodeAt(e.X, e.Y);
		}
	}

	private void SetDeviceFunction(string devstr)
	{
		if (VER_TYPE != 0)
		{
			int idx = devstr.IndexOf("-");
			string TMP = devstr.Substring(idx + 1, 4);
			byte[] dataSignal = Encoding.Unicode.GetBytes(TMP);
			dataApplication.dataDevice.En2Tone = dataSignal[0];
			dataApplication.dataDevice.En5Tone = dataSignal[1];
			dataApplication.dataDevice.EnDTMF = dataSignal[2];
			dataApplication.dataDevice.EnMdc1200 = dataSignal[3];
			dataApplication.dataDevice.EnBdc1200 = dataSignal[3];
			TMP = devstr.Substring(idx + 6, 6);
			byte[] dataFunc = Encoding.Unicode.GetBytes(TMP);
			dataApplication.dataDevice.EnGps = dataFunc[0];
			dataApplication.dataDevice.EnBT = dataFunc[1];
			dataApplication.dataDevice.EnFalldn = dataFunc[2];
			dataApplication.dataDevice.EnNoise = dataFunc[3];
			dataApplication.dataDevice.EnFlight = dataFunc[4];
			dataApplication.dataDevice.EnZone = dataFunc[5];
		}
	}

	private void SaveFile()
	{
		SaveFileDialog sFD = new SaveFileDialog();
		sFD.Filter = "(*.xlc)|*.xlc";
		sFD.ValidateNames = true;
		sFD.AddExtension = true;
		sFD.RestoreDirectory = true;
		sFD.CheckPathExists = true;
		if (sFD.ShowDialog() == DialogResult.OK)
		{
			filePath = sFD.FileName;
			FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
			try
			{
				DataProtocol dt = new DataProtocol(new SerialPort(), dataApplication, OPT_DIR.DIR_WRITE);
				byte[] bytData = DataProtocol.GetBufFullData();
				bytData[bytData.Length - 2] = dataApplication.dataDevice.EnZone;
				bytData[bytData.Length - 1] = Settings.FreqBand;
				fs.Write(bytData, 0, bytData.Length);
			}
			catch
			{
				string KeyVal = "FileSErr";
				string Title = "";
				LanguageSel.ElseCombobox.TryGetValue(KeyVal, out Title);
				MessageBox.Show(Title);
			}
			fs.Close();
		}
	}

	private void mS_File_New_Click(object sender, EventArgs e)
	{
		if (suggestionForSaveFile() == DialogResult.Yes)
		{
			RefreshAndCloseForm();
			if (ReadFlg)
			{
				SaveDeviceArray(0);
			}
			dataApplication = new DataApp();
			if (ReadFlg)
			{
				SaveDeviceArray(1);
			}
			else
			{
				SetDeviceFunction();
			}
			TreeViewShow_Init();
			SetFreqBandRange();
			changeFormLanguage();
			CreateZoneTbl(1);
			CreateChnTbl();
			CreateVFOTbl();
			CreateGpsBook();
			DispAllData();
		}
		filePath = null;
	}

	private void mS_File_Open_Click(object sender, EventArgs e)
	{
		OpenFileDialog oFD = new OpenFileDialog();
		oFD.Filter = "(*.xlc)|*.xlc";
		oFD.ValidateNames = true;
		oFD.CheckFileExists = true;
		oFD.CheckPathExists = true;
		byte[] bytData = new byte[131072];
		if (oFD.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		filePath = oFD.FileName;
		if (!File.Exists(oFD.FileName))
		{
			string KeyVal = "FileNone";
			string Title = "";
			LanguageSel.ElseCombobox.TryGetValue(KeyVal, out Title);
			MessageBox.Show(Title);
			return;
		}
		try
		{
			FileStream fs = new FileStream(oFD.FileName, FileMode.Open, FileAccess.Read);
			if (fs.Length == 0)
			{
				string configstr = Path.GetFileNameWithoutExtension(oFD.FileName);
				dataApplication = new DataApp();
				SetDeviceFunction(configstr);
				TreeViewShow_Init();
				SetFreqBandRange();
				CreateZoneTbl(1);
				CreateChnTbl();
				CreateVFOTbl();
				CreateGpsBook();
				DispAllData();
				return;
			}
			while (fs.Read(bytData, 0, bytData.Length) > 0)
			{
			}
			fs.Close();
			DataProtocol dt = new DataProtocol(new SerialPort(), dataApplication, OPT_DIR.DIR_READ);
			DataProtocol.FileOpenFlg = true;
			Settings.FreqBand = bytData[bytData.Length - 1];
			dataApplication = dt.SetbufFullData(bytData, bytData.Length);
			TreeViewShow_Init();
			RefreshZoneNode();
			changeFormLanguage();
			CreateProKeyItems();
			DispAllData();
		}
		catch
		{
			string KeyVal = "Invalid File";
			string Title = "";
			LanguageSel.ElseCombobox.TryGetValue(KeyVal, out Title);
			MessageBox.Show(Title);
		}
	}

	private void mS_File_Save_Click(object sender, EventArgs e)
	{
		if (filePath != null)
		{
			try
			{
				if (File.GetAttributes(filePath).ToString().IndexOf("ReadOnly") != -1)
				{
					File.SetAttributes(filePath, FileAttributes.Normal);
				}
			}
			catch
			{
			}
			FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
			try
			{
				DataProtocol dt = new DataProtocol(new SerialPort(), dataApplication, OPT_DIR.DIR_WRITE);
				byte[] bytData = DataProtocol.GetBufFullData();
				bytData[bytData.Length - 2] = dataApplication.dataDevice.EnZone;
				bytData[bytData.Length - 1] = Settings.FreqBand;
				fs.Write(bytData, 0, bytData.Length);
			}
			catch
			{
				string KeyVal = "FileSErr";
				string Title = "";
				LanguageSel.ElseCombobox.TryGetValue(KeyVal, out Title);
				MessageBox.Show(Title);
			}
			fs.Close();
		}
		else
		{
			SaveFile();
		}
	}

	private void mS_File_SaveAs_Click(object sender, EventArgs e)
	{
		SaveFile();
	}

	private void mS_File_Exit_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void SaveDeviceArray(int mode)
	{
		if (mode == 0)
		{
			DevArray[0] = dataApplication.dataDevice.En2Tone;
			DevArray[1] = dataApplication.dataDevice.En5Tone;
			DevArray[2] = dataApplication.dataDevice.EnDTMF;
			DevArray[3] = dataApplication.dataDevice.EnMdc1200;
			DevArray[4] = dataApplication.dataDevice.EnBdc1200;
			DevArray[5] = dataApplication.dataDevice.EnGps;
			DevArray[6] = dataApplication.dataDevice.EnBT;
			DevArray[7] = dataApplication.dataDevice.EnFalldn;
			DevArray[8] = dataApplication.dataDevice.EnNoise;
			DevArray[9] = dataApplication.dataDevice.EnFlight;
			DevArray[10] = dataApplication.dataDevice.EnZone;
			DevArray[11] = dataApplication.dataDevice.Dis200m;
			DevArray[12] = dataApplication.dataDevice.Dis350m;
			DevArray[13] = dataApplication.dataDevice.Dis500m;
			DevArray[14] = Settings.FreqBand;
		}
		else
		{
			dataApplication.dataDevice.En2Tone = DevArray[0];
			dataApplication.dataDevice.En5Tone = DevArray[1];
			dataApplication.dataDevice.EnDTMF = DevArray[2];
			dataApplication.dataDevice.EnMdc1200 = DevArray[3];
			dataApplication.dataDevice.EnBdc1200 = DevArray[4];
			dataApplication.dataDevice.EnGps = DevArray[5];
			dataApplication.dataDevice.EnBT = DevArray[6];
			dataApplication.dataDevice.EnFalldn = DevArray[7];
			dataApplication.dataDevice.EnNoise = DevArray[8];
			dataApplication.dataDevice.EnFlight = DevArray[9];
			dataApplication.dataDevice.EnZone = DevArray[10];
			dataApplication.dataDevice.Dis200m = DevArray[11];
			dataApplication.dataDevice.Dis350m = DevArray[12];
			dataApplication.dataDevice.Dis500m = DevArray[13];
			dataApplication.dataDevice.Stand = DevArray[14];
		}
	}

	private void mS_Program_Write_Click(object sender, EventArgs e)
	{
		string[] strPorts = SerialPort.GetPortNames();
		if (portName != null && strPorts.Length > 0)
		{
			FormProgressBar fPB = new FormProgressBar(dataApplication);
			if (lang == "Chinese")
			{
				fPB.Text = "写频";
			}
			else
			{
				fPB.Text = "Write";
			}
			fPB.portName = portName;
			fPB.ShowDialog();
		}
		else if (lang == "Chinese")
		{
			MessageBox.Show("端口不存在!");
		}
		else
		{
			MessageBox.Show("The Port is invalid!");
		}
	}

	private void mS_Program_Read_Click(object sender, EventArgs e)
	{
		string[] strPorts = SerialPort.GetPortNames();
		if (portName != null && strPorts.Length > 0)
		{
			FormProgressBar fPB = new FormProgressBar(dataApplication);
			if (lang == "Chinese")
			{
				fPB.Text = "读频";
			}
			else
			{
				fPB.Text = "Read";
			}
			fPB.portName = portName;
			if (fPB.ShowDialog() == DialogResult.OK)
			{
				dataApplication = fPB.appData;
				TreeViewShow_Init();
				RefreshZoneNode();
				changeFormLanguage();
				CreateProKeyItems();
				DispAllData();
				ReadFlg = true;
				if (lang == "Chinese")
				{
					MessageBox.Show("读频成功！", "提示", MessageBoxButtons.OK);
				}
				else
				{
					MessageBox.Show("Success！", "Tip", MessageBoxButtons.OK);
				}
			}
		}
		else if (lang == "Chinese")
		{
			MessageBox.Show("端口不存在!");
		}
		else
		{
			MessageBox.Show("The Port is invalid!");
		}
	}

	private void mS_Setting_CommunicationPort_Click(object sender, EventArgs e)
	{
		if (new Frm_SetPassWord(0).ShowDialog() != DialogResult.OK)
		{
		}
	}

	private void Language_Setting(string strlang)
	{
		lang = strlang;
		if (strlang == "Chinese")
		{
			LanguageSel.LangType = 0;
		}
		else if (strlang == "English")
		{
			LanguageSel.LangType = 1;
		}
		else
		{
			lang = "Chinese";
			LanguageSel.LangType = 0;
		}
		if (!LanguageSel.Load(lang))
		{
			MessageBox.Show("Load language configuration file err!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			LanguageSel.HasLang = false;
		}
		else
		{
			LanguageSel.HasLang = true;
		}
		Settings.LangDef = lang;
		Settings.Save();
		if (strlang == "English")
		{
			mS_Language_cn.Checked = false;
			mS_Language_en.Checked = true;
		}
		else
		{
			mS_Language_cn.Checked = true;
			mS_Language_en.Checked = false;
		}
	}

	private void ms_Language_Select(string strlang)
	{
		if (lang != strlang)
		{
			Language_Setting(strlang);
			changeFormLanguage();
			CreateProKeyItems();
			RefreshAndCloseForm();
		}
	}

	private void mS_Language_en_Click(object sender, EventArgs e)
	{
		ms_Language_Select("English");
	}

	private void mS_Language_cn_Click(object sender, EventArgs e)
	{
		ms_Language_Select("Chinese");
	}

	private void mS_StatusBar_Click(object sender, EventArgs e)
	{
		if (mS_View_StatusBar.CheckState == CheckState.Checked)
		{
			mS_View_StatusBar.CheckState = CheckState.Unchecked;
			statusStrip1.Visible = false;
		}
		else
		{
			mS_View_StatusBar.CheckState = CheckState.Checked;
			statusStrip1.Visible = true;
		}
	}

	private void mS_ToolBar_Click(object sender, EventArgs e)
	{
		if (toolStrip1.Visible)
		{
			toolStrip1.Visible = false;
			mS_View_ToolBar.CheckState = CheckState.Unchecked;
		}
		else
		{
			toolStrip1.Visible = true;
			mS_View_ToolBar.CheckState = CheckState.Checked;
		}
	}

	private void mS_View_Tree_Click(object sender, EventArgs e)
	{
		if (TreeShow.Visible)
		{
			TreeShow.Visible = false;
			mS_View_Tree.CheckState = CheckState.Unchecked;
		}
		else
		{
			TreeShow.Visible = true;
			mS_View_Tree.CheckState = CheckState.Checked;
		}
	}

	private void mS_CendieWindow_Click(object sender, EventArgs e)
	{
		LayoutMdi(MdiLayout.Cascade);
	}

	private void mS_PinpuWindow_Click(object sender, EventArgs e)
	{
		LayoutMdi(MdiLayout.TileHorizontal);
	}

	private void mS_Help_About_Click(object sender, EventArgs e)
	{
		new FormAbout().ShowDialog();
	}

	private void mS_Help_File_Click(object sender, EventArgs e)
	{
	}

	private DialogResult suggestionForSaveFile()
	{
		if (lang == "Chinese")
		{
			return MessageBox.Show("将清除所有数据?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		}
		return MessageBox.Show("Will clear all data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
	}

	private void CreateProKeyItems()
	{
		ProKeyEnum.Long1Pkey.Clear();
		ProKeyEnum.Short1Pkey.Clear();
		ProKeyEnum.Long2Pkey.Clear();
		ProKeyEnum.Short2Pkey.Clear();
		ProKeyEnum.Long3Pkey.Clear();
		ProKeyEnum.Short3Pkey.Clear();
		ProKeyEnum.Long4Pkey.Clear();
		ProKeyEnum.Short4Pkey.Clear();
		for (int i = 0; i < 16; i++)
		{
			if (i == ProKeyEnum.KEY_FUNC_BT)
			{
				if (dataApplication.dataDevice.EnBT == 0)
				{
					continue;
				}
			}
			else if (i == ProKeyEnum.KEY_FUNC_GPS)
			{
				if (dataApplication.dataDevice.EnGps == 0)
				{
					continue;
				}
			}
			else if (i == ProKeyEnum.KEY_FUNC_FLASH_LIGHT)
			{
				if (dataApplication.dataDevice.EnFlight == 0)
				{
					continue;
				}
			}
			else if (i == ProKeyEnum.KEY_FUNC_FALLING_DW)
			{
				if (dataApplication.dataDevice.EnFalldn == 0)
				{
					continue;
				}
			}
			else if (i == ProKeyEnum.KEY_FUNC_ZONE && dataApplication.dataDevice.EnZone == 0)
			{
				continue;
			}
			if (lang == "Chinese")
			{
				ProKeyEnum.Short1Pkey.Add(ProKeyEnum.KeyFunc_CN[i]);
				ProKeyEnum.Long1Pkey.Add(ProKeyEnum.KeyFunc_CN[i]);
				ProKeyEnum.Short2Pkey.Add(ProKeyEnum.KeyFunc_CN[i]);
				ProKeyEnum.Long2Pkey.Add(ProKeyEnum.KeyFunc_CN[i]);
				ProKeyEnum.Short3Pkey.Add(ProKeyEnum.KeyFunc_CN[i]);
				ProKeyEnum.Long3Pkey.Add(ProKeyEnum.KeyFunc_CN[i]);
				ProKeyEnum.Short4Pkey.Add(ProKeyEnum.KeyFunc_CN[i]);
				ProKeyEnum.Long4Pkey.Add(ProKeyEnum.KeyFunc_CN[i]);
			}
			else
			{
				ProKeyEnum.Short1Pkey.Add(ProKeyEnum.KeyFunc_EN[i]);
				ProKeyEnum.Long1Pkey.Add(ProKeyEnum.KeyFunc_EN[i]);
				ProKeyEnum.Short2Pkey.Add(ProKeyEnum.KeyFunc_EN[i]);
				ProKeyEnum.Long2Pkey.Add(ProKeyEnum.KeyFunc_EN[i]);
				ProKeyEnum.Short3Pkey.Add(ProKeyEnum.KeyFunc_EN[i]);
				ProKeyEnum.Long3Pkey.Add(ProKeyEnum.KeyFunc_EN[i]);
				ProKeyEnum.Short4Pkey.Add(ProKeyEnum.KeyFunc_EN[i]);
				ProKeyEnum.Long4Pkey.Add(ProKeyEnum.KeyFunc_EN[i]);
			}
		}
	}

	private void CreateInitDevice()
	{
	}

	private void CreateZoneTbl(int mode)
	{
		AddZoneData(mode);
	}

	private void RefreshZoneName()
	{
		string name = string.Empty;
		if (dataApplication.dataDevice.EnZone != 1)
		{
			return;
		}
		for (int i = 0; i < DataApp.Zone_Max_Num; i++)
		{
			name = i + 1 + ": ";
			if (dataApplication.dataZoneInfor[i].ChnNum > 0)
			{
				name += dataApplication.dataZoneInfor[i].ZoneName;
			}
			TreeShow.Nodes[0].Nodes[2].Nodes[i + 1].Text = name;
		}
	}

	private void RefreshZoneNode()
	{
		string name = string.Empty;
		for (int i = 0; i < DataApp.Zone_Max_Num; i++)
		{
			TreeNode tn = new TreeNode();
			if (dataApplication.dataDevice.EnZone == 1)
			{
				tn.Text = i + 1 + ": " + dataApplication.dataZoneInfor[i].ZoneName;
				TreeShow.Nodes[0].Nodes[2].Nodes.Add(tn);
				continue;
			}
			tn.Text = (i + 1).ToString();
			if (i == 0)
			{
				TreeShow.Nodes[0].Nodes[2].Nodes.Add(tn);
			}
		}
	}

	public void AddZoneData(int mode)
	{
		string name = string.Empty;
		for (int i = 0; i < DataApp.Zone_Max_Num; i++)
		{
			TreeNode tn = new TreeNode();
			if (i == 0)
			{
				dataApplication.dataZoneInfor[i].ChnNum = 1;
				dataApplication.dataZoneInfor[i].ZoneName = "Zone 1";
				tn.ImageIndex = 1;
				tn.SelectedImageIndex = 1;
				DatZoneInfo.ZoneTotal = 1;
			}
			else
			{
				dataApplication.dataZoneInfor[i].ChnNum = 0;
				dataApplication.dataZoneInfor[i].ZoneName = "";
				tn.ImageIndex = 0;
				tn.SelectedImageIndex = 0;
			}
			for (int j = 0; j < DataApp.Zone_Max_Chn_Num; j++)
			{
				dataApplication.dataZoneInfor[i].SetChnID(j, 0);
			}
			if (mode == 1)
			{
				if (dataApplication.dataDevice.EnZone == 1)
				{
					tn.Text = i + 1 + ": " + dataApplication.dataZoneInfor[i].ZoneName;
					TreeShow.Nodes[0].Nodes[2].Nodes.Add(tn);
					continue;
				}
				tn.Text = (i + 1).ToString();
				if (i == 0)
				{
					TreeShow.Nodes[0].Nodes[2].Nodes.Add(tn);
				}
			}
			else if (i > 0)
			{
				TreeShow.Nodes[0].Nodes[2].Nodes[i + 1].Text = i + 1 + ": ";
			}
		}
	}

	private void CreateChnTbl()
	{
		if (Settings.FreqBand != 4)
		{
			AddChannelData(0, "403.00000");
		}
		else
		{
			AddChannelData(0, "430.01250");
		}
	}

	public void AddChannelData(int chidx, string rxfreq)
	{
		string name = string.Empty;
		DatChannelInfo sInfo = new DatChannelInfo();
		sInfo.ChnName = "CH " + (chidx + 1);
		sInfo.RxFreq = rxfreq;
		sInfo.TxFreq = rxfreq;
		sInfo.RxCtsDcs = "OFF";
		sInfo.TxCtsDcs = "OFF";
		sInfo.Power = HIGH_VALID;
		sInfo.Wideth = 1;
		sInfo.Offsetdir = 0;
		sInfo.FreqInvert = 0;
		sInfo.TalkAround = 0;
		sInfo.DtmfPtt = 0;
		sInfo.FivetonePtt = 0;
		sInfo.SqType = 0;
		sInfo.SignalType = 0;
		sInfo.DtmfIdx = 0;
		sInfo.FivetoneIdx = 0;
		sInfo.Scanlist = 0;
		sInfo.Emerglist = 0;
		sInfo.UseFlg = 1;
		dataApplication.dataChnInfor[chidx] = sInfo;
		DatChannelInfo.ChnTotal++;
	}

	private void CreateVFOTbl()
	{
		AddVFOData(0);
		AddVFOData(1);
	}

	public void AddVFOData(int pos)
	{
		DatChannelInfo sInfo = new DatChannelInfo();
		sInfo.Power = HIGH_VALID;
		sInfo.Wideth = 0;
		if (Settings.FreqBand != 4)
		{
			sInfo.RxFreq = "400.01250";
			sInfo.TxFreq = "400.01250";
		}
		else
		{
			sInfo.RxFreq = "430.01250";
			sInfo.TxFreq = "430.01250";
		}
		sInfo.DivFreq = "0.00000";
		sInfo.RxCtsDcs = "OFF";
		sInfo.TxCtsDcs = "OFF";
		sInfo.ChnName = "VFO CH" + (pos + 1);
		sInfo.Offsetdir = 0;
		sInfo.Emerglist = 0;
		sInfo.Scanlist = 0;
		dataApplication.dataVFOInfor[pos] = sInfo;
	}

	private void CreateGpsBook()
	{
		DatGpsBook sInfo = new DatGpsBook();
		sInfo.UseFlg = 1;
		sInfo.CodeID = 1;
		sInfo.CodeName = "Contact 1";
		DatGpsBook.GpsBook_Total = 1;
		dataApplication.dataGpsBook[0] = sInfo;
	}

	private void RefreshAndCloseForm()
	{
		if (frmGSet != null)
		{
			frmGSet.Close();
		}
		if (frmChn != null)
		{
			frmChn.Close();
		}
		if (frmZoneChn != null)
		{
			frmZoneChn.Close();
		}
		if (frmDevInfo != null)
		{
			frmDevInfo.Close();
		}
		if (frmZone != null)
		{
			frmZone.Close();
		}
		if (frmVFO != null)
		{
			frmVFO.Close();
		}
		if (frmMdcSys != null)
		{
			frmMdcSys.Close();
		}
		if (frmMdcPara != null)
		{
			frmMdcPara.Close();
		}
		if (frmMdcPtt != null)
		{
			frmMdcPtt.Close();
		}
		if (frmMdcDec != null)
		{
			frmMdcDec.Close();
		}
		if (frmMdcBiis != null)
		{
			frmMdcBiis.Close();
		}
		if (frmScan != null)
		{
			frmScan.Close();
		}
		if (frmDtmf != null)
		{
			frmDtmf.Close();
		}
		if (frmTwoTone != null)
		{
			frmTwoTone.Close();
		}
		if (frm5ToneDec != null)
		{
			frm5ToneDec.Close();
		}
		if (frm5ToneEnc != null)
		{
			frm5ToneEnc.Close();
		}
		if (frm5ToneCodeInfo != null)
		{
			frm5ToneCodeInfo.Close();
		}
		if (frmEmerg != null)
		{
			frmEmerg.Close();
		}
		if (frmAprs != null)
		{
			frmAprs.Close();
		}
		if (frmGpsBook != null)
		{
			frmGpsBook.Close();
		}
		DispFrmDevInfo(TreeShow.Nodes[0].Nodes[0].Text);
	}

	private void DispAllData()
	{
		RefreshAndCloseForm();
		RefreshZoneName();
		frmDevInfo.DeviceDataDisp();
	}

	private void SetFreqBandRange()
	{
		if (Settings.FreqBand == 0)
		{
			dataApplication.dataDevice.SetRxStartFreq(0, 13600000);
			dataApplication.dataDevice.SetRxEndFreq(0, 17400000);
			dataApplication.dataDevice.SetRxStartFreq(1, 22000000);
			dataApplication.dataDevice.SetRxEndFreq(1, 26000000);
			dataApplication.dataDevice.SetRxStartFreq(2, 35000000);
			dataApplication.dataDevice.SetRxEndFreq(2, 39000000);
			dataApplication.dataDevice.SetRxStartFreq(3, 40000000);
			dataApplication.dataDevice.SetRxEndFreq(3, 52000000);
			dataApplication.dataDevice.SetTxStartFreq(0, 13600000);
			dataApplication.dataDevice.SetTxEndFreq(0, 17400000);
			dataApplication.dataDevice.SetTxStartFreq(1, 22000000);
			dataApplication.dataDevice.SetTxEndFreq(1, 26000000);
			dataApplication.dataDevice.SetTxStartFreq(2, 0);
			dataApplication.dataDevice.SetTxEndFreq(2, 0);
			dataApplication.dataDevice.SetTxStartFreq(3, 40000000);
			dataApplication.dataDevice.SetTxEndFreq(3, 48000000);
			dataApplication.dataDevice.FreqRxPt = 4;
			dataApplication.dataDevice.FreqTxPt = 3;
		}
		else if (Settings.FreqBand == 1)
		{
			dataApplication.dataDevice.SetRxStartFreq(0, 13600000);
			dataApplication.dataDevice.SetRxEndFreq(0, 17400000);
			dataApplication.dataDevice.SetRxStartFreq(1, 22000000);
			dataApplication.dataDevice.SetRxEndFreq(1, 26000000);
			dataApplication.dataDevice.SetRxStartFreq(2, 35000000);
			dataApplication.dataDevice.SetRxEndFreq(2, 39000000);
			dataApplication.dataDevice.SetRxStartFreq(3, 40000000);
			dataApplication.dataDevice.SetRxEndFreq(3, 52000000);
			dataApplication.dataDevice.SetTxStartFreq(0, 14400000);
			dataApplication.dataDevice.SetTxEndFreq(0, 14800000);
			dataApplication.dataDevice.SetTxStartFreq(3, 43000000);
			dataApplication.dataDevice.SetTxEndFreq(3, 44000000);
			dataApplication.dataDevice.SetTxStartFreq(2, 0);
			dataApplication.dataDevice.SetTxEndFreq(2, 0);
			dataApplication.dataDevice.SetTxStartFreq(1, 0);
			dataApplication.dataDevice.SetTxEndFreq(1, 0);
			dataApplication.dataDevice.FreqRxPt = 4;
			dataApplication.dataDevice.FreqTxPt = 2;
		}
		else if (Settings.FreqBand == 2)
		{
			dataApplication.dataDevice.SetRxStartFreq(0, 13600000);
			dataApplication.dataDevice.SetRxEndFreq(0, 17400000);
			dataApplication.dataDevice.SetRxStartFreq(1, 22000000);
			dataApplication.dataDevice.SetRxEndFreq(1, 26000000);
			dataApplication.dataDevice.SetRxStartFreq(2, 35000000);
			dataApplication.dataDevice.SetRxEndFreq(2, 39000000);
			dataApplication.dataDevice.SetRxStartFreq(3, 40000000);
			dataApplication.dataDevice.SetRxEndFreq(3, 52000000);
			dataApplication.dataDevice.SetTxStartFreq(0, 14400000);
			dataApplication.dataDevice.SetTxEndFreq(0, 14800000);
			dataApplication.dataDevice.SetTxStartFreq(3, 42000000);
			dataApplication.dataDevice.SetTxEndFreq(3, 45000000);
			dataApplication.dataDevice.SetTxStartFreq(2, 0);
			dataApplication.dataDevice.SetTxEndFreq(2, 0);
			dataApplication.dataDevice.SetTxStartFreq(1, 22200000);
			dataApplication.dataDevice.SetTxEndFreq(1, 22500000);
			dataApplication.dataDevice.FreqRxPt = 4;
			dataApplication.dataDevice.FreqTxPt = 3;
		}
		else if (Settings.FreqBand == 3)
		{
			dataApplication.dataDevice.SetRxStartFreq(0, 14400000);
			dataApplication.dataDevice.SetRxEndFreq(0, 14800000);
			dataApplication.dataDevice.SetRxStartFreq(1, 0);
			dataApplication.dataDevice.SetRxEndFreq(1, 0);
			dataApplication.dataDevice.SetRxStartFreq(2, 0);
			dataApplication.dataDevice.SetRxEndFreq(2, 0);
			dataApplication.dataDevice.SetRxStartFreq(3, 43000000);
			dataApplication.dataDevice.SetRxEndFreq(3, 45000000);
			dataApplication.dataDevice.SetTxStartFreq(0, 14400000);
			dataApplication.dataDevice.SetTxEndFreq(0, 14800000);
			dataApplication.dataDevice.SetTxStartFreq(3, 43000000);
			dataApplication.dataDevice.SetTxEndFreq(3, 45000000);
			dataApplication.dataDevice.SetTxStartFreq(2, 0);
			dataApplication.dataDevice.SetTxEndFreq(2, 0);
			dataApplication.dataDevice.SetTxStartFreq(1, 0);
			dataApplication.dataDevice.SetTxEndFreq(1, 0);
			dataApplication.dataDevice.FreqRxPt = 2;
			dataApplication.dataDevice.FreqTxPt = 2;
		}
		else if (Settings.FreqBand == 4)
		{
			dataApplication.dataDevice.SetRxStartFreq(0, 14400000);
			dataApplication.dataDevice.SetRxEndFreq(0, 14600000);
			dataApplication.dataDevice.SetRxStartFreq(3, 43000000);
			dataApplication.dataDevice.SetRxEndFreq(3, 44000000);
			dataApplication.dataDevice.SetRxStartFreq(2, 0);
			dataApplication.dataDevice.SetRxEndFreq(2, 0);
			dataApplication.dataDevice.SetRxStartFreq(1, 0);
			dataApplication.dataDevice.SetRxEndFreq(1, 0);
			dataApplication.dataDevice.SetTxStartFreq(0, 14400000);
			dataApplication.dataDevice.SetTxEndFreq(0, 14600000);
			dataApplication.dataDevice.SetTxStartFreq(3, 43000000);
			dataApplication.dataDevice.SetTxEndFreq(3, 44000000);
			dataApplication.dataDevice.SetTxStartFreq(2, 0);
			dataApplication.dataDevice.SetTxEndFreq(2, 0);
			dataApplication.dataDevice.SetTxStartFreq(1, 0);
			dataApplication.dataDevice.SetTxEndFreq(1, 0);
			dataApplication.dataDevice.FreqRxPt = 2;
			dataApplication.dataDevice.FreqTxPt = 2;
		}
		else if (Settings.FreqBand == 5)
		{
			dataApplication.dataDevice.SetRxStartFreq(0, 13600000);
			dataApplication.dataDevice.SetRxEndFreq(0, 17400000);
			dataApplication.dataDevice.SetRxStartFreq(3, 40000000);
			dataApplication.dataDevice.SetRxEndFreq(3, 43800000);
			dataApplication.dataDevice.SetRxStartFreq(2, 0);
			dataApplication.dataDevice.SetRxEndFreq(2, 0);
			dataApplication.dataDevice.SetRxStartFreq(1, 0);
			dataApplication.dataDevice.SetRxEndFreq(1, 0);
			dataApplication.dataDevice.SetTxStartFreq(0, 13600000);
			dataApplication.dataDevice.SetTxEndFreq(0, 17400000);
			dataApplication.dataDevice.SetTxStartFreq(3, 40000000);
			dataApplication.dataDevice.SetTxEndFreq(3, 43800000);
			dataApplication.dataDevice.SetTxStartFreq(2, 0);
			dataApplication.dataDevice.SetTxEndFreq(2, 0);
			dataApplication.dataDevice.SetTxStartFreq(1, 0);
			dataApplication.dataDevice.SetTxEndFreq(1, 0);
			dataApplication.dataDevice.FreqRxPt = 2;
			dataApplication.dataDevice.FreqTxPt = 2;
		}
		else if (Settings.FreqBand == 6)
		{
			dataApplication.dataDevice.SetRxStartFreq(0, 13600000);
			dataApplication.dataDevice.SetRxEndFreq(0, 17400000);
			dataApplication.dataDevice.SetRxStartFreq(1, 22000000);
			dataApplication.dataDevice.SetRxEndFreq(1, 26000000);
			dataApplication.dataDevice.SetRxStartFreq(2, 35000000);
			dataApplication.dataDevice.SetRxEndFreq(2, 39000000);
			dataApplication.dataDevice.SetRxStartFreq(3, 40000000);
			dataApplication.dataDevice.SetRxEndFreq(3, 52000000);
			dataApplication.dataDevice.SetTxStartFreq(0, 14400000);
			dataApplication.dataDevice.SetTxEndFreq(0, 14600000);
			dataApplication.dataDevice.SetTxStartFreq(3, 43000000);
			dataApplication.dataDevice.SetTxEndFreq(3, 44000000);
			dataApplication.dataDevice.SetTxStartFreq(2, 0);
			dataApplication.dataDevice.SetTxEndFreq(2, 0);
			dataApplication.dataDevice.SetTxStartFreq(1, 0);
			dataApplication.dataDevice.SetTxEndFreq(1, 0);
			dataApplication.dataDevice.FreqRxPt = 4;
			dataApplication.dataDevice.FreqTxPt = 2;
		}
		else if (Settings.FreqBand == 7)
		{
			dataApplication.dataDevice.SetRxStartFreq(0, 13600000);
			dataApplication.dataDevice.SetRxEndFreq(0, 17400000);
			dataApplication.dataDevice.SetRxStartFreq(3, 40000000);
			dataApplication.dataDevice.SetRxEndFreq(3, 48000000);
			dataApplication.dataDevice.SetRxStartFreq(2, 0);
			dataApplication.dataDevice.SetRxEndFreq(2, 0);
			dataApplication.dataDevice.SetRxStartFreq(1, 0);
			dataApplication.dataDevice.SetRxEndFreq(1, 0);
			dataApplication.dataDevice.SetTxStartFreq(0, 13600000);
			dataApplication.dataDevice.SetTxEndFreq(0, 17400000);
			dataApplication.dataDevice.SetTxStartFreq(3, 40000000);
			dataApplication.dataDevice.SetTxEndFreq(3, 48000000);
			dataApplication.dataDevice.SetTxStartFreq(2, 0);
			dataApplication.dataDevice.SetTxEndFreq(2, 0);
			dataApplication.dataDevice.SetTxStartFreq(1, 0);
			dataApplication.dataDevice.SetTxEndFreq(1, 0);
			dataApplication.dataDevice.FreqRxPt = 2;
			dataApplication.dataDevice.FreqTxPt = 2;
		}
		else if (Settings.FreqBand == 8)
		{
			dataApplication.dataDevice.SetRxStartFreq(0, 13600000);
			dataApplication.dataDevice.SetRxEndFreq(0, 17400000);
			dataApplication.dataDevice.SetRxStartFreq(3, 40000000);
			dataApplication.dataDevice.SetRxEndFreq(3, 52000000);
			dataApplication.dataDevice.SetRxStartFreq(2, 0);
			dataApplication.dataDevice.SetRxEndFreq(2, 0);
			dataApplication.dataDevice.SetRxStartFreq(1, 0);
			dataApplication.dataDevice.SetRxEndFreq(1, 0);
			dataApplication.dataDevice.SetTxStartFreq(0, 13600000);
			dataApplication.dataDevice.SetTxEndFreq(0, 17400000);
			dataApplication.dataDevice.SetTxStartFreq(3, 40000000);
			dataApplication.dataDevice.SetTxEndFreq(3, 48000000);
			dataApplication.dataDevice.SetTxStartFreq(2, 0);
			dataApplication.dataDevice.SetTxEndFreq(2, 0);
			dataApplication.dataDevice.SetTxStartFreq(1, 0);
			dataApplication.dataDevice.SetTxEndFreq(1, 0);
			dataApplication.dataDevice.FreqRxPt = 2;
			dataApplication.dataDevice.FreqTxPt = 2;
		}
	}

	private void mS_Program_FreqBand_Click(object sender, EventArgs e)
	{
		if (((VER_TYPE == 0) ? DialogResult.OK : new Frm_SetPassWord(2).ShowDialog()) == DialogResult.OK && new Frm_SetFreqBand().ShowDialog() == DialogResult.OK)
		{
			RefreshAndCloseForm();
			if (ReadFlg)
			{
				SaveDeviceArray(0);
			}
			dataApplication = new DataApp();
			if (ReadFlg)
			{
				SaveDeviceArray(1);
			}
			else
			{
				SetDeviceFunction();
				dataApplication.dataDevice.Stand = Settings.FreqBand;
			}
			TreeViewShow_Init();
			SetFreqBandRange();
			CreateZoneTbl(1);
			CreateChnTbl();
			CreateVFOTbl();
			CreateGpsBook();
			DispAllData();
		}
	}

	private void mS_Program_Embed_Click(object sender, EventArgs e)
	{
		if (((VER_TYPE == 0) ? DialogResult.OK : new Frm_SetPassWord(1).ShowDialog()) == DialogResult.OK && new Frm_EmbedInfo(dataApplication).ShowDialog() == DialogResult.OK)
		{
			RefreshAndCloseForm();
			TreeViewShow_Init();
			changeFormLanguage();
			CreateProKeyItems();
			RefreshZoneNode();
			CreateGpsBook();
			DispAllData();
			DataProtocol.DevReadFlg = true;
		}
	}

	private void mS_Tool_Logo_Click(object sender, EventArgs e)
	{
		string[] strPorts = SerialPort.GetPortNames();
		if (portName != null && strPorts.Length > 0)
		{
			FormProgressBar fPB = new FormProgressBar(dataApplication);
			if (lang == "Chinese")
			{
				fPB.Text = "开机图片";
			}
			else
			{
				fPB.Text = "Boot Picture";
			}
			fPB.portName = portName;
			if (fPB.ShowDialog() == DialogResult.OK)
			{
				dataApplication = fPB.appData;
				if (lang == "Chinese")
				{
					MessageBox.Show("导入成功！", "提示", MessageBoxButtons.OK);
				}
				else
				{
					MessageBox.Show("Import Success！", "Tip", MessageBoxButtons.OK);
				}
			}
		}
		else if (lang == "Chinese")
		{
			MessageBox.Show("端口不存在!");
		}
		else
		{
			MessageBox.Show("The Port is invalid!");
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPS_5RH.FormMain));
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.menuStrip1 = new System.Windows.Forms.MenuStrip();
		this.mS_File = new System.Windows.Forms.ToolStripMenuItem();
		this.mS_File_New = new System.Windows.Forms.ToolStripMenuItem();
		this.mS_File_Open = new System.Windows.Forms.ToolStripMenuItem();
		this.mS_File_Save = new System.Windows.Forms.ToolStripMenuItem();
		this.mS_File_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.mS_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
		this.mS_Program = new System.Windows.Forms.ToolStripMenuItem();
		this.mS_Program_Read = new System.Windows.Forms.ToolStripMenuItem();
		this.mS_Program_Write = new System.Windows.Forms.ToolStripMenuItem();
		this.mS_Program_Password = new System.Windows.Forms.ToolStripMenuItem();
		this.mS_Program_FreqBand = new System.Windows.Forms.ToolStripMenuItem();
		this.mS_Program_Embed = new System.Windows.Forms.ToolStripMenuItem();
		this.mS_Tools = new System.Windows.Forms.ToolStripMenuItem();
		this.mS_Tool_Logo = new System.Windows.Forms.ToolStripMenuItem();
		this.mS_Language = new System.Windows.Forms.ToolStripMenuItem();
		this.mS_Language_cn = new System.Windows.Forms.ToolStripMenuItem();
		this.mS_Language_en = new System.Windows.Forms.ToolStripMenuItem();
		this.mS_View = new System.Windows.Forms.ToolStripMenuItem();
		this.mS_View_ToolBar = new System.Windows.Forms.ToolStripMenuItem();
		this.mS_View_StatusBar = new System.Windows.Forms.ToolStripMenuItem();
		this.mS_View_Tree = new System.Windows.Forms.ToolStripMenuItem();
		this.mS_Window = new System.Windows.Forms.ToolStripMenuItem();
		this.mS_Window_Cendie = new System.Windows.Forms.ToolStripMenuItem();
		this.mS_Window_Pinpu = new System.Windows.Forms.ToolStripMenuItem();
		this.mS_Help = new System.Windows.Forms.ToolStripMenuItem();
		this.mS_Help_File = new System.Windows.Forms.ToolStripMenuItem();
		this.mS_Help_About = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStrip1 = new System.Windows.Forms.ToolStrip();
		this.tsBtn_New = new System.Windows.Forms.ToolStripButton();
		this.tsBtn_Open = new System.Windows.Forms.ToolStripButton();
		this.tsBtn_Save = new System.Windows.Forms.ToolStripButton();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.tsBtn_Com = new System.Windows.Forms.ToolStripButton();
		this.tsBtn_Read = new System.Windows.Forms.ToolStripButton();
		this.tsBtn_Write = new System.Windows.Forms.ToolStripButton();
		this.TreeShow = new System.Windows.Forms.TreeView();
		this.splitter1 = new System.Windows.Forms.Splitter();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.statusStrip1 = new System.Windows.Forms.StatusStrip();
		this.menuStrip1.SuspendLayout();
		this.toolStrip1.SuspendLayout();
		base.SuspendLayout();
		this.timer1.Enabled = true;
		this.timer1.Interval = 1000;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.mS_File, this.mS_Program, this.mS_Tools, this.mS_Language, this.mS_View, this.mS_Window, this.mS_Help });
		resources.ApplyResources(this.menuStrip1, "menuStrip1");
		this.menuStrip1.Name = "menuStrip1";
		this.mS_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.mS_File_New, this.mS_File_Open, this.mS_File_Save, this.mS_File_SaveAs, this.toolStripSeparator1, this.mS_File_Exit });
		this.mS_File.Name = "mS_File";
		resources.ApplyResources(this.mS_File, "mS_File");
		this.mS_File_New.Name = "mS_File_New";
		resources.ApplyResources(this.mS_File_New, "mS_File_New");
		this.mS_File_New.Click += new System.EventHandler(mS_File_New_Click);
		this.mS_File_Open.Name = "mS_File_Open";
		resources.ApplyResources(this.mS_File_Open, "mS_File_Open");
		this.mS_File_Open.Click += new System.EventHandler(mS_File_Open_Click);
		this.mS_File_Save.Name = "mS_File_Save";
		resources.ApplyResources(this.mS_File_Save, "mS_File_Save");
		this.mS_File_Save.Click += new System.EventHandler(mS_File_Save_Click);
		this.mS_File_SaveAs.Name = "mS_File_SaveAs";
		resources.ApplyResources(this.mS_File_SaveAs, "mS_File_SaveAs");
		this.mS_File_SaveAs.Click += new System.EventHandler(mS_File_SaveAs_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
		this.mS_File_Exit.Name = "mS_File_Exit";
		resources.ApplyResources(this.mS_File_Exit, "mS_File_Exit");
		this.mS_File_Exit.Click += new System.EventHandler(mS_File_Exit_Click);
		this.mS_Program.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.mS_Program_Read, this.mS_Program_Write, this.mS_Program_Password, this.mS_Program_FreqBand, this.mS_Program_Embed });
		this.mS_Program.Name = "mS_Program";
		resources.ApplyResources(this.mS_Program, "mS_Program");
		this.mS_Program_Read.Name = "mS_Program_Read";
		resources.ApplyResources(this.mS_Program_Read, "mS_Program_Read");
		this.mS_Program_Read.Click += new System.EventHandler(mS_Program_Read_Click);
		this.mS_Program_Write.Name = "mS_Program_Write";
		resources.ApplyResources(this.mS_Program_Write, "mS_Program_Write");
		this.mS_Program_Write.Click += new System.EventHandler(mS_Program_Write_Click);
		this.mS_Program_Password.Name = "mS_Program_Password";
		resources.ApplyResources(this.mS_Program_Password, "mS_Program_Password");
		this.mS_Program_Password.Click += new System.EventHandler(mS_Setting_CommunicationPort_Click);
		this.mS_Program_FreqBand.Name = "mS_Program_FreqBand";
		resources.ApplyResources(this.mS_Program_FreqBand, "mS_Program_FreqBand");
		this.mS_Program_FreqBand.Click += new System.EventHandler(mS_Program_FreqBand_Click);
		this.mS_Program_Embed.Name = "mS_Program_Embed";
		resources.ApplyResources(this.mS_Program_Embed, "mS_Program_Embed");
		this.mS_Program_Embed.Click += new System.EventHandler(mS_Program_Embed_Click);
		this.mS_Tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.mS_Tool_Logo });
		this.mS_Tools.Name = "mS_Tools";
		resources.ApplyResources(this.mS_Tools, "mS_Tools");
		this.mS_Tool_Logo.Name = "mS_Tool_Logo";
		resources.ApplyResources(this.mS_Tool_Logo, "mS_Tool_Logo");
		this.mS_Tool_Logo.Click += new System.EventHandler(mS_Tool_Logo_Click);
		this.mS_Language.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mS_Language_cn, this.mS_Language_en });
		this.mS_Language.Name = "mS_Language";
		resources.ApplyResources(this.mS_Language, "mS_Language");
		this.mS_Language_cn.Name = "mS_Language_cn";
		resources.ApplyResources(this.mS_Language_cn, "mS_Language_cn");
		this.mS_Language_cn.Click += new System.EventHandler(mS_Language_cn_Click);
		this.mS_Language_en.Name = "mS_Language_en";
		resources.ApplyResources(this.mS_Language_en, "mS_Language_en");
		this.mS_Language_en.Click += new System.EventHandler(mS_Language_en_Click);
		this.mS_View.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.mS_View_ToolBar, this.mS_View_StatusBar, this.mS_View_Tree });
		this.mS_View.Name = "mS_View";
		resources.ApplyResources(this.mS_View, "mS_View");
		this.mS_View_ToolBar.Name = "mS_View_ToolBar";
		resources.ApplyResources(this.mS_View_ToolBar, "mS_View_ToolBar");
		this.mS_View_ToolBar.Click += new System.EventHandler(mS_ToolBar_Click);
		this.mS_View_StatusBar.Name = "mS_View_StatusBar";
		resources.ApplyResources(this.mS_View_StatusBar, "mS_View_StatusBar");
		this.mS_View_StatusBar.Click += new System.EventHandler(mS_StatusBar_Click);
		this.mS_View_Tree.Name = "mS_View_Tree";
		resources.ApplyResources(this.mS_View_Tree, "mS_View_Tree");
		this.mS_View_Tree.Click += new System.EventHandler(mS_View_Tree_Click);
		this.mS_Window.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mS_Window_Cendie, this.mS_Window_Pinpu });
		this.mS_Window.Name = "mS_Window";
		resources.ApplyResources(this.mS_Window, "mS_Window");
		this.mS_Window_Cendie.Name = "mS_Window_Cendie";
		resources.ApplyResources(this.mS_Window_Cendie, "mS_Window_Cendie");
		this.mS_Window_Cendie.Click += new System.EventHandler(mS_CendieWindow_Click);
		this.mS_Window_Pinpu.Name = "mS_Window_Pinpu";
		resources.ApplyResources(this.mS_Window_Pinpu, "mS_Window_Pinpu");
		this.mS_Window_Pinpu.Click += new System.EventHandler(mS_PinpuWindow_Click);
		this.mS_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.mS_Help_File, this.mS_Help_About });
		this.mS_Help.Name = "mS_Help";
		resources.ApplyResources(this.mS_Help, "mS_Help");
		this.mS_Help_File.Name = "mS_Help_File";
		resources.ApplyResources(this.mS_Help_File, "mS_Help_File");
		this.mS_Help_About.Name = "mS_Help_About";
		resources.ApplyResources(this.mS_Help_About, "mS_Help_About");
		this.mS_Help_About.Click += new System.EventHandler(mS_Help_About_Click);
		this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.tsBtn_New, this.tsBtn_Open, this.tsBtn_Save, this.toolStripSeparator2, this.tsBtn_Com, this.tsBtn_Read, this.tsBtn_Write });
		resources.ApplyResources(this.toolStrip1, "toolStrip1");
		this.toolStrip1.Name = "toolStrip1";
		this.tsBtn_New.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		resources.ApplyResources(this.tsBtn_New, "tsBtn_New");
		this.tsBtn_New.Name = "tsBtn_New";
		this.tsBtn_New.Click += new System.EventHandler(mS_File_New_Click);
		this.tsBtn_Open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		resources.ApplyResources(this.tsBtn_Open, "tsBtn_Open");
		this.tsBtn_Open.Name = "tsBtn_Open";
		this.tsBtn_Open.Click += new System.EventHandler(mS_File_Open_Click);
		this.tsBtn_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		resources.ApplyResources(this.tsBtn_Save, "tsBtn_Save");
		this.tsBtn_Save.Name = "tsBtn_Save";
		this.tsBtn_Save.Click += new System.EventHandler(mS_File_Save_Click);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
		this.tsBtn_Com.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		resources.ApplyResources(this.tsBtn_Com, "tsBtn_Com");
		this.tsBtn_Com.Name = "tsBtn_Com";
		this.tsBtn_Read.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		resources.ApplyResources(this.tsBtn_Read, "tsBtn_Read");
		this.tsBtn_Read.Name = "tsBtn_Read";
		this.tsBtn_Read.Click += new System.EventHandler(mS_Program_Read_Click);
		this.tsBtn_Write.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
		resources.ApplyResources(this.tsBtn_Write, "tsBtn_Write");
		this.tsBtn_Write.Name = "tsBtn_Write";
		this.tsBtn_Write.Click += new System.EventHandler(mS_Program_Write_Click);
		this.TreeShow.BackColor = System.Drawing.SystemColors.Control;
		resources.ApplyResources(this.TreeShow, "TreeShow");
		this.TreeShow.Name = "TreeShow";
		this.TreeShow.MouseDown += new System.Windows.Forms.MouseEventHandler(TreeShow_MouseDown);
		this.TreeShow.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(TreeShow_NodeMouseClick);
		resources.ApplyResources(this.splitter1, "splitter1");
		this.splitter1.Name = "splitter1";
		this.splitter1.TabStop = false;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "application-blue.png");
		this.imageList1.Images.SetKeyName(1, "document.ico");
		this.imageList1.Images.SetKeyName(2, "folder.ico");
		this.imageList1.Images.SetKeyName(3, "gear.png");
		this.imageList1.Images.SetKeyName(4, "sphere.png");
		this.imageList1.Images.SetKeyName(5, "alerts.ico");
		this.imageList1.Images.SetKeyName(6, "refresh.png");
		this.imageList1.Images.SetKeyName(7, "Radio-4.png");
		this.imageList1.Images.SetKeyName(8, "tape_drive.png");
		this.imageList1.Images.SetKeyName(9, "lock.png");
		this.imageList1.Images.SetKeyName(10, "circle_blue.png");
		this.imageList1.Images.SetKeyName(11, "Settingsalt2.png");
		this.imageList1.Images.SetKeyName(12, "control-panel.ico");
		this.imageList1.Images.SetKeyName(13, "mail.png");
		this.imageList1.Images.SetKeyName(14, "33grid.png");
		this.imageList1.Images.SetKeyName(15, "keyboard-space.png");
		this.imageList1.Images.SetKeyName(16, "channel.png");
		this.imageList1.Images.SetKeyName(17, "zone.png");
		this.imageList1.Images.SetKeyName(18, "scan.png");
		this.imageList1.Images.SetKeyName(19, "fmc.ico");
		resources.ApplyResources(this.statusStrip1, "statusStrip1");
		this.statusStrip1.Name = "statusStrip1";
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.statusStrip1);
		base.Controls.Add(this.splitter1);
		base.Controls.Add(this.TreeShow);
		base.Controls.Add(this.toolStrip1);
		base.Controls.Add(this.menuStrip1);
		base.IsMdiContainer = true;
		base.KeyPreview = true;
		base.MainMenuStrip = this.menuStrip1;
		base.Name = "FormMain";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(FormMain_Load);
		this.menuStrip1.ResumeLayout(false);
		this.menuStrip1.PerformLayout();
		this.toolStrip1.ResumeLayout(false);
		this.toolStrip1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
