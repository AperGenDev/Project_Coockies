using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UpDown))]
public class ShowdownTrigger : MonoBehaviour
{
    public string showdownSceneName = "Showdown";
    public float floatAmplitude = 0.3f;
    public float floatFrequency = 1f;

    private bool isActivated = false;
    private int activatingPlayerNumber = 0;
    private UpDown upDownAnimation;
    private Collider2D triggerCollider;
    public SpriteRenderer cookieRenderer;

    private void Awake()
    {
        upDownAnimation = GetComponent<UpDown>();
        if (upDownAnimation == null)
        {
            upDownAnimation = gameObject.AddComponent<UpDown>();
        }

        upDownAnimation.enabled = false;
        upDownAnimation.amplitude = floatAmplitude;
        upDownAnimation.frequency = floatFrequency;

        // Инициализация коллайдера
        triggerCollider = GetComponent<Collider2D>();
        if (triggerCollider != null)
        {
            triggerCollider.isTrigger = true;
        }
    }

    public void ActivateForPlayer(int playerNumber)
    {
        if (isActivated) return;

        isActivated = true;
        activatingPlayerNumber = playerNumber;

        // Включаем анимацию
        upDownAnimation.enabled = true;
        if (cookieRenderer != null)
        {
            cookieRenderer.sortingOrder = 2; // Теперь печенька видна
        }
        // Активируем/добавляем коллайдер если нужно
        if (triggerCollider == null)
        {
            triggerCollider = gameObject.AddComponent<BoxCollider2D>();
            triggerCollider.isTrigger = true;
        }
        else
        {
            triggerCollider.enabled = true;
        }

        Debug.Log($"Trigger activated for Player {playerNumber}");
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (isActivated && (other.CompareTag("Player1") || other.CompareTag("Player2")))
    //    {
    //        Players player = other.GetComponent<Players>();
    //        if (player != null && player.playerNumber == activatingPlayerNumber)
    //        {
    //            // Явно сохраняем данные для HandController
    //            PlayerPrefs.SetInt("LastMiniGameWinner", activatingPlayerNumber);
    //            PlayerPrefs.Save(); // Важно: принудительно сохраняем

    //            Debug.Log($"Активировал триггер Игрок {activatingPlayerNumber}");
    //            SceneManager.LoadScene(showdownSceneName);
    //        }
    //    }
    //}
    void OnTriggerEnter2D(Collider2D other)
{
    if (isActivated && (other.CompareTag("Player1") || other.CompareTag("Player2")))
    {
        Players player = other.GetComponent<Players>();
        if (player != null && player.playerNumber == activatingPlayerNumber)
        {
<<<<<<< Updated upstream
            // Сохраняем победителя как строку
            PlayerPrefs.SetString("Winner", $"Player{activatingPlayerNumber}");
            PlayerPrefs.SetInt("LastMiniGameWinner", activatingPlayerNumber);
            PlayerPrefs.Save(); // Сохраняем PlayerPrefs на диск
=======
            Players player = other.GetComponent<Players>();
            if (player != null && player.playerNumber == activatingPlayerNumber)
            {
                // Сохраняем победителя как строку
                PlayerPrefs.SetString("Winner", $"Player{activatingPlayerNumber}");
                PlayerPrefs.SetInt("LastMiniGameWinner", activatingPlayerNumber);
                PlayerPrefs.Save(); // Сохраняем PlayerPrefs на диск
>>>>>>> Stashed changes

            Debug.Log($"Активировал триггер Игрок {activatingPlayerNumber}");
            SceneManager.LoadScene(showdownSceneName);
        }
    }
<<<<<<< Updated upstream
}

}
=======


}
>>>>>>> Stashed changes
