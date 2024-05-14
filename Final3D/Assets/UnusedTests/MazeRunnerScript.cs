using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRunnerScript : MonoBehaviour
{

    public GameObject topFeeler;
    public GameObject bottomFeeler;
    public GameObject rightFeeler;
    public GameObject leftFeeler;
    public bool top;
    public bool bottom;
    public bool right;
    public bool left;
    public int x;
    public int y;
    
    

    // Update is called once per frame
    void Update()
    {
        top = topFeeler.GetComponent<TopFeelerScript>().topBool;
        bottom = bottomFeeler.GetComponent<BottomFeelerScript>().bottomBool;
        right = rightFeeler.GetComponent<RightFeelerScript>().rightBool;
        left = leftFeeler.GetComponent<LeftFeelerScript>().leftBool;
        
        
    }
    
}
