using UnityEngine;
using System.Collections.Generic;

public class BlocList : MonoBehaviour
{
    public static BlocList blocList = null;
    public static BlocList AllBlocsList  => blocList;

    private void Awake()
    {
        if (blocList != null && blocList != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            blocList = this;
        }
    }
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
