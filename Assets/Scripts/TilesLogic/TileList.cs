using UnityEngine;
using System.Collections.Generic;

public class TileList : MonoBehaviour
{
    public static TileList tileList = null;
    public static TileList AllTilesList => tileList;

    private void Awake()
    {
        if (tileList != null && tileList != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            tileList = this;
        }
    }
    [field: SerializeField] public List<VisualTile> Tiles { get; private set; }
}
