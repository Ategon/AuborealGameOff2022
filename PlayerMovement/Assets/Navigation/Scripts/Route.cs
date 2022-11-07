using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Navigation
{
    [Serializable]
    public class Route
    {
        public Island destination;
        public int thirstCost;
    }
}
