using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DracoDimension/Items/Weapon")]
public class WeaponItem : EquipmentItem
{
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private int _attack;
    [SerializeField] private Handedness _handedness;
    [SerializeField] private WeaponTargeting _targeting;
    [SerializeField] private Element _element;
    [SerializeField] private float _critChanceBonus;
    [SerializeField] private List<StatusChance> _onHitStatuses = new();
    [SerializeField] private AudioClip _attackSfx;      // will be none for a while
    [SerializeField] private GameObject _hitVfx;        // will be none for a while

    public override ItemCategory Category => ItemCategory.Weapon;
    public WeaponType WeaponType => _weaponType;
    public int Attack => _attack;
    public Handedness Handedness => _handedness;
    public WeaponTargeting Targeting => _targeting;
    public Element Element => _element;
    public float CritChanceBonus => _critChanceBonus;
    public IReadOnlyList<StatusChance> OnHitStatuses => _onHitStatuses;
    public AudioClip AttackSfx => _attackSfx;
    public GameObject HitVfx => _hitVfx;

    // runs when a value changes in the inspector
    protected override void OnValidate()
    {
        // keep parents valdiation first
        base.OnValidate();
        // weapon must go in weapon slot
        _slot = EquipSlot.Weapon;
        // weapon type can't be none
        if (_weaponType == WeaponType.None)
        {
            Debug.LogWarning("Weapon Type can't be NONE", this);
        }
    }
}
