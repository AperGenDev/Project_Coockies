using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuhska : MonoBehaviour
{
    public void PlayGame()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("��� ��������� �����!");
            // �����, ��������, ��������� � ����:
            SceneManager.LoadScene(0);
        }
    }


    public void ExitGame()
    {
        Debug.Log("����� �� ����");
        Application.Quit();
    }


}
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class MainMenu : MonoBehaviour
//{
//    public void PlayGame()
//    {
//        // ��������� ����, �������� ����
//        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
//    }

//    public void ReturnToMenu()
//    {
//        // ������� ��� ������� �� ����, ������� �� ����� � ����
//        Destroy(GameObject.Find("GameCanvas"));

//        // ��������� ����
//        SceneManager.LoadScene("Menushka", LoadSceneMode.Single);
//    }

//    public void ExitGame()
//    {
//        Application.Quit();
//    }
//}