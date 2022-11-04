using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteTrail : MonoBehaviour
{
    [SerializeField] private GameObject routeDotPrefab;
    [SerializeField] private float distanceBetweenDots;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color hoverColor;
    private List<SpriteRenderer> routeDots;
    [HideInInspector] public Island destination;

    public void CreateRouteDots(Vector2 position1, Vector2 position2)
    {
        routeDots = new List<SpriteRenderer>();
        Vector2 middlePoint = (position1 + position2) / 2;
        Vector2 axis = (position2 - position1).normalized;
        int numDots = (int)(Vector2.Distance(position1, position2) / distanceBetweenDots);
        Vector2 startDot = middlePoint - (axis * distanceBetweenDots * numDots / 2);
        for (int i = 0; i < numDots; i++)
        {
            GameObject routeDot = Instantiate(routeDotPrefab, startDot + (i * distanceBetweenDots * axis), Quaternion.identity);
            routeDot.transform.SetParent(transform);
            routeDots.Add(routeDot.GetComponent<SpriteRenderer>());
        }
    }
    public void Hover()
    {
        foreach (SpriteRenderer routeDot in routeDots)
        {
            routeDot.color = hoverColor;
        }
    }
    public void StopHover()
    {
        foreach (SpriteRenderer routeDot in routeDots)
        {
            routeDot.color = defaultColor;
        }
    }
}
