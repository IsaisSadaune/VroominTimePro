using UnityEngine;
using System.Collections.Generic;

public class Player
{

    public Player(VisualTile activeTile, int rotation, Vector2Int position)
    {
        ActiveTile = activeTile;
        Rotation = rotation;
        this.Position = position;
    }

    public VisualTile ActiveTile { get; private set; }
    public int Rotation { get; set; }
    public Vector2Int Position { get; set; }


    //visuel, à bouger
    public GameObject cursor;
    public List<GameObject> gameObjectTiles = new();


    public int GetGlobalPositionX(int index)
    {
        return Position.x + ActiveTile.position[index].x;
    }
    public int GetGlobalPositionY(int index)
    {
        return Position.y + ActiveTile.position[index].y;
    }
}
