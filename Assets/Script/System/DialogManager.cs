using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public List<DialogSO> listDialogScene;
    public int indexDialog = 0;
    int indexBoxDialog = 0, countBoxDialog;
    bool isDialog = false, isCanClickNextDialog = false, isFinishDialog = false;
    DialogSO currentDialog;

    [Header("===UI===")]
    [SerializeField] TMP_Text dialogTextCharacter;
    [SerializeField] TMP_Text dialogTextWorld, characterNameTextLeft, characterNameTextRight;
    [SerializeField] RawImage worldImageTexture, characterImageLeft, characterImageRight;

    [Header("==Component==")]
    [SerializeField] CanvasGroup canvasGroupTextureWordlImage;
    [SerializeField] CanvasGroup canvasGroupWorldDialog;
    [SerializeField] CanvasGroup canvasGroupCharacterDialog;
    [SerializeField] CanvasGroup canvasGroupCharacterLeft, canvasGroupCharacterRight;


    [Header("===Audio===")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource audioAmbientSource;

    [Header("===Event===")]
    [SerializeField] UnityEvent AfterFinishDialog;

    void Update()
    {
        if (!isDialog || !isCanClickNextDialog) return; 

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(indexDialog);
            if (!isCanClickNextDialog) return;

            isCanClickNextDialog = false; // agar ga di spam
            string language = PlayerPrefs.GetString("lang", "ID");

            List<string> dialogWord = getTextByLanguage(language, currentDialog);
            AudioClip audioDialog = getAudioVoiceByLanguage(language, currentDialog);

            if (indexBoxDialog < countBoxDialog)
                //jika masi ada
                NextBoxDialog(dialogWord[indexBoxDialog], audioDialog, currentDialog.typeDialog);
            else
            {
                // jika box dalam satu dialog sudah habis
                DialogBoxWasFinished();
            }
        }
    }

    /// <summary>
    /// fungsi untuk kebutuhan di panggil script lain / ui
    /// </summary>
    public void StartDialog()
    {
        PlayDialog();
        isDialog = true;
        isCanClickNextDialog = true;
    }

    /// <summary>
    /// Fungsi ketika finish dalam dialog, masi contoh
    /// </summary>
    public void Finish()
    {
        Debug.Log("Finish dialogue..");
    }

    /// <summary>
    /// funsgi yang akan dijalankan pertama kali dan fungsi 
    /// yang akan dijalankan setiap kali questIndex bertambah ++
    /// </summary>
    public void PlayDialog()
    {
        DialogSO dialog = listDialogScene[indexDialog];
        currentDialog = dialog;

        audioAmbientSource.Stop();

        string language = PlayerPrefs.GetString("lang", "ID");

        List<string> dialogWord = getTextByLanguage(language, dialog);
        AudioClip audioDialog = getAudioVoiceByLanguage(language, dialog);

        // cek type
        if (dialog.typeDialog == DialogSO.TypeDialogs.World)
        {
            DialogWorld(dialog, dialogWord, audioDialog);
        }

        if (dialog.typeDialog == DialogSO.TypeDialogs.Character)
        {
            DialogCharacter(dialog, dialogWord, audioDialog);
        }
    }

    /// <summary>
    /// mengambil asset text string berdasarkan language
    /// </summary>
    /// <param name="getLanguage"></param>
    /// <param name="dialog"></param>
    /// <returns></returns>
    List<string> getTextByLanguage(string getLanguage, DialogSO dialog)
    {
        List<string> textList = new List<string> {};

        if (getLanguage == "ID")
        {
            textList = dialog.word_ID;
        }
        else if (getLanguage == "ENG")
        {
            textList = dialog.word_ENG;
        }

        return textList;
    }

    /// <summary>
    /// Mengambil asset voice sesuai dengan language
    /// </summary>
    /// <param name="getLanguage"></param>
    /// <param name="dialog"></param>
    /// <returns></returns>
    AudioClip getAudioVoiceByLanguage(string getLanguage, DialogSO dialog)
    {
        AudioClip audio = null;

        if (getLanguage == "ID")
        {
            audio = dialog.audioVoiceOffer_ID;
        }
        else if (getLanguage == "ENG")
        {
            audio = dialog.audioVoiceOffer_ENG;
        }

        return audio;
    }

    /// <summary>
    /// Fungsi yang berfungsi untuk setup jika type dialog SO nya adalah character dialog
    /// Fungsi ini di panggil ketika pertama dan ketika boxline habis
    /// </summary>
    /// <param name="dialog"></param>
    /// <param name="dialogBox"></param>
    /// <param name="audio"></param>
    public void DialogCharacter(DialogSO dialog, List<string> dialogBox, AudioClip audio)
    {
        // setup canvasgroup
        SetupCanvasGroup(dialog.typeDialog);

        // setup character
        canvasGroupCharacterLeft.alpha = 0;
        canvasGroupCharacterRight.alpha = 0;

        // jika foto texture dikiri
        if (dialog.positionTextureCharacter == DialogSO.PositionCharacterTexture.LEFT)
        {
            characterNameTextLeft.text = dialog.nameActor;
            characterImageLeft.texture = dialog.imageActor;
            characterImageLeft.rectTransform.sizeDelta = new Vector2(dialog.widthTextureCharacter, dialog.heightTextureCharacter);
            canvasGroupCharacterLeft.alpha = 1;
        }
        else
        {
            characterNameTextRight.text = dialog.nameActor;
            characterImageRight.texture = dialog.imageActor;
            characterImageRight.rectTransform.sizeDelta = new Vector2(dialog.widthTextureCharacter, dialog.heightTextureCharacter);
            canvasGroupCharacterRight.alpha = 1;
        }

        // reset logic terakhir
        indexBoxDialog = 0;
        countBoxDialog = dialogBox.Count;

        // isCanClickNextDialog = true;
        dialogTextCharacter.text = "";
        dialogTextWorld.text = "";

        // panggil untuk kebutuhan line selanjutnya
        NextBoxDialog(dialogBox[indexBoxDialog], audio, dialog.typeDialog);
    }

    /// <summary>
    /// Fungsi yang berfungsi untuk setup jika type dialog SO nya adalah world
    /// Fungsi ini di panggil ketika pertama dan ketika boxline habis
    /// </summary>
    /// <param name="dialog"></param>
    /// <param name="dialogBox"></param>
    /// <param name="audio"></param>
    public void DialogWorld(DialogSO dialog, List<string> dialogBox, AudioClip audio)
    {
        // setup canvasgroup
        SetupCanvasGroup(dialog.typeDialog);

        // setup texture
        if (dialog.isIncludeTexture) // jika texture untuk gambar tersedia
        {
            worldImageTexture.texture = dialog.imageWorld;
            worldImageTexture.rectTransform.sizeDelta = new Vector2(dialog.widthTexture, dialog.heightTexture); // set width & height
            worldImageTexture.rectTransform.position = dialog.positionTexture;
        }

        if (dialog.isIncludeTexture) canvasGroupTextureWordlImage.alpha = 1f;
        else canvasGroupTextureWordlImage.alpha = 0f;


        //reset logic last
        indexBoxDialog = 0;
        countBoxDialog = dialogBox.Count;
        // isCanClickNextDialog = true;


        audioAmbientSource.loop = true;
        if (dialog.isIcludeAmbientSound) // jika masukin ambient sound
        {
            audioAmbientSource.clip = dialog.ambientAudio;
            audioAmbientSource.Play();
        }

        dialogTextCharacter.text = "";
        dialogTextWorld.text = "";

        // panggil text boxline pertama
        NextBoxDialog(dialogBox[indexBoxDialog], audio, dialog.typeDialog);
    }

    /// <summary>
    /// Fungsi yang dijalankan untuk hide/show berdasarkan type dialog 
    /// </summary>
    /// <param name="type"></param>
    void SetupCanvasGroup(DialogSO.TypeDialogs type)
    {
        canvasGroupWorldDialog.alpha = 0f;
        canvasGroupCharacterDialog.alpha = 0f;

        if (type == DialogSO.TypeDialogs.World) canvasGroupWorldDialog.DOFade(1f, 0.2f);

        if (type == DialogSO.TypeDialogs.Character) canvasGroupCharacterDialog.DOFade(1f, 0.2f);
    }

    /// <summary>
    /// Fungsi yang akan dijalankan ketika satu box kata sudah selesai
    /// selanjutna akan mengecek dan lanjut ke dialog SO selanjutnya
    /// </summary>
    void DialogBoxWasFinished()
    {
        indexDialog++;
        currentDialog.customEventDialog?.Invoke();


        if (indexDialog < listDialogScene.Count) // jika dialog dalam List Masih ada
        {
            PlayDialog();
            Debug.Log('B');
        }

        else
        {
            Debug.Log('C');
            // kalo sudah habis semua dialog dalam scene
            isFinishDialog = true;
            AfterFinishDialog?.Invoke(); // panggil fungsi untuk selesai
        }
    }

    /// <summary>
    /// Fungsi yang berguna untuk menulis per line
    /// </summary>
    /// <param name="text"></param>
    /// <param name="audioVoice"></param>
    /// <param name="type"></param>
    public void NextBoxDialog(string text, AudioClip audioVoice, DialogSO.TypeDialogs type)
    {
        // jika masih ada 

        TMP_Text text_UI = null;
        if (type == DialogSO.TypeDialogs.Character) text_UI = dialogTextCharacter;
        else text_UI = dialogTextWorld;

        // setupp audio
        audioSource.loop = currentDialog.isLoop;
        audioSource.volume = currentDialog.volume;
        audioSource.pitch = currentDialog.pitch;

        audioSource.PlayOneShot(audioVoice);

        // typping effect
        StartCoroutine(TypingEffect(text, text_UI));
    }

    /// <summary>
    /// Fungsi untuk membuat text menulis per karakter
    /// </summary>
    /// <param name="textDialog"></param>
    /// <param name="textUI"></param>
    /// <param name="waitSecond"></param>
    /// <returns></returns>
    IEnumerator TypingEffect(string textDialog, TMP_Text textUI, float waitSecond = 0.05f)
    {

        foreach (char karakter in textDialog)
        {
            textUI.text += karakter;
            yield return new WaitForSeconds(waitSecond);
        }

        // stopp typing sound
        audioSource.Stop();

        // tambahkan index box
        indexBoxDialog++;
        isCanClickNextDialog = true; // set true agar player bisa klik dan lanjut dialog
    }

}
