using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using CPS_5RH.Data;

namespace CPS_5RH;

public class Frm_VFO : Form
{
	private static int posIdx = 0;

	private static bool initFlg = false;

	private static bool loadFlg = false;

	private DataApp tdata = null;

	private IContainer components = null;

	private ComboBox coBox_Scan;

	private Label lbl3;

	private TextBox tBox_TxFreq;

	private Label lbl10;

	private TextBox tBox_RxFreq;

	private Label lbl9;

	private ComboBox coBox_TxDis;

	private Label lbl15;

	private ComboBox coBox_Power;

	private ComboBox coBox_Emerg;

	private Label lbl7;

	private Label lbl13;

	private ComboBox coBox_IdxDtmf;

	private Label lbl14;

	private Label lbl8;

	private ComboBox coBox_SqlMode;

	private Label lbl19;

	private Label lbl23;

	private Label lbl11;

	private Label lbl21;

	private ComboBox coBox_5tonePtid;

	private ComboBox coBox_QtDec;

	private ComboBox coBox_QtEnc;

	private ComboBox coBox_Jump;

	private GroupBox grp1;

	private Label lbl1;

	private ComboBox coBox_BandWide;

	private Label lbl2;

	private TextBox tBox_Chnname;

	private ComboBox coBox_BusyLock;

	private Label label1;

	private Label label2;

	private ComboBox coBox_DtmfPtid;

	private ComboBox coBox_SelSignal;

	private Label label3;

	private Label label6;

	private ComboBox coBox_Idx2Tone;

	private Label label5;

	private ComboBox coBox_IdxMdc;

	private Label label4;

	private ComboBox coBox_Idx5Tone;

	private ComboBox coBox_Talkaround;

	private Label label8;

	private ComboBox coBox_DaoFreq;

	private Label label7;

	private TextBox tBox_OftFreq;

	private Label label11;

	private Label label10;

	private ComboBox coBox_FreqDir;

	private ComboBox coBox_FreqStep;

	private Label label9;

	public Frm_VFO(DataApp data)
	{
		InitializeComponent();
		tdata = data;
	}

