using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftFeelerScript : MonoBehaviour
{
    public bool leftBool;
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Wall")
        {
            leftBool = false;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        leftBool = true;
    }
}
