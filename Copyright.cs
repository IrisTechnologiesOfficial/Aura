//Copyright 2019 Iris Technologies, All Rights Reserved
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AuraEngine
{
    internal static class Copyright
    {
        private static bool CopyrightLoaded = false;

        private static void BackSelectionEnter(object s, EventArgs e)
        {
            Asset.FindByName("BackSelection").GetEntity().ForeColor = Color.Red;
        }

        private static void BackSelectionLeave(object s, EventArgs e)
        {
            Asset.FindByName("BackSelection").GetEntity().ForeColor = Color.White;
        }

        private static void BackSelection(object s, EventArgs e)
        {
            CheckBox c = (CheckBox)Asset.FindByName("AcceptTerms").GetEntity();
            c.CheckState = CheckState.Unchecked;
            Scene.Hide("Copyright");
            Selection.Draw();
        }

        private static void NextInstallationEnter(object s, EventArgs e)
        {
            Asset.FindByName("NextInstallation").GetEntity().ForeColor = Color.Blue;
        }

        private static void NextInstallationLeave(object s, EventArgs e)
        {
            Asset.FindByName("NextInstallation").GetEntity().ForeColor = Color.White;
        }

        private static void NextInstallation(object s, EventArgs e)
        {
            if (((CheckBox)(Asset.FindByName("AcceptTerms").GetEntity())).CheckState == CheckState.Checked)
            {
                Scene.Hide("Copyright");
                Installation.Draw();
            }
        }

        internal static void Draw()
        {
            Database.ActualScene = "Copyright";
            if (!CopyrightLoaded)
            {
                CopyrightLoaded = true;
                Scene s = new Scene("Copyright");
                CheckBox c = new CheckBox();
                c.Size = new Size(450, 25);
                c.Location = new Point(25, 210);
                c.BackColor = Color.Black;
                c.ForeColor = Color.White;
                c.Font = new Font(FontFamily.GenericSerif, 13, FontStyle.Bold | FontStyle.Italic);
                c.TextAlign = ContentAlignment.MiddleCenter;
                c.Text = "I read and accept the 'Terms and Conditions of Service'";
                Asset ca = new Asset(c, "AcceptTerms");
                s.AddAsset("AcceptTerms");
                Label l = new Label();
                l.Size = new Size(100, 25);
                l.Location = new Point(100, 250);
                l.BackColor = Color.Black;
                l.ForeColor = Color.White;
                l.Font = new Font(FontFamily.GenericSerif, 13, FontStyle.Bold);
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.Text = "Back";
                l.MouseEnter += BackSelectionEnter;
                l.MouseLeave += BackSelectionLeave;
                l.Click += BackSelection;
                Asset la = new Asset(l, "BackSelection");
                s.AddAsset("BackSelection");
                Label ll = new Label();
                ll.Size = new Size(100, 25);
                ll.Location = new Point(300, 250);
                ll.BackColor = Color.Black;
                ll.ForeColor = Color.White;
                ll.Font = new Font(FontFamily.GenericSerif, 13, FontStyle.Bold);
                ll.TextAlign = ContentAlignment.MiddleCenter;
                ll.Text = "Next";
                ll.MouseEnter += NextInstallationEnter;
                ll.MouseLeave += NextInstallationLeave;
                ll.Click += NextInstallation;
                Asset lla = new Asset(ll, "NextInstallation");
                s.AddAsset("NextInstallation");
                RichTextBox r = new RichTextBox();
                r.Size = new Size(450, 120);
                r.Location = new Point(25, 80);
                r.ScrollBars = RichTextBoxScrollBars.Both;
                r.Multiline = true;
                r.ReadOnly = true;
                r.Font = new Font(FontFamily.GenericSerif, 13, FontStyle.Bold | FontStyle.Italic);
                r.BorderStyle = BorderStyle.None;
                r.Text = "      IRIS TECHNOLOGIES PROPIETARY LICENSE\n\n\nBy accepting our terms of 'Service', you accept the following rules:\n\n1.- You(collective or individual of any mean) cannot redistribute,\n nor modify, any of the software copyrighted by Iris Technologies.\n\n2.- You(collective or individual of any mean) gives us(Iris Technologies) the\npermission of collecting your data for any purpose.\n\n3.- You(collective or individual of any mean) gives us(Iris Technologies) the\nfreedom(legal and economic) of any of the terms you impose on your data.";
                Asset ra = new Asset(r, "LicenseTerm");
                s.AddAsset("LicenseTerm");
                Scene.Register(s);
            }
            else
                Scene.Show("Copyright");
        }
    }
}