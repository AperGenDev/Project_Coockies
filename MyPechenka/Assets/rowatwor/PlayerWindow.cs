/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWindow : MonoBehaviour
{
    public float displayTime = 5f; // Время отображения. Пока так!

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowWindow()
    {
        gameObject.SetActive(true);
        Invoke("HideWindow", displayTime);
    }

    private void HideWindow()
    {
        gameObject.SetActive(false);
    }
}*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWindow : MonoBehaviour
{
    [System.Serializable]
    public class MiniGameData
    {
        public string gameName;
        public MiniGame gamePrefab;
    }

    public MiniGameData[] availableGames;
    public Transform gameContainer;
    private MiniGame currentMiniGame;

    public void ShowWindow(Players player)
    {
        player.DisableControl();
        gameObject.SetActive(true);
        StartRandomMiniGame();
    }

    private void StartRandomMiniGame()
    {
        if (availableGames.Length == 0) return;

        if (currentMiniGame != null)
            Destroy(currentMiniGame.gameObject);

        int randomIndex = Random.Range(0, availableGames.Length);
        currentMiniGame = Instantiate(availableGames[randomIndex].gamePrefab, gameContainer);
        currentMiniGame.Initialize(this);
    }

    public void OnMiniGameCompleted(bool success)
    {
        gameObject.SetActive(false);
        // Управление возвращается игроку
        FindObjectOfType<Players>().EnableControl();
    }
}