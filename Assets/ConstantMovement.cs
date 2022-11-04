using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovement : MonoBehaviour
{
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private float moveSpeed;

    private void Update()
    {
        //Lerp to position
        transform.position = Vector3.Lerp(transform.position, transform.position + moveDirection, moveSpeed * Time.deltaTime);
    }
}
