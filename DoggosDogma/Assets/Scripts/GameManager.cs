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

    public GridManager gridManager;

    public List<GameObject> grassUnits;
    public List<GameObject> forestUnits;

    public TextMeshProUGUI levelTextBox;
    public TextMeshProUGUI healthTextBox;

    public Dictionary<Vector2, Tile> tiles;

    public Unit player;
    public Unit enemy;

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
        levelTextBox.SetText("Lvl " + player.unitLevel);
        healthTextBox.SetText(player.currentHP + "/" + player.maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getRandomUnit(List<GameObject> list)
    {
        return(list[Random.Range(0, list.Count)]);
    }
    
}
