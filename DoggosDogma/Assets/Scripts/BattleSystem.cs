using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public TextMeshProUGUI dialogueText;

    public Bowl bowl;

    public BattleState state;
    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        playerUnit = GameManager.instance.player;
        playerUnit.transform.position = playerBattleStation.transform.position;
        playerUnit.setVisible(true);
        bowl.setUpBoard(playerUnit);

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.SetText("You encountered " + enemyUnit.unitName + "!");

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        dialogueText.text = playerUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = enemyUnit.updateHealth(playerUnit.move1.healthPoints);

        enemyHUD.SetHP(enemyUnit);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        } else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.SetText("You defeated " + enemyUnit.unitName + "!");
        } else if (state == BattleState.LOST)
        {
            dialogueText.SetText("You were defeated...");
        }
    }

    IEnumerator EnemyTurn()
    {
        bowl.setUpBoard(enemyUnit);
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.updateHealth(enemyUnit.move1.healthPoints);

        playerHUD.SetHP(playerUnit);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        } else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void PlayerTurn()
    {
        dialogueText.SetText("Choose an action:");
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);

        playerHUD.SetHP(playerUnit);
        dialogueText.SetText("You've been healed.");

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyHeal()
    {
        enemyUnit.Heal(5);

        playerHUD.SetHP(enemyUnit);
        dialogueText.SetText(enemyUnit.unitName + " healed.");

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        StartCoroutine(EnemyTurn());
    }

    public void OnFleeButton()
    {
        StartCoroutine(Flee());
    }

    IEnumerator Flee()
    {
        dialogueText.SetText("Let's get outta here!");

        yield return new WaitForSeconds(2f);

        ReturnToMap();
    }

    public void ReturnToMap()
    {
        playerUnit.setVisible(false);
        GameManager.instance.tileHolder.SetActive(true);
        SceneManager.LoadScene("Game");
    }
}
