using UnityEngine;

[CreateAssetMenu(fileName = "VisualBloc", menuName = "Scriptable Objects/VisualBloc")]
public class VisualBloc : ScriptableObject
{
    public GameObject bloc;
    /// <summary>
    /// ATTENTION : ID UNIQUE !!!
    /// </summary>
    [Tooltip("ATTENTION : ID UNIQUE !!!")] public int id;
}
