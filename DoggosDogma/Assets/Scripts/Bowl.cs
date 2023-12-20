using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    private List<Dice> dicePool = new();
    [SerializeField] private Dice dicePrefab;
    [SerializeField] private int diceCount;
    public float radius;

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
        }

        for (int i = 0; i < diceCount; i++)
        {
            Vector2 randomPoint = new Vector2(this.transform.position.x, this.transform.position.y) + Random.insideUnitCircle * radius;
            Dice newDice = Instantiate(dicePrefab, new Vector3(randomPoint.x, randomPoint.y), Quaternion.identity);
            dicePool.Add(newDice);
            newDice.Roll();
        }
    }


}
