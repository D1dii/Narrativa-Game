using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public struct Cell
{
    public int x;
    public int y;
    public bool isOccupied;
}

public class GridEquipment : MonoBehaviour
{
    [SerializeField] private int rows;
    [SerializeField] private int columns;

    public Cell[,] cells;

    [SerializeField] private GameObject cellImagePrefab;

    public List<GameObject> cellSlots = new List<GameObject>();

    private Color cellColor;

    private void Awake()
    {
        InitializeGrid();
        cellColor = cellImagePrefab.GetComponent<Image>().color;
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
                cells[i, j].isOccupied = false;
                DrawCell(i, j);
            }
        }
    }

    public void DrawCell(int i, int j)
    {
        var cellImage = Instantiate(cellImagePrefab);
        cellImage.transform.SetParent(transform);
        cellImage.GetComponent<RectTransform>().localPosition = new Vector3(-180 + 70 * i, 150 - 70 * j, 0);
        cellImage.GetComponent<CellSlot>().Initialize(i, j);
        cellImage.name = $"Cell_{i}_{j}";

        cellSlots.Add(cellImage);
    }

    public void PlaceObject(GameObject dragObject, CellInfo cellInfo)
    {
        dragObject.GetComponent<RectTransform>().localPosition = new Vector3(-145 + 70 * cellInfo.x, 115 - 70 * cellInfo.y, 0);

        for (int i = 0; i < cellSlots.Count; i++)
        {
            var cellSlot = cellSlots[i].GetComponent<CellSlot>();
            if (cellSlot.X >= cellInfo.x && cellSlot.X < cellInfo.x + cellInfo.width &&
                cellSlot.Y >= cellInfo.y && cellSlot.Y < cellInfo.y + cellInfo.height)
            {
                cellSlot.IsOccupied = true;
            }
        }

        ColorSelectedCells(cellInfo);

        var item = dragObject.GetComponent<IBaseItem>();
        InventoryManager.Instance.AddItem(item);

    }

    public void UnPlaceObject(GameObject dragObject, CellInfo cellInfo)
    {
        for (int i = 0; i < cellSlots.Count; i++)
        {
            var cellSlot = cellSlots[i].GetComponent<CellSlot>();
            if (cellSlot.X >= cellInfo.x && cellSlot.X < cellInfo.x + cellInfo.width &&
                cellSlot.Y >= cellInfo.y && cellSlot.Y < cellInfo.y + cellInfo.height)
            {
                cellSlot.IsOccupied = false;
            }
        }

        ClearCellColors();

        var item = dragObject.GetComponent<IBaseItem>();
        InventoryManager.Instance.RemoveItem(item);
    }

    public void ColorSelectedCells(CellInfo cellInfo)
    {
        for (int i = 0; i < cellSlots.Count; i++)
        {
            var cellSlot = cellSlots[i].GetComponent<CellSlot>();
            if (cellSlot.X >= cellInfo.x && cellSlot.X < cellInfo.x + cellInfo.width &&
                cellSlot.Y >= cellInfo.y && cellSlot.Y < cellInfo.y + cellInfo.height)
            {
                cellSlots[i].GetComponent<Image>().color = Color.green;
            }
            else
            {
                cellSlots[i].GetComponent<Image>().color = cellColor;
            }

            if (cellSlot.IsOccupied)
            {
                cellSlots[i].GetComponent<Image>().color = Color.yellow;
            }

        }
    }

    public void ClearCellColors()
    {
        for (int i = 0; i < cellSlots.Count; i++)
        {
            cellSlots[i].GetComponent<Image>().color = cellColor;

            var cellSlot = cellSlots[i].GetComponent<CellSlot>();
            if (cellSlot.IsOccupied)
            {
                cellSlots[i].GetComponent<Image>().color = Color.yellow;
            }
        }
    }
}
