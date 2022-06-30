using UnityEditor;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.EditorCoroutines.Editor;
public class TransitionsDataFoldout
{
    public int transitionDatasSize;
    // store which dialogue is foldout
    public List<bool> transitionDatasFoldout = new List<bool>();
    public List<TransitionType> transitionDatasTypes = new List<TransitionType>();
}
//[CustomEditor(typeof(GenericBarUI))]
public class GenericBarUIEditor : Editor
{

    //private SerializedProperty transitionsDataHolders;
    //int transitionsDataHoldersSize;
    //// store which dialogue is foldout
    //private List<bool> transitionsDataHoldersFoldout = new List<bool>();


    //List<TransitionsDataFoldout> transitionDatasFoldouts = new List<TransitionsDataFoldout>();
    //private void OnEnable()
    //{
    //    transitionsDataHolders = serializedObject.FindProperty("transitionsDataHolders");
    //    for (var i = 0; i < transitionsDataHolders.arraySize; i++)
    //    {
    //        transitionsDataHoldersFoldout.Add(false);
    //        transitionDatasFoldouts.Add(new TransitionsDataFoldout());
    //    }
    //    for (var i = 0; i < transitionsDataHolders.arraySize; i++)
    //    {
    //        var dialogue = transitionsDataHolders.GetArrayElementAtIndex(i);
    //        var sentences = dialogue.FindPropertyRelative("transitionDatas");
         
    //        transitionDatasFoldouts[i].transitionDatasSize = sentences.arraySize;
    //        for (var si = 0; si < transitionDatasFoldouts[i].transitionDatasSize; si++)
    //        {
    //            transitionDatasFoldouts[i].transitionDatasFoldout.Add(false);
    //            transitionDatasFoldouts[i].transitionDatasTypes.Add(TransitionType.None);
    //        }
    //    }


        
        

      
    //}
    //// The function that makes the custom editor work
    //public override void OnInspectorGUI()
    //{
    //    serializedObject.Update(); // Update list
    //    var color = GUI.color;

    //    var realBarUI = serializedObject.FindProperty("realBarUI");
    //    EditorGUILayout.PropertyField(realBarUI, new GUIContent("realBarUI"));

    //    var ghostBarUI = serializedObject.FindProperty("ghostBarUI");
    //    EditorGUILayout.PropertyField(ghostBarUI, new GUIContent("ghostBarUI"));

    //    var restrictedBarUI = serializedObject.FindProperty("restrictedBarUI");
    //    EditorGUILayout.PropertyField(restrictedBarUI, new GUIContent("restrictedBarUI"));

    //    EditorGUI.BeginChangeCheck();
       
    //    transitionsDataHoldersSize = EditorGUILayout.IntField("Transitions Data Holders Size", transitionsDataHolders.arraySize);
    //    transitionsDataHolders.arraySize = transitionsDataHoldersSize;

    //    if (EditorGUI.EndChangeCheck())
    //    {
    //        transitionsDataHoldersFoldout.Clear();
     

    //        for (var i = 0; i < transitionsDataHolders.arraySize; i++)
    //        {
    //            transitionsDataHoldersFoldout.Add(false);

    //        }

    //        serializedObject.ApplyModifiedProperties();
    //        return;
    //    }

    //    for (var i = 0; i < transitionsDataHolders.arraySize; i++)
    //    {
    //        var transitionsDataHolders = this.transitionsDataHolders.GetArrayElementAtIndex(i);

    //        transitionsDataHoldersFoldout[i] = EditorGUILayout.Foldout(transitionsDataHoldersFoldout[i], "Transitions Data Holder " + i);

    //        // make the next fields look nested below the before one
    //        EditorGUI.indentLevel++;

    //        if (transitionsDataHoldersFoldout[i])
    //        {
    //            //var name = dialogue.FindPropertyRelative("TEST");
    //            var transitionsDataHoldersValues = transitionsDataHolders.FindPropertyRelative("transitionDatas");


    //            //if (string.IsNullOrWhiteSpace(name.stringValue)) GUI.color = Color.yellow;
    //            GUI.color = color;
                
              

    //            //TEMMP
    //            EditorGUI.BeginChangeCheck();


    //            transitionDatasFoldouts[i].transitionDatasSize = EditorGUILayout.IntField("Transition Datas Size", transitionsDataHoldersValues.arraySize);
    //            transitionsDataHoldersValues.arraySize = transitionDatasFoldouts[i].transitionDatasSize;

    //            if (EditorGUI.EndChangeCheck())
    //            {
    //                transitionDatasFoldouts[i].transitionDatasFoldout.Clear();
    //                transitionDatasFoldouts[i].transitionDatasTypes.Clear();

    //                for (var xi = 0; xi < transitionsDataHoldersValues.arraySize; xi++)
    //                {

    //                    transitionDatasFoldouts[i].transitionDatasFoldout.Add(false);
    //                    transitionDatasFoldouts[i].transitionDatasTypes.Add(TransitionType.None);
    //                }

    //                transitionsDataHoldersValues.serializedObject.ApplyModifiedProperties();// serializedObject.ApplyModifiedProperties();
    //                return;
    //            }
               

    //            //TEMP

    //            // make the next fields look nested below the before one
    //            EditorGUI.indentLevel++;

    //            SerializedObject ss = transitionsDataHoldersValues.serializedObject;
    //            ss.Update(); // Update list
               

    //            for (var s = 0; s < transitionsDataHoldersValues.arraySize; s++)
    //            {

