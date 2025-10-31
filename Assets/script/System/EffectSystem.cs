using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectSystem : MonoBehaviour
{
    void OnEnable()
    {
        ActionSystem.AttachPerformer<PerformEffectGA>(PerformEffectPerformer);
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<PerformEffectGA>();
    }
    IEnumerator PerformEffectPerformer(PerformEffectGA performEffectGA)
    {
        GameAction gameAction = performEffectGA.effect.GetGameAction(performEffectGA.targets,HeroSystem.Instance.heroView);//获取效果的gameAction 相应的performer固定的参数
        ActionSystem.Instance.AddReacion(gameAction);
        yield return null;
        
    }
}
