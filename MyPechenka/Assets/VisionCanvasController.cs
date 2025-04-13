using UnityEngine;

public class VisionCanvasController : MonoBehaviour
{
    private void Awake()
    {
        // ������������� �������� Canvas ��� ������
        gameObject.SetActive(false);
        Debug.Log("VisionCanvasController ���������������", this);
    }

    public void EnableCanvas()
    {
        gameObject.SetActive(true);
        Debug.Log("Canvas ����ר�", this);
    }
}