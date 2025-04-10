using UnityEngine;
using UnityEngine.UIElements;
using static MapManager;

public class MapVisuals : MonoBehaviour
{

    [SerializeField] private Transform parentTuiles;

    [SerializeField] private GameObject cursor;


    private GameObject selectedBloc;

    private GameObject[,] visualMap = new GameObject[10, 10];

    public void SetVisual()
    {
        for (int i = 0; i < Instance.Map.GetLength(0); i++)
        {
            for (int j = 0; j < Instance.Map.GetLength(1); j++)
            {
                Debug.Log(Instance.BlocList);
                visualMap[i, j] = Instantiate(Instance.BlocList.GetBloc(Instance.Map[i, j]).bloc, new Vector3(i, 0, j), Quaternion.identity, parentTuiles);
            }
        }
        SetActiveBloc();
        SetCursor();
    }
    public void ApplyPlacement()
    {
        Destroy(visualMap[Instance.position.x, Instance.position.y]);
        visualMap[Instance.position.x, Instance.position.y] = Instantiate(selectedBloc, new Vector3(Instance.position.x, 0,Instance.position.y), selectedBloc.transform.rotation, parentTuiles);
    }
    public void ApplyMovement()
    {
        MoveCursor();
        MoveActiveBloc();
    }
    public void ApplyChangeTile()
    {
        if(selectedBloc!=null) Destroy(selectedBloc);
        SetActiveBloc();
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
        selectedBloc = Instantiate(Instance.BlocList.GetBloc(Instance.ActiveTile1x1).bloc, new Vector3(Instance.position.x, 0.1f, Instance.position.y), Quaternion.identity, parentTuiles);
    }
    public void MoveActiveBloc()
    {
        selectedBloc.transform.position = new Vector3(Instance.position.x, 0.1f, Instance.position.y);
    }

    public void ApplyRotationRight()
    {
        selectedBloc.transform.Rotate(0, 90, 0);
    }
    public void ApplyRotationLeft()
    {
        selectedBloc.transform.Rotate(0, -90, 0);
    }
    

}

