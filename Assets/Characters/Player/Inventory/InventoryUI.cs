using Assets.EventSystem;
using UnityEngine;
using TMPro;

namespace Assets.Player.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private WoodChangedEvent woodChangedEvent;
        [SerializeField] private TreasureChangedEvent treasureChangedEvent;
        [SerializeField] private InventoryController inventoryController;
        [SerializeField] private TextMeshProUGUI woodCountText;
        [SerializeField] private TextMeshProUGUI treasureCountText;
        private void Start()
        {
            UpdateUI();
        }
        private void OnEnable()
        {
            woodChangedEvent.AddListener(OnWoodChanged);
            treasureChangedEvent.AddListener(OnTreasureChanged);
        }
        private void OnDisable()
        {
            woodChangedEvent.RemoveListener(OnWoodChanged);
            treasureChangedEvent.RemoveListener(OnTreasureChanged);
        }

        private void UpdateUI()
        {
            woodCountText.text = inventoryController.woodCount.ToString();
            treasureCountText.text = inventoryController.treasureCount.ToString();
        }
        private void OnWoodChanged(object sender, EventParameters arg2)
        {
            UpdateUI();
        }
        private void OnTreasureChanged(object sender, EventParameters arg2)
        {
            UpdateUI();
        }
    }
}
