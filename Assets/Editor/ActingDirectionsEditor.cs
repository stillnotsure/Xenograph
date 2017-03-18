using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(ActingDirections))]
public class ActingDirectionsEditor : Editor
{
    private ReorderableList list;

    private void OnEnable()
    {
        list = new ReorderableList(serializedObject,
                serializedObject.FindProperty("directions"),
                true, true, true, true);

        list.drawElementCallback =
        (Rect rect, int index, bool isActive, bool isFocused) => {
            var element = list.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;
            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("actor"), GUIContent.none);
            EditorGUI.PropertyField(
                new Rect(rect.x + 60, rect.y, 600, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("direction"), GUIContent.none);
            EditorGUI.PropertyField(
                new Rect(rect.x + rect.width - 55, rect.y, 40, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("timeStart"), GUIContent.none);
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}