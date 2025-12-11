using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ReactionTiming
{
    Pre,
    Post,
}

// Reaction을 시전할 때, 시전자가 Reaction을 수행할 대상.
// i.e. 임의의 Enemy가 Reaction을 등록한 상황.
// All : Enemy든 Player든 어떤 Action 모두가 Reaction Trigger의 대상.
// Friendly : Enemy 기준, Enemy의 Friendly(Enemy)의 Actino이 대상. 
// Hostile : Enemy 기준, Enemy의 Hostile(Player)의 Action이 대상.
public enum ReactionTarget
{
    All,
    Friendly,
    Hostile,
}

public class ReactionKey : IEquatable<ReactionKey>
{
    // Type: 해당 Type(GameAbility)에 대한 Reaction
    // Character: 해당 Reaction을 수행하는 Responder
    // ReactionTarget: 해당 Reaction 수행에 대한 Target
    // ReactionTiming: Reaction을 수행할 시점(타이밍)
    
    public Type AbilityForReaction { get; private set; }
    public Character Responder { get; private set; }
    public ReactionTarget RespondTarget { get; private set; }

    public ReactionKey(
        Type abilityForReaction,
        Character responder,
        ReactionTarget respondTarget)
    {
        AbilityForReaction = abilityForReaction;
        Responder = responder;
        RespondTarget = respondTarget;
    }

    public bool Equals(ReactionKey other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Equals(AbilityForReaction, other.AbilityForReaction) && 
               Equals(Responder, other.Responder) && 
               RespondTarget == other.RespondTarget;
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ReactionKey)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(AbilityForReaction, Responder, (int)RespondTarget);
    }
}

public class GameAbilitySystem : Singleton<GameAbilitySystem>
{
    private List<GameAbility> _reactions = new();
    
    private static Dictionary<ReactionKey, List<GameAbility>> _preReactions = new();
    private static Dictionary<ReactionKey, List<GameAbility>> _postReactions = new();
    
    // 해당 Type(GameAbility)에 대한 Performer(IEnumerator)를 반환합니다.
    private static Dictionary<Type, Func<GameAbility, IEnumerator>> _performers = new();

    public void AddPerformer<T>(Func<T, IEnumerator> performer) where T : GameAbility
    {
        Type type = typeof(T);
        IEnumerator wrappedPerformer(GameAbility ga) => performer((T)ga);
        
        _performers[type] = wrappedPerformer;
    }

    public void RemovePerformer<T>() where T : GameAbility
    {
        Type type = typeof(T);
        _performers.Remove(type);
    }
}
