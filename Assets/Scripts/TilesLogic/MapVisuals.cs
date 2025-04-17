using System.Collections.Generic;
using UnityEngine;
using static MapManager;

public class MapVisuals : MonoBehaviour
{

    [SerializeField] private Transform parentTuiles;

    [SerializeField] private List<GameObject> cursorPrefabs;
    private List<GameObject> cursor = new();

    //tmp, à rendre propre apres
    private List<List<GameObject>> selectedTile = new();

    private GameObject[,] visualMap = new GameObject[10, 10];
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SetVisual()
    {
        for (int i = 0; i < Instance.Map.GetLength(0); i++)
        {
            for (int j = 0; j < Instance.Map.GetLength(1); j++)
            {
                visualMap[i, j] = Instantiate(Instance.Map[i, j].bloc, new Vector3(i, 0, j), Quaternion.identity, parentTuiles);
            }
        }
        for(int i=0; i< NBR_PLAYERS;i++)
        {
            selectedTile.Add(new());
            SetActiveTile(i);
            SetCursor(i);
        }
    }
    public void ApplyPlacement(int p_x, int p_y, GameObject p_bloc, int rotation, int rotationClasse)
    {
        if (p_x < visualMap.GetLength(0) && p_y < visualMap.GetLength(1))
        {
            Destroy(visualMap[p_x, p_y]);
            visualMap[p_x, p_y] = Instantiate(p_bloc, new Vector3(p_x, 0, p_y), Quaternion.Euler(0,90*rotation + rotationClasse, 0), parentTuiles);
        }
    }
    public void ApplyMovement(int player)
    {
        MoveCursor(player);
        MoveActiveTile(player);
        for (int i = 0; i < Instance.rotation[player]; i++)
        {
            ApplyRotationRight(player);
        }
    }
    public void ApplyChangeTile(int player)
    {
        if (selectedTile[player] != null)
            for (int i = 0; i < selectedTile[player].Count; i++)
            {
                Destroy(selectedTile[player][i]);
            }
        selectedTile[player] = null;
        SetActiveTile(player);
    }


    private void SetCursor(int player)
    {
        cursor.Add(Instantiate(cursorPrefabs[player], new Vector3(Instance.position[player].x, 2, Instance.position[player].y), Quaternion.identity));
    }

    public void MoveCursor(int player)
    {
        cursor[player].transform.position = new Vector3(Instance.position[player].x, 2, Instance.position[player].y);
    }
    public void SetActiveTile(int player)
    {
        for (int i = 0; i < Instance.ActiveTile[player].blocs.Count; i++)
        {
            GameObject bloc = Instance.ActiveTile[player].blocs[i].bloc;

            int positionX = Instance.position[player].x + Instance.ActiveTile[player].position[i].x;
            int positionZ = Instance.position[player].y + Instance.ActiveTile[player].position[i].y;

            Quaternion rotation = Quaternion.Euler(0, Instance.ActiveTile[player].rotation[i], 0);
            
            selectedTile[player].Add(Instantiate(bloc, new Vector3(positionX, 0.1f, positionZ), rotation, parentTuiles));
        }
    }
    public void MoveActiveTile(int player)
    {
        for (int i = 0; i < selectedTile.Count; i++)
        {
            selectedTile[player][i].transform.position = new Vector3(
                Instance.position[player].x + Instance.ActiveTile[player].position[i].x,
                0.1f,
                Instance.position[player].y + Instance.ActiveTile[player].position[i].y);
        }
    }

    public void ApplyRotationRight(int player)
    {

        for (int i = 0; i < selectedTile.Count; i++)
        {
            selectedTile[player][i].transform.rotation = Quaternion.identity;
            selectedTile[player][i].transform.position = new Vector3(
                (selectedTile[player][i].transform.position.z - Instance.position[player].y) + Instance.position[player].x,
                0.1f,
                -(selectedTile[player][i].transform.position.x - Instance.position[player].x) + Instance.position[player].y);
            selectedTile[player][i].transform.Rotate(0, 90 * Instance.rotation[player] + Instance.ActiveTile[player].rotation[i], 0);
        }

    }
    public void ApplyRotationLeft(int player)
    {
        for (int i = 0; i < selectedTile.Count; i++)
        {
            selectedTile[player][i].transform.rotation = Quaternion.identity;
            selectedTile[player][i].transform.position = new Vector3(
                -(selectedTile[player][i].transform.position.z - Instance.position[player].y) + Instance.position[player].x,
                0.1f,
                (selectedTile[player][i].transform.position.x - Instance.position[player].x) + Instance.position[player].y);
            selectedTile[player][i].transform.Rotate(0, 90 * Instance.rotation[player] + Instance.ActiveTile[player].rotation[i], 0);
        }
    }

    public void GetParentTile()
    {
        if(!parentTuiles)
        {
            parentTuiles = Instantiate(new GameObject()).transform;
        }
    }
}

