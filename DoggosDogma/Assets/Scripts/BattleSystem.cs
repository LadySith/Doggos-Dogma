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

    public GameObject enemyGO;

    public GameObject DeathPrefab;
    public GameObject LoversPrefab;
    public GameObject FoolPrefab;
    public GameObject SkelePrefab;
    public GameObject MeowPrefab;
    public GameObject MegaPrefab;

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

    private void FixedUpdate()
    {
        foreach (Dice x in bowl.dicePool)
        {
            if (!x.isStopped())
            {
                return;
            }
        }

        if (!bowl.finishedRolling)
        {
            StartCoroutine(startMoves());
            bowl.finishedRolling = true;
        }
    }

    public void NextTurn()
    {
        if (state == BattleState.PLAYERTURN)
        {
            state = BattleState.ENEMYTURN;
            EnemyTurn();
        } else if (state == BattleState.ENEMYTURN)
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    IEnumerator startMoves()
    {
        foreach (Dice x in bowl.dicePool)
        {
            x.hasStopped = true;
            yield return doMove(x.getDicePosition());
            Debug.Log(x.dicePosition, x);
        }

        NextTurn();
    }

    IEnumerator SetupBattle()
    {
        playerUnit = GameManager.instance.player;
        playerUnit.transform.position = playerBattleStation.transform.position;
        playerUnit.setVisible(true);
        bowl.setUpBoard(playerUnit);

        enemyUnit = GameManager.instance.enemy;

        if (enemyUnit.name == "Death")
        {
            enemyUnit = Instantiate(DeathPrefab, enemyBattleStation).GetComponent<Unit>();
        } else if (enemyUnit.name.Contains("Lovers"))
        {
            enemyUnit = Instantiate(LoversPrefab, enemyBattleStation).GetComponent<Unit>();
        } else if (enemyUnit.name.Contains("Fool"))
        {
            enemyUnit = Instantiate(FoolPrefab, enemyBattleStation).GetComponent<Unit>();
        } else if (enemyUnit.name == "Skelesoldier")
        {
            enemyUnit = Instantiate(SkelePrefab, enemyBattleStation).GetComponent<Unit>();
        } else if (enemyUnit.name == "Meowstopheles")
        {
            enemyUnit = Instantiate(MeowPrefab, enemyBattleStation).GetComponent<Unit>();
        } else if (enemyUnit.name.Contains("Mega"))
        {
            enemyUnit = Instantiate(MegaPrefab, enemyBattleStation).GetComponent<Unit>();
        }

        dialogueText.SetText("You encountered " + enemyUnit.unitName + "!");

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack(Move m)
    {
        dialogueText.text = playerUnit.unitName + " used " + m.moveName + "!";

        yield return new WaitForSeconds(1f);

        bool isDead = enemyUnit.updateHealth(m.healthPoints);

        enemyHUD.SetHP(enemyUnit);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        } else
        {
            //state = BattleState.ENEMYTURN;
            //StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyAttack(Move m)
    {
        dialogueText.text = enemyUnit.unitName + " used " + m.moveName + "!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.updateHealth(m.healthPoints);

        playerHUD.SetHP(playerUnit);

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            //state = BattleState.PLAYERTURN;
            //StartCoroutine(PlayerTurn());
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

    public void EnemyTurn()
    {
        bowl.setUpBoard(enemyUnit);
        dialogueText.text = enemyUnit.unitName + " attacks!";
        bowl.RollAll();
    }

    void PlayerTurn()
    {
        bowl.setUpBoard(playerUnit);
        dialogueText.SetText("Choose an action:");
    }

    IEnumerator PlayerHeal(Move m)
    {
        playerUnit.updateHealth(m.healthPoints);

        playerHUD.SetHP(playerUnit);
        dialogueText.SetText("You've been healed.");

        yield return new WaitForSeconds(2f);
    }

    IEnumerator EnemyHeal(Move m)
    {
        enemyUnit.updateHealth(m.healthPoints);

        enemyHUD.SetHP(enemyUnit);
        dialogueText.SetText(enemyUnit.unitName + " healed.");

        yield return new WaitForSeconds(2f);
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

    public IEnumerator doMove(int x)
    {
        Move thisMove;
        if (x == 1)
        {
            thisMove = GameManager.instance.findMove(bowl.Move1Text.text);
        } else if (x == 2)
        {
            thisMove = GameManager.instance.findMove(bowl.Move2Text.text);
        } else if (x == 3)
        {
            thisMove = GameManager.instance.findMove(bowl.Move3Text.text);
        } else if (x == 4)
        {
            thisMove = GameManager.instance.findMove(bowl.Move4Text.text);
        } else
        {
            thisMove = GameManager.instance.emptyMove;
        }

        if (state == BattleState.PLAYERTURN)
        {
            if (thisMove.moveName == "Heal")
            {
                yield return (PlayerHeal(thisMove));
            } else
            {
                yield return (PlayerAttack(thisMove));
            }
        }

        if (state == BattleState.ENEMYTURN)
        {
            if (thisMove.moveName == "Heal")
            {
                yield return (EnemyHeal(thisMove));
            }
            else
            {
                yield return (EnemyAttack(thisMove));
            }
        }
    }

    public void OnRollButton()
    {
        bowl.RollAll();
    }
}
