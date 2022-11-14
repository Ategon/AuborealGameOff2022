
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boat : Interactable
{
    [SerializeField] private string navigationSceneName;
    protected override bool Interact()
    {
        SceneManager.LoadScene(navigationSceneName);
        return true;
    }
}
