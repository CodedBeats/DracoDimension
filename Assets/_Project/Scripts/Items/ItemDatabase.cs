using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DracoDimension/Items/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    // filled by the editor rebuild tool, saved in the asset
    [SerializeField] private List<Item> _itemDB = new();
    // built from _itemDB on first lookup, never saved
    private Dictionary<string, Item> _lookUp;


    public Item GetById(string id)
    {
        if (_lookUp == null) BuildLookUp();

        _lookUp.TryGetValue(id, out Item item);
        return item; // null if the id isn't in the database
    }

    private void BuildLookUp()
    {
        _lookUp = new Dictionary<string, Item>();

        foreach (Item item in _itemDB)
        {
            if (item == null || string.IsNullOrEmpty(item.Id)) continue;

            if (!_lookUp.TryAdd(item.Id, item))
            {
                Debug.LogWarning($"Duplicate item id '{item.Id}' on {item.name}", item);
            }
        }
    }
}