using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateSwitch : MonoBehaviour
{
    public GameObject interactableObject;
    private SwitchInteractable interactable;
    private Collider trigger;

    // Start is called before the first frame update
    void Start()
    {
        trigger = gameObject.GetComponent<Collider>();
        interactable = interactableObject.GetComponent<SwitchInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter() {
        interactable.OnSwitchActivate();
    }

    private void OnTriggerExit() {
        interactable.OnSwitchDeactivate();
    }
}
