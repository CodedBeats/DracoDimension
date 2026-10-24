using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "DracoDimension/Items/Key")]
public class KeyItem : Item, IUsable
{
    // from IUsable interface
    [SerializeField] private UseContext _useContext;
    [SerializeField] private TargetType _targetType;
    [SerializeField] private List<string> _effects = new();

    // update to QuestDefinition
    [SerializeField] private string _relatedQuest;
    [SerializeField] private bool _isUsable;
    [SerializeField] private bool _removeOnQuestComplete;

    public override ItemCategory Category => ItemCategory.Key;
    // from IUsable interface
    public UseContext UseContext => _useContext; 
    public TargetType TargetType => _targetType; 
    public IReadOnlyList<string> Effects => _effects;

    public string RelatedQuest => _relatedQuest;
    public bool IsUsable => _isUsable;
    public bool RemoveOnQuestComplete => _removeOnQuestComplete;

    // runs when a value changes in the inspector
    protected override void OnValidate()
    {
        // keep parents valdiation first
        base.OnValidate();
        _maxStack = 1;
        _canSell = false;
        _canDiscard = false;
    }
}
