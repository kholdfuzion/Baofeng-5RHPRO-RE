using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using CPS_5RH.Data;

namespace CPS_5RH;

public class Frm_GenralSet : Form
{
	private IContainer components = null;

	private ComboBox Cobox_Save;

	private Label lbl18;

	private Label lbl29;

	private Label lbl30;

	private Label lbl17;

	private ComboBox coBox_Voxlv;

	private Label lbl15;

	private GroupBox grp2;

	private GroupBox grp3;

	private Label lbl10;

	private Label lbl11;

	private ComboBox coBox_Tot;

	private ComboBox coBox_VoiSw;

	private ComboBox coBox_LangSel;

	private ComboBox coBox_Sql;

	private ComboBox coBox_VoxDly;

	private Label lbl32;

	private ComboBox coBox_RecordMode;

	private CheckBox ckBox_Aprs;

	private CheckBox ckBox_Gps;

	private CheckBox ckBox_BT;

	private Label lbl2;

	private ComboBox coBox_ChAMode;

	private Label lbl1;

	private ComboBox coBox_ChBMode;

	private Label lbl4;

	private ComboBox coBox_ChAZone;

	private Label lbl3;

	private ComboBox coBox_ChBZone;

	private Label lbl5;

	private ComboBox coBox_ChADispMode;

	private GroupBox groupBox1;

	private CheckBox ckBox_LoneWork;

	private NumericUpDown Num_LoneworkResp;

	private NumericUpDown Num_LoneworkTim;

	private Label lbl_LworkRsp;

	private Label lbl_LworkTim;

	private Label lbl6;

	private ComboBox coBox_DualMode;

	private ComboBox coBox_MainBand;

	private Label lbl7;

	private Label lbl19;

	private ComboBox Cobox_SaveDly;

	private ComboBox coBox_APO;

	private Label lbl26;

	private ComboBox coBox_Hz1750;

	private Label label5;

	private Label lbl12;

	private Label lbl9;

	private TextBox tBox_PowonFace1;

	private TextBox tBox_PowonPwd;

	private Label lbl8;

	private ComboBox coBox_PwonFace;

	private ComboBox coBox_BusyLock;

	private Label lbl23;

	private ComboBox coBox_TotRe;

	private Label lbl28;

	private ComboBox coBox_PreTot;

	private Label lbl27;

	private GroupBox grp4;

	private ComboBox coBox_Key2Long;

	private Label lbl38;

	private ComboBox coBox_Key2Short;

	private Label lbl37;

	private ComboBox coBox_Key1Long;

	private ComboBox coBox_Key1Short;

	private Label lbl36;

	private Label lbl35;

	private ComboBox coBox_BlightTim;

	private ComboBox coBox_BlightLv;

	private Label lbl14;

	private Label lbl16;

	private Label lbl21;

	private ComboBox coBox_CallEndTone;

	private Label lbl_GpsMode;

	private ComboBox coBox_GpsMode;

	private TabControl Tab_Genset;

	private TabPage tabPage1;

	private TabPage tabPage2;

	private GroupBox groupBox2;

	private GroupBox groupBox3;

	private Label lbl44;

	private ComboBox coBox_BtMic;

	private Label lbl43;

	private ComboBox coBox_BtRx;

	private Label lbl42;

	private ComboBox coBox_BtHold;

	private Label lbl40;

	private TextBox txt_BtPassword;

	private Label lbl41;

	private ComboBox coBox_BtPair;

	private CheckBox ckBox_BtApp;

	private Label lbl45;

	private ComboBox coBox_BtSpk;

	private ComboBox coBox_DaoDi;

	private Label lbl34;

	private ComboBox coBox_NOAA;

	private Label lbl33;

	private ComboBox coBox_Record;

	private Label lbl31;

	private ComboBox coBox_Tail;

	private Label lbl22;

	private Label lbl25;

	private ComboBox coBox_Autokey;

	private Label lbl24;

	private ComboBox coBox_KeyLock;

	private Label lbl20;

	private ComboBox coBox_Tone;

	private TextBox tBox_WRPwd;

	private Label label1;

	private Label lbl_GpsZone;

	private ComboBox coBox_GpsZone;

	private Label lbl_GpsReq;

	private ComboBox coBox_GpsReq;

	private ComboBox coBox_ChBDispMode;

	private Label label2;

	private GroupBox groupBox4;

	private TextBox txt_BtName;

	private Label label3;

	private ComboBox coBox_NOAA_Ch;

	private Label label4;

	private Label label6;

	private ComboBox coBox_GpsID;

	private CheckBox ckBox_VoxSw;

	private ComboBox coBox_Tianqi;

	private Label label7;

	private Label label8;

	private ComboBox coBox_DisDir;

	private CheckBox ckBox_Factory;

	private Label label9;

	private Label label10;

	private Label label11;

	private ComboBox coBox_Enhance;

	private ComboBox coBox_Key4Long;

	private Label lbl_pk4long;

	private ComboBox coBox_Key4Short;

	private Label lbl_pk4short;

	private ComboBox coBox_Key3Long;

	private ComboBox coBox_Key3Short;

	private Label lbl_pk3long;

	private Label lbl_pk3short;

	private DatGenSetInfo tdata = null;

	private DataApp adata = null;

	private bool FlgPage2 = false;

	private static bool initFlg = false;

	private static bool changeFlg = false;

	private static List<string> ZoneAList = new List<string> { "" };

	private static List<string> ZoneBList = new List<string> { "" };

