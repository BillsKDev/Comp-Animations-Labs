using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PathManager))]
public class PathManagerEditor : Editor
{
    [SerializeField] PathManager _pathManager;
    [SerializeField] List<Waypoint> _path;
    
    List<int> _toDelete;

    Waypoint _selectedPoint;
    bool _doRepaint = true;

    void OnSceneGUI()
    {
        _path = _pathManager.GetPath();
        DrawPath(_path);
    }
    
    void OnEnable()
    {
        _pathManager = target as PathManager;
        _toDelete = new List<int>();
    }

    public override void OnInspectorGUI()
    {
        this.serializedObject.Update();
        _path = _pathManager.GetPath();   
        
        base.OnInspectorGUI();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Path");

        DrawGUIForPoints();
        if (GUILayout.Button("Add Point to Path") )
        {
            _pathManager.CreateAddPoint();
        }
        EditorGUILayout.EndVertical();
        SceneView.RepaintAll();
    }

    void DrawGUIForPoints()
    {
        if (_path != null && _path.Count > 0)
        {
            for (int i = 0; i < _path.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                Waypoint p = _path[i];

                Color c = GUI.color;
                if (_selectedPoint == p) GUI.color = Color.green;

                Vector3 oldPos = p.GetPosition();
                Vector3 newPos = EditorGUILayout.Vector3Field("", oldPos);

                if (EditorGUI.EndChangeCheck()) p.SetPosition(newPos);

                if (GUILayout.Button("-", GUILayout.Width(25))) _toDelete.Add(i);

                GUI.color = c;
                EditorGUILayout.EndHorizontal();
            }
        }
        if (_toDelete.Count > 0)
        {
            foreach (int i in _toDelete) _path?.RemoveAt(i);
            _toDelete.Clear();
        }
    }
    void DrawPath(List<Waypoint> path)
    {
        if(path != null)
        {
            int current = 0;
            foreach(Waypoint wp in path)
            {
                _doRepaint = DrawPoint(wp);
                int next = (current + 1) % path.Count;
                Waypoint wpNext = path[next];

                DrawPathLine(wp, wpNext);
                current += 1;
            }
        }
        if (_doRepaint) Repaint();
    }

    void DrawPathLine(Waypoint p1, Waypoint p2)
    {
        Color c = Handles.color;
        Handles.color = Color.gray;
        Handles.DrawLine(p1.GetPosition(), p2.GetPosition());
        Handles.color = c;
    }
    
    bool DrawPoint(Waypoint p)
    {
        bool isChanged = false;
        if (_selectedPoint == p)
        {
            Color c = Handles.color;
            Handles.color = Color.green;

            EditorGUI.BeginChangeCheck();
            Vector3 oldPos = p.GetPosition();
            Vector3 newPos = Handles.PositionHandle(oldPos, Quaternion.identity);

            float handleSize = HandleUtility.GetHandleSize(newPos);
            Handles.SphereHandleCap(-1, newPos, Quaternion.identity, 0.25f * handleSize, EventType.Repaint);
            
            if (EditorGUI.EndChangeCheck()) p.SetPosition(newPos);
            Handles.color = c;
        }
        else
        {
            Vector3 currPos = p.GetPosition();
            float handleSize = HandleUtility.GetHandleSize(currPos);
            if (Handles.Button(currPos, Quaternion.identity, 0.25f * handleSize, 0.25f * handleSize, Handles.SphereHandleCap))
            {
                isChanged = true;
                _selectedPoint = p;
            }
        }
        return isChanged;
    }
}