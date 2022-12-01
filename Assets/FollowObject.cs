using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    private Vector3 startingPos;
    private Vector3 objectStartPos;
    [SerializeField] private GameObject gameObject;

    void Start()
    {
        startingPos = transform.localPosition;
        objectStartPos = gameObject.transform.localPosition;
    }

    private void LateUpdate()
    {
        transform.localPosition = new Vector3(gameObject.transform.localPosition.x + startingPos.x, startingPos.y, gameObject.transform.localPosition.z + startingPos.y);
    }
}
