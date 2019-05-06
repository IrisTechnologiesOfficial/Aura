//Copyright 2019 Iris Technologies, All Rights Reserved
using System.Drawing;
using System.Collections.Generic;

namespace AuraEngine
{
    internal class Scene
    {
        private List<string> LocalAssets = new List<string>();

        private string SN;

        private Size MainSize;

        private Point MainLocation;

        internal Scene(string SceneName)
        {
            SN = SceneName;
        }

        internal void AddAsset(string asset)
        {
            bool AlreadyExists = false;
            foreach (string s in LocalAssets)
                if (s == asset)
                    AlreadyExists = true;
            if (!AlreadyExists)
                LocalAssets.Add(asset);
        }

        internal void RemoveAsset(string asset)
        {
            bool AlreadyExists = false;
            foreach (string s in LocalAssets)
                if (s == asset)
                {
                    AlreadyExists = true;
                    break;
                }
            if (AlreadyExists)
                LocalAssets.Remove(asset);
        }

        internal static void Register(Scene s)
        {
            bool AlreadyExists = false;
            foreach (Scene ss in Database.Scenes)
                if (ss.SN == s.SN)
                    AlreadyExists = true;
            if (!AlreadyExists)
                Database.Scenes.Add(s);
            s.MainLocation = Database.Aura.Location;
            s.MainSize = Database.Aura.Size;
            Database.Aura.MinimumSize = Database.Aura.Size;
        }

        internal static Scene Find(string SceneName)
        {
            foreach (Scene s in Database.Scenes)
                if (s.SN == SceneName)
                    return s;
            return null;
        }

        internal static void Hide(string SceneName)
        {
            Scene s = Find(SceneName);
            if (s != null)
                foreach (string assetname in s.LocalAssets)
                    Asset.FindByName(assetname).GetEntity().Visible = false;
        }

        internal static void Show(string SceneName, bool AuraSizeUpdate = true)
        {
            Scene s = Find(SceneName);
            if (s != null)
            {
                foreach (string assetname in s.LocalAssets)
                    Asset.FindByName(assetname).GetEntity().Visible = true;
                if (AuraSizeUpdate)
                {
                    Database.Aura.MinimumSize = s.MainSize;
                    Database.Aura.Size = s.MainSize;
                    Database.Aura.Location = s.MainLocation;
                }
            }
        }

        internal static void Unregister(string SceneName)
        {
            Scene ret = Find(SceneName);
            if (ret != null)
                Database.Scenes.Remove(ret);
        }
    }
}