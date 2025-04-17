using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    //Singleton
    public static MapManager mapManager = null;
    public static MapManager InstanceMapManager => mapManager;

    [SerializeField] private MapVisuals visuals;

    public List<Player> players = new();


    //ne doit pas être défini comme ça dans la version finale. Doit être lié avec l'écran de choix de map
    public VisualBloc[,] Map { get; private set; } = new VisualBloc[10, 10];


    //Je force la tuile active à exister ici. Doit être modifié plus tard
    [field: SerializeField] public List<VisualTile> TMPActiveTile { get; private set; }


    //tmp, à lier avec le GameManager
    public const int NBR_PLAYERS = 4;

    private void Awake()
    {
        if (mapManager != null && mapManager != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            mapManager = this;
        }
        DontDestroyOnLoad(this.gameObject);


        for (int i = 0; i < NBR_PLAYERS; i++)
        {
            players.Add(new Player(TMPActiveTile[i], 0, new Vector2Int(0, 0)));
        }

        //tmp, placée ici pour debug
        CreateMap();

    }
    /// <summary>
    /// Crée la map ainsi que le debut et la fin de la course.
    /// </summary>
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

        visuals.SetVisual();
    }

    /// <summary>
    /// Déplace le curseur du player dans la direction voulue
    /// </summary>
    /// <param name="direction">une direction parmi "left", "right", "up", "down"</param>
    /// <param name="player">L'index du joueur voulu (entre 0 et 3)</param>
    private void Deplacement(string direction, Player player)
    {
        int x = player.Position.x;
        int y = player.Position.y;
        switch (direction)
        {
            case "left":
                if (x - 1 >= 0) player.Position = new Vector2Int(x - 1, y);
                else Debug.Log("feedback déplacement impossible");
                break;
            case "right":
                if (x + 1 <= 9) player.Position = new Vector2Int(x + 1, y);
                else Debug.Log("feedback déplacement impossible");
                break;
            case "up":
                if (y + 1 <= 9) player.Position = new Vector2Int(x, y + 1);
                else Debug.Log("feedback déplacement impossible");
                break;
            case "down":
                if (y - 1 >= 0) player.Position = new Vector2Int(x, y - 1);
                else Debug.Log("feedback déplacement impossible");
                break;
            default:
                Debug.Log("apprend à coder");
                break;
        }
        visuals.ApplyMovement(player);
    }

    /// <summary>
    /// Place la tuile du joueur passé en parametre à l'emplacement position[player]
    /// </summary>
    /// <param name="player">L'index du joueur entre 0 et 3</param>
    [ContextMenu("placeTile")]
    private void PlaceTile(Player player)
    {
        int _pivot;
        int _yCoordonate;
        int _xCoordonate;
        for (int i = 0; i < player.ActiveTile.blocs.Count; i++)
        {
            _xCoordonate = player.ActiveTile.position[i].x;
            _yCoordonate = player.ActiveTile.position[i].y;
            for (int x = 0; x < player.Rotation; x++)
            {
                _pivot = _xCoordonate;
                _xCoordonate = _yCoordonate;
                _yCoordonate = -_pivot;
            }
            _xCoordonate += player.Position.x;
            _yCoordonate += player.Position.y;

            // /!\ PRENDRE EN COMPTE LA ROTATION DANS CE IF, CA CREE UN BUG ET PERMET DE REMPLACER ALORS QUE CA DEVRAIT PAS/!\
            //placer la ligne de départ et d'arrivée dans le code
            if (new Vector2Int(_xCoordonate, _yCoordonate) == new Vector2Int(0, 0)
                || new Vector2Int(_xCoordonate, _yCoordonate) == new Vector2Int(9, 9))
            {
                Debug.Log("erreur en position " + player.ActiveTile.position[i] + player.Position);
            }
            else if (_xCoordonate < Map.GetLength(0) && _yCoordonate < Map.GetLength(1) && _xCoordonate >= 0 && _yCoordonate >= 0)
            {
                Map[player.ActiveTile.position[i].x, player.ActiveTile.position[i].y] = player.ActiveTile.blocs[i];
                visuals.ApplyPlacement(
                    _xCoordonate,
                    _yCoordonate,
                    player.ActiveTile.blocs[i].bloc,
                    player.Rotation,
                    player.ActiveTile.rotation[i]);
            }
        }
    }

    //public void ChangeActiveTile(VisualTile id, int player)
    //{
    //    ActiveTile[player] = id;
    //    visuals.ApplyChangeTile(player);
    //}
    public void RotateRight(int indexPlayer)
    {
        players[indexPlayer].Rotation++;
        if (players[indexPlayer].Rotation > 3) players[indexPlayer].Rotation = 0;
        visuals.ApplyRotationRight(players[indexPlayer]);
    }
    public void RotateLeft(int indexPlayer)
    {
        players[indexPlayer].Rotation--;
        if (players[indexPlayer].Rotation < 0) players[indexPlayer].Rotation = 3;
        visuals.ApplyRotationLeft(players[indexPlayer]);
    }

    public void PlaceTileInt(int indexPlayer)
    {
        PlaceTile(players[indexPlayer]);
    }

    public void Up(int indexPlayer)
    {
        Deplacement("up", players[indexPlayer]);
    }
    public void Down(int indexPlayer)
    {
        Deplacement("down", players[indexPlayer]);
    }
    public void Left(int indexPlayer)
    {
        Deplacement("left", players[indexPlayer]);
    }
    public void Right(int indexPlayer)
    {
        Deplacement("right", players[indexPlayer]);
    }
}
