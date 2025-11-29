using UnityEngine;
using System.IO;
using Model.SaveData;
using System.Collections;
using System;
public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;

    void Awake()
    {
        Instance = this;
    }

    public void SaveGame(int id, string mapName, Action onComplete = null)
    {
        StartCoroutine(CaptureAndSave(id, mapName, onComplete));
    }

    private IEnumerator CaptureAndSave(int id, string mapName, Action onComplete = null)
    {
        yield return new WaitForEndOfFrame();

        // Buat folder save kalau belum ada
        string folder = Application.persistentDataPath + "/saves/";
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        // Screenshot name
        string screenshotName = $"save_{id}.png";
        string fullImagePath = folder + screenshotName;

        // Capture screenshot
        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tex.Apply();

        File.WriteAllBytes(fullImagePath, tex.EncodeToPNG());
        Debug.Log("Screenshot saved: " + fullImagePath);

        // Save JSON
        SaveData data = new SaveData();
        data.id = id;
        data.mapName = mapName;
        data.date = System.DateTime.Now.ToString("dd/MM/yyyy - HH:mm");
        data.screenshotName = screenshotName;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(folder + $"save_{id}.json", json);

        Debug.Log("Game Saved: " + folder + $"save_{id}.json");
        onComplete?.Invoke();
    }
}
