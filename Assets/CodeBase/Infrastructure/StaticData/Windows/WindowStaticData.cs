using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Windows
{
    [CreateAssetMenu(fileName = "WindowData", menuName = "StaticData/Window", order = 51)]
    public class WindowStaticData : ScriptableObject
    {
        public List<WindowConfig> Configs;
    }
}