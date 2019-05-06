//Copyright 2019 Iris Technologies, All Rights Reserved
using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Principal;
using System.Security.AccessControl;
using System.Runtime.InteropServices;

namespace AuraEngine
{
    internal static class Utilities
    {
        internal static bool RunningAsAdmin()
        {
            bool b;
            using (WindowsIdentity id = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal pr = new WindowsPrincipal(id);
                b = pr.IsInRole(WindowsBuiltInRole.Administrator);
            }
            return b;
        }

        internal static void Info(string message)
        {
            MessageBox.Show(message, "Iris Info!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        internal static void Error(string message)
        {
            MessageBox.Show(message, "Iris Error!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            Environment.Exit(0);
        }

        internal static Point CenterOnForm(Size s)
        {
            return new Point(Database.Aura.Size.Width / 2 - s.Width / 2, Database.Aura.Size.Height / 2 - s.Height / 2);
        }

        internal static void CenterForm()
        {
            Database.Aura.Location = new Point((int)(Screen.PrimaryScreen.Bounds.Width * 0.5f - (Database.Aura.Size.Width * 0.5f)), (int)(Screen.PrimaryScreen.Bounds.Height * 0.5f - (Database.Aura.Size.Height * 0.5f)));            
        }

        internal static int BytesToInt(byte[] b)
        {
            return BitConverter.ToInt32(b, 0);
        }

        internal static byte[] IntToBytes(int i)
        {
            return BitConverter.GetBytes(i);
        }

        internal static void NewFolder(string folder)
        {
            bool exists = Directory.Exists(folder);
            if (!exists)
            {
                DirectoryInfo di = Directory.CreateDirectory(folder);
            }
            DirectoryInfo dInfo = new DirectoryInfo(folder);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
            dInfo.SetAccessControl(dSecurity);
        }

        private static void NewFile(string file)
        {
            if (!File.Exists(file))
            {
                FileStream fn = new FileStream(file, FileMode.CreateNew);
                fn.Close();
            }
        }

        internal static string ReadFile(string file)
        {
            FileStream fr = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fr);
            string ret = sr.ReadToEnd();
            sr.Close();
            fr.Close();
            return ret;
        }

        internal static string ReadBinary(string file)
        {
            BinaryReader br = new BinaryReader(new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            byte[] data;
            using (var ms = new MemoryStream())
            {
                br.BaseStream.Position = 0;
                br.BaseStream.CopyTo(ms);
                data = ms.ToArray();
            }
            br.Close();
            return Convert.ToBase64String(data);
        }

        internal static void WriteFile(string file, string content)
        {
            if (File.Exists(file))
                File.Delete(file);
            NewFile(file);
            FileStream fw = new FileStream(file, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
            StreamWriter sw = new StreamWriter(fw);
            sw.Write(content);
            sw.Close();
            fw.Close();
        }

        internal static void WriteBinary(string file, string content)
        {
            if (File.Exists(file))
                File.Delete(file);
            NewFile(file);
            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
            BinaryWriter sw = new BinaryWriter(fs);
            sw.Write(Convert.FromBase64String(content));
            sw.Close();
            fs.Close();
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            private int X;
            private int Y;

            public static implicit operator Point(POINT p)
            {
                return new Point(p.X, p.Y);
            }
        }

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT sp);

        internal static Point GetCursorPosition()
        {
            POINT lp;
            GetCursorPos(out lp);
            return lp;
        }

        internal static int UpdateDownloadPercent()
        {
            if (Database.NetworkExpectedSize != 0 && Database.NetworkSize != 0)
                return (int)Math.Round((double)(Database.NetworkSize / ((double)Database.NetworkExpectedSize / (double)100)));
            return 0;
        }

        internal static int UpdateDownloadBarSize()
        {
            return 4 * UpdateDownloadPercent();
        }
    }
}