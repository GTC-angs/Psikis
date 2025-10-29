using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingAudio : MonoBehaviour
{
    public Slider sliderMasterAudio, sliderBGM, sliderSfx, sliderDialog, sliderAmbient;
   

    public enum TypeSlider { Master, BGM, SFX, Dialog, Ambient }
    public AudioMixer audioMixer;

    void Start()
    {
        sliderMasterAudio.value = PlayerPrefs.GetFloat("master_audio", 0);
        sliderBGM.value = PlayerPrefs.GetFloat("bgm_audio", 0);
        sliderSfx.value = PlayerPrefs.GetFloat("sfx_audio", 0);
        sliderDialog.value = PlayerPrefs.GetFloat("dialog_audio", 0);
        sliderAmbient.value = PlayerPrefs.GetFloat("ambient_audio", 0);
    }

    public void OnSliderAudioChange(string typeSlider)
    {
        switch (typeSlider)
        {
            case "Master":
                float v = sliderMasterAudio.value;
                if (v <= -6) v = -99;
                
                PlayerPrefs.SetFloat("master_audio", v);
                audioMixer.SetFloat("volume_master", v);
                break;
            case "Bg":
                v = sliderBGM.value;
                if (v <= -6) v = -99;

                PlayerPrefs.SetFloat("bgm_audio", v);
                audioMixer.SetFloat("volume_bg", v);
                break;
            case "Sfx":
                v = sliderSfx.value;
                if (v <= -6) v = -99;

                PlayerPrefs.SetFloat("sfx_audio", v);
                audioMixer.SetFloat("volume_sfx", v);
                break;
            case "Dialog":
                v = sliderDialog.value;
                if (v <= -6) v = -99;

                PlayerPrefs.SetFloat("dialog_audio", v);
                audioMixer.SetFloat("volume_dialog", v);
                break;
            case "Ambient":
                v = sliderAmbient.value;
                if (v <= -6) v = -99;

                PlayerPrefs.SetFloat("ambient_audio", v);
                audioMixer.SetFloat("volume_ambient", v);
                break;
        }

    }


}
