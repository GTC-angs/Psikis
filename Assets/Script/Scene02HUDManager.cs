using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Scene02HUDManager : MonoBehaviour
{
    [SerializeField] CanvasGroup CG_Canvas;
    [SerializeField] Animator AnimCanvas, AnimTextEvent;

    public static Scene02HUDManager Instance;

    void Start()
    {
        Instance = this;
        CG_Canvas.alpha = 0;
        CG_Canvas.blocksRaycasts = false;
        CG_Canvas.interactable = false;
    }

    public void ClickNextPasient()
    {
        AnimTextEvent_playAnimation("TextEvent_play");
    }


    void AnimTextEvent_playAnimation(string name)
    {
        AnimTextEvent.Play(name, 0, 0);
    }

    public void Showpasient_UI()
    {
        AnimCanvas.Play("Canvas_pasien_in", 0, 0);
    }

    public void Hidepasient_UI()
    {
        AnimCanvas.Play("Canvas_pasien_out", 0, 0);
        Scene02Manager.Instance.EndWork();
    }

    public void ClickPeriksa()
    {
        // load cutscene kan
        Hidepasient_UI();
        SceneManager.LoadSceneAsync("Cutscene_003");
    }
}
