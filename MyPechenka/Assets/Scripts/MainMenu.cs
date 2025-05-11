using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Название сцены, с которой начинается игра
    [SerializeField] private string storyStartSceneName = "StoryStart";
    [SerializeField] private string resumeSceneName = "SampleScene";

    // Метод для начала игры
    public void PlayGame()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsPaused)
        {
            // Если игра на паузе — продолжаем её и загружаем сцену SampleScene
            GameManager.Instance.IsPaused = false;
            SceneManager.LoadScene(resumeSceneName);
        }
        else
        {
            // Если игра не на паузе — начинаем новую игру с сцены StoryStart
            SceneManager.LoadScene(storyStartSceneName);
        }
    }

    // Метод для выхода из игры
    public void ExitGame()
    {
        Debug.Log("Вышел из игры");
        Application.Quit();
    }
}
