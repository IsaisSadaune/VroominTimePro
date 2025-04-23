using System.Collections.Generic;
using UnityEngine;
using static MapManager;

public class MapVisuals : MonoBehaviour
{

    [SerializeField] private Transform parentTuiles;
    [SerializeField] private List<GameObject> cursorPrefabs;

    private GameObject[,] visualMap = new GameObject[10, 10];

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// Crée la map de gameObjects
    /// </summary>
    /// <param name="blocs">les gameObjects de la map</param>
    /// <param name="lengthX">la taille X de la map</param>
    /// <param name="lengthY">la taille Y de la map</param>
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

    /// <summary>
    /// Crée une tuile active et un curseur pour chaque joueurs
    /// </summary>
    public void SetVisual()
    {
        for(int i=0; i< MultiplayerManager.Instance.players.Count;i++)
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
    public void ApplyPlacement(GameObject cursor, int p_x, int p_y, GameObject p_bloc, int rotation, int rotationClasse)
    {
        if (p_x < visualMap.GetLength(0) && p_y < visualMap.GetLength(1))
        {
            Destroy(visualMap[p_x, p_y]);
            visualMap[p_x, p_y] = Instantiate(p_bloc, new Vector3(p_x, 0, p_y), Quaternion.Euler(0,90*rotation + rotationClasse, 0), parentTuiles);
        }
        DesactivateCursor(cursor);
    }

    /// <summary>
    /// Déplace le curseur et la tuile d'un joueur
    /// </summary>
    /// <param name="player">le joueur qui doit être déplacé</param>
    public void ApplyMovement(PlayerTile player)
    {
        MoveCursor(player.cursor, player.Position);
        MoveActiveTile(player);
        for (int i = 0; i < player.Rotation; i++)
        {
            ApplyRotationRight(player);
        }
    }


    /// <summary>
    /// Crée un curseur
    /// </summary>
    /// <param name="player">le joueur qui doit obtenir un curseur</param>
    /// <param name="prefab">le gameObject du curseur</param>
    private void SetCursor(PlayerTile player, GameObject prefab)
    {
        player.cursor = Instantiate(prefab, new Vector3(player.Position.x, 2, player.Position.y), Quaternion.identity);
    }

    /// <summary>
    /// déplace le gameobject du curseur
    /// </summary>
    /// <param name="cursor">le curseur à déplacer</param>
    /// <param name="position">la position ou est le curseur</param>
    public void MoveCursor(GameObject cursor, Vector2Int position)
    {
        cursor.transform.position = new Vector3(position.x, 2, position.y);
    }


    /// <summary>
    /// crée la tuile active d'un joueur
    /// </summary>
    /// <param name="player"></param>
    public void SetActiveTile(PlayerTile player)
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

    public void MoveActiveTile(PlayerTile player)
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


    /// <summary>
    /// fait tourner tous les blocs d'un joueur de 90 degrés à droite
    /// </summary>
    /// <param name="player">le joueur dont la tuile doit tourner</param>
    public void ApplyRotationRight(PlayerTile player)
    {

        for (int i = 0; i < player.gameObjectTiles.Count; i++)
        {

            ApplyRotationBlocRight(player.gameObjectTiles[i], player.Position, player.Rotation, player.ActiveTile.rotation[i]);
        }
    }

    /// <summary>
    /// déplace un bloc à sa droite
    /// </summary>
    /// <param name="bloc">gameobject à tourner</param>
    /// <param name="positionPlayer">position du point de pivot</param>
    /// <param name="rotation">rotation actuelle du joueur : entre 0 et 3</param>
    /// <param name="rotationBase">rotation de base du bloc : 0, 90, 180, 270</param>
    private void ApplyRotationBlocRight(GameObject bloc, Vector2Int positionPlayer, int rotation, int rotationBase)
    {
        
        bloc.transform.SetPositionAndRotation(new Vector3(
                (bloc.transform.position.z - positionPlayer.y) + positionPlayer.x,
                0.1f,
                -(bloc.transform.position.x - positionPlayer.x) + positionPlayer.y), 
            Quaternion.identity);

        bloc.transform.Rotate(0, 90 * rotation + rotationBase, 0);
    }


    /// <summary>
    /// fait tourner tous les blocs d'un joueur de 90 degrés à gauche
    /// </summary>
    /// <param name="player">le joueur dont la tuile doit tourner</param>
    public void ApplyRotationLeft(PlayerTile player)
    {
        for (int i = 0; i < player.gameObjectTiles.Count; i++)
        {
            ApplyRotationBlocLeft(player.gameObjectTiles[i], player.Position, player.Rotation, player.ActiveTile.rotation[i]);
        }
    }

    /// <summary>
    /// déplace un bloc à sa gauche
    /// </summary>
    /// <param name="bloc">gameobject à tourner</param>
    /// <param name="positionPlayer">position du point de pivot</param>
    /// <param name="rotation">rotation actuelle du joueur : entre 0 et 3</param>
    /// <param name="rotationBase">rotation de base du bloc : 0, 90, 180, 270</param>
    private void ApplyRotationBlocLeft(GameObject bloc, Vector2Int positionPlayer, int rotation, int rotationBase)
    {
        bloc.transform.rotation = Quaternion.identity;
        bloc.transform.position = new Vector3(
            -(bloc.transform.position.z - positionPlayer.y) + positionPlayer.x,
            0.1f,
            (bloc.transform.position.x - positionPlayer.x) + positionPlayer.y);
        bloc.transform.Rotate(0, 90 * rotation + rotationBase, 0);
    }

    /// <summary>
    /// Crée une tuile parent si elle n'existe pas
    /// </summary>
    public void SetParentTile()
    {
        if(!parentTuiles)
        {
            parentTuiles = Instantiate(new GameObject()).transform;
        }
    }

    public void DesactivateCursor(GameObject cursor)
    {
        cursor.SetActive(false);
    }
    public void ActivateCursor(GameObject cursor)
    {
        cursor.SetActive(true);
    }

    public void DestroyActiveTile(List<GameObject> tile)
    {
        foreach(GameObject b in tile)
        {
            Destroy(b);
        }
    }

}

