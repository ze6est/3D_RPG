using CodeBase.Player;
using UnityEngine;

namespace CodeBase.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HpBar _hpBar;

        private PlayerHealth _playerHealth;

        private void OnDestroy() =>
            _playerHealth.HealthChanged -= OnHealthChanged;

        public void Construct(PlayerHealth playerHealth)
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