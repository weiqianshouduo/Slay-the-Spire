using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.UIElements;

public class MatchSystem : MonoBehaviour
{
    [SerializeField] private HeroData heroData;//��������������ת�Ƶ�һ���ط� ����
    [SerializeField] private PerkData perkData;
    [SerializeField] private List<EnemyData> enemyDatas;
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<SystemStartGA>(SystemStartGAPerformer);
    }
    private void ODisable()
    {
        ActionSystem.DetachPerformer<SystemStartGA>();
    }
    private void Start()
    {
         HeroSystem.Instance.SetUp(heroData);//�������ʼ��
        CardSystem.Instance.SetUp(heroData.Decks);//�������ʼ��
        EnemySystem.Instance.SetUp(enemyDatas);//�������б��ʼ��
        if (perkData != null)
        {
            PerkSystem.Instance.AddPerk(new Perk(perkData));
        }
        SystemStartGA systemStartGA = new();
        ActionSystem.Instance.Perform(systemStartGA);
      
    }

    private IEnumerator SystemStartGAPerformer(SystemStartGA systemStartGA)
    {
        DrawCardGA drawCardGA = new(5);
        ActionSystem.Instance.AddReacion(drawCardGA);
        yield return null;
    }
}//ȫ�̵ĳ�ʼ���߼����ɴ˴�������
 