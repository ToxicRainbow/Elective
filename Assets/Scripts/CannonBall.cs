﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector3(0f, 0f, 500 * Time.fixedDeltaTime), Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "GameWall")
        {
            Destroy(gameObject);
        }
        if (other.tag == "Boat")
        {
            Destroy(gameObject);
            Debug.Log("hit");
        }
    }

}
