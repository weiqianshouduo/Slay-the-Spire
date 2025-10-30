using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CostUI : MonoBehaviour
{
    [SerializeField] private TMP_Text Cost;
    public void UpdateCostText(int text)
    {
        Cost.text = text.ToString();
    }
}
