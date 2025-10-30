using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CardView : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text cost;
    [SerializeField] private SpriteRenderer Image;
    [SerializeField] private GameObject wrapper;
    [SerializeField] private SortingGroup sorting;
    [SerializeField] private LayerMask dropLayMask;
    private Vector3 dargStartPos;
    private Quaternion dargStartRot;
    public Card card{ get; private set; }
    public void SetUp(Card _card)
    {
        card = _card;
        title.text = _card.Title;
        description.text = _card.Description;
        cost.text = _card.Cost.ToString();
        Image.sprite = _card.Image;
    }
    public void SetTheGroupOrder(int i)
    {
        sorting.sortingOrder = i;
    }

    void OnMouseEnter()
    {
        if (!Interactions.Instance.PlayerCanHover()) return;//≈–∂œ «∑Òƒ‹πª∆Ù”√–¸Õ£
            Vector3 pos = new(transform.position.x, 0);
            CardHoverSystem.Instance.Show(card, transform.position, pos);
            wrapper.SetActive(false);
    }
    void OnMouseExit()
    {
        if (!Interactions.Instance.PlayerCanHover()) return;//≈–∂œ «∑Òƒ‹πª∆Ù”√–¸Õ£
        wrapper.SetActive(true);
        CardHoverSystem.Instance.Hide();
    }
    void OnMouseUp()
    {
        if (!Interactions.Instance.PlayerCanInteract()) return;
        bool hit = Physics2D.Raycast(
        transform.position,       // ??????????2D???
        Vector2.right,            // ?????2D????right=X????up=Y????
        10,                      // ???????10??
        dropLayMask               // ????????????????
        );
        if(card.ManualTargeteffect != null)
        {
            Debug.Log(1);
            EnemyView target = ManualTargetSystem.Instance.EndTargeting(MouseUtil.GetMousePositionInWorldSpace());//?????enemyview
              Debug.Log(2);
            if(target!= null && CostSystem.Instance.HasEnoughCost(card.Cost))
            {
                  Debug.Log(3);
                PlayCardGA playCardGA = new(card,target);
                ActionSystem.Instance.Perform(playCardGA);
            }
        }
        else
        { 
        if(hit&&CostSystem.Instance.HasEnoughCost(card.Cost))
        {
            PlayCardGA playCardGA = new(card);
            ActionSystem.Instance.Perform(playCardGA);//?????????
        }
        else
        {
            transform.position = dargStartPos;
            transform.rotation = dargStartRot;
        }
        Interactions.Instance.PlayerIsDragging = false;
        }


    }
    void OnMouseDown()
    {
        if (!Interactions.Instance.PlayerCanInteract()) return;
        if(card.ManualTargeteffect!= null)
        {
            ManualTargetSystem.Instance.StartTargeting(transform.position);//??????????
        }
        else
        {
        Interactions.Instance.PlayerIsDragging = true;
        wrapper.SetActive(true);
        CardHoverSystem.Instance.Hide();
        dargStartPos = transform.position;
        dargStartRot = transform.rotation;
        transform.rotation = Quaternion.Euler(0, 0, 0);
            
        }

    }
    void OnMouseDrag()
    {
        if (!Interactions.Instance.PlayerCanInteract()) return;
        if (card.ManualTargeteffect != null) return;
        transform.position = MouseUtil.GetMousePositionInWorldSpace(-1);
        
    }


}
