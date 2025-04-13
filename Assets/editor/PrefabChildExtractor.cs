using UnityEngine;
using UnityEditor;
using System.IO;

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
    }

    private void CreateChildPrefab(GameObject prefab)
    {
        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

        string prefabPath = AssetDatabase.GetAssetPath(prefab);
        string folderPath = Path.GetDirectoryName(prefabPath);
        for (int i = 0; i < instance.transform.childCount; i++)
        {
            Transform child = instance.transform.GetChild(i);
            VisualBloc so = ScriptableObject.CreateInstance<VisualBloc>();
            string assetPath = Path.Combine(folderPath, $"{child.name}_Data.asset");
            GameObject childInstance = child.gameObject;
            GameObject sourcePrefab = (GameObject)PrefabUtility.GetCorrespondingObjectFromSource(childInstance);
            so.bloc = sourcePrefab;
            AssetDatabase.CreateAsset(so, assetPath);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        DestroyImmediate(instance);

    }
}