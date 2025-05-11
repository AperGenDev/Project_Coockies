using UnityEngine;

public class GameManager : MonoBehaviour
{
public static GameManager Instance;public bool IsPaused = false;

private void Awake()
{
    if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
    else
    {
        Destroy(gameObject);
    }
}
}