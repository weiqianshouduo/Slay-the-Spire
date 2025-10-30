using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Splines;

public class HandView : MonoBehaviour
{
    [SerializeField] private int maxHandSize;//最大手牌数量
    [SerializeField] private SplineContainer splineContainer;
    private List<CardView> cards = new();

    public IEnumerator AddCard(CardView cardView)
    {
        cards.Add(cardView);
        yield return UpdateCardPositions(0.15f);
    }
    public CardView RemoveCard(Card card)
    {
        CardView cardView = GetCardView(card);//获取到手牌的Handview
        if (cardView == null) return null;
        cards.Remove(cardView);
        StartCoroutine(UpdateCardPositions(.15f));
        return cardView;
    }
    private CardView GetCardView(Card card)
    {
        return cards.Where(cardView => cardView.card == card).FirstOrDefault();// 从 cards 集合中查找并返回第一个 cardView.card 等于目标 card 的 CardView 对象
    }
 public IEnumerator UpdateCardPositions(float duration)
    {
        // 安全检查：如果没有卡牌则直接结束协程
        if (cards.Count == 0) yield break;

        // 计算卡牌间距：将样条曲线长度均分为N段
        float cardSpacing = 1f /10;

        // 计算第一张卡牌的位置，确保整体居中
        float firstCardPosition = 0.5f - (cards.Count - 1) * cardSpacing / 2;

        // 获取样条曲线引用
        Spline spline = splineContainer.Spline;

        // 遍历所有卡牌
        for (int i = 0; i < cards.Count; i++)
        {
            // 计算当前卡牌在样条曲线上的位置参数(0-1范围)
            float p = firstCardPosition + i * cardSpacing;

            // 获取样条曲线上对应位置的世界坐标
            Vector3 splinePosition = spline.EvaluatePosition(p);
            // 获取样条曲线在该点的切线方向
            Vector3 forward = spline.EvaluateTangent(p);

            // 获取样条曲线在该点的上方向量
            Vector3 up = spline.EvaluateUpVector(p);

            // 计算卡牌的旋转角度，使其面朝相机
            Quaternion rotation = Quaternion.LookRotation(-up, Vector3.Cross(-up, forward).normalized);//cross是叉乘

            // 使用DOTween动画移动卡牌到目标位置
            // 每张卡牌在Z轴方向错开0.01*i的距离，避免遮挡
            cards[i].transform.DOMove(splinePosition + new Vector3(transform.position.x, transform.position.y, 0) + 0.2f * i * Vector3.back, duration);//
            cards[i].SetTheGroupOrder(i);//修该物体的渲染顺序

            // 使用DOTween动画旋转卡牌到目标角度
            cards[i].transform.DORotateQuaternion(rotation, duration);
        }

        // 等待所有动画完成后结束协程
        yield return new WaitForSeconds(duration);
    }

}
