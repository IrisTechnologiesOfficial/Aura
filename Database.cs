//Copyright 2019 Iris Technologies, All Rights Reserved
using System.Windows.Forms;
using System.Collections.Generic;

namespace AuraEngine
{
    internal static class Database
    {
        internal static Form Aura;

        internal static List<Asset> Assets = new List<Asset>();

        internal static List<Scene> Scenes = new List<Scene>();

        internal static string ActualScene = "Welcome", OutputFolder = "", InstallationFile, AuraDirectory;

        internal static bool Iris = false, Nerve = false;

        volatile internal static int NetworkExpectedSize, NetworkSize;

        volatile internal static bool ClientDone = true, NetworkDone = false, Downloaded = false;

        volatile internal static string NetworkResult = "", DownloadFile = "";
    }
}