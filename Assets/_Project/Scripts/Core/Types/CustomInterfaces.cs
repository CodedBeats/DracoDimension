using System.Collections.Generic;

public interface IUsable
{
    UseContext UseContext { get; }
    TargetType TargetType { get; }
    // update to IReadOnlyList<EffectDefinition>
    IReadOnlyList<string> Effects { get; }
}

