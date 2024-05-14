using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomFeelerScript : MonoBehaviour
{
    public bool bottomBool;
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Wall")
        {
            bottomBool = false;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        bottomBool = true;
    }
}
