using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Scene01Manager : MonoBehaviour
{
    public SpriteRenderer bed, lemari, pintu, pintu_km, meja;
    public Vector3 targetPlayerWakeUp;
    public Quaternion targetPlayerWakeUpRotation;
    public BoxCollider2D BC2d_bed, BC2d_door, BC2d_doorkm;

    public GameObject TutorialUIMove, TutorialUIInteract;

    // door_km, door, cabinet
    public List<DoorWithInt> InteractionScripts;

    // dialogs
    public List<DialogSO> dialogs1;

    // Light
    [SerializeField] Light2D light2D;

    public static Scene01Manager Instance;
    [SerializeField] List<AudioSource> envAudioSource;
    void Start()
    {
        Instance = this;
        // setuup env
        // lemari.color = new Color32(255, 255, 255, 0);
        pintu.color = new Color32(255, 255, 255, 0);
        pintu_km.color = new Color32(255, 255, 255, 0);
        BC2d_door.enabled = false;
        BC2d_doorkm.enabled = false;

        BC2d_bed.enabled = false;

        SceneManager.LoadSceneAsync("Cutscene001", LoadSceneMode.Additive);

        StartCoroutine(PrePlay());
    }

    IEnumerator PrePlay()
    {
        light2D.intensity = 0.2f;
        PlayerMovement.Instance.isCanMoveInput = false;
        yield return new WaitForSeconds(11f);
        // cutscene ended

        envAudioSource[0].Play();
        yield return new WaitForSeconds(2f);

        PlayerMovement.Instance.transform.position = targetPlayerWakeUp;
        PlayerMovement.Instance.transform.rotation = targetPlayerWakeUpRotation;

        lemari.color = new Color32(255, 255, 255, 255);

        BC2d_bed.enabled = true;
        PlayerMovement.Instance.isCanMoveInput = true;


        yield return new WaitForSeconds(4f);
        TutorialUIMove.SetActive(false);
        TutorialUIInteract.SetActive(true);
    }

    public void StopAlarm(AudioSource audioSource)
    {
        StartCoroutine(StopAlarmCoroutine(audioSource));
    }

    IEnumerator StopAlarmCoroutine(AudioSource audioSource)
    {
        audioSource.Stop();
        // PlayerMovement.Instance.UpdateDirection(new Vector2(0, -1));
        PlayerAnimationController.Instance.animator.Play("idle_bottom");
        yield return new WaitForSeconds(0.7f);
        light2D.intensity = 0f;
        PlayerMovement.Instance.isCanMoveInput = false;
        PlayerMovement.Instance.transform.position = targetPlayerWakeUp;
        PlayerMovement.Instance.transform.rotation = targetPlayerWakeUpRotation;

        // lemari.color = new Color32(255, 255, 255, 255);
        pintu.color = new Color32(255, 255, 255, 255);
        pintu_km.color = new Color32(255, 255, 255, 255);
        BC2d_door.enabled = true;
        BC2d_doorkm.enabled = true;

        yield return new WaitForSeconds(4f);
        PlayerMovement.Instance.isCanMoveInput = false;
        light2D.intensity = 4f;

        DialogManager.Instance.listDialogScene = dialogs1;
        DialogManager.Instance.StartDialog();

        DialogManager.Instance.AfterFinishDialog.AddListener(AfterDialog1);
    }

    void AfterDialog1()
    {
        Debug.Log("OK AMAN");
        PlayerMovement.Instance.isCanMoveInput = true;
        DialogManager.Instance.HideDialog();

        InteractionScripts[0].isCanInteract = false;
        InteractionScripts[1].isCanInteract = true;
        InteractionScripts[2].isCanInteract = false;
    }

    public void OpenDoorKM() // cutscene 2
    {
        SceneManager.LoadSceneAsync("Cutscene_002", LoadSceneMode.Additive);
        AfterOpenDoorKM();
    }

    public void AfterOpenDoorKM()
    {
        Debug.Log("DONE OK AMAN");
        // set animation to to
        PlayerAnimationController.Instance.animator.Play("idle_bottom");
        InteractionScripts[0].isCanInteract = true; // door keluar
        InteractionScripts[1].isCanInteract = true; // door km
        InteractionScripts[2].isCanInteract = true; // cabinet

    }

    public void OpenCabinet()
    {
        Debug.Log("Cabinet interact sucess");
        PlayerMovement.Instance.isCanMoveInput = false;
        light2D.intensity = 0f;
        float time = 0;

        while (time < 2f)
        {
            time += Time.deltaTime;
        }

        PlayerMovement.Instance.isCanMoveInput = true;
        DOTween.To(() => light2D.intensity,
        val => light2D.intensity = val,
        4f, // endvalue
        0.4f); // duration

        InteractionScripts[0].isCanInteract = true; // door keluar
        InteractionScripts[1].isCanInteract = false; // door km
        InteractionScripts[2].isCanInteract = false; // cabinet

    }

    public void OpenDoorOut()
    {
        // set new transform player

        InteractionScripts[0].isCanInteract = false; // door keluar
        InteractionScripts[1].isCanInteract = false; // door km
        InteractionScripts[2].isCanInteract = false; // cabinet

        DOTween.To(() => light2D.intensity, (val) => light2D.intensity = val, 0, 1.5f);

        SceneManager.LoadScene("Scene_02");
    }


}
