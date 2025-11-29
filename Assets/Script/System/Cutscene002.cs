using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Cutscene002 : MonoBehaviour
{
    [SerializeField] public List<string> words;

    [SerializeField] private TMP_Text dialogueUI;
    [SerializeField] private float typingSpeed = 0.03f;
    [SerializeField] private float pauseDuration = 0.3f, durationWaitNewBuble = 6f;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] AudioSource audioSourceMusic, audioSourceDialogue;
    [SerializeField] string LoadNextSceneName = "Scene_02";
    [SerializeField] bool isLoadNextScene = true, isStartDialogOnStart = true;
    public int currentWordIndex = 0;

    void Start()
    {
        if (canvasGroup != null) canvasGroup.alpha = 0;
        if (isStartDialogOnStart)
            StartCoroutine(ShowDialogue(words[currentWordIndex]));
    }

    public IEnumerator ShowDialogue(string rawText)
    {
      
        dialogueUI.text = "";

        string parsedText = rawText;

        audioSourceDialogue.Play();
        for (int i = 0; i < parsedText.Length; i++)
        {
            // Detect pause (|)
            if (parsedText[i] == '|')
            {
                // Kalau double pipe (||) -> ganti bubble
                if (i < parsedText.Length - 1 && parsedText[i + 1] == '|')
                {
                    currentWordIndex++;
                    audioSourceDialogue.Stop();
                    yield return new WaitForSecondsRealtime(durationWaitNewBuble);
                    if (currentWordIndex >= words.Count)
                    {
                        // cutscene end
                        StartCoroutine(PrepareCloseCutscene());
                        break;
                    }

                    dialogueUI.text = "";
                    StartCoroutine(ShowDialogue(words[currentWordIndex]));
                    break;
                }
                else
                {
                    audioSourceDialogue.Stop();
                    yield return new WaitForSecondsRealtime(pauseDuration);
                    audioSourceDialogue.Play();
                }
                continue;
            }

            // Add letter typing
            dialogueUI.text += parsedText[i];
            // audioSourceDialogue.pitch = Random.Range(0.95f, 1f);
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }

    IEnumerator PrepareCloseCutscene()
    {
        if (audioSourceDialogue != null)
        {
            DOTween.To(
          () => audioSourceMusic.volume,
          x => audioSourceMusic.volume = x,
          0f,
          2f
      ).SetUpdate(true);
        }

        yield return new WaitForSecondsRealtime(2f);
        if (canvasGroup != null) canvasGroup.DOFade(1f, 2f);
        yield return new WaitForSecondsRealtime(2f);
        if (isLoadNextScene)
            SceneManager.LoadScene(LoadNextSceneName);
    }
}
