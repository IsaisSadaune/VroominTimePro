using UnityEngine;

public class BlocLinker
{
    public BlocLinker(Bloc bloc, Vector2Int pos)
    {
        Bloc = bloc;
        Position = pos;
    }

    public Bloc Bloc { get; private set; }
    public Vector2Int Position { get; private set; }
}
