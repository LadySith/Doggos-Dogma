using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public abstract class Tile : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer rend;
    [SerializeField] private GameObject highlight;
    public int xPos;
    public int yPos;
    public GameObject brunoIcon;
    public TextMeshProUGUI panelText;
    public Unit unit;

    public virtual void Init(int x, int y)
    {
        xPos = x;
        yPos = y;
    }

    private void OnMouseEnter()
    {
        if ((System.Math.Abs(GameManager.instance.BrunoPosition.x - xPos) < 2) && (System.Math.Abs(GameManager.instance.BrunoPosition.y - yPos) < 2))
        {
            highlight.SetActive(true);
        }
        
        panelText.SetText(this.ToString() + "\n" + unit.name);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
        panelText.SetText("");
    }

    private void OnMouseDown()
    {
        if ((System.Math.Abs(GameManager.instance.BrunoPosition.x - xPos) < 2) && (System.Math.Abs(GameManager.instance.BrunoPosition.y - yPos) < 2))
        {
            highlight.SetActive(true);

            GameManager.instance.ChangeBrunoIconOnClick(new Vector2(xPos, yPos));
            GameManager.instance.enemy = unit;
            GameManager.instance.tileHolder.SetActive(false);
            SceneManager.LoadScene("BattleScene");
        }
        
    }
}
