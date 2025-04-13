using UnityEngine;

public class StartMiniGame : MonoBehaviour
{
    public GameObject memoryGame;

    private void OnMouseDown() // если используешь физическое касание (2D)
    {
        memoryGame.SetActive(true);
    }
}
