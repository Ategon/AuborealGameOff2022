using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private Vector3 direction = Vector3.zero;

    private void FixedUpdate()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    public ConstantMovement SetDirection(Vector3 direction)
    {
        this.direction = direction;
        return this;
    }

    public ConstantMovement SetSpeed(float speed)
    {
        this.speed = speed;
        return this;
    }
}
