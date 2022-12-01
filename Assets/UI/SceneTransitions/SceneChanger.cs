using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string loadedSceneName;

    [SerializeField] private Transform[] exitObjects;

    public virtual void ChangeScene()
    {
        for (int i = 0; i < exitObjects.Length; i++)
        {
            exitObjects[i].DOLocalMove(Vector3.zero, 2f).SetEase(Ease.OutQuad);
        }
        StartCoroutine(SceneTransition());
    }

    IEnumerator SceneTransition()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(loadedSceneName);
    }
}
