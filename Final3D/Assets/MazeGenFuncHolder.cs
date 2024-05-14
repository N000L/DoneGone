using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenFuncHolder : MonoBehaviour
{
    public GameObject cube;
    private ArrayTest valHolder;

    private void Start()
    {
        valHolder = this.GetComponent<ArrayTest>();
    }

    public void WallGen(int x)
    {
        for (int wallFor = 0; wallFor <= x; wallFor++)
        {
            Instantiate(cube, new Vector3(x, 0, wallFor ), Quaternion.identity);
            Instantiate(cube, new Vector3(0, 0, wallFor ), Quaternion.identity);
            Instantiate(cube, new Vector3(wallFor , 0, x ), Quaternion.identity);
            Instantiate(cube, new Vector3(wallFor, 0, 0), Quaternion.identity);
        }
    }

    
}
