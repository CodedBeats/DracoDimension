using UnityEngine;
using System;


[Serializable]
public struct StatModifier 
{
    // fields
    [SerializeField] private StatType _stat;
    [SerializeField] private int _value;       // (flat only)

    // give access outside to read fields
    public StatType Stat => _stat;
    public int Value => _value;

    // constructor
    public StatModifier(StatType stat, int value)
    {
        _stat = stat;
        _value = value;
    }
}

[Serializable]
public struct ElementResistance 
{
    // fields
    [SerializeField] private Element _element;
    [SerializeField] private int _percent;     // (negative = weakness)

    // give access outside to read fields
    public Element Element => _element;
    public int Percent => _percent;

    // constructor
    public ElementResistance(Element element, int percent)
    {
        _element = element;
        _percent = percent;
    }
}

[Serializable]
public struct StatusResistance 
{
    // fields
    [SerializeField] private StatusEffect _status;
    [SerializeField] private int _percent;

    // give access outside to read fields
    public StatusEffect Status => _status;
    public int Percent => _percent;

    // constructor
    public StatusResistance(StatusEffect status, int percent)
    {
        _status = status;
        _percent = percent;
    }
}

[Serializable]
public struct StatusChance 
{
    // fields
    [SerializeField] private StatusEffect _status;
    [SerializeField] private float _chance;      // decimal value between 0 - 1

    // give access outside to read fields
    public StatusEffect Status => _status;
    public float Chance => _chance;

    // constructor
    public StatusChance(StatusEffect status, float chance)
    {
        _status = status;
        _chance = chance;
    }
}


