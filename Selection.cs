//Copyright 2019 Iris Technologies, All Rights Reserved
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AuraEngine
{
    internal static class Selection
    {
        private static bool SelectionLoaded = false;

        private static void BackOFEnter(object s, EventArgs e)
        {
            Asset.FindByName("BackOF").GetEntity().ForeColor = Color.Red;
        }

        private static void BackOFLeave(object s, EventArgs e)
        {
            Asset.FindByName("BackOF").GetEntity().ForeColor = Color.White;
        }

        private static void BackOF(object s, EventArgs e)
        {
            Scene.Hide("Selection");
            OutputFolder.Draw();
        }

        private static void NextCpyEnter(object s, EventArgs e)
        {
            Asset.FindByName("NextCpy").GetEntity().ForeColor = Color.Blue;
        }

        private static void NextCpyLeave(object s, EventArgs e)
        {
            Asset.FindByName("NextCpy").GetEntity().ForeColor = Color.White;
        }

        private static void NextCpy(object s, EventArgs e)
        {
            if (((CheckBox)(Asset.FindByName("Nerve").GetEntity())).CheckState == CheckState.Checked)
                Database.Nerve = true;
            if (((CheckBox)(Asset.FindByName("Iris").GetEntity())).CheckState == CheckState.Checked)
                Database.Iris = true;
            if (!Database.Nerve && !Database.Iris)
                Utilities.Info("Please, select at least 1 package to install!");
            else
            {
                Scene.Hide("Selection");
                Copyright.Draw();
            }
        }

        internal static void Draw()
        {
            Database.ActualScene = "Selection";
            if (!SelectionLoaded)
            {
                SelectionLoaded = true;
                Database.Aura.MinimumSize = new Size(500, 300);
                Database.Aura.Size = new Size(500, 300);
                Utilities.CenterForm();
                Scene s = new Scene("Selection");
                Label l = new Label();
                l.Size = new Size(480, 25);
                l.Location = new Point(10, 100);
                l.ForeColor = Color.White;
                l.BackColor = Color.Black;
                l.Font = new Font(FontFamily.GenericSerif, 13, FontStyle.Bold);
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.Text = "Please, select the packets you'd like to install";
                Asset la = new Asset(l, "SMsg");
                s.AddAsset("SMsg");
                CheckBox c = new CheckBox();
                c.Size = new Size(100, 25);
                c.Location = new Point(200, 150);
                c.BackColor = Color.Black;
                c.ForeColor = Color.White;
                c.Font = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Bold);
                c.TextAlign = ContentAlignment.MiddleCenter;
                c.Text = "Nerve";
                Asset ca = new Asset(c, "Nerve");
                s.AddAsset("Nerve");
                CheckBox cc = new CheckBox();
                cc.Size = new Size(100, 25);
                cc.Location = new Point(200, 200);
                cc.BackColor = Color.Black;
                cc.ForeColor = Color.White;
                cc.Font = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Bold);
                cc.TextAlign = ContentAlignment.MiddleCenter;
                cc.Text = "Iris";
                Asset cca = new Asset(cc, "Iris");
                s.AddAsset("Iris");
                Label ll = new Label();
                ll.Size = new Size(100, 25);
                ll.Location = new Point(100, 250);
                ll.BackColor = Color.Black;
                ll.ForeColor = Color.White;
                ll.Font = new Font(FontFamily.GenericSerif, 12, FontStyle.Bold);
                ll.TextAlign = ContentAlignment.MiddleCenter;
                ll.Text = "Back";
                ll.MouseEnter += BackOFEnter;
                ll.MouseLeave += BackOFLeave;
                ll.Click += BackOF;
                Asset lla = new Asset(ll, "BackOF");
                s.AddAsset("BackOF");
                Label lll = new Label();
                lll.Size = new Size(100, 25);
                lll.Location = new Point(300, 250);
                lll.BackColor = Color.Black;
                lll.ForeColor = Color.White;
                lll.Font = new Font(FontFamily.GenericSerif, 12, FontStyle.Bold);
                lll.TextAlign = ContentAlignment.MiddleCenter;
                lll.Text = "Next";
                lll.MouseEnter += NextCpyEnter;
                lll.MouseLeave += NextCpyLeave;
                lll.Click += NextCpy;
                Asset llla = new Asset(lll, "NextCpy");
                s.AddAsset("NextCpy");
                Scene.Register(s);
            }
            else
                Scene.Show("Selection");
        }
    }
}