using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance = null;
    public static MapManager Instance => instance;


    public Tiles[,] map = new Tiles[10, 10];
    [SerializeField] private Vector2 position = new(0, 0);
    [SerializeField] private MapVisuals visuals;


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
                map[i, j] = Tiles.bloc;
            }
        }

        map[0, 0] = Tiles.depart;
        map[9, 9] = Tiles.arrivee;
        visuals.ApplyVisual();
    }

    private void Deplacement(string direction)
    {
        switch (direction)
        {
            case "left":
                if (position.x - 1 >= 0) position.x--;
                break;
            case "right":
                if (position.x + 1 <= 9) position.x++;
                break;
            case "up":
                if (position.y + 1 <= 9) position.y++;
                break;
            case "down":
                if (position.y - 1 >= 0) position.y--;
                break;
            default:
                Debug.Log("apprend à coder");
                break;
        }
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
        if (map[(int)position.x, (int)position.y] == Tiles.depart ||
            map[(int)position.x, (int)position.y] == Tiles.depart) Debug.Log("pas possible");
        else
        {
            map[(int)position.x, (int)position.y] = Tiles.placedBloc;
            visuals.ApplyVisual();
        }
    }

    private void ApplyVisual()
    {
        Debug.Log("on applique la modification visuelle");
    }


    public enum Tiles
    {
        depart,
        arrivee,
        bloc,
        placedBloc,
    }
}
