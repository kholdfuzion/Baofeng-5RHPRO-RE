using System;
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
            else
            {
                if (field.FieldType != typeof(string[]))
                {
                    continue;
                }
                object o = field.GetValue(null);
                if (o is string[] langs)
                {
                    string[] newValues = values[index].Split(',');
                    for (int j = 0; j < Math.Min(langs.Length, newValues.Length); j++)
                    {
                        langs[j] = newValues[j].Trim();
                    }
                }
            }
        }
    }

    public static void ApplyTabControl(string section, TabControl tab)
    {
        foreach (TabPage page in tab.TabPages)
        {
            ApplyTabPage(section, page);
        }
    }

    public static void ApplyTabPage(string section, TabPage page)
    {
        page.Text = LangIniHelper.ReadString(section, page.Name, "");
        foreach (Control ctrl in page.Controls)
        {
            ApplyControl(section, ctrl);
        }
    }

    public static void ApplyToolStrip(string section, ToolStrip tsr)
    {
        foreach (ToolStripItem tsi in tsr.Items)
        {
            ApplyToolStripItem(section, tsi);
        }
    }

    public static void ApplyToolStripItem(string section, ToolStripItem tsi)
    {
        if (tsi is ToolStripDropDownItem)
        {
            string text = (tsi.ToolTipText = LangIniHelper.ReadString(section, tsi.Name, ""));
            tsi.Text = text;
            ToolStripDropDownItem tsddi = tsi as ToolStripDropDownItem;
            {
                foreach (ToolStripItem subTsi in tsddi.DropDownItems)
                {
                    ApplyToolStripItem(section, subTsi);
                }
                return;
            }
        }
        if (!(tsi is ToolStripSeparator))
        {
            string text3 = (tsi.ToolTipText = LangIniHelper.ReadString(section, tsi.Name, ""));
            tsi.Text = text3;
        }
    }

    public static void ApplyForm(Form form)
    {
        form.Text = LangIniHelper.ReadString(form.Name, "Text", "");
        foreach (Control ctrl in form.Controls)
        {
            ApplyControl(form.Name, ctrl);
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
            IViewData viewData = ctrl as IViewData;
            viewData.LoadLanguageText(ctrl.Name);
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
            foreach (Control subCtrl in ctrl.Controls)
            {
                ApplyControl(section, subCtrl);
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
