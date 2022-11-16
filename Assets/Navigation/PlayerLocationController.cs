using UnityEngine;


namespace Assets.Navigation
{
    [CreateAssetMenu(fileName = nameof(PlayerLocationController), menuName = "ScriptableObjects/PlayerLocationController")]
    public class PlayerLocationController : ScriptableObject
    {
        public string islandName;
        [SerializeField] private string startingIslandName;

        public Island GetIsland()
        {
            Island[] islands = FindObjectsOfType<Island>();
            foreach (Island island in islands)
            {
                if (island.islandName == islandName)
                    {
                    return island;
                    }
            }
            Debug.LogWarning("An island with name " + islandName + " could not be found.");
            return FindObjectOfType<Island>();
        }

        public void NewGame()
        {
            islandName = startingIslandName;
        }
    }
}
