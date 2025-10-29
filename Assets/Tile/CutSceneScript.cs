using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneScript : MonoBehaviour
{
    [SerializeField] private string sceneNextName;
    [SerializeField] float delayTime = 0f;
    void Start()
    {
        StartCoroutine(WaitAndLoadScene());
    }

    IEnumerator WaitAndLoadScene()
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(sceneNextName);
    }
}
