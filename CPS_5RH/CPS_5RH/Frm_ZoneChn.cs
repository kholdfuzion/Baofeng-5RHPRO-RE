using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using CPS_5RH.Data;
using DevComponents.DotNetBar.Controls;

namespace CPS_5RH;

public class Frm_ZoneChn : Form
{
	private IContainer components = null;

	public DataGridViewX dGV;

	private DataGridViewTextBoxColumn Col_CH;

	private DataGridViewTextBoxColumn Col_RxFreq;

	private DataGridViewTextBoxColumn Col_TxFreq;

	private DataGridViewComboBoxExColumn Col_RxCTS;

	private DataGridViewComboBoxExColumn Col_TxCTS;

	private DataGridViewComboBoxExColumn Col_Power;

	private DataGridViewComboBoxExColumn Col_Band;

	private DataGridViewTextBoxColumn Col_Name;

	private DataGridViewComboBoxExColumn Col_More;

	private Button btn_ClrChn;

	private static int zoneIdx = 128;

	private static int chBaseAddr = 0;

	private static int posIdx = 0;

	public static bool nodeFlg = false;

	public static string lang = null;

	private DataApp tdata = null;

	private DataGridViewTextBoxEditingControl dgvTxt = null;

	private DataGridViewTextBoxEditingControl dgvName = null;

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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPS_5RH.Frm_ZoneChn));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
		this.dGV = new DevComponents.DotNetBar.Controls.DataGridViewX();
		this.Col_CH = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col_RxFreq = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col_TxFreq = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col_RxCTS = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
		this.Col_TxCTS = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
		this.Col_Power = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
		this.Col_Band = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
		this.Col_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col_More = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
		this.btn_ClrChn = new System.Windows.Forms.Button();
		((System.ComponentModel.ISupportInitialize)this.dGV).BeginInit();
		base.SuspendLayout();
		this.dGV.AllowUserToAddRows = false;
		this.dGV.AllowUserToDeleteRows = false;
		this.dGV.AllowUserToResizeColumns = false;
		this.dGV.AllowUserToResizeRows = false;
		this.dGV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
		dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle19.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
		resources.ApplyResources(this.dGV, "dGV");
		this.dGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dGV.Columns.AddRange(this.Col_CH, this.Col_RxFreq, this.Col_TxFreq, this.Col_RxCTS, this.Col_TxCTS, this.Col_Power, this.Col_Band, this.Col_Name, this.Col_More);
		dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle22.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dGV.DefaultCellStyle = dataGridViewCellStyle22;
		this.dGV.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dGV.GridColor = System.Drawing.Color.FromArgb(208, 215, 229);
		this.dGV.MultiSelect = false;
		this.dGV.Name = "dGV";
		dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle23.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dGV.RowHeadersDefaultCellStyle = dataGridViewCellStyle23;
		this.dGV.RowHeadersVisible = false;
		dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.dGV.RowsDefaultCellStyle = dataGridViewCellStyle24;
		this.dGV.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.dGV.RowTemplate.Height = 27;
		this.dGV.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dGV_CellEndEdit);
		this.dGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dGV_CellClick);
		this.dGV.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dGV_EditingControlShowing);
		this.Col_CH.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle25.BackColor = System.Drawing.Color.LightGray;
		dataGridViewCellStyle25.ForeColor = System.Drawing.Color.Black;
		dataGridViewCellStyle25.SelectionBackColor = System.Drawing.Color.LightGray;
		dataGridViewCellStyle25.SelectionForeColor = System.Drawing.Color.Black;
		this.Col_CH.DefaultCellStyle = dataGridViewCellStyle25;
		this.Col_CH.FillWeight = 70f;
		resources.ApplyResources(this.Col_CH, "Col_CH");
		this.Col_CH.Name = "Col_CH";
		this.Col_CH.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_CH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_RxFreq.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		resources.ApplyResources(this.Col_RxFreq, "Col_RxFreq");
		this.Col_RxFreq.MaxInputLength = 9;
		this.Col_RxFreq.Name = "Col_RxFreq";
		this.Col_RxFreq.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_RxFreq.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_TxFreq.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		resources.ApplyResources(this.Col_TxFreq, "Col_TxFreq");
		this.Col_TxFreq.MaxInputLength = 9;
		this.Col_TxFreq.Name = "Col_TxFreq";
		this.Col_TxFreq.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_TxFreq.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_RxCTS.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Col_RxCTS.DefaultCellStyle = dataGridViewCellStyle26;
		this.Col_RxCTS.DisplayMember = "Text";
		this.Col_RxCTS.DropDownHeight = 106;
		this.Col_RxCTS.DropDownWidth = 121;
		this.Col_RxCTS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		resources.ApplyResources(this.Col_RxCTS, "Col_RxCTS");
		this.Col_RxCTS.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.Col_RxCTS.IntegralHeight = false;
		this.Col_RxCTS.ItemHeight = 15;
		this.Col_RxCTS.Items.AddRange(new object[266]
		{
			"OFF", "63.0", "67.0", "69.3", "71.9", "74.4", "77.0", "79.7", "82.5", "85.4",
			"88.5", "91.5", "94.8", "97.4", "100.0", "103.5", "107.2", "110.9", "114.8", "118.8",
			"123.0", "127.3", "131.8", "136.5", "141.3", "146.2", "151.4", "156.7", "159.8", "162.2",
			"165.5", "167.9", "171.3", "173.8", "177.3", "179.9", "183.5", "186.2", "189.9", "192.8",
			"196.6", "199.5", "203.5", "206.5", "210.7", "218.1", "225.7", "229.1", "233.6", "241.8",
			"250.3", "254.1", "D017N", "D023N", "D025N", "D026N", "D031N", "D032N", "D036N", "D043N",
			"D047N", "D050N", "D051N", "D053N", "D054N", "D065N", "D071N", "D072N", "D073N", "D074N",
			"D114N", "D115N", "D116N", "D122N", "D125N", "D131N", "D132N", "D134N", "D143N", "D145N",
			"D152N", "D155N", "D156N", "D162N", "D165N", "D172N", "D174N", "D205N", "D212N", "D223N",
			"D225N", "D226N", "D243N", "D244N", "D245N", "D246N", "D251N", "D252N", "D255N", "D261N",
			"D263N", "D265N", "D266N", "D271N", "D274N", "D306N", "D311N", "D315N", "D325N", "D331N",
			"D332N", "D343N", "D346N", "D351N", "D356N", "D364N", "D365N", "D371N", "D411N", "D412N",
			"D413N", "D423N", "D431N", "D432N", "D445N", "D446N", "D452N", "D454N", "D455N", "D462N",
			"D464N", "D465N", "D466N", "D503N", "D506N", "D516N", "D523N", "D526N", "D532N", "D546N",
			"D565N", "D606N", "D612N", "D624N", "D627N", "D631N", "D632N", "D645N", "D654N", "D662N",
			"D664N", "D703N", "D712N", "D723N", "D731N", "D732N", "D734N", "D743N", "D754N", "D017I",
			"D023I", "D025I", "D026I", "D031I", "D032I", "D036I", "D043I", "D047I", "D050I", "D051I",
			"D053I", "D054I", "D065I", "D071I", "D072I", "D073I", "D074I", "D114I", "D115I", "D116I",
			"D122I", "D125I", "D131I", "D132I", "D134I", "D143I", "D145I", "D152I", "D155I", "D156I",
			"D162I", "D165I", "D172I", "D174I", "D205I", "D212I", "D223I", "D225I", "D226I", "D243I",
			"D244I", "D245I", "D246I", "D251I", "D252I", "D255I", "D261I", "D263I", "D265I", "D266I",
			"D271I", "D274I", "D306I", "D311I", "D315I", "D325I", "D331I", "D332I", "D343I", "D346I",
			"D351I", "D356I", "D364I", "D365I", "D371I", "D411I", "D412I", "D413I", "D423I", "D431I",
			"D432I", "D445I", "D446I", "D452I", "D454I", "D455I", "D462I", "D464I", "D465I", "D466I",
			"D503I", "D506I", "D516I", "D523I", "D526I", "D532I", "D546I", "D565I", "D606I", "D612I",
			"D624I", "D627I", "D631I", "D632I", "D645I", "D654I", "D662I", "D664I", "D703I", "D712I",
			"D723I", "D731I", "D732I", "D734I", "D743I", "D754I"
		});
		this.Col_RxCTS.MaxInputLength = 5;
		this.Col_RxCTS.Name = "Col_RxCTS";
		this.Col_RxCTS.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_RxCTS.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Col_RxCTS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_TxCTS.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		this.Col_TxCTS.DisplayMember = "Text";
		this.Col_TxCTS.DropDownHeight = 106;
		this.Col_TxCTS.DropDownWidth = 121;
		this.Col_TxCTS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		resources.ApplyResources(this.Col_TxCTS, "Col_TxCTS");
		this.Col_TxCTS.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.Col_TxCTS.IntegralHeight = false;
		this.Col_TxCTS.ItemHeight = 15;
		this.Col_TxCTS.Items.AddRange(new object[266]
		{
			"OFF", "63.0", "67.0", "69.3", "71.9", "74.4", "77.0", "79.7", "82.5", "85.4",
			"88.5", "91.5", "94.8", "97.4", "100.0", "103.5", "107.2", "110.9", "114.8", "118.8",
			"123.0", "127.3", "131.8", "136.5", "141.3", "146.2", "151.4", "156.7", "159.8", "162.2",
			"165.5", "167.9", "171.3", "173.8", "177.3", "179.9", "183.5", "186.2", "189.9", "192.8",
			"196.6", "199.5", "203.5", "206.5", "210.7", "218.1", "225.7", "229.1", "233.6", "241.8",
			"250.3", "254.1", "D017N", "D023N", "D025N", "D026N", "D031N", "D032N", "D036N", "D043N",
			"D047N", "D050N", "D051N", "D053N", "D054N", "D065N", "D071N", "D072N", "D073N", "D074N",
			"D114N", "D115N", "D116N", "D122N", "D125N", "D131N", "D132N", "D134N", "D143N", "D145N",
			"D152N", "D155N", "D156N", "D162N", "D165N", "D172N", "D174N", "D205N", "D212N", "D223N",
			"D225N", "D226N", "D243N", "D244N", "D245N", "D246N", "D251N", "D252N", "D255N", "D261N",
			"D263N", "D265N", "D266N", "D271N", "D274N", "D306N", "D311N", "D315N", "D325N", "D331N",
			"D332N", "D343N", "D346N", "D351N", "D356N", "D364N", "D365N", "D371N", "D411N", "D412N",
			"D413N", "D423N", "D431N", "D432N", "D445N", "D446N", "D452N", "D454N", "D455N", "D462N",
			"D464N", "D465N", "D466N", "D503N", "D506N", "D516N", "D523N", "D526N", "D532N", "D546N",
			"D565N", "D606N", "D612N", "D624N", "D627N", "D631N", "D632N", "D645N", "D654N", "D662N",
			"D664N", "D703N", "D712N", "D723N", "D731N", "D732N", "D734N", "D743N", "D754N", "D017I",
			"D023I", "D025I", "D026I", "D031I", "D032I", "D036I", "D043I", "D047I", "D050I", "D051I",
			"D053I", "D054I", "D065I", "D071I", "D072I", "D073I", "D074I", "D114I", "D115I", "D116I",
			"D122I", "D125I", "D131I", "D132I", "D134I", "D143I", "D145I", "D152I", "D155I", "D156I",
			"D162I", "D165I", "D172I", "D174I", "D205I", "D212I", "D223I", "D225I", "D226I", "D243I",
			"D244I", "D245I", "D246I", "D251I", "D252I", "D255I", "D261I", "D263I", "D265I", "D266I",
			"D271I", "D274I", "D306I", "D311I", "D315I", "D325I", "D331I", "D332I", "D343I", "D346I",
			"D351I", "D356I", "D364I", "D365I", "D371I", "D411I", "D412I", "D413I", "D423I", "D431I",
			"D432I", "D445I", "D446I", "D452I", "D454I", "D455I", "D462I", "D464I", "D465I", "D466I",
			"D503I", "D506I", "D516I", "D523I", "D526I", "D532I", "D546I", "D565I", "D606I", "D612I",
			"D624I", "D627I", "D631I", "D632I", "D645I", "D654I", "D662I", "D664I", "D703I", "D712I",
			"D723I", "D731I", "D732I", "D734I", "D743I", "D754I"
		});
		this.Col_TxCTS.MaxInputLength = 5;
		this.Col_TxCTS.Name = "Col_TxCTS";
		this.Col_TxCTS.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_TxCTS.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Col_TxCTS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_Power.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		this.Col_Power.DisplayMember = "Text";
		this.Col_Power.DropDownHeight = 106;
		this.Col_Power.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.Col_Power.DropDownWidth = 121;
		this.Col_Power.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		resources.ApplyResources(this.Col_Power, "Col_Power");
		this.Col_Power.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.Col_Power.IntegralHeight = false;
		this.Col_Power.ItemHeight = 20;
		this.Col_Power.Items.AddRange(new object[3] { "低", "中", "高" });
		this.Col_Power.Name = "Col_Power";
		this.Col_Power.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_Power.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Col_Power.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_Band.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		this.Col_Band.DisplayMember = "Text";
		this.Col_Band.DropDownHeight = 106;
		this.Col_Band.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.Col_Band.DropDownWidth = 121;
		this.Col_Band.FillWeight = 65f;
		this.Col_Band.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		resources.ApplyResources(this.Col_Band, "Col_Band");
		this.Col_Band.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.Col_Band.IntegralHeight = false;
		this.Col_Band.ItemHeight = 20;
		this.Col_Band.Items.AddRange(new object[2] { "12.5K", "25K" });
		this.Col_Band.Name = "Col_Band";
		this.Col_Band.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_Band.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Col_Band.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		resources.ApplyResources(this.Col_Name, "Col_Name");
		this.Col_Name.MaxInputLength = 16;
		this.Col_Name.Name = "Col_Name";
		this.Col_Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_More.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.Col_More.DropDownHeight = 106;
		this.Col_More.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.Col_More.DropDownWidth = 121;
		this.Col_More.FillWeight = 75f;
		this.Col_More.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		resources.ApplyResources(this.Col_More, "Col_More");
		this.Col_More.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.Col_More.IntegralHeight = false;
		this.Col_More.ItemHeight = 20;
		this.Col_More.Items.AddRange(new object[8] { "1", "2", "3", "4", "5", "6", "7", "8" });
		this.Col_More.Name = "Col_More";
		this.Col_More.ReadOnly = true;
		this.Col_More.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_More.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.Col_More.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		resources.ApplyResources(this.btn_ClrChn, "btn_ClrChn");
		this.btn_ClrChn.Name = "btn_ClrChn";
		this.btn_ClrChn.UseVisualStyleBackColor = true;
		this.btn_ClrChn.Click += new System.EventHandler(btn_ClrChn_Click);
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		base.Controls.Add(this.btn_ClrChn);
		base.Controls.Add(this.dGV);
		base.KeyPreview = true;
		base.Name = "Frm_ZoneChn";
		base.ShowInTaskbar = false;
		base.Load += new System.EventHandler(FormZone2_Load);
		base.Activated += new System.EventHandler(Frm_ZoneChn_Activated);
		((System.ComponentModel.ISupportInitialize)this.dGV).EndInit();
		base.ResumeLayout(false);
	}

	public Frm_ZoneChn(DataApp data)
	{
		InitializeComponent();
		tdata = data;
	}

	private void FormZone2_Load(object sender, EventArgs e)
	{
		string[] strItems = new string[14];
		string tmp = "";
		if (FormMain.lang == "Chinese")
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
		}
		ComponentResourceManager crm = new ComponentResourceManager(typeof(Frm_ZoneChn));
		foreach (DataGridViewColumn col in dGV.Columns)
		{
			crm.ApplyResources(col, col.Name);
		}
		LanguageSel.LoadLanguage(this, typeof(Frm_ZoneChn));
		for (int i = 0; i < 14; i++)
		{
			LanguageSel.ChnCoBox.TryGetValue(i.ToString(), out tmp);
			strItems[i] = tmp;
		}
		Col_Power.Items.Clear();
		if (2 == FormMain.HIGH_VALID)
		{
			Col_Power.Items.Add(strItems[6]);
			Col_Power.Items.Add(strItems[7]);
			Col_Power.Items.Add(strItems[8]);
		}
		else
		{
			Col_Power.Items.Add(strItems[6]);
			Col_Power.Items.Add(strItems[8]);
		}
		dGV.CurrentCell = null;
		for (int i = 0; i < DataApp.Zone_Max_Chn_Num; i++)
		{
			DataGridViewRow r = new DataGridViewRow();
			r.CreateCells(dGV);
			r.SetValues(i + 1);
			r.ReadOnly = true;
			r.Cells[1].ReadOnly = false;
			dGV.Rows.Add(r);
		}
	}

	private void DispDgvTbl(int rowidx)
	{
		int start = 0;
		start = chBaseAddr + rowidx;
		if (tdata.dataChnInfor[start].Power > FormMain.HIGH_VALID)
		{
			tdata.dataChnInfor[start].Power = 0;
		}
		dGV.Rows[rowidx].ReadOnly = false;
		dGV.Rows[rowidx].Cells[0].ReadOnly = true;
		if (tdata.dataChnInfor[start].RxFreq != "")
		{
			dGV.Rows[rowidx].Cells[1].Value = tdata.dataChnInfor[start].RxFreq;
			dGV.Rows[rowidx].Cells[2].Value = tdata.dataChnInfor[start].TxFreq;
			dGV.Rows[rowidx].Cells[3].Value = tdata.dataChnInfor[start].RxCtsDcs;
			dGV.Rows[rowidx].Cells[4].Value = tdata.dataChnInfor[start].TxCtsDcs;
			dGV.Rows[rowidx].Cells[5].Value = Col_Power.Items[tdata.dataChnInfor[start].Power].ToString();
			dGV.Rows[rowidx].Cells[6].Value = Col_Band.Items[tdata.dataChnInfor[start].Wideth].ToString();
			dGV.Rows[rowidx].Cells[7].Value = tdata.dataChnInfor[start].ChnName;
			dGV.Rows[rowidx].Cells[8].Value = ">>";
		}
	}

	public void ZoneChnDataDisp(int pos)
	{
		int i = 0;
		int total = 0;
		int start = 0;
		string str = string.Empty;
		if (pos != zoneIdx)
		{
			dGV.FirstDisplayedScrollingRowIndex = 0;
		}
		posIdx = pos + 1;
		zoneIdx = pos;
		chBaseAddr = pos * DataApp.Zone_Max_Chn_Num;
		start = chBaseAddr;
		for (i = 0; i < DataApp.Zone_Max_Chn_Num; i++)
		{
			if (tdata.dataChnInfor[start + i].UseFlg == 1 && tdata.dataZoneInfor[pos].GetChnID(i) != 65535)
			{
				DispDgvTbl(i);
			}
			else
			{
				DispNewChannel(i, 0);
			}
		}
		nodeFlg = false;
	}

	private void UpDate_EmergData(int ros, int ZorC)
	{
		int validZ = 0;
		if (ZorC == 0)
		{
			for (int i = 0; i < DatEmergInfo.Max_Emerg_Sys_Num; i++)
			{
				if (tdata.dataEmergInfor[i].Zone == zoneIdx && tdata.dataEmergInfor[i].Chn == ros)
				{
					tdata.dataEmergInfor[i].ChSel = 1;
				}
			}
			return;
		}
		for (int i = 0; i < DataApp.Zone_Max_Num; i++)
		{
			if (tdata.dataZoneInfor[i].ChnNum > 0)
			{
				validZ = i;
				break;
			}
		}
		for (int i = 0; i < DatEmergInfo.Max_Emerg_Sys_Num; i++)
		{
			if (tdata.dataEmergInfor[i].Zone == zoneIdx)
			{
				tdata.dataEmergInfor[i].ChSel = 1;
				tdata.dataEmergInfor[i].Zone = (byte)validZ;
			}
		}
	}

	private void UpDate_SettingData(int zIdx)
	{
		if (tdata.dataGenSetInfor.ChAZone == zIdx)
		{
			for (int i = 0; i < 10; i++)
			{
				if (tdata.dataZoneInfor[i].ChnNum > 0)
				{
					tdata.dataGenSetInfor.ChAZone = (byte)i;
					break;
				}
			}
		}
		if (tdata.dataGenSetInfor.ChBZone != zIdx)
		{
			return;
		}
		for (int i = 0; i < 10; i++)
		{
			if (tdata.dataZoneInfor[i].ChnNum > 0)
			{
				tdata.dataGenSetInfor.ChBZone = (byte)i;
				break;
			}
		}
	}

	private void UpDate_SettingChnData(int chidx)
	{
		int start = tdata.dataGenSetInfor.ChAZone * DataApp.Zone_Max_Chn_Num;
		if (tdata.dataGenSetInfor.ChANum == chidx)
		{
			for (int i = start; i < start + 64; i++)
			{
				if (tdata.dataChnInfor[i].UseFlg == 1)
				{
					tdata.dataGenSetInfor.ChANum = (byte)(i - start);
					break;
				}
			}
		}
		start = tdata.dataGenSetInfor.ChBZone * DataApp.Zone_Max_Chn_Num;
		if (tdata.dataGenSetInfor.ChBNum != chidx)
		{
			return;
		}
		for (int i = start; i < start + 64; i++)
		{
			if (tdata.dataChnInfor[i].UseFlg == 1)
			{
				tdata.dataGenSetInfor.ChBNum = (byte)(i - start);
				break;
			}
		}
	}

	private void NewChannelUpdateZone(int rIndex, int mode)
	{
		FormMain cfrm = (FormMain)base.ParentForm;
		if (mode == 1)
		{
			if (tdata.dataZoneInfor[zoneIdx].ChnNum == 0)
			{
				if (tdata.dataZoneInfor[zoneIdx].ZoneName == "")
				{
					tdata.dataZoneInfor[zoneIdx].ZoneName = "Zone " + (zoneIdx + 1);
				}
				if (tdata.dataDevice.EnZone == 1)
				{
					cfrm.SetNodeZoneConfigName(tdata.dataZoneInfor[zoneIdx].ZoneName, 2, zoneIdx + 1);
				}
				DatZoneInfo.ZoneTotal++;
			}
			tdata.dataZoneInfor[zoneIdx].SetChnID(rIndex, rIndex + chBaseAddr);
			tdata.dataZoneInfor[zoneIdx].ChnNum++;
		}
		else
		{
			tdata.dataZoneInfor[zoneIdx].SetChnID(rIndex, 65535);
			tdata.dataZoneInfor[zoneIdx].ChnNum--;
			if (tdata.dataZoneInfor[zoneIdx].ChnNum == 0)
			{
				DatZoneInfo.ZoneTotal--;
				UpDate_SettingData(zoneIdx);
				UpDate_EmergData(rIndex, 1);
			}
			else
			{
				UpDate_EmergData(rIndex, 0);
			}
		}
	}

	private void HandRxFreq(int rowIndex)
	{
		FormMain cfrm = (FormMain)base.ParentForm;
		string value = "";
		int start = chBaseAddr + rowIndex;
		if (dGV.CurrentCell.Value == null)
		{
			if (tdata.dataChnInfor[start].UseFlg == 1)
			{
				if (DatChannelInfo.ChnTotal <= 1)
				{
					dGV.Rows[rowIndex].Cells[1].Value = tdata.dataChnInfor[start].RxFreq;
					return;
				}
				tdata.dataChnInfor[start].UseFlg = 0;
				NewChannelUpdateZone(rowIndex, 0);
				UpDate_SettingChnData(rowIndex);
				DispNewChannel(rowIndex, 0);
				DatChannelInfo.ChnTotal--;
			}
			return;
		}
		if (dGV.CurrentCell.Value.ToString() == "")
		{
			if (tdata.dataChnInfor[start].UseFlg == 1)
			{
				tdata.dataChnInfor[start].UseFlg = 0;
				NewChannelUpdateZone(rowIndex, 0);
				UpDate_SettingChnData(rowIndex);
				DispNewChannel(rowIndex, 0);
				DatChannelInfo.ChnTotal--;
			}
			return;
		}
		value = Convert.ToString(dGV.CurrentCell.Value);
		value = tdata.dataDevice.Adjust_Freq(value).ToString("0.00000");
		if (tdata.dataChnInfor[start].UseFlg == 1)
		{
			dGV.Rows[rowIndex].Cells[1].Value = value;
			if (0 != string.Compare(value, tdata.dataChnInfor[start].RxFreq))
			{
				tdata.dataChnInfor[start].RxFreq = value;
				tdata.dataChnInfor[start].TxFreq = value;
				dGV.Rows[rowIndex].Cells[2].Value = value;
			}
		}
		else
		{
			cfrm.AddChannelData(start, value);
			tdata.dataChnInfor[start].UseFlg = 1;
			NewChannelUpdateZone(rowIndex, 1);
			DispNewChannel(rowIndex, 1);
		}
	}

	private void DivFreqDisp(int pos)
	{
		double doubletxfreq = Convert.ToDouble(tdata.dataChnInfor[pos].TxFreq);
		double doublerxfreq = Convert.ToDouble(tdata.dataChnInfor[pos].RxFreq);
		double doufreq;
		if (doubletxfreq < doublerxfreq)
		{
			tdata.dataChnInfor[pos].Offsetdir = 2;
			doufreq = doublerxfreq - doubletxfreq;
		}
		else if (doubletxfreq > doublerxfreq)
		{
			tdata.dataChnInfor[pos].Offsetdir = 1;
			doufreq = doubletxfreq - doublerxfreq;
		}
		else
		{
			tdata.dataChnInfor[pos].Offsetdir = 0;
			doufreq = 0.0;
		}
		tdata.dataChnInfor[pos].DivFreq = doufreq.ToString("0.00000");
	}

	private void HandTxFreq(int rowIndex)
	{
		string value = "";
		int start = chBaseAddr + rowIndex;
		if (dGV.CurrentCell.Value == null)
		{
			dGV.Rows[rowIndex].Cells[2].Value = tdata.dataChnInfor[start].TxFreq;
			return;
		}
		if (dGV.CurrentCell.Value.ToString() == "")
		{
			dGV.Rows[rowIndex].Cells[2].Value = tdata.dataChnInfor[start].TxFreq;
			return;
		}
		value = Convert.ToString(dGV.CurrentCell.Value);
		value = tdata.dataDevice.Adjust_Freq(value).ToString("0.00000");
		tdata.dataChnInfor[start].TxFreq = value;
		dGV.Rows[rowIndex].Cells[2].Value = value;
		DivFreqDisp(start);
	}

	private void DispNewChannel(int rowIndex, int mode)
	{
		if (mode == 1)
		{
			DispDgvTbl(rowIndex);
			return;
		}
		string[] values = new string[9]
		{
			(rowIndex + 1).ToString(),
			"",
			"",
			"",
			"",
			"",
			"",
			"",
			""
		};
		dGV.Rows[rowIndex].SetValues(values);
		for (int i = 0; i < 9; i++)
		{
			if (i != 1)
			{
				dGV.Rows[rowIndex].Cells[i].ReadOnly = true;
			}
		}
	}

	private void UpdateCTSCDS(int rowIndex, int columnIndex, string value)
	{
		int start = chBaseAddr + rowIndex;
		if (columnIndex == 3)
		{
			tdata.dataChnInfor[start].RxCtsDcs = value;
			tdata.dataChnInfor[start].TxCtsDcs = value;
			dGV.Rows[rowIndex].Cells[4].Value = value;
			if (tdata.dataChnInfor[start].RxCtsDcs == "OFF")
			{
				if (tdata.dataChnInfor[start].SignalType == 0)
				{
					tdata.dataChnInfor[start].SqType = 0;
				}
				else
				{
					tdata.dataChnInfor[start].SqType = 2;
				}
			}
			else if (tdata.dataChnInfor[start].SignalType == 0)
			{
				tdata.dataChnInfor[start].SqType = 1;
			}
			else
			{
				tdata.dataChnInfor[start].SqType = 3;
			}
		}
		else
		{
			tdata.dataChnInfor[start].TxCtsDcs = value;
		}
		dGV.CurrentCell.Value = value;
	}

	private void HandSubTone(int colidx, int rowidx)
	{
		int start = chBaseAddr + rowidx;
		if (dGV.CurrentCell.Value == null)
		{
			if (colidx == 3)
			{
				dGV.CurrentCell.Value = tdata.dataChnInfor[start].RxCtsDcs;
			}
			else
			{
				dGV.CurrentCell.Value = tdata.dataChnInfor[start].TxCtsDcs;
			}
			return;
		}
		string strValue = dGV.CurrentCell.Value.ToString().ToUpper();
		if (Col_RxCTS.Items.IndexOf(dGV.CurrentCell.Value) == -1)
		{
			double buf = 0.0;
			int len = strValue.Length;
			if (strValue[0] != 'D' || len != 5)
			{
				try
				{
					buf = double.Parse(dGV.CurrentCell.Value.ToString());
					if (buf >= 60.0 && buf <= 260.0)
					{
						string str = buf.ToString();
						int indexOfPoint = str.IndexOf('.');
						if (indexOfPoint == -1)
						{
							str += ".0";
						}
						else if (indexOfPoint == str.Length - 1)
						{
							str += "0";
						}
						else if (indexOfPoint != str.Length - 2)
						{
							str = str.Remove(indexOfPoint + 2, str.Length - 1 - (indexOfPoint + 1));
						}
						UpdateCTSCDS(rowidx, colidx, str);
					}
					else if (colidx == 3)
					{
						dGV.CurrentCell.Value = tdata.dataChnInfor[start].RxCtsDcs;
					}
					else
					{
						dGV.CurrentCell.Value = tdata.dataChnInfor[start].TxCtsDcs;
					}
					return;
				}
				catch
				{
					if (colidx == 3)
					{
						dGV.CurrentCell.Value = tdata.dataChnInfor[start].RxCtsDcs;
					}
					else
					{
						dGV.CurrentCell.Value = tdata.dataChnInfor[start].TxCtsDcs;
					}
					return;
				}
			}
			if ((strValue[4] == 'I' || strValue[4] == 'N') && strValue[1] >= '0' && strValue[1] <= '7' && strValue[2] >= '0' && strValue[2] <= '7' && strValue[3] >= '0' && strValue[3] <= '7')
			{
				UpdateCTSCDS(rowidx, colidx, strValue);
			}
			else if (colidx == 3)
			{
				dGV.CurrentCell.Value = tdata.dataChnInfor[start].RxCtsDcs;
			}
			else
			{
				dGV.CurrentCell.Value = tdata.dataChnInfor[start].TxCtsDcs;
			}
		}
		else
		{
			UpdateCTSCDS(rowidx, colidx, dGV.CurrentCell.Value.ToString());
		}
	}

	private void dGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		if (dGV.CurrentCell != null)
		{
			int rowIndex = dGV.CurrentCell.RowIndex;
			int columnIndex = dGV.CurrentCell.ColumnIndex;
			string value = "";
			int start = chBaseAddr + rowIndex;
			switch (columnIndex)
			{
			case 1:
				HandRxFreq(rowIndex);
				break;
			case 2:
				HandTxFreq(rowIndex);
				break;
			case 3:
			case 4:
				HandSubTone(columnIndex, rowIndex);
				break;
			case 5:
				tdata.dataChnInfor[start].Power = (byte)Col_Power.Items.IndexOf(dGV.CurrentCell.Value);
				break;
			case 6:
				tdata.dataChnInfor[start].Wideth = (byte)Col_Band.Items.IndexOf(dGV.CurrentCell.Value);
				break;
			case 7:
				break;
			case 8:
				break;
			}
		}
	}

	private void dGV_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dGV.CurrentCell == null)
		{
			return;
		}
		int indexCol = e.ColumnIndex;
		int indexRow = e.RowIndex;
		if (indexRow == -1 || indexCol == -1)
		{
			return;
		}
		switch (indexCol)
		{
		case 0:
			dGV.CurrentCell = dGV.Rows[indexRow].Cells[1];
			break;
		case 8:
		{
			if (!(dGV.CurrentCell.Value.ToString() == ">>"))
			{
				break;
			}
			int idx = ((tdata.dataDevice.EnZone != 1) ? chBaseAddr : indexRow);
			if (new Frm_Chn(tdata, chBaseAddr + indexRow, tdata.dataZoneInfor[idx].ChnNum).ShowDialog() != DialogResult.Cancel)
			{
				break;
			}
			for (int i = 0; i < DataApp.Zone_Max_Chn_Num; i++)
			{
				if (tdata.dataChnInfor[chBaseAddr + i].UseFlg == 1)
				{
					DispDgvTbl(i);
				}
			}
			break;
		}
		}
	}

	private void dGV_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
	{
		switch (dGV.CurrentCell.ColumnIndex)
		{
		case 1:
		case 2:
			dgvTxt = (DataGridViewTextBoxEditingControl)e.Control;
			dgvTxt.SelectAll();
			dgvTxt.KeyPress -= Cells_KeyPress;
			dgvTxt.KeyPress += Cells_KeyPress;
			break;
		case 7:
			dgvName = (DataGridViewTextBoxEditingControl)e.Control;
			dgvName.SelectAll();
			dgvName.KeyPress -= tBox_Name_KeyPress;
			dgvName.KeyPress += tBox_Name_KeyPress;
			dgvName.TextChanged -= EditingTB_TextChanged;
			dgvName.TextChanged += EditingTB_TextChanged;
			break;
		}
	}

	private void Cells_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (char.IsNumber(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == '\b')
		{
			e.Handled = false;
			if (dgvTxt.Text.Length == 0 && e.KeyChar == '.')
			{
				e.Handled = true;
			}
		}
		else
		{
			e.Handled = true;
		}
	}

	private void EditingTB_TextChanged(object sender, EventArgs e)
	{
		int dir = 0;
		int rowIndex = dGV.CurrentCell.RowIndex;
		string value = "";
		string tmp = "";
		int start = chBaseAddr + rowIndex;
		if (dGV.CurrentCell.ColumnIndex != 7)
		{
			return;
		}
		value = (sender as TextBox).Text;
		if (value == "")
		{
			tdata.dataChnInfor[start].ChnName = "";
			return;
		}
		if (DataApp.GetLength(value) >= 16)
		{
			dir = 1;
		}
		tmp = ((sender as TextBox).Text = DataApp.StrFormat(value, 16));
		tdata.dataChnInfor[start].ChnName = tmp;
		if (dir == 1)
		{
			dgvName.SelectionStart = tmp.Length;
		}
	}

	private void tBox_Name_KeyPress(object sender, KeyPressEventArgs e)
	{
		e.Handled = false;
	}

	public static Frm_ZoneChn getInstance(FormMain father, DataApp tdata)
	{
		Frm_ZoneChn form = new Frm_ZoneChn(tdata);
		form.MdiParent = father;
		return form;
	}

	public static void SetChnPos(int pos)
	{
		posIdx = pos + 1;
	}

	private void Frm_ZoneChn_Activated(object sender, EventArgs e)
	{
		if (nodeFlg)
		{
			nodeFlg = false;
		}
		else
		{
			ZoneChnDataDisp(posIdx - 1);
		}
	}

	private DialogResult suggestionForSaveFile()
	{
		if (FormMain.lang == "Chinese")
		{
			return MessageBox.Show("将清除信道数据?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		}
		return MessageBox.Show("Will clear channel data?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
	}

	private void btn_ClrChn_Click(object sender, EventArgs e)
	{
		if (suggestionForSaveFile() != DialogResult.Yes)
		{
			return;
		}
		int start = chBaseAddr;
		for (int i = DataApp.Zone_Max_Chn_Num - 1; i >= 0; i--)
		{
			if (tdata.dataChnInfor[start + i].UseFlg == 1)
			{
				if (DatChannelInfo.ChnTotal <= 1)
				{
					dGV.Rows[i].Cells[1].Value = tdata.dataChnInfor[start + i].RxFreq;
					break;
				}
				tdata.dataChnInfor[start + i].UseFlg = 0;
				NewChannelUpdateZone(i, 0);
				UpDate_SettingChnData(i);
				DispNewChannel(i, 0);
				DatChannelInfo.ChnTotal--;
			}
		}
	}
}
