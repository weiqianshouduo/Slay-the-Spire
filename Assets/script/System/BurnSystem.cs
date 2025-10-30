using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnSystem : MonoBehaviour
{
    [SerializeField] private GameObject burnFX;
    void OnEnable()
    {
        ActionSystem.AttachPerformer<BurnTargetGA>(BurnTargetPerform);
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<BurnTargetGA>();

    }
    private IEnumerator BurnTargetPerform(BurnTargetGA burnTargetGA)
    {
        CombatantView target = burnTargetGA.target;
        Instantiate(burnFX, target.transform);
        DealStatusDamageGA dealStatusDamageGA = new(burnTargetGA.burnDamage, target);
        ActionSystem.Instance.AddReacion(dealStatusDamageGA);
        target.RemoveStatusEffect(StatusEffectType.BURN, 1);
        yield return new WaitForSeconds(.1f);
    }

}
