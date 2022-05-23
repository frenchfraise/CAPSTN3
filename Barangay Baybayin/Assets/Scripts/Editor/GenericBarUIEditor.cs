using UnityEditor;
using UnityEngine;
using System;


[Flags]
public enum EditorListOption
{
    None = 0,
    ListSize = 1,
    ListLabel = 2,
    ElementLabels = 4,
    Buttons = 8,
    Default = ListSize | ListLabel | ElementLabels,
    NoElementLabels = ListSize | ListLabel,
    All = Default | Buttons
}

[CustomEditor(typeof(GenericBarUI))]
public class GenericBarUIEditor : Editor
{


    //public override void OnInspectorGUI()
    //{
    //    base.OnInspectorGUI();
    //    serializedObject.Update();
 
    //    GenericBarUIEditor.Show(serializedObject.FindProperty("transitionsDataHolders"), EditorListOption.Buttons);
    //    GenericBarUIEditor.Show(serializedObject.FindProperty("transitionsDataHolders").FindPropertyRelative("transitionDatas")
    //    , EditorListOption.Buttons) ;
    //    serializedObject.ApplyModifiedProperties();

    //    GenericBarUI genericBarUI = (GenericBarUI)target;
 
      
    //}

    public static void Show(SerializedProperty list, EditorListOption options = EditorListOption.Default)
    {
        if (!list.isArray)
        {
            EditorGUILayout.HelpBox(list.name + " is neither an array nor a list!", MessageType.Error);
            return;
        }

        bool
            showListLabel = (options & EditorListOption.ListLabel) != 0,
            showListSize = (options & EditorListOption.ListSize) != 0;

        if (showListLabel)
        {
            EditorGUILayout.PropertyField(list);
            EditorGUI.indentLevel += 1;
        }
        if (!showListLabel || list.isExpanded)
        {
            SerializedProperty size = list.FindPropertyRelative("Array.size");
            if (showListSize)
            {
                EditorGUILayout.PropertyField(size);
            }
            if (size.hasMultipleDifferentValues)
            {
                EditorGUILayout.HelpBox("Not showing lists with different sizes.", MessageType.Info);
            }
            else
            {
                ShowElements(list, options);
            }
        }
        if (showListLabel)
        {
            EditorGUI.indentLevel -= 1;
        }
    }

    private static GUIContent
    moveButtonContent = new GUIContent("\u21b4", "move down"),
    duplicateButtonContent = new GUIContent("+", "duplicate"),
    deleteButtonContent = new GUIContent("-", "delete"),
    addButtonContent = new GUIContent("+", "add element");
    private static void ShowElements(SerializedProperty list, EditorListOption options)
    {
        bool
            showElementLabels = (options & EditorListOption.ElementLabels) != 0,
            showButtons = (options & EditorListOption.Buttons) != 0;

        for (int i = 0; i < list.arraySize; i++)
        {
            if (showButtons)
            {
                EditorGUILayout.BeginHorizontal();
            }
            if (showElementLabels)
            {
                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
            }
            else
            {
                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), GUIContent.none);
            }
            if (showButtons)
            {
                ShowButtons(list, i);
                EditorGUILayout.EndHorizontal();
            }
        }
        if (showButtons && list.arraySize == 0 && GUILayout.Button(addButtonContent, EditorStyles.miniButton))
        {
            list.arraySize += 1;
        }
    }

    private static GUILayoutOption miniButtonWidth = GUILayout.Width(20f);

    private static void ShowButtons(SerializedProperty list, int index)
    {
        if (GUILayout.Button(moveButtonContent, EditorStyles.miniButtonLeft, miniButtonWidth))
        {
            list.MoveArrayElement(index, index + 1);
        }
        if (GUILayout.Button(duplicateButtonContent, EditorStyles.miniButtonMid, miniButtonWidth))
        {
            list.InsertArrayElementAtIndex(index);
        }
        if (GUILayout.Button(deleteButtonContent, EditorStyles.miniButtonRight, miniButtonWidth))
        {
            int oldSize = list.arraySize;
            list.DeleteArrayElementAtIndex(index);
            if (list.arraySize == oldSize)
            {
                list.DeleteArrayElementAtIndex(index);
            }
        }
    }



    //public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    //{
    //    int oldIndentLevel = EditorGUI.indentLevel;
    //    label = EditorGUI.BeginProperty(position, label, property);
    //    Rect contentPosition = EditorGUI.PrefixLabel(position, label);
    //    if (position.height > 16f)
    //    {
    //        position.height = 16f;
    //        EditorGUI.indentLevel += 1;
    //        contentPosition = EditorGUI.IndentedRect(position);
    //        contentPosition.y += 18f;
    //    }
    //    contentPosition.width *= 0.75f;
    //    EditorGUI.indentLevel = 0;
    //    EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("position"), GUIContent.none);
    //    contentPosition.x += contentPosition.width;
    //    contentPosition.width /= 3f;
    //    EditorGUIUtility.labelWidth = 14f;
    //    EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("color"), new GUIContent("C"));
    //    EditorGUI.EndProperty();
    //    EditorGUI.indentLevel = oldIndentLevel;
    //}
}
