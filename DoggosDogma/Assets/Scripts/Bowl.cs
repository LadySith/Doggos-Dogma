using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Bowl : MonoBehaviour
{
    public List<Dice> dicePool = new();
    [SerializeField] private Dice dicePrefab;
    public int diceCount;
    public float radius;

    public TextMeshProUGUI Move1Text;
    public TextMeshProUGUI Move2Text;
    public TextMeshProUGUI Move3Text;
    public TextMeshProUGUI Move4Text;

    //public List<int> moveOrder = new();

    public bool finishedRolling = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //private void LateUpdate()
    //{
    //    foreach (Dice x in dicePool)
    //    {
    //        if (!x.isStopped())
    //        {
    //            return;
    //        }
    //    }

    //    foreach (Dice x in dicePool)
    //    {
    //        if (!x.hasStopped)
    //        {
    //            x.hasStopped = true;
    //            moveOrder.Add(x.getDicePosition());
    //            Debug.Log(x.dicePosition, x);
    //        }
            
    //    }
    //}

    public void RollAll()
    {
        if (dicePool != null)
        {
            foreach (Dice x in dicePool)
            {
                x.DestroyDice();
            }

            dicePool.Clear();
        }

        finishedRolling = false;

        for (int i = 0; i < diceCount; i++)
        {
            Vector2 randomPoint = new Vector2(this.transform.position.x, this.transform.position.y) + Random.insideUnitCircle * radius;
            Dice newDice = Instantiate(dicePrefab, new Vector3(randomPoint.x, randomPoint.y), Quaternion.identity);
            dicePool.Add(newDice);
            //newDice.Roll();
            StartCoroutine(newDice.RollTheDice(Random.Range(1, 6)));
        }
    }

    public void setUpBoard(Unit unit)
    {
        if (dicePool != null)
        {
            foreach (Dice x in dicePool)
            {
                x.DestroyDice();
            }

            dicePool.Clear();
        }

        diceCount = unit.unitDice;
        Move1Text.SetText((unit.move1 != null) ? unit.move1.name : "");
        Move2Text.SetText((unit.move2 != null) ? unit.move2.name : "");
        Move3Text.SetText((unit.move3 != null) ? unit.move3.name : "");
        Move4Text.SetText((unit.move4 != null) ? unit.move4.name : "");
    }
}
