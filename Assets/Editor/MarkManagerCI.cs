using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

[CustomEditor(typeof(MarkElement))]
public class MarkElementCI : Editor
{

    private void OnEnable()
    {
        var _target = (MarkElement)target;
    }

    public override void OnInspectorGUI()
    {
        var _target = (MarkElement)target;
        EditorGUILayout.Space();
        _target.maxGeneralCount = EditorGUILayout.FloatField(new GUIContent("General Count", "¿Cuánto tiempo pasa desde que el objeto cae al suelo y nadie lo agarra para que vuelva a colocarse en el último player que lo agarró?"), _target.maxGeneralCount);
        _target.maxLocalCount = EditorGUILayout.FloatField(new GUIContent("Local Count", "¿Cuánto tiempo pasa sobre un jugador hasta marcarlo?"), _target.maxLocalCount);
        EditorGUILayout.Space();

    }
}
