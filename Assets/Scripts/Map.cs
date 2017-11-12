using System;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    private int mapSizeX, mapSizeY;

    private List<List<Cell>> cellList = new List<List<Cell>>();
    private List<Cell> emptyCellList = new List<Cell>();

    // Create Map class with Map limits as horizontal and vertical limits
    public Map(int _mapSizeX, int _mapSizeY)
    {
        this.mapSizeX = _mapSizeX;
        this.mapSizeY = _mapSizeY;

        for (var i = 0; i < mapSizeX; i++)
        {
            List<Cell> cellListRow = new List<Cell>();
            for (int j = 0; j < mapSizeY; j++)
            {
                cellListRow.Add(new Cell());
            }
            cellList.Add(cellListRow);
        }
    }

    //get empty cells from cell list
    public void PopulateEmptyCellList()
    {
        emptyCellList.Clear();
        for (var i = 0; i < mapSizeX; i++)
        {
            for (int j = 0; j < mapSizeY; j++)
            {
                if (cellList[i][j].type.GetType() == typeof(Empty))
                {
                   emptyCellList.Add(cellList[i][j]); 
                }
            }
        }
    }

    //Draw an object based on properties of type of object on cells porisiton
    public void DrawObject(IObject type, Cell cell)
    {
        var cells = new Cell[type.CellSizeX * type.CellSizeY];
        var cellCounter = 0;
        var cellPosition = GetCellPosition(cell);

        if (cells.Length == 1)
        {

            if ((cellPosition.x) < 0 || (cellPosition.x) > (mapSizeX - 1) || (cellPosition.y) < 0 || (cellPosition.y) > (mapSizeY - 1))
            {
                Debug.Log("Object is outside of game area");
                return;
            }

            
            if (cell.type.GetType() != typeof(Empty)) 
            {
                PopulateEmptyCellList(); 
                if (emptyCellList.Count <= 0)
                {
                    Debug.Log("No Space for building");
                    return;
                }
                cellPosition = GetCellPosition(GetRandomEmptyCell());
            }

            cells[cellCounter] = cellList[cellPosition.x][cellPosition.y]; 

        }
        else if (cells.Length > 1)
        {
            for (int x = - (type.CellSizeX / 2); x < type.CellSizeX - (type.CellSizeX / 2); x++)
            {
                for (int y = -1; y < type.CellSizeY - 1; y++)
                {
                    if ((x + cellPosition.x) < 0 || (x + cellPosition.x) > (mapSizeX - 1) || (y + cellPosition.y) < 0 || (y + cellPosition.y) > (mapSizeY - 1))
                    {
                        Debug.Log("Object is outside of game area");
                        return;
                    }

                    cells[cellCounter] = cellList[x + cellPosition.x][y + cellPosition.y];
                
                    cellCounter++;
                }
            }

            foreach (var item in cells)
            {
                if (item.type.GetType() != typeof(Empty))
                {
                    Debug.Log("No Space for building or cell is not empty"); 
                    return;
                }
            }
        }
        

        type.Position = new IntegerVector2(cellPosition.x, cellPosition.y);
        GameManager.Instance.objecList.Add(type);

        foreach (var item in cells)
        {
            item.type = type;
            emptyCellList.Remove(item);
        }

    }

    //get color of cell at specific position
    public Color GetCellColor(int posx, int posy)
    {
        return cellList[posx][posy].type.Color;
    }

    //get cell at specific position
    public Cell FindCell(int posx, int posy)
    {
        return cellList[posx][posy]; 
    }

    //find cells around a cell
    public List<CellInfo> GetNeighbours(CellInfo node)
    {
        List<CellInfo> neighbours = new List<CellInfo>();
        
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.position.x + x;
                int checkY = node.position.y + y;

                if (checkX >= 0 && checkX < mapSizeX && checkY >= 0 && checkY < mapSizeY)
                {
                    neighbours.Add(cellList[checkX][checkY].info);
                }
            }
        }

        return neighbours;
    }

    //get a random empty cell in empty cell list
    public Cell GetRandomEmptyCell()
    {
        return emptyCellList.Count == 0 ? null : emptyCellList[UnityEngine.Random.Range(0, emptyCellList.Count - 1)];
    }

    //get cells position
    public IntegerVector2 GetCellPosition(Cell cell)
    {

        for (int x = 0; x < mapSizeX; ++x)
        {
            for (int y = 0; y < mapSizeY; ++y)
            {
                if (cell == cellList[x][y])
                {
                    return new IntegerVector2(x, y);
                }
            }
        }

        return null;
    }

    //get cell from position
    public Cell GetCell(int x, int y)
    {
        return cellList[x][y];
    }

    public Cell GetCell(IntegerVector2 pos)
    {
        return cellList[pos.x][pos.y];
    }
}