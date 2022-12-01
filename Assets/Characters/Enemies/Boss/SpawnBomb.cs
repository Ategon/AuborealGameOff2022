using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBomb : MonoBehaviour
{
    [SerializeField] private GameObject bombIndicatorPrefab;
    [SerializeField] private int bombDamage;
    [SerializeField] private float bombBlastRadius;
    [SerializeField] private float explosionDelay;

    public void CreateBomb(Vector2 location)
    {
        BombIndicator bombIndicator = Instantiate(bombIndicatorPrefab, location, Quaternion.identity).GetComponent<BombIndicator>();
        bombIndicator.explosionDelay = explosionDelay;
        bombIndicator.bombBlastRadius = bombBlastRadius;
        bombIndicator.bombDamage = bombDamage;

    }
}
