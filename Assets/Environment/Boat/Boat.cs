
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boat : Interactable
{
    [SerializeField] private SceneAsset navigationScene;
    protected override bool Interact()
    {
        SceneManager.LoadScene(navigationScene.name);
        return true;
    }
}
