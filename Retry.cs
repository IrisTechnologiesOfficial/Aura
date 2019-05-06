//Copyright 2019 Iris Technologies, All Rights Reserved
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AuraEngine
{
    internal static class Retry
    {
        private static bool RetryLoaded = false;

        private static void RetryEnter(object s, EventArgs e)
        {
            Asset.FindByName("Retry").GetEntity().ForeColor = Color.Red;
        }

        private static void RetryLeave(object s, EventArgs e)
        {
            Asset.FindByName("Retry").GetEntity().ForeColor = Color.White;
        }

        private static void CRetry(object s, EventArgs e)
        {
            Scene.Hide("Retry");
            Installation.Draw();
        }

        private static void SelectionEnter(object s, EventArgs e)
        {
            Asset.FindByName("RSelection").GetEntity().ForeColor = Color.Blue;
        }

        private static void SelectionLeave(object s, EventArgs e)
        {
            Asset.FindByName("RSelection").GetEntity().ForeColor = Color.White;
        }

        private static void CSelection(object s, EventArgs e)
        {
            Scene.Hide("Retry");
            Selection.Draw();
        }

        internal static void Draw()
        {
            Installation.InfoLoaded = false;
            Database.ActualScene = "Retry";
            if (!RetryLoaded)
            {
                RetryLoaded = true;
                Scene s = new Scene("Retry");
                Label l = new Label();
                l.Size = new Size(100, 25);
                l.Location = new Point(200, 125);
                l.ForeColor = Color.White;
                l.BackColor = Color.Black;
                l.Font = new Font(FontFamily.GenericSerif, 13, FontStyle.Bold | FontStyle.Italic);
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.Text = "Retry";
                l.MouseEnter += RetryEnter;
                l.MouseLeave += RetryLeave;
                l.Click += CRetry;
                Asset la = new Asset(l, "Retry");
                s.AddAsset("Retry");
                Label ll = new Label();
                ll.Size = new Size(100, 25);
                ll.Location = new Point(200, 175);
                ll.ForeColor = Color.White;
                ll.BackColor = Color.Black;
                ll.Font = new Font(FontFamily.GenericSerif, 13, FontStyle.Bold | FontStyle.Italic);
                ll.TextAlign = ContentAlignment.MiddleCenter;
                ll.Text = "Selection";
                ll.MouseEnter += SelectionEnter;
                ll.MouseLeave += SelectionLeave;
                ll.Click += CSelection;
                Asset lla = new Asset(ll, "RSelection");
                s.AddAsset("RSelection");
                Scene.Register(s);
            }
            else
                Scene.Show("Retry");
        }
    }
}