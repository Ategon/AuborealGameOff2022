
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boat : Interactable
{
    [SerializeField] private SceneAsset navigationScene;
    protected override void Interact()
    {
        SceneManager.LoadScene(navigationScene.name);
    }
}
