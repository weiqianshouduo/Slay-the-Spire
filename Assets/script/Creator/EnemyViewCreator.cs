using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyViewCreator : Singleton<EnemyViewCreator>
{//用于创造敌人视图的脚本 工厂模式
    [SerializeField] private EnemyView enemyViewPrefab;
    public EnemyView CreateEnemyView(EnemyData enemyData,Vector3 pos,Quaternion rotation)
    {
        EnemyView enemyView = Instantiate(enemyViewPrefab, pos, rotation);
        enemyView.SetUp(enemyData);
        return enemyView;
        
    }
}
