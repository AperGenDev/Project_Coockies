using UnityEngine;

public class UpDown : MonoBehaviour
{
    public float amplitude = 0.5f;
    public float frequency = 1f;
    [Tooltip("Смещение фазы анимации (0-1)")]
    public float phaseOffset = 0f;

    private Vector3 startPosition;
    private float timeCounter = 0f;

    private void Start()
    {
        startPosition = transform.position;
        timeCounter = phaseOffset * Mathf.PI * 2f;
    }

    private void Update()
    {
        if (!enabled) return;

        timeCounter += Time.deltaTime * frequency;
        float newY = startPosition.y + Mathf.Sin(timeCounter) * amplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
    }
}