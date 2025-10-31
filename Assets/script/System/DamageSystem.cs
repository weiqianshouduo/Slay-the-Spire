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
    private IEnumerator DealDamgePerformer(DealDamageGA dealDamageGA)
    {
        CombatantView attacker = dealDamageGA.Caster;
        foreach (var combatantView in dealDamageGA.Targets)
        {
             Tween tween = attacker.transform.DOMoveX(attacker.transform.position.x - 1f, 0.15f);
            yield return tween.WaitForCompletion();//等待动画完成
            attacker.transform.DOMoveX(attacker.transform.position.x + 1f, 0.25f);
            combatantView.Damage(dealDamageGA.Damage);//处理伤害
            Instantiate(damageVFX, combatantView.transform.position, Quaternion.identity);//创造一个特效预制体在这里
            yield return new WaitForSeconds(0.15f);
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
        CombatantView target = dealStatusDamageGA.target;
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
                //人物死亡逻辑
            }

        yield return null;
        }
    }
}
