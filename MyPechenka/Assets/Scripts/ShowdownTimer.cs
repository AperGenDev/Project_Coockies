using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowdownTimer : MonoBehaviour
{
    public float delay = 15f; // ����� �������� ����� ���������

    void Start()
    {
        Invoke(nameof(LoadGameFinishingScene), delay);
    }

    void LoadGameFinishingScene()
    {
        // ������ ��������� ����� GameFinishing
        SceneManager.LoadScene("GameFinishing");
    }
}
