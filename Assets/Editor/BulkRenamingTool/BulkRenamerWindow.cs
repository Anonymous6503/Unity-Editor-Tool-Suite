using UnityEngine;
using UnityEditor;
using System.Text;

public class BulkRenamerWindow : EditorWindow
{
    private string prefix = "";
    private string baseName = "";
    private bool useSequentialNumbers = true;
    private bool setTargetParent = false;
    private Transform targetParent;

    private bool setTag = false;
    private string targetTag = "Untagged";

    private bool setLayer = false;
    private int targetLayer = 0;

    private bool isObjectStatic = false;

    [MenuItem("Tools/Bulk Rename")]
    public static void ShowWindow()
    {
        BulkRenamerWindow window = GetWindow<BulkRenamerWindow>();
        window.titleContent = new GUIContent("Bulk Rename");
        Debug.Log("Hey Created Renamer Window");
    }

    private void OnGUI()
    {
        prefix = EditorGUILayout.TextField("Enter Prefix: ", prefix);
        baseName = EditorGUILayout.TextField("Enter Basename: ", baseName);
        useSequentialNumbers = EditorGUILayout.Toggle("Use Sequential Numbers: ", useSequentialNumbers);

        EditorGUILayout.Space(10);
        setTargetParent = EditorGUILayout.Toggle("Set Target Parent: ", setTargetParent);
        if (setTargetParent)
        {
            targetParent = (Transform)EditorGUILayout.ObjectField("Target Parent: ", targetParent, typeof(Transform), true);
        }
        else
        {
            targetParent = null;
        }

        EditorGUILayout.Space(10);

        setTag = EditorGUILayout.Toggle("Set Tag: ", setTag);
        if (setTag)
            targetTag = EditorGUILayout.TagField("Target Tag: ", targetTag);
        else
            targetTag = null;

        EditorGUILayout.Space(10);
        setLayer = EditorGUILayout.Toggle("Target Layer: ", setLayer);
        if (setLayer)
            targetLayer = EditorGUILayout.LayerField("Set Layer: ", targetLayer);
        else
            targetLayer = 0;

        EditorGUILayout.Space(10);

        isObjectStatic = EditorGUILayout.Toggle("Set Static: ", isObjectStatic);

        GUILayout.Space(25);

        SetExecuteButton();
    }

    private void SetExecuteButton()
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Execute", GUILayout.Width(150)))
        {
            ExecuteBulkAction(Selection.gameObjects);
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }

    private void ExecuteBulkAction(GameObject[] selectedObjects)
    {
        if (selectedObjects.Length == 0)
        {
            Debug.LogWarning("No Objects Selected");
            return;
        }

        for (int i = 0; i < selectedObjects.Length; i++)
        {
            Undo.RecordObject(selectedObjects[i], "Bulk Rename");
            selectedObjects[i].name = GenerateNewName(i + 1);

            if (setTargetParent && targetParent != null)
            {
                Undo.SetTransformParent(selectedObjects[i].transform, targetParent, "Bulk Parent");
            }

            if (setTag)
                selectedObjects[i].tag = targetTag;

            if(setLayer)
                selectedObjects[i].layer = targetLayer;

            selectedObjects[i].isStatic = isObjectStatic;
        }
    }

    private string GenerateNewName(int index)
    {
        StringBuilder finalName = new StringBuilder();

        if (!string.IsNullOrEmpty(prefix))
            finalName.Append(prefix).Append("_");

        finalName.Append(baseName);

        if (useSequentialNumbers)
            finalName.Append("_").Append(index.ToString("D2"));

        return finalName.ToString();
    }

}
