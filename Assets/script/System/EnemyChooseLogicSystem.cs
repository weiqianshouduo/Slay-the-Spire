
using UnityEngine;

public class EnemyChooseLogicSystem : Singleton<EnemyChooseLogicSystem>
{
    public EnemyAutoEffect ChooseTheEnemyLogic(EnemyView enemy)
    {
        int count = enemy.enemyLogic.Count;
        int shouldUseLogic = Random.Range(0, count);
        return enemy.enemyLogic[shouldUseLogic];
    }
    public EnemyAutoEffect ChooseCurrentEnemyLogic(EnemyView enemy)
    {
        int i = enemy.currentStep;
        enemy.currentStep++;
        if (enemy.currentStep >= enemy.enemyLogic.Count)
        {
            enemy.currentStep = 0;
        }
        return enemy.enemyLogic[i];
    }
}
