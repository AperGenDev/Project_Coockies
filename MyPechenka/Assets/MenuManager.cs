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
        Debug.Log("Main Camera: " + (Camera.main != null ? "Есть" : "Нет"));
        if (Camera.main == null) Debug.LogError("Камера не найдена!");
        startButton.onClick.AddListener(() => StartCoroutine(LoadGameScene()));
        exitButton.onClick.AddListener(ExitGame);
    }

    private IEnumerator LoadGameScene()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("SampleScene");
        loadOperation.allowSceneActivation = true;

        // Ждём полной загрузки (не только isDone, но и 100% прогресса)
        while (!loadOperation.isDone || loadOperation.progress < 0.9f)
            yield return null;

        // Ищем Canvas только после полной загрузки
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
    private void OnEnable()
    {
        startButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(() => StartCoroutine(LoadGameScene()));
        exitButton.onClick.AddListener(ExitGame);
    }
    //private IEnumerator LoadGameScene()
    //{
    //    // Загружаем сцену асинхронно
    //    AsyncOperation loadOperation = SceneManager.LoadSceneAsync("SampleScene");
    //    loadOperation.allowSceneActivation = true;

    //    // Ждём полной загрузки
    //    while (!loadOperation.isDone)
    //        yield return null;

    //    // Критически важная задержка!
    //    yield return new WaitForSeconds(delayAfterLoad);

    //    // Поиск Canvas с гарантией
    //    VisionCanvasController canvasController = FindObjectOfType<VisionCanvasController>(true);

    //    if (canvasController != null)
    //    {
    //        canvasController.EnableCanvas();
    //        Debug.Log("Canvas успешно активирован!", canvasController.gameObject);
    //    }
    //    else
    //    {
    //        Debug.LogError("CanvasController не найден в сцене!");
    //    }
    //}

    private void ExitGame()
    {
        Application.Quit();
    }
}