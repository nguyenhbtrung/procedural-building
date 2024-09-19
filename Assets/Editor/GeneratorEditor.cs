using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Generator))]
public class GeneratorEditor : Editor
{
    bool myCheckbox = false;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Generator generator = (Generator)target;
        myCheckbox = EditorGUILayout.Toggle("My Checkbox", myCheckbox);
        if (GUILayout.Button("Generate Area"))
        {
            generator.Initialize();
            generator.InitGrid();
        }
        if (GUILayout.Button("Apply"))
        {
            generator.ApplyArea();
        }
    }
}
