using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StoryTextAnimator : MonoBehaviour
{
public TextMeshProUGUI storyText;
[TextArea(4, 10)] public string fullText;
public float typeSpeed = 0.08f;
public float delayBeforeNextScene = 1f;
public string nextSceneName;
private void Start()
{
    StartCoroutine(ShowText());
}

IEnumerator ShowText()
{
    storyText.text = "";
    foreach (char c in fullText)
    {
        storyText.text += c;
        yield return new WaitForSeconds(typeSpeed);
    }

    // Ждём перед переходом, если указана сцена
    if (!string.IsNullOrEmpty("MemoryGame"))
    {
        yield return new WaitForSeconds(delayBeforeNextScene + 2f);
        SceneManager.LoadScene("MemoryGame");
    }
}
}