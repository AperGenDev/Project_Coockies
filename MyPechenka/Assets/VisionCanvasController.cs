using UnityEngine;

public class VisionCanvasController : MonoBehaviour
{
    private void Awake()
    {
        // Принудительно скрываем Canvas при старте
        gameObject.SetActive(false);
        Debug.Log("VisionCanvasController инициализирован", this);
    }

    public void EnableCanvas()
    {
        gameObject.SetActive(true);
        Debug.Log("Canvas ВКЛЮЧЁН", this);
    }
}