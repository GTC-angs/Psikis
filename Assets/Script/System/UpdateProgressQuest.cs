using UnityEngine;

public class UpdateProgressQuest : MonoBehaviour
{

    [SerializeField] string KeyID_quest;
    [SerializeField] EventProgress typeProgress;
    [SerializeField] int amountProgress;
    enum EventProgress
    {
        CollisionEnter2d,
        TriggerEnter2d
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (typeProgress != EventProgress.CollisionEnter2d) return;

        if (QuestManager.Instance.activeQuest.IDKey == KeyID_quest)
        {
            QuestManager.Instance.UpdateQuest(KeyID_quest, amountProgress);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (typeProgress != EventProgress.TriggerEnter2d) return;

        if (QuestManager.Instance.activeQuest.IDKey == KeyID_quest)
        {
            QuestManager.Instance.UpdateQuest(KeyID_quest, amountProgress);
        }
    }
}
