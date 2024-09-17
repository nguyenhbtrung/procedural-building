using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private int dimensions;
    [SerializeField] private int nRoad;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GameObject roadPrefab;
    [SerializeField] private GameObject landPrefab;

    private List<Cell> cells;
    private int maxTurns = 4;
    

    private void Awake()
    {
        cells = new List<Cell>();
        InitGrid();
    }

    public void InitGrid()
    {
        for (int y = 0; y < dimensions; y++)
        {
            for (int x = 0; x < dimensions; x++)
            {
                Vector3 position = new Vector3(x * 20, 0, y * 20);
                Cell newCell = Instantiate(cellPrefab, position, Quaternion.identity, this.transform)
                    .GetComponent<Cell>();
                newCell.X = x; 
                newCell.Y = y;
                cells.Add(newCell);
            }
        }

        StartCoroutine(CalculateTilesData());
    }

    public IEnumerator CalculateTilesData()
    {
        for (int i = 0; i < nRoad; i++)
        {
            int nTurn = Random.Range(0, maxTurns) + 1;
            int currentPosX = 0;
            int currentPosY = 0;
            GetRandomStartPos(ref currentPosX, ref currentPosY);
            Cell startCell = GetCellAtPosition(currentPosX, currentPosY);
            startCell.CellType = CellType.Road;

            int turnCount = 0;
            int stepCount = 0;
            int nforwardStep = GetRandomRoadSteps();
            int direction = GetNewDirection(currentPosX, currentPosY);
            while (stepCount < dimensions * dimensions)
            {
                if (stepCount == nforwardStep && turnCount < nTurn)
                {
                    stepCount = 0;
                    nforwardStep = GetRandomRoadSteps();
                    direction = GetNewDirection(currentPosX, currentPosY);
                    turnCount++;
                }
                GetNextPos(ref currentPosX, ref currentPosY, direction);
                if (currentPosX < 0 || currentPosY < 0 || currentPosX >= dimensions || currentPosY >= dimensions)
                {
                    break;
                }
                Cell currentCell = GetCellAtPosition(currentPosX, currentPosY);
                currentCell.CellType = CellType.Road;
                stepCount++;
            }
            yield return null;
        }
        GenerateTiles();
    }

    public Cell GetCellAtPosition(int x, int y)
    {
        return cells.Find(cell => cell.X == x && cell.Y == y);
    }

    public void GenerateTiles()
    {
        foreach (Cell cell in cells)
        {
            if (cell.CellType == CellType.Road)
            {
                Instantiate(roadPrefab, cell.transform.position, roadPrefab.transform.rotation);
            }
            else if (cell.CellType == CellType.Land)
            {
                Instantiate(landPrefab, cell.transform.position, landPrefab.transform.rotation);
            }
        }
        //Destroy(gameObject);
    }

    public int GetRandomRoadSteps()
    {
        int minSteps = 2;
        int maxStep = dimensions / 3 * 2;
        return Random.Range(minSteps, maxStep);
    }

    public void GetNextPos(ref int x, ref int y, int direction)
    {
        switch (direction)
        {
            case 0: y += 1; break; //up
            case 1: y -= 1; break; //down
            case 2: x += 1; break; //right
            case 3: x -= 1; break; //left
            default:
                Debug.LogWarning("Invalid direction");
                break;
        }
    }

    public void GetRandomStartPos(ref int x, ref int y)
    {
        x = 0;
        y = 0;
        int selection = Random.Range(0, 2);
        if (selection == 0)
        {
            x = Random.Range(1, dimensions);
        }
        else
        {
            y = Random.Range(1, dimensions);
        }
    }

    public int GetNewDirection(int x, int y)
    {
        int direction = Random.Range(0, 4);
        if (y == 0)
        {
            direction = 0;
        }
        if (y == dimensions - 1)
        {
            direction = 1;
        }
        if (x == 0)
        {
            direction = 2;
        }
        if (x == dimensions - 1)
        {
            direction = 3;
        }
        return direction;
    }
    

}
