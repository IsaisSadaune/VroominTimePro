using UnityEngine;
using UnityEngine.UIElements;
using static MapManager;

public class MapVisuals : MonoBehaviour
{

    [SerializeField] private Transform parentTuiles;

    [SerializeField] private GameObject bloc;
    [SerializeField] private GameObject placedBloc;
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject end;

    [SerializeField] private GameObject cursor;

    private GameObject selectedBloc;

    private GameObject[,] visualMap = new GameObject[10, 10];

    public void SetVisual()
    {
        for (int i = 0; i < Instance.Map.GetLength(0); i++)
        {
            for (int j = 0; j < Instance.Map.GetLength(1); j++)
            {
                switch (Instance.Map[i, j])
                {
                    case Tiles.depart:
                        visualMap[i, j] = Instantiate(start, new Vector3(i, 0, j), Quaternion.identity, parentTuiles);
                        break;
                    case Tiles.arrivee:
                        visualMap[i, j] = Instantiate(end, new Vector3(i, 0, j), Quaternion.identity, parentTuiles);
                        break;
                    case Tiles.startingBloc:
                        visualMap[i, j] = Instantiate(bloc, new Vector3(i, 0, j), Quaternion.identity, parentTuiles);
                        break;
                    case Tiles.placedBloc:
                        visualMap[i, j] = Instantiate(placedBloc, new Vector3(i, 0, j), Quaternion.identity, parentTuiles);
                        break;
                    default:
                        Debug.Log("error");
                        break;
                }
            }
        }
        SetActiveBloc();
        SetCursor();
    }

    public void ApplyPlacement()
    {
        Destroy(visualMap[Instance.position.x, Instance.position.y]);
        visualMap[Instance.position.x, Instance.position.y] = Instantiate(placedBloc, new Vector3(Instance.position.x, 0,Instance.position.y), Quaternion.identity, parentTuiles);
    }

    public void ApplyMovement()
    {
        MoveCursor();
        MoveActiveBloc();

    }
    private void SetCursor()    
    {
        cursor = Instantiate(cursor, new Vector3(Instance.position.x, 2, Instance.position.y), Quaternion.identity);

    }
    public void MoveCursor()
    {
        cursor.transform.position = new Vector3(Instance.position.x, 2, Instance.position.y);
    }

    public void SetActiveBloc()
    {
        selectedBloc = Instantiate(placedBloc, new Vector3(Instance.position.x, 0, Instance.position.y), Quaternion.identity, parentTuiles);
    }
    public void MoveActiveBloc()
    {
        selectedBloc.transform.position = new Vector3(Instance.position.x, 0, Instance.position.y);
    }
}

