using UnityEngine;

public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int width = 1;
    public int height = 1;
    public bool stackable;
    public GameObject worldPrefab;
}