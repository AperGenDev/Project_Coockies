using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameManager_new : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform gridParent;

    private List<MemoryCard> cards = new List<MemoryCard>();

    private MemoryCard firstFlippedCard;
    private MemoryCard secondFlippedCard;
    private bool isProcessing = false;
    public void StartMiniGame()
    {
    // Логика для запуска мини-игры Memory
        Debug.Log("Мини-игра запущена!");
    // Можешь добавить код для активации мини-игры
    }

    void Start()
    {
        SpawnCards();
    }

    void SpawnCards()
    {
        List<int> values = new List<int>();
        for (int i = 0; i < 6; i++)
        {
            values.Add(i);
            values.Add(i);
        }

        Shuffle(values);

        foreach (int value in values)
        {
            GameObject cardObj = Instantiate(cardPrefab, gridParent);
            MemoryCard card = cardObj.GetComponent<MemoryCard>();
            card.Setup(value, this);
            cards.Add(card);
        }
    }

    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randIndex = Random.Range(i, list.Count);
            int temp = list[i];
            list[i] = list[randIndex];
            list[randIndex] = temp;
        }
    }

    public void OnCardClicked(MemoryCard clickedCard)
    {
        if (isProcessing || clickedCard.IsMatched || clickedCard.IsFlipped)
            return;

        clickedCard.Flip();

        if (firstFlippedCard == null)
        {
            firstFlippedCard = clickedCard;
        }
        else
        {
            secondFlippedCard = clickedCard;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        isProcessing = true;
        yield return new WaitForSeconds(1f);

        if (firstFlippedCard.Value == secondFlippedCard.Value)
        {
            firstFlippedCard.SetMatched();
            secondFlippedCard.SetMatched();
        }
        else
        {
            firstFlippedCard.FlipBack();
            secondFlippedCard.FlipBack();
        }

        firstFlippedCard = null;
        secondFlippedCard = null;
        isProcessing = false;
    }
}
