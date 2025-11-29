using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagerHUD : MonoBehaviour
{
    bool ispaused = false;
    [SerializeField] CanvasGroup CG_bg;
    [SerializeField] string mapName;
    [SerializeField] TMP_Text tMP_TextInfoNotif;
    [SerializeField] Animator notifAnimator;
    SaveSystem saveSystem;
    public static MenuManagerHUD Instance;

    [Header("Audio")]
    [SerializeField] AudioSource hoverAudioSource;
    [SerializeField] AudioSource selectAudioSource;
    void Start()
    {
        Instance = this;
        HideMenuPause();
        saveSystem = gameObject.GetComponent<SaveSystem>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    public void TogglePause()
    {
        ispaused = !ispaused;
        if (ispaused) ShowMenuPause();
        else HideMenuPause();
    }

    void ShowMenuPause()
    {
        ispaused = true;
        Saving();
        Time.timeScale = 0;
        CG_bg.alpha = 1f;
        CG_bg.interactable = true;
        CG_bg.blocksRaycasts = true;
    }

    void HideMenuPause()
    {
        ispaused = false;
        Time.timeScale = 1f;
        CG_bg.DOFade(0, 0.4f);
        CG_bg.interactable = false;
        CG_bg.blocksRaycasts = false;
    }

    public void Saving()
    {
        tMP_TextInfoNotif.text = "Saving...";
        notifAnimator.Play("show", 0, 0);
        saveSystem.SaveGame(1, mapName, () =>
        {
            notifAnimator.Play("show", 0, 0);
            tMP_TextInfoNotif.text = "Game Saved!";

            StartCoroutine(WaitAndDo(2f, () =>
            {
                notifAnimator.Play("hide", 0, 0);
            }));

        });
    }

    public void Setting()
    {
        SceneManager.LoadScene("OptionsUI", LoadSceneMode.Additive);
    }

    public void BackToHome()
    {
        SceneManager.LoadScene("Home");
    }

    IEnumerator WaitAndDo(float d, System.Action action)
    {
        yield return new WaitForSeconds(d);
        action?.Invoke();
    }

    public void PlayHoverAudio()
    {
        hoverAudioSource.Stop();
        hoverAudioSource.pitch = Random.Range(0.8f, 1.2f);
        hoverAudioSource.Play();
    }

    public void PlaySelectAudio()
    {
        selectAudioSource.Stop();
        selectAudioSource.pitch = Random.Range(0.8f, 1.2f);
        selectAudioSource.Play();

    }


}
