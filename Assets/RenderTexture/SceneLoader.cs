using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string[] scenesToLoad; // isi di Inspector: misal UI, Audio, System

    void Start()
    {
        foreach (string scene in scenesToLoad)
        {
            if (!SceneManager.GetSceneByName(scene).isLoaded)
                SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
    }
}
