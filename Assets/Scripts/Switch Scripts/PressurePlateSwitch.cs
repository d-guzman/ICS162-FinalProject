﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateSwitch : MonoBehaviour
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

    private void OnTriggerEnter(Collider collider) {
        interactable.OnSwitchActivate();
    }

    private void OnTriggerExit(Collider collider) {
        interactable.OnSwitchDeactivate();
    }
}
