using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float dayLength;

    public float CurrentTime { get; private set; }

    private void FixedUpdate()
    {
        CurrentTime += Time.deltaTime / dayLength;
        if (CurrentTime >= 1)
        {
            CurrentTime = 0;
        }
    }
}
