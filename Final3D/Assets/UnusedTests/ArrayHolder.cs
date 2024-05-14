using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArrayHolder : MonoBehaviour
{
    [System.Serializable]
    public struct rowData
    {
        //what the array holds
        public Vector2Int[] row;
    }

    //you can change 10 to var for different row lengths
    public rowData[] rows = new rowData[10];
}