    //                transitionDatasFoldouts[i].transitionDatasFoldout[s] = EditorGUILayout.Foldout(transitionDatasFoldouts[i].transitionDatasFoldout[s], "Transitions Data " + s);
    //                if (transitionDatasFoldouts[i].transitionDatasFoldout[s])
    //                {
    //                    SerializedProperty transitionDatasValues = transitionsDataHoldersValues.GetArrayElementAtIndex(s);
    //                    //if (string.IsNullOrWhiteSpace(sentence.stringValue)) GUI.color = Color.yellow;
                       
             
    //                    var bar = transitionDatasValues.FindPropertyRelative("bar");

    //                    EditorGUILayout.PropertyField(bar, new GUIContent("bar"));
    //                    SerializedProperty transitionType = transitionDatasValues.FindPropertyRelative("transitionType");
    //                    // Switch statement to handle what happens for each category
    //                    // transitionType.enumValueIndex = (int)(TransitionType)EditorGUILayout.EnumPopup("Transition Type",
    //                    //     (TransitionType)Enum.GetValues(typeof(TransitionType)).GetValue(transitionType.enumValueIndex));
    //                    transitionDatasFoldouts[i].transitionDatasTypes[s] = (TransitionType)EditorGUILayout.EnumPopup("Transition Type", transitionDatasFoldouts[i].transitionDatasTypes[s]);

    //                    Debug.Log(transitionDatasFoldouts[i].transitionDatasTypes[s]);
    //                    GenericBarUI g = (GenericBarUI)target;
    //                    serializedObject.ApplyModifiedProperties();
    //                  //  bool proceed = false;
                   
    //                    //proceed = g.test(i,s, (int)transitionDatasFoldouts[i].transitionDatasTypes[s]);
    //                    serializedObject.ApplyModifiedProperties();
    //                    EditorCoroutineUtility.StartCoroutine(DPopUp(true, (int)transitionDatasFoldouts[i].transitionDatasTypes[s], transitionsDataHoldersValues.GetArrayElementAtIndex(s)), this);
                        
    //                    GUI.color = color;
    //                }
    //            }
    //            EditorGUI.indentLevel--;
    //        }

    //        EditorGUI.indentLevel--;
    //    }

    //    serializedObject.ApplyModifiedProperties();
    //}
    //IEnumerator DPopUp(bool p_proceed,int p_t, SerializedProperty p_r)
    //{
    //    yield return new WaitUntil(() => p_proceed == true);
    //    //Debug.Log(p_proceed);
    //    switch (p_t)
    //    {
    //        case 0:

    //            break;
    //        case 1:
    //           // Debug.Log("RAAA");
    //            InstantColorData(p_r);
    //            break;

    //        case 2:

    //            ColorTransitionData(p_r);
    //            break;

    //        case 3:
    //            FadeTransitionData(p_r);
    //            break;

    //        case 4:
    //            FillTransitionData(p_r);
    //            break;
    //    }
    //}
    //void DoTweenTransitionData(SerializedProperty p_serializedObject)
    //{
    //    var bar = p_serializedObject.FindPropertyRelative("joinToNextTransition");

    //    EditorGUILayout.PropertyField(bar, new GUIContent("joinToNextTransition"));

    //    var bard = p_serializedObject.FindPropertyRelative("waitToFinish");

    //    EditorGUILayout.PropertyField(bard, new GUIContent("waitToFinish"));

    //    var barr = p_serializedObject.FindPropertyRelative("delayTimeToNextTransition");

    //    EditorGUILayout.PropertyField(bar, new GUIContent("delayTimeToNextTransition"));


    //}
    //void InstantColorData(SerializedProperty p_serializedObject)
    //{
    //    EditorGUILayout.ColorField("Amount", p_serializedObject.FindPropertyRelative("color").colorValue);
    //    EditorGUILayout.PropertyField(p_serializedObject.FindPropertyRelative("transitionTime"));

    //    //EditorGUILayout.ColorField("Amount", p_serializedObject.FindPropertyRelative("color").colorValue);

    //}

    //void ColorTransitionData(SerializedProperty p_serializedObject)
    //{
   
    //    DoTweenTransitionData(p_serializedObject);

    //    EditorGUILayout.ColorField("Amount", p_serializedObject.FindPropertyRelative("color").colorValue);
    //    EditorGUILayout.PropertyField(p_serializedObject.FindPropertyRelative("transitionTime"));
    //    //// Store the hasMagic bool as a serializedProperty so we can access it
    //    //SerializedProperty hasMagicProperty = serializedObject.FindProperty("hasMagic");

    //    //// Draw a property for the hasMagic bool
    //    //EditorGUILayout.PropertyField(hasMagicProperty);

    //    //// Check if hasMagic is true
    //    //if (hasMagicProperty.boolValue)
    //    //{
    //    //    EditorGUILayout.PropertyField(serializedObject.FindProperty("mana"));
    //    //    EditorGUILayout.PropertyField(serializedObject.FindProperty("magicType"));
    //    //    EditorGUILayout.PropertyField(serializedObject.FindProperty("magicDamage"));
    //    //}


    //}

    //void FadeTransitionData(SerializedProperty p_serializedObject)
    //{

    //    DoTweenTransitionData(p_serializedObject);

    //    EditorGUILayout.PropertyField(p_serializedObject.FindPropertyRelative("amount"));
    //    EditorGUILayout.PropertyField(p_serializedObject.FindPropertyRelative("transitionTime"));

    //}

    //void FillTransitionData(SerializedProperty p_serializedObject)
    //{

    //    DoTweenTransitionData(p_serializedObject);

    //    EditorGUILayout.ColorField("Amount", p_serializedObject.FindPropertyRelative("color").colorValue);
    //    EditorGUILayout.PropertyField(p_serializedObject.FindPropertyRelative("transitionTime"));

    //}

   
}
