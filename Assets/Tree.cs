using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private float offset;

    private void Start()
    {
        offset = Random.Range(0f, 0.3f);
    }
    private void FixedUpdate()
    {
        // rotate back and forth like a tree on a sin graph
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time + offset) * 5);
        // also scale up and down like a tree on a sin graph
        transform.localScale = Vector3.one * (Mathf.Sin(Time.time * 2 + 1 + offset) * 0.01f + 1);
    }
}
