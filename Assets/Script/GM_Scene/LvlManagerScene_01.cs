using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LvlManagerScene_01 : MonoBehaviour
{
    // template LvlManager
    public static LvlManagerScene_01 Instance;
    [SerializeField] List<QuestSO> listQuest;

    void Start()
    {
        Instance = this;
        AddListenerToQuestWithKey("quest1", OnQuest_1_Completed);
    }

    /// <summary>
    /// fungsi yang dijalankan ketika Quest selesai
    /// </summary>
    public void OnQuest_1_Completed()
    {
        Debug.Log("Selesaii yeyy..");
    }

    /// <summary>
    /// Sebuah fungsi untuk menambahkna action pada suatu quest berdasarakan keyID
    /// </summary>
    /// <param name="key"></param>
    /// <param name="action"></param>
    public void AddListenerToQuestWithKey(string key, Action action)
    {
        // search
        QuestSO quest1 = listQuest.Find(q => q.IDKey == key); // change berdasarakn keyID
        // menambahkan event
        if (quest1 != null) quest1.AddOnCompletedListener(OnQuest_1_Completed);
    }
}
