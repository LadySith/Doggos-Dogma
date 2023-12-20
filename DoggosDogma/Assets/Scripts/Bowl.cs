using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowl : MonoBehaviour
{
    private List<Dice> dicePool;
    [SerializeField] private Dice dicePrefab;
    [SerializeField] private int diceCount;
    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        dicePool = new List<Dice>(diceCount);
        for (int i = 0; i < diceCount; i++)
        {
            Vector2 randomPoint = new Vector2(this.transform.position.x,this.transform.position.y) + Random.insideUnitCircle * radius;
            Dice newDice = Instantiate(dicePrefab, new Vector3(randomPoint.x,randomPoint.y), Quaternion.identity);
            dicePool.Add(newDice);
            //newDice.Roll();
        }
    }


}