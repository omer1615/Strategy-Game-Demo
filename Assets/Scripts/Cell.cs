using UnityEngine;

public class Cell
{
    public SpriteRenderer Renderer;
    public CellInfo info;
    public IObject type;


    public Cell()
    {
        Renderer = null;
        info = null;
    }
}