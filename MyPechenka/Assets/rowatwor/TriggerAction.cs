using UnityEngine;

public class TriggerAction : MonoBehaviour
{
    public PlayerWindow playerWindow;
    public PlayerWindow player2Window;

    private void Start()
    {
        // Изначально выключаем триггер, пока не пройдены комочки
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Players player = other.GetComponent<Players>();
        if (player == null) return;

        PlayerWindow targetWindow = (player.name == "Player") ? playerWindow : player2Window;

        if (targetWindow != null)
        {
            Destroy(gameObject);
            targetWindow.ShowWindow(player);
        }
    }
}