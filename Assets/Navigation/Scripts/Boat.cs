using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private Island startingIsland;
    private bool isTraveling;
    private DockPoint destination;

    private void Awake()
    {
        startingIsland.DockBoat(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTraveling)
        {
            DockPoint dockPoint = collision.GetComponent<DockPoint>();
            if (dockPoint == destination)
            {
                dockPoint.DockBoat(this);
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
        isTraveling = false;
        rigidbody.velocity = Vector3.zero;
    }

}
