using System.Collections.Generic;
using UnityEngine;

public class Player
{

    public Player(int id, VisualTile activeTile, int rotation, Vector2Int position)
    {
        ActiveTile = activeTile;
        Rotation = rotation;
        Position = position;
        Id = id;
    }

    public VisualTile ActiveTile { get; private set; }
    public int Rotation { get; set; }
    public Vector2Int Position { get; set; }
    public int Id { get; private set; }

    //visuel, � bouger
    public GameObject cursor;
    public List<GameObject> gameObjectTiles = new();


    public int GetGlobalPositionX(int index) => Position.x + ActiveTile.position[index].x;
    public int GetGlobalPositionY(int index) => Position.y + ActiveTile.position[index].y;

    public void DestroyActiveTile()
    {
        ActiveTile = null;
    }
}
