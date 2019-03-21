using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchSwitch : MonoBehaviour
{
    [Tooltip("The game object that has a component that inherits from SwitchInteractable.")]
    public GameObject interactableObject;
    private SwitchInteractable interactable;
    
    [SerializeField]
    private Collider collider; // may change later depending on actual model implementation!!!!

    private bool switchPunched;

    public Material[] materials;

    private MeshRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        switchPunched = false;
        if (collider == null) {
            collider = gameObject.GetComponent<Collider>();
        }
        interactable = interactableObject.GetComponent<SwitchInteractable>();

        renderer = collider.gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Hitbox")) {
            Debug.Log("first instantiated material name: " + renderer.material.name);
            Debug.Log("punch switch collided");
            // punching switch activates or deactivates interactable depending on what state the switch is in.
            // case where switch has not been punched
            if (!switchPunched) {
                renderer.material = materials[1];
                interactable.OnSwitchActivate();
            }
            // case where switch has been punched
            else {
                renderer.material = materials[0];
                interactable.OnSwitchDeactivate();
            }

            switchPunched = !switchPunched;    // swaps state
        }
    }

}
