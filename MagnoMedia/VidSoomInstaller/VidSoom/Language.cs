using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace VidSoom
{
    public static class Language
    {
        public enum eLang
        {
            Spanish = 0,
            English = 1
        }
        public static eLang currentLang = eLang.Spanish;

        public static string getString(Form form, string key)
        {
            System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("VidSoom." + currentLang, typeof(Buscadores).Assembly);
            return resMan.GetString((form==null ? "General" : form.Name) + "_" + key);
        }
        public static string getString(Form form, string key, eLang lang)
        {
            System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("VidSoom." + lang, typeof(Buscadores).Assembly);
            return resMan.GetString(form.Name + "_" + key);
        }
        public static void translateForm(Form form)
        {
            Queue<Control.ControlCollection> controls = new Queue<Control.ControlCollection>();
            Control.ControlCollection current;
            string value;
            controls.Enqueue(form.Controls);

            value = getString(form, form.Name);
            if (value != null)
                form.Text = getString(form, form.Name);

            while (controls.Count > 0)
            {
                current = controls.Dequeue();
                foreach (Control c in current)
                {
                    if (c.GetType() == typeof(ToolStrip))
                    {
                        foreach (ToolStripItem i in ((ToolStrip)c).Items)
                        {
                            i.Text = getString(form, i.Name);
                            i.ToolTipText = i.Text;
                        }
                    }
                    else if (c.GetType() == typeof(MenuStrip))
                    {
                        foreach (ToolStripMenuItem i in ((MenuStrip)c).Items)
                        {
                            i.Text = getString(form, i.Name);
                            if (i.DropDownItems.Count > 0)
                            {
                                foreach (ToolStripItem e in ((ToolStripMenuItem)i).DropDownItems)
                                {
                                    e.Text = getString(form, e.Name);
                                }
                            }
                        }
                    }
                    else if (c.GetType() == typeof(ContextMenuStrip))
                    {
                        foreach (ToolStripMenuItem i in ((ContextMenuStrip)c).Items)
                        {
                            i.Text = getString(form, i.Name);
                        }
                    }
                    else if (c.GetType() == typeof(TabControl))
                    {
                        foreach (TabPage i in ((TabControl)c).TabPages)
                        {
                            i.Text = getString(form, i.Name);
                            if (i.HasChildren) controls.Enqueue(i.Controls);
                        }
                    }
                    else if (c.GetType() == typeof(DataGridView))
                    {
                        foreach (DataGridViewColumn i in ((DataGridView)c).Columns)
                        {
                            i.HeaderText = getString(form, i.Name);
                        }
                    }
                    else
                    {
                        value = getString(form, c.Name);
                        if (value != null)
                            c.Text = getString(form, c.Name);
                    }
                    if (c.HasChildren) controls.Enqueue(c.Controls);
                }
            }
        }
    }
}
