using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private void FixedUpdate()
    {
        // rotate back and forth like a tree on a sin graph
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time) * 5);
        // also scale up and down like a tree on a sin graph
        transform.localScale = Vector3.one * (Mathf.Sin(Time.time * 2 + 1) * 0.01f + 1);
    }
}
