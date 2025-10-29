using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ComicSystem : MonoBehaviour
{
    public List<ComicSO> listComicScene;
    bool isCanNext = true;
    public float delayPerScene = 0.3f;
    int index = 0;
    public UnityEvent OnEndedOfScene;

    [Header("==UI==")]
    [SerializeField] RawImage comicImage;
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        StartCoroutine(DisplayNextComic());
    }
    void Update()
    {
        if (!isCanNext) return;
        bool isActionNext = Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space);
        
        if (isActionNext) StartCoroutine(DisplayNextComic());
        
    }

    IEnumerator DisplayNextComic()
    {
        isCanNext = false;
        audioSource.Stop();

        if (index > listComicScene.Count - 1)
        {
            isCanNext = false;
            OnEndedOfScene?.Invoke();
            yield break;
        }
        // change texture 
        comicImage.texture = listComicScene[index].texture;
        audioSource.loop = listComicScene[index].isAudioLoop;


        if (listComicScene[index].isPlayAudio && listComicScene[index].audioClip != null)
        {
            audioSource.clip = listComicScene[index].audioClip;
            audioSource.Play();
        }

        yield return new WaitForSeconds(delayPerScene);
        index++;
        isCanNext = true;
    }

    public void ExampleEndOfComic()
    {
        Debug.LogWarning("You Succesfully Ended of Comic");
    }
}
