using UnityEngine;

public class CellSlot : MonoBehaviour
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public bool IsOccupied { get; set; }

    public void Initialize(int x, int y)
    {
        X = x;
        Y = y;
        IsOccupied = false;
    }

    public Vector2 GetPosition()
    {
        return new Vector2(X, Y);
    }
}
