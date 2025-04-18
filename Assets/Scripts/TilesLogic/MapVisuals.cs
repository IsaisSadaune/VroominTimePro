using System.Collections.Generic;
using UnityEngine;
using static MapManager;

public class MapVisuals : MonoBehaviour
{

    [SerializeField] private Transform parentTuiles;
    [SerializeField] private List<GameObject> cursorPrefabs;

    private GameObject[,] visualMap = new GameObject[10, 10];

    private List<PlayerVisuals> playerVisuals = new();
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SetMapVisual(GameObject[,] blocs, int lengthX, int lengthY)
    {
        for (int i = 0; i < lengthX; i++)
        {
            for (int j = 0; j < lengthY; j++)
            {
                visualMap[i, j] = Instantiate(blocs[i,j], new Vector3(i, 0, j), Quaternion.identity, parentTuiles);
            }
        }
    }

    public void SetVisual()
    {
        for(int i=0; i< NBR_PLAYERS;i++)
        {
            //playerVisuals.Add(new PlayerVisuals(
            //    i,
            //    Instantiate(cursorPrefabs[i], 
            //    new Vector3(InstanceMapManager.players[i].Position.x, 2, InstanceMapManager.players[i].Position.y), 
            //    Quaternion.identity)));

            SetActiveTile(InstanceMapManager.players[i]);
            SetCursor(InstanceMapManager.players[i], cursorPrefabs[i]);
        }
    }

    /// <summary>
    /// Remplace une tuile sur la carte
    /// </summary>
    /// <param name="p_x">position X de la tuile à remplacer</param>
    /// <param name="p_y">position Y de la tuile à remplacer</param>
    /// <param name="p_bloc">le gameObject remplaçant</param>
    /// <param name="rotation">la rotation de la tuile joueur (entre 0 et 3)</param>
    /// <param name="rotationClasse">la rotation du scriptableObject entre 0,90,180 et 270°</param>
    public void ApplyPlacement(int p_x, int p_y, GameObject p_bloc, int rotation, int rotationClasse)
    {
        if (p_x < visualMap.GetLength(0) && p_y < visualMap.GetLength(1))
        {
            Destroy(visualMap[p_x, p_y]);
            visualMap[p_x, p_y] = Instantiate(p_bloc, new Vector3(p_x, 0, p_y), Quaternion.Euler(0,90*rotation + rotationClasse, 0), parentTuiles);
        }
    }

    public void ApplyMovement(Player player)
    {
        MoveCursor(player.cursor, player.Position);
        MoveActiveTile(player);
        for (int i = 0; i < player.Rotation; i++)
        {
            ApplyRotationRight(player);
        }
    }

    /// <summary>
    /// Applique le changement de tuile d'un joueur
    /// </summary>
    /// <param name="player">Index du joueur</param>
    //public void ApplyChangeTile(int player)
    //{
    //    if (selectedTile[player] != null)
    //        for (int i = 0; i < selectedTile[player].Count; i++)
    //        {
    //            Destroy(selectedTile[player][i]);
    //        }
    //    selectedTile[player] = null;
    //    SetActiveTile(player);
    //}

 
    private void SetCursor(Player player, GameObject prefab)
    {
        player.cursor = Instantiate(prefab, new Vector3(player.Position.x, 2, player.Position.y), Quaternion.identity);
    }

    public void MoveCursor(GameObject cursor, Vector2Int position)
    {
        cursor.transform.position = new Vector3(position.x, 2, position.y);
    }

    public void SetActiveTile(Player player)
    {
        for (int i = 0; i < player.ActiveTile.blocs.Count; i++)
        {
            GameObject bloc = player.ActiveTile.blocs[i].bloc;
            Quaternion rotation = Quaternion.Euler(0, player.ActiveTile.rotation[i], 0);
            
            player.gameObjectTiles.Add(Instantiate(bloc, new Vector3(player.GetGlobalPositionX(i), 0.1f, player.GetGlobalPositionY(i)), rotation, parentTuiles));
            //player.gameObjectTiles.Add(SetActiveBloc(bloc, player.Position, rotation, parentTuiles));
        }
    }

    private GameObject SetActiveBloc(GameObject bloc, Vector2Int position, Quaternion rotation, Transform parentTuile)
    {
        return Instantiate(bloc, new Vector3(position.x, 0.1f, position.y), rotation, parentTuiles);
    }

    public void MoveActiveTile(Player player)
    {
        for (int i = 0; i < player.gameObjectTiles.Count; i++)
        {
            MoveActiveBloc(player.gameObjectTiles[i], new Vector2Int(player.GetGlobalPositionX(i), player.GetGlobalPositionY(i)));
        }
    }

    private void MoveActiveBloc(GameObject bloc, Vector2Int position)
    {
        bloc.transform.position = new Vector3(position.x, 0.1f, position.y);
    }


    public void ApplyRotationRight(Player player)
    {

        for (int i = 0; i < player.gameObjectTiles.Count; i++)
        {

            ApplyRotationBlocRight(player.gameObjectTiles[i], player.Position, player.Rotation, player.ActiveTile.rotation[i]);
        }
    }

    private void ApplyRotationBlocRight(GameObject bloc, Vector2Int positionPlayer, int rotation, int rotationBase)
    {
        
        bloc.transform.SetPositionAndRotation(new Vector3(
                (bloc.transform.position.z - positionPlayer.y) + positionPlayer.x,
                0.1f,
                -(bloc.transform.position.x - positionPlayer.x) + positionPlayer.y), 
            Quaternion.identity);

        bloc.transform.Rotate(0, 90 * rotation + rotationBase, 0);
    }



    public void ApplyRotationLeft(Player player)
    {
        for (int i = 0; i < player.gameObjectTiles.Count; i++)
        {
            ApplyRotationBlocLeft(player.gameObjectTiles[i], player.Position, player.Rotation, player.ActiveTile.rotation[i]);
        }
    }
    private void ApplyRotationBlocLeft(GameObject bloc, Vector2Int positionPlayer, int rotation, int rotationBase)
    {
        bloc.transform.rotation = Quaternion.identity;
        bloc.transform.position = new Vector3(
            -(bloc.transform.position.z - positionPlayer.y) + positionPlayer.x,
            0.1f,
            (bloc.transform.position.x - positionPlayer.x) + positionPlayer.y);
        bloc.transform.Rotate(0, 90 * rotation + rotationBase, 0);
    }


    public void SetParentTile()
    {
        if(!parentTuiles)
        {
            parentTuiles = Instantiate(new GameObject()).transform;
        }
    }
}

