using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateSwitch : MonoBehaviour
{
    [Tooltip("The game object that has a component that inherits from SwitchInteractable.")]
    public GameObject interactableObject;
    private SwitchInteractable interactable;

    public float depressionSize = 1.0f;

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

    private void OnTriggerEnter(Collider collider) {
        transform.Translate(-1 * depressionSize * Vector3.up, Space.World);
        renderer.material = turnedOnMaterial;
        interactable.OnSwitchActivate();
    }

    private void OnTriggerExit(Collider collider) {
        transform.Translate(depressionSize * Vector3.up, Space.World);
        renderer.material = turnedOffMaterial;
        interactable.OnSwitchDeactivate();
    }
}
