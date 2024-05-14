using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI battleText;

    public void SetHUD(Unit unit)
    {
        battleText.text = unit.unitName + "   LVL:   " + unit.unitLevel.ToString() + "   HP:   " + unit.currentHP + "/" + unit.maxHP;
    }
    
    
}
