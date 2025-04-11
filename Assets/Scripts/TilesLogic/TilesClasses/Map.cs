using UnityEngine;

[CreateAssetMenu(fileName = "Map", menuName = "Scriptable Objects/Map")]
public class Map : ScriptableObject
{
    private Bloc[] bloc;

    private Vector2Int size;
    public Vector2Int Size
    {
        get
        {
            return size;
        }
        set
        {
            bloc = new Bloc[value.x * value.y];
            size = value;
        }
    }

    public Bloc GetBloc(int x, int y)
    {
        return bloc[y * size.x + x];
    }

}
