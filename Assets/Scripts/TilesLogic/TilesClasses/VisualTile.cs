using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "VisualTile", menuName = "Scriptable Objects/VisualTile")]
public class VisualTile : ScriptableObject
{
    public List<VisualBloc> blocs = new List<VisualBloc>();
    public List<Vector2Int> position = new List<Vector2Int>();
    public List<int> rotation = new List<int>();
}
