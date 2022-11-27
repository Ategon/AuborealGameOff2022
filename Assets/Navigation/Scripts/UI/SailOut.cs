using UnityEngine.SceneManagement;
using Assets.Player.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Navigation
{
    public class SailOut : MonoBehaviour
    {
        public string sceneName;
        [SerializeField] private InventoryController inventoryController;
        [SerializeField] private Button sailOutButton;
        [SerializeField] private GameObject sextantMissingMessage;

        private void Awake()
        {
            sailOutButton.interactable = inventoryController.sextantOwned;
            sextantMissingMessage.SetActive(!inventoryController.sextantOwned);
        }

        public void OnPressSailOut()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
