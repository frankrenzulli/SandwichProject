using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject breadPrefab;
    public GameObject lattucePrefab;
    public GameObject tomatoPrefab;

    public int gridSizeX;
    public int gridSizeY;
    public float cellSize;
    public float spacing;

    private GameObject[,] grid;

    private void Start()
    {
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        grid = new GameObject[gridSizeX, gridSizeY];
        int randomX = Random.Range(0, gridSizeX - 1);
        int randomY = Random.Range(0, gridSizeY - 1);
        Vector2Int breadPosition1 = new Vector2Int(randomX, randomY);
        Vector2Int breadPosition2 = GetAdjacentCell(breadPosition1);

        grid[breadPosition1.x, breadPosition1.y] = Instantiate(breadPrefab, GetPosition(breadPosition1), Quaternion.identity);
        grid[breadPosition2.x, breadPosition2.y] = Instantiate(breadPrefab, GetPosition(breadPosition2), Quaternion.identity);

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                if (grid[x, y] != null)
                {
                    continue;
                }

                Vector3 position = GetPosition(new Vector2Int(x, y));
                GameObject randomPrefab = GetRandomPrefab();
                grid[x, y] = Instantiate(randomPrefab, position, Quaternion.identity);
            }
        }
    }

    private Vector3 GetPosition(Vector2Int cell)
    {
        return new Vector3(cell.x * (cellSize + spacing), 0, cell.y * (cellSize + spacing));
    }

    private Vector2Int GetAdjacentCell(Vector2Int originalCell)
    {
        int xOffset, yOffset;

        if (Random.Range(0, 2) == 0)
        {
            xOffset = 1;
            yOffset = 0;
        }
        else
        {
            xOffset = 0;
            if (Random.Range(0, 2) == 0)
            {
                yOffset = 1;
            }
            else
            {
                yOffset = -1;
            }
        }

        int newX = Mathf.Clamp(originalCell.x + xOffset, 0, gridSizeX - 1);
        int newY = Mathf.Clamp(originalCell.y + yOffset, 0, gridSizeY - 1);

        return new Vector2Int(newX, newY);
    }

    private GameObject GetRandomPrefab()
    {
        if (Random.Range(0, 2) == 0)
        {
            return lattucePrefab;
        }
        else
        {
            return tomatoPrefab;
        }
    }
}
