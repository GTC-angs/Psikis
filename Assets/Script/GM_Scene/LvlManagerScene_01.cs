using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LvlManagerScene_01 : MonoBehaviour
{
    // [SerializeField] GameObject ListTriggerQuestCollider;
    public static LvlManagerScene_01 Instance;
    [SerializeField] List<QuestSO> listQuest;

    void Start()
    {
        Instance = this;
        QuestSO quest1 = listQuest.Find(q => q.IDKey == "quest1"); // change berdasarakn keyID

        if (quest1 != null) quest1.AddOnCompletedListener(OnQuest_1_Completed);
    }

    
    public void OnQuest_1_Completed()
    {
        Debug.Log("Selesaii yeyy..");
    }
}
