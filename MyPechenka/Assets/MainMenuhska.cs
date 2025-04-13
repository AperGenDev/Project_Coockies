using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuhska : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void ExitGame()
    {
        Debug.Log("Вышел из игры");
        Application.Quit();
    }


}
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class MainMenu : MonoBehaviour
//{
//    public void PlayGame()
//    {
//        // Загружаем игру, выгружая меню
//        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
//    }

//    public void ReturnToMenu()
//    {
//        // Удаляем все объекты из игры, которые не нужны в меню
//        Destroy(GameObject.Find("GameCanvas"));

//        // Загружаем меню
//        SceneManager.LoadScene("Menushka", LoadSceneMode.Single);
//    }

//    public void ExitGame()
//    {
//        Application.Quit();
//    }
//}