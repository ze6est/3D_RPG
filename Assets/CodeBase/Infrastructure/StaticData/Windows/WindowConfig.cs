using CodeBase.UI.StaticData;
using CodeBase.UI.Windows;
using System;

namespace CodeBase.Infrastructure.StaticData.Windows
{
    [Serializable]
    public class WindowConfig
    {
        public WindowId WindowId;
        public WindowBase Prefab;
    }
}