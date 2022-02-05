using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Cell))]
public class CellEditor : Editor
{
    private Cell _cell;

    private void OnEnable()
    {
        _cell = target as Cell;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Change lock"))
        {
            _cell.ChangeLock();
        }
    }
}
