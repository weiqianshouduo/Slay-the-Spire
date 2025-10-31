using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class PerksUI : MonoBehaviour
{
    [SerializeField] private PerkUI perkUIPrefab;
    private readonly List<PerkUI> perkUIs = new();
    public void AddPerkUI(Perk perk)
    {
        PerkUI perkUI = Instantiate(perkUIPrefab, transform);
        perkUI.SetUp(perk);//Éè¶¨perk
        perkUIs.Add(perkUI);
    }
    public void RemovePerkUI(Perk perk)
    {
        PerkUI perkUI = perkUIs.Where(pui => pui.perk == perk).FirstOrDefault();
        if (perkUI != null)
        {
            perkUIs.Remove(perkUI);
            Destroy(perkUI.gameObject);
        }
    }

}