	private static List<string> PbookList = new List<string> { "" };

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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPS_5RH.Frm_GenralSet));
		this.lbl15 = new System.Windows.Forms.Label();
		this.coBox_Voxlv = new System.Windows.Forms.ComboBox();
		this.lbl30 = new System.Windows.Forms.Label();
		this.lbl17 = new System.Windows.Forms.Label();
		this.Cobox_Save = new System.Windows.Forms.ComboBox();
		this.lbl18 = new System.Windows.Forms.Label();
		this.lbl29 = new System.Windows.Forms.Label();
		this.grp2 = new System.Windows.Forms.GroupBox();
		this.coBox_TotRe = new System.Windows.Forms.ComboBox();
		this.lbl28 = new System.Windows.Forms.Label();
		this.coBox_PreTot = new System.Windows.Forms.ComboBox();
		this.lbl27 = new System.Windows.Forms.Label();
		this.coBox_Tot = new System.Windows.Forms.ComboBox();
		this.grp3 = new System.Windows.Forms.GroupBox();
		this.ckBox_VoxSw = new System.Windows.Forms.CheckBox();
		this.coBox_VoxDly = new System.Windows.Forms.ComboBox();
		this.lbl10 = new System.Windows.Forms.Label();
		this.lbl11 = new System.Windows.Forms.Label();
		this.coBox_VoiSw = new System.Windows.Forms.ComboBox();
		this.coBox_LangSel = new System.Windows.Forms.ComboBox();
		this.coBox_Sql = new System.Windows.Forms.ComboBox();
		this.lbl32 = new System.Windows.Forms.Label();
		this.coBox_RecordMode = new System.Windows.Forms.ComboBox();
		this.ckBox_Aprs = new System.Windows.Forms.CheckBox();
		this.ckBox_Gps = new System.Windows.Forms.CheckBox();
		this.ckBox_BT = new System.Windows.Forms.CheckBox();
		this.lbl2 = new System.Windows.Forms.Label();
		this.coBox_ChAMode = new System.Windows.Forms.ComboBox();
		this.lbl1 = new System.Windows.Forms.Label();
		this.coBox_ChBMode = new System.Windows.Forms.ComboBox();
		this.lbl4 = new System.Windows.Forms.Label();
		this.coBox_ChAZone = new System.Windows.Forms.ComboBox();
		this.lbl3 = new System.Windows.Forms.Label();
		this.coBox_ChBZone = new System.Windows.Forms.ComboBox();
		this.lbl5 = new System.Windows.Forms.Label();
		this.coBox_ChADispMode = new System.Windows.Forms.ComboBox();
		this.lbl6 = new System.Windows.Forms.Label();
		this.coBox_DualMode = new System.Windows.Forms.ComboBox();
		this.coBox_MainBand = new System.Windows.Forms.ComboBox();
		this.lbl7 = new System.Windows.Forms.Label();
		this.lbl19 = new System.Windows.Forms.Label();
		this.Cobox_SaveDly = new System.Windows.Forms.ComboBox();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.ckBox_LoneWork = new System.Windows.Forms.CheckBox();
		this.Num_LoneworkResp = new System.Windows.Forms.NumericUpDown();
		this.Num_LoneworkTim = new System.Windows.Forms.NumericUpDown();
		this.lbl_LworkRsp = new System.Windows.Forms.Label();
		this.lbl_LworkTim = new System.Windows.Forms.Label();
		this.coBox_APO = new System.Windows.Forms.ComboBox();
		this.lbl26 = new System.Windows.Forms.Label();
		this.coBox_Hz1750 = new System.Windows.Forms.ComboBox();
		this.label5 = new System.Windows.Forms.Label();
		this.lbl12 = new System.Windows.Forms.Label();
		this.lbl9 = new System.Windows.Forms.Label();
		this.tBox_PowonFace1 = new System.Windows.Forms.TextBox();
		this.tBox_PowonPwd = new System.Windows.Forms.TextBox();
		this.lbl8 = new System.Windows.Forms.Label();
		this.coBox_PwonFace = new System.Windows.Forms.ComboBox();
		this.coBox_BusyLock = new System.Windows.Forms.ComboBox();
		this.lbl23 = new System.Windows.Forms.Label();
		this.grp4 = new System.Windows.Forms.GroupBox();
		this.coBox_Key2Long = new System.Windows.Forms.ComboBox();
		this.lbl38 = new System.Windows.Forms.Label();
		this.coBox_Key2Short = new System.Windows.Forms.ComboBox();
		this.lbl37 = new System.Windows.Forms.Label();
		this.coBox_Key1Long = new System.Windows.Forms.ComboBox();
		this.coBox_Key1Short = new System.Windows.Forms.ComboBox();
		this.lbl36 = new System.Windows.Forms.Label();
		this.lbl35 = new System.Windows.Forms.Label();
		this.coBox_BlightTim = new System.Windows.Forms.ComboBox();
		this.coBox_BlightLv = new System.Windows.Forms.ComboBox();
		this.lbl14 = new System.Windows.Forms.Label();
		this.lbl16 = new System.Windows.Forms.Label();
		this.lbl21 = new System.Windows.Forms.Label();
		this.coBox_CallEndTone = new System.Windows.Forms.ComboBox();
		this.lbl_GpsMode = new System.Windows.Forms.Label();
		this.coBox_GpsMode = new System.Windows.Forms.ComboBox();
		this.Tab_Genset = new System.Windows.Forms.TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.label11 = new System.Windows.Forms.Label();
		this.coBox_Enhance = new System.Windows.Forms.ComboBox();
		this.label8 = new System.Windows.Forms.Label();
		this.coBox_DisDir = new System.Windows.Forms.ComboBox();
		this.coBox_ChBDispMode = new System.Windows.Forms.ComboBox();
		this.label2 = new System.Windows.Forms.Label();
		this.tBox_WRPwd = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.coBox_Tail = new System.Windows.Forms.ComboBox();
		this.lbl22 = new System.Windows.Forms.Label();
		this.lbl25 = new System.Windows.Forms.Label();
		this.coBox_Autokey = new System.Windows.Forms.ComboBox();
		this.lbl24 = new System.Windows.Forms.Label();
		this.coBox_KeyLock = new System.Windows.Forms.ComboBox();
		this.lbl20 = new System.Windows.Forms.Label();
		this.coBox_Tone = new System.Windows.Forms.ComboBox();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.label6 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.coBox_GpsID = new System.Windows.Forms.ComboBox();
		this.ckBox_Factory = new System.Windows.Forms.CheckBox();
		this.label9 = new System.Windows.Forms.Label();
		this.coBox_Tianqi = new System.Windows.Forms.ComboBox();
		this.label7 = new System.Windows.Forms.Label();
		this.coBox_NOAA_Ch = new System.Windows.Forms.ComboBox();
		this.label4 = new System.Windows.Forms.Label();
		this.coBox_DaoDi = new System.Windows.Forms.ComboBox();
		this.lbl34 = new System.Windows.Forms.Label();
		this.coBox_NOAA = new System.Windows.Forms.ComboBox();
		this.lbl33 = new System.Windows.Forms.Label();
		this.coBox_Record = new System.Windows.Forms.ComboBox();
		this.lbl31 = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.lbl45 = new System.Windows.Forms.Label();
		this.ckBox_BtApp = new System.Windows.Forms.CheckBox();
		this.coBox_BtSpk = new System.Windows.Forms.ComboBox();
		this.coBox_BtPair = new System.Windows.Forms.ComboBox();
		this.lbl44 = new System.Windows.Forms.Label();
		this.lbl41 = new System.Windows.Forms.Label();
		this.coBox_BtMic = new System.Windows.Forms.ComboBox();
		this.txt_BtPassword = new System.Windows.Forms.TextBox();
		this.lbl43 = new System.Windows.Forms.Label();
		this.lbl40 = new System.Windows.Forms.Label();
		this.coBox_BtRx = new System.Windows.Forms.ComboBox();
		this.coBox_BtHold = new System.Windows.Forms.ComboBox();
		this.lbl42 = new System.Windows.Forms.Label();
		this.txt_BtName = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.lbl_GpsReq = new System.Windows.Forms.Label();
		this.coBox_GpsReq = new System.Windows.Forms.ComboBox();
		this.lbl_GpsZone = new System.Windows.Forms.Label();
		this.coBox_GpsZone = new System.Windows.Forms.ComboBox();
		this.coBox_Key4Long = new System.Windows.Forms.ComboBox();
		this.lbl_pk4long = new System.Windows.Forms.Label();
		this.coBox_Key4Short = new System.Windows.Forms.ComboBox();
		this.lbl_pk4short = new System.Windows.Forms.Label();
		this.coBox_Key3Long = new System.Windows.Forms.ComboBox();
		this.coBox_Key3Short = new System.Windows.Forms.ComboBox();
		this.lbl_pk3long = new System.Windows.Forms.Label();
		this.lbl_pk3short = new System.Windows.Forms.Label();
		this.grp2.SuspendLayout();
		this.grp3.SuspendLayout();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Num_LoneworkResp).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Num_LoneworkTim).BeginInit();
		this.grp4.SuspendLayout();
		this.Tab_Genset.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.tabPage2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox4.SuspendLayout();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.lbl15.Location = new System.Drawing.Point(57, 24);
		this.lbl15.Name = "lbl15";
		this.lbl15.Size = new System.Drawing.Size(118, 18);
		this.lbl15.TabIndex = 88;
		this.lbl15.Text = "发射超时(s)";
		this.lbl15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_Voxlv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Voxlv.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Voxlv.FormattingEnabled = true;
		this.coBox_Voxlv.Items.AddRange(new object[9] { "1", "2", "3", "4", "5", "6", "7", "8", "9" });
		this.coBox_Voxlv.Location = new System.Drawing.Point(154, 38);
		this.coBox_Voxlv.Name = "coBox_Voxlv";
		this.coBox_Voxlv.Size = new System.Drawing.Size(79, 20);
		this.coBox_Voxlv.TabIndex = 85;
		this.lbl30.Location = new System.Drawing.Point(14, 63);
		this.lbl30.Name = "lbl30";
		this.lbl30.Size = new System.Drawing.Size(135, 18);
		this.lbl30.TabIndex = 76;
		this.lbl30.Text = "声控延迟检测(s)";
		this.lbl30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl17.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl17.Location = new System.Drawing.Point(280, 46);
		this.lbl17.Name = "lbl17";
		this.lbl17.Size = new System.Drawing.Size(122, 18);
		this.lbl17.TabIndex = 72;
		this.lbl17.Text = "静噪等级";
		this.lbl17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.Cobox_Save.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.Cobox_Save.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.Cobox_Save.FormattingEnabled = true;
		this.Cobox_Save.Items.AddRange(new object[4] { "关", "1:1", "1:2", "1:4" });
		this.Cobox_Save.Location = new System.Drawing.Point(408, 71);
		this.Cobox_Save.Name = "Cobox_Save";
		this.Cobox_Save.Size = new System.Drawing.Size(80, 20);
		this.Cobox_Save.TabIndex = 64;
		this.lbl18.Location = new System.Drawing.Point(268, 71);
		this.lbl18.Name = "lbl18";
		this.lbl18.Size = new System.Drawing.Size(134, 18);
		this.lbl18.TabIndex = 63;
		this.lbl18.Text = "省电功能";
		this.lbl18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl29.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.lbl29.Location = new System.Drawing.Point(25, 40);
		this.lbl29.Name = "lbl29";
		this.lbl29.Size = new System.Drawing.Size(124, 18);
		this.lbl29.TabIndex = 61;
		this.lbl29.Text = "声控等级";
		this.lbl29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.grp2.Controls.Add(this.coBox_TotRe);
		this.grp2.Controls.Add(this.lbl28);
		this.grp2.Controls.Add(this.coBox_PreTot);
		this.grp2.Controls.Add(this.lbl27);
		this.grp2.Controls.Add(this.coBox_Tot);
		this.grp2.Controls.Add(this.lbl15);
		this.grp2.Location = new System.Drawing.Point(519, 21);
		this.grp2.Name = "grp2";
		this.grp2.Size = new System.Drawing.Size(269, 99);
		this.grp2.TabIndex = 90;
		this.grp2.TabStop = false;
		this.grp2.Text = "TOT";
		this.coBox_TotRe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_TotRe.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_TotRe.FormattingEnabled = true;
		this.coBox_TotRe.Items.AddRange(new object[7] { "0", "30", "60", "90", "120", "150", "180" });
		this.coBox_TotRe.Location = new System.Drawing.Point(180, 75);
		this.coBox_TotRe.Name = "coBox_TotRe";
		this.coBox_TotRe.Size = new System.Drawing.Size(79, 20);
		this.coBox_TotRe.TabIndex = 108;
		this.coBox_TotRe.Visible = false;
		this.lbl28.Location = new System.Drawing.Point(57, 76);
		this.lbl28.Name = "lbl28";
		this.lbl28.Size = new System.Drawing.Size(118, 18);
		this.lbl28.TabIndex = 107;
		this.lbl28.Text = "超时再键(s)";
		this.lbl28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl28.Visible = false;
		this.coBox_PreTot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_PreTot.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_PreTot.FormattingEnabled = true;
		this.coBox_PreTot.Items.AddRange(new object[11]
		{
			"0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
			"10"
		});
		this.coBox_PreTot.Location = new System.Drawing.Point(180, 49);
		this.coBox_PreTot.Name = "coBox_PreTot";
		this.coBox_PreTot.Size = new System.Drawing.Size(79, 20);
		this.coBox_PreTot.TabIndex = 106;
		this.lbl27.Location = new System.Drawing.Point(57, 50);
		this.lbl27.Name = "lbl27";
		this.lbl27.Size = new System.Drawing.Size(118, 18);
		this.lbl27.TabIndex = 105;
		this.lbl27.Text = "超时预警(s)";
		this.lbl27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_Tot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Tot.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Tot.FormattingEnabled = true;
		this.coBox_Tot.Items.AddRange(new object[15]
		{
			"0", "15", "30", "45", "60", "75", "90", "105", "120", "135",
			"150", "165", "180", "195", "210"
		});
		this.coBox_Tot.Location = new System.Drawing.Point(180, 23);
		this.coBox_Tot.Name = "coBox_Tot";
		this.coBox_Tot.Size = new System.Drawing.Size(79, 20);
		this.coBox_Tot.TabIndex = 104;
		this.grp3.Controls.Add(this.ckBox_VoxSw);
		this.grp3.Controls.Add(this.coBox_VoxDly);
		this.grp3.Controls.Add(this.lbl29);
		this.grp3.Controls.Add(this.coBox_Voxlv);
		this.grp3.Controls.Add(this.lbl30);
		this.grp3.Location = new System.Drawing.Point(519, 142);
		this.grp3.Name = "grp3";
		this.grp3.Size = new System.Drawing.Size(269, 99);
		this.grp3.TabIndex = 91;
		this.grp3.TabStop = false;
		this.grp3.Text = "声控";
		this.ckBox_VoxSw.Location = new System.Drawing.Point(57, 14);
		this.ckBox_VoxSw.Name = "ckBox_VoxSw";
		this.ckBox_VoxSw.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.ckBox_VoxSw.Size = new System.Drawing.Size(110, 21);
		this.ckBox_VoxSw.TabIndex = 125;
		this.ckBox_VoxSw.Text = "启用";
		this.ckBox_VoxSw.UseVisualStyleBackColor = true;
		this.ckBox_VoxSw.CheckedChanged += new System.EventHandler(ckBox_VoxSw_CheckedChanged);
		this.coBox_VoxDly.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_VoxDly.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_VoxDly.FormattingEnabled = true;
		this.coBox_VoxDly.Items.AddRange(new object[19]
		{
			"1.0", "1.5", "2.0", "2.5", "3.0", "3.5", "4.0", "4.5", "5.0", "5.5",
			"6.0", "6.5", "7.0", "7.5", "8.0", "8.5", "9.0", "9.5", "10.0"
		});
		this.coBox_VoxDly.Location = new System.Drawing.Point(154, 63);
		this.coBox_VoxDly.Name = "coBox_VoxDly";
		this.coBox_VoxDly.Size = new System.Drawing.Size(79, 20);
		this.coBox_VoxDly.TabIndex = 88;
		this.lbl10.Location = new System.Drawing.Point(235, 21);
		this.lbl10.Name = "lbl10";
		this.lbl10.Size = new System.Drawing.Size(166, 18);
		this.lbl10.TabIndex = 95;
		this.lbl10.Text = "语音播报";
		this.lbl10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl11.Location = new System.Drawing.Point(291, 171);
		this.lbl11.Name = "lbl11";
		this.lbl11.Size = new System.Drawing.Size(110, 18);
		this.lbl11.TabIndex = 96;
		this.lbl11.Text = "语言选择";
		this.lbl11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_VoiSw.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_VoiSw.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_VoiSw.FormattingEnabled = true;
		this.coBox_VoiSw.Items.AddRange(new object[3] { "关", "中文", "英文" });
		this.coBox_VoiSw.Location = new System.Drawing.Point(407, 21);
		this.coBox_VoiSw.Name = "coBox_VoiSw";
		this.coBox_VoiSw.Size = new System.Drawing.Size(80, 20);
		this.coBox_VoiSw.TabIndex = 105;
		this.coBox_LangSel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_LangSel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_LangSel.FormattingEnabled = true;
		this.coBox_LangSel.Items.AddRange(new object[2] { "英文", "中文" });
		this.coBox_LangSel.Location = new System.Drawing.Point(407, 171);
		this.coBox_LangSel.Name = "coBox_LangSel";
		this.coBox_LangSel.Size = new System.Drawing.Size(80, 20);
		this.coBox_LangSel.TabIndex = 106;
		this.coBox_Sql.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Sql.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Sql.FormattingEnabled = true;
		this.coBox_Sql.Items.AddRange(new object[10] { "关", "1", "2", "3", "4", "5", "6", "7", "8", "9" });
		this.coBox_Sql.Location = new System.Drawing.Point(408, 46);
		this.coBox_Sql.Name = "coBox_Sql";
		this.coBox_Sql.Size = new System.Drawing.Size(80, 20);
		this.coBox_Sql.TabIndex = 108;
		this.lbl32.Location = new System.Drawing.Point(525, 359);
		this.lbl32.Name = "lbl32";
		this.lbl32.Size = new System.Drawing.Size(126, 18);
		this.lbl32.TabIndex = 110;
		this.lbl32.Text = "录音类型";
		this.lbl32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl32.Visible = false;
		this.coBox_RecordMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_RecordMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_RecordMode.FormattingEnabled = true;
		this.coBox_RecordMode.Items.AddRange(new object[3] { "接收", "发射", "全部" });
		this.coBox_RecordMode.Location = new System.Drawing.Point(657, 359);
		this.coBox_RecordMode.Name = "coBox_RecordMode";
		this.coBox_RecordMode.Size = new System.Drawing.Size(80, 20);
		this.coBox_RecordMode.TabIndex = 111;
		this.coBox_RecordMode.Visible = false;
		this.ckBox_Aprs.Enabled = false;
		this.ckBox_Aprs.Location = new System.Drawing.Point(17, 43);
		this.ckBox_Aprs.Name = "ckBox_Aprs";
		this.ckBox_Aprs.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.ckBox_Aprs.Size = new System.Drawing.Size(109, 21);
		this.ckBox_Aprs.TabIndex = 113;
		this.ckBox_Aprs.Text = "APRS";
		this.ckBox_Aprs.UseVisualStyleBackColor = true;
		this.ckBox_Gps.Location = new System.Drawing.Point(17, 19);
		this.ckBox_Gps.Name = "ckBox_Gps";
		this.ckBox_Gps.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.ckBox_Gps.Size = new System.Drawing.Size(109, 21);
		this.ckBox_Gps.TabIndex = 114;
		this.ckBox_Gps.Text = "启用";
		this.ckBox_Gps.UseVisualStyleBackColor = true;
		this.ckBox_Gps.CheckedChanged += new System.EventHandler(ckBox_Gps_CheckedChanged);
		this.ckBox_BT.Location = new System.Drawing.Point(38, 20);
		this.ckBox_BT.Name = "ckBox_BT";
		this.ckBox_BT.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.ckBox_BT.Size = new System.Drawing.Size(110, 21);
		this.ckBox_BT.TabIndex = 124;
		this.ckBox_BT.Text = "启用";
		this.ckBox_BT.UseVisualStyleBackColor = true;
		this.lbl2.Location = new System.Drawing.Point(39, 46);
		this.lbl2.Name = "lbl2";
		this.lbl2.Size = new System.Drawing.Size(108, 18);
		this.lbl2.TabIndex = 141;
		this.lbl2.Text = "下边机工作模式";
		this.lbl2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_ChAMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_ChAMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_ChAMode.FormattingEnabled = true;
		this.coBox_ChAMode.Items.AddRange(new object[2] { "VFO", "MR" });
		this.coBox_ChAMode.Location = new System.Drawing.Point(152, 21);
		this.coBox_ChAMode.Name = "coBox_ChAMode";
		this.coBox_ChAMode.Size = new System.Drawing.Size(80, 20);
		this.coBox_ChAMode.TabIndex = 142;
		this.lbl1.Location = new System.Drawing.Point(39, 21);
		this.lbl1.Name = "lbl1";
		this.lbl1.Size = new System.Drawing.Size(108, 18);
		this.lbl1.TabIndex = 140;
		this.lbl1.Text = "上边机工作模式";
		this.lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_ChBMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_ChBMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_ChBMode.FormattingEnabled = true;
		this.coBox_ChBMode.Items.AddRange(new object[2] { "VFO", "MR" });
		this.coBox_ChBMode.Location = new System.Drawing.Point(152, 46);
		this.coBox_ChBMode.Name = "coBox_ChBMode";
		this.coBox_ChBMode.Size = new System.Drawing.Size(80, 20);
		this.coBox_ChBMode.TabIndex = 143;
		this.lbl4.Location = new System.Drawing.Point(39, 96);
		this.lbl4.Name = "lbl4";
		this.lbl4.Size = new System.Drawing.Size(108, 18);
		this.lbl4.TabIndex = 145;
		this.lbl4.Text = "下边机开机区域号";
		this.lbl4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_ChAZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_ChAZone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_ChAZone.FormattingEnabled = true;
		this.coBox_ChAZone.Location = new System.Drawing.Point(152, 71);
		this.coBox_ChAZone.Name = "coBox_ChAZone";
		this.coBox_ChAZone.Size = new System.Drawing.Size(80, 20);
		this.coBox_ChAZone.TabIndex = 146;
		this.coBox_ChAZone.SelectedIndexChanged += new System.EventHandler(coBox_ChAZone_SelectedIndexChanged);
		this.lbl3.Location = new System.Drawing.Point(39, 71);
		this.lbl3.Name = "lbl3";
		this.lbl3.Size = new System.Drawing.Size(108, 18);
		this.lbl3.TabIndex = 144;
		this.lbl3.Text = "上边机开机区域号";
		this.lbl3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_ChBZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_ChBZone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_ChBZone.FormattingEnabled = true;
		this.coBox_ChBZone.Location = new System.Drawing.Point(152, 96);
		this.coBox_ChBZone.Name = "coBox_ChBZone";
		this.coBox_ChBZone.Size = new System.Drawing.Size(80, 20);
		this.coBox_ChBZone.TabIndex = 147;
		this.coBox_ChBZone.SelectedIndexChanged += new System.EventHandler(coBox_ChBZone_SelectedIndexChanged);
		this.lbl5.Location = new System.Drawing.Point(6, 121);
		this.lbl5.Name = "lbl5";
		this.lbl5.Size = new System.Drawing.Size(138, 18);
		this.lbl5.TabIndex = 148;
		this.lbl5.Text = "上边机信道显示模式";
		this.lbl5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_ChADispMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_ChADispMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_ChADispMode.FormattingEnabled = true;
		this.coBox_ChADispMode.Items.AddRange(new object[4] { "频率", "信道名", "信道号", "频率+名称" });
		this.coBox_ChADispMode.Location = new System.Drawing.Point(152, 121);
		this.coBox_ChADispMode.Name = "coBox_ChADispMode";
		this.coBox_ChADispMode.Size = new System.Drawing.Size(111, 20);
		this.coBox_ChADispMode.TabIndex = 149;
		this.lbl6.Location = new System.Drawing.Point(39, 173);
		this.lbl6.Name = "lbl6";
		this.lbl6.Size = new System.Drawing.Size(108, 18);
		this.lbl6.TabIndex = 150;
		this.lbl6.Text = "双守候模式";
		this.lbl6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_DualMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_DualMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_DualMode.FormattingEnabled = true;
		this.coBox_DualMode.Items.AddRange(new object[3] { "单段单守", "双段双守", "双段单守" });
		this.coBox_DualMode.Location = new System.Drawing.Point(152, 173);
		this.coBox_DualMode.Name = "coBox_DualMode";
		this.coBox_DualMode.Size = new System.Drawing.Size(111, 20);
		this.coBox_DualMode.TabIndex = 151;
		this.coBox_MainBand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_MainBand.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_MainBand.FormattingEnabled = true;
		this.coBox_MainBand.Items.AddRange(new object[2] { "A", "B" });
		this.coBox_MainBand.Location = new System.Drawing.Point(152, 198);
		this.coBox_MainBand.Name = "coBox_MainBand";
		this.coBox_MainBand.Size = new System.Drawing.Size(80, 20);
		this.coBox_MainBand.TabIndex = 157;
		this.lbl7.Location = new System.Drawing.Point(20, 198);
		this.lbl7.Name = "lbl7";
		this.lbl7.Size = new System.Drawing.Size(126, 18);
		this.lbl7.TabIndex = 156;
		this.lbl7.Text = "主频段选择";
		this.lbl7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl19.Location = new System.Drawing.Point(238, 96);
		this.lbl19.Name = "lbl19";
		this.lbl19.Size = new System.Drawing.Size(164, 18);
		this.lbl19.TabIndex = 158;
		this.lbl19.Text = "省电延迟[s]";
		this.lbl19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.Cobox_SaveDly.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.Cobox_SaveDly.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.Cobox_SaveDly.FormattingEnabled = true;
		this.Cobox_SaveDly.Items.AddRange(new object[5] { "5", "10", "15", "20", "25" });
		this.Cobox_SaveDly.Location = new System.Drawing.Point(408, 96);
		this.Cobox_SaveDly.Name = "Cobox_SaveDly";
		this.Cobox_SaveDly.Size = new System.Drawing.Size(80, 20);
		this.Cobox_SaveDly.TabIndex = 159;
		this.groupBox1.Controls.Add(this.ckBox_LoneWork);
		this.groupBox1.Controls.Add(this.Num_LoneworkResp);
		this.groupBox1.Controls.Add(this.Num_LoneworkTim);
		this.groupBox1.Controls.Add(this.lbl_LworkRsp);
		this.groupBox1.Controls.Add(this.lbl_LworkTim);
		this.groupBox1.Location = new System.Drawing.Point(343, 11);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(267, 100);
		this.groupBox1.TabIndex = 160;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "单独工作者";
		this.ckBox_LoneWork.Location = new System.Drawing.Point(68, 16);
		this.ckBox_LoneWork.Name = "ckBox_LoneWork";
		this.ckBox_LoneWork.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.ckBox_LoneWork.Size = new System.Drawing.Size(126, 16);
		this.ckBox_LoneWork.TabIndex = 123;
		this.ckBox_LoneWork.Text = "启用";
		this.ckBox_LoneWork.UseVisualStyleBackColor = true;
		this.ckBox_LoneWork.CheckedChanged += new System.EventHandler(ckBox_LoneWork_CheckedChanged);
		this.Num_LoneworkResp.Location = new System.Drawing.Point(180, 38);
		this.Num_LoneworkResp.Maximum = new decimal(new int[4] { 255, 0, 0, 0 });
		this.Num_LoneworkResp.Name = "Num_LoneworkResp";
		this.Num_LoneworkResp.Size = new System.Drawing.Size(79, 21);
		this.Num_LoneworkResp.TabIndex = 122;
		this.Num_LoneworkResp.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.Num_LoneworkTim.Location = new System.Drawing.Point(180, 66);
		this.Num_LoneworkTim.Maximum = new decimal(new int[4] { 255, 0, 0, 0 });
		this.Num_LoneworkTim.Name = "Num_LoneworkTim";
		this.Num_LoneworkTim.Size = new System.Drawing.Size(79, 21);
		this.Num_LoneworkTim.TabIndex = 121;
		this.Num_LoneworkTim.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.lbl_LworkRsp.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.lbl_LworkRsp.Location = new System.Drawing.Point(15, 40);
		this.lbl_LworkRsp.Name = "lbl_LworkRsp";
		this.lbl_LworkRsp.Size = new System.Drawing.Size(158, 18);
		this.lbl_LworkRsp.TabIndex = 61;
		this.lbl_LworkRsp.Text = "响应时间[min]";
		this.lbl_LworkRsp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl_LworkTim.Location = new System.Drawing.Point(25, 68);
		this.lbl_LworkTim.Name = "lbl_LworkTim";
		this.lbl_LworkTim.Size = new System.Drawing.Size(149, 18);
		this.lbl_LworkTim.TabIndex = 76;
		this.lbl_LworkTim.Text = "提醒时间[s]";
		this.lbl_LworkTim.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_APO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_APO.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_APO.FormattingEnabled = true;
		this.coBox_APO.Items.AddRange(new object[6] { "关", "30", "60", "120", "240", "480" });
		this.coBox_APO.Location = new System.Drawing.Point(408, 299);
		this.coBox_APO.Name = "coBox_APO";
		this.coBox_APO.Size = new System.Drawing.Size(80, 20);
		this.coBox_APO.TabIndex = 161;
		this.lbl26.Location = new System.Drawing.Point(291, 299);
		this.lbl26.Name = "lbl26";
		this.lbl26.Size = new System.Drawing.Size(111, 18);
		this.lbl26.TabIndex = 160;
		this.lbl26.Text = "自动关机(分钟)";
		this.lbl26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_Hz1750.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Hz1750.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Hz1750.FormattingEnabled = true;
		this.coBox_Hz1750.Items.AddRange(new object[4] { "1000Hz", "1450Hz", "1750Hz", "2100Hz" });
		this.coBox_Hz1750.Location = new System.Drawing.Point(169, 59);
		this.coBox_Hz1750.Name = "coBox_Hz1750";
		this.coBox_Hz1750.Size = new System.Drawing.Size(80, 20);
		this.coBox_Hz1750.TabIndex = 163;
		this.label5.Location = new System.Drawing.Point(52, 59);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(111, 18);
		this.label5.TabIndex = 162;
		this.label5.Text = "1750Hz";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl12.Location = new System.Drawing.Point(16, 273);
		this.lbl12.Name = "lbl12";
		this.lbl12.Size = new System.Drawing.Size(131, 18);
		this.lbl12.TabIndex = 167;
		this.lbl12.Text = "开机字符显示";
		this.lbl12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl9.Location = new System.Drawing.Point(38, 248);
		this.lbl9.Name = "lbl9";
		this.lbl9.Size = new System.Drawing.Size(108, 18);
		this.lbl9.TabIndex = 166;
		this.lbl9.Text = "开机画面";
		this.lbl9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tBox_PowonFace1.Location = new System.Drawing.Point(150, 273);
		this.tBox_PowonFace1.MaxLength = 16;
		this.tBox_PowonFace1.Name = "tBox_PowonFace1";
		this.tBox_PowonFace1.Size = new System.Drawing.Size(128, 21);
		this.tBox_PowonFace1.TabIndex = 168;
		this.tBox_PowonFace1.TextChanged += new System.EventHandler(tBox_RadioName_TextChanged);
		this.tBox_PowonPwd.Location = new System.Drawing.Point(152, 223);
		this.tBox_PowonPwd.MaxLength = 8;
		this.tBox_PowonPwd.Name = "tBox_PowonPwd";
		this.tBox_PowonPwd.ShortcutsEnabled = false;
		this.tBox_PowonPwd.Size = new System.Drawing.Size(80, 21);
		this.tBox_PowonPwd.TabIndex = 164;
		this.tBox_PowonPwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tB_RadioID_KeyPress);
		this.lbl8.Location = new System.Drawing.Point(38, 223);
		this.lbl8.Name = "lbl8";
		this.lbl8.Size = new System.Drawing.Size(108, 18);
		this.lbl8.TabIndex = 165;
		this.lbl8.Text = "开机密码";
		this.lbl8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_PwonFace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_PwonFace.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_PwonFace.FormattingEnabled = true;
		this.coBox_PwonFace.Items.AddRange(new object[3] { "图片", "字符", "电池电压" });
		this.coBox_PwonFace.Location = new System.Drawing.Point(152, 248);
		this.coBox_PwonFace.Name = "coBox_PwonFace";
		this.coBox_PwonFace.Size = new System.Drawing.Size(80, 20);
		this.coBox_PwonFace.TabIndex = 169;
		this.coBox_BusyLock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_BusyLock.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_BusyLock.FormattingEnabled = true;
		this.coBox_BusyLock.Items.AddRange(new object[2] { "关", "开" });
		this.coBox_BusyLock.Location = new System.Drawing.Point(407, 221);
		this.coBox_BusyLock.Name = "coBox_BusyLock";
		this.coBox_BusyLock.Size = new System.Drawing.Size(80, 20);
		this.coBox_BusyLock.TabIndex = 171;
		this.lbl23.Location = new System.Drawing.Point(329, 221);
		this.lbl23.Name = "lbl23";
		this.lbl23.Size = new System.Drawing.Size(72, 18);
		this.lbl23.TabIndex = 170;
		this.lbl23.Text = "繁忙禁发";
		this.lbl23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.grp4.Controls.Add(this.coBox_Key4Long);
		this.grp4.Controls.Add(this.lbl_pk4long);
		this.grp4.Controls.Add(this.coBox_Key4Short);
		this.grp4.Controls.Add(this.lbl_pk4short);
		this.grp4.Controls.Add(this.coBox_Key3Long);
		this.grp4.Controls.Add(this.coBox_Key3Short);
		this.grp4.Controls.Add(this.lbl_pk3long);
		this.grp4.Controls.Add(this.lbl_pk3short);
		this.grp4.Controls.Add(this.coBox_Key2Long);
		this.grp4.Controls.Add(this.lbl38);
		this.grp4.Controls.Add(this.coBox_Key2Short);
		this.grp4.Controls.Add(this.lbl37);
		this.grp4.Controls.Add(this.coBox_Key1Long);
		this.grp4.Controls.Add(this.coBox_Key1Short);
		this.grp4.Controls.Add(this.lbl36);
		this.grp4.Controls.Add(this.lbl35);
		this.grp4.Location = new System.Drawing.Point(39, 202);
		this.grp4.Name = "grp4";
		this.grp4.Size = new System.Drawing.Size(267, 232);
		this.grp4.TabIndex = 92;
		this.grp4.TabStop = false;
		this.grp4.Text = "可编程键";
		this.coBox_Key2Long.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Key2Long.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Key2Long.FormattingEnabled = true;
		this.coBox_Key2Long.Items.AddRange(new object[14]
		{
			"无", "扫描键", "监听键", "手电灯", "收音机", "紧急警报", "定位系统", "一键扫频", "蓝牙", "1750Hz",
			"倒频", "脱网", "倒地告警", "一键呼叫"
		});
		this.coBox_Key2Long.Location = new System.Drawing.Point(121, 90);
		this.coBox_Key2Long.Name = "coBox_Key2Long";
		this.coBox_Key2Long.Size = new System.Drawing.Size(138, 20);
		this.coBox_Key2Long.TabIndex = 179;
		this.coBox_Key2Long.SelectedIndexChanged += new System.EventHandler(coBox_Key2Long_SelectedIndexChanged);
		this.lbl38.Location = new System.Drawing.Point(10, 90);
		this.lbl38.Name = "lbl38";
		this.lbl38.Size = new System.Drawing.Size(110, 18);
		this.lbl38.TabIndex = 178;
		this.lbl38.Text = "PF2 键长按";
		this.lbl38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_Key2Short.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Key2Short.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Key2Short.FormattingEnabled = true;
		this.coBox_Key2Short.Items.AddRange(new object[14]
		{
			"无", "扫描键", "监听键", "手电灯", "收音机", "紧急警报", "定位系统", "一键扫频", "蓝牙", "1750Hz",
			"倒频", "脱网", "倒地告警", "一键呼叫"
		});
		this.coBox_Key2Short.Location = new System.Drawing.Point(121, 65);
		this.coBox_Key2Short.Name = "coBox_Key2Short";
		this.coBox_Key2Short.Size = new System.Drawing.Size(138, 20);
		this.coBox_Key2Short.TabIndex = 177;
		this.coBox_Key2Short.SelectedIndexChanged += new System.EventHandler(coBox_Key2Short_SelectedIndexChanged);
		this.lbl37.Location = new System.Drawing.Point(10, 65);
		this.lbl37.Name = "lbl37";
		this.lbl37.Size = new System.Drawing.Size(110, 18);
		this.lbl37.TabIndex = 176;
		this.lbl37.Text = "PF2 键短按";
		this.lbl37.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_Key1Long.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Key1Long.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Key1Long.FormattingEnabled = true;
		this.coBox_Key1Long.Items.AddRange(new object[14]
		{
			"无", "扫描键", "监听键", "手电灯", "收音机", "紧急警报", "定位系统", "一键扫频", "蓝牙", "1750Hz",
			"倒频", "脱网", "倒地告警", "一键呼叫"
		});
		this.coBox_Key1Long.Location = new System.Drawing.Point(121, 40);
		this.coBox_Key1Long.Name = "coBox_Key1Long";
		this.coBox_Key1Long.Size = new System.Drawing.Size(138, 20);
		this.coBox_Key1Long.TabIndex = 175;
		this.coBox_Key1Long.SelectedIndexChanged += new System.EventHandler(coBox_Key1Long_SelectedIndexChanged);
		this.coBox_Key1Short.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Key1Short.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Key1Short.FormattingEnabled = true;
		this.coBox_Key1Short.ItemHeight = 12;
		this.coBox_Key1Short.Items.AddRange(new object[14]
		{
			"无", "扫描键", "监听键", "手电灯", "收音机", "紧急警报", "定位系统", "一键扫频", "蓝牙", "1750Hz",
			"倒频", "脱网", "倒地告警", "一键呼叫"
		});
		this.coBox_Key1Short.Location = new System.Drawing.Point(121, 15);
		this.coBox_Key1Short.Name = "coBox_Key1Short";
		this.coBox_Key1Short.Size = new System.Drawing.Size(138, 20);
		this.coBox_Key1Short.TabIndex = 174;
		this.coBox_Key1Short.SelectedIndexChanged += new System.EventHandler(coBox_Key1Short_SelectedIndexChanged);
		this.lbl36.Location = new System.Drawing.Point(10, 40);
		this.lbl36.Name = "lbl36";
		this.lbl36.Size = new System.Drawing.Size(110, 18);
		this.lbl36.TabIndex = 173;
		this.lbl36.Text = "PF1 键长按";
		this.lbl36.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl35.Location = new System.Drawing.Point(8, 15);
		this.lbl35.Name = "lbl35";
		this.lbl35.Size = new System.Drawing.Size(113, 18);
		this.lbl35.TabIndex = 172;
		this.lbl35.Text = "PF1 键短按";
		this.lbl35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_BlightTim.DropDownHeight = 120;
		this.coBox_BlightTim.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_BlightTim.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_BlightTim.FormattingEnabled = true;
		this.coBox_BlightTim.IntegralHeight = false;
		this.coBox_BlightTim.Items.AddRange(new object[27]
		{
			"常亮", "5", "6", "7", "8", "9", "10", "11", "12", "13",
			"14", "15", "16", "17", "18", "19", "20", "21", "22", "23",
			"24", "25", "26", "27", "28", "29", "30"
		});
		this.coBox_BlightTim.Location = new System.Drawing.Point(150, 302);
		this.coBox_BlightTim.Name = "coBox_BlightTim";
		this.coBox_BlightTim.Size = new System.Drawing.Size(80, 20);
		this.coBox_BlightTim.TabIndex = 172;
		this.coBox_BlightLv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_BlightLv.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_BlightLv.FormattingEnabled = true;
		this.coBox_BlightLv.Items.AddRange(new object[5] { "1", "2", "3", "4", "5" });
		this.coBox_BlightLv.Location = new System.Drawing.Point(150, 327);
		this.coBox_BlightLv.Name = "coBox_BlightLv";
		this.coBox_BlightLv.Size = new System.Drawing.Size(80, 20);
		this.coBox_BlightLv.TabIndex = 173;
		this.lbl14.Location = new System.Drawing.Point(18, 302);
		this.lbl14.Name = "lbl14";
		this.lbl14.Size = new System.Drawing.Size(126, 18);
		this.lbl14.TabIndex = 174;
		this.lbl14.Text = "背光灯时间[s]";
		this.lbl14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl16.Location = new System.Drawing.Point(18, 327);
		this.lbl16.Name = "lbl16";
		this.lbl16.Size = new System.Drawing.Size(127, 18);
		this.lbl16.TabIndex = 175;
		this.lbl16.Text = "背光灯亮度";
		this.lbl16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl21.Location = new System.Drawing.Point(282, 147);
		this.lbl21.Name = "lbl21";
		this.lbl21.Size = new System.Drawing.Size(121, 18);
		this.lbl21.TabIndex = 178;
		this.lbl21.Text = "通话结束音";
		this.lbl21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_CallEndTone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_CallEndTone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_CallEndTone.FormattingEnabled = true;
		this.coBox_CallEndTone.Items.AddRange(new object[4] { "关", "模式1", "模式2", "模式3" });
		this.coBox_CallEndTone.Location = new System.Drawing.Point(408, 147);
		this.coBox_CallEndTone.Name = "coBox_CallEndTone";
		this.coBox_CallEndTone.Size = new System.Drawing.Size(80, 20);
		this.coBox_CallEndTone.TabIndex = 177;
		this.lbl_GpsMode.Location = new System.Drawing.Point(32, 116);
		this.lbl_GpsMode.Name = "lbl_GpsMode";
		this.lbl_GpsMode.Size = new System.Drawing.Size(92, 18);
		this.lbl_GpsMode.TabIndex = 180;
		this.lbl_GpsMode.Text = "定位模式";
		this.lbl_GpsMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl_GpsMode.Visible = false;
		this.coBox_GpsMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_GpsMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_GpsMode.FormattingEnabled = true;
		this.coBox_GpsMode.Items.AddRange(new object[3] { "GPS", "北斗", "GPS+北斗" });
		this.coBox_GpsMode.Location = new System.Drawing.Point(129, 116);
		this.coBox_GpsMode.Name = "coBox_GpsMode";
		this.coBox_GpsMode.Size = new System.Drawing.Size(80, 20);
		this.coBox_GpsMode.TabIndex = 179;
		this.coBox_GpsMode.Visible = false;
		this.Tab_Genset.Controls.Add(this.tabPage1);
		this.Tab_Genset.Controls.Add(this.tabPage2);
		this.Tab_Genset.Location = new System.Drawing.Point(12, 12);
		this.Tab_Genset.Name = "Tab_Genset";
		this.Tab_Genset.SelectedIndex = 0;
		this.Tab_Genset.Size = new System.Drawing.Size(818, 475);
		this.Tab_Genset.TabIndex = 181;
		this.Tab_Genset.SelectedIndexChanged += new System.EventHandler(Tab_Genset_SelectedIndexChanged);
		this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
		this.tabPage1.Controls.Add(this.label11);
		this.tabPage1.Controls.Add(this.coBox_Enhance);
		this.tabPage1.Controls.Add(this.label8);
		this.tabPage1.Controls.Add(this.coBox_DisDir);
		this.tabPage1.Controls.Add(this.coBox_ChBDispMode);
		this.tabPage1.Controls.Add(this.label2);
		this.tabPage1.Controls.Add(this.tBox_WRPwd);
		this.tabPage1.Controls.Add(this.label1);
		this.tabPage1.Controls.Add(this.coBox_Tail);
		this.tabPage1.Controls.Add(this.lbl22);
		this.tabPage1.Controls.Add(this.lbl25);
		this.tabPage1.Controls.Add(this.coBox_Autokey);
		this.tabPage1.Controls.Add(this.lbl24);
		this.tabPage1.Controls.Add(this.coBox_KeyLock);
		this.tabPage1.Controls.Add(this.lbl20);
		this.tabPage1.Controls.Add(this.coBox_Tone);
		this.tabPage1.Controls.Add(this.lbl21);
		this.tabPage1.Controls.Add(this.grp2);
		this.tabPage1.Controls.Add(this.coBox_CallEndTone);
		this.tabPage1.Controls.Add(this.grp3);
		this.tabPage1.Controls.Add(this.coBox_Sql);
		this.tabPage1.Controls.Add(this.lbl16);
		this.tabPage1.Controls.Add(this.lbl17);
		this.tabPage1.Controls.Add(this.lbl14);
		this.tabPage1.Controls.Add(this.lbl10);
		this.tabPage1.Controls.Add(this.coBox_BlightLv);
		this.tabPage1.Controls.Add(this.lbl11);
		this.tabPage1.Controls.Add(this.coBox_BlightTim);
		this.tabPage1.Controls.Add(this.coBox_VoiSw);
		this.tabPage1.Controls.Add(this.coBox_LangSel);
		this.tabPage1.Controls.Add(this.lbl18);
		this.tabPage1.Controls.Add(this.coBox_BusyLock);
		this.tabPage1.Controls.Add(this.Cobox_Save);
		this.tabPage1.Controls.Add(this.lbl23);
		this.tabPage1.Controls.Add(this.lbl12);
		this.tabPage1.Controls.Add(this.lbl9);
		this.tabPage1.Controls.Add(this.coBox_ChBMode);
		this.tabPage1.Controls.Add(this.tBox_PowonFace1);
		this.tabPage1.Controls.Add(this.lbl1);
		this.tabPage1.Controls.Add(this.coBox_ChAMode);
		this.tabPage1.Controls.Add(this.tBox_PowonPwd);
		this.tabPage1.Controls.Add(this.lbl2);
		this.tabPage1.Controls.Add(this.lbl8);
		this.tabPage1.Controls.Add(this.coBox_ChBZone);
		this.tabPage1.Controls.Add(this.coBox_PwonFace);
		this.tabPage1.Controls.Add(this.lbl3);
		this.tabPage1.Controls.Add(this.coBox_ChAZone);
		this.tabPage1.Controls.Add(this.lbl4);
		this.tabPage1.Controls.Add(this.coBox_APO);
		this.tabPage1.Controls.Add(this.coBox_ChADispMode);
		this.tabPage1.Controls.Add(this.lbl26);
		this.tabPage1.Controls.Add(this.lbl5);
		this.tabPage1.Controls.Add(this.Cobox_SaveDly);
		this.tabPage1.Controls.Add(this.coBox_DualMode);
		this.tabPage1.Controls.Add(this.lbl19);
		this.tabPage1.Controls.Add(this.lbl6);
		this.tabPage1.Controls.Add(this.coBox_MainBand);
		this.tabPage1.Controls.Add(this.lbl7);
		this.tabPage1.Location = new System.Drawing.Point(4, 22);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(810, 398);
		this.tabPage1.TabIndex = 0;
		this.tabPage1.Text = "普通设置";
		this.label11.Location = new System.Drawing.Point(282, 350);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(120, 18);
		this.label11.TabIndex = 194;
		this.label11.Text = "增强功能";
		this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_Enhance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Enhance.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Enhance.FormattingEnabled = true;
		this.coBox_Enhance.Items.AddRange(new object[2] { "正向", "倒向" });
		this.coBox_Enhance.Location = new System.Drawing.Point(407, 350);
		this.coBox_Enhance.Name = "coBox_Enhance";
		this.coBox_Enhance.Size = new System.Drawing.Size(80, 20);
		this.coBox_Enhance.TabIndex = 193;
		this.label8.Location = new System.Drawing.Point(282, 324);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(120, 18);
		this.label8.TabIndex = 192;
		this.label8.Text = "显示方向";
		this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_DisDir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_DisDir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_DisDir.FormattingEnabled = true;
		this.coBox_DisDir.Items.AddRange(new object[2] { "正向", "倒向" });
		this.coBox_DisDir.Location = new System.Drawing.Point(407, 324);
		this.coBox_DisDir.Name = "coBox_DisDir";
		this.coBox_DisDir.Size = new System.Drawing.Size(80, 20);
		this.coBox_DisDir.TabIndex = 191;
		this.coBox_ChBDispMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_ChBDispMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_ChBDispMode.FormattingEnabled = true;
		this.coBox_ChBDispMode.Items.AddRange(new object[4] { "频率", "信道名", "信道号", "频率+名称" });
		this.coBox_ChBDispMode.Location = new System.Drawing.Point(152, 147);
		this.coBox_ChBDispMode.Name = "coBox_ChBDispMode";
		this.coBox_ChBDispMode.Size = new System.Drawing.Size(111, 20);
		this.coBox_ChBDispMode.TabIndex = 190;
		this.label2.Location = new System.Drawing.Point(6, 147);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(138, 18);
		this.label2.TabIndex = 189;
		this.label2.Text = "下边机信道显示模式";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tBox_WRPwd.Location = new System.Drawing.Point(150, 353);
		this.tBox_WRPwd.MaxLength = 8;
		this.tBox_WRPwd.Name = "tBox_WRPwd";
		this.tBox_WRPwd.ShortcutsEnabled = false;
		this.tBox_WRPwd.Size = new System.Drawing.Size(80, 21);
		this.tBox_WRPwd.TabIndex = 187;
		this.tBox_WRPwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tB_RadioID_KeyPress);
		this.label1.Location = new System.Drawing.Point(36, 353);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(108, 18);
		this.label1.TabIndex = 188;
		this.label1.Text = "读写频密码";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_Tail.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Tail.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Tail.FormattingEnabled = true;
		this.coBox_Tail.Items.AddRange(new object[5] { "关闭", "55Hz", "120°", "180°", "240°" });
		this.coBox_Tail.Location = new System.Drawing.Point(408, 197);
		this.coBox_Tail.Name = "coBox_Tail";
		this.coBox_Tail.Size = new System.Drawing.Size(80, 20);
		this.coBox_Tail.TabIndex = 186;
		this.lbl22.Location = new System.Drawing.Point(330, 197);
		this.lbl22.Name = "lbl22";
		this.lbl22.Size = new System.Drawing.Size(72, 18);
		this.lbl22.TabIndex = 185;
		this.lbl22.Text = "尾音消除";
		this.lbl22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl25.Location = new System.Drawing.Point(284, 273);
		this.lbl25.Name = "lbl25";
		this.lbl25.Size = new System.Drawing.Size(118, 18);
		this.lbl25.TabIndex = 184;
		this.lbl25.Text = "自动键盘锁";
		this.lbl25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_Autokey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Autokey.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Autokey.FormattingEnabled = true;
		this.coBox_Autokey.Items.AddRange(new object[2] { "关", "开" });
		this.coBox_Autokey.Location = new System.Drawing.Point(407, 273);
		this.coBox_Autokey.Name = "coBox_Autokey";
		this.coBox_Autokey.Size = new System.Drawing.Size(80, 20);
		this.coBox_Autokey.TabIndex = 183;
		this.lbl24.Location = new System.Drawing.Point(309, 247);
		this.lbl24.Name = "lbl24";
		this.lbl24.Size = new System.Drawing.Size(93, 18);
		this.lbl24.TabIndex = 182;
		this.lbl24.Text = "键盘锁定";
		this.lbl24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl24.Visible = false;
		this.coBox_KeyLock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_KeyLock.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_KeyLock.FormattingEnabled = true;
		this.coBox_KeyLock.Items.AddRange(new object[2] { "关", "开" });
		this.coBox_KeyLock.Location = new System.Drawing.Point(407, 247);
		this.coBox_KeyLock.Name = "coBox_KeyLock";
		this.coBox_KeyLock.Size = new System.Drawing.Size(80, 20);
		this.coBox_KeyLock.TabIndex = 181;
		this.coBox_KeyLock.Visible = false;
		this.lbl20.Location = new System.Drawing.Point(331, 121);
		this.lbl20.Name = "lbl20";
		this.lbl20.Size = new System.Drawing.Size(72, 18);
		this.lbl20.TabIndex = 180;
		this.lbl20.Text = "提示音";
		this.lbl20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_Tone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Tone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Tone.FormattingEnabled = true;
		this.coBox_Tone.Items.AddRange(new object[2] { "关", "开" });
		this.coBox_Tone.Location = new System.Drawing.Point(408, 121);
		this.coBox_Tone.Name = "coBox_Tone";
		this.coBox_Tone.Size = new System.Drawing.Size(80, 20);
		this.coBox_Tone.TabIndex = 179;
		this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
		this.tabPage2.Controls.Add(this.label6);
		this.tabPage2.Controls.Add(this.label10);
		this.tabPage2.Controls.Add(this.coBox_GpsID);
		this.tabPage2.Controls.Add(this.ckBox_Factory);
		this.tabPage2.Controls.Add(this.label9);
		this.tabPage2.Controls.Add(this.coBox_Tianqi);
		this.tabPage2.Controls.Add(this.label7);
		this.tabPage2.Controls.Add(this.coBox_NOAA_Ch);
		this.tabPage2.Controls.Add(this.label4);
		this.tabPage2.Controls.Add(this.grp4);
		this.tabPage2.Controls.Add(this.coBox_Hz1750);
		this.tabPage2.Controls.Add(this.label5);
		this.tabPage2.Controls.Add(this.coBox_DaoDi);
		this.tabPage2.Controls.Add(this.lbl34);
		this.tabPage2.Controls.Add(this.coBox_NOAA);
		this.tabPage2.Controls.Add(this.lbl33);
		this.tabPage2.Controls.Add(this.coBox_Record);
		this.tabPage2.Controls.Add(this.lbl31);
		this.tabPage2.Controls.Add(this.groupBox3);
		this.tabPage2.Controls.Add(this.coBox_RecordMode);
		this.tabPage2.Controls.Add(this.lbl32);
		this.tabPage2.Controls.Add(this.groupBox2);
		this.tabPage2.Controls.Add(this.groupBox1);
		this.tabPage2.Location = new System.Drawing.Point(4, 22);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(810, 449);
		this.tabPage2.TabIndex = 1;
		this.tabPage2.Text = "高级设置和可编程键";
		this.label6.Location = new System.Drawing.Point(54, 36);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(110, 18);
		this.label6.TabIndex = 188;
		this.label6.Text = "联系人ID";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label10.Location = new System.Drawing.Point(185, 10);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(152, 20);
		this.label10.TabIndex = 190;
		this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.coBox_GpsID.DropDownHeight = 120;
		this.coBox_GpsID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_GpsID.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_GpsID.FormattingEnabled = true;
		this.coBox_GpsID.IntegralHeight = false;
		this.coBox_GpsID.Items.AddRange(new object[80]
		{
			"1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
			"11", "12", "13", "14", "15", "16", "17", "18", "19", "20",
			"21", "22", "23", "24", "25", "26", "27", "28", "29", "30",
			"31", "32", "33", "34", "35", "36", "37", "38", "39", "40",
			"41", "42", "43", "44", "45", "46", "47", "48", "49", "50",
			"51", "52", "53", "54", "55", "56", "57", "58", "59", "60",
			"61", "62", "63", "64", "65", "66", "67", "68", "69", "70",
			"71", "72", "73", "74", "75", "76", "77", "78", "79", "80"
		});
		this.coBox_GpsID.Location = new System.Drawing.Point(169, 34);
		this.coBox_GpsID.Name = "coBox_GpsID";
		this.coBox_GpsID.Size = new System.Drawing.Size(80, 20);
		this.coBox_GpsID.TabIndex = 187;
		this.coBox_GpsID.SelectedIndexChanged += new System.EventHandler(coBox_GpsID_SelectedIndexChanged);
		this.ckBox_Factory.AutoSize = true;
		this.ckBox_Factory.Location = new System.Drawing.Point(169, 14);
		this.ckBox_Factory.Name = "ckBox_Factory";
		this.ckBox_Factory.Size = new System.Drawing.Size(15, 14);
		this.ckBox_Factory.TabIndex = 189;
		this.ckBox_Factory.UseVisualStyleBackColor = true;
		this.ckBox_Factory.CheckedChanged += new System.EventHandler(ckBox_Factory_CheckedChanged);
		this.label9.Location = new System.Drawing.Point(52, 11);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(111, 18);
		this.label9.TabIndex = 188;
		this.label9.Text = "工程模式";
		this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_Tianqi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Tianqi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Tianqi.FormattingEnabled = true;
		this.coBox_Tianqi.Items.AddRange(new object[2] { "关", "开" });
		this.coBox_Tianqi.Location = new System.Drawing.Point(169, 133);
		this.coBox_Tianqi.Name = "coBox_Tianqi";
		this.coBox_Tianqi.Size = new System.Drawing.Size(80, 20);
		this.coBox_Tianqi.TabIndex = 187;
		this.label7.Location = new System.Drawing.Point(37, 133);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(126, 18);
		this.label7.TabIndex = 186;
		this.label7.Text = "天气报警";
		this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_NOAA_Ch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_NOAA_Ch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_NOAA_Ch.FormattingEnabled = true;
		this.coBox_NOAA_Ch.Items.AddRange(new object[10] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" });
		this.coBox_NOAA_Ch.Location = new System.Drawing.Point(169, 109);
		this.coBox_NOAA_Ch.Name = "coBox_NOAA_Ch";
		this.coBox_NOAA_Ch.Size = new System.Drawing.Size(80, 20);
		this.coBox_NOAA_Ch.TabIndex = 185;
		this.label4.Location = new System.Drawing.Point(37, 109);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(126, 18);
		this.label4.TabIndex = 184;
		this.label4.Text = "NOAA报警信道";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_DaoDi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_DaoDi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_DaoDi.FormattingEnabled = true;
		this.coBox_DaoDi.Items.AddRange(new object[2] { "关", "开" });
		this.coBox_DaoDi.Location = new System.Drawing.Point(169, 159);
		this.coBox_DaoDi.Name = "coBox_DaoDi";
		this.coBox_DaoDi.Size = new System.Drawing.Size(80, 20);
		this.coBox_DaoDi.TabIndex = 183;
		this.lbl34.Location = new System.Drawing.Point(37, 159);
		this.lbl34.Name = "lbl34";
		this.lbl34.Size = new System.Drawing.Size(126, 18);
		this.lbl34.TabIndex = 182;
		this.lbl34.Text = "倒地告警";
		this.lbl34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_NOAA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_NOAA.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_NOAA.FormattingEnabled = true;
		this.coBox_NOAA.Items.AddRange(new object[2] { "关", "开" });
		this.coBox_NOAA.Location = new System.Drawing.Point(169, 84);
		this.coBox_NOAA.Name = "coBox_NOAA";
		this.coBox_NOAA.Size = new System.Drawing.Size(80, 20);
		this.coBox_NOAA.TabIndex = 181;
		this.coBox_NOAA.SelectedIndexChanged += new System.EventHandler(coBox_NOAA_SelectedIndexChanged);
		this.lbl33.Location = new System.Drawing.Point(37, 84);
		this.lbl33.Name = "lbl33";
		this.lbl33.Size = new System.Drawing.Size(126, 18);
		this.lbl33.TabIndex = 180;
		this.lbl33.Text = "NOAA报警";
		this.lbl33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_Record.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Record.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Record.FormattingEnabled = true;
		this.coBox_Record.Items.AddRange(new object[2] { "关", "开" });
		this.coBox_Record.Location = new System.Drawing.Point(657, 333);
		this.coBox_Record.Name = "coBox_Record";
		this.coBox_Record.Size = new System.Drawing.Size(80, 20);
		this.coBox_Record.TabIndex = 179;
		this.coBox_Record.Visible = false;
		this.lbl31.Location = new System.Drawing.Point(525, 333);
		this.lbl31.Name = "lbl31";
		this.lbl31.Size = new System.Drawing.Size(126, 18);
		this.lbl31.TabIndex = 178;
		this.lbl31.Text = "录音功能";
		this.lbl31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl31.Visible = false;
		this.groupBox3.Controls.Add(this.groupBox4);
		this.groupBox3.Controls.Add(this.txt_BtName);
		this.groupBox3.Controls.Add(this.label3);
		this.groupBox3.Controls.Add(this.ckBox_BT);
		this.groupBox3.Location = new System.Drawing.Point(343, 117);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(267, 82);
		this.groupBox3.TabIndex = 177;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "蓝牙";
		this.groupBox4.Controls.Add(this.lbl45);
		this.groupBox4.Controls.Add(this.ckBox_BtApp);
		this.groupBox4.Controls.Add(this.coBox_BtSpk);
		this.groupBox4.Controls.Add(this.coBox_BtPair);
		this.groupBox4.Controls.Add(this.lbl44);
		this.groupBox4.Controls.Add(this.lbl41);
		this.groupBox4.Controls.Add(this.coBox_BtMic);
		this.groupBox4.Controls.Add(this.txt_BtPassword);
		this.groupBox4.Controls.Add(this.lbl43);
		this.groupBox4.Controls.Add(this.lbl40);
		this.groupBox4.Controls.Add(this.coBox_BtRx);
		this.groupBox4.Controls.Add(this.coBox_BtHold);
		this.groupBox4.Controls.Add(this.lbl42);
		this.groupBox4.Location = new System.Drawing.Point(38, 87);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(171, 117);
		this.groupBox4.TabIndex = 184;
		this.groupBox4.TabStop = false;
		this.groupBox4.Visible = false;
		this.lbl45.Location = new System.Drawing.Point(-65, 181);
		this.lbl45.Name = "lbl45";
		this.lbl45.Size = new System.Drawing.Size(144, 18);
		this.lbl45.TabIndex = 192;
		this.lbl45.Text = "喇叭增益等级";
		this.lbl45.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.ckBox_BtApp.Location = new System.Drawing.Point(-14, 23);
		this.ckBox_BtApp.Name = "ckBox_BtApp";
		this.ckBox_BtApp.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
		this.ckBox_BtApp.Size = new System.Drawing.Size(110, 21);
		this.ckBox_BtApp.TabIndex = 125;
		this.ckBox_BtApp.Text = "蓝牙APP";
		this.ckBox_BtApp.UseVisualStyleBackColor = true;
		this.coBox_BtSpk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_BtSpk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_BtSpk.FormattingEnabled = true;
		this.coBox_BtSpk.Items.AddRange(new object[5] { "1", "2", "3", "4", "5" });
		this.coBox_BtSpk.Location = new System.Drawing.Point(85, 181);
		this.coBox_BtSpk.Name = "coBox_BtSpk";
		this.coBox_BtSpk.Size = new System.Drawing.Size(80, 20);
		this.coBox_BtSpk.TabIndex = 191;
		this.coBox_BtPair.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_BtPair.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_BtPair.FormattingEnabled = true;
		this.coBox_BtPair.Items.AddRange(new object[4] { "搜索蓝牙", "可用的蓝牙", "已配对列表", "断开蓝牙" });
		this.coBox_BtPair.Location = new System.Drawing.Point(85, 77);
		this.coBox_BtPair.Name = "coBox_BtPair";
		this.coBox_BtPair.Size = new System.Drawing.Size(80, 20);
		this.coBox_BtPair.TabIndex = 181;
		this.lbl44.Location = new System.Drawing.Point(-63, 155);
		this.lbl44.Name = "lbl44";
		this.lbl44.Size = new System.Drawing.Size(142, 18);
		this.lbl44.TabIndex = 190;
		this.lbl44.Text = "麦克风增益等级";
		this.lbl44.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl41.Location = new System.Drawing.Point(-57, 77);
		this.lbl41.Name = "lbl41";
		this.lbl41.Size = new System.Drawing.Size(136, 18);
		this.lbl41.TabIndex = 182;
		this.lbl41.Text = "蓝牙配对";
		this.lbl41.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_BtMic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_BtMic.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_BtMic.FormattingEnabled = true;
		this.coBox_BtMic.Items.AddRange(new object[5] { "1", "2", "3", "4", "5" });
		this.coBox_BtMic.Location = new System.Drawing.Point(85, 155);
		this.coBox_BtMic.Name = "coBox_BtMic";
		this.coBox_BtMic.Size = new System.Drawing.Size(80, 20);
		this.coBox_BtMic.TabIndex = 189;
		this.txt_BtPassword.Location = new System.Drawing.Point(83, 50);
		this.txt_BtPassword.MaxLength = 8;
		this.txt_BtPassword.Name = "txt_BtPassword";
		this.txt_BtPassword.Size = new System.Drawing.Size(82, 21);
		this.txt_BtPassword.TabIndex = 183;
		this.lbl43.Location = new System.Drawing.Point(-61, 129);
		this.lbl43.Name = "lbl43";
		this.lbl43.Size = new System.Drawing.Size(140, 18);
		this.lbl43.TabIndex = 188;
		this.lbl43.Text = "接收延时[ms]";
		this.lbl43.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl40.Location = new System.Drawing.Point(-57, 50);
		this.lbl40.Name = "lbl40";
		this.lbl40.Size = new System.Drawing.Size(134, 18);
		this.lbl40.TabIndex = 184;
		this.lbl40.Text = "蓝牙密码";
		this.lbl40.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_BtRx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_BtRx.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_BtRx.FormattingEnabled = true;
		this.coBox_BtRx.Items.AddRange(new object[11]
		{
			"500", "1000", "1500", "2000", "2500", "3000", "3500", "4000", "4500", "5000",
			"5500"
		});
		this.coBox_BtRx.Location = new System.Drawing.Point(85, 129);
		this.coBox_BtRx.Name = "coBox_BtRx";
		this.coBox_BtRx.Size = new System.Drawing.Size(80, 20);
		this.coBox_BtRx.TabIndex = 187;
		this.coBox_BtHold.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_BtHold.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_BtHold.FormattingEnabled = true;
		this.coBox_BtHold.Items.AddRange(new object[5] { "关", "30", "60", "120", "无限" });
		this.coBox_BtHold.Location = new System.Drawing.Point(85, 103);
		this.coBox_BtHold.Name = "coBox_BtHold";
		this.coBox_BtHold.Size = new System.Drawing.Size(80, 20);
		this.coBox_BtHold.TabIndex = 185;
		this.lbl42.Location = new System.Drawing.Point(-59, 103);
		this.lbl42.Name = "lbl42";
		this.lbl42.Size = new System.Drawing.Size(138, 18);
		this.lbl42.TabIndex = 186;
		this.lbl42.Text = "保持时间[s]";
		this.lbl42.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txt_BtName.Location = new System.Drawing.Point(134, 43);
		this.txt_BtName.MaxLength = 16;
		this.txt_BtName.Name = "txt_BtName";
		this.txt_BtName.Size = new System.Drawing.Size(125, 21);
		this.txt_BtName.TabIndex = 185;
		this.txt_BtName.TextChanged += new System.EventHandler(txt_BtName_TextChanged);
		this.txt_BtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(tB_PcWord_KeyPress);
		this.label3.Location = new System.Drawing.Point(7, 43);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(121, 18);
		this.label3.TabIndex = 186;
		this.label3.Text = "蓝牙名称";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.groupBox2.Controls.Add(this.lbl_GpsReq);
		this.groupBox2.Controls.Add(this.coBox_GpsReq);
		this.groupBox2.Controls.Add(this.lbl_GpsZone);
		this.groupBox2.Controls.Add(this.coBox_GpsZone);
		this.groupBox2.Controls.Add(this.ckBox_Gps);
		this.groupBox2.Controls.Add(this.lbl_GpsMode);
		this.groupBox2.Controls.Add(this.ckBox_Aprs);
		this.groupBox2.Controls.Add(this.coBox_GpsMode);
		this.groupBox2.Location = new System.Drawing.Point(343, 211);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(267, 119);
		this.groupBox2.TabIndex = 161;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "GPS";
		this.lbl_GpsReq.Location = new System.Drawing.Point(32, 142);
		this.lbl_GpsReq.Name = "lbl_GpsReq";
		this.lbl_GpsReq.Size = new System.Drawing.Size(92, 18);
		this.lbl_GpsReq.TabIndex = 186;
		this.lbl_GpsReq.Text = "GPS请求";
		this.lbl_GpsReq.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl_GpsReq.Visible = false;
		this.coBox_GpsReq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_GpsReq.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_GpsReq.FormattingEnabled = true;
		this.coBox_GpsReq.Items.AddRange(new object[2] { "关闭", "打开" });
		this.coBox_GpsReq.Location = new System.Drawing.Point(129, 142);
		this.coBox_GpsReq.Name = "coBox_GpsReq";
		this.coBox_GpsReq.Size = new System.Drawing.Size(80, 20);
		this.coBox_GpsReq.TabIndex = 185;
		this.coBox_GpsReq.Visible = false;
		this.lbl_GpsZone.Location = new System.Drawing.Point(16, 68);
		this.lbl_GpsZone.Name = "lbl_GpsZone";
		this.lbl_GpsZone.Size = new System.Drawing.Size(92, 18);
		this.lbl_GpsZone.TabIndex = 184;
		this.lbl_GpsZone.Text = "GPS时区";
		this.lbl_GpsZone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_GpsZone.DropDownHeight = 120;
		this.coBox_GpsZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_GpsZone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_GpsZone.FormattingEnabled = true;
		this.coBox_GpsZone.IntegralHeight = false;
		this.coBox_GpsZone.Items.AddRange(new object[25]
		{
			"-12", "-11", "-10", "-9", "-8", "-7", "-6", "-5", "-4", "-3",
			"-2", "-1", "0", "1", "2", "3", "4", "5", "6", "7",
			"8", "9", "10", "11", "12"
		});
		this.coBox_GpsZone.Location = new System.Drawing.Point(113, 68);
		this.coBox_GpsZone.Name = "coBox_GpsZone";
		this.coBox_GpsZone.Size = new System.Drawing.Size(80, 20);
		this.coBox_GpsZone.TabIndex = 183;
		this.coBox_Key4Long.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Key4Long.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Key4Long.FormattingEnabled = true;
		this.coBox_Key4Long.Items.AddRange(new object[14]
		{
			"无", "扫描键", "监听键", "手电灯", "收音机", "紧急警报", "定位系统", "一键扫频", "蓝牙", "1750Hz",
			"倒频", "脱网", "倒地告警", "一键呼叫"
		});
		this.coBox_Key4Long.Location = new System.Drawing.Point(121, 191);
		this.coBox_Key4Long.Name = "coBox_Key4Long";
		this.coBox_Key4Long.Size = new System.Drawing.Size(138, 20);
		this.coBox_Key4Long.TabIndex = 187;
		this.coBox_Key4Long.SelectedIndexChanged += new System.EventHandler(coBox_Key4Long_SelectedIndexChanged);
		this.lbl_pk4long.Location = new System.Drawing.Point(10, 191);
		this.lbl_pk4long.Name = "lbl_pk4long";
		this.lbl_pk4long.Size = new System.Drawing.Size(110, 18);
		this.lbl_pk4long.TabIndex = 186;
		this.lbl_pk4long.Text = "PF4 键长按";
		this.lbl_pk4long.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_Key4Short.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Key4Short.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Key4Short.FormattingEnabled = true;
		this.coBox_Key4Short.Items.AddRange(new object[14]
		{
			"无", "扫描键", "监听键", "手电灯", "收音机", "紧急警报", "定位系统", "一键扫频", "蓝牙", "1750Hz",
			"倒频", "脱网", "倒地告警", "一键呼叫"
		});
		this.coBox_Key4Short.Location = new System.Drawing.Point(121, 166);
		this.coBox_Key4Short.Name = "coBox_Key4Short";
		this.coBox_Key4Short.Size = new System.Drawing.Size(138, 20);
		this.coBox_Key4Short.TabIndex = 185;
		this.coBox_Key4Short.SelectedIndexChanged += new System.EventHandler(coBox_Key4Short_SelectedIndexChanged);
		this.lbl_pk4short.Location = new System.Drawing.Point(10, 166);
		this.lbl_pk4short.Name = "lbl_pk4short";
		this.lbl_pk4short.Size = new System.Drawing.Size(110, 18);
		this.lbl_pk4short.TabIndex = 184;
		this.lbl_pk4short.Text = "PF4 键短按";
		this.lbl_pk4short.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_Key3Long.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Key3Long.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Key3Long.FormattingEnabled = true;
		this.coBox_Key3Long.Items.AddRange(new object[14]
		{
			"无", "扫描键", "监听键", "手电灯", "收音机", "紧急警报", "定位系统", "一键扫频", "蓝牙", "1750Hz",
			"倒频", "脱网", "倒地告警", "一键呼叫"
		});
		this.coBox_Key3Long.Location = new System.Drawing.Point(121, 141);
		this.coBox_Key3Long.Name = "coBox_Key3Long";
		this.coBox_Key3Long.Size = new System.Drawing.Size(138, 20);
		this.coBox_Key3Long.TabIndex = 183;
		this.coBox_Key3Long.SelectedIndexChanged += new System.EventHandler(coBox_Key3Long_SelectedIndexChanged);
		this.coBox_Key3Short.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Key3Short.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Key3Short.FormattingEnabled = true;
		this.coBox_Key3Short.ItemHeight = 12;
		this.coBox_Key3Short.Items.AddRange(new object[14]
		{
			"无", "扫描键", "监听键", "手电灯", "收音机", "紧急警报", "定位系统", "一键扫频", "蓝牙", "1750Hz",
			"倒频", "脱网", "倒地告警", "一键呼叫"
		});
		this.coBox_Key3Short.Location = new System.Drawing.Point(121, 116);
		this.coBox_Key3Short.Name = "coBox_Key3Short";
		this.coBox_Key3Short.Size = new System.Drawing.Size(138, 20);
		this.coBox_Key3Short.TabIndex = 182;
		this.coBox_Key3Short.SelectedIndexChanged += new System.EventHandler(coBox_Key3Short_SelectedIndexChanged);
		this.lbl_pk3long.Location = new System.Drawing.Point(10, 141);
		this.lbl_pk3long.Name = "lbl_pk3long";
		this.lbl_pk3long.Size = new System.Drawing.Size(110, 18);
		this.lbl_pk3long.TabIndex = 181;
		this.lbl_pk3long.Text = "PF3 键长按";
		this.lbl_pk3long.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl_pk3short.Location = new System.Drawing.Point(8, 116);
		this.lbl_pk3short.Name = "lbl_pk3short";
		this.lbl_pk3short.Size = new System.Drawing.Size(113, 18);
		this.lbl_pk3short.TabIndex = 180;
		this.lbl_pk3short.Text = "PF3 键短按";
		this.lbl_pk3short.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.AutoScroll = true;
		base.ClientSize = new System.Drawing.Size(846, 499);
		base.Controls.Add(this.Tab_Genset);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Frm_GenralSet";
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = "常规设置";
		base.Load += new System.EventHandler(Frm_GenralSet_Load);
		base.Activated += new System.EventHandler(Frm_GenralSet_Activated);
		this.grp2.ResumeLayout(false);
		this.grp3.ResumeLayout(false);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Num_LoneworkResp).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Num_LoneworkTim).EndInit();
		this.grp4.ResumeLayout(false);
		this.Tab_Genset.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage1.PerformLayout();
		this.tabPage2.ResumeLayout(false);
		this.tabPage2.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	public Frm_GenralSet(DataApp data)
	{
		InitializeComponent();
		adata = data;
		tdata = data.dataGenSetInfor;
	}

	private void Frm_GenralSet_Load(object sender, EventArgs e)
	{
		string[] strItems = new string[45];
		string tmp = "";
		if (FormMain.lang == "Chinese")
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
		}
		LanguageSel.LoadLanguage(this, typeof(Frm_GenralSet));
		for (int i = 0; i < 45; i++)
		{
			LanguageSel.GenCoBox.TryGetValue(i.ToString(), out tmp);
			strItems[i] = tmp;
		}
		coBox_ChADispMode.Items[0] = strItems[4];
		coBox_ChADispMode.Items[1] = strItems[5];
		coBox_ChADispMode.Items[2] = strItems[6];
		coBox_ChADispMode.Items[3] = strItems[44];
		coBox_ChBDispMode.Items[0] = strItems[4];
		coBox_ChBDispMode.Items[1] = strItems[5];
		coBox_ChBDispMode.Items[2] = strItems[6];
		coBox_ChBDispMode.Items[3] = strItems[44];
		coBox_DualMode.Items[0] = strItems[0];
		coBox_DualMode.Items[1] = strItems[7];
		coBox_DualMode.Items[2] = strItems[8];
		coBox_PwonFace.Items[0] = strItems[9];
		coBox_PwonFace.Items[1] = strItems[10];
		coBox_PwonFace.Items[2] = strItems[11];
		coBox_BlightTim.Items[0] = strItems[12];
		coBox_VoiSw.Items[0] = strItems[0];
		coBox_VoiSw.Items[1] = strItems[2];
		coBox_VoiSw.Items[2] = strItems[3];
		Cobox_Save.Items[0] = strItems[0];
		coBox_Tone.Items[0] = strItems[0];
		coBox_Tone.Items[1] = strItems[1];
		coBox_LangSel.Items[1] = strItems[2];
		coBox_LangSel.Items[0] = strItems[3];
		coBox_Tail.Items[0] = strItems[0];
		coBox_BusyLock.Items[0] = strItems[0];
		coBox_BusyLock.Items[1] = strItems[1];
		coBox_KeyLock.Items[0] = strItems[0];
		coBox_KeyLock.Items[1] = strItems[1];
		coBox_Autokey.Items[0] = strItems[0];
		coBox_Autokey.Items[1] = strItems[1];
		coBox_APO.Items[0] = strItems[0];
		coBox_CallEndTone.Items[0] = strItems[0];
		coBox_CallEndTone.Items[1] = strItems[13];
		coBox_CallEndTone.Items[2] = strItems[14];
		coBox_CallEndTone.Items[3] = strItems[15];
		coBox_Tot.Items[0] = strItems[0];
		coBox_PreTot.Items[0] = strItems[0];
		coBox_Record.Items[0] = strItems[0];
		coBox_Record.Items[1] = strItems[1];
		coBox_NOAA.Items[0] = strItems[0];
		coBox_NOAA.Items[1] = strItems[1];
		coBox_Tianqi.Items[0] = strItems[0];
		coBox_Tianqi.Items[1] = strItems[1];
		coBox_DaoDi.Items[0] = strItems[0];
		coBox_DaoDi.Items[1] = strItems[1];
		coBox_RecordMode.Items[0] = strItems[16];
		coBox_RecordMode.Items[1] = strItems[17];
		coBox_RecordMode.Items[2] = strItems[18];
		coBox_GpsMode.Items[1] = strItems[19];
		coBox_GpsMode.Items[2] = strItems[20];
		coBox_BtPair.Items[0] = strItems[21];
		coBox_BtPair.Items[1] = strItems[22];
		coBox_BtPair.Items[2] = strItems[23];
		coBox_BtPair.Items[3] = strItems[24];
		coBox_BtHold.Items[0] = strItems[0];
		coBox_BtHold.Items[4] = strItems[25];
		coBox_Sql.Items[0] = strItems[0];
		coBox_DisDir.Items[0] = strItems[42];
		coBox_DisDir.Items[1] = strItems[43];
		coBox_Enhance.Items[0] = strItems[0];
		coBox_Enhance.Items[1] = strItems[1];
		BindZone();
		Tabpage1bingDing(tdata);
		if (adata.dataDevice.EnBT == 1)
		{
			groupBox3.Visible = true;
		}
		else
		{
			groupBox3.Visible = false;
		}
		if (adata.dataDevice.EnGps == 1)
		{
			groupBox2.Visible = true;
		}
		else
		{
			groupBox2.Visible = false;
		}
		if (adata.dataDevice.EnFalldn == 1)
		{
			lbl34.Visible = true;
			coBox_DaoDi.Visible = true;
		}
		else
		{
			lbl34.Visible = false;
			coBox_DaoDi.Visible = false;
		}
		initFlg = false;
		coBox_Key1Short.DataSource = ProKeyEnum.Short1Pkey;
		coBox_Key2Short.DataSource = ProKeyEnum.Short2Pkey;
		coBox_Key1Long.DataSource = ProKeyEnum.Long1Pkey;
		coBox_Key2Long.DataSource = ProKeyEnum.Long2Pkey;
		coBox_Key3Short.DataSource = ProKeyEnum.Short3Pkey;
		coBox_Key4Short.DataSource = ProKeyEnum.Short4Pkey;
		coBox_Key3Long.DataSource = ProKeyEnum.Long3Pkey;
		coBox_Key4Long.DataSource = ProKeyEnum.Long4Pkey;
		initFlg = true;
		tmp = ProKeyEnum.GetEuumStr(tdata.Skey1);
		coBox_Key1Short.SelectedIndex = coBox_Key1Short.Items.IndexOf(tmp);
		tmp = ProKeyEnum.GetEuumStr(tdata.Lkey1);
		coBox_Key1Long.SelectedIndex = coBox_Key1Long.Items.IndexOf(tmp);
		tmp = ProKeyEnum.GetEuumStr(tdata.Skey2);
		coBox_Key2Short.SelectedIndex = coBox_Key2Short.Items.IndexOf(tmp);
		tmp = ProKeyEnum.GetEuumStr(tdata.Lkey2);
		coBox_Key2Long.SelectedIndex = coBox_Key2Long.Items.IndexOf(tmp);
		tmp = ProKeyEnum.GetEuumStr(tdata.Skey3);
		coBox_Key3Short.SelectedIndex = coBox_Key3Short.Items.IndexOf(tmp);
		tmp = ProKeyEnum.GetEuumStr(tdata.Lkey3);
		coBox_Key3Long.SelectedIndex = coBox_Key3Long.Items.IndexOf(tmp);
		tmp = ProKeyEnum.GetEuumStr(tdata.Skey4);
		coBox_Key4Short.SelectedIndex = coBox_Key4Short.Items.IndexOf(tmp);
		tmp = ProKeyEnum.GetEuumStr(tdata.Lkey4);
		coBox_Key4Long.SelectedIndex = coBox_Key4Long.Items.IndexOf(tmp);
		ControlProgramKeyDisp();
	}

	private void Tabpage1bingDing(DatGenSetInfo tdata)
	{
		if (tdata.ChAMode > 1)
		{
			tdata.ChAMode = 0;
		}
		if (tdata.ChBMode > 1)
		{
			tdata.ChBMode = 0;
		}
		if (tdata.ChADispMode > 3)
		{
			tdata.ChADispMode = 0;
		}
		if (tdata.ChBDispMode > 3)
		{
			tdata.ChBDispMode = 0;
		}
		if (tdata.Sqlv > 9)
		{
			tdata.Sqlv = 5;
		}
		if (tdata.DualMode > 2)
		{
			tdata.DualMode = 2;
		}
		if (tdata.BlightLv > 4)
		{
			tdata.BlightLv = 0;
		}
		if (tdata.TailFreq > 4)
		{
			tdata.TailFreq = 0;
		}
		if (tdata.Voxdettime > 18)
		{
			tdata.Voxdettime = 0;
		}
		if (tdata.VoxLv > 8)
		{
			tdata.VoxLv = 0;
		}
		coBox_ChAMode.DataBindings.Add("SelectedIndex", tdata, "ChAMode", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_ChBMode.DataBindings.Add("SelectedIndex", tdata, "ChBMode", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_BlightTim.DataBindings.Add("SelectedIndex", tdata, "BlightTime", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_BlightLv.DataBindings.Add("SelectedIndex", tdata, "BlightLv", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_ChADispMode.DataBindings.Add("SelectedIndex", tdata, "ChADispMode", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_ChBDispMode.DataBindings.Add("SelectedIndex", tdata, "ChBDispMode", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_DualMode.DataBindings.Add("SelectedIndex", tdata, "DualMode", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_MainBand.DataBindings.Add("SelectedIndex", tdata, "MainBand", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_PwonFace.DataBindings.Add("SelectedIndex", tdata, "PownFace", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_Sql.DataBindings.Add("SelectedIndex", tdata, "Sqlv", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		ckBox_VoxSw.DataBindings.Add("Checked", tdata, "VoxSW", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_Voxlv.DataBindings.Add("SelectedIndex", tdata, "VoxLv", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_VoxDly.DataBindings.Add("SelectedIndex", tdata, "Voxdettime", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		Cobox_Save.DataBindings.Add("SelectedIndex", tdata, "PoSave", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		Cobox_SaveDly.DataBindings.Add("SelectedIndex", tdata, "PoSaveDly", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_APO.DataBindings.Add("SelectedIndex", tdata, "APO", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_Tot.DataBindings.Add("SelectedIndex", tdata, "TOT", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_PreTot.DataBindings.Add("SelectedIndex", tdata, "PreTot", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_VoiSw.DataBindings.Add("SelectedIndex", tdata, "Voice", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_BusyLock.DataBindings.Add("SelectedIndex", tdata, "BusyLock", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_LangSel.DataBindings.Add("SelectedIndex", tdata, "LangSel", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_Tail.DataBindings.Add("SelectedIndex", tdata, "TailFreq", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_Tone.DataBindings.Add("SelectedIndex", tdata, "Tone", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_CallEndTone.DataBindings.Add("SelectedIndex", tdata, "EndTone", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_KeyLock.DataBindings.Add("SelectedIndex", tdata, "KeyLock", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_Autokey.DataBindings.Add("SelectedIndex", tdata, "AutoKey", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_DisDir.DataBindings.Add("SelectedIndex", tdata, "DispDir", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_Enhance.DataBindings.Add("SelectedIndex", tdata, "EnhanceFunc", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		tBox_WRPwd.DataBindings.Add("Text", tdata, "WrPassword", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		tBox_PowonFace1.DataBindings.Add("Text", tdata, "Radioname", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		tBox_PowonPwd.DataBindings.Add("Text", tdata, "PowPassword", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		ControlVoxDisp();
	}

	private void Tabpage2bingDing(DatGenSetInfo tdata)
	{
		if (tdata.GpsZone > 24)
		{
			tdata.GpsZone = 0;
		}
		if (tdata.NoaaCh >= 10)
		{
			tdata.NoaaCh = 0;
		}
		if (tdata.GpsID > 80)
		{
			tdata.GpsID = 0;
		}
		FlgPage2 = true;
		changeFlg = false;
		coBox_Hz1750.DataBindings.Add("SelectedIndex", tdata, "HzTo1750", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_Record.DataBindings.Add("SelectedIndex", tdata, "Reord", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_RecordMode.DataBindings.Add("SelectedIndex", tdata, "RecordMode", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_NOAA.DataBindings.Add("SelectedIndex", tdata, "NOAA", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_Tianqi.DataBindings.Add("SelectedIndex", tdata, "TianQi", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_NOAA_Ch.DataBindings.Add("SelectedIndex", tdata, "NoaaCh", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		ckBox_Factory.DataBindings.Add("Checked", tdata, "Engineering", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_DaoDi.DataBindings.Add("SelectedIndex", tdata, "DaoDi", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		ckBox_BT.DataBindings.Add("Checked", tdata, "BlueT", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		ckBox_BtApp.DataBindings.Add("Checked", tdata, "BluetAPP", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_BtPair.DataBindings.Add("SelectedIndex", tdata, "BTpair", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_BtHold.DataBindings.Add("SelectedIndex", tdata, "BThold", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_BtRx.DataBindings.Add("SelectedIndex", tdata, "BTrxdly", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_BtMic.DataBindings.Add("SelectedIndex", tdata, "BTmic", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_BtSpk.DataBindings.Add("SelectedIndex", tdata, "BTspk", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txt_BtPassword.DataBindings.Add("Text", tdata, "BTpassword", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		txt_BtName.DataBindings.Add("Text", tdata, "BlueTName", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		ckBox_Gps.DataBindings.Add("Checked", tdata, "GpsSW", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_GpsMode.DataBindings.Add("SelectedIndex", tdata, "GpsMode", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_GpsZone.DataBindings.Add("SelectedIndex", tdata, "GpsZone", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		ckBox_Aprs.DataBindings.Add("Checked", tdata, "AprsSw", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		ckBox_LoneWork.DataBindings.Add("Checked", tdata, "LoneWork", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		Num_LoneworkResp.DataBindings.Add("Value", tdata, "LoneWorkRsp", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		Num_LoneworkTim.DataBindings.Add("Value", tdata, "LoneWorkTim", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		BindGpsID();
		ControlGpsDisp();
		ControlLoneworkDisp();
		ControlBTDisp();
		ControlNOAADisp();
		changeFlg = true;
	}

	private void ControlVoxDisp()
	{
		if (ckBox_VoxSw.Checked)
		{
			coBox_Voxlv.Enabled = true;
			coBox_VoxDly.Enabled = true;
		}
		else
		{
			coBox_Voxlv.Enabled = false;
			coBox_VoxDly.Enabled = false;
		}
	}

	private void ControlBTDisp()
	{
		if (ckBox_BT.Checked)
		{
			ckBox_BtApp.Enabled = true;
		}
		else
		{
			ckBox_BtApp.Enabled = false;
		}
	}

	private void ControlGpsDisp()
	{
		if (ckBox_Gps.Checked)
		{
			ckBox_Aprs.Enabled = true;
			coBox_GpsMode.Enabled = true;
			lbl_GpsMode.Enabled = true;
			lbl_GpsZone.Enabled = true;
			coBox_GpsZone.Enabled = true;
		}
		else
		{
			ckBox_Aprs.Enabled = false;
			coBox_GpsMode.Enabled = false;
			lbl_GpsMode.Enabled = false;
			lbl_GpsZone.Enabled = false;
			coBox_GpsZone.Enabled = false;
		}
	}

	private void ControlLoneworkDisp()
	{
		if (ckBox_LoneWork.Checked)
		{
			Num_LoneworkResp.Enabled = true;
			Num_LoneworkTim.Enabled = true;
			lbl_LworkRsp.Enabled = true;
			lbl_LworkTim.Enabled = true;
		}
		else
		{
			Num_LoneworkResp.Enabled = false;
			Num_LoneworkTim.Enabled = false;
			lbl_LworkRsp.Enabled = false;
			lbl_LworkTim.Enabled = false;
		}
	}

	private void ControlNOAADisp()
	{
		if (coBox_NOAA.SelectedIndex <= 0)
		{
			coBox_NOAA_Ch.Enabled = false;
		}
		else
		{
			coBox_NOAA_Ch.Enabled = true;
		}
	}

	private void ControlProgramKeyDisp()
	{
		switch (adata.dataDevice.PkeyCnt)
		{
		case 0:
			lbl35.Visible = true;
			lbl36.Visible = true;
			lbl37.Visible = false;
			lbl38.Visible = false;
			lbl_pk3short.Visible = false;
			lbl_pk3long.Visible = false;
			lbl_pk4short.Visible = false;
			lbl_pk4long.Visible = false;
			coBox_Key1Short.Visible = true;
			coBox_Key1Long.Visible = true;
			coBox_Key2Short.Visible = false;
			coBox_Key2Long.Visible = false;
			coBox_Key3Short.Visible = false;
			coBox_Key3Long.Visible = false;
			coBox_Key4Short.Visible = false;
			coBox_Key4Long.Visible = false;
			break;
		case 2:
			lbl35.Visible = true;
			lbl36.Visible = true;
			lbl37.Visible = true;
			lbl38.Visible = true;
			lbl_pk3short.Visible = true;
			lbl_pk3long.Visible = true;
			lbl_pk4short.Visible = false;
			lbl_pk4long.Visible = false;
			coBox_Key1Short.Visible = true;
			coBox_Key1Long.Visible = true;
			coBox_Key2Short.Visible = true;
			coBox_Key2Long.Visible = true;
			coBox_Key3Short.Visible = true;
			coBox_Key3Long.Visible = true;
			coBox_Key4Short.Visible = false;
			coBox_Key4Long.Visible = false;
			break;
		case 3:
			lbl35.Visible = true;
			lbl36.Visible = true;
			lbl37.Visible = true;
			lbl38.Visible = true;
			lbl_pk3short.Visible = true;
			lbl_pk3long.Visible = true;
			lbl_pk4short.Visible = true;
			lbl_pk4long.Visible = true;
			coBox_Key1Short.Visible = true;
			coBox_Key1Long.Visible = true;
			coBox_Key2Short.Visible = true;
			coBox_Key2Long.Visible = true;
			coBox_Key3Short.Visible = true;
			coBox_Key3Long.Visible = true;
			coBox_Key4Short.Visible = true;
			coBox_Key4Long.Visible = true;
			break;
		default:
			lbl35.Visible = true;
			lbl36.Visible = true;
			lbl37.Visible = true;
			lbl38.Visible = true;
			lbl_pk3short.Visible = false;
			lbl_pk3long.Visible = false;
			lbl_pk4short.Visible = false;
			lbl_pk4long.Visible = false;
			coBox_Key1Short.Visible = true;
			coBox_Key1Long.Visible = true;
			coBox_Key2Short.Visible = true;
			coBox_Key2Long.Visible = true;
			coBox_Key3Short.Visible = false;
			coBox_Key3Long.Visible = false;
			coBox_Key4Short.Visible = false;
			coBox_Key4Long.Visible = false;
			break;
		}
	}

	private void BindZone()
	{
		initFlg = false;
		coBox_ChAZone.DataSource = null;
		coBox_ChBZone.DataSource = null;
		ZoneAList.Clear();
		ZoneBList.Clear();
		for (int i = 0; i < DataApp.Zone_Max_Num; i++)
		{
			if (adata.dataZoneInfor[i].ChnNum > 0)
			{
				ZoneAList.Add((i + 1).ToString());
				ZoneBList.Add((i + 1).ToString());
			}
		}
		coBox_ChAZone.DataSource = ZoneAList;
		coBox_ChBZone.DataSource = ZoneBList;
		initFlg = true;
		int zaidx = coBox_ChAZone.Items.IndexOf((tdata.ChAZone + 1).ToString());
		if (zaidx >= 0)
		{
			coBox_ChAZone.SelectedIndex = zaidx;
		}
		int zbidx = coBox_ChBZone.Items.IndexOf((tdata.ChBZone + 1).ToString());
		if (zbidx >= 0)
		{
			coBox_ChBZone.SelectedIndex = zbidx;
		}
	}

	private void BindGpsID()
	{
		int total = 0;
		string name = string.Empty;
		initFlg = false;
		coBox_GpsID.DataSource = null;
		PbookList.Clear();
		for (int i = 0; i < DatGpsBook.MAX_GpsBook_Num; i++)
		{
			if (adata.dataGpsBook[i].UseFlg == 1)
			{
				PbookList.Add((i + 1).ToString());
				total++;
				if (total == DatGpsBook.GpsBook_Total)
				{
					break;
				}
			}
		}
		coBox_GpsID.DataSource = PbookList;
		initFlg = true;
		if (coBox_GpsID.Items.Count > 0)
		{
			int zaidx = coBox_GpsID.Items.IndexOf(tdata.GpsID.ToString());
			if (zaidx > 0)
			{
				coBox_GpsID.SelectedIndex = zaidx;
			}
			else
			{
				coBox_GpsID.SelectedIndex = 0;
			}
		}
	}

	private void CoBox_Pbook_Disp()
	{
		string cName = "";
		if (tdata.GpsID == 0)
		{
			coBox_GpsID.SelectedItem = PbookList[0];
			return;
		}
		cName = tdata.GpsID.ToString();
		if (cName == "")
		{
			coBox_GpsID.SelectedItem = PbookList[0];
		}
		else
		{
			coBox_GpsID.SelectedItem = cName;
		}
	}

	private void cBox_Pwd_CheckedChanged(object sender, EventArgs e)
	{
	}

	private void tBox_RadioName_TextChanged(object sender, EventArgs e)
	{
		string tmp = "";
		try
		{
			if (DataApp.GetLength(tBox_PowonFace1.Text) > 16)
			{
				tmp = DataApp.StrFormat(tBox_PowonFace1.Text, 16);
				tBox_PowonFace1.Text = tmp;
				tBox_PowonFace1.Focus();
				tBox_PowonFace1.Select(tBox_PowonFace1.TextLength, 0);
				tBox_PowonFace1.ScrollToCaret();
			}
		}
		catch
		{
		}
	}

	private void tB_RadioID_KeyPress(object sender, KeyPressEventArgs e)
	{
		if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != '\b')
		{
			e.Handled = true;
		}
	}

	private void tB_PcWord_KeyPress(object sender, KeyPressEventArgs e)
	{
		if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '\b' || (e.KeyChar >= 'a' && e.KeyChar <= 'z') || (e.KeyChar >= 'A' && e.KeyChar <= 'Z'))
		{
			e.Handled = false;
		}
		else
		{
			e.Handled = true;
		}
	}

	private void cBox_Tone_CheckedChanged(object sender, EventArgs e)
	{
		ControlBTDisp();
	}

	private void Tab_Genset_SelectedIndexChanged(object sender, EventArgs e)
	{
		switch (Tab_Genset.SelectedIndex)
		{
		case 0:
			break;
		case 1:
			if (!FlgPage2)
			{
				Tabpage2bingDing(tdata);
			}
			break;
		}
	}

	private void ckBox_Gps_CheckedChanged(object sender, EventArgs e)
	{
		ControlGpsDisp();
	}

	private void ckBox_LoneWork_CheckedChanged(object sender, EventArgs e)
	{
		ControlLoneworkDisp();
	}

	private int GetValidChnInZone(int zidx)
	{
		int start = zidx * DataApp.Zone_Max_Chn_Num;
		for (int i = 0; i < DataApp.Zone_Max_Chn_Num; i++)
		{
			if (adata.dataChnInfor[start + i].UseFlg == 1)
			{
				return i;
			}
		}
		return start;
	}

	private void coBox_ChAZone_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (initFlg)
		{
			tdata.ChAZone = (byte)(Convert.ToByte(coBox_ChAZone.Text) - 1);
			tdata.ChANum = (ushort)GetValidChnInZone(tdata.ChAZone);
		}
	}

	private void coBox_ChBZone_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (initFlg)
		{
			tdata.ChBZone = (byte)(Convert.ToByte(coBox_ChBZone.Text) - 1);
			tdata.ChBNum = (ushort)GetValidChnInZone(tdata.ChBZone);
		}
	}

	private void Frm_GenralSet_Activated(object sender, EventArgs e)
	{
		BindZone();
		BindGpsID();
	}

	private void txt_BtName_TextChanged(object sender, EventArgs e)
	{
		string tmp = "";
		try
		{
			if (DataApp.GetLength(txt_BtName.Text) > 16)
			{
				tmp = DataApp.StrFormat(txt_BtName.Text, 16);
				txt_BtName.Text = tmp;
				txt_BtName.Focus();
				txt_BtName.Select(txt_BtName.TextLength, 0);
				txt_BtName.ScrollToCaret();
			}
		}
		catch
		{
		}
	}

	private void ckBox_VoxSw_CheckedChanged(object sender, EventArgs e)
	{
		ControlVoxDisp();
	}

	private void coBox_GpsID_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (initFlg)
		{
			if (coBox_GpsID.SelectedIndex == 0)
			{
				tdata.GpsID = 0;
			}
			else
			{
				tdata.GpsID = Convert.ToByte(coBox_GpsID.Text);
			}
		}
	}

	private void coBox_NOAA_SelectedIndexChanged(object sender, EventArgs e)
	{
		ControlNOAADisp();
	}

	private void coBox_Key1Short_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (initFlg)
		{
			tdata.Skey1 = (byte)ProKeyEnum.GetEuumIdx(coBox_Key1Short.Text);
		}
	}

	private void coBox_Key1Long_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (initFlg)
		{
			tdata.Lkey1 = (byte)ProKeyEnum.GetEuumIdx(coBox_Key1Long.Text);
		}
	}

	private void coBox_Key2Short_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (initFlg)
		{
			tdata.Skey2 = (byte)ProKeyEnum.GetEuumIdx(coBox_Key2Short.Text);
		}
	}

	private void coBox_Key2Long_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (initFlg)
		{
			tdata.Lkey2 = (byte)ProKeyEnum.GetEuumIdx(coBox_Key2Long.Text);
		}
	}

	private void ckBox_Factory_CheckedChanged(object sender, EventArgs e)
	{
		if (changeFlg)
		{
			if (FormMain.lang == "Chinese")
			{
				MessageBox.Show("工程模式非专业人士请勿操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				MessageBox.Show("Engineering mode: Do not operate for non-professionals!", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
	}

	private void coBox_Key3Short_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (initFlg)
		{
			tdata.Skey3 = (byte)ProKeyEnum.GetEuumIdx(coBox_Key3Short.Text);
		}
	}

	private void coBox_Key3Long_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (initFlg)
		{
			tdata.Lkey3 = (byte)ProKeyEnum.GetEuumIdx(coBox_Key3Long.Text);
		}
	}

	private void coBox_Key4Short_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (initFlg)
		{
			tdata.Skey4 = (byte)ProKeyEnum.GetEuumIdx(coBox_Key4Short.Text);
		}
	}

	private void coBox_Key4Long_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (initFlg)
		{
			tdata.Lkey4 = (byte)ProKeyEnum.GetEuumIdx(coBox_Key4Long.Text);
		}
	}
}
