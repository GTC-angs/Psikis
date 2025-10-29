using UnityEngine;
using DG;
using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine.SceneManagement;
public class CutScene001 : MonoBehaviour
{

    public CanvasGroup CG_bg, CG_textClock, CG_textBase;
    public TMP_Text clockText, baseText;
    public AudioSource audioSource;
    public List<AudioClip> audioClips;
    // sound indx : clock, alarm



    void Start()
    {
        StartCoroutine(Show());
    }

    IEnumerator Show()
    {
        yield return new WaitForSeconds(2f);
        // nyala
        // suara clock
        CG_textClock.alpha = 1;
        audioSource.clip = audioClips[0];
        audioSource.loop = true;
        audioSource.Play();

        // blick text
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(4f / 5);
            if (CG_textClock.alpha == 0) CG_textClock.alpha = 1f;
            else CG_textClock.alpha = 0f;
        }


        CG_bg.alpha = 0;
        CG_textClock.alpha = 1f;

        // change to 07:00
        clockText.text = "07:00";
        CG_bg.alpha = 1f;

        // suara alarm bunyi
        yield return new WaitForSeconds(0.3f);
        audioSource.clip = audioClips[1];
        audioSource.Play();

        // tunngu lalu unload
        yield return new WaitForSeconds(2f);

        // hide
        StartCoroutine(Hide(2f));
    }

    IEnumerator Hide(float dlay)
    {
        yield return new WaitForSeconds(dlay);
        audioSource.Stop();
        SceneManager.UnloadSceneAsync("Cutscene001");
    }


}
