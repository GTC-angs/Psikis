using System.Collections.Generic;
using UnityEngine;

public class FontFix : MonoBehaviour
{
    public List<Font> fonts;
    void Start(){
        for (int i = 0; i < fonts.Count; i++)
        {
            fonts[i].material.mainTexture.filterMode = FilterMode.Point;
        }

        DontDestroyOnLoad(gameObject);
    }
}
