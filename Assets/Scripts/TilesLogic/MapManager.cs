using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance = null;
    public static MapManager Instance => instance;


    public int[,] Map { get; private set; } = new int[10, 10];
    public Vector2Int position = new(0, 0);
    [SerializeField] private MapVisuals visuals;
    [field:SerializeField] public BlocList BlocList { get; private set; }

    [field:SerializeField] public int ActiveTile1x1 { get; private set; }


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

        CreateMap();
        ChangeActiveTileToBlue();
    }

    private void CreateMap()
    {

        //création de la map
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Map[i, j] = 4;
            }
        }

        Map[0, 0] = 1;
        Map[9, 9] = 0;
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

    public void Left()
    {
        Deplacement("left");
    }
    public void Right()
    {
        Deplacement("right");
    }
    public void Up()
    {
        Deplacement("up");
    }
    public void Down()
    {
        Deplacement("down");
    }

    [ContextMenu("placeTile")]
    public void PlaceTile()
    {
        if (Map[position.x, position.y] == 0 ||
            Map[position.x, position.y] == 1) Debug.Log("pas possible");
        else
        {
            Map[position.x, position.y] = ActiveTile1x1;
            visuals.ApplyPlacement();
        }
    }

    public void ChangeActiveTile(int id)
    {
        ActiveTile1x1 = id;
        visuals.ApplyChangeTile();
    }

    [ContextMenu("Setup Pink Tile")]
    public void ChangeActiveTileToPink()
    {
        ChangeActiveTile(2);
    }

    [ContextMenu("Setup Blue Tile")]
    public void ChangeActiveTileToBlue()
    {
        ChangeActiveTile(3);
    }
    [ContextMenu("Rotate Right")]
    public void RotateRight()
    {
        visuals.ApplyRotationRight();
    }
    [ContextMenu("Rotate Left")]
    public void RotateLeft()
    {
        visuals.ApplyRotationLeft();
    }
}
