using System.Collections;
using System.Collections.Generic;
using Assets.Enemies;
using UnityEngine;

public class AggroZone : MonoBehaviour
{
    [SerializeField] private BossBombPattern bossBombPattern;
    [SerializeField] private BossHealthUI bossHealthUI;
    [SerializeField] private NumAggroedEnemyChangeEvent numAggroedEnemyChangeEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            numAggroedEnemyChangeEvent.Raise(this, new NumEnemyChangeEventParameters(1));
            bossBombPattern.isAggroed = true;
            bossHealthUI.ShowHealth();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            numAggroedEnemyChangeEvent.Raise(this, new NumEnemyChangeEventParameters(-1));
            bossBombPattern.isAggroed = false;
            bossHealthUI.HideHealth();
        }
    }
}
