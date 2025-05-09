using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("���������")]
    public float delayAfterLoad = 0.1f; // �������� ��� ���������

    [Header("������")]
    public Button startButton;
    public Button exitButton;

    private void Start()
    {
        Debug.Log("Main Camera: " + (Camera.main != null ? "����" : "���"));
        if (Camera.main == null) Debug.LogError("������ �� �������!");
        startButton.onClick.AddListener(() => StartCoroutine(LoadGameScene()));
        exitButton.onClick.AddListener(ExitGame);
    }

    private IEnumerator LoadGameScene()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("SampleScene");
        loadOperation.allowSceneActivation = true;

        // ��� ������ �������� (�� ������ isDone, �� � 100% ���������)
        while (!loadOperation.isDone || loadOperation.progress < 0.9f)
            yield return null;

        // ���� Canvas ������ ����� ������ ��������
        VisionCanvasController canvasController = FindObjectOfType<VisionCanvasController>(true);

        if (canvasController != null)
        {
            canvasController.EnableCanvas();
            Debug.Log("Canvas ������� �����������!", canvasController.gameObject);
        }
        else
        {
            Debug.LogError("CanvasController �� ������ � �����!");
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
    //    // ��������� ����� ����������
    //    AsyncOperation loadOperation = SceneManager.LoadSceneAsync("SampleScene");
    //    loadOperation.allowSceneActivation = true;

    //    // ��� ������ ��������
    //    while (!loadOperation.isDone)
    //        yield return null;

    //    // ���������� ������ ��������!
    //    yield return new WaitForSeconds(delayAfterLoad);

    //    // ����� Canvas � ���������
    //    VisionCanvasController canvasController = FindObjectOfType<VisionCanvasController>(true);

    //    if (canvasController != null)
    //    {
    //        canvasController.EnableCanvas();
    //        Debug.Log("Canvas ������� �����������!", canvasController.gameObject);
    //    }
    //    else
    //    {
    //        Debug.LogError("CanvasController �� ������ � �����!");
    //    }
    //}

    private void ExitGame()
    {
        Application.Quit();
    }
}