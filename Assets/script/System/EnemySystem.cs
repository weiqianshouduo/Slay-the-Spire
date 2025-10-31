using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class EnemySystem : Singleton<EnemySystem>{
    [SerializeField] private EnemyBoard enemyBoardView;
    public List<EnemyView> enemyViews => enemyBoardView.enemyViews;
    public List<EnemyAutoEffect> enemyAutoEffects = new(99);
    void OnEnable()
    {
        ActionSystem.AttachPerformer<EnemyTurnGA>(EnemyTurnPerformer);
        ActionSystem.AttachPerformer<KillEnemyGA>(KillEnemyPerformer);
        ActionSystem.AttachPerformer<PlayEnemyLogicGA>(PlayEnemyLogicPerformer);
        ActionSystem.AttachPerformer<ShowEnemyNextActionGA>(ShowEnemyNextActionPerformer);
        ActionSystem.AttachPerformer<EnemySetNextActionGA>(EnemySetNextActionPerformer);
        ActionSystem.SubscribeReaction<SystemStartGA>(SystemStartPreReation, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<SystemStartGA>(SystemStartPostReation, ReactionTiming.POST);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<EnemyTurnGA>();
        ActionSystem.DetachPerformer<KillEnemyGA>();
        ActionSystem.DetachPerformer<PlayEnemyLogicGA>();
        ActionSystem.DetachPerformer<ShowEnemyNextActionGA>();
        ActionSystem.DetachPerformer<EnemySetNextActionGA>();
        ActionSystem.UnsubscribeReation<SystemStartGA>(SystemStartPreReation, ReactionTiming.PRE);
         ActionSystem.UnsubscribeReation<SystemStartGA>(SystemStartPostReation, ReactionTiming.POST);
        ActionSystem.UnsubscribeReation<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.UnsubscribeReation<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }
    public void SetUp(List<EnemyData> enemyDatas)
    {
        foreach (var enemyData in enemyDatas)
        {
            enemyBoardView.AddEnemy(enemyData);//将列表的敌人数据添加进敌人棋盘视图 并且将敌人数据实例化为敌人视图
        }
    }
    private IEnumerator EnemyTurnPerformer(EnemyTurnGA enemyTurnGA)
    {
        int i = 0;
        foreach (var caster in enemyViews)
        {
             if (caster.GetStatusEffectStack(StatusEffectType.ARMOR) > 0)
            {
                ClearAllStatusGA clearAllStatusGA = new(caster, StatusEffectType.ARMOR);//回合结束取消防御 
                ActionSystem.Instance.AddReacion(clearAllStatusGA);
            }
            if (enemyAutoEffects[i] != null)
            {
                PlayEnemyLogicGA playEnemyLogicGA = new(caster, enemyAutoEffects[i].targetMode.GetTargets(), enemyAutoEffects[i].Effect);
                ActionSystem.Instance.AddReacion(playEnemyLogicGA);
                i++;
            }
        }
        enemyAutoEffects.Clear();
        EnemySetNextActionGA enemySetNextActionGA = new();
        ActionSystem.Instance.AddReacion(enemySetNextActionGA);
        yield return null;
    }

    private IEnumerator KillEnemyPerformer(KillEnemyGA killEnemyGA)
    {
        yield return enemyBoardView.RemoveEnemy(killEnemyGA.target);//死亡逻辑
    }
    
    
    private void EnemyTurnPreReaction(EnemyTurnGA enemyTurnGA)
    {
        foreach (var enemyview in enemyBoardView.enemyViews)
        {
            int burnStacks = enemyview.GetStatusEffectStack(StatusEffectType.BURN);
            if (burnStacks > 0)
            {
                BurnTargetGA burnTargetGA = new(burnStacks, enemyview);
                ActionSystem.Instance.AddReacion(burnTargetGA);
            }
        }
    }
    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        int i = 0;
        foreach (var caster in enemyViews)
        {
            ShowEnemyNextActionGA showEnemyNextActionGA = new(enemyAutoEffects[i].Image, caster, enemyAutoEffects[i].Stack.ToString());
            ActionSystem.Instance.AddReacion(showEnemyNextActionGA);
            i++;
            if (caster.GetStatusEffectStack(StatusEffectType.Vulner) > 0) {
                caster.RemoveStatusEffect(StatusEffectType.Vulner, 1);
            }
        }
    }
    
    
    private void SystemStartPreReation(SystemStartGA systemStartGA)
    {
        EnemySetNextActionGA enemySetNextActionGA = new();//获取开局行为
        ActionSystem.Instance.AddReacion(enemySetNextActionGA);
    }
    private void SystemStartPostReation(SystemStartGA systemStartGA)
    {
        int i = 0;//加载开局之后的行为
        foreach (var caster in enemyViews)
        {
            ShowEnemyNextActionGA showEnemyNextActionGA = new(enemyAutoEffects[i].Image, caster, enemyAutoEffects[i].Stack.ToString());
            ActionSystem.Instance.AddReacion(showEnemyNextActionGA);
            i++;
        }
    }
 
   
   
    private IEnumerator PlayEnemyLogicPerformer(PlayEnemyLogicGA playEnemyLogicGA)
    {
        CombatantView attacker = playEnemyLogicGA.Caster;
           Tween tween = attacker.transform.DOMoveX(attacker.transform.position.x - 1f, 0.15f);
            yield return tween.WaitForCompletion();//等待动画完成
            attacker.transform.DOMoveX(attacker.transform.position.x + 1f, 0.25f);
        foreach (var target in playEnemyLogicGA.targets)
        {
               
            GameAction gameAction = playEnemyLogicGA.effect.GetGameAction(new() { target }, playEnemyLogicGA.Caster);
            ActionSystem.Instance.AddReacion(gameAction);
            yield return null;
        }
    }
    private IEnumerator ShowEnemyNextActionPerformer(ShowEnemyNextActionGA showEnemyNextActionGA)
    {
        EnemyView caster = showEnemyNextActionGA.Caster;//获取完意图就开始加载
        caster.UpdateNextActionText(showEnemyNextActionGA.Image, showEnemyNextActionGA.stack);
        Debug.Log(enemyAutoEffects.Count);
        yield return null;
    }

    private IEnumerator EnemySetNextActionPerformer(EnemySetNextActionGA enemySetNextActionGA)
    {
        foreach (var enemyview in enemyBoardView.enemyViews)
        {
            EnemyAutoEffect autoTargetEffect = EnemyChooseLogicSystem.Instance.ChooseCurrentEnemyLogic(enemyview);//每次敌人回合结束后就开始获取敌人的下回合行动意图
            enemyAutoEffects.Add(autoTargetEffect);
        }
        yield return null;
    }
}
