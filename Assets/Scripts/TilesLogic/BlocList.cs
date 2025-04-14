using System.Collections.Generic;
using UnityEngine;

public class BlocList : MonoBehaviour
{
    public static BlocList blocList = null;
    public static BlocList AllBlocsList => blocList;

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

    /// <summary>
    /// Vérifie si les IDs sont tous uniques
    /// </summary>
    /// <returns>retourne -1 si tous les IDs sont uniques, retourne le premier index qui n'est pas unique sinon</returns>
    public int CheckIfIDsAreUniques()
    {
        List<int> ids = new();
        for (int i=0; i< Blocs.Count; i++)
        {
            if (ids.Contains(Blocs[i].id))
            {
                Debug.Log("erreur élément " + i);
                return i;
            }
            ids.Add(i);
        }
        return -1;
    }
}
