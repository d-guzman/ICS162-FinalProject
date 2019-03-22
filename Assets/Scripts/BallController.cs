using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Hitbox")) {
            other.isTrigger = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag.Equals("Hitbox")) {
            other.isTrigger = true;
        }
    }
}
