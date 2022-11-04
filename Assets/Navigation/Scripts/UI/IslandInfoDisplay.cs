using System.Collections;
using System.Collections.Generic;
using Assets.EventSystem;
using Assets.Navigation;
using TMPro;
using UnityEngine;

public class IslandInfoDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDisplay;
    [SerializeField] private IslandClickedEvent islandClickedEvent;
    [SerializeField] private IslandMouseEnter islandMouseEnter;
    [SerializeField] private IslandMouseExit islandMouseExit;
    private void OnEnable()
    {
        islandClickedEvent.AddListener(OnIslandClick);
        islandMouseEnter.AddListener(OnIslandEnter);
        islandMouseExit.AddListener(OnIslandExit);
    }
    private void OnDisable()
    {
        islandClickedEvent.RemoveListener(OnIslandClick);
        islandMouseEnter.RemoveListener(OnIslandEnter);
        islandMouseExit.RemoveListener(OnIslandExit);
    }
    public void OnIslandEnter(object sender, EventParameters arg2)
    {
        Island island = sender as Island;
        textDisplay.text = "Island name: " + island.islandName + "\n";
        int thirstCost = island.GetThirstCostFromDocked();
        if (thirstCost != 0)
        {
            textDisplay.text += "Thirst Cost: " + island.GetThirstCostFromDocked() + "\n";
            textDisplay.text += "Click to travel";
        }
    }
    public void OnIslandExit(object sender, EventParameters arg2)
    {
        textDisplay.text = "";
    }
    public void OnIslandClick(object sender, EventParameters arg2)
    {
        textDisplay.text = "";
    }

}
