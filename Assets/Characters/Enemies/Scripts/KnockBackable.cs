using UnityEngine;
using UnityEngine.AI;

namespace Characters.Enemies
{
    public class KnockBackable : MonoBehaviour
    {
        [field: SerializeField] public float KnockTime { get; private set; }
        [field: SerializeField] public float KnockThurst { get; private set; }

        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            TryGetComponent(out _navMeshAgent);
        }

        public void Knock(Vector3 attackPosition)
        {
            // uncomment this for post effect or values to set
            // async void KnockTimer()
            // {
            //     await Task.Delay((int)KnockTime * 1000);
            //     // post effect or values here
            // }

            Vector3 distance = attackPosition - transform.position;
            Vector3 direction = distance.normalized;
            Vector3 thurstVector = direction * KnockThurst;
            _navMeshAgent.velocity = -thurstVector;

            // uncomment this for post effect or values to set
            // KnockTimer();
        }
    }
}