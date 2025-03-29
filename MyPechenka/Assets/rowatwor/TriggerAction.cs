using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAction : MonoBehaviour
{
    public PlayerWindow playerWindow;
    public Player_2Window player_2Window;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Players player = other.GetComponent<Players>();
        if (player != null && player.name == "Player")
        {
            if (playerWindow != null)
            {
                player.DisableControl(); // Пока не даем двигаться. По хорошему убрать джойстики
                playerWindow.ShowWindow();
                Destroy(gameObject);
            }
        }
        else if (player != null && player.name == "Player_2")
        {
            if (player_2Window != null)
            {
                player.DisableControl(); // Пока не даем двигаться. По хорошему убрать джойстики
                player_2Window.ShowWindow();
                Destroy(gameObject);
            }
        }
    }
}
