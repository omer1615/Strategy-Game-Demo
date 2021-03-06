﻿using UnityEngine;

public class PowerPlant : IObject
{
    IntegerVector2 position = new IntegerVector2(0, 0);
    static Color color = Color.yellow + Color.black ;
    const int cellSizeX = 2, cellSizeY = 3;
    private string name = "Power Plant";

    public IntegerVector2 Position
    {
        get { return position; }
        set { position = value; }
    }

    public Color Color
    {
        get { return color; }
    }

    public string Name
    {
        get { return name; }
    }

    int IObject.CellSizeX
    {
        get { return cellSizeX; }
    }

    int IObject.CellSizeY
    {
        get { return cellSizeY; }
    }

    public PowerPlant(IntegerVector2 pos)
    {
        position = pos;
    }

    public PowerPlant() { }

    public void Produce()
    {
        // do nothing
    }
}