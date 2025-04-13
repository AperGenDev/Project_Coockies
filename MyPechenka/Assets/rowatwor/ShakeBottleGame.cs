/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickCounterGame : MiniGame
{
    [SerializeField] private Button targetButton;
    [SerializeField] private Image progressBar;
    [SerializeField] private int requiredClicks = 10;

    private int currentClicks;

    public override void Initialize(PlayerWindow window)
    {
        base.Initialize(window);
        currentClicks = 0;
        targetButton.onClick.AddListener(RegisterClick);
    }

    private void RegisterClick()
    {
        currentClicks++;

        if (currentClicks >= requiredClicks)
            CompleteGame(true);
    }
}*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShakeBottleGame : MiniGame
{
    [Header("Настройки")]
    public int requiredShakes = 10;
    public float shakeIntensity = 30f; // Сила тряски
    public float shakeDuration = 0.5f;
    public float progressBarHeight = 200f;

    [Header("Ссылки")]
    public RectTransform bottleTransform;
    public Button bottleButton;
    public RectTransform fillBar;

    private int currentShakes;
    private Vector2 originalBottlePos;

    public override void Initialize(PlayerWindow window)
    {
        base.Initialize(window);

        originalBottlePos = bottleTransform.anchoredPosition;
        currentShakes = 0;

        // Сбрасываем прогресс
        fillBar.sizeDelta = new Vector2(fillBar.sizeDelta.x, 0);
        fillBar.anchoredPosition = Vector2.zero;

        // Настраиваем кнопку
        bottleButton.onClick.RemoveAllListeners();
        bottleButton.onClick.AddListener(ShakeBottle);
    }

    private void ShakeBottle()
    {
        StartCoroutine(ShakeAnimation());
        currentShakes++;
        UpdateProgressBar();

        if (currentShakes >= requiredShakes)
            CompleteGame(true);
    }

    private IEnumerator ShakeAnimation()
    {
        Vector2 startPos = originalBottlePos;
        float elapsed = 0f;

        // Агрессивная тряска
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
        fillBar.sizeDelta = new Vector2(
            fillBar.sizeDelta.x,
            progressBarHeight * fillAmount
        );
    }

    public override void CompleteGame(bool success)
    {
        bottleButton.onClick.RemoveAllListeners();
        base.CompleteGame(success);
    }
}