using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniGame : MonoBehaviour
{
    protected PlayerWindow parentWindow;

    public virtual void Initialize(PlayerWindow window)
    {
        parentWindow = window;
        gameObject.SetActive(true);
    }

    public virtual void CompleteGame(bool success)
    {
        gameObject.SetActive(false);
        parentWindow.OnMiniGameCompleted(success);
    }
}
