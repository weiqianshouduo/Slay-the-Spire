using System.Collections;
using System.Collections.Generic;
using SerializeReferenceEditor;
using UnityEngine;
[CreateAssetMenu(menuName = "Data/Perk")]
public class PerkData : ScriptableObject
{
   [field: SerializeField] public Sprite Image { get; private set; }//ͼ�� ������������
    [field: SerializeReference, SR] public PerkConditon perkConditon { get; private set; }//perkӦ�ô�����ʱ��
    [field: SerializeReference, SR] public AutoTargetEffect autoTargetEffect { get; private set; }//Ч��
    [field: SerializeField] public bool UseAutoTarget { get; private set; } = true;
    [field: SerializeField] public bool UseActionCasterAsTarget { get; private set; } = false;

}
