using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private SceneAsset loadedScene;

    public void ChangeScene()
    {
        SceneManager.LoadSceneAsync(loadedScene.name);
    }
}
