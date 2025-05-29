using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DoughGameManager : MonoBehaviour
{
    [Header("Player Settings")]
    public Canvas targetCanvas;
    public GameObject doughBallPrefab;
    public TriggerAction bottleTrigger; // Ссылка на триггер бутылки (перетащи в инспекторе)

    [Header("Ball Positions")]
    public Vector2[] ballPositions = new Vector2[4];

    private List<GameObject> activeBalls = new List<GameObject>();
    private MiniGameTriggerA currentTrigger;
    private bool isGameActive = false;

    public void StartMiniGame(MiniGameTriggerA trigger)
    {
        if (isGameActive) return;

        currentTrigger = trigger;
        isGameActive = true;
        targetCanvas.gameObject.SetActive(true);
        SpawnBalls();
    }

    private void SpawnBalls()
    {
        foreach (var pos in ballPositions)
        {
            GameObject ball = Instantiate(doughBallPrefab, targetCanvas.transform);
            ball.transform.localPosition = pos;
            activeBalls.Add(ball);
        }
    }

    public void NotifyBallDestroyed(GameObject ball)
    {
        activeBalls.Remove(ball);

        if (activeBalls.Count == 0)
        {
            CompleteMiniGame();
        }
    }

    private void CompleteMiniGame()
    {
        targetCanvas.gameObject.SetActive(false);
        isGameActive = false;

        // Активируем триггер бутылки после завершения
        if (bottleTrigger != null)
        {
            bottleTrigger.gameObject.SetActive(true);
        }

        if (currentTrigger != null)
        {
            currentTrigger.DisableTrigger();
        }
    }
}