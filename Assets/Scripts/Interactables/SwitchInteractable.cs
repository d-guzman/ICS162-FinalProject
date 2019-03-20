using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Have all interactables inherit from this interface.
public interface SwitchInteractable 
{
    // defines what happens when activated by a switch.
    void OnSwitchActivate();

    // defines what happens when activated by a switch.
    void OnSwitchDeactivate();
}
