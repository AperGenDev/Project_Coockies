using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShakeBottleGame : MiniGame
{
    [Header("Настройки")]
    public int requiredShakes = 10;
    public float shakeIntensity = 30f;
    public float shakeDuration = 0.5f;
    public float progressBarHeight = 200f;

    [Header("Ссылки")]
    public RectTransform bottleTransform;
    public Button bottleButton;
    public RectTransform fillBar;
    public ShowdownTrigger showdownTrigger;

    private int currentShakes;
    private Vector2 originalBottlePos;
    private PlayerWindow currentPlayerWindow;

    private void Start()
    {
        if (showdownTrigger == null)
        {
            GameObject triggerObj = GameObject.Find("pechen'ka"); // Укажите имя вашего объекта
            if (triggerObj != null)
            {
                showdownTrigger = triggerObj.GetComponent<ShowdownTrigger>();
            }
            else
            {
                Debug.LogError("Не найден объект с именем 'ShowdownTriggerObject'");
            }
        }
    }

    public override void Initialize(PlayerWindow window)
    {
        base.Initialize(window);
        currentPlayerWindow = window; // Сохраняем ссылку на игрока

        originalBottlePos = bottleTransform.anchoredPosition;
        currentShakes = 0;

        fillBar.sizeDelta = new Vector2(fillBar.sizeDelta.x, 0);
        fillBar.anchoredPosition = Vector2.zero;

        bottleButton.onClick.RemoveAllListeners();
        bottleButton.onClick.AddListener(ShakeBottle);
    }

    private void ShakeBottle()
    {
        if (!isActiveAndEnabled) return;

        StartCoroutine(ShakeAnimation());
        currentShakes++;
        UpdateProgressBar();

        if (currentShakes >= requiredShakes)
            CompleteGame();
    }

    private IEnumerator ShakeAnimation()
    {
        Vector2 startPos = originalBottlePos;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float offsetY = Random.Range(-shakeIntensity, shakeIntensity);
            bottleTransform.anchoredPosition = startPos + new Vector2(0, offsetY);
            elapsed += Time.deltaTime;
            yield return null;
        }

        bottleTransform.anchoredPosition = originalBottlePos;
    }

    private void UpdateProgressBar()
    {
        float fillAmount = (float)currentShakes / requiredShakes;
        fillBar.sizeDelta = new Vector2(fillBar.sizeDelta.x, progressBarHeight * fillAmount);
    }

    private void CompleteGame()
    {
        if (showdownTrigger != null && currentPlayerWindow != null)
        {
            // Передаём номер игрока из PlayerWindow
            showdownTrigger.ActivateForPlayer(currentPlayerWindow.playerNumber);
        }
        base.CompleteGame(true);
    }
}