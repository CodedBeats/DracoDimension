using UnityEngine;

[CreateAssetMenu(menuName = "DracoDimension/Items/Material")]
public class MaterialItem : Item
{
    [SerializeField] private MaterialGroup _materialGroup;
    [SerializeField] private bool _gatherable;

    public override ItemCategory Category => ItemCategory.Material;
    public MaterialGroup MaterialGroup => _materialGroup;
    public bool Gatherable => _gatherable;
}
