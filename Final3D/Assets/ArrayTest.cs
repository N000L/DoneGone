using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEditor;
using UnityEngine.UIElements.Experimental;
using Random = UnityEngine.Random;

public class ArrayTest : MonoBehaviour
{
    public int x;
    public int[,] grid;
    public Vector2Int curPos;
    public Vector2Int newPos;
    int limitCount = 0;
    Vector2Int[] dirs = { new Vector2Int(0,2) ,new Vector2Int(2,0), new Vector2Int(0,-2), new Vector2Int(-2,0) };
    private MazeGenFuncHolder funcHolder;
    public GameObject cube;
    public GameObject player;
    public List<Vector2Int> storedLocations = new List<Vector2Int>();
    private int pointsFallenBack = 1;
    private int testedDirections = 0;
    public GameObject teleporter;
    public PlayerData pdata;
    
    // Start is called before the first frame update
    void Start()
    {
        funcHolder = this.GetComponent<MazeGenFuncHolder>();
        pdata = GetComponent<PlayerData>();
        
        //generates the walls for the maze
        funcHolder.WallGen(x);
        
        grid = new int[x, x];
        
        //populates grid with 0
        for (int iy = 0; iy <= x-1; iy++)
        {
            //same for x axis
            for (int ix = 0; ix <= x-1; ix++)
            {
                grid[iy, ix] = 1;
            }
        }
        
        //makes inside maze
        pointsFallenBack = storedLocations.Count+1;
        
        //DebugPrintGrid();

        curPos = new Vector2Int(Random.Range(x/4, (x*3)/4), Random.Range(x/4, (x*3)/4));
        grid[curPos.x, curPos.y] = 0;
        player.transform.position = new Vector3(curPos.x, 0, curPos.y);
        
        populateMaze();
        
        InsideRender();
        ValidSpot();
    }

    void populateMaze()
    {
        if ((pointsFallenBack >= 0) && (limitCount < x*x))
        {
            //setting up values to ref later
            limitCount += 1;
            
            //grabs random vector2
            Vector2Int rndDir = dirs[Random.Range(0, 4)];
        
            //puts dir into new position
            newPos = curPos + rndDir;
            
            //checks if the dir is within the values of the maze
            newPos.x = GridClamp(newPos.x);
            newPos.y = GridClamp(newPos.y);
            
            //checks if the new position is able to have future movements
            for (int usableDirs = 0; usableDirs <= dirs.Length - 1; usableDirs++)
            {
                Vector2Int testPos = newPos + dirs[usableDirs];
                testPos.x = GridClamp(testPos.x);
                testPos.y = GridClamp(testPos.y);
                if (grid[testPos.x, testPos.y] == 0)
                {
                    testedDirections += 1;
                    Debug.Log(testedDirections + " invalid dir");
                    if (testedDirections == 4)
                    {
                        pointsFallenBack -= 1;
                        pointsFallenBack = Math.Clamp(pointsFallenBack, 0, 10000);
                        curPos = storedLocations[pointsFallenBack];
                        populateMaze();
                    }
                    break;
                }

                break;
            }
            
            //resets the directions tested
            testedDirections = 0;

            //resets points fallen back from the direction stack
            if (pointsFallenBack > 0)
            {
                pointsFallenBack = storedLocations.Count;
            }

            //checks if the locations has been checked before
            for (int i = 0; i <= storedLocations.Count - 1; i++)
            {
                if (newPos == storedLocations[i])
                {
                    populateMaze();
                }
                
            }
        
            //carves out the maze and starts the func again
            grid[newPos.x, newPos.y] = 0;
            grid[GridClamp(curPos.x + (rndDir.x/2)),GridClamp( curPos.y + (rndDir.y/2))] = 0;
            storedLocations.Add(curPos);
            curPos = newPos;
            //DebugPrintGrid();
            //Debug.Log(limitCount);
            populateMaze();
        }
        else
        {
            //Debug.Log("end maze gen");
        }
        
    }

    void DebugPrintGrid()
    {
        string printString = "";
        for (int iy = 0; iy <= x-1; iy++)
        {
            //same for x axis
            for (int ix = 0; ix <= x-1; ix++)
            {
               printString += " " + grid[ix,iy];
            }
            Debug.Log(printString);
            printString = "";
        }
    }
    
    public void InsideRender()
    {
        int howManyTimesDoesThisActuallyCarve = 0;
        for (int renderY = 0; renderY < x; renderY++)
        {
            //same for x axis
            for (int renderX = 0; renderX < x; renderX++)
            {
                if (grid[renderX, renderY] == 1)
                {
                    Instantiate(cube, new Vector3Int(renderX, 0, renderY), Quaternion.identity);
                    howManyTimesDoesThisActuallyCarve += 1;
                }
            }
            
        }
        Debug.Log(howManyTimesDoesThisActuallyCarve);
    }

    public void ValidSpot()
    {
        Vector2Int returnVal = new Vector2Int(6,6);
        int limitCheck = 0;
        while ((grid[(returnVal.x),returnVal.y] != 0) && (limitCheck < 100))
        {
            returnVal = new Vector2Int(Random.Range(1, x-1), Random.Range(1, x-1));
            limitCheck += 1;
        }
        
        Debug.Log(grid[(returnVal.x),returnVal.y] + " return value on grid " + limitCheck);
        Instantiate(teleporter, new Vector3(returnVal.x, -0.499f, returnVal.y), Quaternion.identity);
    }

    IEnumerator WasteTime()
    {
        yield return new WaitForSeconds(5);

    }

    public int GridClamp(int hell)
    {
        return Math.Clamp(hell, 0, x - 1);
    }

}
