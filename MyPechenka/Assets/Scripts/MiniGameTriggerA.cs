//using UnityEngine;

//public class MiniGameTriggerA : MonoBehaviour
//{
//  //  private bool isGameCompleted = false;

//    private void OnTriggerEnter2D(Collider2D other)
//    {

//       if (other.CompareTag("Player1"))
//        {
//            // �������� ��������� �������� ������ � ��������� ���
//            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
//            if (playerMovement != null)
//            {
//                playerMovement.SetMovementEnabled(false);
//            }
//            DoughGameManager.Instance.StartMiniGame(1);
//        }
//        // ���� ����� ������ � ����� "Player2"
//        else if (other.CompareTag("Player2"))
//        {
//            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
//            if (playerMovement != null)
//            {
//                playerMovement.SetMovementEnabled(false);
//            }
//            DoughGameManager.Instance.StartMiniGame(2);
//        }
//    }

//    private void OnTriggerExit2D(Collider2D other)
//    {
//        if (other.CompareTag("Player1"))
//        {
//            DoughGameManager.Instance.EndMiniGame(1);
//        }
//        else if (other.CompareTag("Player2"))
//        {
//            DoughGameManager.Instance.EndMiniGame(2);
//        }
//    }
//    // ��������� ����� ��� ���������� ��������
//    public void DisableTrigger()
//    {
//    //    isGameCompleted = true;
//        GetComponent<Collider2D>().enabled = false; // ��������� ���������
//        GetComponent<SpriteRenderer>().enabled = false; // �������� ��������� (���� ���� ������)
//    }

//}

using UnityEngine;

public class MiniGameTriggerA : MonoBehaviour
{
    public DoughGameManager targetManager; // ���������� ������ �������� � ����������!
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