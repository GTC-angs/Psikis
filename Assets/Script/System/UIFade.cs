using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
    public CanvasGroup canvasGroups; //Reference ke Canvas
    [SerializeField]
    private float fadeDuration; //Durasi fade
    public DialogManager dialogue; //Reference ke Script Dialogue
    private int fadeLog; //Index untuk fade(jika fade pertama maka variabel ini akan 0, jika kedua maka variabel ini = 1, dst....)
    

    [System.Serializable]
    public struct Fade
    {
        public int fadeIndex; //Fade Index untuk menyimpan kapan akan fade sesuai dengan input variabel index dialog pada script dialog
        public int fadeCondition; //Fade Condition adalah untuk menentukan kondisi fade(apakah fade in, fade out, atau menjadikan ui transparan atau tidak)
    }   public Fade[] fade; //Array untuk mengisi indeks
    


    void Update()
    {
        //Kondisi fade 
        if (fade[fadeLog].fadeIndex == dialogue.indexDialog)
        {
            if (fade[fadeLog].fadeCondition == 1) //Fade Out
                FadeFun(1, 0);
            else if (fade[fadeLog].fadeCondition == 2) //Fade In
                FadeFun(0, 1);
            else if (fade[fadeLog].fadeCondition == 3) //Set alpha = 1(Membuat object terlihat)
                canvasGroups.alpha = 1;
            else if (fade[fadeLog].fadeCondition == 4) //Set alpha = 0(Membuat object transparan)
                canvasGroups.alpha = 0;

            fadeLog++;
        }
    }



    //Fungsi untuk memanggil coroutine fade, parameter start merupakah awal kondisi gameobject
    //Parameter end adalah kondisi akhir gameobject(1 == terlihat, 0 = transparan)
    public void FadeFun(float start, float end)
    {
        StartCoroutine(FadeCanvas(canvasGroups, start, end));
    }
    
    //Coroutine untuk melakukan fade out dan fade in
    private System.Collections.IEnumerator FadeCanvas(CanvasGroup canvasGroup, float start, float end)
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, elapsed / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = end;
    }
}
