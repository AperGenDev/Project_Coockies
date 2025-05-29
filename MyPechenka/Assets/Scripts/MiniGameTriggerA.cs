using UnityEngine;

public class MiniGameTriggerA : MonoBehaviour
{
    public DoughGameManager targetManager; 
    public enum PlayerTarget { Player1Only, Player2Only }
    public PlayerTarget targetPlayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") && targetPlayer == PlayerTarget.Player1Only)
        {
            ActivateGame(other);
        }
        else if (other.CompareTag("Player2") && targetPlayer == PlayerTarget.Player2Only)
        {
            ActivateGame(other);
        }
    }

    private void ActivateGame(Collider2D player)
    {
        player.GetComponent<PlayerMovement>().SetMovementEnabled(false);
        targetManager.StartMiniGame(this);
    }

    public void DisableTrigger()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
}