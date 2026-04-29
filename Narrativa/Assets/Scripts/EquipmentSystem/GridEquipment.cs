using UnityEngine;


public struct Cell
{
    public int x;
    public int y;
    public float width;
    public float height;
}

public class GridEquipment : MonoBehaviour
{
    [SerializeField] private int rows;
    [SerializeField] private int columns;

    [SerializeField] private Cell[] cells;

    [SerializeField] private GameObject cellImage;

    private void Awake()
    {
        
    }

    public void InitializeGrid()
    {
        cells = new Cell[rows * columns];

        for (int i = 0; i < cells.Length; i++)
        {
            cells[i] = new Cell();
            cells[i].x = i;
        }
    }

    private void DrawGrid()
    {
        
    }
}
