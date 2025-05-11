using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowdownTimer : MonoBehaviour
{
    public float delay = 15f; // время задержки перед переходом

    void Start()
    {
        Invoke(nameof(LoadGameFinishingScene), delay);
    }

    void LoadGameFinishingScene()
    {
        // Просто загружаем сцену GameFinishing
        SceneManager.LoadScene("GameFinishing");
    }
}
