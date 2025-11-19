using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] List<GameObject> Cameras;
    [SerializeField] int activeCamIndex;

    public static CameraManager Instance;
    void Start()
    {
        Instance = this;

        float t = 0;
        while (t < 5f)
        {
            t += Time.deltaTime;
            Debug.Log("Belomm");
        }

        // TurnOffAllCam();
        // TurnOnCam(activeCamIndex);
    }

    public void TurnOffAllCam()
    {
        foreach (GameObject cam in Cameras)
        {
            cam.SetActive(false);
        }
    }

    public void TurnOnCam(int i)
    {
        TurnOffAllCam();
        Cameras[i].SetActive(true);
        activeCamIndex = i;
    }
}
