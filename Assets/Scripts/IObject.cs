using UnityEngine;

//IObject interface for barrack, powerplant, soldier etc.
public interface IObject
{
    IntegerVector2 Position { get; set; }
    Color Color { get; }
    string Name { get; }
    int CellSizeX { get; }
    int CellSizeY { get; }


    void Produce();
}