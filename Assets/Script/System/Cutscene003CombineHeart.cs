using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene003CombineHeart : MonoBehaviour
{
    Cutscene002 cutscene002;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cutscene002 = gameObject.GetComponent<Cutscene002>();
        WaitAndDo(9f, () =>
        {
            cutscene002.StartCoroutine(cutscene002.ShowDialogue(cutscene002.words[cutscene002.currentWordIndex]));
        });
    }

    IEnumerator WaitAndDo(float d, Action action)
    {
        yield return new WaitForSecondsRealtime(d);
        action?.Invoke();
    }

    public void StartDialogue()
    {
        cutscene002.StartCoroutine(cutscene002.ShowDialogue(cutscene002.words[cutscene002.currentWordIndex]));
    }


    public void StartDialogue2()
    {
        List<string> words = new List<string>
        {
            "W-what? ||",
            "Marrionate : Thats it? Youre letting go that easily? || Throwing him away just like that? HAH! || No wonder he’d rather be with her ||",
            "That’s not how it’s- I… I have a say too! || ",
            "Marrionate : You think you do? ||",
            "Marrionate : Selfish girl ||",
            "Marrionate : After all he’d done ||",
            "Marrionate : He knows best ||",
            "No… thats || ",
        };
        cutscene002.currentWordIndex = 0;
        cutscene002.words = words;
        cutscene002.StartCoroutine(cutscene002.ShowDialogue(cutscene002.words[cutscene002.currentWordIndex]));
    }

    public void LoadLevel5()
    {
        SceneManager.LoadScene("Scene_05");
    }



}
