using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public enum GameState
{
    GENERATEGRID = 0,
    MAP = 1,
    STARTBATTLE = 2,
    BRUNOTURN = 3,
    ENEMYTURN = 4,
    WIN = 5,
    LOSE = 6
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static GridManager gridManager;

    public GameObject meow;
    public List<GameObject> grassUnits;
    public List<GameObject> forestUnits;

    public List<Move> allMoves;

    public Dictionary<Vector2, Tile> tiles;

    public GameObject tileHolder;

    public Move emptyMove;

    public Unit player;
    public Unit enemy;

    public Vector2 BrunoPosition;
    public Vector2 OldBrunoPosition;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        tiles = new Dictionary<Vector2, Tile>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //levelTextBox.SetText("Lvl " + player.unitLevel);
        //healthTextBox.SetText(player.currentHP + "/" + player.maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getRandomUnit(List<GameObject> list)
    {
        return(list[Random.Range(0, list.Count)]);
    }

    public Move findMove(string moveName)
    {
        for (int i = 0; i < allMoves.Count; i++)
        {
            if (moveName == allMoves[i].moveName)
            {
                return allMoves[i];
            }
        }

        return emptyMove;
    }

    public void ChangeBrunoIconOnClick(Vector2 newBrunoLocation)
    {
        tiles[OldBrunoPosition].brunoIcon.SetActive(false);
        tiles[BrunoPosition].brunoIcon.SetActive(false);
        OldBrunoPosition = BrunoPosition;
        BrunoPosition = newBrunoLocation;
        tiles[BrunoPosition].brunoIcon.SetActive(true);
    }
}
