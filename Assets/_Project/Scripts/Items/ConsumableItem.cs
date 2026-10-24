using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "DracoDimension/Items/Consumable")]
public class ConsumableItem : Item, IUsable
{
    // from IUsable interface
    [SerializeField] private UseContext _useContext;
    [SerializeField] private TargetType _targetType;
    [SerializeField] private List<string> _effects = new();

    [SerializeField] private bool _allowKnockedOutTarget;
    [SerializeField] private bool _consumeOnUse = true; // false for reusable items
    [SerializeField] private string _battleMessage;
    [SerializeField] private AudioClip _useSfx;         // will be none for a while
    [SerializeField] private GameObject _useVfx;        // will be none for a while

    public override ItemCategory Category => ItemCategory.Consumable;
    // from IUsable interface
    public UseContext UseContext => _useContext; 
    public TargetType TargetType => _targetType; 
    public IReadOnlyList<string> Effects => _effects; // can't do this one yet too

    public bool AllowKnockedOutTarget => _allowKnockedOutTarget;
    public bool ConsumeOnUse => _consumeOnUse;
    public string BattleMessage => _battleMessage;
    public AudioClip UseSfx => _useSfx;
    public GameObject UseVfx => _useVfx;
}
