using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using NUnit.Framework;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using System.Drawing.Printing;

public class PrefabChildExtractor : EditorWindow
{
    private GameObject selectedPrefab;
    [MenuItem("Tools/Créer un Prefab depuis le premier enfant")]
    public static void ShowWindow()
    {
        GetWindow<PrefabChildExtractor>("Extractor de Prefab Enfant");
    }

    private void OnGUI()
    {
        GUILayout.Label("Sélectionne un Prefab", EditorStyles.boldLabel);
        selectedPrefab = (GameObject)EditorGUILayout.ObjectField("Prefab", selectedPrefab, typeof(GameObject), false);

        if (selectedPrefab != null && GUILayout.Button("Créer prefab du 1er enfant"))
        {
            CreateChildPrefab(selectedPrefab);
        }

        GUILayout.Label("Sélectionne un Prefab", EditorStyles.boldLabel);

    }

    //LOGIQUE DE SCRIPTABLE

    private void CreateChildPrefab(GameObject prefab)
    {
        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

        string prefabPath = AssetDatabase.GetAssetPath(prefab);
        string folderPath = Path.GetDirectoryName(prefabPath);
        string Chemin = Path.Combine(folderPath, $"{prefab.name}_Tile.asset");


        VisualTile tileObject = ScriptableObject.CreateInstance<VisualTile>();


        for (int i = 0; i < instance.transform.childCount; i++)
        {
            Transform child = instance.transform.GetChild(i);
            string assetPath = Path.Combine(folderPath, $"{child.name.Trim()}_Data.asset");

            VisualBloc existingSO = AssetDatabase.LoadAssetAtPath<VisualBloc>(assetPath);

            VisualBloc so = ScriptableObject.CreateInstance<VisualBloc>();

            GameObject childInstance = child.gameObject;
            int rotationAmount = Mathf.RoundToInt(childInstance.transform.rotation.eulerAngles.y);

            GameObject sourcePrefab = (GameObject)PrefabUtility.GetCorrespondingObjectFromSource(childInstance);

            if (existingSO != null)
            {
                so = existingSO;
            }
            else
            {
                so = ScriptableObject.CreateInstance<VisualBloc>();
                so.bloc = sourcePrefab;
                AssetDatabase.CreateAsset(so, assetPath);
            }

            tileObject.blocs.Add(so);
            Vector2Int positionBloc = new Vector2Int(Mathf.RoundToInt(child.position.x), Mathf.RoundToInt(child.position.z));
            tileObject.position.Add(positionBloc);
            tileObject.rotation.Add(rotationAmount);

        }


        AssetDatabase.CreateAsset(tileObject, Chemin);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        DestroyImmediate(instance);

    }
}