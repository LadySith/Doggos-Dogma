using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using UnityEngine.SceneManagement;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;
    public Tile grassTile, forestTile, homeTile;
    [SerializeField] private Transform cam;
    [SerializeField] private TextMeshProUGUI panelTextBox;
    public TextMeshProUGUI levelTextBox;
    public TextMeshProUGUI healthTextBox;

    Dictionary<Vector2, Tile> tiles;

    

    private void Start()
    {
        levelTextBox.SetText("Lvl "+ GameManager.instance.player.unitLevel);
        healthTextBox.SetText(GameManager.instance.player.currentHP + "/" + GameManager.instance.player.maxHP);
        tiles = GameManager.instance.tiles;

        if (tiles.Count == 0)
        {
            GenerateGrid();
        }
        else
        {
            cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, cam.transform.position.z);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    tiles[new Vector2(x, y)].panelText = panelTextBox;
                }
            }
        }
    }

    void GenerateGrid()
    {
        var homeX = Random.Range(0, width - 1);
        var homeY = Random.Range(0, height - 1);
        var spawnedHome = Instantiate(homeTile,new Vector3(homeX,homeY),Quaternion.identity);
        spawnedHome.transform.SetParent(GameObject.Find("TileHolder").transform, false);
        spawnedHome.name = $"Tile {homeX} {homeY}";
        spawnedHome.Init(homeX, homeY);
        spawnedHome.panelText = panelTextBox;

        

        tiles[new Vector2(homeX, homeY)] = spawnedHome;

        tiles[new Vector2(homeX, homeY)].brunoIcon.SetActive(true);

        GameManager.instance.BrunoPosition = new Vector2(homeX, homeY);
        GameManager.instance.OldBrunoPosition = new Vector2(homeX, homeY);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (!(x == homeX && y == homeY))
                {
                    var randomTile = Random.Range(0, 2) == 1 ? forestTile : grassTile;
                    var spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.identity);
                    spawnedTile.transform.SetParent(GameObject.Find("TileHolder").transform, false);
                    spawnedTile.name = $"Tile {x} {y}";

                    spawnedTile.Init(x, y);
                    spawnedTile.panelText = panelTextBox;

                    tiles[new Vector2(x, y)] = spawnedTile;
                }
            }
        }

        cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, cam.transform.position.z);
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        if (tiles.TryGetValue(pos, out var tile))
        {
            return tile;
        }

        return null;
    }

    
    
}
