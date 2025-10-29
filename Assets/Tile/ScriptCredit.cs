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
        circelRect.DOScale(new Vector3(1, 1, 1), 1f);
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
        circelRect.DOScale(new Vector3(100, 100, 100), 1f);
        yield return new WaitForSeconds(2f);
        SceneManager.UnloadSceneAsync("CreditUI");
    }

}
