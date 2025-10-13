using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance;
    public Vector3 offsetCamWithTarget = new Vector3(0, 10f, 0);
    public float camFollowSpeed = 1.4f; // speed camera follow, after offset
    public Transform target; // player
    public Camera cam;

    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        Instance = this;
    }
    void LateUpdate()
    {
        CamTrackingTarget();
    }

    /// <summary>
    /// sebuah fungsi untk menggerakan camera berdasarkan posisi player secara smooth
    /// </summary>
    void CamTrackingTarget()
    {
        if (target == null) return;

        // Smooth follow
        Vector3 desiredPosition = target.position + offsetCamWithTarget;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * camFollowSpeed);
    }
}
