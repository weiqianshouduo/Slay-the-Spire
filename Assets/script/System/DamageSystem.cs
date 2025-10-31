using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    [SerializeField] private GameObject damageVFX;//伤害特效
    void OnEnable()
    {
        ActionSystem.AttachPerformer<DealDamageGA>(DealDamgePerformer);
        ActionSystem.AttachPerformer<DealStatusDamageGA>(DealStatusDamagePerformer);
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<DealDamageGA>();
        ActionSystem.DetachPerformer<DealStatusDamageGA>();
    }
    //DealDamgeGA的执行者
    private IEnumerator DealDamgePerformer(DealDamageGA dealDamageGA)
    {
        CombatantView attacker = dealDamageGA.Caster;
        foreach (var combatantView in dealDamageGA.Targets)
        {
            int Damage = dealDamageGA.Damage;
            if (attacker.GetStatusEffectStack(StatusEffectType.Strength) > 0)
            {
                Damage += attacker.GetStatusEffectStack(StatusEffectType.Strength);//计算力量增加的攻击力
            }
             //处理动画
            Tween tween = attacker.transform.DOMoveX(attacker.transform.position.x - 1f, 0.15f);
            yield return tween.WaitForCompletion();//等待动画完成
            
            attacker.transform.DOMoveX(attacker.transform.position.x + 1f, 0.25f);
            combatantView.Damage(Damage);//处理伤害
            
            Instantiate(damageVFX, combatantView.transform.position, Quaternion.identity);//创造一个特效预制体在这里
            yield return new WaitForSeconds(0.15f);
            //处理死亡逻辑
            if (combatantView.CurrentHp <= 0)
            {
                if (combatantView is EnemyView enemyView)
                {
                    KillEnemyGA killEnemyGA = new(enemyView);
                    ActionSystem.Instance.AddReacion(killEnemyGA);
                }
                else
                {
                    KillHeroGA killHeroGA = new();
                    ActionSystem.Instance.AddReacion(killHeroGA);
                }
            }

        }
    }
    private IEnumerator DealStatusDamagePerformer(DealStatusDamageGA dealStatusDamageGA)
    {
        CombatantView target = dealStatusDamageGA.target;//计算 Stats所造成的伤害
        target.Damage(dealStatusDamageGA.Damage);
        if (target.CurrentHp <= 0)
        {
            if (target is EnemyView enemyView)
            {
                KillEnemyGA killEnemyGA = new(enemyView);
                ActionSystem.Instance.AddReacion(killEnemyGA);
            }
            else
            { 
                KillHeroGA killHeroGA = new();
                ActionSystem.Instance.AddReacion(killHeroGA);
            }

        yield return null;
        }
    }
}
