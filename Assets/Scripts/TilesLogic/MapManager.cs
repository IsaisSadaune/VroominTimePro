using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance = null;
    public static MapManager Instance => instance;


    public Tiles[,] Map { get; private set; } = new Tiles[10, 10];
    public Vector2Int position = new(0, 0);
    [SerializeField] private MapVisuals visuals;

    private Tile actualTile;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

        //création de la map
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Map[i, j] = Tiles.startingBloc;
            }
        }

        Map[0, 0] = Tiles.depart;
        Map[9, 9] = Tiles.arrivee;
        visuals.SetVisual();
    }

    private void Deplacement(string direction)
    {
        switch (direction)
        {
            case "left":
                if (position.x - 1 >= 0) position.x--;
                else Debug.Log("feedback déplacement impossible");
                break;
            case "right":
                if (position.x + 1 <= 9) position.x++;
                else Debug.Log("feedback déplacement impossible");
                break;
            case "up":
                if (position.y + 1 <= 9) position.y++;
                else Debug.Log("feedback déplacement impossible");
                break;
            case "down":
                if (position.y - 1 >= 0) position.y--;
                else Debug.Log("feedback déplacement impossible");
                break;
            default:
                Debug.Log("apprend à coder");
                break;
        }
        visuals.ApplyMovement();
    }

    [ContextMenu("left")]
    public void Left()
    {
        Deplacement("left");
    }
    [ContextMenu("right")]
    public void Right()
    {
        Deplacement("right");
    }
    [ContextMenu("up")]
    public void Up()
    {
        Deplacement("up");
    }
    [ContextMenu("down")]
    public void Down()
    {
        Deplacement("down");
    }

    [ContextMenu("placeTile")]
    public void PlaceTile()
    {
        if (Map[position.x, position.y] == Tiles.depart ||
            Map[position.x, position.y] == Tiles.arrivee) Debug.Log("pas possible");
        else
        {
            Map[position.x, position.y] = Tiles.placedBloc;
            visuals.ApplyPlacement();
        }
    }

    public void SetActiveTile(Tile t)
    {
        actualTile = t;
    }


}

public enum Tiles
{
    depart,
    arrivee,
    startingBloc,
    placedBloc,
}