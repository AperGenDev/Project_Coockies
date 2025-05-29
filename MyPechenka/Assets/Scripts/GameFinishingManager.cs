using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameFinishingManager : MonoBehaviour
{
    public TextMeshProUGUI winnerText;
    public Image winnerImage;

    public Sprite player1Sprite;
    public Sprite player2Sprite;

    void Start()
    {
        string winner = PlayerPrefs.GetString("Winner", "Player1");

<<<<<<< Updated upstream
        // Ð£ÑÑ‚Ð°Ð½Ð°Ð²Ð»Ð¸Ð²Ð°ÐµÐ¼ Ñ‚ÐµÐºÑÑ‚ Ð¿Ð¾Ð±ÐµÐ´Ð¸Ñ‚ÐµÐ»Ñ
        winnerText.text = $"ÐŸÐ¾Ð±ÐµÐ´Ð¸Ñ‚ÐµÐ»ÑŒ Ñ Ð»ÑƒÑ‡ÑˆÐµÐ¹ Ð¿ÐµÑ‡ÐµÐ½ÑŒÐºÐ¾Ð¹: {winner}";

        // Ð£ÑÑ‚Ð°Ð½Ð°Ð²Ð»Ð¸Ð²Ð°ÐµÐ¼ ÑÐ¾Ð¾Ñ‚Ð²ÐµÑ‚ÑÑ‚Ð²ÑƒÑŽÑ‰Ð¸Ð¹ ÑÐ¿Ñ€Ð°Ð¹Ñ‚
=======
        // Óñòàíàâëèâàåì òåêñò ïîáåäèòåëÿ
        winnerText.text = $"Íàøè ïîçäðàâëåíèÿ!\r\nÏîáåäèòåëü ñ ëó÷øåé ïå÷åíüêîé: {winner}";

        // Óñòàíàâëèâàåì ñîîòâåòñòâóþùèé ñïðàéò
>>>>>>> Stashed changes
        if (winner == "Player1")
        {
            winnerImage.sprite = player1Sprite;
        }
        else if (winner == "Player2")
        {
            winnerImage.sprite = player2Sprite;
        }
    }
<<<<<<< Updated upstream
}
=======
}
>>>>>>> Stashed changes
