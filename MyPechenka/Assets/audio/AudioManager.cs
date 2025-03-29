using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Для использования UI элементов, таких как ползунок

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;  // Источник аудио для воспроизведения
    public AudioClip introMusic;     // Музыка для Intro
    public AudioClip mainMenuMusic;  // Музыка для главного меню
    public AudioClip gameMusic;      // Музыка для игры
    public AudioClip victoryMusic;   // Музыка при победе
    public AudioClip defeatMusic;    // Музыка при поражении
    public Slider volumeSlider;      // Ползунок для регулировки громкости

    private bool isPaused = false;   // Флаг для отслеживания паузы игры

    private void Start()
    {
        // Начнем с музыки Intro при запуске
        PlayIntroMusic();
        
        // Инициализация громкости из ползунка
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.AddListener(SetVolume);
            audioSource.volume = volumeSlider.value; // Устанавливаем начальную громкость
        }
    }

    // Метод для проигрывания музыки Intro
    private void PlayIntroMusic()
    {
        audioSource.clip = introMusic;
        audioSource.loop = false;  // Музыка Intro не зацикливается
        audioSource.Play();

        // Запускаем корутину для переключения на главное меню после 6.5 секунд
        StartCoroutine(PlayMainMenuAfterDelay(6.5f));
    }

    // Корутина для переключения на музыку главного меню с паузой
    private System.Collections.IEnumerator PlayMainMenuAfterDelay(float delay)
    {
        // Ждем указанное время
        yield return new WaitForSeconds(delay);

        // Пауза в 3 секунды перед началом музыки главного меню
        yield return new WaitForSeconds(3f);

        // Переключаем на музыку главного меню
        PlayMainMenuMusic();
    }

    // Метод для проигрывания музыки главного меню
    private void PlayMainMenuMusic()
    {
        audioSource.clip = mainMenuMusic;
        audioSource.loop = true;  // Зацикливаем музыку главного меню
        audioSource.Play();
    }

    // Метод для проигрывания музыки во время игры
    public void PlayGameMusic()
    {
        audioSource.clip = gameMusic;
        audioSource.loop = true;  // Зацикливаем музыку
        audioSource.Play();
    }

    // Метод для проигрывания музыки при победе
    public void PlayVictoryMusic()
    {
        audioSource.clip = victoryMusic;
        audioSource.loop = false; // Не зацикливаем музыку
        audioSource.Play();
    }

    // Метод для проигрывания музыки при поражении
    public void PlayDefeatMusic()
    {
        audioSource.clip = defeatMusic;
        audioSource.loop = false; // Не зацикливаем музыку
        audioSource.Play();
    }

    // Метод для приостановки музыки (например, при паузе игры)
    public void PauseMusic()
    {
        isPaused = true;
        audioSource.Pause(); // Приостанавливаем музыку
    }

    // Метод для возобновления музыки после паузы
    public void ResumeMusic()
    {
        isPaused = false;
        audioSource.Play(); // Возобновляем воспроизведение музыки
    }

    // Метод для регулировки громкости через ползунок
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;  // Изменяем громкость
    }

    // Метод для переключения музыки в зависимости от состояния игры
    public void OnGameStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.MainMenu:
                PlayMainMenuMusic();
                break;
            case GameState.InGame:
                PlayGameMusic();
                break;
            case GameState.Victory:
                PlayVictoryMusic();
                break;
            case GameState.Defeat:
                PlayDefeatMusic();
                break;
        }
    }
}

// Перечисление состояний игры
public enum GameState
{
    MainMenu,
    InGame,
    Victory,
    Defeat
}
