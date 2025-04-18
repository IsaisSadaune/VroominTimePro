using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals
{
    public PlayerVisuals(int id, GameObject cursor)
    {
        this.id = id;
        this.cursor = cursor;
    }

    public GameObject cursor;
    public List<GameObject> gameObjectTiles = new();
    public int id;

}
