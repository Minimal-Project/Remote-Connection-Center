using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Server_Manager.Theme
{
    public static class ThemeManager
    {
        public static ThemeMode CurrentMode { get; private set; } = ThemeMode.Light;
        public static ThemePalette Palette => CurrentMode == ThemeMode.Dark ? ThemePalette.Dark : ThemePalette.Light;

        public static event EventHandler? ThemeChanged;

        public static void InitializeFromDisk()
        {
            SetMode(ThemeSettingsStore.Load(), persist: false);
        }

        public static void SetMode(ThemeMode mode, bool persist)
        {
            if (CurrentMode == mode) return;

            CurrentMode = mode;
            if (persist)
                ThemeSettingsStore.Save(mode);

            ThemeChanged?.Invoke(null, EventArgs.Empty);
        }

        public static void ApplyTo(Form form)
        {
            if (form is null) return;

            var p = Palette;

            form.BackColor = p.Back;
            form.ForeColor = p.Fore;

            ApplyToControlTree(form);

            foreach (var ms in FindControls<MenuStrip>(form))
                ApplyMenuStrip(ms);

            foreach (var ts in FindControls<ToolStrip>(form))
                ApplyToolStrip(ts);
        }

        private static void ApplyToControlTree(Control root)
        {
            var p = Palette;

            foreach (Control c in root.Controls)
            {
                ApplyControl(c, p);

                if (c.HasChildren)
                    ApplyToControlTree(c);
            }
        }

        private static void ApplyControl(Control c, ThemePalette p)
        {
            switch (c)
            {
                case Panel or GroupBox or TableLayoutPanel or FlowLayoutPanel or SplitContainer:
                    c.BackColor = p.Back;
                    c.ForeColor = p.Fore;
                    break;

                case Label:
                    c.BackColor = Color.Transparent;
                    c.ForeColor = p.Fore;
                    break;

                case TextBox tb:
                    tb.BorderStyle = BorderStyle.FixedSingle;
                    tb.BackColor = p.InputBack;
                    tb.ForeColor = p.InputFore;
                    break;

                case RichTextBox rtb:
                    rtb.BackColor = p.InputBack;
                    rtb.ForeColor = p.InputFore;
                    break;

                case Button b:
                    b.FlatStyle = FlatStyle.Standard;
                    b.BackColor = p.SurfaceAlt;
                    b.ForeColor = p.Fore;
                    break;

                case CheckBox cb:
                    cb.BackColor = Color.Transparent;
                    cb.ForeColor = p.Fore;
                    break;

                case ComboBox combo:
                    combo.BackColor = p.InputBack;
                    combo.ForeColor = p.InputFore;
                    break;

                case DataGridView gv:
                    ApplyGrid(gv, p);
                    break;

                default:
                    c.BackColor = p.Back;
                    c.ForeColor = p.Fore;
                    break;
            }
        }

        private static void ApplyGrid(DataGridView gv, ThemePalette p)
        {
            gv.BackgroundColor = p.Back;
            gv.GridColor = p.Border;

            gv.EnableHeadersVisualStyles = false;

            gv.ColumnHeadersDefaultCellStyle.BackColor = p.SurfaceAlt;
            gv.ColumnHeadersDefaultCellStyle.ForeColor = p.Fore;

            gv.DefaultCellStyle.BackColor = p.Surface;
            gv.DefaultCellStyle.ForeColor = p.Fore;
            gv.DefaultCellStyle.SelectionBackColor = p.Accent;
            gv.DefaultCellStyle.SelectionForeColor = Color.White;

            gv.AlternatingRowsDefaultCellStyle.BackColor = p.SurfaceAlt;
            gv.AlternatingRowsDefaultCellStyle.ForeColor = p.Fore;
        }

        private static void ApplyMenuStrip(MenuStrip ms)
        {
            var p = Palette;
            ms.RenderMode = ToolStripRenderMode.System;
            ms.BackColor = p.SurfaceAlt;
            ms.ForeColor = p.Fore;
        }

        private static void ApplyToolStrip(ToolStrip ts)
        {
            var p = Palette;
            ts.RenderMode = ToolStripRenderMode.System;
            ts.BackColor = p.SurfaceAlt;
            ts.ForeColor = p.Fore;
        }

        private static System.Collections.Generic.IEnumerable<T> FindControls<T>(Control root) where T : Control
        {
            foreach (Control c in root.Controls)
            {
                if (c is T t) yield return t;
                if (c.HasChildren)
                    foreach (var sub in FindControls<T>(c))
                        yield return sub;
            }
        }
    }
}