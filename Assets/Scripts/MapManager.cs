using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{

    private static MapManager _instance;

    public static MapManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public Sprite cellSprite;
    public CellInfo pointerOnCell, cellToMove;
    public int mapSizeX, mapSizeY;
    readonly float mapScale = 3.125f;

    public bool isMoving, clearMoveToCell;

    public Map _map;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        DontDestroyOnLoad(this);
        
    }

    private void Start ()
	{
	    _map = new Map(mapSizeX, mapSizeY);
	    CreateMap();
	    _map.PopulateEmptyCellList();
    }

    //create gameobjects as map
    private void CreateMap()
    {
        for (var x = 0; x < mapSizeX; x++)
        {
            for (var y = 0; y < mapSizeY; y++)
            {
                var cell = new GameObject();
                cell.transform.position = new Vector3((float) (y - mapSizeY / 2) / mapScale, (float)(x - mapSizeX / 2) / mapScale, 0);
                cell.transform.name = "Cell_" + (x /*- mapSizeX / 2*/ ) + "_" + (y /*- mapSizeY / 2*/);
                cell.transform.parent = /*map.*/transform;
                CellInfo cellInfo = cell.AddComponent(typeof(CellInfo)) as CellInfo;
                cellInfo.position = new IntegerVector2(x, y);
                cellInfo.cell = _map.FindCell(x, y);
                cellInfo.cell.type = ObjectFactory.GetObject(ObjectTypes.Empty);
                cellInfo.cell.type.Position = new IntegerVector2(x, y);
                _map.FindCell(x, y).Renderer = cell.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
                _map.FindCell(x, y).Renderer.sprite = cellSprite;
                _map.FindCell(x, y).Renderer.color = _map.GetCellColor(x, y);
            }
        }
    }

    //update color of map
    private void UpdateMap() 
    {
        for (var i = 0; i < mapSizeX; i++)
        {
            for (var j = 0; j < mapSizeY; j++)
            {
                _map.FindCell(i, j).Renderer.color = _map.GetCellColor(i, j);
            }
        }

        if (cellToMove && cellToMove.cell.type.GetType() != typeof(Empty))
        {
            cellToMove.cell.Renderer.color = Color.blue;
        }
        else if (cellToMove && cellToMove.cell.type.GetType() == typeof(Empty))
        {
            cellToMove = null;
        }

        
    }

    //set cell to move but wait to receive position to move
    public void MoveCell(CellInfo from)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (from.cell.type.GetType() == typeof(Empty))
        {
            cellToMove = null;
            return;
        }
        

        if (cellToMove)
        {
            cellToMove.cell.Renderer.color = cellToMove.cell.type.Color;
        }
        cellToMove = from;

        cellToMove.cell.Renderer.color = Color.blue;
    }

    //set target position for cell to move and find path from Pathfinding class
    public void MoveHereCell(CellInfo to)
    {
        if (cellToMove && !isMoving)
        {
            var path = Pathfinding.FindPath(_map, cellToMove.position, to.position);

            if (path.Count > 0)
            {
                StartCoroutine(MoveCell(cellToMove, path));
                cellToMove.cell.type = ObjectFactory.GetObject(ObjectTypes.Empty);
                cellToMove.cell.Renderer.color = cellToMove.cell.type.Color;
                //WakeUpCell(cellToMove); 
                cellToMove = null;
            }
            else
            {
                Debug.Log("No available Path found."); 
            }
        }
    }

    //move cell based on desired path 10 times in a second
    private IEnumerator MoveCell(CellInfo cellInfo, List<IntegerVector2> path)
    {
        isMoving = true;
        clearMoveToCell = false;
        var objectsType = cellInfo.cell.type; 

        int j = -1;
        for (int i = 0; i < path.Count; i++)
        {
            _map.FindCell(path[i].x, path[i].y).type = objectsType;
            if (j >= 0)
            {
                _map.FindCell(path[j].x, path[j].y).type = ObjectFactory.GetObject(ObjectTypes.Empty);
            }
            j++;

            objectsType.Position = new IntegerVector2(path[i].x, path[i].y);
            UpdateMap();
            yield return new WaitForSeconds(0.1f);
        }

   
        if (!cellToMove && !clearMoveToCell)
        {
            CellInfo _cell = _map.GetCell(path[path.Count - 1].x, path[path.Count - 1].y).info;
            WakeUpCell(_cell);
            MoveCell(_cell); 
        }

        clearMoveToCell = false;
        isMoving = false;
    }

    //tell Map class to draw object
    public void DrawObject(IObject type)
    {
        if(isMoving) return;

        if (type.GetType() == typeof(Soldier)) // check if type is kind of soldier
        {
            Cell _cell = _map.GetRandomEmptyCell();
            
            if (_cell == null)
            {
                Debug.Log("No space left for new soldier");
                return;
            }

            type.Position = _cell.info.position;
            _map.DrawObject(type, _cell);

        }
        else if (pointerOnCell)
        {

            _map.DrawObject(type, pointerOnCell.cell);

        }

        UpdateMap();
    }

    //wake up cell and tell CanvasUI to setup information about this cell
    public void WakeUpCell(CellInfo info)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;


        if (info.cell.type.GetType() != typeof(Soldier)) 
        {
            cellToMove = null;
            clearMoveToCell = true;
        }

        if (cellToMove)
        {
            cellToMove.cell.Renderer.color = cellToMove.cell.type.Color;
        }
        CanvasUI.Instance.SetInformationObject(info.cell.type);

        UpdateMap();
    }

}