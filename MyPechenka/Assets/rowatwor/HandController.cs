using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HandController : MonoBehaviour
{
    public static HandController Instance;

    public GameObject handPrefab;
    public Transform cookie;
    public float approachDuration = 2f;
    public float retreatDuration = 2f;
    public Sprite defaultHandSprite;
    public Sprite grabbingHandSprite;

    void Awake()
    {
        Instance = this;
    }

    public void StartAnimation()
    {
        int winnerNumber = PlayerPrefs.GetInt("LastMiniGameWinner", 1);
        bool isPlayer1 = (winnerNumber == 1); // NEW: Флаг для Игрока 1

        Debug.Log($"Старт анимации. Игрок {winnerNumber} (появится {(isPlayer1 ? "справа" : "слева")})");

        StartCoroutine(HandAnimation(isPlayer1));
    }

    IEnumerator HandAnimation(bool isPlayer1)
    {
        // NEW: Инвертированная логика позиций
        float startX = isPlayer1 ? 15f : -15f; // Игрок 1 - справа, Игрок 2 - слева
        Vector3 startPos = new Vector3(startX, 0, 0);

        GameObject hand = Instantiate(handPrefab, startPos, Quaternion.identity);
        SpriteRenderer handRenderer = hand.GetComponent<SpriteRenderer>();
        handRenderer.sortingLayerName = "CookieLines";
        handRenderer.sortingOrder = 3;

        // NEW: Инверсия scale только для Игрока 1 (чтобы рука смотрела влево)
        if (isPlayer1)
        {
            Vector3 scale = hand.transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            hand.transform.localScale = scale;
            Debug.Log("Рука инвертирована для Игрока 1");
        }

        if (handRenderer != null && defaultHandSprite != null)
        {
            handRenderer.sprite = defaultHandSprite;
        }

        // 1. Подход к печенью
        float timer = 0f;
        while (timer < approachDuration)
        {
            timer += Time.deltaTime;
            hand.transform.position = Vector3.Lerp(
                startPos,
                cookie.position,
                timer / approachDuration
            );
            yield return null;
        }

        // 2. Смена спрайта
        if (handRenderer != null && grabbingHandSprite != null)
        {
            handRenderer.sprite = grabbingHandSprite;
        }

        // 3. Прикрепление печенья
        cookie.SetParent(hand.transform);

        // Изменяем порядок сортировки печенья и его линий
        SpriteRenderer cookieRenderer = cookie.GetComponent<SpriteRenderer>();
        if (cookieRenderer != null)
        {
            cookieRenderer.sortingOrder = 3; // Печенье выше всего
        }

        // Линии печенья должны быть выше печенья
        foreach (Transform line in cookie)
        {
            LineRenderer lr = line.GetComponent<LineRenderer>();
            if (lr != null)
            {
                lr.sortingOrder = 4;
            }
        }

        // 4. Возвращение руки
        timer = 0f;
        Vector3 returnStartPos = hand.transform.position;
        Vector3 endPos = new Vector3(startX, 0, 0);

        while (timer < retreatDuration)
        {
            timer += Time.deltaTime;
            hand.transform.position = Vector3.Lerp(
                returnStartPos,
                endPos,
                timer / retreatDuration
            );
            yield return null;
        }

        SceneManager.LoadScene("Menushka");
    }
}