using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSwitch : MonoBehaviour
{
    [Tooltip("The game object that has a component that inherits from SwitchInteractable.")]
    public GameObject interactableObject;
    private SwitchInteractable interactable;

    public Material turnedOffMaterial;
    public Material turnedOnMaterial;

    [SerializeField]
    private Collider trigger; // may change later depending on actual model implementation!!!!

    private MeshRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        if (trigger == null || !trigger.isTrigger) {
            trigger = gameObject.GetComponent<Collider>();
        }
        interactable = interactableObject.GetComponent<SwitchInteractable>();

        renderer = gameObject.GetComponent<MeshRenderer>();
        renderer.material = turnedOffMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Activates Interactable object when something enters its trigger with the right tag
    private void OnTriggerEnter(Collider collider) {
        Debug.Log(gameObject.name + " trigger was entered.");
        if (collider.gameObject.tag.Equals("Special object")) {  // NOTE: the "Special object" tag should be attached to any ball that is used to activate a switch
            interactable.OnSwitchActivate();
            renderer.material = turnedOnMaterial;
            collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            collider.transform.Translate(transform.position.x - collider.transform.position.x, 0, transform.position.z - collider.transform.position.z, Space.World);
            // collider.transform.Translate(collider.transform.position.x - transform.position.x, 0, collider.transform.position.z - transform.position.z);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag.Equals("Special object")) {  // NOTE: the "Special object" tag should be attached to any ball that is used to activate a switch
            interactable.OnSwitchActivate();
        }
    }

    private void OnTriggerExit(Collider collider) {
        Debug.Log(gameObject.name + " trigger was exited.");
        if (collider.gameObject.tag.Equals("Special object")) {
            interactable.OnSwitchDeactivate();
            renderer.material = turnedOffMaterial;
        }
    }
}
