//Copyright 2019 Iris Technologies, All Rights Reserved
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AuraEngine
{
    internal class Asset
    {
        private Control Entity;

        private string Name;

        private bool MoveFormWithIt = false;

        private List<Asset> Childrens = new List<Asset>();

        internal Asset(Control entity, string name, bool MoveForm = false)
        {
            Entity = entity;
            Name = name;
            MoveFormWithIt = MoveForm;
            Database.Aura.Controls.Add(Entity);
            Database.Assets.Add(this);
        }

        internal Control GetEntity()
        {
            return Entity;
        }

        internal void Move(Asset another)
        {
            Entity = another.Entity;
            Name = another.Name;
        }

        internal void Move(Control NewEntity)
        {
            Entity = NewEntity;
        }

        internal void Move(string NewName)
        {
            Name = NewName;
        }

        internal void AddChildren(Asset children)
        {
            Childrens.Add(children);
        }

        internal void RemoveChildren(Asset children)
        {
            Childrens.Remove(children);
        }

        internal void RemoveChildren(string children)
        {
            foreach (Asset child in Childrens)
                if (child.Name == children)
                {
                    Childrens.Remove(child);
                    break;
                }
        }

        internal void UpdatePosition(Point CursorPos)
        {
            if (MoveFormWithIt)
            {
                Point ActualCurPos = Utilities.GetCursorPosition();
                Point Rest = new Point(ActualCurPos.X - CursorPos.X, ActualCurPos.Y - CursorPos.Y);
                Database.Aura.Location = new Point(Database.Aura.Location.X + Rest.X, Database.Aura.Location.Y + Rest.Y);
            }
        }

        internal void Front()
        {
            Entity.BringToFront();
        }

        internal void Back()
        {
            Entity.SendToBack();
        }

        internal static Asset FindByName(string name)
        {
            foreach (Asset a in Database.Assets)
                if (a.Name == name)
                    return a;
            return null;
        }

        internal static void RemoveByName(string name)
        {
            foreach (Asset a in Database.Assets)
                if (a.Name == name)
                    Database.Aura.Controls.Remove(a.Entity);
        }

        internal static void Clear()
        {
            Database.Assets.Clear();
            Database.Aura.Controls.Clear();
        }

        internal static void Show(string AssetName)
        {
            foreach (Asset a in Database.Assets)
                if (a.Name == AssetName)
                {
                    a.GetEntity().Visible = true;
                    break;
                }
        }

        internal static void Hide(string AssetName)
        {
            foreach (Asset a in Database.Assets)
                if (a.Name == AssetName)
                {
                    a.GetEntity().Visible = false;
                    break;
                }
        }
    }
}