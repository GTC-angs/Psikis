using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "QuestSO", menuName = "Scriptable Objects/QuestSO")]
public class QuestSO : ScriptableObject
{
    public string IDKey, questTitle, questHint;
    [TextArea] public string questDesc;

    public bool isCompleted = false, isActive = true, isShowProgress = true;

    public int requiredCount, currentCount = 0;

    public UnityEvent OnQuestCompleted;

     public void AddOnCompletedListener(UnityAction action)
    {
        OnQuestCompleted.AddListener(action);
    }
}
