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
        // Устанавливаем текст победителя
        winnerText.text = $"Победитель с лучшей печенькой: {winner}";

        // Устанавливаем соответствующий спрайт
=======
        // ������������� ����� ����������
        winnerText.text = $"���� ������������!\r\n���������� � ������ ���������: {winner}";

        // ������������� ��������������� ������
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
