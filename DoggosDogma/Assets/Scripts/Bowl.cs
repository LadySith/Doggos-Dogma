using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Bowl : MonoBehaviour
{
    private List<Dice> dicePool = new();
    [SerializeField] private Dice dicePrefab;
    [SerializeField] private int diceCount;
    public float radius;

    public TextMeshProUGUI Move1Text;
    public TextMeshProUGUI Move2Text;
    public TextMeshProUGUI Move3Text;
    public TextMeshProUGUI Move4Text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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

        for (int i = 0; i < diceCount; i++)
        {
            Vector2 randomPoint = new Vector2(this.transform.position.x, this.transform.position.y) + Random.insideUnitCircle * radius;
            Dice newDice = Instantiate(dicePrefab, new Vector3(randomPoint.x, randomPoint.y), Quaternion.identity);
            dicePool.Add(newDice);
            newDice.Roll();
        }
    }

    public void setUpBoard(Unit unit)
    {
        Move1Text.SetText((unit.move1 != null) ? unit.move1.name : "");
        Move2Text.SetText((unit.move2 != null) ? unit.move2.name : "");
        Move3Text.SetText((unit.move3 != null) ? unit.move3.name : "");
        Move4Text.SetText((unit.move4 != null) ? unit.move4.name : "");
    }
}
