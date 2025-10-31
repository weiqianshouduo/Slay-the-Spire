using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCardDescriptionSystem : Singleton<ShowCardDescriptionSystem>
{
    public GameObject ShowPanel;
    public CardOnlySee TheCardUseTosee;
    public void Start()
    {
        ShowPanel.SetActive(false);
    }
    public void Show(Card card)
    {
        ShowPanel.SetActive(true);
        TheCardUseTosee.SetUp(card);
    }
    public void Close()
    {
        ShowPanel.SetActive(false);
    }
    
  
}
