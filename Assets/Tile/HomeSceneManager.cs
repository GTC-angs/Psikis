using UnityEngine;
using DG;
using DG.Tweening;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

// woeld, setting, mission, character

public class HomeSceneManager : MonoBehaviour
{
    public CanvasGroup CG_bg;
    [SerializeField] RectTransform circelRect;
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        circelRect.DOScale(new Vector3(50, 50, 50), 0.01f);
        circelRect.DOScale(new Vector3(1, 1, 1), 2f);
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
        StartCoroutine(NewGameCoroutine());
    }

    IEnumerator NewGameCoroutine()
    {
        ScaleCircle(1f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Cutscene001");
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
        Application.Quit();
    }

    public void Option()
    {
        SceneManager.LoadScene("OptionsUI", LoadSceneMode.Additive);
    }

    public void ShowLoadGame()
    {
        SceneManager.LoadScene("SaveUI", LoadSceneMode.Additive);
    }

    public void HideLoadGame()
    {
        SceneManager.UnloadSceneAsync("SaveUI");
    }

    public void Credit()
    {
        StartCoroutine(CreditCoroutine());
    }

    IEnumerator CreditCoroutine()
    {
        ScaleCircle(1f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("CreditUI", LoadSceneMode.Additive);
    }
}
