using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public Sprite[] diceSides;
    private Rigidbody2D rbDice; 
    private int sideUp = 1;
    [SerializeField]private SpriteRenderer rend;
    //private float dist;
    public GameObject bowl;
    private Vector3 velocity;
    private float maxVelocity = 10f;
    private float multiplier = 100.0f;
    public int dicePosition;
    public bool hasStopped;
    public float timeCreated;

    private void Start()
    {
        rbDice = GetComponent<Rigidbody2D>();
        bowl = GameObject.FindGameObjectWithTag("Bowl");

        //dist = (bowl.transform.position - this.transform.position).magnitude;
        velocity = new Vector3(Random.Range(-1.0f * maxVelocity, maxVelocity), Random.Range(-1.0f * maxVelocity, maxVelocity), 0);
        rbDice.AddForce(velocity*multiplier);
        timeCreated = Time.time;
    }

    private void Update()
    {
        //dist = (bowl.transform.position - this.transform.position).magnitude;
    }

    public int Roll()
    {
        int finalRoll = Random.Range(1, 6);
        StartCoroutine(RollTheDice(finalRoll));
        
        return dicePosition;
    }

    public IEnumerator RollTheDice(int r)
    {
        int randomDiceSide = 0;

        yield return new WaitForSeconds(0.05f);

        while (isMoving())
        {
            randomDiceSide = Random.Range(0, 5);

            rend.sprite = diceSides[randomDiceSide];

            yield return new WaitForSeconds(0.05f);
        }

        rend.sprite = diceSides[r - 1];

        sideUp = r;
        dicePosition = getDicePosition();
    }

    public void resetDice()
    {
        sideUp = 1;
    }

    public int getSideUp()
    {
        return sideUp;
    }

    private bool isMoving()
    {
        if (this.rbDice.velocity.magnitude > 0.5)
        {
            return true;
        }

        return false;
    }

    public bool isStopped()
    {
        if (Time.time - timeCreated < 0.1) return false;
        return this.rbDice.velocity.magnitude <= 0;
    }

    public void DestroyDice()
    {
        Destroy(gameObject);
    }

    public int getDicePosition()
    {
        float x = this.transform.position.x;
        float y = this.transform.position.y;

        if (x <= 0 && y >= 0)
        {
            return 1;
        } else if (x > 0 && y >= 0)
        {
            return 2;
        } else if (x <= 0 && y <= 0)
        {
            return 3;
        } else
        {
            return 4;
        }
    }
}
