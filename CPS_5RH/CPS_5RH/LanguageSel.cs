using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;

namespace CPS_5RH;

public class LanguageSel
{
	private static Dictionary<string, string> combobox1 = new Dictionary<string, string>();

	private static Dictionary<string, string> combobox2 = new Dictionary<string, string>();

	private static Dictionary<string, string> combobox6 = new Dictionary<string, string>();

	private static Dictionary<string, string> genCoBox = new Dictionary<string, string>();

	private static Dictionary<string, string> pbookCoBox = new Dictionary<string, string>();

	private static Dictionary<string, string> scanCoBox = new Dictionary<string, string>();

	private static Dictionary<string, string> aprsCoBox = new Dictionary<string, string>();

	public static byte Language = 0;

	public static string Lang { get; private set; }

	public static bool HasLang { get; set; }

	public static Dictionary<string, string> EmergCombobox
	{
		get
		{
			return combobox1;
		}
		private set
		{
		}
	}

	public static Dictionary<string, string> ElseCombobox
	{
		get
		{
			return combobox2;
		}
		private set
		{
		}
	}

	public static Dictionary<string, string> GenCoBox
	{
		get
		{
			return genCoBox;
		}
		private set
		{
		}
	}

	public static Dictionary<string, string> ScanCoBox
	{
		get
		{
			return scanCoBox;
		}
		private set
		{
		}
	}

	public static Dictionary<string, string> ChnCoBox
	{
		get
		{
			return combobox6;
		}
		private set
		{
		}
	}

	public static Dictionary<string, string> AprsCoBox
	{
		get
		{
			return aprsCoBox;
		}
		private set
		{
		}
	}

	public static byte LangType
	{
		get
		{
			return Language;
		}
		set
		{
			Language = value;
		}
	}

	public static bool Load(string lang)
	{
		string path = "";
		combobox1.Clear();
		combobox2.Clear();
		combobox6.Clear();
		genCoBox.Clear();
		scanCoBox.Clear();
		aprsCoBox.Clear();
		switch (lang)
		{
		case "Chinese":
			LangType = 0;
			path = Application.StartupPath + "\\lang-cn.xml";
			break;
		case "English":
			LangType = 1;
			path = Application.StartupPath + "\\lang-en.xml";
			break;
		case "kor":
			LangType = 2;
			path = Application.StartupPath + "\\lang-korean.xml";
			break;
		default:
			LangType = 0;
			path = Application.StartupPath + "\\lang-zh.xml";
			break;
		}
		return readLanguage(path);
	}

	private static bool readLanguage(string path)
	{
		XmlReader reader;
		try
		{
			reader = XmlReader.Create(path);
		}
		catch (Exception)
		{
			return false;
		}
		try
		{
			reader.ReadToFollowing("AirControl");
			Lang = reader.GetAttribute("language");
			paraseXml(reader, "EmergCombobox", combobox1);
			paraseXml(reader, "ElseCombobox", combobox2);
			paraseXml(reader, "GenCoBox", genCoBox);
			paraseXml(reader, "ScanCoBox", scanCoBox);
			paraseXml(reader, "ChnCoBox", combobox6);
			paraseXml(reader, "AprsCoBox", AprsCoBox);
		}
		catch (Exception)
		{
			return false;
		}
		return true;
	}

	private static void paraseXml(XmlReader reader, string item, Dictionary<string, string> obj)
	{
		reader.ReadToFollowing(item);
		XmlReader subreader = reader.ReadSubtree();
		while (subreader.Read())
		{
			if (subreader.NodeType == XmlNodeType.Element && subreader.Name == "Item")
			{
				obj.Add(subreader.GetAttribute("key"), subreader.GetAttribute("value"));
			}
		}
	}

	public static void LoadLanguage(Form form, Type formType)
	{
		if (form != null)
		{
			ComponentResourceManager resources = new ComponentResourceManager(formType);
			resources.ApplyResources(form, "$this");
			Loading(form, resources);
		}
	}

	private static void Loading(Control control, ComponentResourceManager resources)
	{
		if (control is MenuStrip)
		{
			resources.ApplyResources(control, control.Name);
			MenuStrip ms = (MenuStrip)control;
			if (ms.Items.Count > 0)
			{
				foreach (ToolStripMenuItem item in ms.Items)
				{
					Loading(item, resources);
				}
			}
		}
		foreach (Control c in control.Controls)
		{
			resources.ApplyResources(c, c.Name);
			Loading(c, resources);
		}
	}

	private static void Loading(ToolStripMenuItem item, ComponentResourceManager resources)
	{
		if (item == null)
		{
			return;
		}
		resources.ApplyResources(item, item.Name);
		if (item.DropDownItems.Count <= 0)
		{
			return;
		}
		foreach (ToolStripMenuItem dropDownItem in item.DropDownItems)
		{
			Loading(dropDownItem, resources);
		}
	}
}
