using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoundSpawner))]
public class RoundSpawnerEditor : Editor
{
    private void OnSceneGUI()
    {
        var t = target as RoundSpawner;
        var tr = t.transform;

        Handles.color = Color.green;
        Handles.DrawWireDisc(tr.position, tr.up, t.InnerRadius);
        Handles.color = Color.red;
        Handles.DrawWireDisc(tr.position, tr.up, t.OuterRadius);
    }
}
