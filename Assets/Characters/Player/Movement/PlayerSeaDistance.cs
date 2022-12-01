using Assets.Audio;
using UnityEngine;
using System.Linq;
using System.Globalization;
using UnityEngine.UIElements;

public class PlayerSeaDistance : MonoBehaviour
{
    [SerializeField] private AudioController audioController;
    [SerializeField] private float maxDistance;
    [SerializeField] private float minDistance;
    [SerializeField] private float timeBetweenChecks;
    private float timeSinceLastCheck;
    private void Update()
    {
        timeSinceLastCheck += Time.deltaTime;
        if (timeSinceLastCheck > timeBetweenChecks)
        {
            float seaDistance = ScaleSeaDistance(GetSeaDistanceInGameUnits());
            audioController.SetSeaDistance(seaDistance);
            timeSinceLastCheck = 0;
        }
    }
    private float ScaleSeaDistance(float seaDistanceGameUnits)
    {
        if (seaDistanceGameUnits <= minDistance)
            return 0;
        else if (seaDistanceGameUnits >= maxDistance)
            return 1;
        else
        {
            return (seaDistanceGameUnits - minDistance) / (maxDistance - minDistance);
        }
    }
    private float GetSeaDistanceInGameUnits()
    {
        float[] distances = new float[8];
        for (int i = 0; i < 8; i++)
        {
            float radians = i * Mathf.PI / 4;
            distances[i] = GetMinDistanceInDirection(new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)));
        }
        return Mathf.Min(distances);
    }
    private float GetMinDistanceInDirection(Vector2 direction)
    {
        int layerMask = 128;
        Vector2 startRaycast = new Vector2(transform.position.x, transform.position.y) + direction * maxDistance;
        RaycastHit2D raycastHit = Physics2D.Raycast(startRaycast, -direction, maxDistance, layerMask);
        if (raycastHit)
        {
            return maxDistance - raycastHit.distance;
        }
        else
        {
            return maxDistance;
        }
    }
}
