using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public struct CellInfo
{
    public int x;
    public int y;
    public int width;
    public int height;
    public GameObject go;
}

public abstract class DragObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    private RectTransform rectTransform;
    private Canvas canvas;

    [SerializeField] private int height;
    [SerializeField] private int width;

    [SerializeField] private GridEquipment grid;

    private bool isDragging;
    private bool isPlaced;

    private CellInfo currentPlace = new CellInfo { x = -1, y = -1 };

    private Vector2 originalPos;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalPos = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;

        if (isPlaced)
        {
            UnPlaceObject(currentPlace);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        CellInfo bestOrigin = GetPlacingCells();

        if (bestOrigin.x != -1)
        {
            grid.ColorSelectedCells(bestOrigin);
        }
        else
        {
            grid.ClearCellColors();
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        CellInfo bestOrigin = GetPlacingCells();

        if (bestOrigin.x != -1)
        {
            PlaceObject(new CellInfo { x = bestOrigin.x, y = bestOrigin.y, width = bestOrigin.width, height = bestOrigin.height});
            currentPlace = bestOrigin;
        }
        else if (currentPlace.x != -1)
        {
            UnPlaceObject(currentPlace);
            rectTransform.anchoredPosition = originalPos;
        }
    }

    public CellInfo GetPlacingCells()
    {
        var overlapped = GetOverlappedCells();

        int required = width * height;
        if (overlapped.Count < required)
        {
            return new CellInfo { x = -1, y = -1 };
        }

        var allCells = BuildCellMap();

        var overlappedSet = new HashSet<(int x, int y)>();
        foreach (var c in overlapped) overlappedSet.Add((c.x, c.y));

        int minGridX = int.MaxValue, minGridY = int.MaxValue, maxGridX = int.MinValue, maxGridY = int.MinValue;
        foreach (var key in allCells.Keys)
        {
            minGridX = Mathf.Min(minGridX, key.x);
            minGridY = Mathf.Min(minGridY, key.y);
            maxGridX = Mathf.Max(maxGridX, key.x);
            maxGridY = Mathf.Max(maxGridY, key.y);
        }

        var dragCenter = GetScreenRect(rectTransform).center;

        float bestDistSqr = float.MaxValue;
        (int ox, int oy) bestOrigin = (int.MinValue, int.MinValue);
        bool found = false;

        for (int ox = minGridX; ox <= maxGridX; ox++)
        {
            for (int oy = minGridY; oy <= maxGridY; oy++)
            {

                int endX = ox + width - 1;
                int endY = oy + height - 1;
                if (!allCells.ContainsKey((endX, endY))) continue;

                bool allInside = true;

                for (int dx = 0; dx < width && allInside; dx++)
                {
                    for (int dy = 0; dy < height; dy++)
                    {
                        if (!overlappedSet.Contains((ox + dx, oy + dy)))
                        {
                            allInside = false;
                            break;
                        }
                    }
                }

                if (!allInside) continue;

                Rect rectUnion = RectUnionOfCells(allCells, ox, oy, width, height);
                Vector2 rectCenter = rectUnion.center;
                float distSqr = (rectCenter - dragCenter).sqrMagnitude;
                if (distSqr < bestDistSqr)
                {
                    bestDistSqr = distSqr;
                    bestOrigin = (ox, oy);
                    found = true;
                }
            }
        }

        return found ? new CellInfo { x = bestOrigin.ox, y = bestOrigin.oy, width = this.width, height = this.height } : default;
    }

    public void PlaceObject(CellInfo cellInfo)
    {
        isPlaced = true;
        grid.PlaceObject(gameObject, cellInfo);
    }

    public void UnPlaceObject(CellInfo cellInfo)
    {
        isPlaced = false;
        grid.UnPlaceObject(gameObject, cellInfo);
    }

    private List<CellInfo> GetOverlappedCells()
    {
        var result = new List<CellInfo>();

        var gridRects = grid.cellSlots;

        foreach (var cell in gridRects)
        {
            var rect = cell.GetComponent<RectTransform>();
            if (IsOverlapping(rect))
            {
                var slot = cell.GetComponent<CellSlot>();
                if (!slot.IsOccupied)
                {
                    int x = slot.X;
                    int y = slot.Y;
                    result.Add(new CellInfo { x = x, y = y, go = cell });
                }
            }
        }

        return result;
    }

    private Dictionary<(int x, int y), GameObject> BuildCellMap()
    {
        var map = new Dictionary<(int x, int y), GameObject>();
        foreach (var cell in grid.cellSlots)
        {
            var slot = cell.GetComponent<CellSlot>();
            if (slot == null) continue;
            map[(slot.X, slot.Y)] = cell;
        }
        return map;
    }

    private Rect RectUnionOfCells(Dictionary<(int x, int y), GameObject> allCells, int ox, int oy, int w, int h)
    {
        Camera cam = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;
        bool first = true;
        Vector2 min = Vector2.zero, max = Vector2.zero;

        for (int dx = 0; dx < w; dx++)
        {
            for (int dy = 0; dy < h; dy++)
            {
                var key = (ox + dx, oy + dy);
                if (!allCells.TryGetValue(key, out var go)) continue;
                var rt = go.GetComponent<RectTransform>();
                if (rt == null) continue;

                var corners = new Vector3[4];
                rt.GetWorldCorners(corners);

                for (int i = 0; i < 4; i++)
                {
                    Vector2 sp = RectTransformUtility.WorldToScreenPoint(cam, corners[i]);
                    if (first)
                    {
                        min = sp;
                        max = sp;
                        first = false;
                    }
                    else
                    {
                        min = Vector2.Min(min, sp);
                        max = Vector2.Max(max, sp);
                    }
                }
            }
        }

        if (first) return new Rect(Vector2.zero, Vector2.zero);
        return new Rect(min, max - min);
    }

    private Rect GetScreenRect(RectTransform rt)
    {
        Camera cam = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;
        var corners = new Vector3[4];
        rt.GetWorldCorners(corners);

        Vector2 min = RectTransformUtility.WorldToScreenPoint(cam, corners[0]);
        Vector2 max = min;
        for (int i = 1; i < 4; i++)
        {
            Vector2 sp = RectTransformUtility.WorldToScreenPoint(cam, corners[i]);
            min = Vector2.Min(min, sp);
            max = Vector2.Max(max, sp);
        }

        return new Rect(min, max - min);
    }

    public bool IsOverlapping(RectTransform other)
    {
        var a = GetScreenRect(rectTransform);
        var b = GetScreenRect(other);
        return a.Overlaps(b);
    }
}
