using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ActionSystem : Singleton<ActionSystem>
{
    private List<GameAction> reactions = null;
    public bool IsPerforming { get; private set; } = false;
    private static Dictionary<Type, List<Action<GameAction>>> preSubs = new();
    private static Dictionary<Type, List<Action<GameAction>>> postSubs = new();//���·ֱ���Ԥ�ȷ�Ӧ��֮��Ӧ ���ڶ�gameActionpre��post���б��GameAction��������
    private static Dictionary<Type, Func<GameAction, IEnumerator>> performers = new();// GameAction�ı��巴Ӧ

    public void Perform(GameAction action, System.Action OnPerformFinished = null)
    {
        if (IsPerforming) return;
        IsPerforming = true;
        StartCoroutine(Flow(action, () =>
        {
            IsPerforming = false;
            OnPerformFinished?.Invoke();

        }));
    }
    public void AddReacion(GameAction gameAction)
    {
        reactions?.Add(gameAction);
            }
    public IEnumerator Flow(GameAction action, Action OnFlowFinished = null){
        reactions = action.PerformReactions;
        PerformSubscribes(action, preSubs);
        yield return PerformReaction();
        reactions = action.PreReactions;
        yield return PerformPerformer(action);
        yield return PerformReaction();
        reactions = action.PostformReactions;
        PerformSubscribes(action, postSubs);
        yield return PerformReaction();
        OnFlowFinished?.Invoke();
    }
    private void PerformSubscribes(GameAction action, Dictionary<Type, List<Action<GameAction>>> subs)
    {
        Type type = action.GetType();
        if (subs.ContainsKey(type))
        {
            foreach (var sub in subs[type])
            {
                sub(action);
            }
        }
    }
    private IEnumerator PerformPerformer(GameAction action)
    {
        Type type = action.GetType();
        if (performers.ContainsKey(type))
        {
            yield return performers[type](action);
        }
    }
    private IEnumerator PerformReaction()
    {
        foreach(var reaction in reactions)
        {
            yield return Flow(reaction);
        }
    }
    public static void AttachPerformer<T>(Func<T, IEnumerator> performer) where T : GameAction
    {
        Type type = typeof(T);//��ȡGameAction��Type ���ڰ󶨵��ֵ������Ӧ
        IEnumerator wrappedPerformer(GameAction action) => performer((T)action);
        if (performers.ContainsKey(type))
        {
            performers[type] = wrappedPerformer;
        }
        else
        {
            performers.Add(type, wrappedPerformer);
        }
    }
    public static void DetachPerformer<T>() where T : GameAction
    {
        Type type = typeof(T);
        if (performers.ContainsKey(type))
        {
            performers.Remove(type);//�����
        }
        else
        {
            Debug.Log("û�а󶨸���Ϸ����");
        }
    }
    public static void SubscribeReaction<T>(Action<T> reacton, ReactionTiming timing) where T : GameAction
    {
        Dictionary<Type, List<Action<GameAction>>> subs = timing == ReactionTiming.PRE ? preSubs : postSubs;
        Type type = typeof(T);
        void wrappedReaction(GameAction action) => reacton((T)action);
        if (subs.ContainsKey(type))
        {
            subs[type].Add(wrappedReaction);
        }
        else
        {
            subs.Add(type, new());
            subs[type].Add(wrappedReaction);
        }
    }
    public static void UnsubscribeReation<T>(Action<T> reaction, ReactionTiming timing) where T : GameAction
    {
        Dictionary<Type, List<Action<GameAction>>> subs = timing == ReactionTiming.PRE ? preSubs : postSubs;
        Type type = typeof(T);
        if (subs.ContainsKey(type))
        {
            void wrappedReaction(GameAction action) => reaction((T)action);
            subs[type].Remove(wrappedReaction);
        }
    }
    
    //�˴���Ϊ��GameAction��Ԥ��Ӧ �� ĩ��Ӧ 
}
