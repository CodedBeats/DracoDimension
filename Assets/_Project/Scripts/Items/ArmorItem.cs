using UnityEngine;

[CreateAssetMenu(menuName = "DracoDimension/Items/Armor")]
public class ArmorItem : EquipmentItem
{
    [SerializeField] private int _defense;
    [SerializeField] private float _blockChance;

    public override ItemCategory Category => ItemCategory.Armor;
    public int Defense => _defense;
    public float BlockChance => _blockChance;
}
