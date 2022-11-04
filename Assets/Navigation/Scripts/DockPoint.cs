using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockPoint : MonoBehaviour
{
    [SerializeField] private Island island;

    public void DockBoat(Boat boat)
    {
        island.DockBoat(boat);
    }
}
