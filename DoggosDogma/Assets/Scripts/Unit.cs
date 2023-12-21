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

    Move emptyMove;

    public List<Move> moveList;

    private void Start()
    {
        emptyMove = GameManager.instance.emptyMove;
        move1 = (moveList.Count > 0) ? moveList[0] : emptyMove;
        move2 = (moveList.Count > 1) ? moveList[1] : emptyMove;
        move3 = (moveList.Count > 2) ? moveList[2] : emptyMove;
        move4 = (moveList.Count > 3) ? moveList[3] : emptyMove;

        currentHP = maxHP;
    }

    public bool updateHealth(int amount)
    {
        if (amount <= 0)
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
