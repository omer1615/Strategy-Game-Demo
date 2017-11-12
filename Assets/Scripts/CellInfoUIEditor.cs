#if UNITY_EDITOR
using UnityEditor;

//Unity Editor extend for CellInfo to see variables of Cell class
[CustomEditor(typeof(CellInfo))]
public class CellInfoUIEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        CellInfo myTarget = (CellInfo)target;
        EditorUtility.SetDirty(target);
        DrawDefaultInspector();
        EditorGUILayout.LabelField("Cell position: " + myTarget.cell.type.Position);
        EditorGUILayout.LabelField("Cell parent type: " + myTarget.cell.type);
    }
}
#endif