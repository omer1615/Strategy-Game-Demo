#if UNITY_EDITOR
using UnityEditor;

//Unity Editor extend for GameManager to see inside of objecList
[CustomEditor(typeof(GameManager))]
public class GameManagerUIEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        GameManager myTarget = (GameManager)target;
        EditorUtility.SetDirty(target);
        DrawDefaultInspector();
        foreach (var obj in myTarget.objecList)
        {
            EditorGUILayout.LabelField(obj.Name + ": " + obj.Position.x + " " + obj.Position.y);
        }
        
    }
}
#endif