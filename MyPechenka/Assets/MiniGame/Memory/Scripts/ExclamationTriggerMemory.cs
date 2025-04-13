using UnityEngine;

public class ExclamationTriggerMemory : MonoBehaviour
{
    public GameObject memoryGameUI;  // Панель или объект мини-игры

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Игрок достиг восклицательного знака!");
            if (memoryGameUI != null)
            {
                memoryGameUI.SetActive(true);  // Включаем мини-игру
            }

            // Дополнительно можно убрать восклицательный знак после активации
            gameObject.SetActive(false);
        }
    }
}
