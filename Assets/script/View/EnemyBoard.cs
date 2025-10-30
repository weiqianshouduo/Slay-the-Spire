using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBoard : MonoBehaviour
{
    [SerializeField] private List<Transform> slots;
    public List<EnemyView> enemyViews { get; private set; } = new();
    public void AddEnemy(EnemyData enemyData)
    {
        Transform slot = slots[enemyViews.Count];//分配位置 目前最多三个
        EnemyView enemyView = EnemyViewCreator.Instance.CreateEnemyView(enemyData, slot.position, Quaternion.identity);
        enemyView.transform.parent = slot;
        enemyViews.Add(enemyView);
    }
    public IEnumerator RemoveEnemy(EnemyView enemyView)
    {
        enemyViews.Remove(enemyView);
        Tween tween = enemyView.transform.DOScale(Vector3.zero, 0.25f);
        yield return tween.WaitForCompletion();
        Destroy(enemyView);
    }
}
