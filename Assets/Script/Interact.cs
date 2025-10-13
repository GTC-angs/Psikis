using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class Interact : MonoBehaviour
{
    [System.Serializable]
    public struct ActionCallAndName
    {
        public string name;
        public UnityEvent action;
    }

    [System.Serializable]
    public enum InteractType
    {
        Collide, InputKey
    };


    public bool isInteract = false;
    public List<ActionCallAndName> actionChooses;

    public KeyCode keyCode;
    public InteractType interactType;
}
