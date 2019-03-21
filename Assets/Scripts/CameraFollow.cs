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



    private const float angleMin = -20f;
    private const float angleMax = 30f;

    private float pitch = 0f;
    private float yaw = 0f;

    public float mouseXSpeed = 1f;
    public float mouseYSpeed = 1f;

    private float rotationSpeed = 5f;


    private float distance = 10f;
    private float currentX = 0f;
    private float currentY = 0f;
    private float sensX = 4f;
    private float sensY = 1f;



    private Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform; //will find the player anywhere 
        Vector3 cameraPosition = player.position + offset;
        this.transform.position = cameraPosition; //moves the camera in the beginning
        cam = GetComponent<Camera>();

       // Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        currentX += Input.GetAxis("Mouse X") * mouseXSpeed;
        //currentY += Input.GetAxis("Mouse Y") * mouseYSpeed;
        //currentY = Mathf.Clamp(currentY, angleMin, angleMax);

    }


    void SetPosition()
    {

        //Vector3 dir = new Vector3(0f, 2.5f, -5f);
        Quaternion rotation = Quaternion.Euler(0f, currentX, 0);

        Vector3 cameraPosition = player.position + (rotation * offset);
        this.transform.position = Vector3.Slerp(this.transform.position, cameraPosition, cameraDelay * Time.deltaTime);
       // this.transform.position = player.position + (rotation * offset);
        this.transform.LookAt(player);

    }


    private void FixedUpdate() //Fixed Update for Physics yup yup
    {
        SetPosition();

    }
}
