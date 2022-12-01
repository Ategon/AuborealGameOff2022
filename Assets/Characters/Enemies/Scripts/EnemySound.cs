using Assets.Audio;
using Assets.Audio.Events;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.AI;

public class EnemySound : MonoBehaviour
{
    [SerializeField] private EventReference attackSound;
    [SerializeField] private EventReference dieSound;
    [SerializeField] private EventReference footstepSound;
    [SerializeField] private EventReference hurtSound;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private float timeBetweenFootsteps;
    private float timeSinceLastFootstep;
    [HideInInspector] public bool isWalking;
    private bool isOnGrass;
    void Update()
    {
        if (navMeshAgent)
        {
            if (navMeshAgent.velocity.magnitude > 0.1f)
            {
                timeSinceLastFootstep += Time.deltaTime;
                if (timeSinceLastFootstep > timeBetweenFootsteps)
                {
                    if (isOnGrass)
                    {
                        FootstepSound("grass");
                    }
                    else
                    {
                        FootstepSound("sand");
                    }
                    timeSinceLastFootstep = 0;
                }
            }
            else
            {
                timeSinceLastFootstep = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14)
        {
            isOnGrass = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14)
        {
            isOnGrass = false;
        }
    }

    private void CreateSound(EventReference fmodRef)
    {
        EventInstance fmodInst = RuntimeManager.CreateInstance(fmodRef);
        fmodInst.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform));
        fmodInst.start();
    }
    public void AttackSound()
    {
        CreateSound(attackSound);
    }
    public void HurtSound()
    {
        CreateSound(hurtSound);
    }
    public void DieSound()
    {
        CreateSound(dieSound);
    }
    public void FootstepSound(string labelValue)
    {
        EventInstance fmodInst = RuntimeManager.CreateInstance(footstepSound);
        fmodInst.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform));
        fmodInst.setParameterByNameWithLabel("footsteps", labelValue);
        fmodInst.start();
    }
}
