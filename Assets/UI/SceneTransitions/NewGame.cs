using Assets.Navigation;
using Assets.Player.Health;
using Assets.Player.Inventory;
using Assets.Player.Thirst;
using UnityEditor;
using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class NewGame : SceneChanger
{
    [SerializeField] private ThirstController thirstController;
    [SerializeField] private HealthController healthController;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private PlayerLocationController playerLocationController;
    [SerializeField] private Transform[] exitObjects;

    bool started = false;

    public override void ChangeScene()
    {
        if (started) return;
        started = true;
        for (int i = 0; i < exitObjects.Length; i++)
        {
            exitObjects[i].DOLocalMove(Vector3.zero, 2f).SetEase(Ease.OutQuad);
        }
        StartCoroutine(SceneTransition());
    }

    IEnumerator SceneTransition()
    {
        yield return new WaitForSeconds(2f);
        thirstController.NewGame();
        healthController.NewGame();
        inventoryController.NewGame();
        playerLocationController.NewGame();
        base.ChangeScene();
    }
}
