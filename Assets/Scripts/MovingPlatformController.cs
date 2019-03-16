using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour, SwitchInteractable
{
    [SerializeField]
    private float moveDistance; // scalar distance of how far the platform should travel in total

    [SerializeField]
    private float speed;

    public Vector3 direction; 
    private Vector3 normalizedDirection;

    private bool switchActivated;

    // Start is called before the first frame update
    void Start()
    {
        normalizedDirection = direction.normalized;
        switchActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSwitchActivate() {
        switchActivated = true;
    }

    public void OnSwitchDeactivate() {
        switchActivated = false;
    }
}
