using System;
using System.Windows.Forms;

namespace Radio;

internal static class ComboBoxHelper
{
    public static void Init(this ComboBox cmb, int min, int max, int step)
    {
        cmb.Items.Clear();
        for (int i = min; i <= max; i += step)
        {
            cmb.Items.Add(i.ToString());
        }
    }

    public static void Init(this ComboBox cmb, int min, int max, int step, double scale)
    {
        cmb.Init(min, max, step, scale, "f1");
    }

    public static void Init(this ComboBox cmb, int min, int max, int step, double scale, string format)
    {
        cmb.Items.Clear();
        for (int i = min; i <= max; i += step)
        {
            cmb.Items.Add(Common.MulDecimal(i, scale).ToString(format));
        }
    }

    public static void Init(this ComboBox cmb, string first, int min, int max, int step)
    {
        cmb.Items.Clear();
        if (!string.IsNullOrEmpty(first))
        {
            cmb.Items.Add(first);
        }
        for (int i = min; i <= max; i += step)
        {
            cmb.Items.Add(i.ToString());
        }
    }

    public static void Init(this ComboBox cmb, string[] items)
    {
        cmb.Items.Clear();
        foreach (string item in items)
        {
            cmb.Items.Add(item);
        }
    }

    public static void Init(this ComboBox cmb, string[] items, int[] indexs)
    {
        int index = 0;
        cmb.Items.Clear();
        foreach (string item in items)
        {
            if (Array.IndexOf(indexs, index) >= 0)
            {
                cmb.Items.Add(item);
            }
            index++;
        }
    }

    public static void SetCurSel(this ComboBox cmb, int index)
    {
        if (index < cmb.Items.Count)
        {
            cmb.SelectedIndex = index;
        }
        else
        {
            cmb.SelectedIndex = 0;
        }
    }
}
