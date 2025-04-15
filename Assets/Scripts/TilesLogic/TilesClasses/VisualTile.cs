using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "VisualTile", menuName = "Scriptable Objects/VisualTile")]
public class VisualTile : ScriptableObject
{
    public List<VisualBloc> blocs;
    public List<Vector2Int> position;
    /// <summary>
    /// donne la rotation de la tuile.
    /// /!\ Ne mettre en valeur que 0, 90, 180, 270 /!\
    /// </summary>
    [Tooltip("/!\\ Ne mettre en valeur que 0, 90, 180, 270 /!\\")]
    public List<int> rotation;

}
