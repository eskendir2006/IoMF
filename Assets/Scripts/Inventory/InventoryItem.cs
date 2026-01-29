using UnityEngine;

public class InventoryItem
{
    public ItemData data;
    public int x;
    public int y;
    public bool rotated;

    public int Width => rotated ? data.height : data.width;
    public int Height => rotated ? data.width : data.height;
}