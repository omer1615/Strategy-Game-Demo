using UnityEngine;

public class Barrack : IObject
{
    IntegerVector2 position = new IntegerVector2(0, 0);
    static Color color = Color.green;
    const int cellSizeX = 4, cellSizeY = 4;
    private string name = "Barrack";

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

    public Barrack(IntegerVector2 pos)
    {
        position = pos;
    }

    public Barrack() { }

    public void Produce()
    {
        // spawn soldier
        MapManager.Instance.DrawObject(ObjectFactory.GetObject(ObjectTypes.Soldier));
    }
}