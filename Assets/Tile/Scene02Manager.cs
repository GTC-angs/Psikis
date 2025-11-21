using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;


[System.Serializable]
public class Pasient
{
    public string name;
    public int umur;
    public enum Sex { LakiLaki, Perempuan }

    public Sex gender;
    public List<string> Synthomps;



}
public class Scene02Manager : MonoBehaviour
{
    // cutscene
    [Header("==Cutscene==")]
    [SerializeField] Light2D light, light2;

    public static Scene02Manager Instance;
    [SerializeField] List<AudioSource> envAudioSource;
    public AudioSource musicAudioRoom, paperSound, chooseSound;

    [SerializeField] List<DialogSO> dialog1, dialog2;

    void Start()
    {
        Instance = this;
    }

    public void CancelInteract()
    {
        PlayerInteractSystem.Instance.HideInteractUI();
        PlayerMovement.Instance.isCanMoveInput = true;
        PlayerInteractSystem.Instance.interactable = null;
    }

    public void TurnOffMusic()
    {
        CancelInteract();
        Debug.Log("Audio pause");
        musicAudioRoom.Pause();
    }

    public void TurnOnMusic()
    {
        musicAudioRoom.UnPause();
        CancelInteract();
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


    public void StartCutSceneStartWork()
    {
        // fade 1s
        // wait 2
        // change poss, kiara, cowo, camera
        // play dialog

        // cutscene rasyid
        // fade hitam

        // Headspace (2f)
        // Cut scene 
    }

    public IEnumerator PrepareForDialog()
    {
        light.intensity = 0;
        light2.intensity = 0;
        PlayerMovement.Instance.gameObject.SetActive(false);
        DialogManager.Instance.indexDialog = 0;
        DialogManager.Instance.listDialogScene = dialog1;
        DialogManager.Instance.AfterFinishDialog.AddListener(AfterDialog1);

        yield return new WaitForSeconds(11f);

        DialogManager.Instance.StartDialog();
    }

    public IEnumerator StopAudioIn(AudioSource audioSource, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.Stop();
    }
    
    void AfterDialog1()
    {
        ClearUiDialog();
        // cutscene rasyid

        SceneManager.LoadScene("Cutscene_004_tea");
    }

    void ClearUiDialog()
    {
        DialogManager.Instance.HideDialog();
    }

    public void PlaypaperSound()
    {
        paperSound.Stop();
        paperSound.Play();
        StartCoroutine(StopAudioIn(paperSound, 1.4f));
    }

    public void PlayChooseSound()
    {
        chooseSound.Stop();
        chooseSound.Play();
    }






}
