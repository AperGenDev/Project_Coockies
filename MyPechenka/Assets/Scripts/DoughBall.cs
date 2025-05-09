//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.EventSystems;

//public class DoughBall : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
//{
//    public float requiredPressTime = 2f;
//    public float targetScale = 0.3f;

//    private float pressTime = 0f;
//    private bool isPressed = false;
//    private int playerNumber;
//    private Vector3 originalScale;

//    public void Init(int player)
//    {
//        playerNumber = player;
//        originalScale = transform.localScale;
//    }

//    public void OnPointerDown(PointerEventData eventData)
//    {
//        isPressed = true;
//    }

//    public void OnPointerUp(PointerEventData eventData)
//    {
//        isPressed = false;
//    }

//    private void Update()
//    {
//        if (isPressed)
//        {
//            pressTime += Time.deltaTime;
//            float progress = Mathf.Clamp01(pressTime / requiredPressTime);
//            transform.localScale = Vector3.Lerp(originalScale, originalScale * targetScale, progress);

//            if (progress >= 1f)
//            {
//                // Комочек готов
//                Destroy(gameObject);
//            }
//        }
//    }
//}


//using UnityEngine;
//using UnityEngine.EventSystems;

//public class DoughBall : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
//{
//    [Header("Particle Effects")]
//    public GameObject spawnEffect;
//    public GameObject destroyEffect;

//    private void Start()
//    {
//        // Эффект при создании
//        if (spawnEffect != null)
//        {
//            Instantiate(spawnEffect, transform.position, Quaternion.identity);
//        }
//    }

//    private void OnDestroy()
//    {
//        // Эффект при уничтожении
//        if (!gameObject.scene.isLoaded) return; // Чтобы не срабатывало при выходе из игры

//        if (destroyEffect != null)
//        {
//            Instantiate(destroyEffect, transform.position, Quaternion.identity);
//        }
//    }

//    public static event System.Action<int> OnDoughBallCompleted; // Событие при завершении

//    public float requiredPressTime = 2f;
//    public float targetScale = 0.3f;

//    private float pressTime = 0f;
//    private bool isPressed = false;
//    private int playerNumber;
//    private Vector3 originalScale;

//    public void Init(int player)
//    {
//        playerNumber = player;
//        originalScale = transform.localScale;
//    }

//    public void OnPointerDown(PointerEventData eventData)
//    {
//        isPressed = true;
//    }

//    public void OnPointerUp(PointerEventData eventData)
//    {
//        isPressed = false;
//    }

//    private void Update()
//    {
//        // Автоуничтожение если вышел за пределы
//        if (transform.position.x > Screen.width ||
//           transform.position.x < 0 ||
//           transform.position.y > Screen.height ||
//           transform.position.y < 0)
//        {
//            Destroy(gameObject);
//        }
//        if (isPressed)
//        {
//            pressTime += Time.deltaTime;
//            float progress = Mathf.Clamp01(pressTime / requiredPressTime);
//            transform.localScale = Vector3.Lerp(originalScale, originalScale * targetScale, progress);

//            if (progress >= 1f)
//            {
//                OnDoughBallCompleted?.Invoke(playerNumber); // Уведомляем о завершении
//                Destroy(gameObject);
//            }
//        }
//    }
//}

using UnityEngine;
using UnityEngine.EventSystems;

public class DoughBall : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float requiredPressTime = 2f;
    public float targetScale = 0.3f;
    private DoughGameManager manager;
    private float pressTime;
    private bool isPressed;
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
        manager = GetComponentInParent<DoughGameManager>(); // Находим менеджер через родителя
    }

    public void OnPointerDown(PointerEventData eventData) => isPressed = true;
    public void OnPointerUp(PointerEventData eventData) => isPressed = false;

    private void Update()
    {
        if (isPressed)
        {
            pressTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(originalScale, originalScale * targetScale, pressTime / requiredPressTime);

            if (pressTime >= requiredPressTime)
            {
                manager?.NotifyBallDestroyed(gameObject);
                Destroy(gameObject);
            }
        }
    }
}