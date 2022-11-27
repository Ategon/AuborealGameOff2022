using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSummoner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSummon;
    [SerializeField] private float summonMinDelay = 1f;
    [SerializeField] private float summonMaxDelay = 5f;
    [SerializeField] private GameObject overrideParent;
    [SerializeField] private bool useOverrideColor;
    [SerializeField] private Color overrideColor;

    private Coroutine summonCoroutine;

    private void Start()
    {
        summonCoroutine = StartCoroutine(SummonObject());
    }

    private void OnDestroy()
    {
        StopCoroutine(summonCoroutine);
    }

    private IEnumerator SummonObject()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(summonMinDelay, summonMaxDelay));
            GameObject summonedObject = Instantiate(objectToSummon, transform.position, Quaternion.identity);
            Vector3 objectScale = summonedObject.transform.localScale;
            
            if(overrideParent) summonedObject.transform.SetParent(overrideParent.transform);
            else summonedObject.transform.SetParent(transform);

            if (useOverrideColor)
            {
                if (summonedObject.GetComponent<SpriteRenderer>() != null)
                {
                    summonedObject.GetComponent<SpriteRenderer>().color = overrideColor;
                }
                if (summonedObject.GetComponent<Image>() != null) 
                {
                    summonedObject.GetComponent<Image>().color = overrideColor;
                }
            }
            
            summonedObject.transform.localScale = objectScale;
        }
    }
}
