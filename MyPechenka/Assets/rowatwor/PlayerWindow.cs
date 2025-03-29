using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWindow : MonoBehaviour
{
    public float displayTime = 5f; // Время отображения. Пока так!

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowWindow()
    {
        gameObject.SetActive(true);
        Invoke("HideWindow", displayTime);
    }

    private void HideWindow()
    {
        gameObject.SetActive(false);
    }
}
