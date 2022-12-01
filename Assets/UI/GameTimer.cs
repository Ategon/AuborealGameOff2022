using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float dayLength;
    [SerializeField] private float startTime;

    public float CurrentTime { get; private set; }

    private void Start()
    {
        CurrentTime = startTime;
    }

    private void FixedUpdate()
    {
        CurrentTime += Time.deltaTime / dayLength;
        if (CurrentTime >= 1)
        {
            CurrentTime = 0;
        }
    }
}
