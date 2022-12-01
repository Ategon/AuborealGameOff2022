using UnityEngine;

namespace Assets.Navigation
{
    public class Boat : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private new Rigidbody2D rigidbody;
        [SerializeField] private PlayerLocationController playerLocationController;
        private bool isTraveling;
        private DockPoint destination;

        private float bobAmount = 0.01f;
        private float bobSpeed = -1;

        private Vector3 startPos;

        private void Start()
        {
            startPos = transform.localPosition;
        }

        private void FixedUpdate()
        {
            if (!isTraveling) transform.position = new Vector3(transform.position.x, startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobAmount, transform.position.z);
        }

        private void Awake()
        {
            Island dockedIsland = playerLocationController.GetIsland();
            dockedIsland.InitializeBoat(this);
            transform.position = dockedIsland.dockPoint.transform.position;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isTraveling)
            {
                DockPoint dockPoint = collision.GetComponent<DockPoint>();
                if (dockPoint == destination)
                {
                    dockPoint.DockBoat();
                    Dock();
                }
            }
        }
        public void SetDestination(Island island)
        {
            isTraveling = true;
            destination = island.dockPoint;
            Vector3 dockPosition = island.dockPoint.transform.position;
            rigidbody.velocity = (dockPosition - transform.position).normalized * speed;
        }
        private void Dock()
        {
            startPos = new Vector3(startPos.x, transform.position.y, startPos.z);
            isTraveling = false;
            rigidbody.velocity = Vector3.zero;
        }
    }
}