	private void Frm_Chn_Load(object sender, EventArgs e)
	{
		string[] strItems = new string[16];
		string tmp = "";
		if (FormMain.lang == "Chinese")
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
		}
		else
		{
			Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
		}
		LanguageSel.LoadLanguage(this, typeof(Frm_VFO));
		for (int i = 0; i < 16; i++)
		{
			LanguageSel.ChnCoBox.TryGetValue(i.ToString(), out tmp);
			strItems[i] = tmp;
		}
		coBox_SelSignal.Items[0] = strItems[0];
		coBox_FreqDir.Items[0] = strItems[0];
		coBox_Talkaround.Items[0] = strItems[1];
		coBox_Talkaround.Items[1] = strItems[2];
		coBox_DaoFreq.Items[0] = strItems[1];
		coBox_DaoFreq.Items[1] = strItems[2];
		coBox_Jump.Items[0] = strItems[1];
		coBox_Jump.Items[1] = strItems[14];
		coBox_Jump.Items[2] = strItems[15];
		coBox_TxDis.Items[0] = strItems[1];
		coBox_TxDis.Items[1] = strItems[2];
		coBox_DtmfPtid.Items[0] = strItems[1];
		coBox_DtmfPtid.Items[1] = strItems[3];
		coBox_DtmfPtid.Items[2] = strItems[4];
		coBox_DtmfPtid.Items[3] = strItems[5];
		coBox_5tonePtid.Items[0] = strItems[1];
		coBox_5tonePtid.Items[1] = strItems[3];
		coBox_5tonePtid.Items[2] = strItems[4];
		coBox_5tonePtid.Items[3] = strItems[5];
		coBox_Power.Items.Clear();
		if (2 == FormMain.HIGH_VALID)
		{
			coBox_Power.Items.Add(strItems[6]);
			coBox_Power.Items.Add(strItems[7]);
			coBox_Power.Items.Add(strItems[8]);
		}
		else
		{
			coBox_Power.Items.Add(strItems[6]);
			coBox_Power.Items.Add(strItems[8]);
		}
		coBox_BusyLock.Items[0] = strItems[1];
		coBox_BusyLock.Items[1] = strItems[2];
		coBox_SqlMode.Items[0] = strItems[0];
		coBox_SqlMode.Items[2] = strItems[11];
		coBox_SqlMode.Items[3] = strItems[13];
		coBox_Scan.Items[0] = strItems[0];
		coBox_Emerg.Items[0] = strItems[0];
		loadFlg = false;
		tBox_RxFreq.LostFocus -= tBox_RxFreq_LostFocus;
		tBox_RxFreq.LostFocus += tBox_RxFreq_LostFocus;
		tBox_TxFreq.LostFocus -= tBox_TxFreq_Leave;
		tBox_TxFreq.LostFocus += tBox_TxFreq_Leave;
		tBox_Chnname.LostFocus += tBox_Chnname_LostFocus;
		coBox_QtDec.LostFocus += coBox_QtDec_Leave;
		coBox_QtEnc.LostFocus += coBox_QtEnc_Leave;
		tBox_OftFreq.LostFocus -= tBox_OftFreq_LostFocus;
		tBox_OftFreq.LostFocus += tBox_OftFreq_LostFocus;
		loadFlg = true;
	}

	private void Frm_Chn_Activated(object sender, EventArgs e)
	{
		VFODataDisp(posIdx, Text);
	}

	private void bingDingTheControls(int pos)
	{
		coBox_FreqDir.DataBindings.Clear();
		coBox_FreqStep.DataBindings.Clear();
		coBox_BandWide.DataBindings.Clear();
		coBox_Power.DataBindings.Clear();
		coBox_SqlMode.DataBindings.Clear();
		coBox_BusyLock.DataBindings.Clear();
		coBox_IdxDtmf.DataBindings.Clear();
		coBox_Idx2Tone.DataBindings.Clear();
		coBox_Idx5Tone.DataBindings.Clear();
		coBox_IdxMdc.DataBindings.Clear();
		coBox_DtmfPtid.DataBindings.Clear();
		coBox_5tonePtid.DataBindings.Clear();
		coBox_Talkaround.DataBindings.Clear();
		coBox_Scan.DataBindings.Clear();
		coBox_Emerg.DataBindings.Clear();
		coBox_TxDis.DataBindings.Clear();
		coBox_Jump.DataBindings.Clear();
		coBox_DaoFreq.DataBindings.Clear();
		if (tdata.dataVFOInfor[pos].Wideth >= 2 || tdata.dataVFOInfor[pos].Wideth < 0)
		{
			tdata.dataVFOInfor[pos].Wideth = 0;
		}
		if (tdata.dataVFOInfor[pos].Offsetdir > 2)
		{
			tdata.dataVFOInfor[pos].Offsetdir = 0;
		}
		if (tdata.dataVFOInfor[pos].FreqStep > 10)
		{
			tdata.dataVFOInfor[pos].FreqStep = 0;
		}
		if (tdata.dataVFOInfor[pos].SignalType > 6)
		{
			tdata.dataVFOInfor[pos].SignalType = 0;
		}
		if (tdata.dataVFOInfor[pos].BusyLock > 2)
		{
			tdata.dataVFOInfor[pos].BusyLock = 0;
		}
		if (tdata.dataVFOInfor[pos].SqType > 3)
		{
			tdata.dataVFOInfor[pos].SqType = 0;
		}
		if (tdata.dataVFOInfor[pos].MdcIdx > 5)
		{
			tdata.dataVFOInfor[pos].MdcIdx = 0;
		}
		if (tdata.dataVFOInfor[pos].TxDis > 1)
		{
			tdata.dataVFOInfor[pos].TxDis = 0;
		}
		if (tdata.dataVFOInfor[pos].TalkAround > 1)
		{
			tdata.dataVFOInfor[pos].TalkAround = 0;
		}
		if (tdata.dataVFOInfor[pos].JumpFreq > 2)
		{
			tdata.dataVFOInfor[pos].JumpFreq = 0;
		}
		if (tdata.dataVFOInfor[pos].FreqInvert > 1)
		{
			tdata.dataVFOInfor[pos].FreqInvert = 0;
		}
		if (tdata.dataVFOInfor[pos].Power > FormMain.HIGH_VALID)
		{
			tdata.dataVFOInfor[pos].Power = 0;
		}
		if (tdata.dataVFOInfor[pos].DtmfIdx > 15)
		{
			tdata.dataVFOInfor[pos].DtmfIdx = 0;
		}
		if (tdata.dataVFOInfor[pos].TwotoneIdx > 15)
		{
			tdata.dataVFOInfor[pos].TwotoneIdx = 0;
		}
		if (tdata.dataVFOInfor[pos].FivetoneIdx > 99)
		{
			tdata.dataVFOInfor[pos].FivetoneIdx = 0;
		}
		if (tdata.dataVFOInfor[pos].Scanlist > 10)
		{
			tdata.dataVFOInfor[pos].Scanlist = 0;
		}
		if (tdata.dataVFOInfor[pos].Emerglist > 10)
		{
			tdata.dataVFOInfor[pos].Emerglist = 0;
		}
		coBox_FreqDir.DataBindings.Add("SelectedIndex", tdata.dataVFOInfor[pos], "Offsetdir", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_FreqStep.DataBindings.Add("SelectedIndex", tdata.dataVFOInfor[pos], "FreqStep", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_BandWide.DataBindings.Add("SelectedIndex", tdata.dataVFOInfor[pos], "Wideth", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_Power.DataBindings.Add("SelectedIndex", tdata.dataVFOInfor[pos], "Power", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_SqlMode.DataBindings.Add("SelectedIndex", tdata.dataVFOInfor[pos], "SqType", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_BusyLock.DataBindings.Add("SelectedIndex", tdata.dataVFOInfor[pos], "BusyLock", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_IdxDtmf.DataBindings.Add("SelectedIndex", tdata.dataVFOInfor[pos], "DtmfIdx", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_Idx2Tone.DataBindings.Add("SelectedIndex", tdata.dataVFOInfor[pos], "TwotoneIdx", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_Idx5Tone.DataBindings.Add("SelectedIndex", tdata.dataVFOInfor[pos], "FivetoneIdx", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_IdxMdc.DataBindings.Add("SelectedIndex", tdata.dataVFOInfor[pos], "MdcIdx", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_DtmfPtid.DataBindings.Add("SelectedIndex", tdata.dataVFOInfor[pos], "DtmfPtt", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_5tonePtid.DataBindings.Add("SelectedIndex", tdata.dataVFOInfor[pos], "FivetonePtt", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_Talkaround.DataBindings.Add("SelectedIndex", tdata.dataVFOInfor[pos], "TalkAround", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_Scan.DataBindings.Add("SelectedIndex", tdata.dataVFOInfor[pos], "Scanlist", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_Emerg.DataBindings.Add("SelectedIndex", tdata.dataVFOInfor[pos], "Emerglist", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_TxDis.DataBindings.Add("SelectedIndex", tdata.dataVFOInfor[pos], "TxDis", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_Jump.DataBindings.Add("SelectedIndex", tdata.dataVFOInfor[pos], "JumpFreq", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
		coBox_DaoFreq.DataBindings.Add("SelectedIndex", tdata.dataVFOInfor[pos], "FreqInvert", formattingEnabled: false, DataSourceUpdateMode.OnPropertyChanged);
	}

	private void tBox_RxFreq_LostFocus(object sender, EventArgs e)
	{
		string value = ((TextBox)sender).Text;
		double doufreq = tdata.dataDevice.Adjust_Freq(value);
		tBox_RxFreq.Text = doufreq.ToString("0.00000");
		tdata.dataVFOInfor[posIdx].RxFreq = tBox_RxFreq.Text;
		tBox_TxFreq.Text = tBox_RxFreq.Text;
		tdata.dataVFOInfor[posIdx].TxFreq = tBox_TxFreq.Text;
		coBox_FreqDir.SelectedIndex = 0;
		tdata.dataVFOInfor[posIdx].DivFreq = 0.0.ToString("0.00000");
		tBox_OftFreq.Text = tdata.dataVFOInfor[posIdx].DivFreq;
	}

	private void tBox_Chnname_LostFocus(object sender, EventArgs e)
	{
		if (tBox_Chnname.Text == "")
		{
			tBox_Chnname.Text = tdata.dataVFOInfor[posIdx].ChnName;
		}
		else
		{
			tdata.dataVFOInfor[posIdx].ChnName = tBox_Chnname.Text;
		}
	}

	private void DivFreqDisp(int pos)
	{
		double doubletxfreq = Convert.ToDouble(tdata.dataVFOInfor[pos].TxFreq);
		double doublerxfreq = Convert.ToDouble(tdata.dataVFOInfor[pos].RxFreq);
		double doufreq;
		if (doubletxfreq < doublerxfreq)
		{
			tdata.dataVFOInfor[pos].Offsetdir = 2;
			doufreq = doublerxfreq - doubletxfreq;
		}
		else if (doubletxfreq > doublerxfreq)
		{
			tdata.dataVFOInfor[pos].Offsetdir = 1;
			doufreq = doubletxfreq - doublerxfreq;
		}
		else
		{
			tdata.dataVFOInfor[pos].Offsetdir = 0;
			coBox_FreqDir.SelectedIndex = 0;
			doufreq = 0.0;
		}
		tdata.dataVFOInfor[pos].DivFreq = doufreq.ToString("0.00000");
		tBox_OftFreq.Text = tdata.dataVFOInfor[pos].DivFreq;
		coBox_FreqDir.Text = coBox_FreqDir.Items[tdata.dataVFOInfor[pos].Offsetdir].ToString();
	}

	private void tBox_TxFreq_Leave(object sender, EventArgs e)
	{
		string value = ((TextBox)sender).Text;
		double doufreq = tdata.dataDevice.Adjust_Freq(value);
		tdata.dataVFOInfor[posIdx].TxFreq = doufreq.ToString("0.00000");
		tBox_TxFreq.Text = tdata.dataVFOInfor[posIdx].TxFreq;
		if (tBox_TxFreq.Text != tBox_RxFreq.Text)
		{
		}
		DivFreqDisp(posIdx);
	}

	private void coBox_QtDec_Leave(object sender, EventArgs e)
	{
		string strValue = coBox_QtDec.Text;
		if (!loadFlg || !initFlg)
		{
			return;
		}
		if (strValue == "")
		{
			coBox_QtDec.Text = tdata.dataVFOInfor[posIdx].RxCtsDcs;
			return;
		}
		strValue = strValue.ToUpper();
		switch (coBox_QtDec.Items.IndexOf(strValue))
		{
		case -1:
		{
			double buf = 0.0;
			int len = strValue.Length;
			if (strValue[0] == 'D' && len >= 5 && (strValue[4] == 'I' || strValue[4] == 'N'))
			{
				if (strValue[1] >= '0' && strValue[1] <= '7' && strValue[2] >= '0' && strValue[2] <= '7' && strValue[3] >= '0' && strValue[3] <= '7' && (strValue[4] == 'I' || strValue[4] == 'N'))
				{
					tdata.dataVFOInfor[posIdx].RxCtsDcs = strValue;
					coBox_QtDec.Text = tdata.dataVFOInfor[posIdx].RxCtsDcs;
					if (tdata.dataVFOInfor[posIdx].SignalType == 0)
					{
						coBox_SqlMode.SelectedIndex = 1;
					}
					else
					{
						coBox_SqlMode.SelectedIndex = 3;
					}
				}
				else
				{
					coBox_QtDec.Text = tdata.dataVFOInfor[posIdx].RxCtsDcs;
				}
				return;
			}
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
					tdata.dataVFOInfor[posIdx].RxCtsDcs = str;
					coBox_QtDec.Text = tdata.dataVFOInfor[posIdx].RxCtsDcs;
					if (tdata.dataVFOInfor[posIdx].SignalType == 0)
					{
						coBox_SqlMode.SelectedIndex = 1;
					}
					else
					{
						coBox_SqlMode.SelectedIndex = 3;
					}
				}
				else
				{
					coBox_QtDec.Text = tdata.dataVFOInfor[posIdx].RxCtsDcs;
				}
				return;
			}
			catch
			{
				coBox_QtDec.Text = tdata.dataVFOInfor[posIdx].RxCtsDcs;
				return;
			}
		}
		case 0:
			if (tdata.dataVFOInfor[posIdx].SignalType == 0)
			{
				coBox_SqlMode.SelectedIndex = 0;
			}
			else
			{
				coBox_SqlMode.SelectedIndex = 2;
			}
			break;
		default:
			if (tdata.dataVFOInfor[posIdx].SignalType == 0)
			{
				coBox_SqlMode.SelectedIndex = 1;
			}
			else
			{
				coBox_SqlMode.SelectedIndex = 3;
			}
			break;
		}
		tdata.dataVFOInfor[posIdx].RxCtsDcs = strValue;
	}

	private void coBox_QtEnc_Leave(object sender, EventArgs e)
	{
		string strValue = coBox_QtEnc.Text;
		if (strValue == "")
		{
			coBox_QtEnc.Text = tdata.dataVFOInfor[posIdx].TxCtsDcs;
			return;
		}
		strValue = strValue.ToUpper();
		if (coBox_QtEnc.Items.IndexOf(strValue) == -1)
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
						tdata.dataVFOInfor[posIdx].TxCtsDcs = str;
						coBox_QtEnc.Text = tdata.dataVFOInfor[posIdx].TxCtsDcs;
					}
					else
					{
						coBox_QtEnc.Text = tdata.dataVFOInfor[posIdx].TxCtsDcs;
					}
					return;
				}
				catch
				{
					coBox_QtEnc.Text = tdata.dataVFOInfor[posIdx].TxCtsDcs;
					return;
				}
			}
			if (strValue[1] >= '0' && strValue[1] <= '7' && strValue[2] >= '0' && strValue[2] <= '7' && strValue[3] >= '0' && strValue[3] <= '7' && (strValue[4] == 'I' || strValue[4] == 'N'))
			{
				tdata.dataVFOInfor[posIdx].TxCtsDcs = strValue;
				coBox_QtEnc.Text = tdata.dataVFOInfor[posIdx].TxCtsDcs;
			}
			else
			{
				coBox_QtEnc.Text = tdata.dataVFOInfor[posIdx].TxCtsDcs;
			}
		}
		else
		{
			tdata.dataVFOInfor[posIdx].TxCtsDcs = strValue;
		}
	}

	private bool JubgeFreqValid(string txFreq, string matchFreq)
	{
		bool valid = false;
		int mFreq = Convert.ToInt32(Regex.Replace(txFreq, "[^0-9]+", ""));
		int tTfreq = ((mFreq > tdata.dataDevice.GetTxEndFreq(0)) ? ((mFreq <= tdata.dataDevice.GetTxEndFreq(1)) ? 1 : ((mFreq > tdata.dataDevice.GetTxEndFreq(2)) ? 3 : 2)) : 0);
		mFreq = Convert.ToInt32(Regex.Replace(matchFreq, "[^0-9]+", ""));
		switch (tTfreq)
		{
		case 0:
			if (mFreq >= tdata.dataDevice.GetTxStartFreq(0) && mFreq <= tdata.dataDevice.GetTxEndFreq(0))
			{
				valid = true;
			}
			break;
		case 1:
			if (mFreq >= tdata.dataDevice.GetTxStartFreq(1) && mFreq <= tdata.dataDevice.GetTxEndFreq(1))
			{
				valid = true;
			}
			break;
		case 2:
			if (mFreq >= tdata.dataDevice.GetTxStartFreq(2) && mFreq <= tdata.dataDevice.GetTxEndFreq(2))
			{
				valid = true;
			}
			break;
		default:
			if (mFreq >= tdata.dataDevice.GetTxStartFreq(3) && mFreq <= tdata.dataDevice.GetTxEndFreq(3))
			{
				valid = true;
			}
			break;
		}
		return valid;
	}

	private void tBox_OftFreq_LostFocus(object sender, EventArgs e)
	{
		double doufreq = 0.0;
		bool valid = false;
		string value = ((TextBox)sender).Text;
		double doubletxfreq = Convert.ToDouble(tdata.dataVFOInfor[posIdx].TxFreq);
		double doublerxfreq = Convert.ToDouble(tdata.dataVFOInfor[posIdx].RxFreq);
		if (tdata.dataVFOInfor[posIdx].Offsetdir == 0)
		{
			doufreq = 0.0;
		}
		else if (tdata.dataVFOInfor[posIdx].Offsetdir == 1)
		{
			try
			{
				doufreq = Convert.ToDouble(value);
				double tmp = doufreq + doublerxfreq;
				if (JubgeFreqValid(tdata.dataVFOInfor[posIdx].TxFreq, tmp.ToString("0.00000")))
				{
					tdata.dataVFOInfor[posIdx].TxFreq = tmp.ToString("0.00000");
					tBox_TxFreq.Text = tdata.dataVFOInfor[posIdx].TxFreq;
				}
				else
				{
					doufreq = doubletxfreq - doublerxfreq;
				}
			}
			catch
			{
				doufreq = 0.0;
			}
		}
		else if (tdata.dataVFOInfor[posIdx].Offsetdir == 2)
		{
			try
			{
				doufreq = Convert.ToDouble(value);
				double tmp = doublerxfreq - doufreq;
				if (JubgeFreqValid(tdata.dataVFOInfor[posIdx].TxFreq, tmp.ToString("0.00000")))
				{
					tdata.dataVFOInfor[posIdx].TxFreq = tmp.ToString("0.00000");
					tBox_TxFreq.Text = tdata.dataVFOInfor[posIdx].TxFreq;
				}
				else
				{
					doufreq = doublerxfreq - doubletxfreq;
				}
			}
			catch
			{
				doufreq = 0.0;
			}
		}
		tdata.dataVFOInfor[posIdx].DivFreq = doufreq.ToString("0.00000");
		tBox_OftFreq.Text = tdata.dataVFOInfor[posIdx].DivFreq;
	}

	private void EditFreq_KeyPress(object sender, KeyPressEventArgs e)
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

	private void coBox_SelSignal_SelectedIndexChanged(object sender, EventArgs e)
	{
		tdata.dataVFOInfor[posIdx].SignalType = (byte)coBox_SelSignal.SelectedIndex;
		switch (coBox_SelSignal.SelectedIndex)
		{
		case 1:
			coBox_IdxDtmf.Enabled = true;
			coBox_Idx2Tone.Enabled = false;
			coBox_Idx5Tone.Enabled = false;
			coBox_IdxMdc.Enabled = false;
			return;
		case 2:
			coBox_IdxDtmf.Enabled = false;
			coBox_Idx2Tone.Enabled = true;
			coBox_Idx5Tone.Enabled = false;
			coBox_IdxMdc.Enabled = false;
			return;
		case 3:
			coBox_IdxDtmf.Enabled = false;
			coBox_Idx2Tone.Enabled = false;
			coBox_Idx5Tone.Enabled = true;
			coBox_IdxMdc.Enabled = false;
			return;
		case 4:
			coBox_IdxDtmf.Enabled = false;
			coBox_Idx2Tone.Enabled = false;
			coBox_Idx5Tone.Enabled = false;
			coBox_IdxMdc.Enabled = true;
			return;
		case 5:
			coBox_IdxDtmf.Enabled = false;
			coBox_Idx2Tone.Enabled = false;
			coBox_Idx5Tone.Enabled = false;
			coBox_IdxMdc.Enabled = false;
			return;
		}
		coBox_IdxDtmf.Enabled = false;
		coBox_Idx2Tone.Enabled = false;
		coBox_Idx5Tone.Enabled = false;
		coBox_IdxMdc.Enabled = false;
		if (tdata.dataVFOInfor[posIdx].SqType == 2)
		{
			coBox_SqlMode.SelectedIndex = 0;
		}
		else if (tdata.dataVFOInfor[posIdx].SqType == 3)
		{
			coBox_SqlMode.SelectedIndex = 1;
		}
	}

	public static void SetNodePos(int pos)
	{
		posIdx = pos;
	}

	public static Frm_VFO getInstance(FormMain father, DataApp tdata)
	{
		Frm_VFO form = new Frm_VFO(tdata);
		form.MdiParent = father;
		return form;
	}

	public void VFODataDisp(int pos, string nodename)
	{
		Text = nodename;
		grp1.Text = nodename;
		initFlg = false;
		bingDingTheControls(pos);
		coBox_SelSignal.SelectedIndex = tdata.dataVFOInfor[pos].SignalType;
		tBox_Chnname.Text = tdata.dataVFOInfor[pos].ChnName;
		tBox_RxFreq.Text = tdata.dataVFOInfor[pos].RxFreq;
		tBox_TxFreq.Text = tdata.dataVFOInfor[pos].TxFreq;
		coBox_QtDec.Text = tdata.dataVFOInfor[pos].RxCtsDcs;
		coBox_QtEnc.Text = tdata.dataVFOInfor[pos].TxCtsDcs;
		tBox_OftFreq.Text = tdata.dataVFOInfor[pos].DivFreq;
		initFlg = true;
	}

	private void coBox_Talkaround_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (coBox_Talkaround.SelectedIndex == 1)
		{
			coBox_DaoFreq.Enabled = false;
		}
		else
		{
			coBox_DaoFreq.Enabled = true;
		}
	}

	private void coBox_DaoFreq_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (coBox_DaoFreq.SelectedIndex == 1)
		{
			coBox_Talkaround.Enabled = false;
		}
		else
		{
			coBox_Talkaround.Enabled = true;
		}
	}

	private void coBox_FreqDir_SelectedIndexChanged(object sender, EventArgs e)
	{
		bool valid = false;
		if (!initFlg)
		{
			return;
		}
		double doubletxfreq = Convert.ToDouble(tdata.dataVFOInfor[posIdx].TxFreq);
		double doublerxfreq = Convert.ToDouble(tdata.dataVFOInfor[posIdx].RxFreq);
		double doufreq = Convert.ToDouble(tdata.dataVFOInfor[posIdx].DivFreq);
		if (coBox_FreqDir.SelectedIndex == 0)
		{
			if (doubletxfreq != doublerxfreq)
			{
				tBox_TxFreq.Text = tBox_RxFreq.Text;
				tdata.dataVFOInfor[posIdx].TxFreq = tBox_TxFreq.Text;
				tdata.dataVFOInfor[posIdx].DivFreq = 0.0.ToString("0.00000");
				tBox_OftFreq.Text = tdata.dataVFOInfor[posIdx].DivFreq;
			}
			return;
		}
		doubletxfreq = ((coBox_FreqDir.SelectedIndex != 1) ? (doublerxfreq - doufreq) : (doublerxfreq + doufreq));
		if (JubgeFreqValid(tdata.dataVFOInfor[posIdx].TxFreq, doubletxfreq.ToString("0.00000")))
		{
			tdata.dataVFOInfor[posIdx].TxFreq = doubletxfreq.ToString("0.00000");
			tBox_TxFreq.Text = tdata.dataVFOInfor[posIdx].TxFreq;
			return;
		}
		tBox_TxFreq.Text = tBox_RxFreq.Text;
		tdata.dataVFOInfor[posIdx].TxFreq = tBox_TxFreq.Text;
		tdata.dataVFOInfor[posIdx].DivFreq = 0.0.ToString("0.00000");
		tBox_OftFreq.Text = tdata.dataVFOInfor[posIdx].DivFreq;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CPS_5RH.Frm_VFO));
		this.coBox_5tonePtid = new System.Windows.Forms.ComboBox();
		this.lbl9 = new System.Windows.Forms.Label();
		this.tBox_RxFreq = new System.Windows.Forms.TextBox();
		this.coBox_QtEnc = new System.Windows.Forms.ComboBox();
		this.coBox_QtDec = new System.Windows.Forms.ComboBox();
		this.lbl23 = new System.Windows.Forms.Label();
		this.lbl21 = new System.Windows.Forms.Label();
		this.lbl2 = new System.Windows.Forms.Label();
		this.coBox_BandWide = new System.Windows.Forms.ComboBox();
		this.coBox_Jump = new System.Windows.Forms.ComboBox();
		this.lbl11 = new System.Windows.Forms.Label();
		this.lbl10 = new System.Windows.Forms.Label();
		this.lbl3 = new System.Windows.Forms.Label();
		this.tBox_TxFreq = new System.Windows.Forms.TextBox();
		this.lbl7 = new System.Windows.Forms.Label();
		this.coBox_BusyLock = new System.Windows.Forms.ComboBox();
		this.label1 = new System.Windows.Forms.Label();
		this.coBox_SqlMode = new System.Windows.Forms.ComboBox();
		this.lbl15 = new System.Windows.Forms.Label();
		this.coBox_TxDis = new System.Windows.Forms.ComboBox();
		this.lbl19 = new System.Windows.Forms.Label();
		this.lbl14 = new System.Windows.Forms.Label();
		this.coBox_IdxDtmf = new System.Windows.Forms.ComboBox();
		this.lbl13 = new System.Windows.Forms.Label();
		this.coBox_Emerg = new System.Windows.Forms.ComboBox();
		this.coBox_Scan = new System.Windows.Forms.ComboBox();
		this.lbl8 = new System.Windows.Forms.Label();
		this.coBox_Power = new System.Windows.Forms.ComboBox();
		this.grp1 = new System.Windows.Forms.GroupBox();
		this.tBox_OftFreq = new System.Windows.Forms.TextBox();
		this.coBox_FreqDir = new System.Windows.Forms.ComboBox();
		this.label10 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.coBox_FreqStep = new System.Windows.Forms.ComboBox();
		this.label9 = new System.Windows.Forms.Label();
		this.coBox_Talkaround = new System.Windows.Forms.ComboBox();
		this.label8 = new System.Windows.Forms.Label();
		this.coBox_DaoFreq = new System.Windows.Forms.ComboBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.coBox_Idx2Tone = new System.Windows.Forms.ComboBox();
		this.label5 = new System.Windows.Forms.Label();
		this.coBox_IdxMdc = new System.Windows.Forms.ComboBox();
		this.label4 = new System.Windows.Forms.Label();
		this.coBox_Idx5Tone = new System.Windows.Forms.ComboBox();
		this.coBox_SelSignal = new System.Windows.Forms.ComboBox();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.coBox_DtmfPtid = new System.Windows.Forms.ComboBox();
		this.tBox_Chnname = new System.Windows.Forms.TextBox();
		this.lbl1 = new System.Windows.Forms.Label();
		this.grp1.SuspendLayout();
		base.SuspendLayout();
		this.coBox_5tonePtid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_5tonePtid.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_5tonePtid.FormattingEnabled = true;
		this.coBox_5tonePtid.Items.AddRange(new object[4] { "关", "上线码", "下线码", "两者" });
		this.coBox_5tonePtid.Location = new System.Drawing.Point(147, 350);
		this.coBox_5tonePtid.Name = "coBox_5tonePtid";
		this.coBox_5tonePtid.Size = new System.Drawing.Size(85, 20);
		this.coBox_5tonePtid.TabIndex = 121;
		this.lbl9.Location = new System.Drawing.Point(34, 20);
		this.lbl9.Name = "lbl9";
		this.lbl9.Size = new System.Drawing.Size(111, 20);
		this.lbl9.TabIndex = 14;
		this.lbl9.Text = "接收频率(MHz)";
		this.lbl9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.tBox_RxFreq.Location = new System.Drawing.Point(147, 20);
		this.tBox_RxFreq.MaxLength = 9;
		this.tBox_RxFreq.Name = "tBox_RxFreq";
		this.tBox_RxFreq.Size = new System.Drawing.Size(100, 21);
		this.tBox_RxFreq.TabIndex = 153;
		this.tBox_RxFreq.KeyPress += new System.Windows.Forms.KeyPressEventHandler(EditFreq_KeyPress);
		this.coBox_QtEnc.DropDownHeight = 80;
		this.coBox_QtEnc.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_QtEnc.FormattingEnabled = true;
		this.coBox_QtEnc.IntegralHeight = false;
		this.coBox_QtEnc.Items.AddRange(new object[266]
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
		this.coBox_QtEnc.Location = new System.Drawing.Point(429, 110);
		this.coBox_QtEnc.MaxLength = 5;
		this.coBox_QtEnc.Name = "coBox_QtEnc";
		this.coBox_QtEnc.Size = new System.Drawing.Size(85, 20);
		this.coBox_QtEnc.TabIndex = 120;
		this.coBox_QtDec.DropDownHeight = 80;
		this.coBox_QtDec.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_QtDec.FormattingEnabled = true;
		this.coBox_QtDec.IntegralHeight = false;
		this.coBox_QtDec.Items.AddRange(new object[266]
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
		this.coBox_QtDec.Location = new System.Drawing.Point(148, 110);
		this.coBox_QtDec.MaxLength = 5;
		this.coBox_QtDec.Name = "coBox_QtDec";
		this.coBox_QtDec.Size = new System.Drawing.Size(84, 20);
		this.coBox_QtDec.TabIndex = 114;
		this.lbl23.Location = new System.Drawing.Point(282, 110);
		this.lbl23.Name = "lbl23";
		this.lbl23.Size = new System.Drawing.Size(142, 20);
		this.lbl23.TabIndex = 109;
		this.lbl23.Text = "CTCSS/DCS编码";
		this.lbl23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl21.Location = new System.Drawing.Point(30, 110);
		this.lbl21.Name = "lbl21";
		this.lbl21.Size = new System.Drawing.Size(106, 20);
		this.lbl21.TabIndex = 106;
		this.lbl21.Text = "CTCSS/DCS解码";
		this.lbl21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl2.Location = new System.Drawing.Point(41, 140);
		this.lbl2.Name = "lbl2";
		this.lbl2.Size = new System.Drawing.Size(100, 20);
		this.lbl2.TabIndex = 123;
		this.lbl2.Text = "信道带宽";
		this.lbl2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_BandWide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_BandWide.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_BandWide.FormattingEnabled = true;
		this.coBox_BandWide.Items.AddRange(new object[2] { "12.5kHz", "25kHz" });
		this.coBox_BandWide.Location = new System.Drawing.Point(148, 140);
		this.coBox_BandWide.Name = "coBox_BandWide";
		this.coBox_BandWide.Size = new System.Drawing.Size(85, 20);
		this.coBox_BandWide.TabIndex = 124;
		this.coBox_Jump.DropDownHeight = 90;
		this.coBox_Jump.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Jump.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Jump.FormattingEnabled = true;
		this.coBox_Jump.IntegralHeight = false;
		this.coBox_Jump.Items.AddRange(new object[3] { "关闭", "普通", "严格" });
		this.coBox_Jump.Location = new System.Drawing.Point(429, 288);
		this.coBox_Jump.Name = "coBox_Jump";
		this.coBox_Jump.Size = new System.Drawing.Size(85, 20);
		this.coBox_Jump.TabIndex = 118;
		this.lbl11.Location = new System.Drawing.Point(323, 288);
		this.lbl11.Name = "lbl11";
		this.lbl11.Size = new System.Drawing.Size(103, 20);
		this.lbl11.TabIndex = 97;
		this.lbl11.Text = "跳频";
		this.lbl11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl10.Location = new System.Drawing.Point(316, 20);
		this.lbl10.Name = "lbl10";
		this.lbl10.Size = new System.Drawing.Size(108, 20);
		this.lbl10.TabIndex = 16;
		this.lbl10.Text = "发射频率(MHz)";
		this.lbl10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl3.Location = new System.Drawing.Point(334, 380);
		this.lbl3.Name = "lbl3";
		this.lbl3.Size = new System.Drawing.Size(92, 20);
		this.lbl3.TabIndex = 0;
		this.lbl3.Text = "扫描列表";
		this.lbl3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl3.Visible = false;
		this.tBox_TxFreq.Location = new System.Drawing.Point(427, 20);
		this.tBox_TxFreq.MaxLength = 9;
		this.tBox_TxFreq.Name = "tBox_TxFreq";
		this.tBox_TxFreq.Size = new System.Drawing.Size(100, 21);
		this.tBox_TxFreq.TabIndex = 152;
		this.tBox_TxFreq.KeyPress += new System.Windows.Forms.KeyPressEventHandler(EditFreq_KeyPress);
		this.lbl7.Location = new System.Drawing.Point(327, 140);
		this.lbl7.Name = "lbl7";
		this.lbl7.Size = new System.Drawing.Size(99, 20);
		this.lbl7.TabIndex = 32;
		this.lbl7.Text = "功率级别";
		this.lbl7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_BusyLock.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
		this.coBox_BusyLock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_BusyLock.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_BusyLock.FormattingEnabled = true;
		this.coBox_BusyLock.Items.AddRange(new object[2] { "无", "中继" });
		this.coBox_BusyLock.Location = new System.Drawing.Point(429, 170);
		this.coBox_BusyLock.Name = "coBox_BusyLock";
		this.coBox_BusyLock.Size = new System.Drawing.Size(85, 20);
		this.coBox_BusyLock.TabIndex = 119;
		this.coBox_BusyLock.Visible = false;
		this.label1.Location = new System.Drawing.Point(302, 170);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(120, 20);
		this.label1.TabIndex = 118;
		this.label1.Text = "繁忙锁定";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label1.Visible = false;
		this.coBox_SqlMode.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
		this.coBox_SqlMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_SqlMode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_SqlMode.FormattingEnabled = true;
		this.coBox_SqlMode.Items.AddRange(new object[4] { "无", "CTDCS", "可选信令", "CTDCS+可选信令" });
		this.coBox_SqlMode.Location = new System.Drawing.Point(429, 200);
		this.coBox_SqlMode.Name = "coBox_SqlMode";
		this.coBox_SqlMode.Size = new System.Drawing.Size(137, 20);
		this.coBox_SqlMode.TabIndex = 50;
		this.lbl15.Location = new System.Drawing.Point(306, 258);
		this.lbl15.Name = "lbl15";
		this.lbl15.Size = new System.Drawing.Size(120, 20);
		this.lbl15.TabIndex = 21;
		this.lbl15.Text = "发射禁止";
		this.lbl15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_TxDis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_TxDis.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_TxDis.FormattingEnabled = true;
		this.coBox_TxDis.Items.AddRange(new object[2] { "关闭", "开启" });
		this.coBox_TxDis.Location = new System.Drawing.Point(427, 258);
		this.coBox_TxDis.Name = "coBox_TxDis";
		this.coBox_TxDis.Size = new System.Drawing.Size(85, 20);
		this.coBox_TxDis.TabIndex = 22;
		this.lbl19.Location = new System.Drawing.Point(304, 200);
		this.lbl19.Name = "lbl19";
		this.lbl19.Size = new System.Drawing.Size(120, 20);
		this.lbl19.TabIndex = 47;
		this.lbl19.Text = "静噪模式";
		this.lbl19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl14.Location = new System.Drawing.Point(22, 200);
		this.lbl14.Name = "lbl14";
		this.lbl14.Size = new System.Drawing.Size(120, 20);
		this.lbl14.TabIndex = 29;
		this.lbl14.Text = "可选信令(DTMF)序号";
		this.lbl14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_IdxDtmf.DropDownHeight = 80;
		this.coBox_IdxDtmf.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_IdxDtmf.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_IdxDtmf.FormattingEnabled = true;
		this.coBox_IdxDtmf.IntegralHeight = false;
		this.coBox_IdxDtmf.Items.AddRange(new object[16]
		{
			"1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
			"11", "12", "13", "14", "15", "16"
		});
		this.coBox_IdxDtmf.Location = new System.Drawing.Point(148, 200);
		this.coBox_IdxDtmf.Name = "coBox_IdxDtmf";
		this.coBox_IdxDtmf.Size = new System.Drawing.Size(84, 20);
		this.coBox_IdxDtmf.TabIndex = 30;
		this.lbl13.Location = new System.Drawing.Point(306, 228);
		this.lbl13.Name = "lbl13";
		this.lbl13.Size = new System.Drawing.Size(120, 20);
		this.lbl13.TabIndex = 31;
		this.lbl13.Text = "紧急警报系统";
		this.lbl13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_Emerg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Emerg.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Emerg.FormattingEnabled = true;
		this.coBox_Emerg.Items.AddRange(new object[11]
		{
			"无", "1", "2", "3", "4", "5", "6", "7", "8", "9",
			"10"
		});
		this.coBox_Emerg.Location = new System.Drawing.Point(427, 229);
		this.coBox_Emerg.Name = "coBox_Emerg";
		this.coBox_Emerg.Size = new System.Drawing.Size(85, 20);
		this.coBox_Emerg.TabIndex = 36;
		this.coBox_Scan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Scan.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Scan.FormattingEnabled = true;
		this.coBox_Scan.Items.AddRange(new object[11]
		{
			"无", "1", "2", "3", "4", "5", "6", "7", "8", "9",
			"10"
		});
		this.coBox_Scan.Location = new System.Drawing.Point(429, 380);
		this.coBox_Scan.Name = "coBox_Scan";
		this.coBox_Scan.Size = new System.Drawing.Size(85, 20);
		this.coBox_Scan.TabIndex = 1;
		this.coBox_Scan.Visible = false;
		this.lbl8.Location = new System.Drawing.Point(34, 80);
		this.lbl8.Name = "lbl8";
		this.lbl8.Size = new System.Drawing.Size(107, 20);
		this.lbl8.TabIndex = 2;
		this.lbl8.Text = "信道名称";
		this.lbl8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.lbl8.Visible = false;
		this.coBox_Power.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
		this.coBox_Power.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Power.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Power.FormattingEnabled = true;
		this.coBox_Power.Items.AddRange(new object[3] { "低", "中", "高" });
		this.coBox_Power.Location = new System.Drawing.Point(429, 140);
		this.coBox_Power.Name = "coBox_Power";
		this.coBox_Power.Size = new System.Drawing.Size(85, 20);
		this.coBox_Power.TabIndex = 40;
		this.grp1.Controls.Add(this.tBox_OftFreq);
		this.grp1.Controls.Add(this.coBox_FreqDir);
		this.grp1.Controls.Add(this.label10);
		this.grp1.Controls.Add(this.label11);
		this.grp1.Controls.Add(this.coBox_FreqStep);
		this.grp1.Controls.Add(this.label9);
		this.grp1.Controls.Add(this.coBox_Talkaround);
		this.grp1.Controls.Add(this.label8);
		this.grp1.Controls.Add(this.coBox_DaoFreq);
		this.grp1.Controls.Add(this.label7);
		this.grp1.Controls.Add(this.label6);
		this.grp1.Controls.Add(this.coBox_Idx2Tone);
		this.grp1.Controls.Add(this.label5);
		this.grp1.Controls.Add(this.coBox_IdxMdc);
		this.grp1.Controls.Add(this.label4);
		this.grp1.Controls.Add(this.coBox_Idx5Tone);
		this.grp1.Controls.Add(this.coBox_BusyLock);
		this.grp1.Controls.Add(this.lbl15);
		this.grp1.Controls.Add(this.label1);
		this.grp1.Controls.Add(this.coBox_SelSignal);
		this.grp1.Controls.Add(this.coBox_TxDis);
		this.grp1.Controls.Add(this.label3);
		this.grp1.Controls.Add(this.lbl14);
		this.grp1.Controls.Add(this.label2);
		this.grp1.Controls.Add(this.coBox_IdxDtmf);
		this.grp1.Controls.Add(this.coBox_DtmfPtid);
		this.grp1.Controls.Add(this.coBox_SqlMode);
		this.grp1.Controls.Add(this.lbl13);
		this.grp1.Controls.Add(this.lbl2);
		this.grp1.Controls.Add(this.coBox_Emerg);
		this.grp1.Controls.Add(this.coBox_QtEnc);
		this.grp1.Controls.Add(this.lbl19);
		this.grp1.Controls.Add(this.coBox_BandWide);
		this.grp1.Controls.Add(this.coBox_QtDec);
		this.grp1.Controls.Add(this.tBox_Chnname);
		this.grp1.Controls.Add(this.lbl23);
		this.grp1.Controls.Add(this.lbl1);
		this.grp1.Controls.Add(this.lbl21);
		this.grp1.Controls.Add(this.coBox_Scan);
		this.grp1.Controls.Add(this.coBox_Jump);
		this.grp1.Controls.Add(this.coBox_5tonePtid);
		this.grp1.Controls.Add(this.coBox_Power);
		this.grp1.Controls.Add(this.lbl8);
		this.grp1.Controls.Add(this.lbl7);
		this.grp1.Controls.Add(this.lbl11);
		this.grp1.Controls.Add(this.lbl9);
		this.grp1.Controls.Add(this.tBox_TxFreq);
		this.grp1.Controls.Add(this.lbl3);
		this.grp1.Controls.Add(this.tBox_RxFreq);
		this.grp1.Controls.Add(this.lbl10);
		this.grp1.Location = new System.Drawing.Point(11, 13);
		this.grp1.Name = "grp1";
		this.grp1.Size = new System.Drawing.Size(572, 412);
		this.grp1.TabIndex = 122;
		this.grp1.TabStop = false;
		this.tBox_OftFreq.Location = new System.Drawing.Point(427, 50);
		this.tBox_OftFreq.MaxLength = 7;
		this.tBox_OftFreq.Name = "tBox_OftFreq";
		this.tBox_OftFreq.Size = new System.Drawing.Size(100, 21);
		this.tBox_OftFreq.TabIndex = 154;
		this.tBox_OftFreq.KeyPress += new System.Windows.Forms.KeyPressEventHandler(EditFreq_KeyPress);
		this.coBox_FreqDir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_FreqDir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_FreqDir.FormattingEnabled = true;
		this.coBox_FreqDir.Items.AddRange(new object[3] { "无", "+", "-" });
		this.coBox_FreqDir.Location = new System.Drawing.Point(148, 50);
		this.coBox_FreqDir.Name = "coBox_FreqDir";
		this.coBox_FreqDir.Size = new System.Drawing.Size(85, 20);
		this.coBox_FreqDir.TabIndex = 152;
		this.coBox_FreqDir.SelectedIndexChanged += new System.EventHandler(coBox_FreqDir_SelectedIndexChanged);
		this.label10.Location = new System.Drawing.Point(6, 50);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(136, 20);
		this.label10.TabIndex = 151;
		this.label10.Text = "差频方向";
		this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label11.Location = new System.Drawing.Point(239, 50);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(184, 20);
		this.label11.TabIndex = 153;
		this.label11.Text = "差频频率(MHz)";
		this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_FreqStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_FreqStep.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_FreqStep.FormattingEnabled = true;
		this.coBox_FreqStep.Items.AddRange(new object[11]
		{
			"2.5", "5", "6.25", "8.33", "10", "12.5", "20", "25", "30", "50",
			"100"
		});
		this.coBox_FreqStep.Location = new System.Drawing.Point(427, 80);
		this.coBox_FreqStep.Name = "coBox_FreqStep";
		this.coBox_FreqStep.Size = new System.Drawing.Size(85, 20);
		this.coBox_FreqStep.TabIndex = 150;
		this.label9.Location = new System.Drawing.Point(301, 80);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(121, 20);
		this.label9.TabIndex = 149;
		this.label9.Text = "频率步进(kHz)";
		this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_Talkaround.DropDownHeight = 90;
		this.coBox_Talkaround.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Talkaround.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Talkaround.FormattingEnabled = true;
		this.coBox_Talkaround.IntegralHeight = false;
		this.coBox_Talkaround.Items.AddRange(new object[2] { "关闭", "开启" });
		this.coBox_Talkaround.Location = new System.Drawing.Point(147, 380);
		this.coBox_Talkaround.Name = "coBox_Talkaround";
		this.coBox_Talkaround.Size = new System.Drawing.Size(85, 20);
		this.coBox_Talkaround.TabIndex = 148;
		this.coBox_Talkaround.SelectedIndexChanged += new System.EventHandler(coBox_Talkaround_SelectedIndexChanged);
		this.label8.Location = new System.Drawing.Point(38, 380);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(103, 20);
		this.label8.TabIndex = 147;
		this.label8.Text = "脱网";
		this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_DaoFreq.DropDownHeight = 90;
		this.coBox_DaoFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_DaoFreq.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_DaoFreq.FormattingEnabled = true;
		this.coBox_DaoFreq.IntegralHeight = false;
		this.coBox_DaoFreq.Items.AddRange(new object[2] { "关闭", "开启" });
		this.coBox_DaoFreq.Location = new System.Drawing.Point(429, 318);
		this.coBox_DaoFreq.Name = "coBox_DaoFreq";
		this.coBox_DaoFreq.Size = new System.Drawing.Size(85, 20);
		this.coBox_DaoFreq.TabIndex = 146;
		this.coBox_DaoFreq.SelectedIndexChanged += new System.EventHandler(coBox_DaoFreq_SelectedIndexChanged);
		this.label7.Location = new System.Drawing.Point(320, 318);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(103, 20);
		this.label7.TabIndex = 145;
		this.label7.Text = "倒频";
		this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label6.Location = new System.Drawing.Point(22, 230);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(120, 20);
		this.label6.TabIndex = 143;
		this.label6.Text = "可选信令(2音)序号";
		this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_Idx2Tone.DropDownHeight = 80;
		this.coBox_Idx2Tone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Idx2Tone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Idx2Tone.FormattingEnabled = true;
		this.coBox_Idx2Tone.IntegralHeight = false;
		this.coBox_Idx2Tone.Items.AddRange(new object[16]
		{
			"1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
			"11", "12", "13", "14", "15", "16"
		});
		this.coBox_Idx2Tone.Location = new System.Drawing.Point(148, 230);
		this.coBox_Idx2Tone.Name = "coBox_Idx2Tone";
		this.coBox_Idx2Tone.Size = new System.Drawing.Size(84, 20);
		this.coBox_Idx2Tone.TabIndex = 144;
		this.label5.Location = new System.Drawing.Point(21, 290);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(120, 20);
		this.label5.TabIndex = 141;
		this.label5.Text = "可选信令(MDC)序号";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_IdxMdc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_IdxMdc.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_IdxMdc.FormattingEnabled = true;
		this.coBox_IdxMdc.Items.AddRange(new object[5] { "1", "2", "3", "4", "5" });
		this.coBox_IdxMdc.Location = new System.Drawing.Point(148, 290);
		this.coBox_IdxMdc.Name = "coBox_IdxMdc";
		this.coBox_IdxMdc.Size = new System.Drawing.Size(84, 20);
		this.coBox_IdxMdc.TabIndex = 142;
		this.label4.Location = new System.Drawing.Point(21, 261);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(120, 20);
		this.label4.TabIndex = 139;
		this.label4.Text = "可选信令(5音)序号";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_Idx5Tone.DropDownHeight = 80;
		this.coBox_Idx5Tone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_Idx5Tone.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_Idx5Tone.FormattingEnabled = true;
		this.coBox_Idx5Tone.IntegralHeight = false;
		this.coBox_Idx5Tone.Items.AddRange(new object[100]
		{
			"1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
			"11", "12", "13", "14", "15", "16", "17", "18", "19", "20",
			"21", "22", "23", "24", "25", "26", "27", "28", "29", "30",
			"31", "32", "33", "34", "35", "36", "37", "38", "39", "40",
			"41", "42", "43", "44", "45", "46", "47", "48", "49", "50",
			"51", "52", "53", "54", "55", "56", "57", "58", "59", "60",
			"61", "62", "63", "64", "65", "66", "67", "68", "69", "70",
			"71", "72", "73", "74", "75", "76", "77", "78", "79", "80",
			"81", "82", "83", "84", "85", "86", "87", "88", "89", "90",
			"91", "92", "93", "94", "95", "96", "97", "98", "99", "100"
		});
		this.coBox_Idx5Tone.Location = new System.Drawing.Point(148, 260);
		this.coBox_Idx5Tone.Name = "coBox_Idx5Tone";
		this.coBox_Idx5Tone.Size = new System.Drawing.Size(84, 20);
		this.coBox_Idx5Tone.TabIndex = 140;
		this.coBox_SelSignal.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
		this.coBox_SelSignal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_SelSignal.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_SelSignal.FormattingEnabled = true;
		this.coBox_SelSignal.Items.AddRange(new object[7] { "关", "DTMF", "2Tone", "5Tone", "MDC", "BIIS", "APRS" });
		this.coBox_SelSignal.Location = new System.Drawing.Point(148, 170);
		this.coBox_SelSignal.Name = "coBox_SelSignal";
		this.coBox_SelSignal.Size = new System.Drawing.Size(84, 20);
		this.coBox_SelSignal.TabIndex = 138;
		this.coBox_SelSignal.SelectedIndexChanged += new System.EventHandler(coBox_SelSignal_SelectedIndexChanged);
		this.label3.Location = new System.Drawing.Point(24, 170);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(120, 20);
		this.label3.TabIndex = 137;
		this.label3.Text = "可选信令";
		this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label2.Location = new System.Drawing.Point(41, 320);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(100, 20);
		this.label2.TabIndex = 136;
		this.label2.Text = "DTMF PTT ID";
		this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.coBox_DtmfPtid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.coBox_DtmfPtid.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.coBox_DtmfPtid.FormattingEnabled = true;
		this.coBox_DtmfPtid.Items.AddRange(new object[4] { "关", "上线码", "下线码", "两者" });
		this.coBox_DtmfPtid.Location = new System.Drawing.Point(147, 320);
		this.coBox_DtmfPtid.Name = "coBox_DtmfPtid";
		this.coBox_DtmfPtid.Size = new System.Drawing.Size(85, 20);
		this.coBox_DtmfPtid.TabIndex = 135;
		this.tBox_Chnname.Location = new System.Drawing.Point(147, 80);
		this.tBox_Chnname.MaxLength = 16;
		this.tBox_Chnname.Name = "tBox_Chnname";
		this.tBox_Chnname.Size = new System.Drawing.Size(115, 21);
		this.tBox_Chnname.TabIndex = 1;
		this.tBox_Chnname.Visible = false;
		this.lbl1.Location = new System.Drawing.Point(41, 350);
		this.lbl1.Name = "lbl1";
		this.lbl1.Size = new System.Drawing.Size(100, 20);
		this.lbl1.TabIndex = 122;
		this.lbl1.Text = "5音 PTT ID";
		this.lbl1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		this.AutoScroll = true;
		base.ClientSize = new System.Drawing.Size(612, 436);
		base.Controls.Add(this.grp1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Frm_VFO";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = "VFO数据";
		base.Load += new System.EventHandler(Frm_Chn_Load);
		base.Activated += new System.EventHandler(Frm_Chn_Activated);
		this.grp1.ResumeLayout(false);
		this.grp1.PerformLayout();
		base.ResumeLayout(false);
	}
}
