using UnityEngine;
using System.Collections.Generic;

public abstract class EquipmentItem : Item
{
    [SerializeField] protected EquipSlot _slot;
    // update to IReadOnlyList<VocationDefinition>
    [SerializeField] private List<string> _restrictToVocations = new();
    [SerializeField] private List<StatModifier> _statModifiers = new();
    [SerializeField] private List<ElementResistance> _elementResistances = new();
    [SerializeField] private List<StatusResistance> _statusResistances = new();
    // update to IReadOnlyList<EffectDefinition>
    [SerializeField] private List<string> _passiveEffects = new();
    [SerializeField] private Sprite _equipedAppearance; // what the character looks like wearing it

    public EquipSlot Slot => _slot;
    // update to IReadOnlyList<VocationDefinition>
    public IReadOnlyList<string> RestrictToVocations => _restrictToVocations;
    public IReadOnlyList<StatModifier> StatModifiers => _statModifiers;
    public IReadOnlyList<ElementResistance> ElementResistances => _elementResistances;
    public IReadOnlyList<StatusResistance> StatusResistances => _statusResistances;
    // update to IReadOnlyList<EffectDefinition>
    public IReadOnlyList<string> PassiveEffects => _passiveEffects;
    public Sprite EquippedAppearance => _equipedAppearance;
}
