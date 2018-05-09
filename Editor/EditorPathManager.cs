using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathManager))]
public class PathEditor : Editor {

    PathManager pathManager;

    void OnEnable() 
    {
        pathManager = (target as PathManager);
    }

    void OnSceneGUI()
    {
        SceneView.RepaintAll();

        if (Event.current.type == EventType.Layout)
        {
            HandleUtility.AddDefaultControl(0);
        }

        foreach (Waypoint waypoint in pathManager.waypoints)
        {
            EditorGUI.BeginChangeCheck();
            Vector3 newPos = Handles.FreeMoveHandle(waypoint.position, Quaternion.identity, 1f, Vector3.one * 0.5f, Handles.SphereHandleCap);
            
            if (EditorGUI.EndChangeCheck())
            {
                waypoint.position = newPos;
            }
        }

        if (Event.current.type == EventType.MouseDown && Event.current.button == 0)
        {
            Waypoint temp = new Waypoint();
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            temp.position = new Vector3(ray.origin.x, ray.origin.y, 0f);
            pathManager.waypoints.Add(temp);
        }
    }

    public override void OnInspectorGUI()
    {
        Repaint();
        // default inspector properties from base class won't appear
        //base.OnInspectorGUI();
        GUILayout.Label("Waypoints");

        serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("handleColor"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("lineColor"));

            GUILayout.Space(15f);
        
            GUILayout.Label("Number of Waypoints (" + pathManager.waypoints.Count.ToString() + ")");
            GUILayout.BeginHorizontal();
                if (GUILayout.Button("Add")) 
                {
                    Waypoint temp = new Waypoint();
                    pathManager.waypoints.Add(temp);
                }
                if (GUILayout.Button("Remove All")) 
                {
                    pathManager.waypoints.Clear();
                }
            GUILayout.EndHorizontal();

            SerializedProperty list = serializedObject.FindProperty("waypoints");
            for (int i = 0; i < list.arraySize; i++)
            {
                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i).FindPropertyRelative("position"));
            }

            if (pathManager.waypoints.Count > 0)
            {
                if (GUILayout.Button("Remove")) 
                {
                    if (list.arraySize > 0)
                        pathManager.waypoints.RemoveAt(list.arraySize - 1);
                }
            }

        serializedObject.ApplyModifiedProperties();
    }
}