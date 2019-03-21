using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour, SwitchInteractable
{


    public Animation LeftDoorOpen;
    //public Animation RightDoorOpen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSwitchActivate() {
        Debug.Log("Door has been activated");
       // LeftDoorOpen.Play();
    }
    

    public void OnSwitchDeactivate() {
        Debug.Log("Goodbye, door");
    }
}
