using System.Collections.Generic;
using UnityEngine;
using static MapManager;

public class MapVisuals : MonoBehaviour
{

    [SerializeField] private Transform parentTuiles;

    [SerializeField] private GameObject cursor;


    private List<GameObject> selectedTile = new();

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
        for (int i = 0; i < Instance.rotation; i++)
        {
            ApplyRotationRight(player);
        }
    }
    public void ApplyChangeTile(int player)
    {
        if (selectedTile != null)
            for (int i = 0; i < selectedTile.Count; i++)
            {
                Destroy(selectedTile[i]);
            }
        selectedTile = null;
        SetActiveTile(player);
    }


    private void SetCursor(int player)
    {
        cursor = Instantiate(cursor, new Vector3(Instance.position[player].x, 2, Instance.position[player].y), Quaternion.identity);

    }
    public void MoveCursor(int player)
    {
        cursor.transform.position = new Vector3(Instance.position[player].x, 2, Instance.position[player].y);
    }
    public void SetActiveTile(int player)
    {
        for (int i = 0; i < Instance.ActiveTile[player].blocs.Count; i++)
        {
            selectedTile.Add(Instantiate(Instance.ActiveTile[player].blocs[i].bloc,
                new Vector3(Instance.position[player].x + Instance.ActiveTile[player].position[i].x, 0.1f, Instance.position[player].y + Instance.ActiveTile[player].position[i].y),
                Quaternion.Euler(0, Instance.ActiveTile[player].rotation[i],0),
                parentTuiles));
        }
    }
    public void MoveActiveTile(int player)
    {
        for (int i = 0; i < selectedTile.Count; i++)
        {
            selectedTile[i].transform.position = new Vector3(
                Instance.position[player].x + Instance.ActiveTile[player].position[i].x,
                0.1f,
                Instance.position[player].y + Instance.ActiveTile[player].position[i].y);
        }
    }

    public void ApplyRotationRight(int player)
    {

        for (int i = 0; i < selectedTile.Count; i++)
        {
            selectedTile[i].transform.rotation = Quaternion.identity;
            selectedTile[i].transform.position = new Vector3(
                (selectedTile[i].transform.position.z - Instance.position[player].y) + Instance.position[player].x,
                0.1f,
                -(selectedTile[i].transform.position.x - Instance.position[player].x) + Instance.position[player].y);
            selectedTile[i].transform.Rotate(0, 90 * Instance.rotation + Instance.ActiveTile[player].rotation[i], 0);
        }

    }
    public void ApplyRotationLeft(int player)
    {
        for (int i = 0; i < selectedTile.Count; i++)
        {
            selectedTile[i].transform.rotation = Quaternion.identity;
            selectedTile[i].transform.position = new Vector3(
                -(selectedTile[i].transform.position.z - Instance.position[player].y) + Instance.position[player].x,
                0.1f,
                (selectedTile[i].transform.position.x - Instance.position[player].x) + Instance.position[player].y);
            selectedTile[i].transform.Rotate(0, 90 * Instance.rotation + Instance.ActiveTile[player].rotation[i], 0);
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

