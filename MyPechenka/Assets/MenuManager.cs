using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Настройки")]
    public float delayAfterLoad = 0.1f; // Задержка для надёжности

    [Header("Кнопки")]
    public Button startButton;
    public Button exitButton;

    private void Start()
    {
        startButton.onClick.AddListener(() => StartCoroutine(LoadGameScene()));
        exitButton.onClick.AddListener(ExitGame);
    }

    private IEnumerator LoadGameScene()
    {
        // Загружаем сцену асинхронно
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("SampleScene");
        loadOperation.allowSceneActivation = true;

        // Ждём полной загрузки
        while (!loadOperation.isDone)
            yield return null;

        // Критически важная задержка!
        yield return new WaitForSeconds(delayAfterLoad);

        // Поиск Canvas с гарантией
        VisionCanvasController canvasController = FindObjectOfType<VisionCanvasController>(true);

        if (canvasController != null)
        {
            canvasController.EnableCanvas();
            Debug.Log("Canvas успешно активирован!", canvasController.gameObject);
        }
        else
        {
            Debug.LogError("CanvasController не найден в сцене!");
        }
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}