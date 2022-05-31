using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;


[CustomEditor(typeof(GenericBarUI))]

public class SAVINGCUSTOM : Editor
{


    private SerializedProperty transitionsDataHolders;
    int transitionsDataHoldersSize;
    // store which dialogue is foldout
    private List<bool> transitionsDataHoldersFoldout = new List<bool>();

    int transitionDatasSize;
    // store which dialogue is foldout
    List<bool> transitionDatasFoldout = new List<bool>();
    private void OnEnable()
    {
        transitionsDataHolders = serializedObject.FindProperty("transitionsDataHolders");
        for (var i = 0; i < transitionsDataHolders.arraySize; i++)
        {
            transitionsDataHoldersFoldout.Add(false);

        }
        for (var i = 0; i < transitionsDataHolders.arraySize; i++)
        {
            var dialogue = transitionsDataHolders.GetArrayElementAtIndex(i);
            var sentences = dialogue.FindPropertyRelative("transitionDatas");
            Debug.Log(sentences.arraySize);
            transitionDatasSize = sentences.arraySize;
        }


        for (var si = 0; si < transitionDatasSize; si++)
        {
            transitionDatasFoldout.Add(false);
        }



    }
    // The function that makes the custom editor work
    public override void OnInspectorGUI()
    {
        serializedObject.Update(); // Update list
        var color = GUI.color;

        var realBarUI = serializedObject.FindProperty("realBarUI");
        EditorGUILayout.PropertyField(realBarUI, new GUIContent("realBarUI"));

        var ghostBarUI = serializedObject.FindProperty("ghostBarUI");
        EditorGUILayout.PropertyField(ghostBarUI, new GUIContent("ghostBarUI"));

        var restrictedBarUI = serializedObject.FindProperty("restrictedBarUI");
        EditorGUILayout.PropertyField(restrictedBarUI, new GUIContent("restrictedBarUI"));

        EditorGUI.BeginChangeCheck();

        transitionsDataHoldersSize = EditorGUILayout.IntField("Transitions Data Holders Size", transitionsDataHolders.arraySize);
        transitionsDataHolders.arraySize = transitionsDataHoldersSize;

        if (EditorGUI.EndChangeCheck())
        {
            transitionsDataHoldersFoldout.Clear();

            for (var i = 0; i < transitionsDataHolders.arraySize; i++)
            {
                transitionsDataHoldersFoldout.Add(false);
            }

            serializedObject.ApplyModifiedProperties();
            return;
        }

        for (var i = 0; i < transitionsDataHolders.arraySize; i++)
        {
            var dialogue = transitionsDataHolders.GetArrayElementAtIndex(i);

            transitionsDataHoldersFoldout[i] = EditorGUILayout.Foldout(transitionsDataHoldersFoldout[i], "Transitions Data Holders" + i);

            // make the next fields look nested below the before one
            EditorGUI.indentLevel++;

            if (transitionsDataHoldersFoldout[i])
            {
                //var name = dialogue.FindPropertyRelative("TEST");
                var sentences = dialogue.FindPropertyRelative("transitionDatas");


                //if (string.IsNullOrWhiteSpace(name.stringValue)) GUI.color = Color.yellow;
                //EditorGUILayout.PropertyField(name);
                GUI.color = color;



                //TEMMP
                EditorGUI.BeginChangeCheck();


                transitionDatasSize = EditorGUILayout.IntField("Transition Datas Size", sentences.arraySize);
                sentences.arraySize = transitionDatasSize;

                if (EditorGUI.EndChangeCheck())
                {
                    transitionDatasFoldout.Clear();

                    for (var xi = 0; xi < sentences.arraySize; xi++)
                    {

                        transitionDatasFoldout.Add(false);
                    }

                    serializedObject.ApplyModifiedProperties();
                    return;
                }


                //TEMP

                // make the next fields look nested below the before one
                EditorGUI.indentLevel++;

                SerializedObject ss = sentences.serializedObject;
                ss.Update(); // Update list


                for (var s = 0; s < sentences.arraySize; s++)
                {

                    transitionDatasFoldout[s] = EditorGUILayout.Foldout(transitionDatasFoldout[s], "Transitions Data" + s);
                    if (transitionDatasFoldout[s])
                    {
                        var sentence = sentences.GetArrayElementAtIndex(s);
                        //if (string.IsNullOrWhiteSpace(sentence.stringValue)) GUI.color = Color.yellow;
                        var image = sentence.FindPropertyRelative("image");
                        var displayCategory = sentence.FindPropertyRelative("display");

                        EditorGUILayout.PropertyField(image, new GUIContent("image " + s));

                        // Switch statement to handle what happens for each category
                        displayCategory.enumValueIndex = (int)(TransitionType)EditorGUILayout.EnumPopup("displayCategory:",
                            (TransitionType)Enum.GetValues(typeof(TransitionType)).GetValue(displayCategory.enumValueIndex));
                        switch (displayCategory.enumValueIndex)
                        {
                            case 0:
                                DisplayBasicInfo();
                                break;

                            case 1:
                                DisplayCombatInfo();
                                break;

                            case 2:
                                DisplayMagicInfo();
                                break;

                        }
                        GUI.color = color;
                    }
                }
                EditorGUI.indentLevel--;
            }

            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();
    }

    // When the categoryToDisplay enum is at "Basic"
    void DisplayBasicInfo()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("amount"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("transitionTime"));

    }

    // When the categoryToDisplay enum is at "Combat"
    void DisplayCombatInfo()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("amount"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("transitionTime"));

    }

    // When the categoryToDisplay enum is at "Magic"
    void DisplayMagicInfo()
    {
        EditorGUILayout.PropertyField(serializedObject.FindProperty("amount"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("transitionTime"));
        // Store the hasMagic bool as a serializedProperty so we can access it
        SerializedProperty hasMagicProperty = serializedObject.FindProperty("hasMagic");

        // Draw a property for the hasMagic bool
        EditorGUILayout.PropertyField(hasMagicProperty);

        // Check if hasMagic is true
        if (hasMagicProperty.boolValue)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("mana"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("magicType"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("magicDamage"));
        }


    }
}
