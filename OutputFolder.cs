//Copyright 2019 Iris Technologies, All Rights Reserved
using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace AuraEngine
{
    internal static class OutputFolder
    {
        private static bool OutputFolderLoaded = false;

        private static void OutputFolderBrowserEnter(object s, EventArgs e)
        {
            Control c = Asset.FindByName("OFBrowser").GetEntity();
            c.BackColor = Color.AliceBlue;
            c.ForeColor = Color.Black;
        }

        private static void OutputFolderBrowserLeave(object s, EventArgs e)
        {
            Control c = Asset.FindByName("OFBrowser").GetEntity();
            c.BackColor = Color.Black;
            c.ForeColor = Color.White;
        }

        private static void OutputFolderBrowser(object s, EventArgs e)
        {
            Scene.Hide("OFBrowser");
            using(CommonOpenFileDialog ofd = new CommonOpenFileDialog())
            {
                ofd.AddToMostRecentlyUsedList = true;
                ofd.AllowPropertyEditing = true;
                ofd.AllowNonFileSystemItems = true;
                ofd.IsFolderPicker = true;
                ofd.Multiselect = false;
                ofd.RestoreDirectory = true;
                ofd.ShowHiddenItems = true;
                ofd.ShowPlacesList = true;
                ofd.Title = "Select Aura Installation Output Folder...";
                if (ofd.ShowDialog() == CommonFileDialogResult.Ok)
                    Asset.FindByName("OF").GetEntity().Text = ofd.FileName;
            }
            Scene.Show("OFBrowser");
        }

        private static void NextSelectEnter(object s, EventArgs e)
        {
            Asset.FindByName("NextSelect").GetEntity().ForeColor = Color.Blue;
        }

        private static void NextSelectLeave(object s, EventArgs e)
        {
            Asset.FindByName("NextSelect").GetEntity().ForeColor = Color.White;
        }

        private static void NextSelect(object s, EventArgs e)
        {
            string OF = Asset.FindByName("OF").GetEntity().Text;
            if (OF == "")
                Utilities.Info("Output folder cannot be empty!");
            else
            {
                bool AlreadyExists = false;
                if (Directory.Exists(OF))
                    AlreadyExists = true;
                Utilities.NewFolder(OF);
                if (Directory.Exists(OF))
                {
                    if(!AlreadyExists)
                        Directory.Delete(OF);
                    Database.OutputFolder = OF;
                    Scene.Hide("OutputFolder");
                    Selection.Draw();
                }
                else
                    Utilities.Info("Selected output folder isn't a valid path!");
            }
        }

        internal static void Draw()
        {
            Database.ActualScene = "OutputFolder";
            if (!OutputFolderLoaded)
            {
                OutputFolderLoaded = true;
                Database.Aura.MinimumSize = new Size(500, 250);
                Database.Aura.Size = new Size(500, 250);
                Utilities.CenterForm();
                Scene s = new Scene("OutputFolder");
                Label l = new Label();
                l.Size = new Size(450, 25);
                l.Location = new Point(25, 100);
                l.ForeColor = Color.White;
                l.BackColor = Color.Black;
                l.Font = new Font(FontFamily.GenericSerif, 13, FontStyle.Bold | FontStyle.Italic);
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.Text = "Please, select the output folder";
                Asset la = new Asset(l, "OFMsg");
                s.AddAsset("OFMsg");
                TextBox t = new TextBox();
                t.Size = new Size(400, 25);
                t.Location = new Point(50, 150);
                t.BorderStyle = BorderStyle.None;
                t.Font = new Font(FontFamily.GenericSerif, 13, FontStyle.Bold | FontStyle.Italic);
                Asset ta = new Asset(t, "OF");
                s.AddAsset("OF");
                Label ll = new Label();
                ll.Size = new Size(30, 20);
                ll.Location = new Point(460, 152);
                ll.ForeColor = Color.White;
                ll.BackColor = Color.Black;
                ll.Font = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Bold | FontStyle.Italic);
                ll.TextAlign = ContentAlignment.MiddleCenter;
                ll.Text = "...";
                ll.MouseEnter += OutputFolderBrowserEnter;
                ll.MouseLeave += OutputFolderBrowserLeave;
                ll.Click += OutputFolderBrowser;
                Asset lla = new Asset(ll, "OFBrowser");
                s.AddAsset("OFBrowser");
                Label lll = new Label();
                lll.Size = new Size(100, 25);
                lll.Location = new Point(300, 200);
                lll.ForeColor = Color.White;
                lll.BackColor = Color.Black;
                lll.Font = new Font(FontFamily.GenericSansSerif, 13, FontStyle.Bold);
                lll.TextAlign = ContentAlignment.MiddleCenter;
                lll.Text = "Next";
                lll.MouseEnter += NextSelectEnter;
                lll.MouseLeave += NextSelectLeave;
                lll.Click += NextSelect;
                Asset llla = new Asset(lll, "NextSelect");
                s.AddAsset("NextSelect");
                Scene.Register(s);
            }
            else
                Scene.Show("OutputFolder");
        }
    }
}