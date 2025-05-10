using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PostMemoryNarration : MonoBehaviour
{
    [TextArea(5, 10)]
    public string storyText = "Рецепт в руках. Миллионер, разочарованный их сантиментами, уже хлопнул дверью. В тишине кухни повисло неловкое молчание.\n\n— «Так… значит, бабушка просто хотела, чтобы мы…» — начал Луи.\n\n— «Заткнись, — перебил Тони, хватая миксер. — Раз у нас один рецепт — давай проверим, кто лучше его приготовит!»";

    public float typingSpeed = 0.08f;
    public TMP_Text uiText; // или TMP_Text, если TMP
    public float delayBeforeNextScene = 3f;

    void Start()
    {
        uiText.text = "";
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        foreach (char letter in storyText)
        {
            uiText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(delayBeforeNextScene);

        // Загрузка следующей сцены
        SceneManager.LoadScene("SampleScene");
    }
}
