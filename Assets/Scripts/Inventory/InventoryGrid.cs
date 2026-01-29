using UnityEngine;

public class InventoryGrid
{
    public int width;
    public int height;
    private InventoryItem[,] cells;

    public InventoryGrid(int w, int h)
    {
        width = w;
        height = h;
        cells = new InventoryItem[w, h];
    }

    public bool CanPlace(InventoryItem item, int x, int y)
    {
        if (x < 0 || y < 0 || x + item.Width > width || y + item.Height > height)
            return false;

        for (int i = 0; i < item.Width; i++)
            for (int j = 0; j < item.Height; j++)
                if (cells[x + i, y + j] != null)
                    return false;

        return true;
    }

    public void Place(InventoryItem item, int x, int y)
    {
        item.x = x;
        item.y = y;

        for (int i = 0; i < item.Width; i++)
            for (int j = 0; j < item.Height; j++)
                cells[x + i, y + j] = item;
    }

    public void Remove(InventoryItem item)
    {
        for (int i = 0; i < item.Width; i++)
            for (int j = 0; j < item.Height; j++)
                cells[item.x + i, item.y + j] = null;
    }

    public bool TryAdd(ItemData data, out InventoryItem created)
    {
        created = new InventoryItem { data = data };

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                if (CanPlace(created, x, y))
                {
                    Place(created, x, y);
                    return true;
                }

        return false;
    }
}

