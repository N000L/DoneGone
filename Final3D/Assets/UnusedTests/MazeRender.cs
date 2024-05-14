using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRender : MonoBehaviour
{
    [SerializeField] private MazeGenerator mazeGenerator;
    [SerializeField] private GameObject mazeCellPrefab;

    
    //gaps between each cell
    public float cellSize = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        MazeCell[,] maze = mazeGenerator.GetMaze();

        for (int x = 0; x < mazeGenerator.mazeWidth; x++)
        {
            for (int y = 0; y < mazeGenerator.mazeHeight; y++)
            {
                //new cell as child of main object
                GameObject newCell = Instantiate(mazeCellPrefab,
                    new Vector3(((float)x * cellSize), 0f, ((float)y * cellSize)), Quaternion.identity, transform);

                MazeCellObject mazeCell = newCell.GetComponent<MazeCellObject>();

                

                if (y > 500)
                {
                    break;
                }

            }
            if (x > 500)
            {
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
