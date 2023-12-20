using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Tile : MonoBehaviour
{
    
    [SerializeField] protected SpriteRenderer rend;
    [SerializeField] private GameObject highlight;
    [SerializeField] protected Unit tileUnit;
    public TextMeshProUGUI panelText;

    public virtual void Init(int x, int y)
    {
        
    }

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
        Debug.Log(this.ToString());
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }
}
