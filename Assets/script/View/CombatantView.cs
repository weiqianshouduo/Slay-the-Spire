using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

public class CombatantView : MonoBehaviour
{
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private StatusEffectsUI statusEffectsUI;
    public int MaxHp { get; private set; }
    public int CurrentHp { get; private set; }
    private Dictionary<StatusEffectType, int> statusEffects = new();

    protected void SetUpBase(int Hp,Sprite image)
    {
        MaxHp = CurrentHp= Hp;
        spriteRenderer.sprite = image;
        UpdateHPText();//加载血量
    }
    protected void UpdateHPText()
    {
        hpText.text = "Hp" + CurrentHp.ToString();
    }
    public void Damage(int DamageAmount)
    {
        int remainingDamage = DamageAmount;
        if (GetStatusEffectStack(StatusEffectType.Vulner) > 0)
        {
            remainingDamage = (remainingDamage * GameParameters.Vulnerability)/100;//计算易伤效果 
        }
        int CurrentArrom = GetStatusEffectStack(StatusEffectType.ARMOR);
        if (CurrentArrom >= remainingDamage)//计算防与伤害
        {
        RemoveStatusEffect(StatusEffectType.ARMOR, remainingDamage);
           remainingDamage = 0;
        }
        else
        {
            RemoveStatusEffect(StatusEffectType.ARMOR, CurrentArrom);
            remainingDamage -= CurrentArrom;
        }

        if (remainingDamage > 0)//若是仍存在数值在计算
        {
            CurrentHp -= remainingDamage;
            if (CurrentHp < 0)
            {
                CurrentHp = 0;
            }
        }

        transform.DOShakePosition(0.2f, 0.5f);
        
        UpdateHPText();//处理伤害的逻辑
    }
    public void AddStatusEffect(StatusEffectType statusEffectType, int stackCount)
    {
        if (statusEffects.ContainsKey(statusEffectType))
        {
            statusEffects[statusEffectType] += stackCount;//若是已经存在键 就可以依照键直接加减
        }
        else
        {
            statusEffects.Add(statusEffectType, stackCount);//若是没有 便加一个进去
        }
        statusEffectsUI.UpdateStatusEffectUI(statusEffectType, GetStatusEffectStack(statusEffectType));
    }
    public void RemoveStatusEffect(StatusEffectType statusEffectType, int stackCount)
    {
        if (statusEffects.ContainsKey(statusEffectType))
        {
            statusEffects[statusEffectType] -= stackCount;
            if (statusEffects[statusEffectType] == 0)
            {
                statusEffects.Remove(statusEffectType);

            }
        }
        statusEffectsUI.UpdateStatusEffectUI(statusEffectType, GetStatusEffectStack(statusEffectType));//此处计算的数值和ui上显示的是分开的 
    }
    public int GetStatusEffectStack(StatusEffectType type)
    {
        if (statusEffects.ContainsKey(type)) return statusEffects[type];
        else return 0;
    }

}
