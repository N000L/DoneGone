using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class MazeGenerator : MonoBehaviour
{
    //dims of maze
    [Range(5, 250)] public int mazeWidth = 5, mazeHeight = 5;
    //pos on with it starts from
    public int startX, startY;
    //array of maze cells 
    private MazeCell[,] maze;
    //current cell pos
    private Vector2Int currentCell;

    public Vector2Int neighbour;

    public MazeCell[,] GetMaze()
    {
        //MazeCell mazeData = gameObject.AddComponent<MazeCell>();
        
        
        //sets the maze to the vals no idea how this actually works
        maze = new MazeCell[mazeWidth, mazeHeight];
        
        //loops through the maze conditions
        for (int x = 0; x < mazeWidth; x++)
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                maze[x, y] = new MazeCell(x, y);
            }
        }
        CarvePath(startX,startY);

        return maze;
    }
    
    //list of directions to reference 
    private List<Direction> directions = new List<Direction>()
    {
        Direction.Up,
        Direction.Down,
        Direction.Right,
        Direction.Left,
    };

    List<Direction> GetRandomDirections()
    {
        //make a copy of our directions so we can mess with it
        List<Direction> dir = new List<Direction>(directions);
        
        //Make a directions list to put our randomized directions into
        List<Direction> rndDir = new List<Direction>();
        
        //removes from dir and puts it into rnddir
        while (dir.Count > 0)
        {
            int rnd = Random.Range(0, dir.Count);
            rndDir.Add(dir[rnd]);
            dir.RemoveAt(rnd);
        }

        return rndDir;
    }

    bool IsCellValid(int x, int y)
    {
        //check if cell is within parameters and has been checked
        if (x < 0 || y < 0 || x > mazeWidth - 1 || y > mazeHeight - 1 || maze[x,y].visited)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    Vector2Int CheckNeighbour()
    {
        List<Direction> rndDir = GetRandomDirections();

        for (int i = 0; i < rndDir.Count; i++)
        {
            //set neighbour coordinates to current cell
            neighbour = currentCell;
            
            //look through each dir while we loop through
            switch (rndDir[i])
            {
                case Direction.Up:
                    neighbour.y++;
                    break;
                case Direction.Down:
                    neighbour.y--;
                    break;
                case Direction.Right:
                    neighbour.x++;
                    break;
                case Direction.Left:
                    neighbour.x--;
                    break;
            }
        }

        if (IsCellValid(neighbour.x, neighbour.y))
        {
            return neighbour;
        }
        else
        {
            return currentCell;
        }
    }

    void BreakWalls(Vector2Int primaryCell, Vector2Int secondaryCell)
    {
        if (primaryCell.x > secondaryCell.x)
        {
           // Debug.Log(maze[primaryCell.x, primaryCell.y]);
        }
    }

    //starting at the x,y passed in, carve a pthat through the maze till it stops
    void CarvePath(int x, int y)
    {
        //perform a quick check to make sure the start is within parameters
        //if not, set them to 0 and throw warning

        if (x < 0 || y < 0 || x > mazeWidth - 1 || y > mazeHeight - 1)
        {
            x = y = 0;
            //Debug.Log("messed up start point default to 0");
        }

        //set cur cell to starting pos passed
        currentCell = new Vector2Int(x, y);

        //list of cur path
        List<Vector2Int> path = new List<Vector2Int>();
        
        //loop until dead end
        bool deadEnd = false;
        while (!deadEnd)
        {
            //get next cell we try
            Vector2Int nextCell = CheckNeighbour();
            
            //if no vailid neighbors, set deadend to true
            if (nextCell == currentCell)
            {
                for (int i = path.Count - 1; i >= 0; i--)
                {
                    BreakWalls(currentCell,nextCell);
                    //set cur cel to previous step back in path
                    currentCell = path[i];
                    //remove path before step back
                    path.RemoveAt(i);
                    //check if neighbours are valid
                    nextCell = CheckNeighbour();

                    if (nextCell != currentCell)
                    {
                        break;
                    }
                }

                if (nextCell == currentCell)
                {
                    deadEnd = true;
                }
            }
            else
            {
                //break wall would go here but since its cubes no walls will be broken
                maze[currentCell.x, currentCell.y].visited = true;
                //set cur cel to neighbour found
                currentCell = nextCell;
                //add cur cell to path
                path.Add(currentCell);
            }
        }



    }

    //look up what the hell enum is
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right,
    }
    
    
    
}

public class MazeCell
{
    public bool visited;
    public int x;
    public int y;
    

    //returns x and y as a vector2int 
    public Vector2Int position
    {
        get
        {
            return new Vector2Int(x, y);
        }
    }
    
    public MazeCell(int x, int y)
    {
        //x and y pos on maze grid
        this.x = x;
        this.y = y;
        
        //whether the algorithm has visited the cell
        visited = false;
        
        
    }
}
