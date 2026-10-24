using UnityEngine;
using System.Collections.Generic;

public abstract class Item : GameDefinition
{
    [SerializeField] private ItemRarity _rarity;
    [SerializeField] private int _buyPrice;
    [SerializeField] private int _sellPrice;
    [SerializeField] protected bool _canSell = true;      // false for key items
    [SerializeField] protected bool _canDiscard = true;   // false for key items
    [SerializeField] protected int _maxStack = 99;
    [SerializeField] private int _sortOrder;
    // update to List<Tags> later
    [SerializeField] private List<string> _tags = new();
    [SerializeField] private DimensionLock _dimensionLock;
    // update to DimensionDefinition later
    [SerializeField] private string _lockedDimension;
    

    // no field: each subclass overrides this to return its own category (WeaponItem -> Weapon)
    public abstract ItemCategory Category { get; }

    public ItemRarity Rarity => _rarity;
    public int BuyPrice => _buyPrice;
    public int SellPrice => _sellPrice;
    public bool CanSell => _canSell;
    public bool CanDiscard => _canDiscard;
    public int MaxStack => _maxStack;
    public int SortOrder => _sortOrder;
    public IReadOnlyList<string> Tags => _tags;
    public DimensionLock DimensionLock => _dimensionLock;


    // validate purely for overriding in other sub classes?
    protected virtual void OnValidate()
    {
        // id can't be nothing
        if(string.IsNullOrEmpty(_id))
        {
            _id = _displayName.ToLower().Replace(" ", "_");
        }
        // no item can have a stack size below 1
        _maxStack = Mathf.Max(1, _maxStack);
    }
}
