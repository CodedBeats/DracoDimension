using UnityEngine;

public abstract class GameDefinition : ScriptableObject
{
    [SerializeField] protected string _id;
    [SerializeField] protected string _displayName;
    [SerializeField] private Sprite _icon;
    [SerializeField, TextArea] private string _description;
    // only for migrations and should never be edited by hand
    [SerializeField, HideInInspector] private int _schemaVersion = 1; // i'll try to remember to use you :)

    public string Id => _id;
    public string DisplayName => _displayName;
    public Sprite Icon => _icon;
    public string Description => _description;
    public int SchemaVersion => _schemaVersion;
}
