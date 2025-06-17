using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using CPS_5RH.Data;

namespace CPS_5RH;

public class Frm_Aprs : Form
{
	private DataApp tdata = null;

	private DataGridViewTextBoxEditingControl dgvTxt = null;

	private DataGridViewTextBoxEditingControl recvFilterdgvTxt = null;

	private IContainer components = null;

	private ComboBox coBox_Src;

	private ComboBox coBox_Des;

	private Label label1;

	private Label label5;

	private TextBox tb_SrcNo;

	private Label label3;

	private TextBox tb_DesNo;

	private Label lbl4;

	private Label lbl9;

	private Label lbl8;

	private NumericUpDown Num_PreTim;

	private DataGridView dgv_Aprs;

	private NumericUpDown Num_CodeDly;

	private DataGridViewTextBoxColumn Col_CallSign;

	private DataGridViewComboBoxColumn Col_SSID;

	private GroupBox groupBox1;

	private Label lbl1;

	private Label lbl2;

	private Label label2;

	private Label label4;

	private Label label6;

	private Label label7;

	private Label label8;

	private Label label9;

	private Label label10;

	private Label label11;

	private Label label12;

	private Label label13;

	private Label label14;

	private Label label15;

	private Label label16;

	private Label label18;

	private Label label19;

	private Label label17;

	private Label label20;

	private Label label21;

	private Label label22;

	private Label label23;

	private Label label24;

	private Label label25;

	private Label label26;

	private Label label27;

	private Label label28;

	private CheckBox checkBox_pos;

	private CheckBox checkBox_mice;

	private CheckBox checkBox_item;

	private CheckBox checkBox_obj;

	private CheckBox checkBox_starep;

	private CheckBox checkBox_nmearep;

	private CheckBox checkBox_wxrep;

	private CheckBox checkBox_msg;

	private CheckBox checkBox_other;

	private ComboBox Cbox_SendTimer;

	private ComboBox Cbox_Beacon;

	private ComboBox Cbox_BeaconType;

	private ComboBox Cbox_AprsDisTime;

	private ComboBox Cbox_Lat;

	private ComboBox Cbox_Long;

	private ComboBox Cbox_Pass;

	private ComboBox comboBox_sendpow;

	private Label label29;

	private Label label31;

	private TextBox textBox_posicon;

	private Label label30;

	private TextBox textBox_postable;

	private TextBox textBox_sendtxt;

	private Label label36;

	private ComboBox comboBox_band;

	private Label label35;

	private Label label33;

	private ComboBox comboBox_beep;

	private Label label32;

	private DataGridView dataGridView_recvfilter;

	private NumericUpDown Num_sendtimein;

	private NumericUpDown Num_BeaconHeight;

	private NumericUpDown Num_Lat;

	private NumericUpDown Num_Long;

	private TextBox TxtBox_SendFreq1;

	private TextBox TxtBox_SendFreq2;

	private TextBox TxtBox_SendFreq3;

	private TextBox TxtBox_SendFreq4;

	private TextBox TxtBox_SendFreq8;

	private TextBox TxtBox_SendFreq7;

	private TextBox TxtBox_SendFreq6;

	private TextBox TxtBox_SendFreq5;

	private ComboBox coBox_Ctdcs;

	private DataGridViewTextBoxColumn RxCallSign;

	private DataGridViewComboBoxColumn RxCallSignSSID;

	private DataGridViewComboBoxColumn RxCallSignFilter;

	private Label label34;

	private ComboBox comboBoxPTTID;

	private ComboBox comboBoxMiceType;

	private Label label37;

	public Frm_Aprs(DataApp data)
	{
		InitializeComponent();
		tdata = data;
	}

	public void PbookDataDisp()
	{
		int i = 0;
		for (i = 0; i < 8; i++)
		{
			dgv_Aprs.Rows[i].HeaderCell.Value = (i + 1).ToString();
			dgv_Aprs.Rows[i].Cells[0].Value = tdata.dataAprsSet.GetCallSignNo(i);
			dgv_Aprs.Rows[i].Cells[1].Value = Col_SSID.Items[tdata.dataAprsSet.GetCallSignID(i)].ToString();
		}
		for (i = 0; i < 32; i++)
		{
			dataGridView_recvfilter.Rows[i].HeaderCell.Value = (i + 1).ToString();
			dataGridView_recvfilter.Rows[i].Cells[0].Value = tdata.dataAprsSet.GetRxCallSignNo(i);
			dataGridView_recvfilter.Rows[i].Cells[1].Value = RxCallSignSSID.Items[tdata.dataAprsSet.GetRxCallSignID(i)].ToString();
			dataGridView_recvfilter.Rows[i].Cells[2].Value = RxCallSignFilter.Items[tdata.dataAprsSet.GetRxFilter(i)];
		}
	}

