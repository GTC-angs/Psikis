using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;
public class ScriptCredit : MonoBehaviour
{
    [SerializeField] RectTransform circelRect;

    void Start()
    {
        // circelRect.DOScale(new Vector3(1, 1, 1), 1f);
        StartCoroutine(BackToHomeWithTime());
    }

    IEnumerator BackToHomeWithTime()
    {
        yield return new WaitForSecondsRealtime(48f);
        SceneManager.LoadScene("Home");
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            BackToHome();
        }
    }
    public void BackToHome()
    {
        StartCoroutine(BackToHomeCoroutine());
    }

    IEnumerator BackToHomeCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        SceneManager.LoadScene("Home");
    }

}
