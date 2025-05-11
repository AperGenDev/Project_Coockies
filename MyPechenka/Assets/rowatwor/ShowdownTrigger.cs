using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(UpDown))]
public class ShowdownTrigger : MonoBehaviour
{
    public string showdownSceneName = "Showdown";
    public float floatAmplitude = 0.3f;
    public float floatFrequency = 1f;

    private bool isActivated = false;
    private int activatingPlayerNumber = 0;
    private UpDown upDownAnimation;
    private Collider2D triggerCollider;

    private void Awake()
    {
        upDownAnimation = GetComponent<UpDown>();
        if (upDownAnimation == null)
        {
            upDownAnimation = gameObject.AddComponent<UpDown>();
        }

        upDownAnimation.enabled = false;
        upDownAnimation.amplitude = floatAmplitude;
        upDownAnimation.frequency = floatFrequency;

        // ������������� ����������
        triggerCollider = GetComponent<Collider2D>();
        if (triggerCollider != null)
        {
            triggerCollider.isTrigger = true;
        }
    }

    public void ActivateForPlayer(int playerNumber)
    {
        if (isActivated) return;

        isActivated = true;
        activatingPlayerNumber = playerNumber;

        // �������� ��������
        upDownAnimation.enabled = true;

        // ����������/��������� ��������� ���� �����
        if (triggerCollider == null)
        {
            triggerCollider = gameObject.AddComponent<BoxCollider2D>();
            triggerCollider.isTrigger = true;
        }
        else
        {
            triggerCollider.enabled = true;
        }

        Debug.Log($"Trigger activated for Player {playerNumber}");
    }

    void OnTriggerEnter2D(Collider2D other)
{
    if (isActivated && (other.CompareTag("Player1") || other.CompareTag("Player2")))
    {
        Players player = other.GetComponent<Players>();
        if (player != null && player.playerNumber == activatingPlayerNumber)
        {
            // ��������� ���������� ��� ������
            PlayerPrefs.SetString("Winner", $"Player{activatingPlayerNumber}");
            PlayerPrefs.SetInt("LastMiniGameWinner", activatingPlayerNumber);
            PlayerPrefs.Save(); // ��������� PlayerPrefs �� ����

            Debug.Log($"����������� ������� ����� {activatingPlayerNumber}");
            SceneManager.LoadScene(showdownSceneName);
        }
    }
}

}