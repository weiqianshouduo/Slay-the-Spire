using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using DG.Tweening;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CardSystem :Singleton<CardSystem>
{
    [SerializeField] private HandView handView;
    [SerializeField] private Transform drawCardPoint;
    [SerializeField] private Transform discardPilePoint;
    private readonly List<Card> drawPile = new();//������
    private readonly List<Card> discardPile = new();//������
    private readonly List<Card> hand = new();//������ 
    void OnEnable()
    {
        ActionSystem.AttachPerformer<DrawCardGA>(DrawCardPerformer);
        ActionSystem.AttachPerformer<DisAllCardsGA>(DiscardAllPerformer);
        ActionSystem.AttachPerformer<PlayCardGA>(PlayCardPerformer);
  
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<DrawCardGA>();
        ActionSystem.DetachPerformer<DisAllCardsGA>();
        ActionSystem.DetachPerformer<PlayCardGA>();
       
    }
    public void SetUp(List<CardData> deckData)
    {
     foreach(var CardData in deckData)
        {
            Card card = new(CardData);//�����µ�cardΪ���ƶ��������
            drawPile.Add(card);
        }   
    }

    private IEnumerator DrawCardPerformer(DrawCardGA drawCardGA)
    {
        int actualAmount = math.min(drawCardGA.mount, drawPile.Count);//�õ����Ƶ������ͳ��ƶѵı�
        int notDrawAmount = drawCardGA.mount - actualAmount;//���������Ƿ��ܹ���ҳ�ȡӦ�е���
        for (int i = 0; i < actualAmount; i++)
        {
            yield return DrawCard();//����
        }
        if (notDrawAmount > 0)
        {
            RefillDeck();//�����ƿ� 
            for(int i = 0; i < notDrawAmount; i++)
            {
                if (!(drawPile.Count == 0 && discardPile.Count == 0))
                {
                    yield return DrawCard();
                }
            }
        }

    }
    private IEnumerator DiscardAllPerformer(DisAllCardsGA disAllCardsGA)
    {
        foreach (var card in hand)
        {
           
            CardView cardView = handView.RemoveCard(card);
            yield return DiscardCard(cardView);
        }
        hand.Clear();
    }
    private IEnumerator DrawCard()
    {
        Card card = drawPile.Draw();//���ƶѷ���card
        hand.Add(card);//�����б����
        CardView cardView = CardViewCreator.Instance.CreateCardView(card, drawCardPoint.position, Quaternion.identity);
        yield return handView.AddCard(cardView);//����������ӵ�����
    }
    private void RefillDeck()
    {
        drawPile.AddRange(discardPile);//��������������ϴ����ӵ�������
        discardPile.Clear();
    }
    private IEnumerator DiscardCard(CardView cardView)
    {
         discardPile.Add(cardView.card);//������ӵ�������
        cardView.transform.DOScale(Vector3.zero, .15f);//��������С
        Tween tween = cardView.transform.DOMove(discardPilePoint.position, .15f);//�ƶ�����
        yield return tween.WaitForCompletion();//��ͣ��ǰЭ�̵�ִ�У��ȴ� DOTween ������ɺ��ټ���ִ�к�������
        Destroy(cardView.gameObject);
    }
    private IEnumerator PlayCardPerformer(PlayCardGA playGardGA)
    {
        hand.Remove(playGardGA.card);//�������Ƴ��ֿ�
        discardPile.Add(playGardGA.card);//��ӵ�������
        CardView cardView = handView.RemoveCard(playGardGA.card);//ִ���Ƴ���������ͼ
        yield return DiscardCard(cardView);

        SpendCostGA spendCostGA = new(playGardGA.card.Cost);
        ActionSystem.Instance.AddReacion(spendCostGA);

        if (playGardGA.card.ManualTargeteffect != null)
        {
            PerformEffectGA performEffectGA = new(playGardGA.card.ManualTargeteffect, new() { playGardGA.ManualTarget });
            ActionSystem.Instance.AddReacion(performEffectGA);
        }
        
        foreach(var effectWrapper in playGardGA.card.OtherEffects)
        {
            List<CombatantView> targets = effectWrapper.targetMode.GetTargets();
            PerformEffectGA performEffectGA = new(effectWrapper.Effect,targets);
            ActionSystem.Instance.AddReacion(performEffectGA);
        }
        // ����Ч��
    }

    
}
