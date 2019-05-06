//Copyright 2019 Iris Technologies, All Rights Reserved
using System.Windows.Forms;

namespace AuraEngine
{
    public static class Engine
    {
        public static void Init(Form f)
        {
            Database.Aura = f;
            Initialization.Init();
        }
    }
}