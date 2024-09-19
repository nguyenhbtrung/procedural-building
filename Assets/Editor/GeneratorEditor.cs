using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Generator))]
public class GeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        DrawDefaultInspector();
        Generator generator = (Generator)target;
        
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
