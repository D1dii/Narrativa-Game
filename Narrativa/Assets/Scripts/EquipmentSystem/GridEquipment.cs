using UnityEngine;


public struct Cell
{
    public int x;
    public int y;
}

public class GridEquipment : MonoBehaviour
{
    [SerializeField] private int rows;
    [SerializeField] private int columns;

    [SerializeField] private Cell[,] cells;

    [SerializeField] private GameObject cellImagePrefab;

    private void Awake()
    {
        InitializeGrid();
    }

    public void InitializeGrid()
    {
        cells = new Cell[rows, columns];

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                cells[i, j] = new Cell();
                cells[i, j].x = i;
                cells[i, j].y = j;
                DrawCell(i, j);
            }
        }
    }

    public void DrawCell(int i, int j)
    {
        var cellImage = Instantiate(cellImagePrefab);
        cellImage.transform.SetParent(transform);
        cellImage.GetComponent<RectTransform>().localPosition = new Vector3(-180 + 70 * i, 150 - 70 * j, 0);

    }
}
