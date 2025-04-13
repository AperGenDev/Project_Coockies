using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    public int Value { get; private set; }
    public bool IsMatched { get; private set; }
    public bool IsFlipped { get; private set; }

    [SerializeField] private GameObject front;
    [SerializeField] private GameObject back;

    private MemoryGameManager_new manager;

    // Настройка карты: передаем значение и ссылку на менеджер
    public void Setup(int value, MemoryGameManager_new mgr)
    {
        Value = value;
        manager = mgr;
        FlipBack();
    }

    // Метод, вызываемый при клике на карту
    public void OnClick()
    {
        manager.OnCardClicked(this);
    }

    // Переворачиваем карту лицевой стороной вверх
    public void Flip()
    {
        front.SetActive(true);
        back.SetActive(false);
        IsFlipped = true;
    }

    // Переворачиваем карту обратно
    public void FlipBack()
    {
        front.SetActive(false);
        back.SetActive(true);
        IsFlipped = false;
    }

    // Помечаем карту как подобранную пару
    public void SetMatched()
    {
        IsMatched = true;
    }
}
