using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NUnit.Framework;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;


public class Scene05Manager : MonoBehaviour
{
    bool isPlayerDeath = false;
    public static Scene05Manager Instance;
    [SerializeField] List<AudioSource> envAudioSource;

    [Header("UI")]
    [SerializeField] CanvasGroup canvasGroupGameOver;

    [Header("Sound")]
    [SerializeField] AudioSource audioSourceBGM, audioSourceGameOver;

    [Header("Win cutscene")]
    [SerializeField] CanvasGroup canvasGroupWin, canvasGroupWinText;
    [SerializeField] TMP_Text textDialogKiara;
    [SerializeField] CameraFollow cameraFollow;

    Cutscene002 cutscene002;
    void Start()
    {
        Instance = this;
        canvasGroupGameOver.alpha = 0f;
        cutscene002 = gameObject.GetComponent<Cutscene002>();
    }

    void Update()
    {
        if (isPlayerDeath)
        {
            CheckInputGameOver();
        }
    }

    void CheckInputGameOver()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (MenuManagerHUD.Instance != null)
            {
                Time.timeScale = 1f;
                MenuManagerHUD.Instance.Saving();
                SceneManager.LoadScene("Home");
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Scene_05");
        }
    }

    public void StartWork()
    {
        PlayerMovement.Instance.isCanMoveInput = false;
        Scene02HUDManager.Instance.Showpasient_UI();
    }


    public void EndWork()
    {
        PlayerMovement.Instance.isCanMoveInput = true;
    }

    public void GameOver()
    {
        if (isPlayerDeath) return;
        isPlayerDeath = true;

        audioSourceBGM.DOFade(0, 0.4f);
        audioSourceGameOver.Stop();
        audioSourceGameOver.Play();

        canvasGroupGameOver.DOFade(1, 0.4f).OnComplete(() =>
        {
            Time.timeScale = 0;
        });

    }

    public void WinGame()
    {

        cutscene002.currentWordIndex = 0;
        cutscene002.words = new List<string>
        {
            "Marionate : Why you dont believe to me? ||",
            "... ||",
            "This is over? ||",
            "I feel Tired, But I find myself in better place. ||",
            "Thank you Kiara, you saving yourself. ||",
            "... ||",
            " ||"
        };

        StartCoroutine(CutsceneWin());
    }

    IEnumerator CutsceneWin()
    {
        cameraFollow.target = EnemyAttack.Instance.transform;
        yield return new WaitForSeconds(1.2f);
        textDialogKiara.color = Color.black;
        canvasGroupWin.DOFade(1f, 0.4f);



        StartCoroutine(cutscene002.ShowDialogue(cutscene002.words[0]));
        yield return new WaitForSeconds(20f);
        canvasGroupWinText.DOFade(1f, 0.3f);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("CreditUI");
    }

}
