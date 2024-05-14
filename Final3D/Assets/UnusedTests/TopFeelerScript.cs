using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopFeelerScript : MonoBehaviour
{

    public bool topBool;
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Wall")
        {
            topBool = false;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        topBool = true;
    }
}
