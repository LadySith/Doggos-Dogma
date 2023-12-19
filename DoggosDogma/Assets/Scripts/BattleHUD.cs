using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public Slider hpSlider;
    public TextMeshProUGUI hpText;

    public void SetHUD(Unit unit)
    {
        nameText.SetText(unit.unitName);
        levelText.SetText("Lvl " + unit.unitLevel);
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        hpText.SetText($"{ unit.currentHP}");
    }

    public void SetHP(Unit unit)
    {
        hpSlider.value = unit.currentHP;
        hpText.SetText($"{ unit.currentHP}");
    }
}
