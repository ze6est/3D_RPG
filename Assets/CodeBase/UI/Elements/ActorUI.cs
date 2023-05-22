using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;

        private IHealth _playerHealth;

        private void Start()
        {
            IHealth health = GetComponent<IHealth>();

            if (health != null)
                Construct(health);
        }

        private void OnDestroy()
        {
            if (_playerHealth != null)
                _playerHealth.HealthChanged -= OnHealthChanged;
        }

        public void Construct(IHealth playerHealth)
        {
            _playerHealth = playerHealth;

            _playerHealth.HealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            UpdateHPBar();
        }

        private void UpdateHPBar()
        {
            _hpBar.SetValue(_playerHealth.Current, _playerHealth.Max);
        }
    }
}