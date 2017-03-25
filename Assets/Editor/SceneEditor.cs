using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(Scene))]
public class LevelDataEditor : Editor
{
    private int actorWidth = 30;
    private int directionTypeWidth = 100;
    private int timeStartWidth = 30;
    private int dialogueWidth = 600;
    private int markedWidth = 30;
    private int animationWidth = 300;
    private int flipXWidth = 30;
    private int targetPositionWidth = 300;
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
            int xTotal = 0;
            //Generic Fields
            EditorGUI.PropertyField(
                new Rect(rect.x + xTotal, rect.y, actorWidth, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("actor"), GUIContent.none);
            xTotal += actorWidth;
            EditorGUI.PropertyField(
                new Rect(rect.x + xTotal, rect.y, timeStartWidth, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("timeStart"), GUIContent.none);
            xTotal += timeStartWidth;
            EditorGUI.PropertyField(
                new Rect(rect.x + xTotal, rect.y, directionTypeWidth, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("directionType"), GUIContent.none);
            xTotal += directionTypeWidth;
            //Dialogue Fields
            if (element.FindPropertyRelative("directionType").enumValueIndex == (int)ActingDirection.DirectionType.Dialogue)
            {
                EditorGUI.PropertyField(
                    new Rect(rect.x + xTotal, rect.y, rect.width - xTotal - markedWidth, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("dialogue"), GUIContent.none);
                xTotal += (int)(rect.width - xTotal - markedWidth);
                EditorGUI.PropertyField(
                    new Rect(rect.x + xTotal, rect.y, rect.width - xTotal, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("marked"), GUIContent.none);
                xTotal += markedWidth;
            } else
            //Animation Fields
            if (element.FindPropertyRelative("directionType").enumValueIndex == (int)ActingDirection.DirectionType.Animation)
            {
                EditorGUI.PropertyField(
                    new Rect(rect.x + xTotal, rect.y, rect.width-xTotal, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("animation"), GUIContent.none);
                xTotal += animationWidth;
            } else
            //Position Fields
            if (element.FindPropertyRelative("directionType").enumValueIndex == (int)ActingDirection.DirectionType.Position)
            {
                EditorGUI.PropertyField(
                    new Rect(rect.x + xTotal, rect.y, flipXWidth, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("flipX"), GUIContent.none);
                xTotal += flipXWidth;
                EditorGUI.PropertyField(
                    new Rect(rect.x + xTotal, rect.y, rect.width - xTotal, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("targetPosition"), GUIContent.none);
                xTotal += targetPositionWidth;
            }
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}