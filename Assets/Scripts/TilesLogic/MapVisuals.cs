using System.Collections.Generic;
using UnityEngine;
using static MapManager;

public class MapVisuals : MonoBehaviour
{

    [SerializeField] private Transform parentTuiles;

    [SerializeField] private List<GameObject> cursorPrefabs;

    //tmp, à rendre propre apres
    private List<List<GameObject>> selectedTile = new();

    private GameObject[,] visualMap = new GameObject[10, 10];
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// Instantie les visuels nécessaires pour lancer la partie
    /// </summary>
    public void SetVisual()
    {
        for (int i = 0; i < InstanceMapManager.Map.GetLength(0); i++)
        {
            for (int j = 0; j < InstanceMapManager.Map.GetLength(1); j++)
            {
                visualMap[i, j] = Instantiate(InstanceMapManager.Map[i, j].bloc, new Vector3(i, 0, j), Quaternion.identity, parentTuiles);
            }
        }
        for(int i=0; i< NBR_PLAYERS;i++)
        {
            selectedTile.Add(new());
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

    /// <summary>
    /// Applique le mouvement provoqué par un joueur
    /// </summary>
    /// <param name="player">Index du joueur qui se déplace</param>
    public void ApplyMovement(Player player)
    {
        MoveCursor(player);
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

    /// <summary>
    /// Instancie le curseur à la position du joueur
    /// </summary>
    /// <param name="player">Index du joueur</param>
    private void SetCursor(Player player, GameObject prefab)
    {
        player.cursor =Instantiate(prefab, new Vector3(player.Position.x, 2, player.Position.y), Quaternion.identity);
    }

    /// <summary>
    /// Déplace le curseur vers la nouvelle position du joueur
    /// </summary>
    /// <param name="player">Index du joueur</param>
    public void MoveCursor(Player player)
    {
        player.cursor.transform.position = new Vector3(player.Position.x, 2, player.Position.y);
    }

    /// <summary>
    /// Définit la tuile active d'un joueur
    /// </summary>
    /// <param name="player">Index du joueur</param>
    public void SetActiveTile(Player player)
    {
        for (int i = 0; i < player.ActiveTile.blocs.Count; i++)
        {
            GameObject bloc = player.ActiveTile.blocs[i].bloc;

            int positionX = player.Position.x + player.ActiveTile.position[i].x;
            int positionZ = player.Position.y + player.ActiveTile.position[i].y;

            Quaternion rotation = Quaternion.Euler(0, player.ActiveTile.rotation[i], 0);
            
            player.gameObjectTiles.Add(Instantiate(bloc, new Vector3(positionX, 0.1f, positionZ), rotation, parentTuiles));
        }
    }

    /// <summary>
    /// Applique le déplacement d'une tuile
    /// </summary>
    /// <param name="player">Index du joueur</param>
    public void MoveActiveTile(Player player)
    {
        for (int i = 0; i < player.gameObjectTiles.Count; i++)
        {
            player.gameObjectTiles[i].transform.position = new Vector3(
                player.Position.x + player.ActiveTile.position[i].x,
                0.1f,
                player.Position.y + player.ActiveTile.position[i].y);
        }
    }

    /// <summary>
    /// Applique la rotation d'une tuile vers la droite
    /// </summary>
    /// <param name="player"></param>
    public void ApplyRotationRight(Player player)
    {

        for (int i = 0; i < player.gameObjectTiles.Count; i++)
        {
            player.gameObjectTiles[i].transform.rotation = Quaternion.identity;
            player.gameObjectTiles[i].transform.position = new Vector3(
                (player.gameObjectTiles[i].transform.position.z - player.Position.y) + player.Position.x,
                0.1f,
                -(player.gameObjectTiles[i].transform.position.x - player.Position.x) + player.Position.y);
            player.gameObjectTiles[i].transform.Rotate(0, 90 * player.Rotation + player.ActiveTile.rotation[i], 0);
        }

    }

    /// <summary>
    /// Applique la rotation d'une tuile vers la gauche
    /// </summary>
    /// <param name="player"></param>
    public void ApplyRotationLeft(Player player)
    {
        for (int i = 0; i < player.gameObjectTiles.Count; i++)
        {
            player.gameObjectTiles[i].transform.rotation = Quaternion.identity;
            player.gameObjectTiles[i].transform.position = new Vector3(
                -(player.gameObjectTiles[i].transform.position.z - player.Position.y) + player.Position.x,
                0.1f,
                (player.gameObjectTiles[i].transform.position.x - player.Position.x) + player.Position.y);
            player.gameObjectTiles[i].transform.Rotate(0, 90 * player.Rotation + player.ActiveTile.rotation[i], 0);
        }
    }

    public void SetParentTile()
    {
        if(!parentTuiles)
        {
            parentTuiles = Instantiate(new GameObject()).transform;
        }
    }
}

