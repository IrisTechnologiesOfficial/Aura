//Copyright 2019 Iris Technologies, All Rights Reserved
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AuraEngine
{
    internal static class Finish
    {
        private static void FinishEnter(object s, EventArgs e)
        {
            Asset.FindByName("Finish").GetEntity().ForeColor = Color.Silver;
        }

        private static void FinishLeave(object s, EventArgs e)
        {
            Asset.FindByName("Finish").GetEntity().ForeColor = Color.White;
        }

        private static void CFinish(object s, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }

        internal static void Draw()
        {
            Label l = new Label();
            l.Size = new Size(100, 25);
            l.Location = Utilities.CenterOnForm(l.Size);
            l.ForeColor = Color.White;
            l.BackColor = Color.Black;
            l.Font = new Font(FontFamily.GenericSerif, 13, FontStyle.Bold | FontStyle.Italic);
            l.TextAlign = ContentAlignment.MiddleCenter;
            l.Text = "Finish";
            l.MouseEnter += FinishEnter;
            l.MouseLeave += FinishLeave;
            l.Click += CFinish;
            Asset la = new Asset(l, "Finish");
        }
    }
}