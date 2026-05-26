using UnityEditor;
using UnityEngine;


public class SurfaceSnapperTool : EditorWindow
{
    private bool m_alignToNormal = true;

    [SerializeField] private LayerMask m_layerMask = ~0;
    private SerializedObject m_serializedObject;
    private SerializedProperty m_layerMaskProperty;

    private float m_maxDistance = 100;
    private Vector3 m_offset = Vector3.zero;
    private Vector3 m_targetPosition;

    private bool m_randomizeRotation = false;

    private void OnEnable()
    {
        m_serializedObject = new SerializedObject(this);
        m_layerMaskProperty = m_serializedObject.FindProperty("m_layerMask");
    }

    [MenuItem("Tools/Surface Snapper Tool")]
    public static void ShowWindow()
    {
        SurfaceSnapperTool surfaceSnapper = GetWindow<SurfaceSnapperTool>();
        surfaceSnapper.titleContent = new GUIContent("Surface Snapper");
        Debug.Log("Hey Created Surface Snapper Tool Window");
    }

    private void OnGUI()
    {
        m_alignToNormal = EditorGUILayout.Toggle("Align To Normal: ", m_alignToNormal);

        EditorGUILayout.Space(10);
        m_serializedObject.Update();
        EditorGUILayout.PropertyField(m_layerMaskProperty, new GUIContent("Target Layers"));
        m_serializedObject.ApplyModifiedProperties();

        EditorGUILayout.Space(10);
        m_maxDistance = EditorGUILayout.FloatField("Max Distance", m_maxDistance);
        m_offset = EditorGUILayout.Vector3Field("Max Offset", m_offset);

        EditorGUILayout.Space(10);
        m_randomizeRotation = EditorGUILayout.Toggle("Randomize Rotation: ", m_randomizeRotation);

        SetExecutionButton();
    }

    private void SetExecutionButton()
    {
        EditorGUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Execute", GUILayout.Width(150)))
        {
            DropObjects(Selection.gameObjects);
        }

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }

    private void DropObjects(GameObject[] selectedObjects)
    {
        // Find posisition 
        foreach (GameObject obj in selectedObjects)
        {
            GetSnapPosition(obj);
        }
    }

    private void GetSnapPosition(GameObject currentObj)
    {
        if (Physics.Raycast(currentObj.transform.position, Vector3.down, out RaycastHit hitInfo, m_maxDistance, m_layerMask))
        {
            Undo.RecordObject(currentObj.transform, "Snap To Surface");


            m_targetPosition = hitInfo.point;
            currentObj.transform.position = m_targetPosition + m_offset;

            if (m_alignToNormal)
                currentObj.transform.up = hitInfo.normal;
            if (m_randomizeRotation)
            {
                float val = Random.Range(0, 360);
                currentObj.transform.Rotate(0f, val, 0f, Space.Self);
            }
        }
        else if (Physics.Raycast(currentObj.transform.position, Vector3.up, out hitInfo, m_maxDistance, m_layerMask))
        {
            Undo.RecordObject(currentObj.transform, "Snap To Surface");
            m_targetPosition = hitInfo.point;
            currentObj.transform.position = m_targetPosition - m_offset;

            if (m_alignToNormal)
                currentObj.transform.up = hitInfo.normal;

            if (m_randomizeRotation)
            {
                float val = Random.Range(0, 360);
                currentObj.transform.Rotate(0f, val, 0f, Space.Self);
            }
        }
        else
        {
            Debug.Log("No Ground is found to snap to.");
        }
    }
}
