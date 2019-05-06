//Copyright 2019 Iris Technologies, All Rights Reserved
using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace AuraEngine
{
    internal static class Installation
    {
        private static bool InstallationLoaded = false;
        private static Stopwatch st = new Stopwatch();
        private static Timer t = null;
        private static byte Mode = 0;
        private static int duh = 0;
        private static int duh2 = 0;
        internal static bool InfoLoaded = false;

        private static void UpdateInstall()
        {
            Asset.FindByName("FileDownloading").GetEntity().Text = Database.DownloadFile + "...";
            Asset.FindByName("DownloadKB").GetEntity().Text = Database.NetworkSize.ToString() + "kb / " + Database.NetworkExpectedSize.ToString() + "kb";
            Asset.FindByName("DownloadPercent").GetEntity().Text = Utilities.UpdateDownloadPercent().ToString() + "%";
            Control c = Asset.FindByName("DownloadBar").GetEntity();
            c.Size = new Size(Utilities.UpdateDownloadBarSize(), 79);
            c.BackColor = Color.FromArgb(255, duh2, duh2 / 2, duh2 / 3 + duh + 5);
        }

        private static void UpdateFiles(string Download, string Install)
        {
            Database.DownloadFile = "Downloading " + Download;
            Database.InstallationFile = Database.OutputFolder + "\\" + Install;
        }

        private static void Clean()
        {
            Database.ClientDone = true;
            duh = 0;
            duh2 = 0;
        }

        private static void Install()
        {
            Utilities.WriteBinary(Database.InstallationFile, Database.NetworkResult);
            Client.Flush();
            Mode++;
        }

        private static void UpdateBoth(object s, EventArgs e)
        {
            if(!Database.Downloaded)
            {
                if (st.ElapsedMilliseconds <= 60000)
                {
                    if (duh == 3)
                        duh = 0;
                    if (duh2 == 255)
                        duh2 = 0;
                    UpdateInstall();
                    if (Database.ClientDone)
                        switch (Mode)
                        {
                            case 0:
                                UpdateFiles("Iris", "Iris.exe");
                                Client.Send(Operations.RequestFile + "????????????");
                                break;
                            case 1:
                                UpdateFiles("Nerve", "Nerve.exe");
                                Client.Send(Operations.RequestFile + "???");
                                break;
                            case 2:
                                UpdateFiles("Eye", "Eye.exe");
                                Client.Send(Operations.RequestFile + "???");
                                break;
                            case 3:
                                UpdateFiles("Iris Engine", "IrisEngine.dll");
                                Client.Send(Operations.RequestFile + "???");
                                break;
                            case 4:
                                UpdateFiles("Nerve Engine", "NerveEngine.dll");
                                Client.Send(Operations.RequestFile + "???");
                                break;
                            case 5:
                                UpdateFiles("Maximize Image", "Resources\\Maximize.png");
                                Client.Send(Operations.RequestFile + "??????");
                                break;
                            case 6:
                                UpdateFiles("Minimize Image", "Resources\\Minimize.png");
                                Client.Send(Operations.RequestFile + "???");
                                break;
                            case 7:
                                UpdateFiles("SendToBackground Image", "Resources\\SendToBackground.png");
                                Client.Send(Operations.RequestFile + "???");
                                break;
                            case 8:
                                Database.Downloaded = true;
                                break;
                        }
                    if (Database.NetworkDone)
                        Install();
                    duh++;
                    duh2++;
                }
                else
                {
                    if (!InfoLoaded)
                    {
                        InfoLoaded = true;
                        Utilities.Info("You run out of time downloading the content!");
                    }
                    Clean();
                    Client.Flush();
                    Scene.Hide("Installation");
                    st.Reset();
                    t.Stop();
                    Retry.Draw();
                }
            }
            else
            {
                Client.Flush();
                Scene.Hide("Installation");
                Utilities.WriteBinary(Database.OutputFolder + "\\Resources\\Iris.ico", Utilities.ReadBinary(Database.AuraDirectory + "\\Resources\\Iris.ico"));
                Utilities.WriteBinary(Database.OutputFolder + "\\Resources\\Nerve.ico", Utilities.ReadBinary(Database.AuraDirectory + "\\Resources\\Iris.ico"));
                Utilities.WriteBinary(Database.OutputFolder + "\\Microsoft.WindowsAPICodePack.dll", Utilities.ReadBinary(Database.AuraDirectory + "\\Microsoft.WindowsAPICodePack.dll"));
                Utilities.WriteBinary(Database.OutputFolder + "\\Microsoft.WindowsAPICodePack.Shell.dll", Utilities.ReadBinary(Database.AuraDirectory + "\\Microsoft.WindowsAPICodePack.Shell.dll"));
                st.Stop();
                t.Stop();
                Finish.Draw();
            }
        }

        private static void UpdateIris(object s, EventArgs e)
        {
            if (!Database.Downloaded)
            {
                if (st.ElapsedMilliseconds <= 60000)
                {
                    if (duh == 3)
                        duh = 0;
                    if (duh2 == 255)
                        duh2 = 0;
                    UpdateInstall();
                    if (Database.ClientDone)
                        switch (Mode)
                        {
                            case 0:
                                UpdateFiles("Iris", "Iris.exe");
                                Client.Send(Operations.RequestFile + "???");
                                break;
                            case 1:
                                UpdateFiles("Iris Engine", "IrisEngine.dll");
                                Client.Send(Operations.RequestFile + "???");
                                break;
                            case 2:
                                UpdateFiles("Maximize Image", "Resources\\Maximize.png");
                                Client.Send(Operations.RequestFile + "???");
                                break;
                            case 3:
                                UpdateFiles("Minimize Image", "Resources\\Minimize.png");
                                Client.Send(Operations.RequestFile + "???");
                                break;
                            case 4:
                                UpdateFiles("SendToBackground Image", "Resources\\SendToBackground.png");
                                Client.Send(Operations.RequestFile + "???");
                                break;
                            case 5:
                                Database.Downloaded = true;
                                break;
                        }
                    if (Database.NetworkDone)
                        Install();
                    duh++;
                    duh2++;
                }
                else
                {
                    if (!InfoLoaded)
                    {
                        InfoLoaded = true;
                        Utilities.Info("You run out of time downloading the content!");
                    }
                    Clean();
                    Client.Flush();
                    Scene.Hide("Installation");
                    st.Reset();
                    t.Stop();
                    Retry.Draw();
                }
            }
            else
            {
                Client.Flush();
                Scene.Hide("Installation");
                Utilities.WriteBinary(Database.OutputFolder + "\\Resources\\Iris.ico", Utilities.ReadBinary(Database.AuraDirectory + "\\Resources\\Iris.ico"));
                Utilities.WriteBinary(Database.OutputFolder + "\\Microsoft.WindowsAPICodePack.dll", Utilities.ReadBinary(Database.AuraDirectory + "\\Microsoft.WindowsAPICodePack.dll"));
                Utilities.WriteBinary(Database.OutputFolder + "\\Microsoft.WindowsAPICodePack.Shell.dll", Utilities.ReadBinary(Database.AuraDirectory + "\\Microsoft.WindowsAPICodePack.Shell.dll"));
                st.Stop();
                t.Stop();
                Finish.Draw();
            }
        }

        private static void UpdateNerve(object s, EventArgs e)
        {
            if (!Database.Downloaded)
            {
                if (st.ElapsedMilliseconds <= 60000)
                {
                    if (duh == 3)
                        duh = 0;
                    if (duh2 == 255)
                        duh2 = 0;
                    UpdateInstall();
                    if (Database.ClientDone)
                        switch (Mode)
                        {
                            case 0:
                                UpdateFiles("Nerve", "Nerve.exe");
                                Client.Send(Operations.RequestFile + "???");
                                break;
                            case 1:
                                UpdateFiles("Eye", "Eye.exe");
                                Client.Send(Operations.RequestFile + "???");
                                break;
                            case 2:
                                UpdateFiles("Nerve Engine", "NerveEngine.dll");
                                Client.Send(Operations.RequestFile + "???");
                                break;
                            case 3:
                                Database.Downloaded = true;
                                break;
                        }
                    if (Database.NetworkDone)
                        Install();
                    duh++;
                    duh2++;
                }
                else
                {
                    if (!InfoLoaded)
                    {
                        InfoLoaded = true;
                        Utilities.Info("You run out of time downloading the content!");
                    }
                    Clean();
                    Client.Flush();
                    Scene.Hide("Installation");
                    st.Reset();
                    t.Stop();
                    Retry.Draw();
                }
            }
            else
            {
                Client.Flush();
                Scene.Hide("Installation");
                Utilities.WriteBinary(Database.OutputFolder + "\\Resources\\Nerve.ico", Utilities.ReadBinary(Database.AuraDirectory + "\\Resources\\Iris.ico"));
                Utilities.WriteBinary(Database.OutputFolder + "\\Microsoft.WindowsAPICodePack.dll", Utilities.ReadBinary(Database.AuraDirectory + "\\Microsoft.WindowsAPICodePack.dll"));
                Utilities.WriteBinary(Database.OutputFolder + "\\Microsoft.WindowsAPICodePack.Shell.dll", Utilities.ReadBinary(Database.AuraDirectory + "\\Microsoft.WindowsAPICodePack.Shell.dll"));
                st.Stop();
                t.Stop();
                Finish.Draw();
            }
        }

        internal static void Draw()
        {
            Database.ActualScene = "Installation";
            if (!InstallationLoaded)
            {
                InstallationLoaded = true;
                Scene installlation = new Scene("Installation");
                Label l = new Label();
                Label lll = new Label();
                lll.Size = new Size(300, 20);
                lll.Location = new Point(100, 200);
                lll.Font = new Font(FontFamily.GenericSansSerif, 12f, FontStyle.Bold);
                lll.TextAlign = ContentAlignment.MiddleCenter;
                lll.ForeColor = Color.White;
                Asset llla = new Asset(lll, "FileDownloading");
                installlation.AddAsset("FileDownloading");
                Label llllll = new Label();
                llllll.Size = new Size(100, 20);
                llllll.Location = new Point(410, 200);
                llllll.TextAlign = ContentAlignment.MiddleCenter;
                llllll.ForeColor = Color.Red;
                Asset lllllla = new Asset(llllll, "DownloadKB");
                installlation.AddAsset("DownloadKB");
                Label lllllll = new Label();
                lllllll.Size = new Size(30, 20);
                lllllll.Location = new Point(460, 130);
                lllllll.TextAlign = ContentAlignment.MiddleCenter;
                lllllll.ForeColor = Color.Blue;
                Asset llllllla = new Asset(lllllll, "DownloadPercent");
                installlation.AddAsset("DownloadPercent");
                Label llll = new Label();
                llll.Size = new Size(400, 80);
                llll.Location = new Point(50, 100);
                llll.BorderStyle = BorderStyle.Fixed3D;
                llll.BackColor = Color.White;
                Asset lllla = new Asset(llll, "FullDownloadBar");
                installlation.AddAsset("FullDownloadBar");
                Label ll = new Label();
                ll.Size = new Size(1, 79);
                ll.Location = new Point(50, 100);
                ll.BorderStyle = BorderStyle.None;
                ll.BackColor = Color.PowderBlue;
                Asset lla = new Asset(ll, "DownloadBar");
                lla.Front();
                installlation.AddAsset("DownloadBar");
                Scene.Register(installlation);
                t = new Timer();
                t.Interval = 5;
                if (Database.Iris && Database.Nerve)
                    t.Tick += UpdateBoth;
                else if (Database.Iris)
                    t.Tick += UpdateIris;
                else
                    t.Tick += UpdateNerve;
            }
            else
                Scene.Show("Installation");
            if (Client.CheckConnection())
            {
                st.Start();
                bool OutTime = true;
                Client.Send(Operations.CheckPing);
                while (st.ElapsedMilliseconds < 5000)
                    if(Database.NetworkResult == "1")
                    {
                        OutTime = false;
                        break;
                    }
                if (OutTime)
                {
                    Utilities.Info("You run out of time trying to connect with the server!");
                    Clean();
                    Client.Flush();
                    Scene.Hide("Installation");
                    st.Reset();
                    Retry.Draw();
                }
                else
                {
                    if (Database.OutputFolder != "")
                        Utilities.NewFolder(Database.OutputFolder);
                    Utilities.NewFolder(Database.OutputFolder + "\\Resources");
                    Client.Flush();
                    t.Start();
                    st.Restart();
                }
            }
            else
                Utilities.Info("You are not connected to a network or your network is having troubles!");
        }
    }
}
