using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyViewCreator : Singleton<EnemyViewCreator>
{//���ڴ��������ͼ�Ľű� ����ģʽ
    [SerializeField] private EnemyView enemyViewPrefab;
    public EnemyView CreateEnemyView(EnemyData enemyData,Vector3 pos,Quaternion rotation)
    {
        EnemyView enemyView = Instantiate(enemyViewPrefab, pos, rotation);
        enemyView.SetUp(enemyData);
        return enemyView;
        
    }
}
