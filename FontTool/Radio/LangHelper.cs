using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Radio;

internal class LangHelper
{
	public static IniHelper LangIniHelper = new IniHelper(Application.StartupPath + Path.DirectorySeparatorChar + Global.CurLang + ".ini");

	public static void ApplyCommon(Type t)
	{
		string[] keys = null;
		string[] values = null;
		FieldInfo[] fields = t.GetFields();
		LangIniHelper.GetAllKeyValues("Common", out keys, out values);
		FieldInfo[] array = fields;
		foreach (FieldInfo field in array)
		{
			if (Array.IndexOf(keys, field.Name) < 0)
			{
				continue;
			}
			int index = Array.IndexOf(keys, field.Name);
			if (field.FieldType == typeof(string))
			{
				field.SetValue(null, values[index]);
			}
			else if (field.FieldType == typeof(string[]) && field.GetValue(null) is string[] langs)
			{
				string[] newValues = values[index].Split(',');
				for (int j = 0; j < Math.Min(langs.Length, newValues.Length); j++)
				{
					langs[j] = newValues[j].Trim();
				}
			}
		}
	}

	public static void ApplyTabControl(string section, TabControl tab)
	{
		foreach (TabPage tabPage in tab.TabPages)
		{
			ApplyTabPage(section, tabPage);
		}
	}

	public static void ApplyTabPage(string section, TabPage page)
	{
		page.Text = LangIniHelper.ReadString(section, page.Name, "");
		foreach (Control control in page.Controls)
		{
			ApplyControl(section, control);
		}
	}

	public static void ApplyToolStrip(string section, ToolStrip tsr)
	{
		foreach (ToolStripItem item in tsr.Items)
		{
			ApplyToolStripItem(section, item);
		}
	}

	public static void ApplyToolStripItem(string section, ToolStripItem tsi)
	{
		if (tsi is ToolStripDropDownItem)
		{
			tsi.Text = (tsi.ToolTipText = LangIniHelper.ReadString(section, tsi.Name, ""));
			{
				foreach (ToolStripItem dropDownItem in (tsi as ToolStripDropDownItem).DropDownItems)
				{
					ApplyToolStripItem(section, dropDownItem);
				}
				return;
			}
		}
		if (!(tsi is ToolStripSeparator))
		{
			tsi.Text = (tsi.ToolTipText = LangIniHelper.ReadString(section, tsi.Name, ""));
		}
	}

	public static void ApplyForm(Form form)
	{
		form.Text = LangIniHelper.ReadString(form.Name, "Text", "");
		IEnumerator enumerator = form.Controls.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				ApplyControl(ctrl: (Control)enumerator.Current, section: form.Name);
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
	}

	public static void ApplyControl(string section, Control ctrl)
	{
		if (ctrl is ToolStrip)
		{
			ApplyToolStrip(section, ctrl as ToolStrip);
			return;
		}
		if (ctrl is TabControl)
		{
			ApplyTabControl(section, ctrl as TabControl);
			return;
		}
		if (ctrl is IViewData)
		{
			(ctrl as IViewData).LoadLanguageText(ctrl.Name);
			return;
		}
		if (ctrl is DataGridView)
		{
			DataGridView dgv = ctrl as DataGridView;
			string text = LangIniHelper.ReadString(section, ctrl.Name, "");
			if (!string.IsNullOrEmpty(text))
			{
				string[] newValues = text.Split(',');
				for (int i = 0; i < Math.Min(dgv.ColumnCount, newValues.Length); i++)
				{
					dgv.Columns[i].HeaderText = newValues[i].Trim();
				}
			}
			return;
		}
		if (ctrl.HasChildren)
		{
			foreach (Control control in ctrl.Controls)
			{
				ApplyControl(section, control);
			}
			return;
		}
		if (ctrl is Label || ctrl is Button)
		{
			string text2 = LangIniHelper.ReadString(section, ctrl.Name, "");
			if (!string.IsNullOrEmpty(text2))
			{
				ctrl.Text = text2;
			}
		}
	}
}
