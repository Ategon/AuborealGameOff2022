
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;

public class Boat : Interactable
{
    private bool triggered = false;
    
    [SerializeField] private string navigationSceneName;

    [SerializeField] private Transform[] exitObjects;
    
    protected override bool Interact()
    {
        if (triggered) return false;
        triggered = true;
        for (int i = 0; i < exitObjects.Length; i++)
        {
            exitObjects[i].DOLocalMove(Vector3.zero, 2f).SetEase(Ease.OutQuad);
        }
        StartCoroutine(SwapScene());
        return true;
    }

    IEnumerator SwapScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(navigationSceneName);
    }
}
