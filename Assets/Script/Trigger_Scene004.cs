using UnityEngine;
using UnityEngine.SceneManagement;

public class Trigger_Scene004 : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // check gamemanager
            // lallu load scene
            if (Scene04Manager.Instance.CheckIsCollectAllHeart())
                SceneManager.LoadScene("Scene_05");
        }
    }
}
