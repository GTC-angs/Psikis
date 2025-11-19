using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

// woeld, setting, mission, character

public class HomeSceneManager : MonoBehaviour
{
    public CanvasGroup CG_bg;
    [SerializeField] RectTransform circelRect;
    [SerializeField] AudioSource audioSource, audioSourceHover, audioSourceClick;
    public static HomeSceneManager Instance;

    void Start()
    {
        Instance = this;
        circelRect.DOScale(new Vector3(50, 50, 50), 0.01f);
        circelRect.DOScale(new Vector3(1, 1, 1), 2f);
    }

    public void PlayHoverSfx()
    {
        audioSourceHover.volume = 1f;
        audioSourceHover.loop = false;
        audioSourceHover.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        audioSourceHover.Stop();
        audioSourceHover.Play();
    }

    public void PlayClickSfx()
    {
        audioSourceClick.volume = 1f;
        audioSourceClick.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        audioSourceClick.loop = false;
        audioSourceClick.Stop();
        audioSourceClick.Play();
    }

    public IEnumerator StartCrossFadingON(Action callb)
    {
        CG_bg.alpha = 0f;
        CG_bg.DOFade(1f, 0.3f);
        yield return new WaitForSeconds(0.3f);
        callb?.Invoke();
    }

    public IEnumerator StartCrossFadingOFF(Action callb)
    {
        CG_bg.alpha = 1f;
        CG_bg.DOFade(0f, 0.3f);
        yield return new WaitForSeconds(0.3f);
        callb?.Invoke();
    }


    // Handle UI Click
    public void NewGame()
    {
        PlayClickSfx();
        StartCoroutine(NewGameCoroutine());
    }

    IEnumerator NewGameCoroutine()
    {
        ScaleCircle(1f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Scene01");
    }
    void ScaleCircle(float duration)
    {
        circelRect.DOScale(new Vector3(50, 50, 50), duration);
    }

    void UnScaleCircle(float duration)
    {
        circelRect.DOScale(new Vector3(1, 1, 1), duration);
    }

    public void Quit()
    {
        PlayClickSfx();
        Application.Quit();
    }

    public void Option()
    {
        PlayClickSfx();
        SceneManager.LoadScene("OptionsUI", LoadSceneMode.Additive);
    }

    public void ShowLoadGame()
    {
        PlayClickSfx();
        SceneManager.LoadScene("SaveUI", LoadSceneMode.Additive);
    }

    public void HideLoadGame()
    {
        PlayClickSfx();
        SceneManager.UnloadSceneAsync("SaveUI");
    }

    public void Credit()
    {
        PlayClickSfx();
        StartCoroutine(CreditCoroutine());
    }

    IEnumerator CreditCoroutine()
    {
        ScaleCircle(1f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("CreditUI", LoadSceneMode.Additive);
    }
}
