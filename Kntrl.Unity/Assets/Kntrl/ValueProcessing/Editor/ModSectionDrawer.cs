using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR1
using UnityEditor;
[CustomPropertyDrawer(typeof(ModSection))]
public class ModSectionDrawer : PropertyDrawer
{
    SerializedProperty useCurve;
    SerializedProperty useDamper;
    SerializedProperty useDelay;
    SerializedProperty useInputMap;
    SerializedProperty useOutputClamp;
    SerializedProperty useOvershoot;
    SerializedProperty invert;
    SerializedProperty square;

    SerializedProperty animationCurve;
    SerializedProperty damper;
    SerializedProperty delayValue;
    SerializedProperty inputMap;
    SerializedProperty outputMap;
    SerializedProperty clamp;
    SerializedProperty overShootAmount;
SerializedProperty overShootFade;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);
        if (useCurve == null) useCurve = property.FindPropertyRelative("useCurve");
        if (useDamper == null) useDamper = property.FindPropertyRelative("useDamper");
        if (useDelay == null) useDelay = property.FindPropertyRelative("useDelay");
        if (useInputMap == null) useInputMap = property.FindPropertyRelative("useInputMap");
        if (useOutputClamp == null) useOutputClamp = property.FindPropertyRelative("useOutputClamp");
        if (useCurve == null) useCurve = property.FindPropertyRelative("useCurve");

        if (useOvershoot == null) useOvershoot = property.FindPropertyRelative("useOvershoot");

        EditorGUILayout.PropertyField(useCurve);
        if (useCurve.boolValue)
        {
            if (animationCurve == null) animationCurve = property.FindPropertyRelative("animationCurve");
            EditorGUILayout.PropertyField(animationCurve);
        }


        EditorGUILayout.PropertyField(useDamper);
        if (useDamper.boolValue)
        {
            if (damper == null) damper = property.FindPropertyRelative("damper");
            EditorGUILayout.PropertyField(damper);
        }

        EditorGUILayout.PropertyField(useDelay);
        if (useDelay.boolValue)
        {
            if (delayValue == null) delayValue = property.FindPropertyRelative("delayValue");
            EditorGUILayout.PropertyField(delayValue);
        }




        EditorGUILayout.PropertyField(useInputMap);
        if (useInputMap.boolValue)
        {
            if (inputMap == null) inputMap = property.FindPropertyRelative("inputMap");
            EditorGUILayout.PropertyField(inputMap);
        }
        EditorGUILayout.PropertyField(useOvershoot);
        if (useOvershoot.boolValue)
        {
            GUILayout.Label("use delay to get the oteher factor");
            if (overShootAmount == null) overShootAmount = property.FindPropertyRelative("overShootAmount");
            EditorGUILayout.PropertyField(overShootAmount);

                if (damper == null) damper = property.FindPropertyRelative("damper");
            EditorGUILayout.PropertyField(damper);

//   GUILayout.Label("We substract delayed value to simulate overshoot");
//               if (delayValue == null) delayValue = property.FindPropertyRelative("delayValue");
//             EditorGUILayout.PropertyField(delayValue);
        }

        EditorGUILayout.PropertyField(useOutputClamp);
        if (useOutputClamp.boolValue)
        {
            if (clamp == null) clamp = property.FindPropertyRelative("clamp");
            EditorGUILayout.PropertyField(clamp);
        }
        if (outputMap == null) outputMap = property.FindPropertyRelative("outputMap");
        EditorGUILayout.PropertyField(outputMap);

        if (square == null) square = property.FindPropertyRelative("square");
        EditorGUILayout.PropertyField(square);


        if (invert == null) invert = property.FindPropertyRelative("invert");
        EditorGUILayout.PropertyField(invert);


        EditorGUI.EndProperty();
    }
}

#endif
