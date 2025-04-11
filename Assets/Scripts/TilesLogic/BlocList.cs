using UnityEngine;
using System.Collections.Generic;

public class BlocList : MonoBehaviour
{
    [field: SerializeField] public List<VisualBloc> Blocs { get; private set; }

    public VisualBloc GetBloc(int id)
    {
        foreach (VisualBloc v in Blocs)
        {
            if (v.id == id) return v;
        }
        Debug.Log("id inconnu");
        return Blocs[0];
    }

}
