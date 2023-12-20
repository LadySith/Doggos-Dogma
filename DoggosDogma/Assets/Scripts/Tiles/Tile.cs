using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public abstract class Tile : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer rend;
    [SerializeField] private GameObject highlight;
    public TextMeshProUGUI panelText;
    public Unit unit;

    public virtual void Init(int x, int y)
    {
        
    }

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
        panelText.SetText(this.ToString());
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
        panelText.SetText("");
    }

    private void OnMouseDown()
    {
        GameManager.instance.enemy = unit;
        SceneManager.LoadScene("BattleScene");
    }
}
