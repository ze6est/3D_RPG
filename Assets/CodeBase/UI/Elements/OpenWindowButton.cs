using CodeBase.UI.Services;
using CodeBase.UI.StaticData;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class OpenWindowButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private WindowId _windowId;

        private IWindowService _windowServise;

        public void Construct(IWindowService windowServise) => 
            _windowServise = windowServise;

        private void Awake() => 
            _button.onClick.AddListener(Open);

        private void Open() => 
            _windowServise.Open(_windowId);
    }
}