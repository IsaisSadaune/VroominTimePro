using System.Collections.Generic;
using UnityEngine;
using static MapManager;

public class MapVisuals : MonoBehaviour
{

    [SerializeField] private Transform parentTuiles;

    [SerializeField] private GameObject cursor;


    private List<GameObject> selectedTile = new();

    private GameObject[,] visualMap = new GameObject[10, 10];

    public void SetVisual()
    {
        for (int i = 0; i < Instance.Map.GetLength(0); i++)
        {
            for (int j = 0; j < Instance.Map.GetLength(1); j++)
            {
                visualMap[i, j] = Instantiate(Instance.Map[i, j].bloc, new Vector3(i, 0, j), Quaternion.identity, parentTuiles);
            }
        }
        SetActiveTile();
        SetCursor();
    }
    public void ApplyPlacement(int p_x, int p_y, GameObject p_bloc, int rotation)
    {
        if (p_x < visualMap.GetLength(0) && p_y < visualMap.GetLength(1))
        {
            Destroy(visualMap[p_x, p_y]);
            visualMap[p_x, p_y] = Instantiate(p_bloc, new Vector3(p_x, 0, p_y), Quaternion.Euler(0,90*rotation,0), parentTuiles);
        }
    }
    public void ApplyMovement()
    {
        MoveCursor();
        MoveActiveTile();
        for (int i = 0; i < Instance.rotation; i++)
        {
            ApplyRotationRight();
        }
    }
    public void ApplyChangeTile()
    {
        if (selectedTile != null)
            for (int i = 0; i < selectedTile.Count; i++)
            {
                Destroy(selectedTile[i]);
            }
        selectedTile = null;
        SetActiveTile();
    }


    private void SetCursor()
    {
        cursor = Instantiate(cursor, new Vector3(Instance.position.x, 2, Instance.position.y), Quaternion.identity);

    }
    public void MoveCursor()
    {
        cursor.transform.position = new Vector3(Instance.position.x, 2, Instance.position.y);
    }
    public void SetActiveTile()
    {
        for (int i = 0; i < Instance.ActiveTile.blocs.Count; i++)
        {
            selectedTile.Add(Instantiate(Instance.ActiveTile.blocs[i].bloc,
                new Vector3(Instance.position.x + Instance.ActiveTile.position[i].x, 0.1f, Instance.position.y + Instance.ActiveTile.position[i].y),
                Quaternion.identity,
                parentTuiles));
        }
    }
    public void MoveActiveTile()
    {
        for (int i = 0; i < selectedTile.Count; i++)
        {
            selectedTile[i].transform.position = new Vector3(
                Instance.position.x + Instance.ActiveTile.position[i].x,
                0.1f,
                Instance.position.y + Instance.ActiveTile.position[i].y);
        }
    }

    public void ApplyRotationRight()
    {

        for (int i = 0; i < selectedTile.Count; i++)
        {
            selectedTile[i].transform.rotation = Quaternion.identity;
            selectedTile[i].transform.position = new Vector3(
                (selectedTile[i].transform.position.z - Instance.position.y) + Instance.position.x,
                0.1f,
                -(selectedTile[i].transform.position.x - Instance.position.x) + Instance.position.y);
            selectedTile[i].transform.Rotate(0, 90 * Instance.rotation, 0);
        }

    }
    public void ApplyRotationLeft()
    {
        for (int i = 0; i < selectedTile.Count; i++)
        {
            selectedTile[i].transform.rotation = Quaternion.identity;
            selectedTile[i].transform.position = new Vector3(
                -(selectedTile[i].transform.position.z - Instance.position.y) + Instance.position.x,
                0.1f,
                (selectedTile[i].transform.position.x - Instance.position.x) + Instance.position.y);
            selectedTile[i].transform.Rotate(0, 90 * Instance.rotation, 0);
        }
    }


}

