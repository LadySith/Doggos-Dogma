using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public Sprite[] diceSides;
    private Rigidbody2D rbDice; 
    private int sideUp = 1;
    private SpriteRenderer rend;
    private float dist;
    public GameObject bowl;
    private Vector3 velocity;
    private float maxVelocity = 10f;
    private float multiplier = 100.0f;

    private void Start()
    {
        rbDice = GetComponent<Rigidbody2D>();
        bowl = GameObject.FindGameObjectWithTag("Bowl");
        rend = GetComponentInChildren<SpriteRenderer>();

        dist = (bowl.transform.position - this.transform.position).magnitude;
        velocity = new Vector3(Random.Range(-1.0f * maxVelocity, maxVelocity), Random.Range(-1.0f * maxVelocity, maxVelocity), 0);
        rbDice.AddForce(velocity*multiplier);
    }

    private void Update()
    {
        dist = (bowl.transform.position - this.transform.position).magnitude;
    }

    public void Roll()
    {
        StartCoroutine(RollTheDice());
    }

    private IEnumerator RollTheDice()
    {
        int randomDiceSide = 0;

        int finalSide = 0;

        for (int i = 0; i <= 20; i++)
        {
            randomDiceSide = Random.Range(0, 5);

            rend.sprite = diceSides[2]; //don't know why this doesn't work

            yield return new WaitForSeconds(0.05f);
        }

        finalSide = randomDiceSide + 1;
        sideUp = finalSide;
    }

    public void resetDice()
    {
        sideUp = 1;
    }

    public int getSideUp()
    {
        return sideUp;
    }
}
