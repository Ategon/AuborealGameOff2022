using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string loadedSceneName;

    public virtual void ChangeScene()
    {
        SceneManager.LoadSceneAsync(loadedSceneName);
    }
}
