using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    float startingY;
    float randomSineOffset;
    // Start is called before the first frame update
    void Start()
    {
        startingY = transform.localPosition.y;
        randomSineOffset = Random.Range(0, Mathf.PI * 2);
    }

    private void FixedUpdate()
    {
        //calculate a new y position based on time along a sine wave
        float newY = startingY + Mathf.Sin(Time.time * 2 + randomSineOffset) * 8;

        transform.localPosition = new Vector3(transform.localPosition.x + 25 * Time.deltaTime, newY, 0);
        if (transform.localPosition.x > 120)
        {
            transform.localPosition = new Vector3(-60, transform.localPosition.y, 0);
        }
    }
}
