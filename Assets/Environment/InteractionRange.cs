using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionRange : MonoBehaviour
{
    [SerializeField] private Interactable interactable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            interactable.PlayerEnterRange();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            interactable.PlayerExitRange();
        }
    }
}
