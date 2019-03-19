using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSwitch : MonoBehaviour
{
    [Tooltip("The game object that has a component that inherits from SwitchInteractable.")]
    public GameObject interactableObject;
    private SwitchInteractable interactable;

    [SerializeField]
    private Collider trigger; // may change later depending on actual model implementation!!!!
    // Start is called before the first frame update
    void Start()
    {
        if (trigger == null || !trigger.isTrigger) {
            trigger = gameObject.GetComponent<Collider>();
        }
        interactable = interactableObject.GetComponent<SwitchInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Activates Interactable object when something enters its trigger with the right tag
    private void OnTriggerEnter(Collider collider) {
        if (collider.gameObject.tag.Equals("Special object"))  // NOTE: the "Special object" tag should be attached to any ball that is used to activate a switch
            interactable.OnSwitchActivate();
    }

    private void OnTriggerExit(Collider collider) {
        if (collider.gameObject.tag.Equals("Special object"))
            interactable.OnSwitchDeactivate();
    }
}
