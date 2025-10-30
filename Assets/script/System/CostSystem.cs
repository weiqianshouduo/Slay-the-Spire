using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CostSystem : Singleton<CostSystem>
{
    [SerializeField] private CostUI costUI;
    private const int MAX_COST = 3;
    private int currentCost = MAX_COST;
    void OnEnable()
    {
        ActionSystem.AttachPerformer<SpendCostGA>(SpendCostPerformer);
        ActionSystem.AttachPerformer<RefillCostGA>(RefillCostPerformer);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
        costUI.UpdateCostText(MAX_COST);
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<SpendCostGA>();
        ActionSystem.DetachPerformer<RefillCostGA>();
        ActionSystem.UnsubscribeReation<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    private IEnumerator SpendCostPerformer(SpendCostGA spendCostGA)
    {
        currentCost -= spendCostGA.Amount;
        costUI.UpdateCostText(currentCost);
        yield return null;
    }
    private IEnumerator RefillCostPerformer(RefillCostGA refillCostGA)
    {
        currentCost = MAX_COST;
        costUI.UpdateCostText(currentCost);
        yield return null;
    }
    public bool HasEnoughCost(int cost)
    {
        return currentCost >= cost;
    }
    public void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        RefillCostGA refillCostGA = new();
        ActionSystem.Instance.AddReacion(refillCostGA);//对于此类跟随其他事件的应加入到其他GamgAction的post或pre列表进行
    }

}
