using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ImagePointSpawner))]
public class ImagePointSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ImagePointSpawner spawner = (ImagePointSpawner)target;

        GUILayout.Space(8);

        if (GUILayout.Button("Spawn"))
            spawner.Spawn();

        if (GUILayout.Button("Clear"))
            spawner.Clear();

        if (GUILayout.Button("Merge"))
            spawner.Merge();
    }
}
