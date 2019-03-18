using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour, SwitchInteractable
{
    // [SerializeField]
    // private float moveDistance; // scalar distance of how far the platform should travel in total

    public Transform startPoint;
    public Transform endPoint;

    private float moveDistance;

    [SerializeField]
    private float speed;

    public Vector3 direction; 
    private Vector3 normalizedDirection;

    private bool switchActivated;

    private float startTime;
    private Vector3 prevPosition;

    // Start is called before the first frame update
    void Start()
    {
        normalizedDirection = direction.normalized;
        switchActivated = false;
        startTime = Time.time;
        prevPosition = transform.position;
        moveDistance = Vector3.Distance(startPoint.position, endPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (switchActivated) {
            float distance = speed * (Time.time - startTime) + Vector3.Distance(startPoint.position, prevPosition);
            float frac = distance / moveDistance;
            transform.position = Vector3.Lerp(startPoint.position, endPoint.position, frac);
            // transform.Translate(normalizedDirection * speed * Time.deltaTime);
        }
        else {}
    }

    public void OnSwitchActivate() {
        switchActivated = true;
        startTime = Time.time;
        prevPosition = transform.position;
    }

    public void OnSwitchDeactivate() {
        switchActivated = false;
    }
}
