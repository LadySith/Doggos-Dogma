using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTile : Tile
{
    [SerializeField] private Color baseColor, offsetColor;

    // Start is called before the first frame update
    void Start()
    {
        unit = (GameManager.instance.getRandomUnit(GameManager.instance.grassUnits)).GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Init(int x, int y)
    {
        base.Init(x, y);
        var isOffset = (x + y) % 2 == 1;
        rend.color = isOffset ? offsetColor : baseColor;
    }
}
