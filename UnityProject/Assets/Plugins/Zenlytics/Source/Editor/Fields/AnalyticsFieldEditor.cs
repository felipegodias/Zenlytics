using System;
using System.Reflection;

using UnityEditor;

using Zenlytics.Editor.Utils;

namespace Zenlytics.Editor.Fields
{

    public static class AnalyticsFieldEditor
    {

        public static void DrawAnalyticsField(SerializedProperty serializedProperty, Type signalType)
        {
            SerializedProperty useConstant = serializedProperty.FindPropertyRelative("m_UseConstant");
            SerializedProperty value = serializedProperty.FindPropertyRelative("m_Value");
            SerializedProperty argumentIndex = serializedProperty.FindPropertyRelative("m_ArgumentIndex");
            SerializedProperty argumentProperty = serializedProperty.FindPropertyRelative("m_ArgumentProperty");

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField($":: {serializedProperty.displayName} ::", EditorStyles.centeredGreyMiniLabel);

            Type baseType = signalType.BaseType;
            int argumentCount = baseType.GenericTypeArguments.Length;

            if (argumentCount > 1)
            {
                EditorGUILayout.PropertyField(useConstant);
            }
            else
            {
                useConstant.boolValue = true;
            }

            if (useConstant.boolValue)
            {
                EditorGUILayout.PropertyField(value);
            }
            else
            {
                Type argumentType = EditorGUILayoutUtils.DrawSignalArgumentsPopup(argumentIndex, signalType);

                if (argumentType != null)
                {
                    BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
                    Type parentType = serializedProperty.serializedObject.targetObject.GetType();
                    FieldInfo typeFieldInfo = parentType.GetField(serializedProperty.propertyPath, bindingFlags);
                    Type fieldType = typeFieldInfo.FieldType;
                    PropertyInfo propertyInfo =
                        fieldType.GetProperty("FieldTypes", BindingFlags.Static | BindingFlags.Public);
                    var fieldTypes = (Type[]) propertyInfo.GetValue(null);

                    EditorGUILayoutUtils.DrawPropertyPopup(argumentProperty, argumentType, fieldTypes, bindingFlags);
                }
            }

            EditorGUILayout.EndVertical();
        }

    }

}