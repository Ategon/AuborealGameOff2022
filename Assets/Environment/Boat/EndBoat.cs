
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;

public class EndBoat : Boat
{
    

    public bool hasSextant;
    
    protected override bool Interact()
    {
        if (hasSextant)
            return base.Interact();
        return false;
    }

}
