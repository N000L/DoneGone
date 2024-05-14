using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightFeelerScript : MonoBehaviour
{
    public bool rightBool;
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Wall")
        {
            rightBool = false;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        rightBool = true;
    }
}
