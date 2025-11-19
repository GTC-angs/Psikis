using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingBasic : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public Toggle toggle;


    Resolution[] resolutions;

    void Start()
    {
        // toggle fullscreen
        bool isFull = PlayerPrefs.GetInt("fullscreen", 1) == 1;
        toggle.isOn = isFull;
        Screen.fullScreen = isFull;

        // resolusi
        resolutions = Screen.resolutions;

        dropdown.ClearOptions();

        foreach (var res in resolutions)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(res.width + " x " + res.height));
        }

        int savedIndex = PlayerPrefs.GetInt("resolutionIndex", resolutions.Length - 1);
        dropdown.value = savedIndex;
        dropdown.RefreshShownValue();

        ApplyResolution(savedIndex);

        dropdown.onValueChanged.AddListener(OnDropdownChanged);
        toggle.onValueChanged.AddListener(OnToggleChanged);
    }

    public void OnToggleChanged(bool isOn)
    {
        Screen.fullScreen = isOn;
        PlayerPrefs.SetInt("fullscreen", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void OnDropdownChanged(int index)
    {
        ApplyResolution(index);
        PlayerPrefs.SetInt("resolutionIndex", index);
        PlayerPrefs.Save();
    }

    void ApplyResolution(int index)
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
