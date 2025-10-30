using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Splines;

public class HandView : MonoBehaviour
{
    [SerializeField] private int maxHandSize;//�����������
    [SerializeField] private SplineContainer splineContainer;
    private List<CardView> cards = new();

    public IEnumerator AddCard(CardView cardView)
    {
        cards.Add(cardView);
        yield return UpdateCardPositions(0.15f);
    }
    public CardView RemoveCard(Card card)
    {
        CardView cardView = GetCardView(card);//��ȡ�����Ƶ�Handview
        if (cardView == null) return null;
        cards.Remove(cardView);
        StartCoroutine(UpdateCardPositions(.15f));
        return cardView;
    }
    private CardView GetCardView(Card card)
    {
        return cards.Where(cardView => cardView.card == card).FirstOrDefault();// �� cards �����в��Ҳ����ص�һ�� cardView.card ����Ŀ�� card �� CardView ����
    }
 public IEnumerator UpdateCardPositions(float duration)
    {
        // ��ȫ��飺���û�п�����ֱ�ӽ���Э��
        if (cards.Count == 0) yield break;

        // ���㿨�Ƽ�ࣺ���������߳��Ⱦ���ΪN��
        float cardSpacing = 1f /10;

        // �����һ�ſ��Ƶ�λ�ã�ȷ���������
        float firstCardPosition = 0.5f - (cards.Count - 1) * cardSpacing / 2;

        // ��ȡ������������
        Spline spline = splineContainer.Spline;

        // �������п���
        for (int i = 0; i < cards.Count; i++)
        {
            // ���㵱ǰ���������������ϵ�λ�ò���(0-1��Χ)
            float p = firstCardPosition + i * cardSpacing;

            // ��ȡ���������϶�Ӧλ�õ���������
            Vector3 splinePosition = spline.EvaluatePosition(p);
            // ��ȡ���������ڸõ�����߷���
            Vector3 forward = spline.EvaluateTangent(p);

            // ��ȡ���������ڸõ���Ϸ�����
            Vector3 up = spline.EvaluateUpVector(p);

            // ���㿨�Ƶ���ת�Ƕȣ�ʹ���泯���
            Quaternion rotation = Quaternion.LookRotation(-up, Vector3.Cross(-up, forward).normalized);//cross�ǲ��

            // ʹ��DOTween�����ƶ����Ƶ�Ŀ��λ��
            // ÿ�ſ�����Z�᷽���0.01*i�ľ��룬�����ڵ�
            cards[i].transform.DOMove(splinePosition + new Vector3(transform.position.x, transform.position.y, 0) + 0.2f * i * Vector3.back, duration);//
            cards[i].SetTheGroupOrder(i);//�޸��������Ⱦ˳��

            // ʹ��DOTween������ת���Ƶ�Ŀ��Ƕ�
            cards[i].transform.DORotateQuaternion(rotation, duration);
        }

        // �ȴ����ж�����ɺ����Э��
        yield return new WaitForSeconds(duration);
    }

}
