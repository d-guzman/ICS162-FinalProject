using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; //object the camera will be facing

    [Header("how far behind the player should the camera be?")]
    public Vector3 offset; //how far behind the player should the camera be?

    [Header("the pause that makes the camera movement feel smoooth")]
    public float cameraDelay; //the pause that makes the camera movement feel smoooth

    private float pitch = 0f;
    private float yaw = 0f;

    public float mouseXSpeed = 1f;
    public float mouseYSpeed = 1f;

   


    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform; //will find the player anywhere 
        Vector3 cameraPosition = player.position + offset;
        this.transform.position = cameraPosition; //moves the camera in the beginning

    }

    // Update is called once per frame
    void Update()
    {

    }


    void SetPosition()
    {
        Vector3 cameraPosition = player.position + offset;
        this.transform.position = Vector3.Lerp(this.transform.position, cameraPosition, cameraDelay * Time.deltaTime);
    }

    void SetRoation()
    {
         yaw += mouseYSpeed * Input.GetAxis("Mouse X");
        pitch -= mouseXSpeed * Input.GetAxis("Mouse Y");

        this.transform.eulerAngles = new Vector3(pitch, yaw, 0f); //use the mouse to rotate the camera 

    }


    private void FixedUpdate() //Fixed Update for Physics yup yup
    {
        SetPosition();
        SetRoation();

    }
}
