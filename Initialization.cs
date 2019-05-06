//Copyright 2019 Iris Technologies, All Rights Reserved
using System;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace AuraEngine
{
    internal static class Initialization
    {
        private static Timer t = null;
        private static Timer tt = null;
        private static int duh = 0;
        private static int duh2 = 0;
        private static Point Cursor;

        private static void TitleDown(object s, EventArgs e)
        {
            tt.Start();
            Scene.Hide(Database.ActualScene);
            Cursor = Utilities.GetCursorPosition();
        }

        private static void TitleUp(object s, EventArgs e)
        {
            tt.Stop();
            Scene.Show(Database.ActualScene, false);
            duh = 0;
            duh2 = 0;
            Database.Aura.BackColor = Color.Black;
            Control c = Asset.FindByName("Title").GetEntity();
            c.BackColor = Color.Black;
            c.ForeColor = Color.White;
        }

        private static void TitleMoving(object s, EventArgs e)
        {
            if (duh == 255)
                duh = 0;
            if (duh2 == 255)
                duh2 = 0;
            Asset c = Asset.FindByName("Title");
            c.UpdatePosition(Cursor);
            Cursor = Utilities.GetCursorPosition();
            c.GetEntity().BackColor = Color.FromArgb(255, duh, duh2, 0);
            c.GetEntity().ForeColor = Color.FromArgb(255, 0, duh2, duh);
            Database.Aura.BackColor = Color.FromArgb(255, duh, duh2, 0);
            duh++;
            duh2++;
        }

        private static void CloseEnter(object s, EventArgs e)
        {
            Control c = Asset.FindByName("Close").GetEntity();
            c.BackColor = Color.Red;
            c.ForeColor = Color.Black;
        }

        private static void CloseLeave(object s, EventArgs e)
        {
            Control c = Asset.FindByName("Close").GetEntity();
            c.BackColor = Color.Black;
            c.ForeColor = Color.White;
        }

        private static void Close(object s, EventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }

        private static void NextToOutputFolderEnter(object s, EventArgs e)
        {
            Asset.FindByName("NextOutputFolder").GetEntity().ForeColor = Color.Blue;
        }

        private static void NextToOutputFolderLeave(object s, EventArgs e)
        {
            Asset.FindByName("NextOutputFolder").GetEntity().ForeColor = Color.White;
        }

        private static void NextToOutputFolder(object s, EventArgs e)
        {
            Scene.Hide("Welcome");
            OutputFolder.Draw();
        }

        private static void AuraOpacity(object s, EventArgs e)
        {
            if (Database.Aura.Opacity < 1)
            {
                if (duh == 255)
                    duh = 0;
                if (duh2 == 24)
                    duh2 = 0;
                Database.Aura.Opacity += 0.001;
                Asset.FindByName("SplashScreen_1").GetEntity().ForeColor = Color.FromArgb(0, 0, duh, duh2);
                duh++;
                duh2++;
            }
            else
            {
                t.Stop();
                duh = 0;
                duh2 = 0;
                tt = new Timer();
                tt.Interval = 4;
                tt.Tick += TitleMoving;
                Scene.Hide("SplashScreen");
                Label l = new Label();
                l.Size = new Size(300, 20);
                l.Location = new Point(5, 5);
                l.ForeColor = Color.White;
                l.BackColor = Color.Black;
                l.Font = new Font(FontFamily.GenericSerif, 15, FontStyle.Bold | FontStyle.Italic);
                l.TextAlign = ContentAlignment.MiddleLeft;
                l.Text = "AURA";
                l.MouseDown += TitleDown;
                l.MouseUp += TitleUp;
                Asset la = new Asset(l, "Title", true);
                Label ll = new Label();
                ll.Size = new Size(20, 20);
                ll.Location = new Point(480, 0);
                ll.Font = new Font(ll.Font, FontStyle.Bold);
                ll.ForeColor = Color.White;
                ll.BackColor = Color.Black;
                ll.TextAlign = ContentAlignment.MiddleCenter;
                ll.Text = "X";
                ll.MouseEnter += CloseEnter;
                ll.MouseLeave += CloseLeave;
                ll.Click += Close;
                Asset lla = new Asset(ll, "Close");
                Scene ss = new Scene("Welcome");
                Label lll = new Label();
                lll.Size = new Size(450, 25);
                lll.Location = new Point(25, 135);
                lll.ForeColor = Color.White;
                lll.BackColor = Color.Black;
                lll.Font = new Font(FontFamily.GenericSerif, 13, FontStyle.Bold | FontStyle.Italic);
                lll.TextAlign = ContentAlignment.MiddleCenter;
                lll.Text = "Welcome to Aura, bringed to you by Iris Technologies";
                Asset llla = new Asset(lll, "WMsg");
                ss.AddAsset("WMsg");
                Label llll = new Label();
                llll.Size = new Size(100, 25);
                llll.Location = new Point(200, 220);
                llll.ForeColor = Color.White;
                llll.BackColor = Color.Black;
                llll.Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);
                llll.TextAlign = ContentAlignment.MiddleCenter;
                llll.Text = "Next";
                llll.MouseEnter += NextToOutputFolderEnter;
                llll.MouseLeave += NextToOutputFolderLeave;
                llll.Click += NextToOutputFolder;
                Asset lllla = new Asset(llll, "NextOutputFolder");
                ss.AddAsset("NextOutputFolder");
                Scene.Register(ss);
                Database.Aura.MinimumSize = new Size(500, 300);
                Database.Aura.Size = new Size(500, 300);
                Utilities.CenterForm();
            }
        }

        internal static void Init()
        {
            Database.Aura.Opacity = 0;
            if (!Utilities.RunningAsAdmin())
                Utilities.Error("Run this app as administrator!");
            Database.AuraDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", ""));
            if (!File.Exists(Database.AuraDirectory + "\\Microsoft.WindowsAPICodePack.dll"))
                Utilities.Error("Microsoft dependency file(" + Database.AuraDirectory + "\\Microsoft.WindowsAPICodePack.dll) was not found!");
            if (!File.Exists(Database.AuraDirectory + "\\Microsoft.WindowsAPICodePack.Shell.dll"))
                Utilities.Error("Microsoft dependency file(" + Database.AuraDirectory + "\\Microsoft.WindowsAPICodePack.Shell.dll) was not found!");
            if (!Directory.Exists(Database.AuraDirectory + "\\Resources"))
                Utilities.Error("Resources folder(" + Database.AuraDirectory + "\\Resources) was not found!");
            Database.Aura.AllowDrop = true;
            Database.Aura.AllowTransparency = true;
            Database.Aura.AutoScroll = true;
            Database.Aura.AutoValidate = AutoValidate.EnableAllowFocusChange;
            Database.Aura.BackColor = Color.Black;
            Database.Aura.CausesValidation = true;
            Database.Aura.ControlBox = false;
            Database.Aura.FormBorderStyle = FormBorderStyle.None;
            Database.Aura.KeyPreview = true;
            Database.Aura.StartPosition = FormStartPosition.Manual;
            Database.Aura.WindowState = FormWindowState.Normal;
            Database.Aura.Size = new Size(400, 300);
            Utilities.CenterForm();
            Database.Aura.MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            if (!File.Exists(Database.AuraDirectory + "\\Resources\\Iris.ico"))
                Utilities.Error("Iris icon file(" + Database.AuraDirectory + "\\Resources\\Iris.ico) was not found!");
            else
                Database.Aura.Icon = Icon.ExtractAssociatedIcon(Database.AuraDirectory + "\\Resources\\Iris.ico");
            t = new Timer();
            t.Interval = 5;
            t.Tick += AuraOpacity;
            t.Start();
            Scene s = new Scene("SplashScreen");
            Label l = new Label();
            l.Size = new Size(20, 20);
            l.Location = new Point(150, 90);
            l.TextAlign = ContentAlignment.MiddleCenter;
            l.Font = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold);
            l.ForeColor = Color.IndianRed;
            l.Text = "A";
            Asset a = new Asset(l, "SplashScreen_1");
            s.AddAsset("SplashScreen_1");
            Label ll = new Label();
            ll.Size = new Size(20, 20);
            ll.Location = new Point(230, 90);
            ll.TextAlign = ContentAlignment.MiddleCenter;
            ll.Font = new Font(FontFamily.GenericSansSerif, 20, FontStyle.Bold);
            ll.ForeColor = Color.DeepSkyBlue;
            ll.Text = "A";
            Asset la = new Asset(ll, "SplashScreen_2");
            s.AddAsset("SplashScreen_2");
            Label lll = new Label();
            lll.Size = new Size(60, 20);
            lll.Location = new Point(170, 92);
            lll.TextAlign = ContentAlignment.MiddleCenter;
            lll.Font = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Bold);
            lll.ForeColor = Color.MediumOrchid;
            lll.Text = "U  R";
            Asset lla = new Asset(lll, "SplashScreen_3");
            s.AddAsset("SplashScreen_3");
            Label llll = new Label();
            llll.Size = new Size(200, 20);
            llll.Location = new Point(110, 200);
            llll.TextAlign = ContentAlignment.MiddleCenter;
            llll.Font = new Font(FontFamily.GenericSerif, 11, FontStyle.Bold);
            llll.ForeColor = Color.White;
            llll.Text = "by Iris Technologies";
            Asset llla = new Asset(llll, "IT");
            s.AddAsset("IT");
            Scene.Register(s);
        }
    }
}