using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int unitDice;

    public int maxHP;
    public int currentHP;
    public Move move1;
    public Move move2;
    public Move move3;
    public Move move4;

    public List<Move> moveList;

    private void Start()
    {
        move1 = moveList[0];
        move2 = moveList[1];
        move3 = moveList[2];
        move4 = moveList[3];

        currentHP = maxHP;
    }

    public bool updateHealth(int amount)
    {
        if (amount >= 0)
        {
            currentHP += amount;
        } else
        {
            Heal(amount);
        }

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
