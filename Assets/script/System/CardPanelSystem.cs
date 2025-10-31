using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class CardPanelSystem : MonoBehaviour
{
    [SerializeField] private GameObject CardusingSee;
    public bool NotEnterThePanel = true;
    public List<Card> drawDardPile;
    public List<Card> disCardPile;
    public GameObject CardPanel;
    public GameObject CardCheckPanel;
    public List<GameObject> CardToseeS;
    public void Start()
    {
        CardPanel.SetActive(false);
    }
    public void UseUpdateDrawPile()
    {
        StartCoroutine(UpdateDrawPile());
    }
     public void UseUpdateDiscardPile()
    {
        StartCoroutine(UpdateDiscardPile());
    }

    public IEnumerator UpdateDrawPile()
    {
        Debug.Log(NotEnterThePanel);
        if (NotEnterThePanel)
        {
            NotEnterThePanel = false;
            CardCheckPanel.SetActive(true);
            CardCheckPanel.transform.localScale = Vector3.zero;
           Tween tween =   CardCheckPanel.transform.DOScale(new Vector3(1, 1, 1), 0.15f);
            yield return tween.WaitForCompletion();
            CardPanel.SetActive(true);
            drawDardPile = CardSystem.Instance.ReturnDarwCard();
            foreach (var card in drawDardPile)
            {
                GameObject gameObject = Instantiate(CardusingSee, CardCheckPanel.transform);
                gameObject.GetComponent<CardOnlySee>().SetUp(card);
                gameObject.transform.localScale = Vector3.zero;
                gameObject.transform.DOScale(new Vector3(1, 1, 1), 0.15f);
                CardToseeS.Add(gameObject);
            }
        }
    }
    public IEnumerator UpdateDiscardPile()
    {
         Debug.Log(NotEnterThePanel);
        if (NotEnterThePanel)
        {   NotEnterThePanel = false;
            CardCheckPanel.SetActive(true);
            CardCheckPanel.transform.localScale = Vector3.zero;
           Tween tween =   CardCheckPanel.transform.DOScale(new Vector3(1, 1, 1), 0.15f);//¶¯»­Ð§¹û
            yield return tween.WaitForCompletion();
            CardPanel.SetActive(true);
            drawDardPile = CardSystem.Instance.ReturnDisCard();
            foreach (var card in drawDardPile)
            {
                GameObject gameObject = Instantiate(CardusingSee, CardCheckPanel.transform);
                gameObject.GetComponent<CardOnlySee>().SetUp(card);
                gameObject.transform.localScale = Vector3.zero;
                gameObject.transform.DOScale(new Vector3(1, 1, 1), 0.15f);
                CardToseeS.Add(gameObject);
            }
        }
        
    }
    public void CloseThePanle()
    {
        for(int i =0;i<CardToseeS.Count; i++)
        {
            Destroy(CardToseeS[i].gameObject);
        }
        NotEnterThePanel = true;
        CardPanel.transform.DOScale(Vector3.zero, 0.15f).OnComplete(() =>
        {
        CardPanel.transform.localScale = new Vector3(1, 1, 1);
        CardToseeS.Clear();
        CardPanel.SetActive(false);
        CardCheckPanel.SetActive(false);
            
        });
    }

}
