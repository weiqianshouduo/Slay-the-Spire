using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class StatusEffectsUI : MonoBehaviour
{
    [SerializeField] private StatusEffectUI statusEffectUIPrefab;
    [SerializeField] private Sprite armorSprite, burnSprite, VulnerSprite,StrengthSprite;//此处获取图像
    private Dictionary<StatusEffectType, StatusEffectUI> statusEffectUIs = new();//此处只用来处理 ui 
    public void UpdateStatusEffectUI(StatusEffectType statusEffectType, int stackCount)
    {
        if (stackCount == 0)//若是当buff的层数为0的时候
        {
            //remove
            if (statusEffectUIs.ContainsKey(statusEffectType))//查看字典是否存储了相关的数据
            {
                StatusEffectUI statusEffectUI = statusEffectUIs[statusEffectType];//获取相关的game object
                statusEffectUIs.Remove(statusEffectType);//从列表中去掉
                Destroy(statusEffectUI.gameObject);
            }
        }
        else
        {
            if (!statusEffectUIs.ContainsKey(statusEffectType))//若是不存在的情况
            {
                StatusEffectUI statusEffectUI = Instantiate(statusEffectUIPrefab, transform);//将预制体生成为物体的子物体 
                statusEffectUIs.Add(statusEffectType, statusEffectUI);
            }
            Sprite sprite = GetSpiteByType(statusEffectType);
            statusEffectUIs[statusEffectType].SetUp(sprite, stackCount);//将数值设置成可显示的ui

        }
    }
    private Sprite GetSpiteByType(StatusEffectType statusEffectType)
    {
        return statusEffectType switch
        {
            StatusEffectType.ARMOR => armorSprite,
            StatusEffectType.BURN => burnSprite,
            StatusEffectType.Vulner => VulnerSprite,
            StatusEffectType.Strength => StrengthSprite,
            _ => null,
        };
    }//获取图片

 
}
