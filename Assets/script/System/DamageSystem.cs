using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    [SerializeField] private GameObject damageVFX;//�˺���Ч
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
    //DealDamgeGA��ִ����
    private IEnumerator DealDamgePerformer(DealDamageGA dealDamageGA)
    {
        CombatantView attacker = dealDamageGA.Caster;
        foreach (var combatantView in dealDamageGA.Targets)
        {
            int Damage = dealDamageGA.Damage;
            if (attacker.GetStatusEffectStack(StatusEffectType.Strength) > 0)
            {
                Damage += attacker.GetStatusEffectStack(StatusEffectType.Strength);//�����������ӵĹ�����
            }
             //������
            Tween tween = attacker.transform.DOMoveX(attacker.transform.position.x - 1f, 0.15f);
            yield return tween.WaitForCompletion();//�ȴ��������
            
            attacker.transform.DOMoveX(attacker.transform.position.x + 1f, 0.25f);
            combatantView.Damage(Damage);//�����˺�
            
            Instantiate(damageVFX, combatantView.transform.position, Quaternion.identity);//����һ����ЧԤ����������
            yield return new WaitForSeconds(0.15f);
            //���������߼�
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
        CombatantView target = dealStatusDamageGA.target;//���� Stats����ɵ��˺�
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
