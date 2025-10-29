using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene01Manager : MonoBehaviour
{
    public SpriteRenderer bed, lemari, pintu, pintu_km, meja;
    public Vector3 targetPlayerWakeUp;
    public BoxCollider2D BC2d_bed;
    void Start()
    {
        // setuup env
        lemari.color = new Color32(255, 255, 255, 0);
        pintu.color = new Color32(255, 255, 255, 0);
        pintu_km.color = new Color32(255, 255, 255, 0);
        BC2d_bed.enabled = false;

        SceneManager.LoadSceneAsync("Cutscene001", LoadSceneMode.Additive);

        StartCoroutine(PrePlay());
    }

    IEnumerator PrePlay()
    {
        PlayerMovement.Instance.isCanMoveInput = false;
        yield return new WaitForSeconds(13f);

        PlayerMovement.Instance.transform.position = targetPlayerWakeUp;
        BC2d_bed.enabled = true;
        PlayerMovement.Instance.isCanMoveInput = true;
    }
}
