using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "VisualTile", menuName = "Scriptable Objects/VisualTile")]
public class VisualTile : ScriptableObject
{
    public List<VisualBloc> blocs;
    public List<Vector2Int> position;
    public List<int> rotation;
}
