using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuhska : MonoBehaviour
{
    [SerializeField] private string storyStartSceneName = "StoryStart";
    [SerializeField] private string resumeSceneName = "SampleScene";

    [SerializeField] private GameObject OptionsMenu; // Перетяни OptionsMenu в инспекторе

    public void PlayGame()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Нет следующей сцены!");
            // Можно, например, вернуться в меню:
            SceneManager.LoadScene(0);
	}
        if (GameManager.Instance != null && GameManager.Instance.IsPaused)
        {
            GameManager.Instance.IsPaused = false;
            SceneManager.LoadScene(resumeSceneName);
        }
        else
        {
            SceneManager.LoadScene(storyStartSceneName);
        }
    }

    public void OpenSettings()
    {
        OptionsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        OptionsMenu.SetActive(false);
    }

    public void ExitGame()
    {
        Debug.Log("Вышел из игры");
        Application.Quit();
    }

}