using UnityEngine;

public class RoomCameraTrigger : MonoBehaviour
{
    public int cameraIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraManager.Instance.TurnOnCam(cameraIndex);
        }
    }
}
