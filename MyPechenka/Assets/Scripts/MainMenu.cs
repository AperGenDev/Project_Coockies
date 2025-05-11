using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // �������� �����, � ������� ���������� ����
    [SerializeField] private string storyStartSceneName = "StoryStart";
    [SerializeField] private string resumeSceneName = "SampleScene";

    // ����� ��� ������ ����
    public void PlayGame()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsPaused)
        {
            // ���� ���� �� ����� � ���������� � � ��������� ����� SampleScene
            GameManager.Instance.IsPaused = false;
            SceneManager.LoadScene(resumeSceneName);
        }
        else
        {
            // ���� ���� �� �� ����� � �������� ����� ���� � ����� StoryStart
            SceneManager.LoadScene(storyStartSceneName);
        }
    }

    // ����� ��� ������ �� ����
    public void ExitGame()
    {
        Debug.Log("����� �� ����");
        Application.Quit();
    }
}
