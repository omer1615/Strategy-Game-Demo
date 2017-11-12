using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellInfo : MonoBehaviour
{

    public Cell cell;
    private BoxCollider2D collider;
    public IntegerVector2 position;

    public int gCost;
    public int hCost;
    public float penalty;
    public CellInfo parent;

    void Awake()
    {
        //add collider to make this gameobject interactable
        collider = gameObject.AddComponent(typeof(BoxCollider2D)) as BoxCollider2D;
        collider.size = new Vector2(0.32f, 0.32f);
        StartCoroutine(WaitUntilHasCell());
    }


    void OnMouseOver()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            MapManager.Instance.WakeUpCell(this);

            if (cell.type.GetType() == typeof(Soldier))
            {
                MapManager.Instance.MoveCell(this);
            }
        }

        MapManager.Instance.pointerOnCell = this;
        //if this cell is empty and we pressed right mouse button tell MapManager to move selected soldier to here
        if(Input.GetMouseButtonDown(1) && cell.type.GetType() == typeof(Empty))
        {
            MapManager.Instance.MoveHereCell(this);
        }
    } 

    //wait until this cell has and referance this cellinfo
    IEnumerator WaitUntilHasCell()
    {
        yield return cell;
        cell.info = this;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

}
