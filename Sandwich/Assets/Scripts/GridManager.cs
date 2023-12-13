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

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                float offsetX = x * (cellSize + spacing);
                float offsetY = y * (cellSize + spacing);

                Vector3 position = new Vector3(offsetX, 0, offsetY);
                grid[x, y] = Instantiate(breadPrefab, position, Quaternion.identity);
            }
        }
    }
}
