using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    //public int damage;

    public int maxHP;
    public int currentHP;
    public Move currentMove;

    public List<Move> moveList;

    private void Start()
    {
        currentMove = moveList[0];
    }

    public bool TakeDamage(int dmg)
    {
        currentHP += dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public void setVisible(bool b)
    {
        this.transform.GetChild(0).gameObject.SetActive(b);
    }
}
