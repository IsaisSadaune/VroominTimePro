using UnityEngine;

public class MapManager : MonoBehaviour
{
    //Singleton
    public static MapManager instance = null;
    public static MapManager Instance => instance;

    public VisualBloc[,] Map { get; private set; } = new VisualBloc[10, 10];
    public Vector2Int position = new(0, 0);
    [SerializeField] private MapVisuals visuals;
    [field:SerializeField] public VisualTile ActiveTile { get; private set; }


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
    }

    private void CreateMap()
    {

        //cr�ation de la map
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Map[i, j] = BlocList.blocList.GetBloc(4);
            }
        }

        Map[0, 0] = BlocList.blocList.GetBloc(1);
        Map[9, 9] = BlocList.blocList.GetBloc(0);


        //ActiveTile = TileList.tileList.Tiles[1];

        visuals.SetVisual();
    }

    private void Deplacement(string direction)
    {
        switch (direction)
        {
            case "left":
                if (position.x - 1 >= 0) position.x--;
                else Debug.Log("feedback d�placement impossible");
                break;
            case "right":
                if (position.x + 1 <= 9) position.x++;
                else Debug.Log("feedback d�placement impossible");
                break;
            case "up":
                if (position.y + 1 <= 9) position.y++;
                else Debug.Log("feedback d�placement impossible");
                break;
            case "down":
                if (position.y - 1 >= 0) position.y--;
                else Debug.Log("feedback d�placement impossible");
                break;
            default:
                Debug.Log("apprend � coder");
                break;
        }
        visuals.ApplyMovement();
    }
    [ContextMenu("placeTile")]
    public void PlaceTile()
    {
        if (Map[position.x, position.y].id == 0 ||
            Map[position.x, position.y].id == 1 ) Debug.Log("pas possible");
        else
        {
            for(int i=0;i<ActiveTile.blocs.Count;i++)
            {
                Map[ActiveTile.position[i].x, ActiveTile.position[i].y] = ActiveTile.blocs[i];
                visuals.ApplyPlacement(
                    position.x + ActiveTile.position[i].x, 
                    position.y + ActiveTile.position[i].y, 
                    ActiveTile.blocs[i].bloc);
            }
        }
    }

    public void ChangeActiveTile(VisualTile id)
    {
        ActiveTile = id;
        visuals.ApplyChangeTile();
    }
    [ContextMenu("Setup Pink Tile")]
    public void ChangeActiveTileToPink()
    {
        ChangeActiveTile(TileList.tileList.Tiles[0]);
    }
    [ContextMenu("Setup 1x2 Tile")]
    public void ChangeActiveTileTo1x2()
    {
        ChangeActiveTile(TileList.tileList.Tiles[1]);
    }
    public void RotateRight()
    {
        visuals.ApplyRotationRight();
    }
    public void RotateLeft()
    {
        visuals.ApplyRotationLeft();
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
}
