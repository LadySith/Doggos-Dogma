using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private Tile grassTile, forestTile, homeTile;
    [SerializeField] private Transform cam;

    private Dictionary<Vector2, Tile> tiles;

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();
        var homeX = Random.Range(0, width - 1);
        var homeY = Random.Range(0, height - 1);
        var spawnedHome = Instantiate(homeTile,new Vector3(homeX,homeY),Quaternion.identity);
        spawnedHome.name = $"Tile {homeX} {homeY}";
        //Init
        tiles[new Vector2(homeX, homeY)] = spawnedHome;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (!(x == homeX && y == homeY))
                {
                    var randomTile = Random.Range(0, 6) == 3 ? forestTile : grassTile;
                    var spawnedTile = Instantiate(randomTile, new Vector3(x, y), Quaternion.identity);
                    spawnedTile.name = $"Tile {x} {y}";

                    spawnedTile.Init(x, y);

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
