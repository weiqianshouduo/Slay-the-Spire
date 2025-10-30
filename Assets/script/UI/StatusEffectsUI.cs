using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class StatusEffectsUI : MonoBehaviour
{
    [SerializeField] private StatusEffectUI statusEffectUIPrefab;
    [SerializeField] private Sprite armorSprite, burnSprite;
    private Dictionary<StatusEffectType, StatusEffectUI> statusEffectUIs = new();
    public void UpdateStatusEffectUI(StatusEffectType statusEffectType, int stackCount)
    {
        if (stackCount == 0)//���ǵ�buff�Ĳ���Ϊ0��ʱ��
        {
            //remove
            if (statusEffectUIs.ContainsKey(statusEffectType))//�鿴�ֵ��Ƿ�洢����ص�����
            {
                StatusEffectUI statusEffectUI = statusEffectUIs[statusEffectType];
                statusEffectUIs.Remove(statusEffectType);
                Destroy(statusEffectUI.gameObject);
            }
        }
        else
        {
            if (!statusEffectUIs.ContainsKey(statusEffectType))
            {
                StatusEffectUI statusEffectUI = Instantiate(statusEffectUIPrefab, transform);
                statusEffectUIs.Add(statusEffectType, statusEffectUI);
            }
            Sprite sprite = GetSpiteByType(statusEffectType);
            statusEffectUIs[statusEffectType].SetUp(sprite, stackCount);

        }
    }
    private Sprite GetSpiteByType(StatusEffectType statusEffectType)
    {
        return statusEffectType switch
        {
            StatusEffectType.ARMOR => armorSprite,
            StatusEffectType.BURN => burnSprite,
            _ => null,
        };
    }

 
}
