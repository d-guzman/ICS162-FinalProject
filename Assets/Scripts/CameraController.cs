using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform camFocus;  // What the camera is looking at. Almost always the player.
    public Transform currentCamera;
    public float CameraDistance = -10f;
    public bool invertVerticalControls = false;
    private Vector3 CameraPosition;

    private float rotateX;   // Numbers that are used to make the Quaternion 
    private float rotateY;

    private Quaternion nextRotation;

    void Start() {
        if (CameraDistance > 0) { CameraDistance = -CameraDistance; }
        CameraPosition = new Vector3(0f, 0f, CameraDistance);
    }

    void LateUpdate() {
        updatePivotPosition();
        rotateCamera();
        zoomCamera();
    }

    //Private Functions
    private void zoomCamera() {
        RaycastHit hitInfo;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        if (Physics.Raycast(transform.position, -transform.forward, out hitInfo, -CameraDistance, layerMask))
        {
            Vector3 CurrentPosition = currentCamera.localPosition;
            Vector3 NextPosition = new Vector3(0, 0, -hitInfo.distance+.03f);
            currentCamera.localPosition = Vector3.Lerp(CurrentPosition, NextPosition, 1f);
        }
        else
        {
            Vector3 CurrentPosition = currentCamera.localPosition;
            currentCamera.localPosition = Vector3.Lerp(CurrentPosition, CameraPosition, 1f);
        }
    }
    private void updatePivotPosition() {
        transform.position = camFocus.position;
    }
    private void rotateCamera() {
        rotateX += Input.GetAxis("CamHorizontal") * Time.deltaTime;
        if (invertVerticalControls)
            rotateY += -1 * Input.GetAxis("CamVertical") * Time.deltaTime;
        else
            rotateY += Input.GetAxis("CamVertical") * Time.deltaTime;
        rotateY = Mathf.Clamp(rotateY, -89.5f, 89.5f);

        nextRotation = Quaternion.Euler(rotateY, rotateX, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, nextRotation, 1f);
    }
}