	private void bangDingSysData()
	{
		coBox_Ctdcs.Text = tdata.dataAprsSet.Ctdcs;
		if (tdata.dataAprsSet.SrcID > 15)
		{
			tdata.dataAprsSet.SrcID = 0;
		}
		if (tdata.dataAprsSet.DesID > 15)
		{
			tdata.dataAprsSet.DesID = 0;
		}
		if (tdata.dataAprsSet.PreTime > 2550)
		{
			tdata.dataAprsSet.PreTime = 0;
		}
		if (tdata.dataAprsSet.CodeDly > 2550)
		{
			tdata.dataAprsSet.CodeDly = 0;
		}
		if (tdata.dataAprsSet.HeightType == 0)
		{
			Num_BeaconHeight.Maximum = 65535m;
		}
		else
		{
			Num_BeaconHeight.Maximum = 19975m;
		}
		Cbox_BeaconType.SelectedIndex = tdata.dataAprsSet.HeightType;
		TxtBox_SendFreq1.Text = tdata.dataAprsSet.Freq1;
		TxtBox_SendFreq2.Text = tdata.dataAprsSet.Freq2;
		TxtBox_SendFreq3.Text = tdata.dataAprsSet.Freq3;
		TxtBox_SendFreq4.Text = tdata.dataAprsSet.Freq4;
		TxtBox_SendFreq5.Text = tdata.dataAprsSet.Freq5;
		TxtBox_SendFreq6.Text = tdata.dataAprsSet.Freq6;
		TxtBox_SendFreq7.Text = tdata.dataAprsSet.Freq7;
		TxtBox_SendFreq8.Text = tdata.dataAprsSet.Freq8;
		textBox_sendtxt.Text = tdata.dataAprsSet.Txt;
		Num_sendtimein.DataBindings.Add("Value", tdata.dataAprsSet, "SendInterval", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		Num_BeaconHeight.DataBindings.Add("Value", tdata.dataAprsSet, "Height", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		Num_PreTim.DataBindings.Add("Value", tdata.dataAprsSet, "PreTime", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		Num_CodeDly.DataBindings.Add("Value", tdata.dataAprsSet, "CodeDly", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		Num_Lat.DataBindings.Add("Value", tdata.dataAprsSet, "Lat", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		Num_Long.DataBindings.Add("Value", tdata.dataAprsSet, "Long", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		Cbox_SendTimer.DataBindings.Add("SelectedIndex", tdata.dataAprsSet, "RegularlySend", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		Cbox_Beacon.DataBindings.Add("SelectedIndex", tdata.dataAprsSet, "Beacon", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		Cbox_AprsDisTime.DataBindings.Add("SelectedIndex", tdata.dataAprsSet, "APRSDisplayTime", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_Src.DataBindings.Add("SelectedIndex", tdata.dataAprsSet, "SrcID", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_Des.DataBindings.Add("SelectedIndex", tdata.dataAprsSet, "DesID", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		Cbox_Lat.DataBindings.Add("SelectedIndex", tdata.dataAprsSet, "LatDir", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		Cbox_Long.DataBindings.Add("SelectedIndex", tdata.dataAprsSet, "LongDir", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		comboBox_sendpow.DataBindings.Add("SelectedIndex", tdata.dataAprsSet, "Power", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		Cbox_Pass.DataBindings.Add("SelectedIndex", tdata.dataAprsSet, "PassAll", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		comboBox_band.DataBindings.Add("SelectedIndex", tdata.dataAprsSet, "Band", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		comboBox_beep.DataBindings.Add("SelectedIndex", tdata.dataAprsSet, "BeepTone", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		comboBoxPTTID.DataBindings.Add("SelectedIndex", tdata.dataAprsSet, "PttId", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		comboBoxMiceType.DataBindings.Add("SelectedIndex", tdata.dataAprsSet, "MicEType", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		tb_SrcNo.DataBindings.Add("Text", tdata.dataAprsSet, "SrcNo", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		tb_DesNo.DataBindings.Add("Text", tdata.dataAprsSet, "DesNo", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		textBox_postable.DataBindings.Add("Text", tdata.dataAprsSet, "PostionTable", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		textBox_posicon.DataBindings.Add("Text", tdata.dataAprsSet, "PostionIcon", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		TxtBox_SendFreq1.DataBindings.Add("Text", tdata.dataAprsSet, "Freq1", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		TxtBox_SendFreq2.DataBindings.Add("Text", tdata.dataAprsSet, "Freq2", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		TxtBox_SendFreq3.DataBindings.Add("Text", tdata.dataAprsSet, "Freq3", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		TxtBox_SendFreq4.DataBindings.Add("Text", tdata.dataAprsSet, "Freq4", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		TxtBox_SendFreq5.DataBindings.Add("Text", tdata.dataAprsSet, "Freq5", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		TxtBox_SendFreq6.DataBindings.Add("Text", tdata.dataAprsSet, "Freq6", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		TxtBox_SendFreq7.DataBindings.Add("Text", tdata.dataAprsSet, "Freq7", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		TxtBox_SendFreq8.DataBindings.Add("Text", tdata.dataAprsSet, "Freq8", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		checkBox_pos.DataBindings.Add("Checked", tdata.dataAprsSet, "Position", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		checkBox_mice.DataBindings.Add("Checked", tdata.dataAprsSet, "MicE", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		checkBox_obj.DataBindings.Add("Checked", tdata.dataAprsSet, "Object", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		checkBox_item.DataBindings.Add("Checked", tdata.dataAprsSet, "Item", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		checkBox_msg.DataBindings.Add("Checked", tdata.dataAprsSet, "Message", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		checkBox_wxrep.DataBindings.Add("Checked", tdata.dataAprsSet, "WxReport", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		checkBox_nmearep.DataBindings.Add("Checked", tdata.dataAprsSet, "NMEAReport", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		checkBox_starep.DataBindings.Add("Checked", tdata.dataAprsSet, "StatusReport", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		checkBox_other.DataBindings.Add("Checked", tdata.dataAprsSet, "Other", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
	}

	private void Frm_Pbook_Load(object sender, EventArgs e)
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
		ComponentResourceManager crm = new ComponentResourceManager(typeof(Frm_Aprs));
		LanguageSel.LoadLanguage(this, typeof(Frm_Aprs));
		for (int i = 0; i < 14; i++)
		{
			LanguageSel.AprsCoBox.TryGetValue(i.ToString(), out tmp);
			strItems[i] = tmp;
		}
		dgv_Aprs.Rows.Clear();
		dgv_Aprs.Rows.Add(8);
		dataGridView_recvfilter.Rows.Clear();
		dataGridView_recvfilter.Rows.Add(32);
		comboBox_sendpow.Items.Clear();
		if (FormMain.HIGH_VALID == 2)
		{
			comboBox_sendpow.Items.Add(strItems[2]);
			comboBox_sendpow.Items.Add(strItems[3]);
			comboBox_sendpow.Items.Add(strItems[4]);
		}
		else
		{
			if (tdata.dataAprsSet.Power == 2)
			{
				tdata.dataAprsSet.Power = 1;
			}
			comboBox_sendpow.Items.Add(strItems[2]);
			comboBox_sendpow.Items.Add(strItems[4]);
		}
		Cbox_SendTimer.Items[0] = strItems[0];
		Cbox_BeaconType.Items[0] = strItems[10];
		Cbox_BeaconType.Items[1] = strItems[11];
		Cbox_Beacon.Items[0] = strItems[0];
		Cbox_Beacon.Items[1] = strItems[1];
		Cbox_AprsDisTime.Items[0] = strItems[0];
		Cbox_Pass.Items[0] = strItems[0];
		Cbox_Pass.Items[1] = strItems[1];
		comboBox_beep.Items[0] = strItems[0];
		comboBox_beep.Items[1] = strItems[1];
		Cbox_Lat.Items[0] = strItems[7];
		Cbox_Lat.Items[1] = strItems[8];
		Cbox_Long.Items[0] = strItems[5];
		Cbox_Long.Items[1] = strItems[6];
		checkBox_pos.Text = strItems[9];
		checkBox_mice.Text = strItems[9];
		checkBox_obj.Text = strItems[9];
		checkBox_item.Text = strItems[9];
		checkBox_msg.Text = strItems[9];
		checkBox_wxrep.Text = strItems[9];
		checkBox_nmearep.Text = strItems[9];
		checkBox_starep.Text = strItems[9];
		checkBox_other.Text = strItems[9];
		comboBoxPTTID.Items[0] = strItems[0];
		comboBoxPTTID.Items[1] = strItems[12];
		comboBoxPTTID.Items[2] = strItems[13];
		bangDingSysData();
	}

	private void Cells_KeyPress(object sender, KeyPressEventArgs e)
	{
		if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar < 'a' || e.KeyChar > 'z') && (e.KeyChar < 'A' || e.KeyChar > 'Z') && e.KeyChar != '\b')
		{
			e.Handled = true;
		}
	}

	private void dgv_Aprs_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
	{
		if (dgv_Aprs.CurrentCell.ColumnIndex == 0)
		{
			dgvTxt = (DataGridViewTextBoxEditingControl)e.Control;
			dgvTxt.SelectAll();
			dgvTxt.KeyPress -= Cells_KeyPress;
			dgvTxt.KeyPress += Cells_KeyPress;
			dgvTxt.TextChanged -= EditingTBName_TextChanged;
			dgvTxt.TextChanged += EditingTBName_TextChanged;
		}
	}

	private void EditingTBName_TextChanged(object sender, EventArgs e)
	{
		int rowIndex = dgv_Aprs.CurrentCell.RowIndex;
		if (tdata.dataAprsSet.GetCallSignNo(rowIndex) != "")
		{
			if (dgvTxt.Text == "")
			{
				tdata.dataAprsSet.SetCallSignNo(rowIndex, "");
				DatAprsSet.CallSign_Total--;
			}
			else
			{
				tdata.dataAprsSet.SetCallSignNo(rowIndex, dgvTxt.Text);
			}
		}
		else
		{
			if (dgvTxt.Text != "")
			{
				DatAprsSet.CallSign_Total++;
			}
			tdata.dataAprsSet.SetCallSignNo(rowIndex, dgvTxt.Text);
		}
	}

	private void dgv_Aprs_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		if (dgv_Aprs.CurrentCell == null)
		{
		}
	}

	private void dgv_Aprs_CurrentCellDirtyStateChanged(object sender, EventArgs e)
	{
		int val = 0;
		if (dgv_Aprs.CurrentCell != null)
		{
			int indexCol = dgv_Aprs.CurrentCell.ColumnIndex;
			int indexRow = dgv_Aprs.CurrentCell.RowIndex;
			if (indexRow != -1 && indexCol != -1 && dgv_Aprs.IsCurrentCellDirty && indexCol == 1)
			{
				dgv_Aprs.CommitEdit(DataGridViewDataErrorContexts.Display);
				val = Col_SSID.Items.IndexOf(dgv_Aprs.CurrentCell.Value);
				tdata.dataAprsSet.SetCallSignID(indexRow, (byte)val);
			}
		}
	}

	private void Cbox_BeaconType_SelectedIndexChanged(object sender, EventArgs e)
	{
		ComboBox tmp = (ComboBox)sender;
		if (tmp.SelectedIndex == 0)
		{
			Num_BeaconHeight.Maximum = 65535m;
		}
		else
		{
			Num_BeaconHeight.Maximum = 19975m;
		}
		tdata.dataAprsSet.HeightType = (byte)tmp.SelectedIndex;
	}

	private void textBox_postable_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && (e.KeyChar < ' ' || e.KeyChar > '\u007f'))
		{
			e.Handled = true;
		}
	}

	private void textBox_posicon_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && (e.KeyChar < ' ' || e.KeyChar > '\u007f'))
		{
			e.Handled = true;
		}
	}

	private void TxtBox_SendFreq_KeyPress(object sender, KeyPressEventArgs e)
	{
		string str = ((TextBox)sender).Text;
		e.Handled = e.KeyChar < '0' || e.KeyChar > '9';
		if (e.KeyChar == '\b')
		{
			e.Handled = false;
		}
		if (e.KeyChar != '.')
		{
			return;
		}
		if (str == "")
		{
			e.Handled = true;
			return;
		}
		string text = str;
		for (int i = 0; i < text.Length; i++)
		{
			if (char.IsPunctuation(text[i]))
			{
				e.Handled = true;
				return;
			}
		}
		e.Handled = false;
	}

	private void TxtBox_SendFreq1_Leave(object sender, EventArgs e)
	{
		string value = ((TextBox)sender).Text;
		if (!string.IsNullOrEmpty(value))
		{
			double doufreq = tdata.dataDevice.Adjust_Freq(value);
			tdata.dataAprsSet.Freq1 = doufreq.ToString("0.00000");
			((TextBox)sender).Text = tdata.dataAprsSet.Freq1;
		}
	}

	private void TxtBox_SendFreq2_Leave(object sender, EventArgs e)
	{
		string value = ((TextBox)sender).Text;
		if (!string.IsNullOrEmpty(value))
		{
			double doufreq = tdata.dataDevice.Adjust_Freq(value);
			tdata.dataAprsSet.Freq2 = doufreq.ToString("0.00000");
			((TextBox)sender).Text = tdata.dataAprsSet.Freq2;
		}
	}

	private void TxtBox_SendFreq3_Leave(object sender, EventArgs e)
	{
		string value = ((TextBox)sender).Text;
		if (!string.IsNullOrEmpty(value))
		{
			double doufreq = tdata.dataDevice.Adjust_Freq(value);
			tdata.dataAprsSet.Freq3 = doufreq.ToString("0.00000");
			((TextBox)sender).Text = tdata.dataAprsSet.Freq3;
		}
	}

	private void TxtBox_SendFreq4_Leave(object sender, EventArgs e)
	{
		string value = ((TextBox)sender).Text;
		if (!string.IsNullOrEmpty(value))
		{
			double doufreq = tdata.dataDevice.Adjust_Freq(value);
			tdata.dataAprsSet.Freq4 = doufreq.ToString("0.00000");
			((TextBox)sender).Text = tdata.dataAprsSet.Freq4;
		}
	}

	private void TxtBox_SendFreq5_Leave(object sender, EventArgs e)
	{
		string value = ((TextBox)sender).Text;
		if (!string.IsNullOrEmpty(value))
		{
			double doufreq = tdata.dataDevice.Adjust_Freq(value);
			tdata.dataAprsSet.Freq5 = doufreq.ToString("0.00000");
			((TextBox)sender).Text = tdata.dataAprsSet.Freq5;
		}
	}

	private void TxtBox_SendFreq6_Leave(object sender, EventArgs e)
	{
		string value = ((TextBox)sender).Text;
		if (!string.IsNullOrEmpty(value))
		{
			double doufreq = tdata.dataDevice.Adjust_Freq(value);
			tdata.dataAprsSet.Freq6 = doufreq.ToString("0.00000");
			((TextBox)sender).Text = tdata.dataAprsSet.Freq6;
		}
	}

	private void TxtBox_SendFreq7_Leave(object sender, EventArgs e)
	{
		string value = ((TextBox)sender).Text;
		if (!string.IsNullOrEmpty(value))
		{
			double doufreq = tdata.dataDevice.Adjust_Freq(value);
			tdata.dataAprsSet.Freq7 = doufreq.ToString("0.00000");
			((TextBox)sender).Text = tdata.dataAprsSet.Freq7;
		}
	}

	private void TxtBox_SendFreq8_Leave(object sender, EventArgs e)
	{
		string value = ((TextBox)sender).Text;
		if (!string.IsNullOrEmpty(value))
		{
			double doufreq = tdata.dataDevice.Adjust_Freq(value);
			tdata.dataAprsSet.Freq8 = doufreq.ToString("0.00000");
			((TextBox)sender).Text = tdata.dataAprsSet.Freq8;
		}
	}

	private void Ctdcs_Leave(object sender, EventArgs e)
	{
		string strValue = coBox_Ctdcs.Text;
		if (strValue == "")
		{
			coBox_Ctdcs.Text = tdata.dataAprsSet.Ctdcs;
			return;
		}
		strValue = strValue.ToUpper();
		if (coBox_Ctdcs.Items.IndexOf(strValue) == -1)
		{
			double buf = 0.0;
			int len = strValue.Length;
			if (strValue[0] != 'D' || len < 5 || (strValue[4] != 'I' && strValue[4] != 'N'))
			{
				try
				{
					buf = double.Parse(strValue);
					if (buf >= 60.0 && buf <= 255.0)
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
						tdata.dataAprsSet.Ctdcs = str;
						coBox_Ctdcs.Text = tdata.dataAprsSet.Ctdcs;
					}
					else
					{
						coBox_Ctdcs.Text = tdata.dataAprsSet.Ctdcs;
					}
					return;
				}
				catch
				{
					coBox_Ctdcs.Text = tdata.dataAprsSet.Ctdcs;
					return;
				}
			}
			if (strValue[1] >= '0' && strValue[1] <= '7' && strValue[2] >= '0' && strValue[2] <= '7' && strValue[3] >= '0' && strValue[3] <= '7' && (strValue[4] == 'I' || strValue[4] == 'N'))
			{
				tdata.dataAprsSet.Ctdcs = strValue;
				coBox_Ctdcs.Text = tdata.dataAprsSet.Ctdcs;
			}
			else
			{
				coBox_Ctdcs.Text = tdata.dataAprsSet.Ctdcs;
			}
		}
		else
		{
			tdata.dataAprsSet.Ctdcs = strValue;
		}
	}

	private void textBox_sendtxt_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsControl(e.KeyChar) && (e.KeyChar < ' ' || e.KeyChar > '\u007f'))
		{
			e.Handled = true;
		}
	}

	private void textBox_sendtxt_Leave(object sender, EventArgs e)
	{
		tdata.dataAprsSet.TxtLength = (byte)((TextBox)sender).TextLength;
		tdata.dataAprsSet.Txt = ((TextBox)sender).Text;
	}

	private void dataGridView_recvfilter_CurrentCellDirtyStateChanged(object sender, EventArgs e)
	{
		int val = 0;
		if (dataGridView_recvfilter.CurrentCell == null)
		{
			return;
		}
		int indexCol = dataGridView_recvfilter.CurrentCell.ColumnIndex;
		int indexRow = dataGridView_recvfilter.CurrentCell.RowIndex;
		if (indexRow != -1 && indexCol != -1 && dataGridView_recvfilter.IsCurrentCellDirty)
		{
			switch (indexCol)
			{
			case 1:
				dataGridView_recvfilter.CommitEdit(DataGridViewDataErrorContexts.Display);
				val = RxCallSignSSID.Items.IndexOf(dataGridView_recvfilter.CurrentCell.Value);
				tdata.dataAprsSet.SetRxCallSignID(indexRow, (byte)val);
				break;
			case 2:
				dataGridView_recvfilter.CommitEdit(DataGridViewDataErrorContexts.Display);
				val = RxCallSignFilter.Items.IndexOf(dataGridView_recvfilter.CurrentCell.Value);
				tdata.dataAprsSet.SetRxFilter(indexRow, (byte)val);
				break;
			}
		}
	}

	private void EditingRxCallsign_TextChanged(object sender, EventArgs e)
	{
		int rowIndex = dataGridView_recvfilter.CurrentCell.RowIndex;
		if (tdata.dataAprsSet.GetRxCallSignNo(rowIndex) != "")
		{
			if (recvFilterdgvTxt.Text == "")
			{
				tdata.dataAprsSet.SetRxCallSignNo(rowIndex, "");
				tdata.dataAprsSet.RxCallSignNum--;
			}
			else
			{
				tdata.dataAprsSet.SetRxCallSignNo(rowIndex, recvFilterdgvTxt.Text);
			}
		}
		else
		{
			if (recvFilterdgvTxt.Text != "")
			{
				tdata.dataAprsSet.RxCallSignNum++;
			}
			tdata.dataAprsSet.SetRxCallSignNo(rowIndex, recvFilterdgvTxt.Text);
		}
	}

	private void dataGridView_recvfilter_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
	{
		if (dataGridView_recvfilter.CurrentCell.ColumnIndex == 0)
		{
			recvFilterdgvTxt = (DataGridViewTextBoxEditingControl)e.Control;
			recvFilterdgvTxt.SelectAll();
			recvFilterdgvTxt.KeyPress -= Cells_KeyPress;
			recvFilterdgvTxt.KeyPress += Cells_KeyPress;
			recvFilterdgvTxt.TextChanged -= EditingRxCallsign_TextChanged;
			recvFilterdgvTxt.TextChanged += EditingRxCallsign_TextChanged;
		}
	}

	private void textBox_sendtxt_TextChanged(object sender, EventArgs e)
	{
		tdata.dataAprsSet.TxtLength = (byte)((TextBox)sender).TextLength;
		tdata.dataAprsSet.Txt = ((TextBox)sender).Text;
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPS_5RH.Frm_Aprs));
		this.coBox_Src = new System.Windows.Forms.ComboBox();
		this.coBox_Des = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.tb_SrcNo = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.tb_DesNo = new System.Windows.Forms.TextBox();
		this.lbl4 = new System.Windows.Forms.Label();
		this.lbl9 = new System.Windows.Forms.Label();
		this.lbl8 = new System.Windows.Forms.Label();
		this.Num_PreTim = new System.Windows.Forms.NumericUpDown();
		this.Num_CodeDly = new System.Windows.Forms.NumericUpDown();
		this.dgv_Aprs = new System.Windows.Forms.DataGridView();
		this.Col_CallSign = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Col_SSID = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.coBox_Ctdcs = new System.Windows.Forms.ComboBox();
		this.dataGridView_recvfilter = new System.Windows.Forms.DataGridView();
		this.RxCallSign = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.RxCallSignSSID = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.RxCallSignFilter = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.textBox_sendtxt = new System.Windows.Forms.TextBox();
		this.label36 = new System.Windows.Forms.Label();
		this.comboBox_band = new System.Windows.Forms.ComboBox();
		this.label35 = new System.Windows.Forms.Label();
		this.label33 = new System.Windows.Forms.Label();
		this.comboBox_beep = new System.Windows.Forms.ComboBox();
		this.label32 = new System.Windows.Forms.Label();
		this.label31 = new System.Windows.Forms.Label();
		this.textBox_posicon = new System.Windows.Forms.TextBox();
		this.label30 = new System.Windows.Forms.Label();
		this.textBox_postable = new System.Windows.Forms.TextBox();
		this.comboBox_sendpow = new System.Windows.Forms.ComboBox();
		this.label29 = new System.Windows.Forms.Label();
		this.lbl1 = new System.Windows.Forms.Label();
		this.lbl2 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.label10 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.label16 = new System.Windows.Forms.Label();
		this.label18 = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.label20 = new System.Windows.Forms.Label();
		this.label21 = new System.Windows.Forms.Label();
		this.label22 = new System.Windows.Forms.Label();
		this.label23 = new System.Windows.Forms.Label();
		this.label24 = new System.Windows.Forms.Label();
		this.label25 = new System.Windows.Forms.Label();
		this.label26 = new System.Windows.Forms.Label();
		this.label27 = new System.Windows.Forms.Label();
		this.label28 = new System.Windows.Forms.Label();
		this.checkBox_pos = new System.Windows.Forms.CheckBox();
		this.checkBox_mice = new System.Windows.Forms.CheckBox();
		this.checkBox_item = new System.Windows.Forms.CheckBox();
		this.checkBox_obj = new System.Windows.Forms.CheckBox();
		this.checkBox_starep = new System.Windows.Forms.CheckBox();
		this.checkBox_nmearep = new System.Windows.Forms.CheckBox();
		this.checkBox_wxrep = new System.Windows.Forms.CheckBox();
		this.checkBox_msg = new System.Windows.Forms.CheckBox();
		this.checkBox_other = new System.Windows.Forms.CheckBox();
		this.Cbox_SendTimer = new System.Windows.Forms.ComboBox();
		this.Cbox_Beacon = new System.Windows.Forms.ComboBox();
		this.Cbox_BeaconType = new System.Windows.Forms.ComboBox();
		this.Cbox_AprsDisTime = new System.Windows.Forms.ComboBox();
		this.Cbox_Lat = new System.Windows.Forms.ComboBox();
		this.Cbox_Long = new System.Windows.Forms.ComboBox();
		this.Cbox_Pass = new System.Windows.Forms.ComboBox();
		this.Num_sendtimein = new System.Windows.Forms.NumericUpDown();
		this.Num_BeaconHeight = new System.Windows.Forms.NumericUpDown();
		this.Num_Lat = new System.Windows.Forms.NumericUpDown();
		this.Num_Long = new System.Windows.Forms.NumericUpDown();
		this.TxtBox_SendFreq1 = new System.Windows.Forms.TextBox();
		this.TxtBox_SendFreq2 = new System.Windows.Forms.TextBox();
		this.TxtBox_SendFreq3 = new System.Windows.Forms.TextBox();
		this.TxtBox_SendFreq4 = new System.Windows.Forms.TextBox();
		this.TxtBox_SendFreq8 = new System.Windows.Forms.TextBox();
		this.TxtBox_SendFreq7 = new System.Windows.Forms.TextBox();
		this.TxtBox_SendFreq6 = new System.Windows.Forms.TextBox();
		this.TxtBox_SendFreq5 = new System.Windows.Forms.TextBox();
		this.label34 = new System.Windows.Forms.Label();
		this.comboBoxPTTID = new System.Windows.Forms.ComboBox();
		this.comboBoxMiceType = new System.Windows.Forms.ComboBox();
		this.label37 = new System.Windows.Forms.Label();
		((System.ComponentModel.ISupportInitialize)this.Num_PreTim).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Num_CodeDly).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dgv_Aprs).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView_recvfilter).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Num_sendtimein).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Num_BeaconHeight).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Num_Lat).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Num_Long).BeginInit();
		base.SuspendLayout();
		this.coBox_Src.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Src.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Src.FormattingEnabled = true;
		this.coBox_Src.Items.AddRange(new object[16]
		{
			"0", "-1", "-2", "-3", "-4", "-5", "-6", "-7", "-8", "-9",
			"-10", "-11", "-12", "-13", "-14", "-15"
		});
		this.coBox_Src.Location = new System.Drawing.Point(101, 126);
		this.coBox_Src.Name = "coBox_Src";
		this.coBox_Src.Size = new System.Drawing.Size(123, 20);
		this.coBox_Src.TabIndex = 66;
		this.coBox_Des.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Des.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Des.FormattingEnabled = true;
		this.coBox_Des.Items.AddRange(new object[16]
		{
			"0", "-1", "-2", "-3", "-4", "-5", "-6", "-7", "-8", "-9",
			"-10", "-11", "-12", "-13", "-14", "-15"
		});
		this.coBox_Des.Location = new System.Drawing.Point(101, 73);
		this.coBox_Des.Name = "coBox_Des";
		this.coBox_Des.Size = new System.Drawing.Size(123, 20);
		this.coBox_Des.TabIndex = 67;
		this.label1.Location = new System.Drawing.Point(6, 127);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(91, 20);
		this.label1.TabIndex = 60;
		this.label1.Text = "自身SSID";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label5.Location = new System.Drawing.Point(5, 74);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(91, 20);
		this.label5.TabIndex = 61;
		this.label5.Text = "目标SSID";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tb_SrcNo.Location = new System.Drawing.Point(100, 99);
		this.tb_SrcNo.MaxLength = 6;
		this.tb_SrcNo.Name = "tb_SrcNo";
		this.tb_SrcNo.Size = new System.Drawing.Size(123, 21);
		this.tb_SrcNo.TabIndex = 62;
		this.tb_SrcNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Cells_KeyPress);
		this.label3.Location = new System.Drawing.Point(10, 100);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(87, 20);
		this.label3.TabIndex = 64;
		this.label3.Text = "自身呼号";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tb_DesNo.Location = new System.Drawing.Point(100, 46);
		this.tb_DesNo.MaxLength = 6;
		this.tb_DesNo.Name = "tb_DesNo";
		this.tb_DesNo.Size = new System.Drawing.Size(123, 21);
		this.tb_DesNo.TabIndex = 63;
		this.tb_DesNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Cells_KeyPress);
		this.lbl4.Location = new System.Drawing.Point(10, 49);
		this.lbl4.Name = "lbl4";
		this.lbl4.Size = new System.Drawing.Size(85, 18);
		this.lbl4.TabIndex = 65;
		this.lbl4.Text = "目标呼号";
		this.lbl4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl9.Location = new System.Drawing.Point(243, 49);
		this.lbl9.Name = "lbl9";
		this.lbl9.Size = new System.Drawing.Size(155, 20);
		this.lbl9.TabIndex = 71;
		this.lbl9.Text = "发送延迟时间(10ms)";
		this.lbl9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl8.Location = new System.Drawing.Point(228, 18);
		this.lbl8.Name = "lbl8";
		this.lbl8.Size = new System.Drawing.Size(170, 20);
		this.lbl8.TabIndex = 70;
		this.lbl8.Text = "预载波时间(10ms)";
		this.lbl8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.Num_PreTim.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.Num_PreTim.Location = new System.Drawing.Point(401, 18);
		this.Num_PreTim.Maximum = new decimal(new int[4] { 255, 0, 0, 0 });
		this.Num_PreTim.Name = "Num_PreTim";
		this.Num_PreTim.Size = new System.Drawing.Size(123, 21);
		this.Num_PreTim.TabIndex = 68;
		this.Num_PreTim.Value = new decimal(new int[4] { 25, 0, 0, 0 });
		this.Num_CodeDly.Increment = new decimal(new int[4] { 10, 0, 0, 0 });
		this.Num_CodeDly.Location = new System.Drawing.Point(401, 49);
		this.Num_CodeDly.Maximum = new decimal(new int[4] { 255, 0, 0, 0 });
		this.Num_CodeDly.Name = "Num_CodeDly";
		this.Num_CodeDly.Size = new System.Drawing.Size(123, 21);
		this.Num_CodeDly.TabIndex = 69;
		this.Num_CodeDly.Value = new decimal(new int[4] { 25, 0, 0, 0 });
		this.dgv_Aprs.AllowUserToAddRows = false;
		this.dgv_Aprs.AllowUserToDeleteRows = false;
		this.dgv_Aprs.AllowUserToResizeColumns = false;
		this.dgv_Aprs.AllowUserToResizeRows = false;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgv_Aprs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgv_Aprs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgv_Aprs.Columns.AddRange(this.Col_CallSign, this.Col_SSID);
		this.dgv_Aprs.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dgv_Aprs.Location = new System.Drawing.Point(22, 221);
		this.dgv_Aprs.MultiSelect = false;
		this.dgv_Aprs.Name = "dgv_Aprs";
		this.dgv_Aprs.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.dgv_Aprs.RowTemplate.Height = 23;
		this.dgv_Aprs.Size = new System.Drawing.Size(277, 205);
		this.dgv_Aprs.TabIndex = 72;
		this.dgv_Aprs.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgv_Aprs_EditingControlShowing);
		this.dgv_Aprs.CurrentCellDirtyStateChanged += new System.EventHandler(dgv_Aprs_CurrentCellDirtyStateChanged);
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
		this.Col_CallSign.DefaultCellStyle = dataGridViewCellStyle2;
		this.Col_CallSign.FillWeight = 20f;
		this.Col_CallSign.HeaderText = "Call Sign";
		this.Col_CallSign.MaxInputLength = 6;
		this.Col_CallSign.Name = "Col_CallSign";
		this.Col_CallSign.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Col_CallSign.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Col_CallSign.Width = 180;
		this.Col_SSID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Col_SSID.DefaultCellStyle = dataGridViewCellStyle3;
		this.Col_SSID.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.Col_SSID.DisplayStyleForCurrentCellOnly = true;
		this.Col_SSID.FillWeight = 80f;
		this.Col_SSID.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.Col_SSID.HeaderText = "SSID";
		this.Col_SSID.Items.AddRange("0", "-1", "-2", "-3", "-4", "-5", "-6", "-7", "-8", "-9", "-10", "-11", "-12", "-13", "-14", "-15");
		this.Col_SSID.Name = "Col_SSID";
		this.Col_SSID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.groupBox1.Controls.Add(this.coBox_Ctdcs);
		this.groupBox1.Controls.Add(this.dataGridView_recvfilter);
		this.groupBox1.Controls.Add(this.textBox_sendtxt);
		this.groupBox1.Controls.Add(this.label36);
		this.groupBox1.Controls.Add(this.comboBox_band);
		this.groupBox1.Controls.Add(this.label35);
		this.groupBox1.Controls.Add(this.label33);
		this.groupBox1.Controls.Add(this.comboBox_beep);
		this.groupBox1.Controls.Add(this.label32);
		this.groupBox1.Controls.Add(this.label31);
		this.groupBox1.Controls.Add(this.textBox_posicon);
		this.groupBox1.Controls.Add(this.label30);
		this.groupBox1.Controls.Add(this.textBox_postable);
		this.groupBox1.Controls.Add(this.comboBox_sendpow);
		this.groupBox1.Controls.Add(this.label29);
		this.groupBox1.Controls.Add(this.tb_SrcNo);
		this.groupBox1.Controls.Add(this.dgv_Aprs);
		this.groupBox1.Controls.Add(this.lbl4);
		this.groupBox1.Controls.Add(this.lbl9);
		this.groupBox1.Controls.Add(this.tb_DesNo);
		this.groupBox1.Controls.Add(this.lbl8);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.Num_PreTim);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.Num_CodeDly);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.coBox_Src);
		this.groupBox1.Controls.Add(this.coBox_Des);
		this.groupBox1.Location = new System.Drawing.Point(49, 252);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(870, 455);
		this.groupBox1.TabIndex = 73;
		this.groupBox1.TabStop = false;
		this.coBox_Ctdcs.DropDownHeight = 80;
		this.coBox_Ctdcs.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Ctdcs.FormattingEnabled = true;
		this.coBox_Ctdcs.IntegralHeight = false;
		this.coBox_Ctdcs.Items.AddRange(new object[266]
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
		this.coBox_Ctdcs.Location = new System.Drawing.Point(401, 102);
		this.coBox_Ctdcs.MaxLength = 5;
		this.coBox_Ctdcs.Name = "coBox_Ctdcs";
		this.coBox_Ctdcs.Size = new System.Drawing.Size(123, 20);
		this.coBox_Ctdcs.TabIndex = 138;
		this.coBox_Ctdcs.Leave += new System.EventHandler(Ctdcs_Leave);
		this.dataGridView_recvfilter.AllowUserToAddRows = false;
		this.dataGridView_recvfilter.AllowUserToDeleteRows = false;
		this.dataGridView_recvfilter.AllowUserToResizeColumns = false;
		this.dataGridView_recvfilter.AllowUserToResizeRows = false;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dataGridView_recvfilter.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
		this.dataGridView_recvfilter.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dataGridView_recvfilter.Columns.AddRange(this.RxCallSign, this.RxCallSignSSID, this.RxCallSignFilter);
		this.dataGridView_recvfilter.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dataGridView_recvfilter.Location = new System.Drawing.Point(329, 221);
		this.dataGridView_recvfilter.MultiSelect = false;
		this.dataGridView_recvfilter.Name = "dataGridView_recvfilter";
		this.dataGridView_recvfilter.RowHeadersWidth = 50;
		this.dataGridView_recvfilter.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.dataGridView_recvfilter.RowTemplate.Height = 23;
		this.dataGridView_recvfilter.Size = new System.Drawing.Size(435, 205);
		this.dataGridView_recvfilter.TabIndex = 137;
		this.dataGridView_recvfilter.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dataGridView_recvfilter_EditingControlShowing);
		this.dataGridView_recvfilter.CurrentCellDirtyStateChanged += new System.EventHandler(dataGridView_recvfilter_CurrentCellDirtyStateChanged);
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
		this.RxCallSign.DefaultCellStyle = dataGridViewCellStyle5;
		this.RxCallSign.FillWeight = 20f;
		this.RxCallSign.HeaderText = "Rx Call Sign";
		this.RxCallSign.MaxInputLength = 6;
		this.RxCallSign.Name = "RxCallSign";
		this.RxCallSign.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.RxCallSign.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.RxCallSign.Width = 180;
		this.RxCallSignSSID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.RxCallSignSSID.DefaultCellStyle = dataGridViewCellStyle6;
		this.RxCallSignSSID.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.RxCallSignSSID.DisplayStyleForCurrentCellOnly = true;
		this.RxCallSignSSID.FillWeight = 80f;
		this.RxCallSignSSID.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.RxCallSignSSID.HeaderText = "SSID";
		this.RxCallSignSSID.Items.AddRange("0", "-1", "-2", "-3", "-4", "-5", "-6", "-7", "-8", "-9", "-10", "-11", "-12", "-13", "-14", "-15");
		this.RxCallSignSSID.Name = "RxCallSignSSID";
		this.RxCallSignSSID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.RxCallSignFilter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.RxCallSignFilter.DefaultCellStyle = dataGridViewCellStyle7;
		this.RxCallSignFilter.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
		this.RxCallSignFilter.DisplayStyleForCurrentCellOnly = true;
		this.RxCallSignFilter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.RxCallSignFilter.HeaderText = "Receive Filter";
		this.RxCallSignFilter.Items.AddRange("off", "on");
		this.RxCallSignFilter.Name = "RxCallSignFilter";
		this.RxCallSignFilter.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.textBox_sendtxt.Location = new System.Drawing.Point(565, 46);
		this.textBox_sendtxt.MaxLength = 60;
		this.textBox_sendtxt.Multiline = true;
		this.textBox_sendtxt.Name = "textBox_sendtxt";
		this.textBox_sendtxt.Size = new System.Drawing.Size(199, 47);
		this.textBox_sendtxt.TabIndex = 136;
		this.textBox_sendtxt.TextChanged += new System.EventHandler(textBox_sendtxt_TextChanged);
		this.textBox_sendtxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_sendtxt_KeyPress);
		this.label36.Location = new System.Drawing.Point(530, 25);
		this.label36.Name = "label36";
		this.label36.Size = new System.Drawing.Size(85, 18);
		this.label36.TabIndex = 135;
		this.label36.Text = "发送文本";
		this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.comboBox_band.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboBox_band.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.comboBox_band.FormattingEnabled = true;
		this.comboBox_band.Items.AddRange(new object[2] { "12.5K", "25K" });
		this.comboBox_band.Location = new System.Drawing.Point(401, 129);
		this.comboBox_band.Name = "comboBox_band";
		this.comboBox_band.Size = new System.Drawing.Size(123, 20);
		this.comboBox_band.TabIndex = 134;
		this.label35.Location = new System.Drawing.Point(310, 132);
		this.label35.Name = "label35";
		this.label35.Size = new System.Drawing.Size(85, 18);
		this.label35.TabIndex = 133;
		this.label35.Text = "带宽";
		this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label33.Location = new System.Drawing.Point(304, 76);
		this.label33.Name = "label33";
		this.label33.Size = new System.Drawing.Size(91, 20);
		this.label33.TabIndex = 128;
		this.label33.Text = "侧音";
		this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.comboBox_beep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboBox_beep.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.comboBox_beep.FormattingEnabled = true;
		this.comboBox_beep.Items.AddRange(new object[2] { "关", "开" });
		this.comboBox_beep.Location = new System.Drawing.Point(401, 76);
		this.comboBox_beep.Name = "comboBox_beep";
		this.comboBox_beep.Size = new System.Drawing.Size(123, 20);
		this.comboBox_beep.TabIndex = 129;
		this.label32.Location = new System.Drawing.Point(310, 105);
		this.label32.Name = "label32";
		this.label32.Size = new System.Drawing.Size(85, 18);
		this.label32.TabIndex = 127;
		this.label32.Text = "CTCSS/DCS";
		this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label31.Location = new System.Drawing.Point(10, 185);
		this.label31.Name = "label31";
		this.label31.Size = new System.Drawing.Size(85, 18);
		this.label31.TabIndex = 125;
		this.label31.Text = "定位图标";
		this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.textBox_posicon.Location = new System.Drawing.Point(99, 182);
		this.textBox_posicon.MaxLength = 1;
		this.textBox_posicon.Name = "textBox_posicon";
		this.textBox_posicon.Size = new System.Drawing.Size(123, 21);
		this.textBox_posicon.TabIndex = 124;
		this.textBox_posicon.KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_posicon_KeyPress);
		this.label30.Location = new System.Drawing.Point(11, 157);
		this.label30.Name = "label30";
		this.label30.Size = new System.Drawing.Size(85, 18);
		this.label30.TabIndex = 123;
		this.label30.Text = "定位表格";
		this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.textBox_postable.Location = new System.Drawing.Point(100, 155);
		this.textBox_postable.MaxLength = 1;
		this.textBox_postable.Name = "textBox_postable";
		this.textBox_postable.Size = new System.Drawing.Size(123, 21);
		this.textBox_postable.TabIndex = 122;
		this.textBox_postable.KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_postable_KeyPress);
		this.comboBox_sendpow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboBox_sendpow.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.comboBox_sendpow.FormattingEnabled = true;
		this.comboBox_sendpow.Items.AddRange(new object[3] { "低", "中", "高" });
		this.comboBox_sendpow.Location = new System.Drawing.Point(101, 19);
		this.comboBox_sendpow.Name = "comboBox_sendpow";
		this.comboBox_sendpow.Size = new System.Drawing.Size(121, 20);
		this.comboBox_sendpow.TabIndex = 121;
		this.label29.Location = new System.Drawing.Point(12, 23);
		this.label29.Name = "label29";
		this.label29.Size = new System.Drawing.Size(83, 12);
		this.label29.TabIndex = 120;
		this.label29.Text = "发送功率";
		this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl1.Location = new System.Drawing.Point(36, 14);
		this.lbl1.Name = "lbl1";
		this.lbl1.Size = new System.Drawing.Size(113, 12);
		this.lbl1.TabIndex = 74;
		this.lbl1.Text = "发送时间间隔[s]";
		this.lbl1.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.lbl2.Location = new System.Drawing.Point(32, 39);
		this.lbl2.Name = "lbl2";
		this.lbl2.Size = new System.Drawing.Size(116, 12);
		this.lbl2.TabIndex = 75;
		this.lbl2.Text = "定时发送时间";
		this.lbl2.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label2.Location = new System.Drawing.Point(40, 63);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(106, 12);
		this.label2.TabIndex = 76;
		this.label2.Text = "固定信标";
		this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label4.Location = new System.Drawing.Point(49, 90);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(97, 12);
		this.label4.TabIndex = 77;
		this.label4.Text = "固定信标高度";
		this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label6.Location = new System.Drawing.Point(14, 116);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(134, 18);
		this.label6.TabIndex = 78;
		this.label6.Text = "APRS显示时间";
		this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label7.Location = new System.Drawing.Point(38, 145);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(106, 12);
		this.label7.TabIndex = 79;
		this.label7.Text = "纬度";
		this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label8.Location = new System.Drawing.Point(12, 196);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(133, 12);
		this.label8.TabIndex = 80;
		this.label8.Text = "经度";
		this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label9.Location = new System.Drawing.Point(38, 169);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(108, 12);
		this.label9.TabIndex = 81;
		this.label9.Text = "南北纬";
		this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label10.Location = new System.Drawing.Point(30, 222);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(116, 12);
		this.label10.TabIndex = 82;
		this.label10.Text = "东西经";
		this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
		this.label11.Location = new System.Drawing.Point(309, 11);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(89, 12);
		this.label11.TabIndex = 83;
		this.label11.Text = "发送频率1[MHz]";
		this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label12.Location = new System.Drawing.Point(309, 37);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(89, 12);
		this.label12.TabIndex = 84;
		this.label12.Text = "发送频率2[MHz]";
		this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label13.Location = new System.Drawing.Point(309, 62);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(89, 12);
		this.label13.TabIndex = 85;
		this.label13.Text = "发送频率3[MHz]";
		this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label14.Location = new System.Drawing.Point(309, 146);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(89, 12);
		this.label14.TabIndex = 88;
		this.label14.Text = "发送频率6[MHz]";
		this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label15.Location = new System.Drawing.Point(309, 119);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(89, 12);
		this.label15.TabIndex = 87;
		this.label15.Text = "发送频率5[MHz]";
		this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label16.Location = new System.Drawing.Point(309, 89);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(89, 12);
		this.label16.TabIndex = 86;
		this.label16.Text = "发送频率4[MHz]";
		this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label18.Location = new System.Drawing.Point(309, 201);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(89, 12);
		this.label18.TabIndex = 90;
		this.label18.Text = "发送频率8[MHz]";
		this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label19.Location = new System.Drawing.Point(309, 173);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(89, 12);
		this.label19.TabIndex = 89;
		this.label19.Text = "发送频率7[MHz]";
		this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label17.Location = new System.Drawing.Point(311, 228);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(84, 12);
		this.label17.TabIndex = 91;
		this.label17.Text = "PASS ALL";
		this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label20.AutoSize = true;
		this.label20.Location = new System.Drawing.Point(778, 131);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(59, 12);
		this.label20.TabIndex = 97;
		this.label20.Text = "WX REPORT";
		this.label21.AutoSize = true;
		this.label21.Location = new System.Drawing.Point(778, 109);
		this.label21.Name = "label21";
		this.label21.Size = new System.Drawing.Size(47, 12);
		this.label21.TabIndex = 96;
		this.label21.Text = "MESSAGE";
		this.label22.AutoSize = true;
		this.label22.Location = new System.Drawing.Point(778, 87);
		this.label22.Name = "label22";
		this.label22.Size = new System.Drawing.Size(29, 12);
		this.label22.TabIndex = 95;
		this.label22.Text = "ITEM";
		this.label23.AutoSize = true;
		this.label23.Location = new System.Drawing.Point(778, 63);
		this.label23.Name = "label23";
		this.label23.Size = new System.Drawing.Size(41, 12);
		this.label23.TabIndex = 94;
		this.label23.Text = "OBJECT";
		this.label24.AutoSize = true;
		this.label24.Location = new System.Drawing.Point(778, 41);
		this.label24.Name = "label24";
		this.label24.Size = new System.Drawing.Size(35, 12);
		this.label24.TabIndex = 93;
		this.label24.Text = "MIC_E";
		this.label25.AutoSize = true;
		this.label25.Location = new System.Drawing.Point(778, 19);
		this.label25.Name = "label25";
		this.label25.Size = new System.Drawing.Size(53, 12);
		this.label25.TabIndex = 92;
		this.label25.Text = "POSITION";
		this.label26.AutoSize = true;
		this.label26.Location = new System.Drawing.Point(778, 195);
		this.label26.Name = "label26";
		this.label26.Size = new System.Drawing.Size(35, 12);
		this.label26.TabIndex = 100;
		this.label26.Text = "OTHER";
		this.label27.AutoSize = true;
		this.label27.Location = new System.Drawing.Point(778, 173);
		this.label27.Name = "label27";
		this.label27.Size = new System.Drawing.Size(83, 12);
		this.label27.TabIndex = 99;
		this.label27.Text = "STATUS REPORT";
		this.label28.AutoSize = true;
		this.label28.Location = new System.Drawing.Point(778, 151);
		this.label28.Name = "label28";
		this.label28.Size = new System.Drawing.Size(71, 12);
		this.label28.TabIndex = 98;
		this.label28.Text = "NMEA REPORT";
		this.checkBox_pos.AutoSize = true;
		this.checkBox_pos.Location = new System.Drawing.Point(871, 16);
		this.checkBox_pos.Name = "checkBox_pos";
		this.checkBox_pos.Size = new System.Drawing.Size(48, 16);
		this.checkBox_pos.TabIndex = 101;
		this.checkBox_pos.Text = "开启";
		this.checkBox_pos.UseVisualStyleBackColor = true;
		this.checkBox_mice.AutoSize = true;
		this.checkBox_mice.Location = new System.Drawing.Point(871, 38);
		this.checkBox_mice.Name = "checkBox_mice";
		this.checkBox_mice.Size = new System.Drawing.Size(48, 16);
		this.checkBox_mice.TabIndex = 102;
		this.checkBox_mice.Text = "开启";
		this.checkBox_mice.UseVisualStyleBackColor = true;
		this.checkBox_item.AutoSize = true;
		this.checkBox_item.Location = new System.Drawing.Point(871, 83);
		this.checkBox_item.Name = "checkBox_item";
		this.checkBox_item.Size = new System.Drawing.Size(48, 16);
		this.checkBox_item.TabIndex = 104;
		this.checkBox_item.Text = "开启";
		this.checkBox_item.UseVisualStyleBackColor = true;
		this.checkBox_obj.AutoSize = true;
		this.checkBox_obj.Location = new System.Drawing.Point(871, 60);
		this.checkBox_obj.Name = "checkBox_obj";
		this.checkBox_obj.Size = new System.Drawing.Size(48, 16);
		this.checkBox_obj.TabIndex = 103;
		this.checkBox_obj.Text = "开启";
		this.checkBox_obj.UseVisualStyleBackColor = true;
		this.checkBox_starep.AutoSize = true;
		this.checkBox_starep.Location = new System.Drawing.Point(871, 170);
		this.checkBox_starep.Name = "checkBox_starep";
		this.checkBox_starep.Size = new System.Drawing.Size(48, 16);
		this.checkBox_starep.TabIndex = 108;
		this.checkBox_starep.Text = "开启";
		this.checkBox_starep.UseVisualStyleBackColor = true;
		this.checkBox_nmearep.AutoSize = true;
		this.checkBox_nmearep.Location = new System.Drawing.Point(871, 148);
		this.checkBox_nmearep.Name = "checkBox_nmearep";
		this.checkBox_nmearep.Size = new System.Drawing.Size(48, 16);
		this.checkBox_nmearep.TabIndex = 107;
		this.checkBox_nmearep.Text = "开启";
		this.checkBox_nmearep.UseVisualStyleBackColor = true;
		this.checkBox_wxrep.AutoSize = true;
		this.checkBox_wxrep.Location = new System.Drawing.Point(871, 127);
		this.checkBox_wxrep.Name = "checkBox_wxrep";
		this.checkBox_wxrep.Size = new System.Drawing.Size(48, 16);
		this.checkBox_wxrep.TabIndex = 106;
		this.checkBox_wxrep.Text = "开启";
		this.checkBox_wxrep.UseVisualStyleBackColor = true;
		this.checkBox_msg.AutoSize = true;
		this.checkBox_msg.Location = new System.Drawing.Point(871, 105);
		this.checkBox_msg.Name = "checkBox_msg";
		this.checkBox_msg.Size = new System.Drawing.Size(48, 16);
		this.checkBox_msg.TabIndex = 105;
		this.checkBox_msg.Text = "开启";
		this.checkBox_msg.UseVisualStyleBackColor = true;
		this.checkBox_other.AutoSize = true;
		this.checkBox_other.Location = new System.Drawing.Point(871, 192);
		this.checkBox_other.Name = "checkBox_other";
		this.checkBox_other.Size = new System.Drawing.Size(48, 16);
		this.checkBox_other.TabIndex = 109;
		this.checkBox_other.Text = "开启";
		this.checkBox_other.UseVisualStyleBackColor = true;
		this.Cbox_SendTimer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.Cbox_SendTimer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.Cbox_SendTimer.FormattingEnabled = true;
		this.Cbox_SendTimer.Items.AddRange(new object[11]
		{
			"关", "30s", "1min", "2min", "3min", "5min", "10min", "15min", "20min", "30min",
			"60min"
		});
		this.Cbox_SendTimer.Location = new System.Drawing.Point(152, 36);
		this.Cbox_SendTimer.Name = "Cbox_SendTimer";
		this.Cbox_SendTimer.Size = new System.Drawing.Size(121, 20);
		this.Cbox_SendTimer.TabIndex = 111;
		this.Cbox_Beacon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.Cbox_Beacon.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.Cbox_Beacon.FormattingEnabled = true;
		this.Cbox_Beacon.Items.AddRange(new object[2] { "关", "开" });
		this.Cbox_Beacon.Location = new System.Drawing.Point(152, 61);
		this.Cbox_Beacon.Name = "Cbox_Beacon";
		this.Cbox_Beacon.Size = new System.Drawing.Size(121, 20);
		this.Cbox_Beacon.TabIndex = 112;
		this.Cbox_BeaconType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.Cbox_BeaconType.FormattingEnabled = true;
		this.Cbox_BeaconType.Items.AddRange(new object[2] { "英尺", "米" });
		this.Cbox_BeaconType.Location = new System.Drawing.Point(152, 88);
		this.Cbox_BeaconType.Name = "Cbox_BeaconType";
		this.Cbox_BeaconType.Size = new System.Drawing.Size(50, 20);
		this.Cbox_BeaconType.TabIndex = 114;
		this.Cbox_BeaconType.SelectedIndexChanged += new System.EventHandler(Cbox_BeaconType_SelectedIndexChanged);
		this.Cbox_AprsDisTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.Cbox_AprsDisTime.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.Cbox_AprsDisTime.FormattingEnabled = true;
		this.Cbox_AprsDisTime.Items.AddRange(new object[14]
		{
			"关", "3", "4", "5", "6", "7", "8", "9", "10", "11",
			"12", "13", "14", "15"
		});
		this.Cbox_AprsDisTime.Location = new System.Drawing.Point(152, 114);
		this.Cbox_AprsDisTime.Name = "Cbox_AprsDisTime";
		this.Cbox_AprsDisTime.Size = new System.Drawing.Size(121, 20);
		this.Cbox_AprsDisTime.TabIndex = 115;
		this.Cbox_Lat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.Cbox_Lat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.Cbox_Lat.FormattingEnabled = true;
		this.Cbox_Lat.Items.AddRange(new object[2] { "南", "北" });
		this.Cbox_Lat.Location = new System.Drawing.Point(151, 167);
		this.Cbox_Lat.Name = "Cbox_Lat";
		this.Cbox_Lat.Size = new System.Drawing.Size(121, 20);
		this.Cbox_Lat.TabIndex = 117;
		this.Cbox_Long.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.Cbox_Long.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.Cbox_Long.FormattingEnabled = true;
		this.Cbox_Long.Items.AddRange(new object[2] { "东", "西" });
		this.Cbox_Long.Location = new System.Drawing.Point(150, 220);
		this.Cbox_Long.Name = "Cbox_Long";
		this.Cbox_Long.Size = new System.Drawing.Size(121, 20);
		this.Cbox_Long.TabIndex = 119;
		this.Cbox_Pass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.Cbox_Pass.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.Cbox_Pass.FormattingEnabled = true;
		this.Cbox_Pass.Items.AddRange(new object[2] { "关", "开" });
		this.Cbox_Pass.Location = new System.Drawing.Point(404, 223);
		this.Cbox_Pass.Name = "Cbox_Pass";
		this.Cbox_Pass.Size = new System.Drawing.Size(121, 20);
		this.Cbox_Pass.TabIndex = 127;
		this.Num_sendtimein.Location = new System.Drawing.Point(151, 9);
		this.Num_sendtimein.Maximum = new decimal(new int[4] { 255, 0, 0, 0 });
		this.Num_sendtimein.Name = "Num_sendtimein";
		this.Num_sendtimein.Size = new System.Drawing.Size(120, 21);
		this.Num_sendtimein.TabIndex = 136;
		this.Num_sendtimein.Value = new decimal(new int[4] { 15, 0, 0, 0 });
		this.Num_BeaconHeight.Location = new System.Drawing.Point(208, 87);
		this.Num_BeaconHeight.Name = "Num_BeaconHeight";
		this.Num_BeaconHeight.Size = new System.Drawing.Size(65, 21);
		this.Num_BeaconHeight.TabIndex = 137;
		this.Num_Lat.DecimalPlaces = 5;
		this.Num_Lat.Location = new System.Drawing.Point(151, 140);
		this.Num_Lat.Maximum = new decimal(new int[4] { 900000, 0, 0, 262144 });
		this.Num_Lat.Name = "Num_Lat";
		this.Num_Lat.Size = new System.Drawing.Size(120, 21);
		this.Num_Lat.TabIndex = 138;
		this.Num_Long.DecimalPlaces = 5;
		this.Num_Long.Location = new System.Drawing.Point(152, 194);
		this.Num_Long.Maximum = new decimal(new int[4] { 1800000, 0, 0, 262144 });
		this.Num_Long.Name = "Num_Long";
		this.Num_Long.Size = new System.Drawing.Size(120, 21);
		this.Num_Long.TabIndex = 139;
		this.TxtBox_SendFreq1.Location = new System.Drawing.Point(404, 5);
		this.TxtBox_SendFreq1.MaxLength = 9;
		this.TxtBox_SendFreq1.Name = "TxtBox_SendFreq1";
		this.TxtBox_SendFreq1.Size = new System.Drawing.Size(123, 21);
		this.TxtBox_SendFreq1.TabIndex = 140;
		this.TxtBox_SendFreq1.Leave += new System.EventHandler(TxtBox_SendFreq1_Leave);
		this.TxtBox_SendFreq1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtBox_SendFreq_KeyPress);
		this.TxtBox_SendFreq2.Location = new System.Drawing.Point(404, 31);
		this.TxtBox_SendFreq2.MaxLength = 9;
		this.TxtBox_SendFreq2.Name = "TxtBox_SendFreq2";
		this.TxtBox_SendFreq2.Size = new System.Drawing.Size(123, 21);
		this.TxtBox_SendFreq2.TabIndex = 141;
		this.TxtBox_SendFreq2.Leave += new System.EventHandler(TxtBox_SendFreq2_Leave);
		this.TxtBox_SendFreq2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtBox_SendFreq_KeyPress);
		this.TxtBox_SendFreq3.Location = new System.Drawing.Point(404, 59);
		this.TxtBox_SendFreq3.MaxLength = 9;
		this.TxtBox_SendFreq3.Name = "TxtBox_SendFreq3";
		this.TxtBox_SendFreq3.Size = new System.Drawing.Size(123, 21);
		this.TxtBox_SendFreq3.TabIndex = 142;
		this.TxtBox_SendFreq3.Leave += new System.EventHandler(TxtBox_SendFreq3_Leave);
		this.TxtBox_SendFreq3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtBox_SendFreq_KeyPress);
		this.TxtBox_SendFreq4.Location = new System.Drawing.Point(404, 84);
		this.TxtBox_SendFreq4.MaxLength = 9;
		this.TxtBox_SendFreq4.Name = "TxtBox_SendFreq4";
		this.TxtBox_SendFreq4.Size = new System.Drawing.Size(123, 21);
		this.TxtBox_SendFreq4.TabIndex = 143;
		this.TxtBox_SendFreq4.Leave += new System.EventHandler(TxtBox_SendFreq4_Leave);
		this.TxtBox_SendFreq4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtBox_SendFreq_KeyPress);
		this.TxtBox_SendFreq8.Location = new System.Drawing.Point(404, 195);
		this.TxtBox_SendFreq8.MaxLength = 9;
		this.TxtBox_SendFreq8.Name = "TxtBox_SendFreq8";
		this.TxtBox_SendFreq8.Size = new System.Drawing.Size(123, 21);
		this.TxtBox_SendFreq8.TabIndex = 147;
		this.TxtBox_SendFreq8.Leave += new System.EventHandler(TxtBox_SendFreq8_Leave);
		this.TxtBox_SendFreq8.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtBox_SendFreq_KeyPress);
		this.TxtBox_SendFreq7.Location = new System.Drawing.Point(404, 170);
		this.TxtBox_SendFreq7.MaxLength = 9;
		this.TxtBox_SendFreq7.Name = "TxtBox_SendFreq7";
		this.TxtBox_SendFreq7.Size = new System.Drawing.Size(123, 21);
		this.TxtBox_SendFreq7.TabIndex = 146;
		this.TxtBox_SendFreq7.Leave += new System.EventHandler(TxtBox_SendFreq7_Leave);
		this.TxtBox_SendFreq7.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtBox_SendFreq_KeyPress);
		this.TxtBox_SendFreq6.Location = new System.Drawing.Point(404, 142);
		this.TxtBox_SendFreq6.MaxLength = 9;
		this.TxtBox_SendFreq6.Name = "TxtBox_SendFreq6";
		this.TxtBox_SendFreq6.Size = new System.Drawing.Size(123, 21);
		this.TxtBox_SendFreq6.TabIndex = 145;
		this.TxtBox_SendFreq6.Leave += new System.EventHandler(TxtBox_SendFreq6_Leave);
		this.TxtBox_SendFreq6.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtBox_SendFreq_KeyPress);
		this.TxtBox_SendFreq5.Location = new System.Drawing.Point(404, 116);
		this.TxtBox_SendFreq5.MaxLength = 9;
		this.TxtBox_SendFreq5.Name = "TxtBox_SendFreq5";
		this.TxtBox_SendFreq5.Size = new System.Drawing.Size(123, 21);
		this.TxtBox_SendFreq5.TabIndex = 144;
		this.TxtBox_SendFreq5.Leave += new System.EventHandler(TxtBox_SendFreq5_Leave);
		this.TxtBox_SendFreq5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TxtBox_SendFreq_KeyPress);
		this.label34.Location = new System.Drawing.Point(544, 11);
		this.label34.Name = "label34";
		this.label34.Size = new System.Drawing.Size(84, 12);
		this.label34.TabIndex = 148;
		this.label34.Text = "PTT ID";
		this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.comboBoxPTTID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboBoxPTTID.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.comboBoxPTTID.FormattingEnabled = true;
		this.comboBoxPTTID.Items.AddRange(new object[3] { "关", "上线码", "下线码" });
		this.comboBoxPTTID.Location = new System.Drawing.Point(637, 7);
		this.comboBoxPTTID.Name = "comboBoxPTTID";
		this.comboBoxPTTID.Size = new System.Drawing.Size(121, 20);
		this.comboBoxPTTID.TabIndex = 149;
		this.comboBoxMiceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.comboBoxMiceType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.comboBoxMiceType.FormattingEnabled = true;
		this.comboBoxMiceType.Items.AddRange(new object[8] { "Emergency", "Priority", "Special", "Committed", "Returning", "In Service", "En Route", "Off Duty" });
		this.comboBoxMiceType.Location = new System.Drawing.Point(637, 34);
		this.comboBoxMiceType.Name = "comboBoxMiceType";
		this.comboBoxMiceType.Size = new System.Drawing.Size(121, 20);
		this.comboBoxMiceType.TabIndex = 151;
		this.label37.Location = new System.Drawing.Point(544, 38);
		this.label37.Name = "label37";
		this.label37.Size = new System.Drawing.Size(84, 12);
		this.label37.TabIndex = 150;
		this.label37.Text = "MIC-E类型";
		this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(970, 719);
		base.Controls.Add(this.comboBoxMiceType);
		base.Controls.Add(this.label37);
		base.Controls.Add(this.comboBoxPTTID);
		base.Controls.Add(this.label34);
		base.Controls.Add(this.TxtBox_SendFreq8);
		base.Controls.Add(this.TxtBox_SendFreq7);
		base.Controls.Add(this.TxtBox_SendFreq6);
		base.Controls.Add(this.TxtBox_SendFreq5);
		base.Controls.Add(this.TxtBox_SendFreq4);
		base.Controls.Add(this.TxtBox_SendFreq3);
		base.Controls.Add(this.TxtBox_SendFreq2);
		base.Controls.Add(this.TxtBox_SendFreq1);
		base.Controls.Add(this.Num_Long);
		base.Controls.Add(this.Num_Lat);
		base.Controls.Add(this.Num_BeaconHeight);
		base.Controls.Add(this.Num_sendtimein);
		base.Controls.Add(this.Cbox_Pass);
		base.Controls.Add(this.Cbox_Long);
		base.Controls.Add(this.Cbox_Lat);
		base.Controls.Add(this.Cbox_AprsDisTime);
		base.Controls.Add(this.Cbox_BeaconType);
		base.Controls.Add(this.Cbox_Beacon);
		base.Controls.Add(this.Cbox_SendTimer);
		base.Controls.Add(this.checkBox_other);
		base.Controls.Add(this.checkBox_starep);
		base.Controls.Add(this.checkBox_nmearep);
		base.Controls.Add(this.checkBox_wxrep);
		base.Controls.Add(this.checkBox_msg);
		base.Controls.Add(this.checkBox_item);
		base.Controls.Add(this.checkBox_obj);
		base.Controls.Add(this.checkBox_mice);
		base.Controls.Add(this.checkBox_pos);
		base.Controls.Add(this.label26);
		base.Controls.Add(this.label27);
		base.Controls.Add(this.label28);
		base.Controls.Add(this.label20);
		base.Controls.Add(this.label21);
		base.Controls.Add(this.label22);
		base.Controls.Add(this.label23);
		base.Controls.Add(this.label24);
		base.Controls.Add(this.label25);
		base.Controls.Add(this.label17);
		base.Controls.Add(this.label18);
		base.Controls.Add(this.label19);
		base.Controls.Add(this.label14);
		base.Controls.Add(this.label15);
		base.Controls.Add(this.label16);
		base.Controls.Add(this.label13);
		base.Controls.Add(this.label12);
		base.Controls.Add(this.label11);
		base.Controls.Add(this.label10);
		base.Controls.Add(this.label9);
		base.Controls.Add(this.label8);
		base.Controls.Add(this.label7);
		base.Controls.Add(this.label6);
		base.Controls.Add(this.label4);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.lbl2);
		base.Controls.Add(this.lbl1);
		base.Controls.Add(this.groupBox1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Frm_Aprs";
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = "APRS";
		base.Load += new System.EventHandler(Frm_Pbook_Load);
		((System.ComponentModel.ISupportInitialize)this.Num_PreTim).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Num_CodeDly).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dgv_Aprs).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView_recvfilter).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Num_sendtimein).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Num_BeaconHeight).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Num_Lat).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Num_Long).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
