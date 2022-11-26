using System.Collections;
using System.Collections.Generic;
using Assets.Audio.Events;
using UnityEngine;

public class PlayerFootstepSound : MonoBehaviour
{
    [SerializeField] private PlayerFootstepEvent playerFootstepEvent;
    [SerializeField] private float timeBetweenFootsteps;
    private float timeSinceLastFootstep;
    [HideInInspector] public bool isWalking;
    private bool isOnGrass;
    void Update()
    {
        if (isWalking)
        {
            timeSinceLastFootstep += Time.deltaTime;
            if (timeSinceLastFootstep > timeBetweenFootsteps)
            {
                Debug.Log("Event being raised");
                FootstepEventParameters footstepEventParameters;
                if (isOnGrass)
                {
                    footstepEventParameters = new FootstepEventParameters("footsteps", "grass");
                }
                else
                {
                    footstepEventParameters = new FootstepEventParameters("footsteps", "sand");
                }
                playerFootstepEvent.Raise(this, footstepEventParameters);
                timeSinceLastFootstep = 0;
            }
        }
        else
        {
            timeSinceLastFootstep = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Grass")
        {
            isOnGrass = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Grass")
        {
            isOnGrass = false;
        }
    }
}
