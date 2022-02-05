using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PoolCells))]
public class PoolCellsEditor : Editor
{
    private PoolCells _poolCells;

    private void OnEnable()
    {
        _poolCells = target as PoolCells;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Generate"))
        {
            _poolCells.DeleteAllCells();
            _poolCells.Generate();
        }

        if (GUILayout.Button("Set Player"))
        {
            _poolCells.SetPlayer();
        }
    }
}
