using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(BlockScript))]
public class BlockScriptEditor: Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BlockScript myScript = (BlockScript)target;
        if (GUILayout.Button("Reset Positions"))
        {
            myScript.ResetTilePositions();
        }
    }
}