﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPunchingScript : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hitbox")
        {
            Debug.Log("Player Punched: " + gameObject.name);
        }
    }
}
