using CodeBase.Data;
using TMPro;
using UnityEngine;

namespace CodeBase.Enemy
{
    public class LootPiece : MonoBehaviour
    {
        [SerializeField] private GameObject _coin;
        [SerializeField] private GameObject _pickupFxPrefab;
        [SerializeField] private TextMeshPro _lootText;
        [SerializeField] private GameObject _pickupPopup;

        private Loot _loot;
        private bool _picked;
        private WorldData _worldData;

        public void Construct(WorldData worldData) => 
            _worldData = worldData;

        public void Initialize(Loot loot) => 
            _loot = loot;

        private void OnTriggerEnter(Collider other) => 
            Pickup();

        private void Pickup()
        {
            if (_picked)
                return;

            _picked = true;

            UpdateWorldData();
            HideCoin();
            PlayPickupFx();
            ShowText();

            Destroy(gameObject, 1.5f);
        }

        private void UpdateWorldData() => 
            _worldData.LootData.Collect(_loot);

        private void HideCoin() => 
            _coin.SetActive(false);

        private void PlayPickupFx()
        {
            GameObject pickupFx = Instantiate(_pickupFxPrefab, transform.position, Quaternion.identity);
            pickupFx.transform.rotation = Quaternion.Euler(-90, 0, 0);
        }

        private void ShowText()
        {
            _lootText.text = $"{_loot.Value}";
            _pickupPopup.SetActive(true);
        }
    }
}