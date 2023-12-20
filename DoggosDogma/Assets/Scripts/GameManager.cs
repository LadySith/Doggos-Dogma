using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
