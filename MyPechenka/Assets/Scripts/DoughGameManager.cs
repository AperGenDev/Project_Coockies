//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using static System.Net.Mime.MediaTypeNames;
//public class DoughGameManager : MonoBehaviour
//{
//    [Header("UI Settings")]
//    public TextMeshProUGUI doughCounterText; // Перетащите сюда ваш TextMeshPro
//    public ParticleSystem completionEffect; // Эффект при завершении
//    //public Text doughCounterText; // Для стандартного UI Text

//    private void UpdateCounter()
//    {
//        if (doughCounterText != null)
//        {
//            doughCounterText.text = $"Осталось: {activeDoughBalls.Count}";

//            // Анимация при изменении
//            doughCounterText.transform.localScale = Vector3.one * 1.2f;
//            StartCoroutine(ScaleAnimation(doughCounterText.gameObject));

//            IEnumerator ScaleAnimation(GameObject obj)
//            {
//                float duration = 0.3f;
//                float elapsed = 0f;
//                Vector3 startScale = Vector3.one * 1.2f;
//                Vector3 endScale = Vector3.one;

//                while (elapsed < duration)
//                {
//                    obj.transform.localScale = Vector3.Lerp(startScale, endScale, elapsed / duration);
//                    elapsed += Time.deltaTime;
//                    yield return null;
//                }

//                obj.transform.localScale = endScale;
//            }
//            // Изменение цвета при малом количестве
//            doughCounterText.color = activeDoughBalls.Count <= 2 ?
//                Color.red : new Color(0.2f, 0.8f, 0.2f);
//        }
//    }


//    public static DoughGameManager Instance;

//    public Canvas player1Canvas;
//    public Canvas player2Canvas;
//    public GameObject doughBallPrefab;

//    private bool player1InGame = false;
//    private bool player2InGame = false;
//    private List<GameObject> activeDoughBalls = new List<GameObject>();//
//    public MiniGameTriggerA gameTrigger;

//    private void Awake()
//    {
//        if (Instance == null) Instance = this;
//        else Destroy(gameObject);

//        player1Canvas.gameObject.SetActive(false);
//        player2Canvas.gameObject.SetActive(false);
//        DoughBall.OnDoughBallCompleted += HandleDoughBallCompleted;//
//    }
//    private void OnDestroy()//
//    {
//        DoughBall.OnDoughBallCompleted -= HandleDoughBallCompleted;//
//    }

//    private void HandleDoughBallCompleted(int playerNumber)
//    {
//        // Удаляем все уничтоженные комочки из списка
//        activeDoughBalls.RemoveAll(item => item == null);
//        Debug.Log($"Осталось комочков: {activeDoughBalls.Count}"); // <- Добавьте эту строку
//        UpdateCounter();
//        if (activeDoughBalls.Count == 0)
//        {
//            if (completionEffect != null)
//                completionEffect.Play();

//            StartCoroutine(EndGameWithDelay(playerNumber, 1f));
//        }
//        IEnumerator EndGameWithDelay(int playerNumber, float delay)
//        {
//            yield return new WaitForSeconds(delay);
//            EndMiniGame(playerNumber);
//        }
//        if (activeDoughBalls.Count == 1)
//        {
//            Destroy(activeDoughBalls[0]);
//            activeDoughBalls.Clear();
//        }
//        // Если комочков не осталось - завершаем игру
//        if (activeDoughBalls.Count == 0)
//        {
//            Debug.Log("Все комочки раскатаны!"); // 
//            EndMiniGame(playerNumber);
//            if (gameTrigger != null)
//            {
//                gameTrigger.DisableTrigger();
//            }
//        }

//    }

//    public void StartMiniGame(int playerNumber)
//    {
//        if (playerNumber == 1 && !player1InGame)
//        {
//            player1InGame = true;
//            player1Canvas.gameObject.SetActive(true);
//            SpawnDoughBalls(player1Canvas.transform, playerNumber); // Добавляем playerNumber
//        }
//        else if (playerNumber == 2 && !player2InGame)
//        {
//            player2InGame = true;
//            player2Canvas.gameObject.SetActive(true);
//            SpawnDoughBalls(player2Canvas.transform, playerNumber); // Добавляем playerNumber
//        }
//    }

//    public void EndMiniGame(int playerNumber)
//    {
//        GameObject player = GameObject.FindGameObjectWithTag(playerNumber == 1 ? "Player1" : "Player2");
//        if (player != null)
//        {
//            PlayerMovement movement = player.GetComponent<PlayerMovement>();
//            if (movement != null)
//            {
//                movement.SetMovementEnabled(true);
//            }
//        }
//        if (playerNumber == 1)
//        {
//            player1InGame = false;
//            player1Canvas.gameObject.SetActive(false);
//        }
//        else if (playerNumber == 2)
//        {
//            player2InGame = false;
//            player2Canvas.gameObject.SetActive(false);
//        }
//    }

//    //private void SpawnDoughBalls(Transform parent, int playerNumber) // Добавляем параметр playerNumber
//    //{
//    //    for (int i = 0; i < 5; i++) // Создаем 5 комочков
//    //    {
//    //        GameObject ball = Instantiate(doughBallPrefab, parent);
//    //        ball.transform.localPosition = new Vector3(
//    //            Random.Range(-200, 200),
//    //            Random.Range(-100, 100),
//    //            0
//    //        );

//    //        // Настройка обработчика для комочка
//    //        DoughBall doughBall = ball.AddComponent<DoughBall>();
//    //        doughBall.Init(playerNumber); // Теперь playerNumber передается корректно
//    //    }
//    //}
//    private void SpawnDoughBalls(Transform parent, int playerNumber)
//    {
//        activeDoughBalls.Clear();
//        int ballsCount = 4;

//        for (int i = 0; i < ballsCount; i++)
//        {
//            GameObject ball = Instantiate(doughBallPrefab, parent);
//            ball.transform.localPosition = new Vector3(
//                Random.Range(-150, 150),
//                Random.Range(-80, 80),
//                0
//            );
//            ball.transform.localScale = Vector3.one; // Сброс масштаба
//            DoughBall doughBall = ball.GetComponent<DoughBall>();
//            doughBall.Init(playerNumber);

//            activeDoughBalls.Add(ball); // Добавляем в список
//        }
//    }


//}





using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DoughGameManager : MonoBehaviour
{
    [Header("Player Settings")]
    public Canvas targetCanvas; // Canvas для этого игрока
    public GameObject doughBallPrefab;

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

        if (currentTrigger != null)
        {
            currentTrigger.DisableTrigger();
        }
    }

}