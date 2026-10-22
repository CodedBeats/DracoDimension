using System;
using UnityEngine;

[CreateAssetMenu(menuName = "DracoDimension/Items/Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string _id = "item_name";
    [SerializeField] private string _icon = "sprite_file_ref?";
    // round down to 2 decimal places
    [SerializeField] private double _price = Math.Floor(5.55 * 100) / 100;
    [SerializeField] private string[] _effectsList = new[] { "splash_effect", "other_effect" };

    public string Id => _id;
    public string Icon => _icon;
    public double Price => _price;
    public string[] EffectsList => _effectsList;
}
