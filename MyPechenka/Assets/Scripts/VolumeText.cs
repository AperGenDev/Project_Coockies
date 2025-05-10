using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeText : MonoBehaviour
{
    public Slider volumeSlider;  // Слайдер для громкости
    public TMP_Text volumeText;  // Текст для отображения громкости

    void Start()
    {
        // Инициализация текста с текущим значением громкости
        UpdateVolumeText(volumeSlider.value);

        // Подписка на изменение значений слайдера
        volumeSlider.onValueChanged.AddListener(UpdateVolumeText);
    }

    void UpdateVolumeText(float value)
    {
        // Обновляем текст с громкостью
        volumeText.text = "Громкость: " + Mathf.RoundToInt(value * 100).ToString() + "%";
    }
}
