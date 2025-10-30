using System;
using UnityEngine;

public abstract class PerkConditon
{
    [SerializeField] protected ReactionTiming reactionTiming;
    public abstract void SubscribeCondition(Action<GameAction> reation);
    public abstract void UnsubscribeCondition(Action<GameAction> reation);
    public abstract bool SubCondtionIsMet(GameAction gameAction);
}
