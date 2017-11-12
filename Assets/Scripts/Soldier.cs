using UnityEngine;

public class Soldier : IObject
{
    IntegerVector2 position = new IntegerVector2(0, 0);
    static Color color = Color.gray;
    const int cellSizeX = 1, cellSizeY = 1;
    private string name = "Soldier";

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

    public void Produce()
    {
        // do nothing
    }

}