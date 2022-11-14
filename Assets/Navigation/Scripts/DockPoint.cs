using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Navigation
{
    public class DockPoint : MonoBehaviour
    {
        [SerializeField] private Island island;

        public void DockBoat()
        {
            island.DockBoat();
        }
    }
}