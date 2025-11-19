using UnityEngine;

public class SpawnerRoomTrigger : MonoBehaviour
{
   [SerializeField] GameObject TriggerGameObject;
   [SerializeField]  Vector3 scaleObject = new Vector3(11,6,1);
   [SerializeField] int index;

    void Start()
    {
        GameObject TriggerGO = Instantiate(TriggerGameObject, transform.position, Quaternion.identity); 
        TriggerGO.transform.localScale = scaleObject;
        // TriggerGO.name = $"CamT_{gameObject.name}";

        TriggerGO.GetComponent<RoomCameraTrigger>().cameraIndex = index;
    }
}
