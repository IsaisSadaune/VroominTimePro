using UnityEngine;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    //Singleton
    public static MapManager instance = null;
    public static MapManager Instance => instance;

    public VisualBloc[,] Map { get; private set; } = new VisualBloc[10, 10];


    public List<Vector2Int> position = new();
    public List<int> rotation = new();
    [SerializeField] private MapVisuals visuals;
    [field: SerializeField] public List<VisualTile> ActiveTile { get; private set; }


    //tmp
    public const int NBR_PLAYERS = 4;

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

        //remplacer 4 par le nombre de joueurs
        for(int i=0;i<NBR_PLAYERS;i++)
        {
            rotation.Add(0);
            position.Add(new Vector2Int(0, 0));
        }

        //tmp, placée ici pour debug
        CreateMap();
        
    }

    public void CreateMap()
    {

        //création de la map
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

    private void Deplacement(string direction, int player)
    {
        int x = position[player].x;
        int y = position[player].y;
        switch (direction)
        {
            case "left":
                if (position[player].x - 1 >= 0) position[player] = new Vector2Int(x-1, y);
                else Debug.Log("feedback déplacement impossible");
                break;
            case "right":
                if (position[player].x + 1 <= 9) position[player] = new Vector2Int(x+1, y);
                else Debug.Log("feedback déplacement impossible");
                break;
            case "up":
                if (position[player].y + 1 <= 9) position[player] = new Vector2Int(x, y+1);
                else Debug.Log("feedback déplacement impossible");
                break;
            case "down":
                if (position[player].y - 1 >= 0) position[player] = new Vector2Int(x, y-1);
                else Debug.Log("feedback déplacement impossible");
                break;
            default:
                Debug.Log("apprend à coder");
                break;
        }
        visuals.ApplyMovement(player);
    }
    [ContextMenu("placeTile")]
    public void PlaceTile(int player)
    {

        for (int i = 0; i < ActiveTile[player].blocs.Count; i++)
        {
            // /!\ PRENDRE EN COMPTE LA ROTATION DANS CE IF ET LE TRANSFORMER EN FONCTION/!\
            if (ActiveTile[player].position[i] + position[player] == new Vector2Int(0, 0)
                || ActiveTile[player].position[i] + position[player] == new Vector2Int(9, 9))
            {
                Debug.Log("erreur en position "+ ActiveTile[player].position[i] + position);
            }
            else
            {
                int _xCoordonate =ActiveTile[player].position[i].x;
                int _yCoordonate =ActiveTile[player].position[i].y;
                int _pivot;
                for (int x = 0; x < rotation[player]; x++)
                {
                    _pivot = _xCoordonate;
                    _xCoordonate = _yCoordonate;
                    _yCoordonate = -_pivot;
                }
                _xCoordonate += position[player].x;
                _yCoordonate += position[player].y;

                if (_xCoordonate < Map.GetLength(0) && _yCoordonate < Map.GetLength(1) && _xCoordonate >= 0 && _yCoordonate >= 0)
                {
                    Map[ActiveTile[player].position[i].x, ActiveTile[player].position[i].y] = ActiveTile[player].blocs[i];
                    visuals.ApplyPlacement(
                        _xCoordonate,
                        _yCoordonate,
                        ActiveTile[player].blocs[i].bloc,
                        rotation[player],
                        ActiveTile[player].rotation[i]);
                }
            }
        }
    }

    public void ChangeActiveTile(VisualTile id, int player)
    {
        ActiveTile[player] = id;
        visuals.ApplyChangeTile(player);
    }
    public void RotateRight(int player)
    {
        rotation[player]++;
        if (rotation[player] > 3) rotation[player] = 0;
        visuals.ApplyRotationRight(player);
    }
    public void RotateLeft(int player)
    {

        rotation[player]--;
        if (rotation[player] < 0) rotation[player] = 3;
        visuals.ApplyRotationLeft(player);
    }
    public void Up(int player)
    {
        Deplacement("up", player);
    }
    public void Down(int player)
    {
        Deplacement("down", player);
    }
    public void Left(int player)
    {
        Deplacement("left", player);
    }
    public void Right(int player)
    {
        Deplacement("right", player);
    }

}
