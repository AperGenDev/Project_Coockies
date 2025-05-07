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
        bool isPlayer1 = (winnerNumber == 1); // NEW: ���� ��� ������ 1

        Debug.Log($"����� ��������. ����� {winnerNumber} (�������� {(isPlayer1 ? "������" : "�����")})");

        StartCoroutine(HandAnimation(isPlayer1));
    }

    IEnumerator HandAnimation(bool isPlayer1)
    {
        // NEW: ��������������� ������ �������
        float startX = isPlayer1 ? 15f : -15f; // ����� 1 - ������, ����� 2 - �����
        Vector3 startPos = new Vector3(startX, 0, 0);

        GameObject hand = Instantiate(handPrefab, startPos, Quaternion.identity);
        SpriteRenderer handRenderer = hand.GetComponent<SpriteRenderer>();
        handRenderer.sortingLayerName = "CookieLines";
        handRenderer.sortingOrder = 3;

        // NEW: �������� scale ������ ��� ������ 1 (����� ���� �������� �����)
        if (isPlayer1)
        {
            Vector3 scale = hand.transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            hand.transform.localScale = scale;
            Debug.Log("���� ������������� ��� ������ 1");
        }

        if (handRenderer != null && defaultHandSprite != null)
        {
            handRenderer.sprite = defaultHandSprite;
        }

        // 1. ������ � �������
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

        // 2. ����� �������
        if (handRenderer != null && grabbingHandSprite != null)
        {
            handRenderer.sprite = grabbingHandSprite;
        }

        // 3. ������������ �������
        cookie.SetParent(hand.transform);

        // �������� ������� ���������� ������� � ��� �����
        SpriteRenderer cookieRenderer = cookie.GetComponent<SpriteRenderer>();
        if (cookieRenderer != null)
        {
            cookieRenderer.sortingOrder = 3; // ������� ���� �����
        }

        // ����� ������� ������ ���� ���� �������
        foreach (Transform line in cookie)
        {
            LineRenderer lr = line.GetComponent<LineRenderer>();
            if (lr != null)
            {
                lr.sortingOrder = 4;
            }
        }

        // 4. ����������� ����
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