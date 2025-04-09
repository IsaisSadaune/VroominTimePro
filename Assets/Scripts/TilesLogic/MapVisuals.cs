using UnityEngine;
using static MapManager;

public class MapVisuals : MonoBehaviour
{

    [SerializeField] private Transform parentTuiles;

    [SerializeField] private GameObject bloc;
    [SerializeField] private GameObject placedBloc;
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject end;

    public GameObject[,] visualMap = new GameObject[10, 10];

    public void ApplyVisual()
    {
        for (int i = 0; i < Instance.map.GetLength(0); i++)
        {
            for (int j = 0; j < Instance.map.GetLength(1); j++)
            {
                switch (Instance.map[i, j])
                {
                    case Tiles.depart:
                        visualMap[i, j] = Instantiate(start, new Vector3(i, 0, j), Quaternion.identity, parentTuiles);
                        break;
                    case Tiles.arrivee:
                        visualMap[i, j] = Instantiate(end, new Vector3(i, 0, j), Quaternion.identity, parentTuiles);
                        break;
                    case Tiles.bloc:
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
    }
}

