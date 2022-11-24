using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroZone : MonoBehaviour
{
    [SerializeField] private BossBombPattern bossBombPattern;
    [SerializeField] private BossHealthUI bossHealthUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            bossBombPattern.isAggroed = true;
            bossHealthUI.ShowHealth();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            bossBombPattern.isAggroed = false;
            bossHealthUI.HideHealth();
        }
    }
}
