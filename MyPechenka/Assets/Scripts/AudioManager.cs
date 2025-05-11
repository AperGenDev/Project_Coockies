using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource audioSource;

    public AudioClip mainMenuMusic;
    public AudioClip storyTellMusic;
    public AudioClip sampleSceneMusic;

    private void Awake()
    {
        // Singleton: не создавать второй экземпляр
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true; // Зацикливаем музыку
            audioSource.volume = PlayerPrefs.GetFloat("Volume", 1f); // Громкость из сохранения
        }
        else
        {
            Destroy(gameObject); // Удаляем дубликат
        }
    }

    private void Start()
    {
    	PlayMusicForCurrentScene();

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForCurrentScene();
    }

    private void PlayMusicForCurrentScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        AudioClip clipToPlay = null;

        switch (sceneName)
        {
            case "Menushka":
                clipToPlay = mainMenuMusic;
                break;
            case "StoryStart":
            case "StoryEnd":
            case "MemoryGame":
                clipToPlay = storyTellMusic;
                break;
            case "SampleScene":
                clipToPlay = sampleSceneMusic;
                break;
        }

        if (clipToPlay != null && audioSource.clip != clipToPlay)
        {
            audioSource.clip = clipToPlay;
            audioSource.Play();
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }
}
